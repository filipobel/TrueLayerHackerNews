using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrueLayerHackerNews
{
    public class RetrieveStoryModel
    {
        // The RetrieveStoryModel is different from the OutputStoryModel to allow for different json variable names
        // between what the api returns and what is expected in the challenge


        [JsonProperty("by")]
        public string Author { get; set; }

        [JsonProperty("descendants")]
        public long Descendants { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("kids")]
        public List<long> Kids { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("time")]
        public long CreatedAt_UnixTime { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string hackerNewsType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }


        public bool testStory()
        {
            /*  Method to check if the current object is a valid object as per the Requirements of Tech Spec
             *  URL must a valid URI
             *  Score must be >=0
             *  Descendants must be >=0
             *  it most be a Story
             *  Return true if this is a valid story
             */
            return testURL() && testScore() && testDescendants() && testType();
        }

        #region testingMethods
        public bool testURL() 
        {
            //Testing that the url is a valid URI as per https://tools.ietf.org/html/rfc3986
            Uri uriResult;
            bool result = Uri.TryCreate(this.Url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                && Uri.IsWellFormedUriString(this.Url,UriKind.Absolute);
            return result;
        }

        public bool testScore()
        {
            //Testing that the score is >=0
            return this.Score >= 0;
        }

        public bool testDescendants()
        {
            //Testing that the score is >=0
            return this.Descendants >= 0;
        }

        public bool testType()
        {
            // Testing that this of a storyType
            // The available typs are one of "job", "story", "comment", "poll", or "pollopt".

            return String.Equals(this.hackerNewsType.ToLower(), "story");
        }
        #endregion
    }
}
