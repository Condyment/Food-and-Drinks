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
    public class SelectingFood : Notfy
    {
        public Com ButtonSelected { get; private set; }
        public Com Addaction { get; private set; }

        public Com Removeaction { get; private set; }
        public Com Removeone { get; private set; }
        public Com RemoveAll { get; private set; }
        public Com ButtonSelectedSelectedRemoved { get; private set; }

        public ObservableCollection<ShowButton> MancareaAleasa { get; private set; }
        public ObservableCollection<ShowButton> ToateTipurileDeMancare { get; private set; }

        private int[] Array= new int[25];
        public int ArrayIndex=-1;

        private string FoodinAir;
        private string FoodinGarbage;

        public double[] ArrayofPrice = new double[25];
        public double Price { get; private set; }

        public SelectingFood()
        { 
            Removeone = new Com(Deleteone);
            Removeaction = new Com(Remove);
            RemoveAll = new Com(RemoveallFood);
            ButtonSelectedSelectedRemoved = new Com(Selectingtoremove);

            ButtonSelected = new Com(GetingFoodName);
            Addaction = new Com(AddingFood);

            MancareaAleasa = new ObservableCollection<ShowButton>();
            ToateTipurileDeMancare = new ObservableCollection<ShowButton>();

            MakeCollection();
        }


        public void Deleteone(string paramater)
        {
            for (int i = 0; i < MancareaAleasa.Count; i++)
            {
                if (MancareaAleasa[i].Food.Equals(FoodinGarbage))
                {
                    MancareaAleasa[i].Number = MancareaAleasa[i].Number - 1;
                    Array[BestStringFunctionEver(FoodinGarbage)] = Array[BestStringFunctionEver(FoodinGarbage)] - 1;
                    ArrayofPrice[BestStringFunctionEver(FoodinGarbage)] = ToateTipurileDeMancare[BestStringFunctionEver(FoodinGarbage)].Price * Array[BestStringFunctionEver(FoodinGarbage)];
                    pricearrayfunction();

                    onepropretychanged("Price");
                    if (MancareaAleasa[i].Number == 0)
                    {
                        FoodinGarbage = "";
                        MancareaAleasa.RemoveAt(i);
                    }
                }

            }
        }



        public void Selectingtoremove(string paramater)
        {
            FoodinGarbage = paramater;
            for (int i = 0; i < MancareaAleasa.Count; i++)
            {
                if (MancareaAleasa[i].Food.Equals(paramater))
                {
                    MancareaAleasa[i].Colorr = "#034488";
                }
                else
                {
                    MancareaAleasa[i].Colorr = "White";
                }
            }
        }



        public void Remove(string paramater)
        {
            for (int i = 0; i < MancareaAleasa.Count; i++)
            {
                if (MancareaAleasa[i].Food.Equals(FoodinGarbage))
                {
                    MancareaAleasa.RemoveAt(i);
                    Array[BestStringFunctionEver(FoodinGarbage)] = 0;
                    ArrayofPrice[BestStringFunctionEver(FoodinGarbage)] = 0;
                    FoodinGarbage = "";


                    pricearrayfunction();
                }

            }
        }

        public void RemoveallFood(string paramater)
        {

            for (int i = 0; i < 25; i++)
            {
                if (Array[i] != 0)
                {
                    Array[i] = 0;

                }
            }
            int k = MancareaAleasa.Count;
            for (int i = 0; i < k; i++)
            {
                MancareaAleasa.RemoveAt(0);
            }
            for (int i = 0; i < 25; i++)
            {
                ArrayofPrice[i] = 0;

            }
            pricearrayfunction();


        }


        public void GetingFoodName(string paramater)
        {
            for (int i = 0; i < 25; i++)
            {
                if (ToateTipurileDeMancare[i].Food.Equals(paramater))
                {
                    ToateTipurileDeMancare[i].Colorr = "#034488";
                }
                else
                {
                    ToateTipurileDeMancare[i].Colorr = "White";
                }
            }
            FoodinAir = paramater;
            ArrayIndex = BestStringFunctionEver(paramater);


        }
        public void AddingFood(string paramater)
        {

            if (ArrayIndex != -1)
            {

                Array[ArrayIndex] = Array[ArrayIndex] + 1;

                ArrayofPrice[ArrayIndex] = ToateTipurileDeMancare[ArrayIndex].Price * Array[ArrayIndex];
                pricearrayfunction();




                if (ArrayIndex == -1)
                {

                }
                else
                {


                    for (int i = 0; i < MancareaAleasa.Count; i++)
                    {
                        if (MancareaAleasa[i].Food.Contains(FoodinAir))
                        {
                            MancareaAleasa[i].Number = Array[ArrayIndex];
                            break;
                        }

                        else if (i == MancareaAleasa.Count - 1)
                        {
                            MancareaAleasa.Add(new ShowButton() { Food = FoodinAir, Number = Array[ArrayIndex], Colorr = "White" });
                        }
                    }
                }

                if (MancareaAleasa.Count == 0)
                {
                    MancareaAleasa.Add(new ShowButton() { Food = FoodinAir, Number = Array[ArrayIndex], Colorr = "White" });

                }
            }
        }
        public void pricearrayfunction()
        {
            double total = 0;
            for (int i = 0; i < 25; i++)
            {
                total = total + ArrayofPrice[i];

            }
            Price = total;
            onepropretychanged("Price");

        }

        public int BestStringFunctionEver(string foodname)
        {
            if (foodname.Equals("CIORBA TARANEASCA DE VACUTA"))
            { return 0; }
            else if (foodname.Equals("SUPA CREMA DE LEGUME CU CRUTOANE"))
            { return 1; }
            else if (foodname.Equals("PILAF DE OREZ CU MORCOVI"))
            { return 2; }
            else if (foodname.Equals("PIURE DE CARTOFI CU UNT"))
            { return 3; }
            else if (foodname.Equals("CARTOFI CU MASLINE"))
            { return 4; }
            else if (foodname.Equals("CARTOFI CU UNT SI SMANTANA"))
            { return 5; }
            else if (foodname.Equals("CASCAVAL PANE"))
            { return 6; }
            else if (foodname.Equals("GRATAR COTLET DE PORC"))
            { return 7; }
            else if (foodname.Equals("SNITEL PANE DE PUI"))
            { return 8; }
            else if (foodname.Equals("GRATAR PIEPT DE PUI"))
            { return 9; }
            else if (foodname.Equals("MAMALIGA CU BRANZA SI SMANTANA"))
            { return 10; }
            else if (foodname.Equals("SPAGHETE CU ROSII SI MASLINE"))
            { return 11; }
            else if (foodname.Equals("CARTOFI FRANTUZESTI"))
            { return 12; }
            else if (foodname.Equals("FRIPTURA PUI CU SOS TOMAT"))
            { return 13; }
            else if (foodname.Equals("SALATA ORIENTALA MURATURI"))
            { return 14; }
            else if (foodname.Equals(" MICI LA GRATAR"))
            { return 15; }
            else if (foodname.Equals("COD ALASKA PANE"))
            { return 16; }
            else if (foodname.Equals("CARTOFI GRATINATI CU SMANTANA"))
            { return 17; }
            else if (foodname.Equals("CARTOFI HASH"))
            { return 18; }
            else if (foodname.Equals("CARTOFI RONDELE"))
            { return 19; }
            else if (foodname.Equals("CARTOFI PAI"))
            { return 20; }
            else if (foodname.Equals("OREZ CU LAPTE"))
            { return 21; }
            else if (foodname.Equals("SALATA DE VARZA"))
            { return 22; }
            else if (foodname.Equals("SALATA DE MURATURI ASORTATE"))
            { return 23; }
            else if (foodname.Equals("SALATA DE SFECLA ROSIE"))
            { return 24; }
            return -1;
        }
        public void MakeCollection()
        {
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CIORBA TARANEASCA DE VACUTA", Price = 4.70 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SUPA CREMA DE LEGUME CU CRUTOANE", Price = 2.60 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "PILAF DE OREZ CU MORCOVI", Price = 1.60 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "PIURE DE CARTOFI CU UNT", Price = 1.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI CU MASLINE", Price = 2.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI CU UNT SI SMANTANA", Price = 4.00 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CASCAVAL PANE", Price = 5.60 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "GRATAR COTLET DE PORC", Price = 3.70 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SNITEL PANE DE PUI", Price = 4.00 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "GRATAR PIEPT DE PUI", Price = 5.60 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "MAMALIGA CU BRANZA SI SMANTANA", Price = 2.90 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SPAGHETE CU ROSII SI MASLINE", Price = 4.50 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI FRANTUZESTI", Price = 2.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "FRIPTURA PUI CU SOS TOMAT", Price = 4.60 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SALATA ORIENTALA MURATURI", Price = 2.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = " MICI LA GRATAR", Price = 3.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "COD ALASKA PANE", Price = 5.90 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI GRATINATI CU SMANTANA", Price = 2.90 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI HASH", Price = 2.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI RONDELE", Price = 2.50 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "CARTOFI PAI", Price = 1.40 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "OREZ CU LAPTE", Price = 1.30 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SALATA DE VARZA", Price = 1.10 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SALATA DE MURATURI ASORTATE", Price = 2.20 });
            ToateTipurileDeMancare.Add(new ShowButton() { Food = "SALATA DE SFECLA ROSIE", Price = 1.10 });
            for (int i = 0; i < 25; i++)
            {
                ToateTipurileDeMancare[i].Colorr = "White";

            }



        }

        public int[] GetArrayoffood()
        {
            return Array;
        }
        public double GetPrice()
        {
            return Price;
        }


    } 

}
