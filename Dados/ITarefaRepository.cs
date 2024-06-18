using Dados;

public interface ITarefaRepository
{
    Tarefa Adicionar(Tarefa tarefa);
    IEnumerable<Tarefa> ObterTodas();
    Tarefa ObterPorId(int id);
    Tarefa ObterPorUUID(string uuid);
    void Atualizar(Tarefa tarefa);
    void Excluir(int id);
    // Add other repository methods as needed
}