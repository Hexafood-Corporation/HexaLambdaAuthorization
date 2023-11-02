using HexaLambdaAuthorization.Repositories;
using HexaLambdaAuthorization.Services;
using Microsoft.AspNetCore.Mvc;

namespace HexaLambdaAuthorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly JWTAuthenticationManager _jwtAuthenticationManager;
        public ClienteController(IClienteRepository clienteRepository, JWTAuthenticationManager jwtAuthenticationManager)
        {
            _clienteRepository = clienteRepository;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        [Route("AuthenticationByCPF")]
        public async Task<IActionResult> AuthenticationByCPF(string cpf)
        {
            try
            {
                var cliente = await _clienteRepository.GetByCPFAsync(cpf);
                if (cliente != null)
                {
                    return Ok(await _jwtAuthenticationManager.GenerateToken(cliente));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("AnonymousAuthentication")]
        public async Task<IActionResult> AnonymousAuthentication()
        {
            try
            {
                return Ok(await _jwtAuthenticationManager.GenerateAnonymousTokenAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }
    }
}
