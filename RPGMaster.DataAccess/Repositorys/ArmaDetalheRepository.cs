using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ArmaDetalheRepository
    {
        private readonly RPGMasterContext _context;

        public ArmaDetalheRepository(RPGMasterContext context) => _context = context;

        public Arma_Detalhe? ObterPorItem(long idItem) =>
            _context.ArmasDetalhe.FirstOrDefault(a => a.Id_Item == idItem);

        public bool JaPossui(long idItem) =>
            _context.ArmasDetalhe.Any(a => a.Id_Item == idItem);

        public bool Cadastrar(Arma_Detalhe arma)
        {
            _context.ArmasDetalhe.Add(arma);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Arma_Detalhe arma)
        {
            _context.ArmasDetalhe.Remove(arma);
            return _context.SaveChanges() > 0;
        }
    }
}
