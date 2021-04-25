using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrueLayerHackerNews
{
    public class ReturnStoryModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uri")]
        public string Url { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("points")]
        public long Score { get; set; }

        [JsonProperty("comments")]
        public long Descendants { get; set; }

        [JsonProperty("rank")]
        public int rank { get; set; }

        public ReturnStoryModel(RetrieveStoryModel rSM, int rank)
        {
            this.Title = rSM.Title;
            this.Url = rSM.Url;
            this.Author = rSM.Author;
            this.Score = rSM.Score;
            this.Descendants = rSM.Descendants;
            this.rank = rank;
        }
    }
}
