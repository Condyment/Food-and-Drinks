using FoodandDrinks.ModelView;
using FoodandDrinks.ModelView.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FoodandDrinks.Model.NotfyStuff;
using System.Collections.ObjectModel;

namespace FoodandDrinks.Model
{
  public  class BaseClass : Notfy
    {
        
        public OnlyForLogin Login { get; private set; }
        public OnlyForCreate Create { get; private set; }
        public SelectingFood Select_Food { get; private set; }
        public TabelComenzi Tabel_Comand { get; private set; }
        public SelectTime Select_Time { get; private set; }
       
        

        public Command Login_Command { get; private set; }
        public Command Create_Comand_Panel { get; private set; }
        public Command Back_Login { get; private set; }
        public Command Tabel_Comand_Panel { get; private set; }
        public Command SelectTime__Command_Panel { get; private set; }
        public Command Back_SelectFood_SelectTime { get; private set; }
        public Command Back_SelectFood_Tabel { get; private set; }
        public Command Logout_Reset { get; private set; }

        public ObservableCollection<Visibility> Visibility_Proprety_OC { get; private set; }

        private string Collapse = "Collapsed";
        private string Visiby = "Visible";

        private string enab;
        public string Enb
        {
            get { return enab; }
            set
            {
                enab = value;
                onepropretychanged("Enb");
            }
        }


        private Boolean ConectionWorked = false;
        public BaseClass()
        {

            ConectionWorked = TheDatabase.ConnectioDB();
            Login = new OnlyForLogin();
            Login_Command = new Command(Login_Action);
            Create_Comand_Panel = new Command(Create_Panel);
            Back_Login = new Command(Back_to_Login);
            Tabel_Comand_Panel = new Command(Tabel_Panel);
            SelectTime__Command_Panel = new Command(SelecTime_Panel);
            Back_SelectFood_SelectTime = new Command(Back_to_SelectFood_SelectTime);
            Back_SelectFood_Tabel = new Command(Back_to_SelectFood_Tabel);
            Logout_Reset=new Command(Logout);
            Visibility_Proprety_OC = new ObservableCollection<Visibility>();
            Select_Food = new SelectingFood();
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    Visibility_Proprety_OC.Add(new Visibility() { Vib = Visiby });
                }
                Visibility_Proprety_OC.Add(new Visibility() { Vib = Collapse });
            }
           
        }

        public void Login_Action()
        {
           
            if (ConectionWorked == true) {
            if (Login.Log()) {

                Select_Food = new SelectingFood();
                onepropretychanged("Select_Food");

                Visibility_Proprety_OC[0].Vib = Collapse;
                Visibility_Proprety_OC[2].Vib = Visiby;
               
            }
            }
            else
            {
                MessageBox.Show("You have no Connection with the Data Base");
            }
        }
        public void Create_Panel()
        {
            if (ConectionWorked == true)
            {
                Create = new OnlyForCreate();
                onepropretychanged("Create");

                Visibility_Proprety_OC[0].Vib = Collapse;
                Visibility_Proprety_OC[1].Vib = Visiby;
            }
            else
            {
                MessageBox.Show("You have no Connection with the Data Base");
            }


        }
        public void Back_to_Login()
        {
            Login = new OnlyForLogin();
            onepropretychanged("Login");

            Visibility_Proprety_OC[0].Vib = Visiby;
            Visibility_Proprety_OC[1].Vib = Collapse;

           
        }


        public void Tabel_Panel()
        {
            Tabel_Comand = new TabelComenzi();
            onepropretychanged("Tabel_Comand");

            Visibility_Proprety_OC[3].Vib = Visiby; 
            Enb = "false";
        }

        public void SelecTime_Panel()
        {
            int is_empty = 0;
            double price = Select_Food.GetPrice();
            int[]Foods = Select_Food.GetArrayoffood();
            for(int i = 0; i < 25; i++)
            {
                if (Foods[i] == 0)
                {
                    is_empty++;
                   
                }
            }
            
            if (is_empty == 25)
            {
                MessageBox.Show("Yoy have nothing on the table");
            }
            else
            {
                Select_Time = new SelectTime();
                onepropretychanged("Select_Time");

                Select_Time.SetarrayFood(Foods, price);

                Visibility_Proprety_OC[4].Vib = Visiby;
                
                Enb = "false";
            }




        }
        public void Back_to_SelectFood_SelectTime()
        {
           

            Visibility_Proprety_OC[4].Vib =Collapse;
          
            Enb = "true";

        }
        public void Back_to_SelectFood_Tabel()
        {

            Visibility_Proprety_OC[3].Vib = Collapse;

            Enb = "true";

        }
        public void Logout()
        {
            Login = new OnlyForLogin();
            onepropretychanged("Login");
            Visibility_Proprety_OC[0].Vib = Visiby;
            Visibility_Proprety_OC[2].Vib = Collapse;

        }



    }

    public class Visibility:Notfy {

        private string Visibiliti;
        public string Vib
        {
            get { return Visibiliti; }
            set
            {
                Visibiliti = value;
                onepropretychanged("Vib");
            }
        }




    }
}
