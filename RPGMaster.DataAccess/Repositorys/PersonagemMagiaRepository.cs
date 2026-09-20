using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PersonagemMagiaRepository
    {
        private readonly RPGMasterContext _context;

        public PersonagemMagiaRepository (RPGMasterContext context)
        {
            _context = context;
        }

        public List<Personagem_Magia> ObterTodos (long idPersonagem) =>  
            _context.PersonagemMagias.Where(pm => pm.Id_Personagem == idPersonagem).ToList();

        public Personagem_Magia? ObterPorId(long idPersonagem, long idMagia) =>
            _context.PersonagemMagias.Where(pm => pm.Id_Personagem == idPersonagem && pm.Id_Magia == idMagia).FirstOrDefault();

        public bool Adicionar (Personagem_Magia pm)
        {
            _context.PersonagemMagias.Add(pm);
            return _context.SaveChanges() > 0;
        }

        public bool JaPossui (long idPersonagem, long idMagia) =>
            _context.PersonagemMagias.Any(pm => pm.Id_Personagem == idPersonagem && pm.Id_Magia == idMagia);

        public bool Editar () => _context.SaveChanges() > 0;

        public bool Excluir(Personagem_Magia pm) 
        { 
            _context.PersonagemMagias.Remove(pm);
            return _context.SaveChanges () > 0;
        } 
        
    }
}
