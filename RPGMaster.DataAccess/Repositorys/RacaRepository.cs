using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class RacaRepository
    {
        private readonly RPGMasterContext _context;

        public RacaRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public List<Raca> ObterTodos(long idCampanha)
        {
            return _context.Racas.Where(r => r.Id_Campanha == idCampanha).ToList();    
        }

        public Raca? ObterPorId (long id, long idCampanha)
        {
            return _context.Racas.Where(r => r.Id_Raca == id && r.Id_Campanha == idCampanha).FirstOrDefault();
        }

        public bool CriarRaca(Raca raca)
        {
            _context.Racas.Add(raca);
            return _context.SaveChanges() > 0;
        }

        public bool EditarRaca (Raca raca)
        {
            return _context.SaveChanges() > 0;
        }

        public bool ExcluirRaca (long id, long idCampanha)
        {
            var raca = _context.Racas.Where(r => r.Id_Raca == id && r.Id_Campanha == idCampanha).FirstOrDefault();
            _context.Racas.Remove(raca);

            return _context.SaveChanges() > 0;
        }
    }
}
