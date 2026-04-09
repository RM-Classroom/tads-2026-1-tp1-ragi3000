using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Locadora.Data;
using Locadora.Models;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public ClientesController(LocadoraContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
        //Get: api/Clientes/ListaVeiculos
        [HttpGet("{clienteId}/carros-alugados")]
        public async Task<ActionResult> GetCarrosDoCliente(int clienteId)
        {
            // Realiza o Join entre Aluguel, Cliente e Veiculo
            // E um ThenInclude para buscar a Marca (Fabricante) do Veiculo
            var resultado = await _context.Alugueis
                .Include(a => a.Cliente) // Join 1: Aluguel + Cliente
                .Include(a => a.Veiculo) // Join 2: Aluguel + Veiculo
                    .ThenInclude(v => v.Fabricante) // Extra: Traz a Marca para o relatório
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new
                {
                    a.Veiculo.Modelo,
                    Marca = a.Veiculo.Fabricante.Nome, // Informação extra útil
                    Ano = a.Veiculo.AnoFabricacao,     // Substituindo a Placa
                    DataDaLocacao = a.DataInicio
                })
                .ToListAsync();

            if (resultado == null || !resultado.Any())
            {
                return NotFound("Nenhum aluguel encontrado para este cliente.");
            }

            return Ok(resultado);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
