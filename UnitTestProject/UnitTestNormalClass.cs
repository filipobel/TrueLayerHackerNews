using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;
using TrueLayerHackerNews;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestNormalClass
    {
        [TestMethod]
        public void TestReturnStoryModel1()
        {
            TrueLayerHackerNews.TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews.TrueLayerHackerNews();
            WebClient webclient = new WebClient();
            trueLayerHackerNews.returnStoryModels(1, webclient);

            Assert.AreEqual(trueLayerHackerNews.storyList.Count, 1);
        }

        [TestMethod]
        public void TestReturnStoryModel2()
        {
            TrueLayerHackerNews.TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews.TrueLayerHackerNews();
            WebClient webclient = new WebClient();
            trueLayerHackerNews.returnStoryModels(2, webclient);

            Assert.AreEqual(trueLayerHackerNews.storyList.Count, 2);
        }

        [TestMethod]
        public void TestReturnStoryModel100()
        {
            TrueLayerHackerNews.TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews.TrueLayerHackerNews();
            WebClient webclient = new WebClient();
            trueLayerHackerNews.returnStoryModels(100, webclient);

            Assert.AreEqual(trueLayerHackerNews.storyList.Count, 100);
        } 

        [TestMethod]
        public void TestGetStoryJsonStory()
        {
            //Testing that the following Json is returned:
            /*
             * {
                "by" : "dhouston",
                "descendants" : 71,
                 "id" : 8863,
                "kids" : [ 8952, 9224, 8917, 8884, 8887, 8943, 8869, 8958, 9005, 9671, 8940, 9067, 8908, 9055, 8865, 8881, 8872, 8873, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902, 8907, 8894, 8878, 8870, 8980, 8934, 8876 ],
                "score" : 111,
                "time" : 1175714200,
                "title" : "My YC app: Dropbox - Throw away your USB drive",
                "type" : "story",
                "url" : "http://www.getdropbox.com/u/2/screencast.html"
                }
             */
            TrueLayerHackerNews.TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews.TrueLayerHackerNews();
            WebClient webclient = new WebClient();
            RetrieveStoryModel rsm = trueLayerHackerNews.getStoryJson("8863", webclient);

            Assert.AreEqual(rsm.Id, 8863);
            Assert.AreEqual(rsm.Descendants, 71);
            Assert.AreEqual(rsm.hackerNewsType, "story");
        }

        [TestMethod]
        public void TestGetTopStories()
        {
            //Test that running the get top stories return a list of 500 strings
            const string testUrl = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
            TrueLayerHackerNews.TrueLayerHackerNews trueLayerHackerNews = new TrueLayerHackerNews.TrueLayerHackerNews();
            WebClient webclient = new WebClient();

            List<string> returnList = trueLayerHackerNews.getObejctFromAPI<List<string>>(testUrl, webclient);

            Assert.AreEqual(returnList.Count, 500);
        }
    }
}
