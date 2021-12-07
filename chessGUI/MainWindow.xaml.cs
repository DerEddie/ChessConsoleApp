using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess;



namespace ChessTest1
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Row m_r;
        public MainWindow()
        {
            InitializeComponent();
            ChessBoardGrid.Rows = 9;
            ChessBoardGrid.Columns = 8;

            for (int r = 1; r < 9; r++)
            {
                for (int c = 1; c < 9; c++)
                {
                    if ((r + c) % 2 == 0)
                    {
                        Button r1 = new Button();
                        
                        r1.Content = "B";
                        r1.Background = Brushes.Black;
                        r1.Foreground = Brushes.White;
                        r1.Name = $"_{r}_{c}";
                        ChessBoardGrid.Children.Add(r1);
                        Row row = (Row)r;
                    }
                    else
                    {
                        Button r1 = new Button();
                        r1.Background = Brushes.White;
                        r1.Content = "W";
                        ChessBoardGrid.Children.Add(r1);
                    }

                }

            }
            Button startButton = new Button();
            startButton.Background = Brushes.LawnGreen;
            startButton.Content = "Start";
            ChessBoardGrid.Children.Add(startButton);
            startButton.Click += StartButton_Click;

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            //var cGame = 


            foreach (Button cell in ChessBoardGrid.Children)
            {
                

                cell.Background = Brushes.Aqua;
            }
        }
    }
}
/*
             if (i % 2 == 0)
                {
                    
                    Rectangle r2 = new Rectangle();

                    r1.Fill = Brushes.BlueViolet;
                    ChessBoardGrid.Children.Add(r2);
                }
                else
                {

                }
*/