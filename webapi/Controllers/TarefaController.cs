namespace webapi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Dados;

    [ApiController]
    [Route("api/tarefas")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaRepository _tarefaRepository;
        private readonly ILogger<TarefaController> _logger;

        public TarefaController(TarefaRepository tarefaRepository, ILogger<TarefaController> logger)
        {
            _tarefaRepository = tarefaRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CriarTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest();
            }

            _tarefaRepository.Adicionar(tarefa);
            _logger.Log(LogLevel.Information, "Tarefa Criada", tarefa);
            return CreatedAtAction(nameof(ListarTarefas), new { id = tarefa.Id }, tarefa);
        }

        [HttpGet]
        public IActionResult ListarTarefas()
        {
            var tarefas = _tarefaRepository.ObterTodas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterTarefaPorId(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTarefa(int id, [FromBody] Tarefa tarefa)
        {
            if (tarefa == null || tarefa.Id != id)
            {
                return BadRequest();
            }

            var tarefaExistente = _tarefaRepository.ObterPorId(id);

            if (tarefaExistente == null)
            {
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Tarefa Atualizada", tarefa);
            _tarefaRepository.Atualizar(tarefa);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirTarefa(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);

            if (tarefa == null)
            {
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Tarefa Excluida", tarefa);
            _tarefaRepository.Excluir(id);
            return Ok();
        }
    }
}
