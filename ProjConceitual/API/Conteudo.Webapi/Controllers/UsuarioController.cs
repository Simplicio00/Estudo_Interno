using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conteudo.Webapi.Interfaces;
using Conteudo.Webapi.Models;
using Conteudo.Webapi.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conteudo.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuario banco { get; set; }
        public UsuarioController()
        {
            banco = new UsuarioRepositorio();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = banco.Get();
            if (lista.Count != 0)
            {
                return Ok(lista);
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            IActionResult badrequest = BadRequest("Este email já está cadastrado");
            var lista = banco.Get();

            if (!lista.Contains(lista.FirstOrDefault(a => a.Email == usuario.Email)))
            {
                try
                {
                    banco.Post(usuario);
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return Forbid(ex.Message.ToString());
                }
            }
            return badrequest;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            IActionResult result = NotFound("O usuário não existe");
            var usr = banco.GetById(id);

            if (usr != null)
            {
                return Ok(usr);
            }
            return result;
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario)
        {
            IActionResult result = NotFound("O usuário não existe");
            var usr = banco.GetById(id);
            
            if (usr != null)
            {
                try
                {
                    banco.Update(usr.IdUsuario, usuario);
                    return StatusCode(200,"Atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
                }
            }
            return result;
        }

    }
}