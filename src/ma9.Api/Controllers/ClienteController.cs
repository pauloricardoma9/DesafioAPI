﻿using AutoMapper;
using ma9.Business.Interfaces;
using ma9.Business.Models;
using ma9.Business.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ma9.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository clienteRepository, IMapper mapper, IClienteService clienteService)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ClienteViewModel clienteViewModel)
        {
            //ValidarViewModel

            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            var Adicionado = await _clienteService.Adicionar(cliente);

            if (Adicionado) return CreatedAtAction("Adicionar", null);
            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ClienteViewModel clienteAtualizadoViewModel)
        {
            //Validar ViewModel

            var clienteAtualizado = _mapper.Map<Cliente>(clienteAtualizadoViewModel);
            var Atualizado = await _clienteService.Atualizar(id, clienteAtualizado);

            if (Atualizado) return CreatedAtAction("Atualizar", null);
            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var removido = await _clienteService.RemoverPorId(id);

            if (removido) return NoContent();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodosOsClientes()
        {
            var clientes = await _clienteRepository.ObterTodos();
            var clientesViewModel = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);

            return clientesViewModel;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterClienteComContatoPorId(Guid id)
        {
            var cliente = await _clienteRepository.ObterClienteComContato(id);
            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

            if(clienteViewModel == null) return NotFound();
            return clienteViewModel;
        }

    }
}
