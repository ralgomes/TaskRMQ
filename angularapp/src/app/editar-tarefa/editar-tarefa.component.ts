import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { TarefaService } from '../services/tarefa.service';
import { Tarefa } from '../modelo/Tarefa';

@Component({
  selector: 'app-editar-tarefa',
  templateUrl: './editar-tarefa.component.html',
  styleUrls: ['./editar-tarefa.component.css']
})

export class EditarTarefaComponent implements OnInit {
  tarefa:Tarefa = {
    id: 0,
    uuid: '',
    data: '',
    descricao: '',
    status: 0
  };
  isEdicao = false;
  tarefaId: any = -1;
  datePipe: DatePipe = new DatePipe('en-US');

  constructor(
    private tarefaService: TarefaService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.tarefaId = params.get('id');
      if (this.tarefaId) {
        this.isEdicao = true;
        this.tarefaService.getTarefaPorId(this.tarefaId).subscribe((tarefa) => {
          this.tarefa = tarefa;
          this.tarefa.data = this.transformaData(tarefa.data) ?? ""
        }, error => { console.error(error) });
      }
    });
  }

  transformaData(s: string) {
    return this.datePipe.transform(s, 'yyyy-MM-dd');
  }

  cancelar() {
    this.router.navigate(['/tarefas']);
  }

  salvarTarefa() {
    this.tarefa.status = +this.tarefa.status; // garante que seja um número no json
    if (this.isEdicao) {
      this.tarefaService.atualizarTarefa(this.tarefaId, this.tarefa).subscribe(() => {
        this.router.navigate(['/tarefas']);
      }, error => { console.error(error) });
    } else {
      this.tarefa.id = 0;
      this.tarefaService.criarTarefa(this.tarefa).subscribe(() => {
        this.router.navigate(['/tarefas']);
      }, error => { console.error(error) });
    }
  }

  excluirTarefa() {
    this.tarefaService.excluirTarefa(this.tarefaId).subscribe(() => {
      this.router.navigate(['/tarefas']);
    }, error => { console.error(error) });
  }
}
