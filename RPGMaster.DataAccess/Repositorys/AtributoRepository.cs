using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class AtributoRepository
    {
        private readonly RPGMasterContext _context;

        public AtributoRepository(RPGMasterContext context) => _context = context;

        public List<AtributoDto> ObterPorCampanha(long idCampanha)
        {
            return _context.Atributos.AsNoTracking()
                .Where(a => a.Id_Campanha == idCampanha)
                .Select(a => new AtributoDto
                {
                    Id_Atributo = a.Id_Atributo,
                    Nome = a.Nome,
                    Valor_Padrao = a.Valor_Padrao,
                    Id_Campanha = a.Id_Campanha
                }).ToList();
        }

        public Atributo? ObterEntidadePorId(long id, long idCampanha) =>
            _context.Atributos.FirstOrDefault(a => a.Id_Atributo == id && a.Id_Campanha == idCampanha);

        public bool Cadastrar(Atributo atributo)
        {
            _context.Atributos.Add(atributo);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Atributo atributo)
        {
            _context.Atributos.Remove(atributo);
            return _context.SaveChanges() > 0;
        }
    }
}
