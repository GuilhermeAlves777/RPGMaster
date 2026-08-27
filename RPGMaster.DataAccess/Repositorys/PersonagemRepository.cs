using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PersonagemRepository
    {
        private readonly RPGMasterContext _context;

        public PersonagemRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public List<Personagem> ObterTodos()
        {
            var listaPersonagens = _context.Personagens.ToList();
            return listaPersonagens;
        }

        public Personagem? ObterPorId(long id, long idCampanha)
        {
            var personagem = _context.Personagens.Where(p => p.Id_Personagem == id && p.Id_Campanha == idCampanha).FirstOrDefault();
            return personagem;
        }

        public bool CriarPersonagem(Personagem persona)
        {
            _context.Personagens.Add(persona);
            return _context.SaveChanges() > 0;
        }

        public bool EditarPersonagem()
        {
            return _context.SaveChanges() > 0;
        }

        public bool ExcluirPersonagem(long idPersonagem, long idCampanha)
        {
            var personagem = _context.Personagens.Where(p => p.Id_Personagem == idPersonagem && p.Id_Campanha == idCampanha).FirstOrDefault();
            _context.Personagens.Remove(personagem);

            return _context.SaveChanges() > 0;
        }

        public bool PossuiPersonagem (long idJogador, long vidaAtual)
        {
            return _context.Personagens.Any(p => p.Id_Jogador == idJogador && p.VidaAtual == vidaAtual);
        }
    }
}
