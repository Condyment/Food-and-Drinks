using FoodandDrinks.Model.NotfyStuff;
using FoodandDrinks.Model;
using FoodandDrinks.ModelView.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodandDrinks.ModelView
{
    public class OnlyForCreate:Notfy
    {
        private string firstname="";
        private string lastname="";
        private string username ="";
        private string email ="";
        private string password1 ="";
        private string password2 ="";
   
        public string alert_firstname {  get; private set; }
        public string alert_lastname {  get; private set; }
        public string alert_username {  get; private set; }
        public string alert_email {  get; private set; }
        public string alert_password1 {  get; private set; }
        public string alert_password2 {  get; private set; }

        public String FirstName {
            get
            {
                return firstname;
            }
            set
            {
                if (firstname != value)
                {

                    firstname = value;

                }
            }
        }
        public String LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                if (lastname != value)
                {

                    lastname = value;
                }
            }
        }
        public String UserName
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
        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {

                    email = value;
                }
            }
        }
        public String Password1
        {
            get
            {
                return password1;
            }
            set
            {
                if (password1 != value)
                {

                    password1 = value;
                }
            }
        }
        public String Password2
        {
            get
            {
                return password2;
            }
            set
            {
                if (password2 != value)
                {

                    password2 = value;
                }
            }
        }

        public Command Create { get; private set; }
       
        public OnlyForCreate()
        {
            Create = new Command(CreareCont);  
        }
       
        public void CreareCont()
        {
            int i = 0;
            if (firstname.Length == 0) {
                alert_firstname = "Error,Please write something";
                onepropretychanged("alert_firstname");

            }
            else
            {
                i++;
                alert_firstname = "";
                onepropretychanged("alert_firstname");
            }

            if (lastname.Length == 0)
            {
                alert_lastname = "Error,Please write something";
                onepropretychanged("alert_lastname");

            }
            else
            {
                i++;
                alert_lastname = "";
                onepropretychanged("alert_lastname");

            }
            if (username.Length == 0)
            {
                alert_username = "Error,Please write something";
                onepropretychanged("alert_username");

            }
            else
            {
                i++;
                alert_username = "";
                onepropretychanged("alert_username");
            }
            if (email.Length == 0)
            {
                alert_email = "Error,Please write something";
                onepropretychanged("alert_email");

            }
            else
            {
                i++;
                alert_email = "";
                onepropretychanged("alert_email");
            }
            if (password1.Length == 0)
            {
                alert_password1 = "Error,Please write something";
                onepropretychanged("alert_password1");
            }
            else
            {
                i++;
                alert_password1 = "";
                onepropretychanged("alert_password1");
            }

            if (password2.Length == 0)
            {

                alert_password2 = "Error,Please write something";
                onepropretychanged("alert_password2");

            }
            else
            {
                i++;
                alert_password2 = "";
                onepropretychanged("alert_password2");
            }
            if((password1.Length!=0)&&(password2.Length != 0))
            {
                if (password1.Equals(password2)) {
                    alert_password1 = "";
                    onepropretychanged("alert_password1");
                    alert_password2 = "";
                    onepropretychanged("alert_password2");
                    i++;
                }
                else
                {
                    alert_password1 = "No match between Passwords";
                    onepropretychanged("alert_password1");
                    alert_password2 = "No match between Passwords";
                    onepropretychanged("alert_password2");
                }


            }
       
        
             
            if (i == 7)
            {
               int k= TheDatabase.Create_Account(firstname, lastname, username, email, password1);
               if(k==1)
                {
                        alert_email = "Email already exists";
                    onepropretychanged("alert_email");

                        alert_username = "UserName already exists";
                    onepropretychanged("alert_username");
                }
               else if( k == 2)
                {
                        alert_username = "UserName already exists";
                    onepropretychanged("alert_username");
                        alert_email = "";
                    onepropretychanged("alert_email");
                }
                else if (k == 3)
                {
                        alert_email = "Email already exists";
                    onepropretychanged("alert_email");
                        alert_username = "";
                    onepropretychanged("alert_username");
                }
                else if (k == 4)
                {
                    MessageBox.Show("You have account");
                }
                else if (k == 0) { MessageBox.Show("idk"); }
            }
             
        }    
             
    }
}
