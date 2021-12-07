using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chess;
using OOPChessProject;

namespace ChessTest1
{

    public partial class MainWindow : Window
    {

        private ChessGame m_ChessGame;
        
        public Row m_r;
        public MainWindow()
        {
            InitializeComponent();
            ChessBoardGrid.Rows = 9;
            ChessBoardGrid.Columns = 8;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col)c;
                    Row row = (Row)r;
                    Button r1 = new Button();
                    if ((r + c) % 2 == 0)
                    {
                        r1.Background = Brushes.Black;
                        r1.Foreground = Brushes.White;
                    }

                    else
                    {
                        r1.Background = Brushes.White;
                    }
                    r1.Name = $"{col}{r + 1}";
                    r1.Click += fieldButton_Click;

                    //in XAML registration is automated, but not if
                    //the elements are created programmatically
                    //so you need to register them

                    RegisterName(r1.Name, r1);
                    ChessBoardGrid.Children.Add(r1);
                }

            }
            Button startButton = new Button();
            startButton.Background = Brushes.LawnGreen;
            startButton.Content = "Start";
            ChessBoardGrid.Children.Add(startButton);
            startButton.Click += StartButton_Click;
        }



        private void fieldButton_Click(object sender, RoutedEventArgs e)
        {
            string field = (string)(sender as Button).Content;
            

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (Button cell in ChessBoardGrid.Children)
            {
                m_ChessGame = Controller.InitChessBoard();
                cell.Name = "";
            }

            m_ChessGame = Controller.InitChessBoard();
            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                // From inside the custom Button type:
                Button b = this.FindName(kvp.Key.ToString()) as Button;
                b.Content = kvp.Value.ToString();

            }


        }

        
    }
}
