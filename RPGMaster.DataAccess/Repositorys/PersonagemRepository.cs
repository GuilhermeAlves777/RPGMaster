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

        public Personagem? ObterPorId(long id)
        {
            var personagem = _context.Personagens.Where(p => p.Id_Personagem == id).FirstOrDefault();
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

        public bool ExcluirPersonagem(long id)
        {
            var personagem = _context.Personagens.Where(p => p.Id_Personagem == id).FirstOrDefault();
            _context.Personagens.Remove(personagem);

            return _context.SaveChanges() > 0;
        }

        public Personagem ObterPersonagemPorJogador

        public bool PossuiPersonagem (long idJogador, int vidaAtual)
        {
            return _context.Personagens.Any(p => p.Id_Jogador == idJogador && p.VidaAtual == vidaAtual);
        }
    }
}
