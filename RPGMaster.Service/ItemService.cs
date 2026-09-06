using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository) => _itemRepository = itemRepository;

        public List<ItemDto> ObterPorCampanha(long idCampanha) =>
            _itemRepository.ObterPorCampanha(idCampanha);

        public bool Cadastrar(string nome, TiposEnum tipo, string? descricao, string? imagem, long idCampanha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O item precisa ter um nome");

            if (tipo <= 0) throw new Exception("O item precisa de um tipo");

            var item = new Item { Nome = nome, Tipo = tipo, Descricao = descricao, Imagem = imagem, Id_Campanha = idCampanha };
            return _itemRepository.Cadastrar(item);
        }

        public bool Atualizar(long id, string? nome, TiposEnum tipo, string? descricao, string? imagem)
        {
            var item = _itemRepository.ObterEntidadePorId(id)
                ?? throw new Exception("Esse item não existe");

            if (nome != null) item.Nome = nome;
            if (tipo != item.Tipo && tipo <= 0) item.Tipo = tipo;
            if (descricao != null) item.Descricao = descricao;
            if (imagem != null) item.Imagem = imagem;

            return _itemRepository.Atualizar();
        }

        public bool Deletar(long id)
        {
            var item = _itemRepository.ObterEntidadePorId(id)
                ?? throw new Exception("Esse item não existe");

            return _itemRepository.Deletar(item);
        }
    }
}
