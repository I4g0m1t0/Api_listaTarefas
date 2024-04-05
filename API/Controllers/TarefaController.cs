using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public TarefaController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //Criar
        [HttpPost]
        public async Task<ActionResult<Tarefa>> AddTarefa(Tarefa novaTarefa)
        {
            if (novaTarefa != null)
            {
                appDbContext.Tarefas.Add(novaTarefa);
                await appDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTarefa), new { id = novaTarefa.Id }, novaTarefa);
            }
            return BadRequest();
        }

        //Lê todas as tarefas
        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> GetTarefas()
        {
            var tarefas = await appDbContext.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        //Lê a terfa de acordo com o Id informado
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == id);
            if (tarefa != null)
            {
                return Ok(tarefa);
            }
            return NotFound("Tarefa não encontrada!");
        }

        //Edita a tarefa que foi previamente cadastrada
        [HttpPut]
        public async Task<ActionResult<Tarefa>> AtualizaTarefa(Tarefa atualizaTarefa)
        {
            if (atualizaTarefa != null)
            {
                var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == atualizaTarefa.Id);
                if (tarefa != null)
                {
                    tarefa.Nome = atualizaTarefa.Nome;
                    tarefa.Descricao = atualizaTarefa.Descricao;
                    tarefa.Concluida = atualizaTarefa.Concluida;
                    tarefa.DataConclusao = atualizaTarefa.DataConclusao;
                    await appDbContext.SaveChangesAsync();

                    var tarefas = await appDbContext.Tarefas.ToListAsync();
                    return Ok(tarefas);
                }
                return NotFound();
            }
            return BadRequest();
        }

        //Deletar tarefas
        [HttpDelete]
        public async Task<ActionResult<List<Tarefa>>> DeleteEmployee(int id)
        {
            var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == id);
            if (tarefa != null)
            {
                appDbContext.Tarefas.Remove(tarefa);
                await appDbContext.SaveChangesAsync();

                var tarefas = await appDbContext.Tarefas.ToListAsync();
                return Ok(tarefas);
            }
            return BadRequest();
        }
    }
}
