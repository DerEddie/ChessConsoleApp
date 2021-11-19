
using NUnit.Framework;
using System;

namespace OOPChessProject.Tests
{
    //decorator = special attribute
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(2, 1 + 1);

            //Assert.istrue;

        }

        [Test]
        public void TestMethod2()
        {
            Field f = new Field(row._1, col.A);
            Assert.AreEqual(4, 5);
        }
    }
}
