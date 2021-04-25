using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrueLayerHackerNews
{
    public class TrueLayerHackerNews
    {
        public TrueLayerHackerNews()
        {
            storyList = new List<ReturnStoryModel>();
            webClient = new WebClient();
        }

        static readonly string topStoriesURL = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        public List<ReturnStoryModel> storyList { get; set; }

        private WebClient webClient { get; set; }
        static void Main(string[] args)
        {
            TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews();
            int numberOfStories = 0;
            bool inputCorrect = false;
            //Checking user input
            while (!inputCorrect)
            {
                /*bool firstTest = args.Length != 2;
                bool secondTest = args[0] == "--posts";
                bool thirdTest = !Int32.TryParse(args[1], out numberOfStories);
                bool fouthTest = numberOfStories > 100; */
                if (args.Length != 2 || args[0] != "--posts" || !Int32.TryParse(args[1], out numberOfStories) || numberOfStories > 100)
                {
                    Console.WriteLine("Please enter a valid format of arguments in the following format:");
                    Console.WriteLine("--posts n \n Where posts how many posts to print. N = positive integer <= 100");
                    Console.WriteLine("Please enter new args");
                    args = Console.ReadLine().Split(" ");
                }
                else
                {
                    inputCorrect = true;
                }
            }
            Console.WriteLine("Your json file is being created please stand by...");

            trueLayerHackerNews.returnStoryModels(numberOfStories);


            string TestPath = AppDomain.CurrentDomain.BaseDirectory + numberOfStories + "HackerNewsStories.json";

            trueLayerHackerNews.writeJSONtoFile(TestPath);

            Console.WriteLine("Your file has been created at been create at " + TestPath);
            Console.WriteLine("press any button followed by enter to exit the program");
            Console.ReadLine();
        }

        public void returnStoryModels(int numberOfStories)
        {
            /*
             * This Method populates the storyList with the request number of ReturnStoryModel classes
             * that can then be seralized into a json object to output to a file
             */
            List<string> topStories = getObejctFromAPI<List<string>>(topStoriesURL);

            int returnStories = 1;
            int retrieveStoryNumber = 0;
            while (returnStories <= numberOfStories)
            {
                RetrieveStoryModel retrieveStoryModel = getStoryJson(topStories[retrieveStoryNumber]);
                retrieveStoryNumber++;

                if (retrieveStoryModel.testStory())
                {
                    ReturnStoryModel returnStoryModel = new ReturnStoryModel(retrieveStoryModel, returnStories);
                    storyList.Add(returnStoryModel);
                    returnStories++;
                }
            }


        }

        public   RetrieveStoryModel getStoryJson(string storyNumber)
        {
            //Method to return a RetrieveStoryModel from the API
            const string FIRSTHALFURL = "https://hacker-news.firebaseio.com/v0/item/";
            const string SECONDHALFURL = ".json?print=pretty";

            string apiUrl = FIRSTHALFURL + storyNumber + SECONDHALFURL;


            return getObejctFromAPI<RetrieveStoryModel>(apiUrl);
        }

        public  T getObejctFromAPI<T>(string url)
        {
            //Return a spefied object from the HackerNews/Firebase api
            string jsonString = webClient.DownloadString(url);

            return JsonConvert.DeserializeObject<T>(jsonString);

        }

        private void writeJSONtoFile(string path)
        {
            //Outputs the list of stories to the given path

            File.WriteAllText(path, JsonConvert.SerializeObject(storyList, Formatting.Indented));
        }



    }

}
