using System;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess;
using OOPChessProject;
using Color = Chess.Pieces.Color;

namespace ChessTest1
{

    

    public partial class MainWindow : Window
    {
        private bool isFirstInput;
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
                        r1.Background = Brushes.SaddleBrown;
                        r1.Foreground = Brushes.White;
                        
                    }

                    else
                    {
                        r1.Background = Brushes.Beige;
                    }
                    r1.Name = $"{col}{r + 1}";
                    r1.Click += FieldButton_Click;

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



        private void FieldButton_Click(object sender, RoutedEventArgs e)
        {
            
            string field = (sender as Button).Name.ToString();
            Button b = sender as Button;
            b.Background = Brushes.Red;

            if (isFirstInput)
            {

            }

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (Button cell in ChessBoardGrid.Children)
            {
                m_ChessGame = Controller.InitChessGame();
                cell.Name = "";
            }

            m_ChessGame = Controller.InitChessGame();
            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                // From inside the custom Button type:
                Button b = this.FindName(kvp.Key.ToString()) as Button;
                //b.Content = kvp.Value.ToString();

                string s = "";
                if (kvp.Value.PieceColor == Color.White)
                {
                    s = "W";
                }
                else
                {
                    s = "B";
                }

                Image img = new Image();
                img.Source = new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value.ToString()}.png"));
                b.Content = img;
            }


        }

        
    }
}
