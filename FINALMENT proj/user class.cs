using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALMENT_proj
{
    public class User
    {
        public string username;
        public string name;
        public string permissions;


        public User(string Useranme, string Name, string Permissions)
        {
            username = Useranme;
            name = Name;
            permissions = Permissions;
            
        }

    }

}
