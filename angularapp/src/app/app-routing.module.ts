import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaTarefasComponent } from './lista-tarefas/lista-tarefas.component';
import { EditarTarefaComponent } from './editar-tarefa/editar-tarefa.component';

const routes: Routes = [
  { path: '', redirectTo: '/tarefas', pathMatch: 'full' }, // Rota padrão
  { path: 'tarefas', component: ListaTarefasComponent },
  { path: 'editar-tarefa', component: EditarTarefaComponent },
  { path: 'editar-tarefa/:id', component: EditarTarefaComponent },
  // Outras rotas podem ser adicionadas aqui, se necessário
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
