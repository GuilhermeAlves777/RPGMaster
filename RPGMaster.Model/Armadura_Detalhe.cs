using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Armadura_Detalhe
    {
        public long Id_Item {  get; set; }
        public Item Item { get; set; }
        public int Defesa { get; set; }
    }
}
