using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DarkWing;
namespace Tests
{
    [TestClass]
    public class PositionTests
    {
        private Position a, b, c, d;
        public PositionTests()
        {
            a = new Position(1, 2);
            b = new Position(1, 2);
            c = new Position(2, 1);
            d = new Position(4, 3);
        }
        [TestMethod]
        public void TestNorme1()
        {
            Assert.AreEqual(3, a.Norme1());
            Assert.AreEqual(3, b.Norme1());
            Assert.AreEqual(7, d.Norme1());
        }
        [TestMethod]
        public void TestNorme2()
        {
            Assert.AreEqual(5, d.Norme2(), "erreur Norme 2");
        }
        [TestMethod]
        public void TestOpPlus()
        {
            Assert.AreEqual(new Position(3,3), b+c, "erreur op +");
        }
        [TestMethod]
        public void TestOpMoins()
        {
            Assert.AreEqual(new Position(0,0), b-a, "erreur op -");
        }
        [TestMethod]
        public void TestOpEgal()
        {
            Assert.AreEqual(true,a==b, "Erreur op ==");
            Assert.AreEqual(false, null == b, "op == ne gère pas les références nulles");
        }
        [TestMethod]
        public void TestOpDifferent()
        {
            Assert.AreEqual(true, b!=c, "Erreur op !=");
            Assert.AreEqual(true, null != c, "op != ne gère pas les références nulles");
        }
        [TestMethod]
        public void TestEquals()
        {
            Assert.AreEqual(true, a.Equals(a), "a et a sont différents");
            Assert.AreEqual(true, a.Equals(b),"a et b sont différents");
            Assert.AreEqual(true, b.Equals(a), "equals non commutatif");
            Assert.AreEqual(false, b.Equals(c),"b et c sont égaux");
            Assert.AreEqual(false, b.Equals(null), "la référence nulle n'est pas gérée");
            Assert.AreEqual(false, b.Equals(5), "les casts ne sont pas gérés");
        }
        [TestMethod]
        public void TestGetHashCode()
        {
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "hashcode différent pour positions égales");
            Assert.AreNotEqual(c.GetHashCode(), b.GetHashCode(), "hashcode trivialement surjectif");
        }
    }
}
