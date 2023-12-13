using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace justchecking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        DispatcherTimer timer= new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            SetupGame();
            
                

            


        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            
            tenthOfSecondsElapsed++;
            timerTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0 s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timerTextBlock.Text = timerTextBlock.Text + " -Play Again? ";
                
            }

        }

        private void SetupGame()
        {
            List<string> AnimalEmoji = new List<string>()
            {
                "🐸", "🐸",
                "🦇", "🦇",
                "🐿️", "🐿️",
                "🐷", "🐷",
                "🐇", "🐇",
                "🐤", "🐤",
                "🐢", "🐢",
                "🦨", "🦨",

            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timerTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(AnimalEmoji.Count);
                    string nextemoji = AnimalEmoji[index];
                    textBlock.Text = nextemoji;
                    AnimalEmoji.RemoveAt(index);
                }
                
            }
            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock LastTextBlockClicked;
        bool FindingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            
                TextBlock textBlock = sender as TextBlock;
                if (FindingMatch == false)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    LastTextBlockClicked = textBlock;
                    FindingMatch = true;
                }
                else if (textBlock.Text == LastTextBlockClicked.Text)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    FindingMatch = false;
                matchesFound++;

                }
                else
                {
                    LastTextBlockClicked.Visibility = Visibility.Visible;
                    FindingMatch = false;
                }
            
        }

        private void timerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetupGame();
            }
        }
    }
}
