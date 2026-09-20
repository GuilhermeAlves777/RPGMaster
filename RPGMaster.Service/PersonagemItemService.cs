using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace RPGMaster.Service
{
    public class PersonagemItemService
    {
        private readonly PersonagemItemRepository _repository;
        private readonly PersonagemRepository _personagemRepository;
        private readonly ItemRepository _itemRepository;

        public PersonagemItemService (PersonagemItemRepository repository, PersonagemRepository personagemRepository, ItemRepository itemRepository)
        {
            _repository = repository;
            _personagemRepository = personagemRepository;
            _itemRepository = itemRepository;
        }

        public List<Personagem_Item> ObterTodos(long idPersonagem) => _repository.ObterTodos(idPersonagem);

        public bool Adicionar (long idPersonagem, long idCampanha, long idItem, int quant)
        {
            var p = _personagemRepository.ObterPorId(idPersonagem, idCampanha);
            var i = _itemRepository.ObterEntidadePorId(idItem);

            if (p == null || i == null) throw new Exception("O personagem ou o item não existem");

            var pi = new Personagem_Item
            {
                Id_Item = idItem,
                Id_Personagem = idPersonagem,
                Quantidade = quant <= 0 ? 0 : quant
            };

            return _repository.Adicionar(pi); 
        }

        public bool Editar (long idPersonagem, long idItem, int quant)
        {
            var pi = _repository.ObterPorId(idPersonagem, idItem);
            if (pi == null) throw new Exception("O personagem não possui esse item");

            if (quant != pi.Quantidade) pi.Quantidade = quant;

            if (pi.Quantidade <= 0) Excluir(idPersonagem, idItem);

            return _repository.EditarQuantidade();
        }

        public bool Excluir (long idPersonagem, long idItem)
        {
            var pi = _repository.ObterPorId(idPersonagem, idItem);

            if (pi == null) throw new Exception("O personagem não possui esse item");

            return _repository.Excluir(pi);
        }

    }
}
