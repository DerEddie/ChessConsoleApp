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



namespace chessGUI
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush _colorDarkFields = new SolidColorBrush(Color.FromArgb(150, 12, 6, 54));
        readonly SolidColorBrush _colorBrightField = new SolidColorBrush(Color.FromArgb(255, 233, 212, 136));
        readonly SolidColorBrush _colorSelection = new SolidColorBrush(Color.FromArgb(190, 1, 227, 182));
        readonly SolidColorBrush _colorPossibleMoves = new SolidColorBrush(Color.FromArgb(255, 1, 227, 182));
        private SolidColorBrush _colorControls = new SolidColorBrush(Color.FromArgb(255, 83, 186, 131));
        readonly SolidColorBrush _colorActiveChessClock = new SolidColorBrush(Color.FromArgb(150, 1, 150, 182));
        readonly SolidColorBrush _colorDeactivatedChessClock = new SolidColorBrush(Color.FromArgb(255, 159, 150, 150));
        private ChessGame m_ChessGame;
        private List<Move> _moves;
        private Stopwatch stopwatch = new Stopwatch();
        public Row m_r;
        public DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            //always init..
            InitializeComponent();
            InitGui();
        }
        public void InitGui()
        {
            Uri uri = new Uri("C://Users//eduard.krutitsky//Pictures//StartRed.png");
            var startIcon = new Image();
            startIcon.Source = new BitmapImage(uri);
            Starter.Content = startIcon;

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
                        r1.Background = _colorDarkFields;
                    }
                    else
                    {
                        r1.Background = _colorBrightField;
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

        public void MakeChessBoardGrey()
        {
            MessageBox.Show("Game Over!");
            SolidColorBrush darkGrayColor = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
            SolidColorBrush lightgreyColor = new SolidColorBrush(Color.FromArgb(255, 150, 150, 150));
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col)c;

                    string name = $"{col}{r + 1}";
                    Button b = this.FindName(name) as Button;

                    b.Background = (r + c) % 2 == 0 ? darkGrayColor : lightgreyColor;
                }
            }
            
            
        }
        public void SetInitialTimersInGui()
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
           
            if (sender is Button)
            {
                Button b = (Button) sender;
                string field = b.Name;
                Field inputField = HelperFunctions.StringToField(field);
                //First Input
                if (Controller.IsFieldValid(m_ChessGame, inputField) && m_ChessGame.IfFirstInputTrueElseFalse)
                {
                    b.Background = _colorSelection;
                    _moves = Controller.GetMovesForField(m_ChessGame, inputField);
                    if (_moves.Count == 0)
                    {
                        return;
                    }
                    foreach (var m in _moves)
                    {
                        if (this.FindName(m.ToField.ToString()) is Button buttonGoToField) buttonGoToField.Background = _colorPossibleMoves;
                    }
                    m_ChessGame.IfFirstInputTrueElseFalse = false;
                }
                //Second Input
                if (!m_ChessGame.IfFirstInputTrueElseFalse && Controller.IsALegalMove(_moves, field))
                {
                    if (Controller.TryGetMoveFromListBasedOnDestinationField(_moves, field, out var chosenMove))
                    {
                        //TODO Put somewhere else
                        RefreshPlayerTimers();
                        m_ChessGame.TurnCounter += 1;
                        m_ChessGame.AddMoveToMoveList(new Move("x",chosenMove.FromField, chosenMove.ToField, chosenMove.MovementType));
                        UpdateMovesList();
                        m_ChessGame.CurrentChessBoard.MovePiece(chosenMove.FromField, chosenMove.ToField, chosenMove.MovementType, m_ChessGame.TurnCounter);
                        m_ChessGame.IfFirstInputTrueElseFalse = true;
                        
                        //Switching players turn
                        Controller.SwitchPlayerTurn(m_ChessGame);
                        UpdatePlayerTimerColors();
                        //Removing en passant pawns
                        m_ChessGame.CurrentChessBoard.RemoveSomeGhosts(m_ChessGame.TurnCounter);
                        //After all the logic refresh board...
                        Controller.UpdateGameState(m_ChessGame);
                        if (m_ChessGame.GameState == GameState.Checkmate)
                        {
                            MakeChessBoardGrey();
                        }
                        RefreshChessBoard();
                        ResetColor();
                    }
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
                
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }


            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                // From inside the custom Button type:
                Button b = this.FindName(kvp.Key) as Button;
                //b.Content = kvp.Value.ToString();
                var s = kvp.Value.PieceColor == gColor.White ? "W" : "B";

                Image img = new Image
                {
                    Source = new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value}.png"))
                };
                if (b != null) b.Content = img;
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
                ChessClockWhite.Background = _colorActiveChessClock;
                ChessClockBlack.Background = _colorDeactivatedChessClock;
            }
            else
            {
                ChessClockWhite.Background = _colorDeactivatedChessClock;
                ChessClockBlack.Background = _colorActiveChessClock;
            }

        }
        public void UpdateMovesList()
        {
            var color = m_ChessGame.CurrentPlayer.Color == gColor.Black ? "Black" : "White";
            var lastItem = m_ChessGame.MovesHistory.Last();
            MovesList.Items.Add($"Move #{lastItem.Item1}: {lastItem.Item2.FromField} --> {lastItem.Item2.ToField} by {color}");
        }
        private void RefreshChessBoard()
        {
            EmptyChessBoard();
            foreach (var kvp in m_ChessGame.CurrentChessBoard.KeyFieldValuePiece)
            {
                ResetColor();
                // From inside the custom Button type:
                var b = this.FindName(kvp.Key) as Button;
                //b.Content = kvp.Value.ToString();
                string s;
                s = kvp.Value.PieceColor == gColor.White ? "W" : "B";

                var img = new Image
                {
                    Source = new BitmapImage(new Uri($"C://Users//eduard.krutitsky//Pictures//{s}{kvp.Value.ToString()}.png"))
                };
                if (b != null) b.Content = img;
            }
        }
        private void EmptyChessBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col) c;
                    string name = $"{col}{r + 1}";
                    Button b = this.FindName(name) as Button;
                    if (b != null) b.Content = "";
                    b.IsEnabled = true;
                }
            }
        }
        public void UpdateStatusBar()
        {
            var l = this.FindName("StatusBar") as Label;
            if (l != null) l.Content = $"Current State:\n{m_ChessGame.GameState}";
        }
        public void ResetColor()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Col col = (Col)c;

                    string name = $"{col}{r + 1}";
                    Button b = this.FindName(name) as Button;

                    if (b != null) b.Background = (r + c) % 2 == 0 ? _colorDarkFields : _colorBrightField;
                }
            }
        }

    }
}

