using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALMENT_proj
{
    public class Menu
    {
    public int _FoodID { get; set; }
    public string _Name { get; set; }
    public string _Description { get; set; }
    public float _Price { get; set; } // Or Price? Up to your DB type
    public bool _Availability { get; set; }

        public Menu()
        {
            /*
            This constructor is used when we need to create a new Food Item
             */

            _FoodID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _Price = 0.00f;
            _Availability = false;
        }

        public Menu(int FoodID, string Name, string Description, float Price, bool Availability)
        {
            /*
            This constructor is used when we need to manage existing Menu Item
             */

            _FoodID = FoodID;
            _Name = Name;
            _Description = Description;
            _Price = Price;
            _Availability = Availability;
        }

        
        public int FoodID
        {
            //this has to be read only as stated it is automatically generated in SQL
            get { return _FoodID; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public float Price
        {
            get { return _Price; }
            set
            {
                _Price = value;

            }
        }

        public bool Availability
        {
            get { return _Availability; }
            set { _Availability = value; }
        }
    }

}
