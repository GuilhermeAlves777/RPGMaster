using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RPGMaster.Model.DTOs
{
    public class UsuarioDto
    {
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string User { get; set; }

        [JsonIgnore]
        public string SenhaHash { get; set; }
        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        public long Id_Campanha { get; set; }
        public List<CampanhaUsuarioDto> Campanhas { get; set; }
    }
}
