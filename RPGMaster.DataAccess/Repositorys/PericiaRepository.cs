using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class PericiaRepository
    {
        private readonly RPGMasterContext _context;

        public PericiaRepository(RPGMasterContext context) => _context = context;

        public List<PericiaDto> ObterPorCampanha(long idCampanha)
        {
            return _context.Pericias.AsNoTracking()
                .Where(p => p.Id_Campanha == idCampanha)
                .Select(p => new PericiaDto
                {
                    Id_Pericia = p.Id_Pericia,
                    Nome = p.Nome,
                    Valor_Padrao = p.Valor_Padrao,
                    Id_Campanha = p.Id_Campanha
                }).ToList();
        }

        public Pericia? ObterEntidadePorId(long id, long idCampanha) =>
            _context.Pericias.FirstOrDefault(p => p.Id_Pericia == id && p.Id_Campanha == idCampanha);

        public bool Cadastrar(Pericia pericia)
        {
            _context.Pericias.Add(pericia);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Pericia pericia)
        {
            _context.Pericias.Remove(pericia);
            return _context.SaveChanges() > 0;
        }
    }
}
