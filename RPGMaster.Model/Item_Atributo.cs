using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Item_Atributo
    {
        public long Id_Item { get; set; }
        public Item Item { get; set; }

        public long Id_Atributo { get; set; }
        public Atributo Atributo { get; set; }

        public int Modificador { get; set; }
    }
}
