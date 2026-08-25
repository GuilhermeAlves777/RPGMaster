using RPGMaster.Model;
using RPGMaster.Model.DTOs;
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

        public List<Campanha_Jogador> ObterTodos ()
        {
            var jogadores = _context.CampanhaJogadores.ToList();
            return jogadores;
        }

        public Campanha_Jogador? ObterJogador(long idCampanha, long idUsuario)
        {
            var jogador = _context.CampanhaJogadores.Where(cj => cj.Id_Campanha == idCampanha && cj.Id_Usuario == idUsuario).FirstOrDefault();
            return jogador;
        }

        public List<CampanhaJogadorDto> ObterJogadorPorCampanha(long idCampanha)
        {
            return _context.CampanhaJogadores
                .Where(cj => cj.Id_Campanha == idCampanha)
                .Select(cj => new CampanhaJogadorDto
                {
                    Id_Usuario = cj.Id_Usuario,
                    NomeUsuario = cj.Usuario.user,
                    Id_Campanha = cj.Id_Campanha,
                    NomeCampanha = cj.Campanha.Nome
                })
                .ToList();
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

        public bool ExcluirJogador (Campanha_Jogador jogador)
        {
            _context.CampanhaJogadores.Remove(jogador);
            return _context.SaveChanges() > 0;
        }
    }
}
