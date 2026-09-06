using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ClasseRepository
    {
        private readonly RPGMasterContext _context;

        public ClasseRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public List<Classe> ObterTodos (long idCampanha)
        {
            return _context.Classes.Where(c => c.Id_Campanha == idCampanha).ToList();
        }

        public Classe? ObterPorId (long id, long idCampanha)
        {
            return _context.Classes.Where(c => c.Id_Classe == id && c.Id_Campanha == idCampanha).FirstOrDefault();
        }

        public bool AdicionarClasse (Classe c)
        {
            _context.Classes.Add(c);
            return _context.SaveChanges() > 0;
        }

        public bool EditarClasse ()
        {
            return _context.SaveChanges () > 0;
        }

        public bool ExcluirClasse (long id, long idCampanha)
        {
            var c = _context.Classes.Where(c => c.Id_Classe == id && c.Id_Campanha == idCampanha).FirstOrDefault();
            _context.Classes.Remove(c);

            return _context.SaveChanges() > 0;
        }
    }
}
