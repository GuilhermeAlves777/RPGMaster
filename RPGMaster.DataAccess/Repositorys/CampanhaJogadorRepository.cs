using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class CampanhaJogadorRepository
    {
        private readonly RPGMasterContext _context;

        public CampanhaJogadorRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public bool JaParticipa(long idCampanha, long idUsuario)
        {
            return _context.CampanhaJogadores
                .Any(x => x.Id_Campanha == idCampanha && x.Id_Usuario == idUsuario);
        }

        public bool Adicionar(Campanha_Jogador jogador)
        {
            _context.CampanhaJogadores.Add(jogador);
            return _context.SaveChanges() > 0;
        }
    }
}
