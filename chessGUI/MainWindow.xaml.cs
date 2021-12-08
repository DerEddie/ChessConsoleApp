using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess;
using OOPChessProject;
using gColor = Chess.Pieces.Color;

namespace ChessTest1
{
    //TODO fix castling long 

    public partial class MainWindow : Window
    {
        private SolidColorBrush DarkFieldsColor = new SolidColorBrush(Color.FromArgb(100, 0, 0, 92));
        SolidColorBrush BrightFieldColor = new SolidColorBrush(Color.FromArgb(100, 255, 166, 2));
        SolidColorBrush SelectionColor = new SolidColorBrush(Color.FromArgb(100, 227, 1, 45));
        SolidColorBrush PossibleMovesColor = new SolidColorBrush(Color.FromArgb(100, 1, 227, 182));
        SolidColorBrush ControlsColor = new SolidColorBrush(Color.FromArgb(100, 88, 80, 141));

        private ChessGame m_ChessGame;
        private List<Move> moves;

        public Row m_r;

        public MainWindow()
        {
            //bei WPF klasse immer initComp machen...
            InitializeComponent();
            initGUI();
        }

        public void initGUI()
        {
            ChessBoardGrid.Rows = 8;
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
                        r1.Background = DarkFieldsColor;
                    }
                    else
                    {
                        r1.Background = BrightFieldColor;
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
        }

        private void FieldButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string field = b.Name;
            Field inputField = Helperfunctions.StringToField(field);
            //First Input
            if (Controller.isFieldValid(m_ChessGame, inputField) && m_ChessGame.IfFirstInputTrueElseFalse)
            {
                b.Background = SelectionColor;
                moves = Controller.getMovesForField(m_ChessGame, inputField);
                if (moves.Count == 0)
                {
                    return;
                }


                foreach (var m in moves)
                {
                    Button ButtonGoToField = this.FindName(m.ToField.ToString()) as Button;
                    ButtonGoToField.Background = PossibleMovesColor;
                }
                m_ChessGame.IfFirstInputTrueElseFalse = false;
            }
            //Second Input
            if (!m_ChessGame.IfFirstInputTrueElseFalse && Controller.IsALegalMove(moves, field))
            {
                Move chosenMove;
                    if (Controller.TryGetMoveFromListBasedOnDestinationField(moves, field, out chosenMove))
                    {
                        m_ChessGame.TurnCounter += 1;
                        m_ChessGame.CurrentChessBoard.MovePiece(chosenMove.FromField, chosenMove.ToField, chosenMove.MovementType, m_ChessGame.TurnCounter);
                        m_ChessGame.IfFirstInputTrueElseFalse = true;
                        
                        Controller.switchPlayerTurn(m_ChessGame);
                        m_ChessGame.CurrentChessBoard.RemoveSomeGhosts(m_ChessGame.TurnCounter);

                        //After all the logic refresh board...
                        RefreshChessBoard();
                        ResetColor();
                        
                        //DETERMINE Whether game will continue
                        Controller.UpdateGameState(m_ChessGame);
                        

                    }
                    
            }


            UpdateStatusBar();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyChessBoard();
            m_ChessGame = Controller.InitChessGame();

            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                // From inside the custom Button type:
                Button b = this.FindName(kvp.Key.ToString()) as Button;
                //b.Content = kvp.Value.ToString();
                string s = "";
                if (kvp.Value.PieceColor == gColor.White)
                {
                    s = "W";
                }
                else
                {
                    s = "B";
                }

                Image img = new Image();
                img.Source =
                    new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value.ToString()}.png"));
                b.Content = img;
            }


        }

        private void RefreshChessBoard()
        {
            EmptyChessBoard();
            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                ResetColor();
                // From inside the custom Button type:
                var b = this.FindName(kvp.Key.ToString()) as Button;
                //b.Content = kvp.Value.ToString();
                var s = "";
                if (kvp.Value.PieceColor == gColor.White)
                {
                    s = "W";
                }
                else
                {
                    s = "B";
                }

                var img = new Image();
                img.Source =
                    new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value.ToString()}.png"));
                b.Content = img;
            }
        }

        private void EmptyChessBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col) c;
                    string Name = $"{col}{r + 1}";
                    Button b = this.FindName(Name) as Button;
                    b.Content = "";
                }
            }
        }

        public void UpdateStatusBar()
        {
            var l = this.FindName("StatusBar") as Label;
            l.Content = $" {m_ChessGame.CurrentPlayer.Color}'s Turn \n Current State:{m_ChessGame.GameState} \n Move Counter:{m_ChessGame.TurnCounter}";
        }

        public void ResetColor()
        {

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col)c;

                    string Name = $"{col}{r + 1}";
                    Button b = this.FindName(Name) as Button;

                    if ((r + c) % 2 == 0)
                    {
                        b.Background = DarkFieldsColor;
                    }

                    else
                    {
                        b.Background = BrightFieldColor;
                    }
                }
            }
        }

    }
}
