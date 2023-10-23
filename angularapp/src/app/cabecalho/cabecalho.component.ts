import { Component } from '@angular/core';

@Component({
  selector: 'app-cabecalho',
  templateUrl: './cabecalho.component.html',
  styleUrls: ['./cabecalho.component.css']
})

export class CabecalhoComponent {
  isUserLoggedIn: boolean = true;

  login() {
    // o app pode ser expandido para gerenciar isto. Por enquanto, nós vamos passar um userID 1. Segurança pode ser obtida
    // de várias formas, como por exemplo o sistema enviando um segredo para o user após o login com sucesso e usando apenas
    // comunicação encriptada.
  }

  logout() {

  }
}
