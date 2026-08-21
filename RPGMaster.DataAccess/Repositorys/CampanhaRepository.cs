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

        public List<CampanhaDto> ObterPorId (long id)
        {
            return _context.Campanhas
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
                .ToList();
        }
    }
}
