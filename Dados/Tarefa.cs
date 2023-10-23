using System;
using System.ComponentModel;

namespace Dados
{

    public class Tarefa
    {
        public int Id { get; set; }
        public string? UUID { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
    }

    public enum StatusTarefa
    {
        [Description("Não Iniciada")]
        NaoIniciada = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Concluída")]
        Concluida = 2,
        [Description("Cancelada")]
        Cancelada = 3
    }
}
