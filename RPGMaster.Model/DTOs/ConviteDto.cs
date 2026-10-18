using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class ConviteDto
    {
        public long Id_Convite { get; set; }
        public long Id_Campanha { get; set; }
        public string Campanha { get; set; }
    }
}
