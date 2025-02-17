import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WebSocketService {
  private socket!: WebSocket;
  private messageSubject = new Subject<string>();
  message$ = this.messageSubject.asObservable();

  constructor() {}

  connect() {
    this.socket = new WebSocket('ws://localhost:5056/ws');

    this.socket.onmessage = (event) => {
      console.log('Mensagem recebida:', event.data);
      this.messageSubject.next(event.data);
    };

    this.socket.onopen = () => {
      console.log('Conectado ao WebSocket.');
    };

    this.socket.onclose = () => {
      console.log('Conexão WebSocket fechada.');

      setTimeout(() => this.connect(), 5000);
    };

    this.socket.onerror = (error) => {
      console.error('Erro na conexão WebSocket:', error);
    };
  }

  sendMessage(message: string) {
    if (this.socket.readyState === WebSocket.OPEN) {
      this.socket.send(message);
    } else {
      console.error('WebSocket não está aberto.');
    }
  }

  disconnect() {
    if (this.socket) {
      this.socket.close();
    }
  }
}
