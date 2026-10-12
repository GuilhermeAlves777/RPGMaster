using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
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

        public List<Personagem> ObterTodos(long idCampanha)
        {
            var listaPersonagens = _context.Personagens.Where(p => p.Id_Campanha == idCampanha).ToList();
            return listaPersonagens;
        }


        public Personagem? ObterPorId(long id, long idCampanha)
        {
            var personagem = _context.Personagens.Where(p => p.Id_Personagem == id && p.Id_Campanha == idCampanha).FirstOrDefault();
            return personagem;
        }

        public PersonagemDetalheDto? ObterPorIdFront(long idPersonagem, long idCampanha)
        {
            return _context.Personagens.AsNoTracking()
                .Where(p => p.Id_Personagem == idPersonagem && p.Id_Campanha == idCampanha)
                .Select(pd => new PersonagemDetalheDto
                {
                    Id_Campanha = pd.Id_Campanha,
                    Id_Personagem = pd.Id_Personagem,
                    Nome = pd.Nome,
                    Nivel = pd.Nivel,
                    EhNpc = pd.EhNpc,
                    Id_Jogador = pd.Id_Jogador,
                    NomeJogador = pd.Jogador != null ? pd.Jogador.Nome : null,
                    vidaMaxima = pd.VidaMaxima,
                    vidaAtual = pd.VidaAtual,
                    manaAtual = pd.ManaAtual,
                    manaMaxima = pd.ManaMaxima,
                    Imagem = pd.Imagem,
                    Atributos = pd.Atributos
                    .Select(a => new PersonagemAtributoDto
                    {
                        Id_Atributo = a.Id_Atributo,
                        Nome = a.Atributo.Nome,
                        Valor = a.Valor
                    }).ToList(),
                    Classe = new Classe
                    {
                        Id_Classe = pd.Classe.Id_Classe,
                        Nome = pd.Classe.Nome,
                        Descricao = pd.Classe.Descricao
                    },
                    Itens = pd.Itens.Select(i => new PersonagemItemDto
                    {
                        Id_Item = i.Id_Item,
                        Nome = i.Item.Nome,
                        Imagem = i.Item.Imagem,
                        Quantidade = i.Quantidade
                    }).ToList(),
                    Pericias = pd.Pericias
                    .Select(p => new PersonagemPericiaDto
                    {
                        Id_Pericia = p.Id_Pericia,
                        Nome = p.Pericia.Nome,
                        Valor = p.Valor
                    }).ToList(),
                    Magias = pd.Magias
                    .Select( m => new PersonagemMagiaDto
                    {
                        Id_Magia = m.Id_Magia,
                        Nome = m.Magia.Nome,
                        Dados = m.Magia.Dados
                    }).ToList()
                }).FirstOrDefault();
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

            if (personagem == null) throw new NullReferenceException("Personagem não existe");
            _context.Personagens.Remove(personagem);

            return _context.SaveChanges() > 0;
        }

        public bool PossuiPersonagem (long idJogador, long vidaAtual)
        {
            return _context.Personagens.Any(p => p.Id_Jogador == idJogador && p.VidaAtual == vidaAtual);
        }
    }
}
