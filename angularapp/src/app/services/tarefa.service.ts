import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tarefa } from '../modelo/Tarefa';

@Injectable({
  providedIn: 'root'
})

export class TarefaService {
  private baseUrl: string = 'http://localhost:5196/api';

  constructor(private http: HttpClient) { }

  // Aqui, com o RabbitMQ configurado, eu altero os métodos criarTarefa, atualizarTarefa e excluirTarefa
  // para, em vez de irem ao banco, escreverem mensagens numa fila do RabbitMQ.

  getTarefas(): Observable<any> {
    return this.http.get<Tarefa[]>(`${this.baseUrl}/tarefas`);
  }

  getTarefaPorId(id: number): Observable<any> {
    return this.http.get<Tarefa>(`${this.baseUrl}/tarefas/${id}`);
  }

  criarTarefa(tarefa: Tarefa): Observable<any> {
    tarefa.status = 0;
    return this.http.post<Tarefa>(`${this.baseUrl}/tarefas`, tarefa, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    });
  }

  atualizarTarefa(id: number, tarefa: Tarefa): Observable<any> {
    return this.http.put<Tarefa>(`${this.baseUrl}/tarefas/${id}`, tarefa, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    });
  }

  excluirTarefa(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/tarefas/${id}`);
  }
}
