import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TarefaService } from '../services/tarefa.service'; // Importe o serviço de tarefas
import { SortingService } from '../services/sorting.service';
import { Tarefa } from '../modelo/Tarefa';

@Component({
  selector: 'app-lista-tarefas',
  templateUrl: './lista-tarefas.component.html',
  styleUrls: ['./lista-tarefas.component.css']
})
export class ListaTarefasComponent implements OnInit {

  tarefas: Tarefa[];
  cargaInicial: boolean = false;
  datePipe: DatePipe = new DatePipe('en-US');
  Statuses: string[] = ['Não Iniciada', 'Em Andamento', 'Concluída', 'Cancelada'];
  sortColumn: string = ''; // Track the current sorting column
  sortDirection: string = 'asc'; // Track sorting direction (asc or desc)

  constructor(private router: Router, private tarefaService: TarefaService, private sortingService: SortingService) {
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
    this.sortColumn = this.sortingService.sortColumn;
    this.sortDirection = this.sortingService.sortDirection;
    if (this.sortColumn) {
      this.sortBy(this.sortColumn); // Apply initial sorting
    }
  }

  sortBy(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc'; // Toggle direction if same column
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc'; // Default to ascending for new column
    }

    this.tarefas.sort((a:any, b:any) => {
      const valueA = this.getComparableValue(a[column]);
      const valueB = this.getComparableValue(b[column]);

      if (valueA < valueB) {
        return -1 * (this.sortDirection === 'desc' ? -1 : 1);
      } else if (valueA > valueB) {
        return 1 * (this.sortDirection === 'desc' ? -1 : 1);
      } else {
        return 0;
      }
    });

    this.sortingService.sortColumn = this.sortColumn;
    this.sortingService.sortDirection = this.sortDirection;
  }

  private getComparableValue(value: any): string | number {
    if (value instanceof Date) {
      return value.getTime(); // Convert dates to numeric timestamps for comparison
    } else if (typeof value === 'string') {
      return value.toLowerCase(); // Case-insensitive string comparison
    } else {
      return value; // No conversion needed for numbers or other comparable types
    }
  }

}
