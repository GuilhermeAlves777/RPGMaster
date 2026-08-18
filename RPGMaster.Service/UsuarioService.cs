using Microsoft.AspNetCore.Identity;
using RPGMaster.DataAccess.Repositorys;
using RPGMaster.Model;
using RPGMaster.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public List<UsuarioDto> ObterTodos()
        {
            var listaUsuario = _usuarioRepository.ObterTodosComCampanha();
            return listaUsuario;
        }

        public UsuarioDto? ObterPorId(long id)
        {
            var result = _usuarioRepository.ObterPorId(id);
            return result;
        }
        public Usuario? ObterPorUser(string user)
        {
            var result = _usuarioRepository.ObterPorUser(user);
            return result;
        }

        public bool CadastrarUsuario(string nome, string sobrenome, string user, string senha, string email, string cpf, DateTime Dn)
        {
            var hasher = new PasswordHasher<Usuario>();

            var usu = new Usuario()
            {
                Nome = nome,
                Sobrenome = sobrenome,
                user = user,
                CPF = cpf,
                email = email,
                DataNascimento = Dn,
            };

            usu.senha_hash = hasher.HashPassword(usu, senha);

            var listaUsuario = _usuarioRepository.ObterTodos();
            var existe = listaUsuario.Any(u => u.CPF == usu.CPF || u.user == usu.user);

            if (existe)
                throw new Exception("Esse CPF já está registrado dentro do sistema");

            var result = _usuarioRepository.Cadastrar(usu);
            return result;

        }

        public bool LoginUsuario(string user, string senha)
        {
            var usuarios = _usuarioRepository.ObterTodos();

            var usuario = usuarios.FirstOrDefault(u => u.user == user);

            if (usuario == null)
                return false;

            var hasher = new PasswordHasher<Usuario>();

            var resultado = hasher.VerifyHashedPassword(
                usuario,
                usuario.senha_hash,
                senha
            );

            if (resultado == PasswordVerificationResult.Success)
                return true;

            return false;
        }

        public bool AtualizarUsuario(long id, string nome, string sobrenome, string user, string email, int empresaId, string cpf, DateTime Dn)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                throw new Exception("Usuário não existe");

            usuario.Nome = nome;
            usuario.Sobrenome = sobrenome;
            usuario.User = user;
            usuario.Email = email;
            usuario.CPF = cpf;
            usuario.DataNascimento = Dn;

            var listaUsuario = _usuarioRepository.ObterTodos();
            var existe = listaUsuario.Any(u => u.CPF == usuario.CPF || u.user == usuario.User);

            if (existe)
                throw new Exception("Existem dados duplicados");

            var result = _usuarioRepository.Atualizar();
            return result;
        }

        public bool ExcluirUsuario(long id)
        {
            throw new NotImplementedException();
        }
    }
}
