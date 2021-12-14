using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Chess;
using OOPChessProject;
using gColor = Chess.Pieces.Color;

/*
"RGB Hex","RGB Hex3","HSL","RGB","HTML Keyword"
"#4fb17c","#5b8","hsl(147,38,50)","rgb(79,177,124)","mediumseagreen"
"#0c0636","#103","hsl(247,80,11)","rgb(12,6,54)","black"
"#000000","#000","hsl(0,0,0)","rgb(0,0,0)","black"
"#095169","#157","hsl(195,84,22)","rgb(9,81,105)","darkslategray"
"#059b9a","#0aa","hsl(179,93,31)","rgb(5,155,154)","darkcyan"
"#53ba83","#5c8","hsl(147,42,52)","rgb(83,186,131)","mediumseagreen"
"#9fd96b","#ae7","hsl(91,59,63)","rgb(159,217,107)","lightgreen"

*/

namespace chessGUI
{
    //TODO fix castling long 

    public partial class MainWindow : Window
    {
        readonly SolidColorBrush ColorDarkFields = new SolidColorBrush(Color.FromArgb(150, 12, 6, 54));
        readonly SolidColorBrush ColorBrightField = new SolidColorBrush(Color.FromArgb(255, 233, 212, 136));

        readonly SolidColorBrush ColorSelection = new SolidColorBrush(Color.FromArgb(190, 1, 227, 182));
        readonly SolidColorBrush ColorPossibleMoves = new SolidColorBrush(Color.FromArgb(255, 1, 227, 182));
        private SolidColorBrush ColorControls = new SolidColorBrush(Color.FromArgb(255, 83, 186, 131));
        readonly SolidColorBrush ColorActiveChessClock = new SolidColorBrush(Color.FromArgb(150, 1, 150, 182));
        readonly SolidColorBrush ColorDeactivatedChessClock = new SolidColorBrush(Color.FromArgb(255, 159, 150, 150));
        private ChessGame m_ChessGame;
        private List<Move> moves;
        private Stopwatch stopwatch = new Stopwatch();
        public Row m_r;
        public DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            //bei WPF klasse immer initComp machen...
            InitializeComponent();
            initGUI();
            Stopwatch stopwatch = new Stopwatch();
        }

        public void initGUI()
        {
            
            Uri uri = new Uri($"C://Users//eduard.krutitsky//Pictures//StartRed.png");
            var starticon = new Image();
            starticon.Source = new BitmapImage(uri);
            Starter.Content = starticon;

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
                        r1.Background = ColorDarkFields;
                    }
                    else
                    {
                        r1.Background = ColorBrightField;
                    }
                    r1.Name = $"{col}{r + 1}";
                    r1.Click += FieldButton_Click;
                    //r1.PreviewMouseLeftButtonDown;
                    //r1.PreviewMouseLeftButtonUp;

                    //in XAML registration is automated, but not if
                    //the elements are created programmatically
                    //so you need to register them

                    RegisterName(r1.Name, r1);
                    ChessBoardGrid.Children.Add(r1);
                }

            }
        }

        public void setInitialTimersInGUI()
        {
            int minutes = m_ChessGame.Player1.TimeLeftInSeconds / 60;
            int seconds = m_ChessGame.Player1.TimeLeftInSeconds % 60;
            ChessClockWhite.Content = $"{minutes:D2}:{seconds:D2}";

            minutes = m_ChessGame.Player2.TimeLeftInSeconds / 60;
            seconds = m_ChessGame.Player2.TimeLeftInSeconds % 60;
            ChessClockBlack.Content = $"{minutes:D2}:{seconds:D2}";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RefreshPlayerTimers();
        }
        private void FieldButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string field = b.Name;
            Field inputField = HelperFunctions.StringToField(field);
            //First Input
            if (Controller.isFieldValid(m_ChessGame, inputField) && m_ChessGame.IfFirstInputTrueElseFalse)
            {
                b.Background = ColorSelection;
                moves = Controller.getMovesForField(m_ChessGame, inputField);
                if (moves.Count == 0)
                {
                    return;
                }
                foreach (var m in moves)
                {
                    Button ButtonGoToField = this.FindName(m.ToField.ToString()) as Button;
                    ButtonGoToField.Background = ColorPossibleMoves;
                }
                m_ChessGame.IfFirstInputTrueElseFalse = false;
            }
            //Second Input
            if (!m_ChessGame.IfFirstInputTrueElseFalse && Controller.IsALegalMove(moves, field))
            {
                Move chosenMove;
                    if (Controller.TryGetMoveFromListBasedOnDestinationField(moves, field, out chosenMove))
                    {
                        //TODO Put somewhere else
                        RefreshPlayerTimers();
                        m_ChessGame.TurnCounter += 1;
                        m_ChessGame.AddMoveToMoveList(new Move("x",chosenMove.FromField, chosenMove.ToField, chosenMove.MovementType));
                        UpdateMovesList();

                        m_ChessGame.CurrentChessBoard.MovePiece(chosenMove.FromField, chosenMove.ToField, chosenMove.MovementType, m_ChessGame.TurnCounter);
                        m_ChessGame.IfFirstInputTrueElseFalse = true;
                        
                        //Switching players turn
                        Controller.switchPlayerTurn(m_ChessGame);
                        UpdatePlayerTimerColors();

                        //Removing en passant pawns
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



            UpdatePlayerTimerColors();

            if (!dispatcherTimer.IsEnabled)
            {
                
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }


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


            //TimeTracking;
            stopwatch.Start();

        }
        public void RefreshPlayerTimers()
        {
            int minutes;
            int seconds;

            Console.WriteLine(stopwatch.Elapsed);
            if (m_ChessGame.CurrentPlayer.Color == gColor.White)
            {
                m_ChessGame.Player1.TimeLeftInSeconds -= 1;
                minutes = m_ChessGame.Player1.TimeLeftInSeconds / 60;
                seconds = m_ChessGame.Player1.TimeLeftInSeconds % 60;
                ChessClockWhite.Content = $"{minutes:D2}:{seconds:D2}";
            }
            else
            {
                m_ChessGame.Player2.TimeLeftInSeconds -= 1;
                minutes = m_ChessGame.Player2.TimeLeftInSeconds / 60;
                seconds = m_ChessGame.Player2.TimeLeftInSeconds % 60;
                ChessClockBlack.Content = $"{minutes:D2}:{seconds:D2}";
            }
        }
        public void UpdatePlayerTimerColors()
        {
            if (m_ChessGame.CurrentPlayer.Color == gColor.White)
            {
                ChessClockWhite.Background = ColorActiveChessClock;
                ChessClockBlack.Background = ColorDeactivatedChessClock;
            }
            else
            {
                ChessClockWhite.Background = ColorDeactivatedChessClock;
                ChessClockBlack.Background = ColorActiveChessClock;
            }

        }
        public void UpdateMovesList()
        {
            string color = "";
            if (m_ChessGame.CurrentPlayer.Color == gColor.Black)
            {
                color = "Black";
            }
            else
            {
                color = "White";
            }
            var lastItem = m_ChessGame.MovesHistory.Last();
            MovesList.Items.Add($"Move #{lastItem.Item1}: {lastItem.Item2.FromField.ToString()} --> {lastItem.Item2.ToField.ToString()} by {color}");
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
                img.Source = new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value.ToString()}.png"));
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
            l.Content = $"Current State:\n{m_ChessGame.GameState}";
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
                        b.Background = ColorDarkFields;
                    }

                    else
                    {
                        b.Background = ColorBrightField;
                    }
                }
            }
        }
    }
}

