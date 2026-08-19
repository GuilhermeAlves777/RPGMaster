using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class CampanhaRepository
    {
        private readonly RPGMasterContext _context;

        public CampanhaRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public bool Cadastrar(Campanha c)
        {
            //inserir a base de dados
            _context.Campanhas.Add(c);
            return _context.SaveChanges() > 0;
        }

        public List<Campanha> ObterTodos()
        {
            return _context.Campanhas.ToList();
        }
    }
}
