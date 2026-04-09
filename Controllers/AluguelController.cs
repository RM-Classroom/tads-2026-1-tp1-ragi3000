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
    public class AluguelController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public AluguelController(LocadoraContext context)
        {
            _context = context;
        }

        // GET: api/Aluguel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis.ToListAsync();
        }

        // GET: api/Aluguel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);

            if (aluguel == null)
            {
                return NotFound();
            }

            return aluguel;
        }
        // GET: api/Aluguel/NomeCliente/ModeloVeiculo
        [HttpGet("relatorio-geral")]
        public async Task<ActionResult> GetRelatorioGeral()
        {
            return Ok(await _context.Alugueis
                .Include(a => a.Cliente) // Join 1
                .Include(a => a.Veiculo) // Join 2
                .Select(a => new
                {
                    a.Id,
                    Cliente = a.Cliente.Nome,
                    Carro = a.Veiculo.Modelo,
                    a.DataInicio
                }).ToListAsync());
        }

        // GET: api/Aluguel/por-categoria/{categoriaId}
        [HttpGet("por-categoria/{categoriaId}")]
        public async Task<ActionResult> GetAlugueisPorCategoria(int categoriaId)
        {
            return Ok(await _context.Alugueis
                .Include(a => a.Cliente)                   
                .Include(a => a.Veiculo)                   
                    .ThenInclude(v => v.Categoria)         
                .Include(a => a.Veiculo)                  
                    .ThenInclude(v => v.Fabricante)        
                .Where(a => a.Veiculo.CategoriaId == categoriaId)
                .ToListAsync());
        }
        //GET: api/Aluguel/por-fabricante/1
        [HttpGet("por-fabricante/{fabricanteId}")]
        public async Task<ActionResult> GetAlugueisPorFabricante(int fabricanteId)
        {
            return Ok(await _context.Alugueis
                .Include(a => a.Cliente) 
                .Include(a => a.Veiculo) 
                    .ThenInclude(v => v.Fabricante) 
                .Include(a => a.Veiculo)
                    .ThenInclude(v => v.Categoria) 
                .Where(a => a.Veiculo.FabricanteId == fabricanteId)
                .ToListAsync());
        }

        // PUT: api/Aluguel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(int id, Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluguel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AluguelExists(id))
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

        // POST: api/Aluguel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(Aluguel aluguel)
        {
            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluguel", new { id = aluguel.Id }, aluguel);
        }

        // DELETE: api/Aluguel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AluguelExists(int id)
        {
            return _context.Alugueis.Any(e => e.Id == id);
        }
    }
}
