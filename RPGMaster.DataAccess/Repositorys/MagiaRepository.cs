using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class MagiaRepository
    {
        private readonly RPGMasterContext _context;

        public MagiaRepository(RPGMasterContext context) => _context = context;

        public List<MagiaDto> ObterPorCampanha(long idCampanha)
        {
            return _context.Magias.AsNoTracking()
                .Where(m => m.Id_Campanha == idCampanha)
                .Select(m => new MagiaDto
                {
                    Id_Magia = m.Id_Magia,
                    Nome = m.Nome,
                    Dados = m.Dados,
                    Id_Campanha = m.Id_Campanha
                }).ToList();
        }

        public Magia? ObterEntidadePorId(long id) =>
            _context.Magias.FirstOrDefault(m => m.Id_Magia == id);

        public bool Cadastrar(Magia magia)
        {
            _context.Magias.Add(magia);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Magia magia)
        {
            _context.Magias.Remove(magia);
            return _context.SaveChanges() > 0;
        }
    }

}
