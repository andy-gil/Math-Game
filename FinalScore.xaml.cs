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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for FinalScore.xaml
    /// </summary>
    public partial class FinalScore : Window
    {
        /// <summary>
        /// Access to user class
        /// </summary>
        User UserInfo;

        public FinalScore()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /// <summary>
        /// Gets the info in the user class
        /// </summary>
        public User myUser
        {
            get { return UserInfo; }
            set { UserInfo = value; }
        }

        /// <summary>
        /// Print out the result screen 
        /// </summary>
        /// <param name="iCorrect"></param>
        /// <param name="iNumRounds"></param>
        internal void PrintResults(int iCorrect, int iNumRounds,int iMin, int seconds)
        {
            try
            {
                lblFinalName.Content = "Name: " + myUser.sName;
                lblFinalAge.Content = "Age: " + myUser.iAge;
                lblResults.Content = "Results Are: " + iCorrect + "/" + iNumRounds + "\nWith a time of: " + iMin + ":" + seconds;
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
        /// exit the results screen and back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
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
        /// helps see where any errors are
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
        /// for exiting the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
