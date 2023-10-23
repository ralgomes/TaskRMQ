import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TarefaService } from '../services/tarefa.service'; // Importe o serviço de tarefas
import { Tarefa } from '../modelo/Tarefa';

@Component({
  selector: 'app-lista-tarefas',
  templateUrl: './lista-tarefas.component.html',
  styleUrls: ['./lista-tarefas.component.css']
})
export class ListaTarefasComponent implements OnInit {

  tarefas: Tarefa[];
  cargaInicial: boolean;
  datePipe: DatePipe = new DatePipe('en-US');
  Statuses: string[] = ['Não Iniciada', 'Em Andamento', 'Concluída', 'Cancelada'];

  constructor(private router: Router, private tarefaService: TarefaService) {
    this.cargaInicial = false;
    this.tarefas = [];
  }

  transformaData(s: string) {
    return this.datePipe.transform(s, 'dd/MM/yyyy');
  }

  abrirEdicao(id: number) {
    if (id === 0 || id === -1) {
      // Se não temos uma ID, é uma inclusão...
      this.router.navigate(['/editar-tarefa']);
    } else {
      // Se temos, é edição
      this.router.navigate(['/editar-tarefa', id]);
    }
  }

  ngOnInit() {
    // Abrimos chamando as tarefas...
    this.tarefaService.getTarefas().subscribe(tarefas => {
      this.cargaInicial = true;
      this.tarefas = tarefas;
    }, error => console.error(error));
  }
}
