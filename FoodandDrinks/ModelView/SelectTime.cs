using FoodandDrinks.Model;
using FoodandDrinks.Model.NotfyStuff;
using FoodandDrinks.ModelView.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace FoodandDrinks.ModelView
{
   public class SelectTime:Notfy
    {
        public ObservableCollection<ButtonWithColors> Hour { get; private set; }
        public ObservableCollection<ButtonWithColors> Minute { get; private set; }

        public Command Open_Time { get; private set; }
        public Command Send_Command { get; private set; }

        public Com Select_Hour { get; private set; }
        public Com Select_Minute { get; private set; }

        private string Enabled = "false";
        public String Is_Enabled
        {
            get
            {
                return Enabled;
            }
            set
            {
                if (value != Enabled)
                {
                    Enabled = value;
                    onepropretychanged("Is_Enabled");
                }



            }
        }


        public string Time;
        public string DataSelectedd
        {
            get
            {

                return Time;
            }

            set
            {
                if (Time != value)
                {
                    Time = value;
                    

                    String[] data_intreaga = Time.Split(' ');
                    String[] data_pareteadesus= data_intreaga[0].Split('/');

                    day = Int32.Parse(data_pareteadesus[0]);
                    month = Int32.Parse(data_pareteadesus[1]);
                    year = Int32.Parse(data_pareteadesus[2]);

                    String Data_Prezenta = DateTime.Now.ToString("MM:d:y");
                    String[] Data_Prezenta_bucati = Data_Prezenta.Split(':');
                   
                    int d= Int32.Parse(Data_Prezenta_bucati[0]);
                    int m= Int32.Parse(Data_Prezenta_bucati[1]);
                    int y= Int32.Parse(Data_Prezenta_bucati[2]);
                    y = y + 2000;
                   
                  


                    if (d== day && month == m && year== y)
                    {
                       
                        First_Text = "   ";
                        Second_Text = "   ";
                        onepropretychanged("Hour_Minute_Text");

                        String Data_Prezenta_ore = DateTime.Now.ToString("HH:mm:ss");
                        String[] Data_Prezenta_ore_bucati = Data_Prezenta_ore.Split(':');
                        int h = Int32.Parse(Data_Prezenta_ore_bucati[0]);
                        Hour.Clear();
                        Minute.Clear();
                       
                        hour = h+1;
                        if (h <= 12) { hour = 12; }
                        if (h<=16)
                        {
                            Is_Enabled = "true";
                            Workingwithcollections();
                        }
                        else
                        {
                            MessageBox.Show("Sorry the Place is closed");
                            Is_Enabled = "false";
                        } 
                    }
                    else
                    {
                        Is_Enabled = "true";
                        Minute.Clear();
                        Hour.Clear();
                        hour = 12;
                       
                        Workingwithcollections();
                    }

                   
                }


            }
        }
       

        public string Time_Tabel= "Collapsed";
        public string Time_Tabel_Visibility
        {
            get
            {

                return Time_Tabel;
            }

            set
            {
                if (Time_Tabel != value)
                {

                    Time_Tabel = value;
                    onepropretychanged("Time_Tabel_Visibility");
                  
                }
              

            }
        }

        private int number = 1;
        private int year = 0;
        private int month = 0;
        private int day = 0;
        private int hour = 0;
        private int[] ArrayFood;

        private int Switch_Hour = 0;
        private int Switch_Minute = 0;

        private string Total_Text="";
        private string First_Text = "   ";
        private string Second_Text ="   ";

        public double Price = 0;
        public String Hour_Minute_Text
        {
            get
            {
                return Total_Text=First_Text+":"+Second_Text;
                
            }
            set
            {if(Total_Text.Equals(First_Text + ":" + Second_Text)==false)
                {
                    Total_Text = First_Text + ":" + Second_Text;
                    onepropretychanged("Hour_Minute_Text");
                }
     
            }
        }

       
        public SelectTime()
        {
            Hour= new ObservableCollection<ButtonWithColors>();
            Minute = new ObservableCollection<ButtonWithColors>();

            Open_Time = new Command(Open_Wrappanel);
            Select_Hour = new Com(Select_Hour_Action);
            Select_Minute = new Com(Select_Minute_Action);
            Send_Command= new Command(Send_command_Action);


        }
       
     
        public void Select_Hour_Action( string paramater)
        {
            int a =  Int32.Parse(paramater);

            int b= Hour.Count;
            for (int i=0;i<b;i++)
            {
                
                if (Hour[i].Number==a)
            {

                Hour[i].Colorr = "#034488";
                    First_Text=""+Hour[i].Number;
                    onepropretychanged("Hour_Minute_Text");
                    Switch_Hour = 1;
                    AND_Switch();
                }
                else {
                    Hour[i].Colorr = "White";
                }
            

            }
        

        }

     

        public void Select_Minute_Action(string paramater)
        {
            int a = Int32.Parse(paramater);
           
            int b = Minute.Count;
            for (int i = 0; i < b; i++)
            {
                if (Minute[i].Number == a)
                {
                    Minute[i].Colorr = "#034488";
                    Second_Text ="" + Minute[i].Number;
                    onepropretychanged("Hour_Minute_Text");
                    Switch_Minute = 1;
                    AND_Switch();
                }
                else
                {
                    Minute[i].Colorr = "White";
                }


            }
          
        }
       
        public void Open_Wrappanel()
        {
            if (number % 2 == 1)
            {

                Switch_Hour = 0;
                Switch_Minute = 0;
                Time_Tabel_Visibility = "Visible";
               
            }
            else {

                Time_Tabel_Visibility = "Collapsed"; }
            number = number + 1;

        }
        public void Workingwithcollections()
        {
             
            for (int i = hour; i <= 17; i++)
            {
                Hour.Add(new ButtonWithColors() {  Number = i, Colorr = "White" });
               
            }
           
            for (int i = 0; i <=50; i = i + 10)
            {
                Minute.Add(new ButtonWithColors() { Number = i, Colorr = "White" });
                
            }
        }
        public void Send_command_Action()
        {
            if (MessageBox.Show("Send the Command?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                int b = 0;
                if (First_Text.Equals("   "))
                {
                    MessageBox.Show("Please select the Hour ");

                }
                else { b++; }
                if (Second_Text.Equals("   "))
                {
                    MessageBox.Show("Please select the Minute ");
                }
                else { b++; }
                if (b == 2)
                {


                    string[] something = Total_Text.Split(':');
                    int anotheryear = year;

                    String stringmonth;
                    if (month >= 10)
                    {
                        stringmonth = "" + month;
                    }
                    else
                    {
                        stringmonth = "0" + month;
                    }
                    String stringday;
                    if (day >= 10)
                    {
                        stringday = "" + day;
                    }
                    else
                    {
                        stringday = "0" + day;
                    }

                    string Hour_Minute_Seconds = something[0] + ":" + something[1] + ":00";
                    string Data_Comanda_Finalizata = anotheryear + "/" + stringday + "/" + stringmonth + " " + Hour_Minute_Seconds;
                    String Data_Prezenta = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    TheDatabase.AddCommadDB(Data_Prezenta, Data_Comanda_Finalizata, ArrayFood, Price);

                }
            }

        }
        public void AND_Switch()
        {
            if (Switch_Hour + Switch_Minute == 2)
            {
                Time_Tabel_Visibility = "Collapsed";
                number = number + 1;
            }
        }

        public void  SetarrayFood(int[]ArrayFood, double Price)
        {

          this.ArrayFood = ArrayFood;
           this.Price = Price;


        }
       
    }
   
}
