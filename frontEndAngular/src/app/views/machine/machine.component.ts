import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';

import { HeaderService } from '../../services/header.service';
import { MachineReadComponent } from '../../components/machine/machine-read/machine-read.component';
import { WebSocketService } from '../../services/websocket.service';

@Component({
  selector: 'app-machine',
  standalone: true,
  imports: [MatButtonModule, RouterModule, MachineReadComponent],
  templateUrl: './machine.component.html',
  styleUrl: './machine.component.css',
})
export class MachineComponent implements OnInit {
  statusMessage: string = '';

  constructor(private router: Router, private headerService: HeaderService, private webSocketService: WebSocketService) {
    headerService.headerData = {
      title: 'Cadastro de Maquinas',
      icon: 'storefront',
      routeUrl: '/machines'
    }
  }

  ngOnInit(): void {
    this.webSocketService.connect();
    this.listenForStatusUpdates();
  }

  listenForStatusUpdates() {
    this.webSocketService.message$.subscribe((message) => {
      this.statusMessage = message; 
    });
  }

  navigateToMachineCreate(): void {
    this.router.navigate(['/machines/create']);
  }
}
