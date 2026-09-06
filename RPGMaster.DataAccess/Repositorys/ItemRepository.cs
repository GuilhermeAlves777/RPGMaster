using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ItemRepository
    {
        private readonly RPGMasterContext _context;

        public ItemRepository(RPGMasterContext context) => _context = context;

        public List<ItemDto> ObterPorCampanha(long idCampanha)
        {
            return _context.Itens.AsNoTracking()
                .Where(i => i.Id_Campanha == idCampanha)
                .Select(i => new ItemDto
                {
                    Id_Item = i.Id_Item,
                    Nome = i.Nome,
                    Tipo = i.Tipo,
                    Descricao = i.Descricao,
                    Imagem = i.Imagem,
                    Id_Campanha = i.Id_Campanha
                }).ToList();
        }

        public Item? ObterEntidadePorId(long id) =>
            _context.Itens.FirstOrDefault(i => i.Id_Item == id);

        public bool Cadastrar(Item item)
        {
            _context.Itens.Add(item);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Item item)
        {
            _context.Itens.Remove(item);
            return _context.SaveChanges() > 0;
        }
    }
}
