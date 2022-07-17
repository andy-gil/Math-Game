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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes
        /// <summary>
        /// To connect to the game.xaml
        /// </summary>
        Game wndGameWPF;
        /// <summary>
        /// To connect to the user class
        /// </summary>
        User UserCls;
        /// <summary>
        /// To connect to game class
        /// </summary>
        GameInfo GameInfoCls;
        /// <summary>
        /// To determine which game type the user picks
        /// </summary>
        public int gameType;
        /// <summary>
        /// Help store the age entered
        /// </summary>
        public int Age;
        /// <summary>
        /// Help store the name entered
        /// </summary>
        public string Name;
        /// <summary>
        /// Music used as an intro and when the begin button gets pressed
        /// </summary>
        SoundPlayer Begin = new SoundPlayer("Sounds/Begin.wav");
        SoundPlayer Main = new SoundPlayer("Sounds/Main.wav");
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            wndGameWPF = new Game();
            GameInfoCls = new GameInfo();
            StartSound();
        }

        #region Methods
        /// <summary>
        /// Get a song to play in the main menu
        /// </summary>
        public void StartSound()
        {
            Main.Play();
        }

        /// <summary>
        /// When the begin button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //If the name is left blank leave an error
                if (NameTxt.Text == "")
                {
                    lblNameError.Content = "Error: Please enter a Name.";
                    NameTxt.Text = "";
                    return;
                }
                //if the age was left blank leave an error
                else if (AgeTxt.Text == "")
                {
                    lblAgeError.Content = "Error: Age must be 3-10";
                    AgeTxt.Text = "";
                    return;
                }
                else
                {
                    //Plays the voice to get started
                    Begin.PlaySync(); 

                    //Check if the age is between 3-10
                    int age;
                    Int32.TryParse(AgeTxt.Text, out age);
                    if (age < 3 || age > 10)
                    {
                        lblAgeError.Content = "Error: Age must be 3-10";
                        return;
                    }
                    //The user info gets put into the variables
                    Age = Int32.Parse(AgeTxt.Text);
                    Name = NameTxt.Text;

                    //Have the name and age send to the user class
                    UserCls = new User(Name, Age);

                    //Checks which radio button was pressed
                    //assigns gameType to a number
                    if (AddRdo.IsChecked == true)
                    {
                        gameType = 1;
                    }
                    else if (SubRdo.IsChecked == true)
                    {
                        gameType = 2;
                    }
                    else if (DivideRdo.IsChecked == true)
                    {
                        gameType = 3;
                    }
                    else // MultRdo.IsChecked
                    {
                        gameType = 4;
                    }
                    reset(); //Makes sure all the labels are reset
                    this.Hide(); //Hide main window
                    wndGameWPF.ResetGame(); //reset the gaming window
                    wndGameWPF.myUser = UserCls;//////////////////////////////////////////////Pass the user class to the main window, which passes it to the high score
                    wndGameWPF.ShowDialog(); // goes to the game.xaml
                    this.Show(); // brings back main window
                    StartSound();//restarts main menu song
                }
            }
            catch(Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Reset the gaming labels and answers
        /// </summary>
        private void reset()
        {
            //wndGameWPF.StartBtn.Visibility = Visibility.Visible;
            GameInfoCls.Reset();
            wndGameWPF.iNumRounds = 0;
            wndGameWPF.lblFirstInt.Content = "";
            wndGameWPF.lblSecondInt.Content = "";
            wndGameWPF.lblSymbols.Content = "";
            wndGameWPF.lblEqual.Content = "=";
            wndGameWPF.UserTxt.Text = "";
        }

        /// <summary>
        /// Helps to find errors quick
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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

        /// <summary>
        /// For when the user uses the X button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
            e.Cancel = true;
        }
        #endregion
    }
}
