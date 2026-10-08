using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ItemAtributoRepository
    {
        private readonly RPGMasterContext _context;

        public ItemAtributoRepository(RPGMasterContext context) => _context = context;

        public List<Item_Atributo> ObterTodos(long idItem) =>
            _context.ItemAtributos.Where(ia => ia.Id_Item == idItem).ToList();

        public Item_Atributo? Obter(long idItem, long idAtributo) =>
            _context.ItemAtributos.FirstOrDefault(ia => ia.Id_Item == idItem && ia.Id_Atributo == idAtributo);

        public bool JaPossui(long idItem, long idAtributo) =>
            _context.ItemAtributos.Any(ia => ia.Id_Item == idItem && ia.Id_Atributo == idAtributo);

        public bool Adicionar(Item_Atributo ia)
        {
            _context.ItemAtributos.Add(ia);
            return _context.SaveChanges() > 0;
        }

        public bool Editar() => _context.SaveChanges() > 0;

        public bool Excluir(Item_Atributo ia)
        {
            _context.ItemAtributos.Remove(ia);
            return _context.SaveChanges() > 0;
        }
    }
}
