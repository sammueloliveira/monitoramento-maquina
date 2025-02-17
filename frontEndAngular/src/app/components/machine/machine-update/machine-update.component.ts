import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { Machine } from '../../../interfaces/machine.module';
import { EnumStatus } from '../../../enums/enum-status';
import { MachineService } from '../../../services/machine.service';
import { WebSocketService } from '../../../services/websocket.service';

@Component({
  selector: 'app-machine-update',
  standalone: true,
  imports: [
    RouterModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatTableModule,
    CommonModule,
  ],
  templateUrl: './machine-update.component.html',
  styleUrls: ['./machine-update.component.css'],
})
export class MachineUpdateComponent implements OnInit {
  statusMessage: string = '';

  machine: Machine = {
    id: '',
    name: '',
    location: '',
    latitude: 0,
    longitude: 0,
    status: EnumStatus.Operando,
  };

  enumStatus = EnumStatus;
  constructor(
    private route: ActivatedRoute,
    private machineService: MachineService,
    private webSocketService: WebSocketService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.webSocketService.connect();

    this.webSocketService.message$.subscribe((message: string) => {
      this.statusMessage = message;
      this.machineService.showMessage(`Nova atualização: ${message}`);
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.machineService.readById(id).subscribe((machine) => {
        this.machine = machine;
      });
    }
  }

  editMachine(): void {
    this.machineService.update(this.machine).subscribe(() => {
      this.router.navigate(['/machines']);
      this.machineService.showMessage('Maquina atualizada com sucesso!');
    });
  }

  cancel(): void {
    this.router.navigate(['/machines']);
  }
}
