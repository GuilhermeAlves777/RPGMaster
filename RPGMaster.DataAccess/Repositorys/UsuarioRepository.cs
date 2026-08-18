using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Repositorys
{
    public class UsuarioRepository
    {
        private readonly RPGMasterContext _context;

        public UsuarioRepository(RPGMasterContext context)
        {
            _context = context;
        }

        public bool Cadastrar(Usuario c)
        {
            //inserir a base de dados
            _context.Usuarios.Add(c);
            return _context.SaveChanges() > 0;
        }

        public List<Usuario> ObterTodos()
        {
            return _context.Usuarios.ToList();
        }

        public List<UsuarioDto> ObterTodosComCampanha()
        {
            return _context.Usuarios
                 .Select(u => new UsuarioDto
                 {
                     ID = u.Id_Usuario,
                     Nome = u.Nome,
                     Sobrenome = u.Sobrenome,
                     CPF = u.CPF,
                     Email = u.email,
                     User = u.user,
                     SenhaHash = u.senha_hash,
                     Campanhas = u.CampanhasCriadas.Select(c => new CampanhaUsuarioDto
                     {
                         Id = Convert.ToInt32(c.ID_Campanha),
                         Nome = c.Nome
                     }).ToList()
                 }).ToList();
        }

        public Usuario? ObterPorUser(string user)
        {
            var usuario = _context.Usuarios.Where(x => x.user == user).FirstOrDefault();
            return usuario;
        }

        public bool Excluir(long id)
        {
            var usuario = _context.Usuarios.Where(x => x.Id_Usuario == id).FirstOrDefault();

            _context.Usuarios.Remove(usuario);
            return _context.SaveChanges() > 0;
        }
        public bool Atualizar()
        {
            return _context.SaveChanges() > 0;
        }

        public UsuarioDto? ObterPorId(long id)
        {
            return _context.Usuarios
                .Where(x => x.Id_Usuario == id)
                .Select(u => new UsuarioDto
                {
                    ID = u.Id_Usuario,
                    Nome = u.Nome,
                    Sobrenome = u.Sobrenome,
                    CPF = u.CPF,
                    Email = u.email,
                    User = u.user,
                    SenhaHash = u.senha_hash,
                    Campanhas = u.CampanhasCriadas.Select(c => new CampanhaUsuarioDto
                    {
                        Id = Convert.ToInt32(c.ID_Campanha),
                        Nome = c.Nome
                    }).ToList()
                })
                .FirstOrDefault();
        }
    }
}
