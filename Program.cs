using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrueLayerHackerNews
{
    class Program
    {
        static readonly string topStoriesURL = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        static readonly HttpClient _client = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };
        //static readonly string testString = "https://hacker-news.firebaseio.com/v0/item/8863.json?print=pretty";
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            int numberOfStories = 20;
            List<ReturnStoryModel> storyList = new List<ReturnStoryModel>();

            List<string> topStories = getObejctFromAPI<List<string>>(topStoriesURL, webClient);

            int returnStories = 1;
            int retrieveStoryNumber = 0;
            while(returnStories <= numberOfStories ) { 
                RetrieveStoryModel retrieveStoryModel = getStoryJson(topStories[retrieveStoryNumber], webClient);
                retrieveStoryNumber++;

                if (retrieveStoryModel.testStory())
                {
                    ReturnStoryModel returnStoryModel = new ReturnStoryModel(retrieveStoryModel, returnStories);
                    storyList.Add(returnStoryModel);
                    returnStories++;
                    Console.Write(JsonConvert.SerializeObject(returnStoryModel, Formatting.Indented) + "\n");
                }
            }


        }

        public static RetrieveStoryModel getStoryJson(string storyNumber, WebClient webClient)
        {
            //Method to return a RetrieveStoryModel from the API
            const string FIRSTHALFURL = "https://hacker-news.firebaseio.com/v0/item/";
            const string SECONDHALFURL = ".json?print=pretty";

            string apiUrl = FIRSTHALFURL + storyNumber + SECONDHALFURL;


            return getObejctFromAPI<RetrieveStoryModel>(apiUrl, webClient);
        }

        public static T getObejctFromAPI<T>(string url, WebClient webClient)
        {
            //Return a spefied object from the HackerNews/Firebase api
            string jsonString = webClient.DownloadString(url);

            return JsonConvert.DeserializeObject<T>(jsonString);

        }

    }

}
