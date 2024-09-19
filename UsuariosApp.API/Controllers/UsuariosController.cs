using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models.Dtos;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //método construtor para injeção de dependência (inicialização dos atributos)
        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }




        /// <summary>
        /// Endpoint da API para criação de usuário
        /// </summary>
        /// <returns></returns>

        [HttpPost] // método HTTP
        [Route("Criar")] //ENDPOINT: /api/usuarios/criar

        [ProducesResponseType(typeof(CriarUsuarioResponseDto), 201)]
        public IActionResult Criar(CriarUsuarioRequestDto request)
        {
            try
            {
                //executando a criação do usuário na camada de domínio
                var response = _usuarioDomainService.Criar(request);

                return StatusCode(201, response);


            }
            catch (ApplicationException e)
            { 
            
                return StatusCode(400, new { mensagem = e.Message  });
            }
            catch (Exception e)
            {
                //HTTP 500
                return StatusCode(500, new {mensagem = $"Falha ao criar usuário: {e.Message}"});
            
            }
        }
    }
}
