using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Proekt_BarBer
{

    internal class Driver
    {
        string textBoxName;
        string  textBoxDate;
        string textBoxTime;
        string textBoxUsl;
        string textBoxDescription;
        string textBoxTimecomletion;
        string textBoxPrice1;
        string textBoxPrice2;
        string textBoxMaster;
        decimal price;
        


        public Driver()
        {

        }
        public string TextBoxName { get => textBoxName; set => textBoxName = value; }
        public string TextBoxDate { get => textBoxDate; set => textBoxDate = value; }
        public string TextBoxTime { get => textBoxTime; set => textBoxTime = value; }
        public string TextBoxUsl { get => textBoxUsl; set => textBoxUsl = value; }
        public string TextBoxDescription { get => textBoxDescription; set => textBoxDescription = value; }
        public string TextBoxTimecomletion { get => textBoxTimecomletion; set => textBoxTimecomletion = value; }
        public decimal Price { get => price; set => price = value; }





        public string TextBoxPrice1
        {
            get => textBoxPrice1;
            set => textBoxPrice1 = string.IsNullOrEmpty(value) ? "0" : value;
        }

        public string TextBoxPrice2
        {
            get => textBoxPrice2;
            set => textBoxPrice2 = string.IsNullOrEmpty(value) ? "0" : value;
        }
        public string TextBoxMaster { get => textBoxMaster; set => textBoxMaster = value; }

        

        public override string ToString()
        {


            return $"Запись внесена";

        }
    }
}

