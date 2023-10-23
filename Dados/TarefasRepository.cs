namespace Dados
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class TarefaRepository
    {
        private readonly TarefaContext _context;

        public TarefaRepository(TarefaContext context)
        {
            _context = context;
        }

        public Tarefa ObterPorId(int id)
        {
            return _context.Tarefas.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tarefa> ObterTodas()
        {
            return _context.Tarefas.ToList();
        }

        public void Adicionar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa)
        {
            var existingEntity = _context.Tarefas.Local.FirstOrDefault(e => e.Id == tarefa.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Entry(tarefa).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Excluir(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();
            }
        }
    }
}
