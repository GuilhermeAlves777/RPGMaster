using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class ItemAtributoService
    {
        private readonly ItemAtributoRepository _itemAtributoRepository;
        private readonly ItemRepository _itemRepository;
        private readonly AtributoRepository _atributoRepository;

        public ItemAtributoService(ItemAtributoRepository itemAtributoRepository,
            ItemRepository itemRepository,
            AtributoRepository atributoRepository)
        {
            _itemAtributoRepository = itemAtributoRepository;
            _itemRepository = itemRepository;
            _atributoRepository = atributoRepository;
        }

        public List<Item_Atributo> ObterTodos(long idItem) =>
            _itemAtributoRepository.ObterTodos(idItem);

        public bool Adicionar(long idItem, long idAtributo, long idCampanha, int modificador)
        {
            var item = _itemRepository.ObterEntidadePorId(idItem, idCampanha);
            var atr = _atributoRepository.ObterEntidadePorId(idAtributo, idCampanha);

            if (item == null || atr == null) throw new Exception("Item ou atributo não existem na campanha");
            if (_itemAtributoRepository.JaPossui(idItem, idAtributo)) throw new Exception("Item já possui esse modificador de atributo");

            var ia = new Item_Atributo
            {
                Id_Item = idItem,
                Id_Atributo = idAtributo,
                Modificador = modificador
            };

            var ret = _itemAtributoRepository.Adicionar(ia);
            if (!ret) throw new Exception("Não foi possível adicionar o modificador ao item");

            return ret;
        }

        public bool Editar(long idItem, long idCampanha, long idAtributo, int modificador)
        {
            var item = _itemRepository.ObterEntidadePorId(idItem, idCampanha);
            var atr = _atributoRepository.ObterEntidadePorId(idAtributo, idCampanha);
            if (item == null || atr == null) throw new Exception("Item ou atributo não existem na campanha");

            var ia = _itemAtributoRepository.Obter(idItem, idAtributo);
            if (ia == null) throw new Exception("Esse item não possui esse atributo");

            ia.Modificador = modificador;

            var ret = _itemAtributoRepository.Editar();
            if (!ret) throw new Exception("Não foi possível editar o modificador");

            return ret;
        }

        public bool Excluir(long idItem, long idAtributo)
        {
            var ia = _itemAtributoRepository.Obter(idItem, idAtributo);
            if (ia == null) throw new Exception("Esse item não possui esse atributo");

            return _itemAtributoRepository.Excluir(ia);
        }
    }
}
