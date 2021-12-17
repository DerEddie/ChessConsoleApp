using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using NUnit.Framework;

namespace OOPChessProject.Tests
{
    internal class GameLogicTests
    {
        [Test]
        public void KingCouldCaptureThePawn()
        {
            string s = "rnbqkbnr/pppp1ppp/8/4p2Q/2B1P3/8/PPPP1PPP/RNB1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("H5"), new Field("F7"),MovementType.Capturing,5);
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentPlayer = cG.Player2;
            var x = cG.CheckMitigationPossible();
            Console.WriteLine(x);
        }
        [Test]
        public void isChecked_CheckDetected()
        {
            string s = "r2qk2r/1ppppppp/8/8/8/7R/1PPPPPPP/4K2R";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            
        }
    }
}
