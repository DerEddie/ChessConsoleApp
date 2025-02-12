﻿using Chess;
using NUnit.Framework;
using System;
namespace OOPChessProject.Tests
{
    [TestFixture]
    internal class TestPins
    {
        [Test]
        public void PawnsPinned_GetMovesForField_CantExposeKingByMoving()
        {
            const string s = "4k3/8/4r3/b7/5n2/4P3/3P1P2/4K3";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D2"));
            Assert.AreEqual(0, fields.Count);
        }
        
        [Test]
        public void PawnsPinned_GetMovesForField_CantExposeKingByCapturing()
        {
            const string s = "4k3/8/4r3/b7/5n2/4P3/3P1P2/4K3";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("E3"));
            Assert.AreEqual(1, fields.Count);
        }
    }
}
