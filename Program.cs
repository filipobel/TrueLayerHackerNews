﻿using System;
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
        static List<ReturnStoryModel> storyList = new List<ReturnStoryModel>();
        static void Main(string[] args)
        {
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

            WebClient webClient = new WebClient();

            returnStoryModels(numberOfStories, webClient);


            string TestPath = AppDomain.CurrentDomain.BaseDirectory + numberOfStories + "HackerNewsStories.json";

            writeJSONtoFile(TestPath);

            Console.WriteLine("Your file at been create at " + TestPath);
            Console.WriteLine("press any button followed by enter to exit the program");
            Console.ReadLine();
        }

        private static void returnStoryModels(int numberOfStories, WebClient webClient)
        {
            /*
             * This Method populates the storyList with the request number of ReturnStoryModel classes
             * that can then be seralized into a json object to output to a file
             */
            List<string> topStories = getObejctFromAPI<List<string>>(topStoriesURL, webClient);

            int returnStories = 1;
            int retrieveStoryNumber = 0;
            while (returnStories <= numberOfStories)
            {
                RetrieveStoryModel retrieveStoryModel = getStoryJson(topStories[retrieveStoryNumber], webClient);
                retrieveStoryNumber++;

                if (retrieveStoryModel.testStory())
                {
                    ReturnStoryModel returnStoryModel = new ReturnStoryModel(retrieveStoryModel, returnStories);
                    storyList.Add(returnStoryModel);
                    returnStories++;
                }
            }


        }

        private static RetrieveStoryModel getStoryJson(string storyNumber, WebClient webClient)
        {
            //Method to return a RetrieveStoryModel from the API
            const string FIRSTHALFURL = "https://hacker-news.firebaseio.com/v0/item/";
            const string SECONDHALFURL = ".json?print=pretty";

            string apiUrl = FIRSTHALFURL + storyNumber + SECONDHALFURL;


            return getObejctFromAPI<RetrieveStoryModel>(apiUrl, webClient);
        }

        private static T getObejctFromAPI<T>(string url, WebClient webClient)
        {
            //Return a spefied object from the HackerNews/Firebase api
            string jsonString = webClient.DownloadString(url);

            return JsonConvert.DeserializeObject<T>(jsonString);

        }

        private static void writeJSONtoFile(string path)
        {
            //Outputs the list of stories to the given path
            List<string> outputStrings = new List<string>();
            foreach(ReturnStoryModel rsm in storyList)
            {
                outputStrings.Add(JsonConvert.SerializeObject(rsm, Formatting.Indented));
            }

            File.WriteAllLines(path, outputStrings.ToArray());
        }

    }

}
