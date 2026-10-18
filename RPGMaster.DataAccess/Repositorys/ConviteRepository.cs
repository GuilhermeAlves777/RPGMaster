using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ConviteRepository
    {
        private readonly RPGMasterContext _context;

        public ConviteRepository (RPGMasterContext context)
        {
            _context = context;
        }

        public bool GerarConvite(Convite c)
        {
            _context.Convite.Add(c);
            return _context.SaveChanges() > 0;
        }

        public List<Convite> ObterAtivoPorCampanha(long idCampanha, bool ativo = true) =>
            _context.Convite.Where(c => c.Id_Campanha == idCampanha && c.Ativo == ativo).ToList();

        public Convite? ObterUltimoAtivo(long idCampanha, bool ativo = true) =>
             _context.Convite.Where(c => c.Id_Campanha == idCampanha && c.Ativo == ativo)
            .OrderByDescending(c => c.Id_Convite)
            .FirstOrDefault();

        public ConviteDto? ConviteRecebido(string codigo) =>
            _context.Convite.Where(c => c.Codigo == codigo && c.Ativo == true)
            .Select(con => new ConviteDto
            {
                Id_Campanha = con.Id_Campanha,
                Id_Convite = con.Id_Convite,
                Campanha = con.Campanha.Nome
            }).FirstOrDefault();

        public bool Atualizar()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
