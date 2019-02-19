using FoodandDrinks.Model.NotfyStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodandDrinks.Model
{
    public class ShowButton : Notfy
    {
        public string Food { get; set; }
        public double Price { get; set; }
        private int nr;
        private string color;
        public string Colorr
        {

            get
            {

                return color;
            }

            set
            {
                if (color != value)
                {

                    color = value;
                    onepropretychanged("Colorr");

                }


            }
        }
        public int Number
        {

            get
            {

                return nr;
            }

            set
            {
                if (nr != value)
                {

                    nr = value;
                    onepropretychanged("Number");

                }


            }
        }
    }
}
