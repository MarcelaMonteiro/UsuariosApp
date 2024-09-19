using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Domain.Models.Entities;
using UsuariosApp.Domain.Models.Messages;

namespace UsuariosApp.Domain.Services
{
    /// <summary>
    /// Implementação dos serviços de domínio de usuário
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributo para acessar os métodos da interface IUsuarioRepository
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMessageProducer _messageProducer;

        //construtor que irá inicializar / instanciar a interface em tempo de execução
        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IMessageProducer messageProducer)
        {
            _usuarioRepository = usuarioRepository;
            _messageProducer = messageProducer;
        }


        public CriarUsuarioResponseDto Criar(CriarUsuarioRequestDto request)
        {
            //verficar se o email do usuário já está cadastrado no banco de dados
            if (_usuarioRepository.IsExists(request.Email))
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            //capturando os dados do usuário
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = CryptoHelper.CreateSHA256(request.Senha)
            };

            //gravar o usuário no banco de dados
            _usuarioRepository.Create(usuario);

            //escrever uma mensagem de boas vindas para o usuário
            var message = new MessageModel
            {
                EmailDestinatario = usuario.Email,
                Assunto = "Confirmação de cadastro de usuário - COTI Informática",
                Texto = $"Olá, {usuario.Nome} \n\n Sua conta de usuário foi criada com sucesso! \nAtt\nEquipe COTI - Informática. "

            };

            //enviar a mensagem para a fila
            _messageProducer.Send(JsonConvert.SerializeObject(message));

            //retornar a resposta
            var response = new CriarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.Now
            };

            return response;
        }
    }
}



