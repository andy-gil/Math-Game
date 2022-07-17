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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Media;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        #region Attributes
        /// <summary>
        /// Connects to The final window
        /// </summary>
        FinalScore wndFinalWPF;
        /// <summary>
        /// Connects to the game class
        /// </summary>
        GameInfo GameInfoCls;
        /// <summary>
        /// Used to store the 1st number gotten from the game class
        /// </summary>
        private int iNum1;
        /// <summary>
        /// Used to store the 2nd number from the game class
        /// </summary>
        private int iNum2;
        /// <summary>
        /// Keeps track of the number of rounds
        /// </summary>
        public int iNumRounds = 0;
        /// <summary>
        /// Keeps track of the number of correct answers
        /// </summary>
        public int iCorrect = 0;
        /// <summary>
        /// Keeps track of the number of wrong answers
        /// </summary>
        public int iWrong = 0;
        /// <summary>
        /// Makes sure to play the song once
        /// </summary>
        int j = 0;
        /// <summary>
        /// seconds for timer
        /// </summary>
        int seconds = 00;
        /// <summary>
        /// mins for timer
        /// </summary>
        int iMin = 0;
        /// <summary>
        /// For a timer
        /// </summary>
        DispatcherTimer MyTimer;
        /// <summary>
        /// Song used while playing the game
        /// </summary>
        SoundPlayer simpleSound = new SoundPlayer("Sounds/MainTheme.wav");
        SoundPlayer Results = new SoundPlayer("Sounds/Results.wav");
        SoundPlayer Expected = new SoundPlayer("Sounds/Expected.wav");
        SoundPlayer Practice = new SoundPlayer("Sounds/Practice.wav");
        SoundPlayer ending1 = new SoundPlayer("Sounds/ending1.wav");
        SoundPlayer ending2 = new SoundPlayer("Sounds/ending1.wav");
        SoundPlayer ending3 = new SoundPlayer("Sounds/ending2.wav");

        /// <summary>
        /// Access to user class
        /// </summary>
        User UserInfo;

        public User myUser
        {
            get { return UserInfo; }
            set { UserInfo = value; }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            InitializeComponent();
            MyTimer = new DispatcherTimer();
            MyTimer.Interval = TimeSpan.FromSeconds(1);
            MyTimer.Tick += Ticker;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            wndFinalWPF = new FinalScore();
            GameInfoCls = new GameInfo(); 
        }

        #region Method
        /// <summary>
        /// Method to start up the song
        /// and resets the timer
        /// </summary>
        public void StartSound()
        {
            simpleSound.Play();
            int increment = 00;
            int iMin = 0;
        }

        /// <summary>
        /// Method to start up the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ticker(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                iMin++;
                seconds = 00;
            }
            lblTimer.Content = iMin.ToString() + ":" + seconds.ToString();
        }

        /// <summary>
        /// Method when the Start Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyTimer.Start();
                StartBtn.Visibility = Visibility.Hidden; //Hide the start button when it gets clicked 
                if (j == 0)//Helps start the song and only get played once
                {
                    Ticker(sender, e); // Start the timer
                    j++; // increase j so it doesnt replay song
                }
                //The random number of 1 and 2 gets stored to our private variables
                iNum1 = GameInfoCls.FirstNumber();
                iNum2 = GameInfoCls.SecondNumber();

                //Gametype number assigned in the main window is used to determine which game will be played(addition, subtraction, divide, or mult)
                if (((MainWindow)System.Windows.Application.Current.MainWindow).gameType == 1)
                {
                    GameInfoCls.Addition();
                }
                else if (((MainWindow)System.Windows.Application.Current.MainWindow).gameType == 2)
                {
                    GameInfoCls.Subtraction();
                }
                else if (((MainWindow)System.Windows.Application.Current.MainWindow).gameType == 3)
                {
                    GameInfoCls.Divide();
                }
                else
                {
                    GameInfoCls.Multiplication();
                }

                //All of the labels get filled with the numbers and symbols
                lblFirstInt.Content = GameInfoCls.iFirstNum;
                lblSecondInt.Content = GameInfoCls.iSecondNum;
                lblSymbols.Content = GameInfoCls.sSymbol;
                lblEqual.Content = "=";
                UserTxt.Text = "";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Where the user hits the submit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iNumRounds++; //number of rounds increase with each button submitted
                int userGuess; //use the userGuess to store the answer the user gives
                userGuess = Int32.Parse(UserTxt.Text);

                //Send the guess to the matching method
                Matching(userGuess);
                
                //If number of rounds = 10 close the window and show the final score window
                //otherwise send to get another question
                if (iNumRounds == 10)
                {
                    simpleSound.Stop();
                    MyTimer.Stop();
                    iCorrect = GameInfoCls.iCorrect;
                    iWrong = GameInfoCls.iWrong;
                    if (iCorrect < 5)
                    {
                        ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"Image/losing.jpg", UriKind.Relative))); // changes background depeneding on results
                        wndFinalWPF.Background = myBrush;
                        Practice.Play(); // Plays song depending on results
                        //ending1.Play();
                    }
                    else if (GameInfoCls.iCorrect < 8)
                    {
                        ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"Image/normal.jpg", UriKind.Relative))); // changes background depeneding on results
                        wndFinalWPF.Background = myBrush;
                        Expected.Play(); // Plays song depending on results
                        //ending2.Play();
                    }
                    else
                    {
                        ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"Image/master.png", UriKind.Relative))); // changes background depeneding on results
                        wndFinalWPF.Background = myBrush;
                        Results.Play(); // Plays song depending on results
                        //ending3.Play();
                    }
                    this.Hide();
                    wndFinalWPF.myUser = UserInfo;//////////////////////////////Pass user's info to final score window
                    wndFinalWPF.PrintResults(iCorrect, iNumRounds, iMin, seconds);
                    wndFinalWPF.ShowDialog();
                }
                else
                {
                    StartBtn_click(sender, e);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Checks the user guess to see if it was correct or wrong
        /// </summary>
        /// <param name="userGuess"></param>
        private void Matching(int userGuess)
        {
            try
            {
                if (userGuess == GameInfoCls.iAnswer)
                {
                    GameInfoCls.Correct(); // goes to the correct method in user class
                    lblCheck.Content = "Correct!"; //print out correct
                }
                else
                {
                    GameInfoCls.Wrong(); // goes to the wrong method in user class
                    lblCheck.Content = "Wrong!"; // prints out wrong
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Reset the timer, making sure the game variable are also reset
        /// </summary>
        public void ResetGame()
        {
            try
            {
                StartBtn.Visibility = Visibility.Visible;
                lblTimer.Content = "";
                lblCheck.Content = "";
                MyTimer.Stop();
                seconds = 0;
                iMin = 0;
                GameInfoCls.Reset();
                StartSound();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Cancel method
        /// goes back to main window and stops the song
        /// Also stops timer and resets it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.Visibility = Visibility.Visible; //gets the start button back
            this.Hide();
            simpleSound.Stop();
            MyTimer.Stop();
            lblTimer.Content = "";
            seconds = 0;
            iMin = 0;
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.Txt", Environment.NewLine +
                    "HandleError Exception: " + ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            simpleSound.Stop();
            this.Hide();
            e.Cancel = true;
        }
        #endregion
    }
}
