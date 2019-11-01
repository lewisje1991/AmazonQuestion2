using AmazonQuestion2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CloudFrontTest
    {
        private Cloudfront _testSubject;

        [TestInitialize]
        public void Setup()
        {
            _testSubject = new Cloudfront();
        }

        [TestMethod]
        public void Test1()
        {
            var grid = new int[][] {
                new int[] {1, 0, 0, 0, 0},
                new int[] {0, 1, 0, 0, 0},
                new int[] {0, 0, 1, 0, 0},
                new int[] {0, 0, 0, 1, 0},
                new int[] {0, 0, 0, 0, 1},
            };

            Assert.AreEqual(4, _testSubject.TimeToPopulate(
                5,
                5,
                grid
            ));
        }

        [TestMethod]
        public void Test2()
        {
            var grid = new int[][] {
                    new int[] {0, 0, 1, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 0},
                    new int[] {0, 0, 0, 0, 0, 1},
                    new int[] {0, 0, 0, 0, 0, 0},
                    new int[] {0, 1, 0, 0, 0, 0},
            };

            Assert.AreEqual(3, _testSubject.TimeToPopulate(
                5,
                6,
                grid
            ));
        }
    }
}
