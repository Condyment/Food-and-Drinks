using FoodandDrinks.Model.NotfyStuff;
using FoodandDrinks.ModelView.Commands;
using FoodandDrinks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodandDrinks.ModelView
{
   public class OnlyForLogin: Notfy
    {
        private string username="";
        private string password="";

        
        public string alert_password { get;private set; }
        public string alert_username { get; private set; }
  
        
       
        public string User_Name
        {
       get
            {

                return username;
            }

           set
            {
                if (username != value)
                {

                    username = value;
                 
                  }


            }
        }
        public string Password_
        {
            get
            {

                return password;
            }

            set
            {
                if (password != value)
                {

                    password = value;
                    
                }


            }
        }

        

        public OnlyForLogin()
         {
          
        }
        public Boolean Log()
        {
            if (UserNamePasswordisfull())
            {
              
                if (TheDatabase.Login_DB(username, password) == false)
                {
                    alert_password = "Maybe your Password or UserName is wrong";
                    onepropretychanged("alert_password");
                    alert_username = "Maybe your Password or UserName is wrong";
                    onepropretychanged("alert_username");
                    return  false;
                }
                else {

                    return  true;

                }

            }
            else
            {
                return  false;
            }


        }

       
        public Boolean UserNamePasswordisfull()
        {
            int verificare = 0;
            if (password.Length == 0)
            {
                alert_password = "You forgot to write the Password";
                onepropretychanged("alert_password");
            }
            else
            {
                alert_password = "";
                onepropretychanged("alert_password");
                verificare++;
            }
            if (username.Length == 0)
            {
                alert_username = "You forgot to write the UserName";
                onepropretychanged("alert_username");
            }
            else
            {
                alert_username = "";
                onepropretychanged("alert_username");
                verificare++;
            }
            if (verificare==2)
            {
                return true;
            }
            else
            {
                return false;

            }

        }

      

        }
    }

