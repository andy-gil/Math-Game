using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class User
    {
        public string sName { get; set; }
        public int iAge { get; set; }
        public User(string name, int age)
        {
            sName = name;
            iAge = age;
        }
    }
}
