using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
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

        public List<CampanhaDto> ObterTodas()
        {
            return _context.Campanhas
                .Select(c => new CampanhaDto
                {
                    Id_Campanha = c.ID_Campanha,
                    Nome = c.Nome,
                    Id_Criador = c.ID_Criador,
                    Personagens = c.Personagens.Select(p => new PersonagemResumoDto
                    {
                        Id_Personagem = p.Id_Personagem,
                        Nome = p.Nome,
                        EhNpc = p.EhNpc
                    }).ToList(),
                    Jogadores = c.CampanhaJogadores.Select(cj => new JogadorResumoDto
                    {
                        Id_Usuario = cj.Id_Usuario,
                        Nome = cj.Usuario.Nome
                    }).ToList()
                })
                .ToList();
        }

        public CampanhaDto? ObterPorId (long id)
        {
            return _context.Campanhas.AsNoTracking()
                .Where(x => x.ID_Campanha == id)
                .Select(c => new CampanhaDto
                {
                    Id_Campanha = c.ID_Campanha,
                    Nome = c.Nome,
                    Id_Criador = c.ID_Criador,
                    Personagens = c.Personagens.Select(p => new PersonagemResumoDto
                    {
                        Id_Personagem = p.Id_Personagem,
                        Nome = p.Nome,
                        EhNpc = p.EhNpc
                    }).ToList(),
                    Jogadores = c.CampanhaJogadores.Select(cj => new JogadorResumoDto
                    {
                        Id_Usuario = cj.Id_Usuario,
                        Nome = cj.Usuario.Nome
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public List<CampanhaDto> ObterPorJogador(long idJogador)
        {
            return _context.Campanhas.AsNoTracking()
                .Where(c => c.ID_Criador == idJogador || c.CampanhaJogadores.Any(cj => cj.Id_Usuario == idJogador ))
                .Select(c => new CampanhaDto
                {
                    Id_Campanha = c.ID_Campanha,
                    Nome = c.Nome,
                    Id_Criador = c.ID_Criador,
                    Personagens = c.Personagens.Select(p => new PersonagemResumoDto
                    {
                        Id_Personagem = p.Id_Personagem,
                        Nome = p.Nome,
                        EhNpc = p.EhNpc,
                        Nivel = p.Nivel,
                        vidaMaxima = p.VidaMaxima,
                        vidaAtual = p.VidaAtual,
                        manaMaxima = p.ManaMaxima,
                        manaAtual = p.ManaAtual
                    }).ToList(),
                    Jogadores = c.CampanhaJogadores.Select(cj => new JogadorResumoDto
                    {
                        Id_Usuario = cj.Id_Usuario,
                        Nome = cj.Usuario.Nome
                    }).ToList()
                })
                .ToList();
        }

        public Campanha? ObterPorIdSemDto(long id) {
            return _context.Campanhas.FirstOrDefault(x => x.ID_Campanha == id);
        }

        public bool Excluir(Campanha campanha)
        {
            _context.Campanhas.Remove(campanha);
            return _context.SaveChanges() > 0;
        }
        public bool Atualizar()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
