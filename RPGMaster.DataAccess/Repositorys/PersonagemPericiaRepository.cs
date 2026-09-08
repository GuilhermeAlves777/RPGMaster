using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PersonagemPericiaRepository
    {
        private readonly RPGMasterContext _context;

        public PersonagemPericiaRepository(RPGMasterContext context) => _context = context;

        public List<Personagem_Pericia> ObterTodos(long idPersonagem) =>
            _context.PersonagemPericias.Where(pp => pp.Id_Personagem == idPersonagem).ToList();

        public Personagem_Pericia? Obter(long idPersonagem, long idPericia) =>
            _context.PersonagemPericias.FirstOrDefault(pp => pp.Id_Personagem == idPersonagem && pp.Id_Pericia == idPericia);

        public bool JaPossui(long idPersonagem, long idPericia) =>
            _context.PersonagemPericias.Any(pp => pp.Id_Personagem == idPersonagem && pp.Id_Pericia == idPericia);

        public bool Adicionar(Personagem_Pericia pp)
        {
            _context.PersonagemPericias.Add(pp);
            return _context.SaveChanges() > 0;
        }

        public bool Editar() => _context.SaveChanges() > 0;

        public bool Excluir(Personagem_Pericia pp)
        {
            _context.PersonagemPericias.Remove(pp);
            return _context.SaveChanges() > 0;
        }
    }
}
