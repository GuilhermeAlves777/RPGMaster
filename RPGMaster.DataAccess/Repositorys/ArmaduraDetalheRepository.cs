using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class ArmaduraDetalheRepository
    {
        private readonly RPGMasterContext _context;

        public ArmaduraDetalheRepository(RPGMasterContext context) => _context = context;

        public Armadura_Detalhe? ObterPorItem(long idItem) =>
            _context.ArmadurasDetalhe.FirstOrDefault(a => a.Id_Item == idItem);

        public bool JaPossui(long idItem) =>
            _context.ArmadurasDetalhe.Any(a => a.Id_Item == idItem);

        public bool Cadastrar(Armadura_Detalhe armadura)
        {
            _context.ArmadurasDetalhe.Add(armadura);
            return _context.SaveChanges() > 0;
        }

        public bool Atualizar() => _context.SaveChanges() > 0;

        public bool Deletar(Armadura_Detalhe armadura)
        {
            _context.ArmadurasDetalhe.Remove(armadura);
            return _context.SaveChanges() > 0;
        }
    }

}
