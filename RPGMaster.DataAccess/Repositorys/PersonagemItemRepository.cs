using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PersonagemItemRepository
    {
        private readonly RPGMasterContext _context;
        public PersonagemItemRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public List<Personagem_Item> ObterTodos(long idPersonagem) => _context.PersonagemItens.Where(pi => pi.Id_Personagem == idPersonagem).ToList();
        public Personagem_Item? ObterPorId(long idPersonagem, long idItem) =>
            _context.PersonagemItens.Where(pi => pi.Id_Personagem == idPersonagem && pi.Id_Item == idItem).FirstOrDefault();

        public bool Adicionar(Personagem_Item pi)
        {
            _context.PersonagemItens.Add(pi);
            return _context.SaveChanges() > 0;
        }

        public bool EditarQuantidade() => _context.SaveChanges() > 0;

        public bool Excluir(Personagem_Item pi)
        {
            _context.PersonagemItens.Remove(pi);
            return _context.SaveChanges() > 0;
        }
    }
}
