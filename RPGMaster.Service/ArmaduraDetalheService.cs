using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class ArmaduraDetalheService
    {
        private readonly ArmaduraDetalheRepository _armaduraDetalheRepository;
        private readonly ItemRepository _itemRepository;

        public ArmaduraDetalheService(ArmaduraDetalheRepository armaduraDetalheRepository, ItemRepository itemRepository)
        {
            _armaduraDetalheRepository = armaduraDetalheRepository;
            _itemRepository = itemRepository;
        }

        public Armadura_Detalhe? ObterPorItem(long idItem) =>
            _armaduraDetalheRepository.ObterPorItem(idItem);

        public bool Cadastrar(long idItem, long idCampanha, int defesa)
        {
            var item = _itemRepository.ObterEntidadePorId(idItem, idCampanha)
                ?? throw new Exception("Esse item não existe nessa campanha");

            if (_armaduraDetalheRepository.JaPossui(idItem))
                throw new Exception("Esse item já possui detalhes de armadura cadastrados");

            var armadura = new Armadura_Detalhe { Id_Item = idItem, Defesa = defesa };

            var ret = _armaduraDetalheRepository.Cadastrar(armadura);
            if (!ret) throw new Exception("Não foi possível cadastrar os detalhes da armadura");

            return ret;
        }

        public bool Atualizar(long idItem, int defesa)
        {
            var armadura = _armaduraDetalheRepository.ObterPorItem(idItem)
                ?? throw new Exception("Esse item não possui detalhes de armadura");

            armadura.Defesa = defesa;

            return _armaduraDetalheRepository.Atualizar();
        }

        public bool Deletar(long idItem)
        {
            var armadura = _armaduraDetalheRepository.ObterPorItem(idItem)
                ?? throw new Exception("Esse item não possui detalhes de armadura");

            return _armaduraDetalheRepository.Deletar(armadura);
        }
    }
}
