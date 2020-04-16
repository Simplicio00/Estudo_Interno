using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conteudo.Webapi.DataContent;
using Conteudo.Webapi.Interfaces;
using Conteudo.Webapi.Models;
using Conteudo.Webapi.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conteudo.Webapi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuario banco { get; set; }
        private DefaultValues postData;
        public UsuarioController()
        {
            banco = new UsuarioRepositorio();
            postData = new DefaultValues();
        }
        public IActionResult InvalidUser() { return NotFound("O usuário não existe"); }
        public IActionResult InvalidMail() { return NotFound("O Email já está cadastrado"); }
        public IActionResult Error(string erro) { return BadRequest($"Ocorreu um erro: {erro}"); }


        [HttpGet]
        public IActionResult Get()
        {
            var lista = banco.Get();
            int limite = lista.Count;

            if (lista.Exists(m => m.StatusU == false))
                lista.RemoveAll(m => m.StatusU == false);

            if (lista.Count != 0)
            return Ok(lista);

            postData.PostDefaultUser(limite);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            var lista = banco.Get();

            if (!lista.Contains(
                lista.FirstOrDefault(a => a.Email == usuario.Email)))
                try
                {
                    banco.Post(usuario);
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return Error(ex.Message);
                }
            
            return InvalidMail();
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usr = banco.GetById(id);

            if (usr != null && usr.StatusU != false)
                return Ok(usr);
            
            return InvalidUser();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario)
        {
            var usr = banco.GetById(id);
            
            if (usr != null && usr.StatusU != false)

                try
                {
                    banco.Update(usr.IdUsuario, usuario);
                    return StatusCode(200,"Atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    return Error(ex.Message);
                }
            
            return InvalidUser();
        }

    }
}