using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class ArmaDetalheService
    {
        private readonly ArmaDetalheRepository _armaDetalheRepository;
        private readonly ItemRepository _itemRepository;

        public ArmaDetalheService(ArmaDetalheRepository armaDetalheRepository, ItemRepository itemRepository)
        {
            _armaDetalheRepository = armaDetalheRepository;
            _itemRepository = itemRepository;
        }

        public Arma_Detalhe? ObterPorItem(long idItem) =>
            _armaDetalheRepository.ObterPorItem(idItem);

        public bool Cadastrar(long idItem, long idCampanha, string dano, TipoDanoEnum tipoDano, int alcance)
        {
            var item = _itemRepository.ObterEntidadePorId(idItem, idCampanha)
                ?? throw new Exception("Esse item não existe nessa campanha");

            if (_armaDetalheRepository.JaPossui(idItem))
                throw new Exception("Esse item já possui detalhes de arma cadastrados");

            var arma = new Arma_Detalhe
            {
                Id_Item = idItem,
                Dano = dano,
                Tipo_Dano = tipoDano,
                Alcance = alcance
            };

            var ret = _armaDetalheRepository.Cadastrar(arma);
            if (!ret) throw new Exception("Não foi possível cadastrar os detalhes da arma");

            return ret;
        }

        public bool Atualizar(long idItem, string? dano, TipoDanoEnum tipoDano, int? alcance)
        {
            var arma = _armaDetalheRepository.ObterPorItem(idItem)
                ?? throw new Exception("Esse item não possui detalhes de arma");

            if (dano != null) arma.Dano = dano;
            if (tipoDano != null) arma.Tipo_Dano = tipoDano;
            if (alcance.HasValue) arma.Alcance = alcance.Value;

            return _armaDetalheRepository.Atualizar();
        }

        public bool Deletar(long idItem)
        {
            var arma = _armaDetalheRepository.ObterPorItem(idItem)
                ?? throw new Exception("Esse item não possui detalhes de arma");

            return _armaDetalheRepository.Deletar(arma);
        }
    }
}
