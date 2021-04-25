using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;
using TrueLayerHackerNews;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestRetriveStoryModel
    {
        [TestMethod]
        public void TestUrlPass()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Url = "https://www.google.co.uk/";

            Assert.IsTrue(rsm.testURL());
        }

        [TestMethod]
        public void TestURLFail()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Url = "http:\\host/path/file";

            Assert.IsFalse(rsm.testURL());
        }

        [TestMethod]
        public void TestScorePass()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Score = 55;

            Assert.IsTrue(rsm.testScore());
        }

        [TestMethod]
        public void TestScoreFail()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Score = -55;

            Assert.IsFalse(rsm.testScore());
        }

        [TestMethod]
        public void TestScoreLimit()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Score = 0;

            Assert.IsTrue(rsm.testScore());
        }

        [TestMethod]
        public void TestDescendantsPass()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Descendants = 55;

            Assert.IsTrue(rsm.testDescendants());
        }

        [TestMethod]
        public void TestDescendantsFail()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Descendants = -55;

            Assert.IsFalse(rsm.testDescendants());
        }

        [TestMethod]
        public void TestDescendantsLimit()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Descendants = 0;

            Assert.IsTrue(rsm.testDescendants());
        }

        public void TestTypePass()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.hackerNewsType = "story";

            Assert.IsTrue(rsm.testType());
        }

        [TestMethod]
        public void TestTypeFail()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.hackerNewsType = "job";

            Assert.IsFalse(rsm.testType());
        }

        [TestMethod]
        public void TestStoryPass()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Url = "https://www.google.co.uk/";
            rsm.Score = 55;
            rsm.Descendants = 55;
            rsm.hackerNewsType = "story";

            Assert.IsTrue(rsm.testStory());
        }

        [TestMethod]
        public void TestStoryFail()
        {
            RetrieveStoryModel rsm = new RetrieveStoryModel();
            rsm.Url = "https://www.google.co.uk/";
            rsm.Score = 55;
            rsm.Descendants = 55;
            rsm.hackerNewsType = "job";

            Assert.IsFalse(rsm.testStory());
        }
    }
}
