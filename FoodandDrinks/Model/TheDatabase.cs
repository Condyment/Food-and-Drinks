using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace FoodandDrinks.Model
{
  public static class TheDatabase
    {
     private static  MySqlConnection MyConnection;
        private static int logi_id = 0;

        public  static Boolean ConnectioDB()
        {
            
            try
            {
                MyConnection = new MySqlConnection("server=localhost; user id=root;database=cantina_bd;password=20021995;pooling = false; convert zero datetime=True");

                MyConnection.Open();
                return true;
                


            }
            catch (MySqlException ex)
            {

                return false; 
            }
            

        }

      
        public static Boolean Login_DB(string username,string password)
        {
            Boolean retu = false;
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM login where UserName=@u and Password=@p", MyConnection);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                logi_id=reader.GetInt32("idLogin");

                retu= true;
            }
            reader.Close();
            return retu;
            
        }

      

        public static int Create_Account(string firstname, string lastname, string username, string email, string password)
        {
            int i = 0;
            int j = 0;
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM login where UserName=@u", MyConnection);
            cmd.Parameters.AddWithValue("@u", username);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                i++;

            }
            reader.Close();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM login where Email=@e", MyConnection);
            cmd1.Parameters.AddWithValue("@e", email);
            MySqlDataReader reader1 = cmd1.ExecuteReader();


            while (reader1.Read())
            {
                j++;

            }

            reader1.Close();
            if (j + i == 2)
            {
                return 1;
            }


            if (i > 0)
            {
                return 2;
            }
            else if (j > 0)
            {
                return 3;
            }

            if (j + i == 0)
            {
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO login(FirstName,LastName,UserName,Email,Password) VALUES(@fn,@ln,@un,@ema,@pas)", MyConnection);
                cmd2.Parameters.AddWithValue("@fn", firstname);
                cmd2.Parameters.AddWithValue("@ln", lastname);
                cmd2.Parameters.AddWithValue("@un", username);
                cmd2.Parameters.AddWithValue("@ema", email);
                cmd2.Parameters.AddWithValue("@pas", password);
                cmd2.ExecuteNonQuery();

                return 4;
            }
            return 0;
        }
      
        public static void AddCommadDB(string Data_Prezenta, string Data_Comanda_Finalizata, int[] ArrayFood, double Price)
        {
            int comnumber = 0;
            string g="";
            for (int i = 0; i < 25; i++)
            {
                g = g +","+ ArrayFood[i];

            }
           
            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO comanda(Price,ComandSent,ComandReady,Login_idLogin) VALUES(@price,@sent,@ready,@logid)", MyConnection);
            cmd1.Parameters.AddWithValue("@price", Price);
            cmd1.Parameters.AddWithValue("@sent", Data_Prezenta);
            cmd1.Parameters.AddWithValue("@ready", Data_Comanda_Finalizata);
            cmd1.Parameters.AddWithValue("@logid", logi_id);
            cmd1.ExecuteNonQuery();

            MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM comanda where ComandSent=@u and Login_idLogin=@p", MyConnection);
            cmd2.Parameters.AddWithValue("@u", Data_Prezenta);
            cmd2.Parameters.AddWithValue("@p", logi_id);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                comnumber = reader.GetInt32("idComanda");
            }
            reader.Close();


            MySqlCommand cmd3 = new MySqlCommand("INSERT INTO products(FoodArray,Comanda_idComanda) VALUES(@food,@com)", MyConnection);
            cmd3.Parameters.AddWithValue("@food", g);
            cmd3.Parameters.AddWithValue("@com", comnumber);
            cmd3.ExecuteNonQuery();
            
        }

        public static List<ComandTabel> GetCommandDB()
        {
            List<ComandTabel> Command_Tabele_List = new List<ComandTabel>();

            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM comanda where Login_idLogin=@e", MyConnection);
            cmd1.Parameters.AddWithValue("@e", logi_id);
            MySqlDataReader reader1 = cmd1.ExecuteReader();


            while (reader1.Read())
            {
                Command_Tabele_List.Add(new ComandTabel { IdComanda= reader1.GetInt32("idComanda"), Price=reader1.GetDouble("Price"), CommandSent=reader1.GetDateTime("ComandReady") });

            }

            reader1.Close();
            return Command_Tabele_List;



        } 
        public static List<NumberandFood> GetProductDB(int idCommand)
        {
            List<NumberandFood> Product_Table_List = new List<NumberandFood>();
            string Food_Array = "";
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM products where Comanda_idComanda=@e", MyConnection);
            cmd1.Parameters.AddWithValue("@e", idCommand);
            MySqlDataReader reader1 = cmd1.ExecuteReader();


            while (reader1.Read())
            {

                Food_Array = reader1.GetString("FoodArray");

                         }

            reader1.Close();

            Food_Array = Food_Array.Remove(0, 1);
            int[] array = Food_Array.Split(',').Select(int.Parse).ToArray();
            
            for (int i = 0; i < 25; i++)
            {
                if (array[i] != 0)
                {
                    
                    Product_Table_List.Add(new NumberandFood { Number = array[i], ProductName = Transform_FoodInt_to_sTRING(i) });
                }


            }


            return Product_Table_List;

        }

        public static void Delete_FromTable(int therowfromwheretodelete)
        {
            MySqlCommand cmd1 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0", MyConnection);
            cmd1.ExecuteNonQuery();

            MySqlCommand cmd2 = new MySqlCommand("DELETE FROM  comanda WHERE idComanda=@e", MyConnection);
            cmd2.Parameters.AddWithValue("@e", therowfromwheretodelete);
            cmd2.ExecuteNonQuery();

            MySqlCommand cmd3 = new MySqlCommand("DELETE FROM  products WHERE Comanda_idComanda=@e", MyConnection);
            cmd3.Parameters.AddWithValue("@e", therowfromwheretodelete);
            cmd3.ExecuteNonQuery();

            MySqlCommand cmd4 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1", MyConnection);
            cmd4.ExecuteNonQuery();

        }
        public static string Transform_FoodInt_to_sTRING(int foodarraynumber)
        {

            string cool="";
            switch (foodarraynumber)
            {
                case 0:
                    cool="CIORBA TARANEASCA DE VACUTA";
                    break;
                case 1:
                    cool = "SUPA CREMA DE LEGUME CU CRUTOANE";
                    break;
                case 2:
                    cool = "PILAF DE OREZ CU MORCOVI";
                    break;
                case 3:
                    cool = "PIURE DE CARTOFI CU UNT";
                    break;
                case 4:
                    cool = "CARTOFI CU MASLINE";
                    break;
                case 5:
                    cool = "CARTOFI CU UNT SI SMANTANA";
                    break;
                case 6:
                    cool = "CASCAVAL PANE";
                    break;
                case 7:
                    cool = "GRATAR COTLET DE PORC";
                    break;
                case 8:
                    cool = "SNITEL PANE DE PUI";
                    break;
                case 9:
                    cool = "GRATAR PIEPT DE PUI";
                    break;
                case 10:
                    cool = "MAMALIGA CU BRANZA SI SMANTANA";
                    break;
                case 11:
                    cool = "SPAGHETE CU ROSII SI MASLINE";
                    break;
                case 12:
                    cool = "CARTOFI FRANTUZESTI";
                    break;
                case 13:
                    cool = "FRIPTURA PUI CU SOS TOMAT";
                    break;
                case 14:
                    cool = "SALATA ORIENTALA MURATURI";
                    break;
                case 15:
                    cool = " MICI LA GRATAR";
                    break;
                case 16:
                    cool = "COD ALASKA PANE";
                    break;
                case 17:
                    cool = "CARTOFI GRATINATI CU SMANTANA";
                    break;
                case 18:
                    cool = "CARTOFI HASH";
                    break;
                case 19:
                    cool = "CARTOFI RONDELE";
                    break;
                case 20:
                    cool = "CARTOFI PAI";
                    break;
                case 21:
                    cool = "OREZ CU LAPTE";
                    break;
                case 22:
                    cool = "SALATA DE VARZA";
                    break;
                case 23:
                    cool = "SALATA DE MURATURI ASORTATE";
                    break;
                case 24:
                    cool = "SALATA DE SFECLA ROSIE";
                    break;
                
            }

            return cool;
            
   
        }
        public static void Delete_All_From_User()
        {
            List<int> idCommandlist = new List<int>();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM comanda where Login_idLogin=@e", MyConnection);
            cmd1.Parameters.AddWithValue("@e", logi_id);
            MySqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {

                idCommandlist.Add(reader1.GetInt32("idComanda"));

            }
            reader1.Close();

            MySqlCommand cmd2 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0", MyConnection);
            cmd1.ExecuteNonQuery();
            int k = idCommandlist.Count;
            for (int i = 0; i <k; i++)
            {
                MySqlCommand cmd4 = new MySqlCommand("DELETE FROM  products WHERE Comanda_idComanda=@e", MyConnection);
                cmd4.Parameters.AddWithValue("@e", idCommandlist[i]);
                cmd4.ExecuteNonQuery();
                MySqlCommand cmd3 = new MySqlCommand("DELETE FROM  comanda WHERE idComanda=@e", MyConnection);
                cmd3.Parameters.AddWithValue("@e", idCommandlist[i]);
                cmd3.ExecuteNonQuery();
            }
            
            MySqlCommand cmd5 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1", MyConnection);
            cmd5.ExecuteNonQuery();



        }


        public static void Close_Connection()
        {
            MyConnection.Close();



        }

    }
}
