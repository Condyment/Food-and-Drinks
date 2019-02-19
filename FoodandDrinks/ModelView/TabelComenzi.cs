using FoodandDrinks.Model;
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
  public class TabelComenzi
    {
        public ObservableCollection<ComandTabel> Command_Data { get; private set; }
        public ObservableCollection<NumberandFood> Product_Data { get; private set; }
        public Command Delete_From_Table { get; private set; }
        public Command Delete_All_Table { get; private set; }
        private int rowindex=-1;
        public int Rownumber
        {
            get
            {

                return rowindex;
            }

            set
            {
                if (rowindex != value)
                {

                    if (value <= -1)
                    {
                        rowindex = value;
                    }
                    else
                    {
                        rowindex = value;
                     

                       
                        if (Product_Data.Count != 0)
                        {
                            int k = Product_Data.Count;
                            for (int i = 0; i < k; i++)
                            {

                                Product_Data.RemoveAt(0);
                            }


                        }
                        if (TheDatabase.GetCommandDB().Count != 0)
                        {
                            int b = TheDatabase.GetCommandDB()[rowindex].IdComanda;

                            for (int i = 0; i < TheDatabase.GetProductDB(b).Count; i++)
                            {
                                Product_Data.Add(new NumberandFood() { Number = TheDatabase.GetProductDB(b)[i].Number, ProductName = TheDatabase.GetProductDB(b)[i].ProductName });
                            }
                        }

                    }
                }
            }
            }
                
        public TabelComenzi()
        {

            Command_Data = new ObservableCollection<ComandTabel>();
            Product_Data = new ObservableCollection<NumberandFood>();
            Delete_From_Table = new Command(Delete);
            Delete_All_Table = new Command(Delete_All);
            Filing_Table();

        }

        public void Delete()
        {

            if (rowindex == -1)
            {
                MessageBox.Show("Please Select Something");

            }
            else {
                int b = TheDatabase.GetCommandDB()[rowindex].IdComanda;
                TheDatabase.Delete_FromTable(b);
                Command_Data.RemoveAt(rowindex);
                int k = Product_Data.Count;
                for (int i = 0; i < k; i++)
                {
                    Product_Data.RemoveAt(0);
                }
            }
            
            
            
        }
        public void Delete_All()
        {
            
            if (MessageBox.Show("Delete all commands?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
              
            }
            else
            {
                TheDatabase.Delete_All_From_User();
                int k = Command_Data.Count;
                for (int i = 0; i < k; i++)
                {

                    Command_Data.RemoveAt(0);
                }
                int p = Product_Data.Count;
                for (int i = 0; i < p; i++)
                {

                    Product_Data.RemoveAt(0);
                }
            }
        }
        public void Filing_Table()
        {
             
            for (int i = 0; i < TheDatabase.GetCommandDB().Count; i++)
            { Command_Data.Add(new ComandTabel() { IdComanda = TheDatabase.GetCommandDB()[i].IdComanda, Price = TheDatabase.GetCommandDB()[i].Price, CommandSent = TheDatabase.GetCommandDB()[i].CommandSent }); }
        }

    }
}
