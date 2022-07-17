using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class creates the game questions
/// </summary>
namespace WpfApp1
{
    public class GameInfo
    {
        public int iAnswer;
        public int iFirstNum;
        public int iSecondNum;
        public int iCorrect = 0;
        public int iWrong = 0;
        public string sSymbol;
        Random randomNum = new Random();


        /// <summary>
        /// Get the first number to be random
        /// </summary>
        /// <returns></returns>
        public int FirstNumber()
        {
            iFirstNum = randomNum.Next(0, 11);
            return iFirstNum;
        }

        /// <summary>
        /// Get the second number to be random
        /// </summary>
        /// <returns></returns>
        public int SecondNumber()
        {
            iSecondNum = randomNum.Next(0, 11);
            return iSecondNum;
        }

        /// <summary>
        /// Resets the attributes back to 0 when a game is restarted
        /// </summary>
        public void Reset()
        {
            iFirstNum = 0;
            iSecondNum = 0;
            iCorrect = 0;
            iWrong = 0;
        }

        /// <summary>
        /// Keeps track of the number of correct and increase it each time
        /// </summary>
        /// <returns></returns>
        public int Correct()
        {
            iCorrect++;
            return iCorrect;
        }

        /// <summary>
        /// Keeps track of the wrongs
        /// </summary>
        /// <returns></returns>
        public int Wrong()
        {
            iWrong++;
            return iWrong;
        }

        /// <summary>
        /// Adds the first and second number
        /// also returns the "+" symbol
        /// </summary>
        /// <returns></returns>
        public int Addition()
        {
            sSymbol = "+";
            iAnswer = iFirstNum + iSecondNum;
            return iAnswer;
        }
        
        /// <summary>
        /// Subtraction method, difference of the two numbers
        /// Use an if to get rid of any negative numbers
        /// </summary>
        /// <returns></returns>
        public int Subtraction()
        {
            sSymbol = "-";
            if (iFirstNum > iSecondNum)
            {
                iAnswer = iFirstNum - iSecondNum;
                return iAnswer;
            }
            else
            {
                int temp = iFirstNum;
                iFirstNum = iSecondNum;
                iSecondNum = temp;
                iAnswer = iFirstNum - iSecondNum;
                return iAnswer;
            }
        }

        /// <summary>
        /// Multiplication method
        /// </summary>
        /// <returns></returns>
        public int Multiplication()
        {
            sSymbol = "*";
            iAnswer = iFirstNum * iSecondNum;
            return iAnswer;
        }

        /// <summary>
        /// Divide method
        /// Used a while loop when any number is 0 or the numbers divided left a remainder
        /// If it did, the number get randomized again until the conditions are no longer met
        /// </summary>
        /// <returns></returns>
        public int Divide()
        {
            sSymbol = "/";
            while (iFirstNum == 0 || iSecondNum == 0 || iFirstNum % iSecondNum != 0)
            {
                if(iFirstNum == 0)
                {
                    iFirstNum = randomNum.Next(1, 11);
                }
                iSecondNum = randomNum.Next(1, 11);
            }
            iAnswer = iFirstNum / iSecondNum;
            return iAnswer;
        }
    }
}
