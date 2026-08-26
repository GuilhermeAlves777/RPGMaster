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

        public int Rolar()
        {
            int total = 0;
            for (int i = 0; i < Quantidade; i++)
            {
                total += Random.Shared.Next(1, (int)Faces + 1);
            }
            return total + Modificador;
        }
    }
}
