using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Dado
    {
        public int Quantidade { get; set; }      
        public TipoDado Faces { get; set; }  
        public int Modificador { get; set; }     
    }
}
