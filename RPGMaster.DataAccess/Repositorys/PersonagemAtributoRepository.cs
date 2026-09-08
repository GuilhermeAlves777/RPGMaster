using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PersonagemAtributoRepository
    {
        private readonly RPGMasterContext _context;

        public PersonagemAtributoRepository (RPGMasterContext context)
        {
            _context = context;
        }

        public List<Personagem_Atributo> ObterTodos(long idPersonagem) =>
            _context.PersonagemAtributos.Where(pa => pa.Id_Personagem == idPersonagem).ToList();

        public Personagem_Atributo? Obter(long idPersonagem, long idAtributo) => 
            _context.PersonagemAtributos.Where(pa => pa.Id_Personagem == idPersonagem && pa.Id_Atributo == idAtributo).FirstOrDefault(); 

        public bool JaPossui (long idPersonagem, long idAtributo) => 
            _context.PersonagemAtributos.Any(pa => pa.Id_Personagem == idPersonagem && pa.Id_Atributo == idAtributo);

        public bool Adicionar (Personagem_Atributo pa)
        {
            _context.PersonagemAtributos.Add(pa);
            return _context.SaveChanges() > 0;
        }

        public bool Editar() => _context.SaveChanges() > 0;

        public bool Excluir(Personagem_Atributo pa)
        {
            _context.PersonagemAtributos.Remove(pa);
            return _context.SaveChanges () > 0;
        }
    }
}
