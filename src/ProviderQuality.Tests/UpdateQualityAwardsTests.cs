using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderQuality.Console;

namespace ProviderQuality.Tests
{
    [TestClass]
    public class UpdateQualityAwardsTests
    {

        [TestMethod]
        public void TestImmutabilityOfBlueDistinctionPlus()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Distinction Plus", ExpiresIn = 0, Quality = 80}
                }
            };

            Assert.IsTrue(app.Awards.Count == 1);
            Assert.IsTrue(app.Awards[0].Name == "Blue Distinction Plus");
            Assert.IsTrue(app.Awards[0].Quality == 80);

            app.UpdateQuality();

            Assert.IsTrue(app.Awards[0].Quality == 80);
        }

        [TestMethod]
        public void TestBlueFirstQualityValue()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue First", ExpiresIn = 2, Quality = 0},
                    new Award {Name = "Blue First", ExpiresIn = 0, Quality = 2},
                    new Award {Name = "Blue First", ExpiresIn = -1, Quality = -1}
                }

            };

            app.UpdateQuality();

            Assert.IsTrue(app.Awards[0].Quality == 1);
            Assert.IsTrue(app.Awards[1].Quality == 3);
            Assert.IsTrue(app.Awards[2].Quality == 1);
        }

        public void TestBlueCompareQualityValue()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", ExpiresIn = 11, Quality = 0},
                    new Award {Name = "Blue Compare", ExpiresIn = 7, Quality = 0},
                    new Award {Name = "Blue Compare", ExpiresIn = 4, Quality = 0},
                    new Award {Name = "Blue Compare", ExpiresIn = 0, Quality = 0}
                }

            };

            app.UpdateQuality();

            Assert.IsTrue(app.Awards[0].Quality == 1);
            Assert.IsTrue(app.Awards[0].Quality == 2);
            Assert.IsTrue(app.Awards[0].Quality == 3);
            Assert.IsTrue(app.Awards[0].Quality == 0);
        }

        [TestMethod]
        public void TestBlueStarQualityValue()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Star", ExpiresIn = 2, Quality = 10},
                    new Award {Name = "Blue Star", ExpiresIn = 0, Quality = 8}
                }

            };

            app.UpdateQuality();

            Assert.IsTrue(app.Awards[0].Quality == 8);
            Assert.IsTrue(app.Awards[1].Quality == 4);
        }

        [TestMethod]
        public void TestDecreasingQualityValueNormalBrands()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 10, Quality = 20},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 5, Quality = 7},
                    new Award {Name = "Top Connected Providres", ExpiresIn = 3, Quality = 6},
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 0, Quality = 20},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 0, Quality = 7},
                    new Award {Name = "Top Connected Providres", ExpiresIn = 0, Quality = 6}
                }

            };

            app.UpdateQuality();

            Assert.IsTrue((app.Awards[0].Quality + 1) == 20);
            Assert.IsTrue((app.Awards[1].Quality + 1) == 7);
            Assert.IsTrue((app.Awards[2].Quality + 1) == 6);
            Assert.IsTrue(app.Awards[3].Quality == 18);
            Assert.IsTrue(app.Awards[4].Quality == 5);
            Assert.IsTrue(app.Awards[5].Quality == 4);
        }

        [TestMethod]
        public void TestQualityValueIsValid()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 10, Quality = 20},
                    new Award {Name = "Blue First", ExpiresIn = 2, Quality = 0},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 5, Quality = 7},
                    new Award {Name = "Blue Distinction Plus", ExpiresIn = 0, Quality = 80},
                    new Award {Name = "Blue Compare", ExpiresIn = 15, Quality = 20},
                    new Award {Name = "Top Connected Providres", ExpiresIn = 3, Quality = 6},
                    new Award {Name = "Blue Star", ExpiresIn = 3, Quality = 6}
                }

            };

            app.UpdateQuality();

            foreach (Award item in app.Awards)
            {
                if (item.Name != "Blue Distinction Plus")
                {
                    Assert.IsTrue(item.Quality < 50);
                }
                Assert.IsTrue(item.Quality >= 0);
            }
        }

        [TestMethod]
        public void TestDecreasingExpirationDate()
        {
            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 10, Quality = 20},
                    new Award {Name = "Blue First", ExpiresIn = 2, Quality = 0},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 5, Quality = 7},
                    new Award {Name = "Blue Distinction Plus", ExpiresIn = 0, Quality = 80},
                    new Award {Name = "Blue Compare", ExpiresIn = 15, Quality = 20},
                    new Award {Name = "Top Connected Providres", ExpiresIn = 3, Quality = 6},
                    new Award {Name = "Blue Star", ExpiresIn = 3, Quality = 6}
                }

            };

            app.UpdateQuality();

            Assert.IsTrue((app.Awards[0].ExpiresIn + 1) == 10);
            Assert.IsTrue((app.Awards[1].ExpiresIn + 1) == 2);
            Assert.IsTrue((app.Awards[2].ExpiresIn + 1) == 5);
            Assert.IsTrue((app.Awards[3].ExpiresIn + 1) == 0);
            Assert.IsTrue((app.Awards[4].ExpiresIn + 1) == 15);
            Assert.IsTrue((app.Awards[5].ExpiresIn + 1) == 3);
            Assert.IsTrue((app.Awards[6].ExpiresIn + 1) == 3);

        }
    }
}
