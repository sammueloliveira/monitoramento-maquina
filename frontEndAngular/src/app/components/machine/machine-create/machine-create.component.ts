import { Component, OnInit } from '@angular/core';
import { MachineService } from '../../../services/machine.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { Machine } from '../../../interfaces/machine.module';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { EnumStatus } from '../../../enums/enum-status';
import { LocationService } from '../../../services/location.service';


@Component({
  selector: 'app-machine-create',
  standalone: true, 
  imports: [
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MatButtonModule,     
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    CommonModule
  ],
  templateUrl: './machine-create.component.html',
  styleUrls: ['./machine-create.component.css'],
})
export class MachineCreateComponent implements OnInit {
  machine: Machine = {
    id: '',
    name: '',
    location: '',
    latitude: 0,
    longitude: 0,
    status: EnumStatus.Operando, 
  };

  
  constructor(
    private machineService: MachineService,
    private locationService: LocationService,
    private router: Router
  ) {}
   
  EnumStatus = EnumStatus;

  ngOnInit(): void {}

  searchLocation(): void {
    if (this.machine.location) {
      this.locationService.getCoordinates(this.machine.location).subscribe({
        next: (response) => {
          if (response.length > 0) {
            this.machine.latitude = parseFloat(response[0].lat);
            this.machine.longitude = parseFloat(response[0].lon);
          } else {
            alert('Localização não encontrada. Tente um nome mais específico.');
          }
        },
        error: () => {
          alert('Erro ao buscar localização. Verifique sua conexão.');
        },
      });
    }
  }

  createMachine(form: NgForm): void {
  if (form.valid) {
    this.machineService.create(this.machine).subscribe({
      next: () => {
        this.machineService.showMessage('Máquina cadastrada com sucesso!');
        this.router.navigate(['/machines']);
      },
      error: (error) => {
        console.error('Erro ao cadastrar máquina', error);
        this.machineService.showMessage('Erro ao cadastrar máquina!');
      },
    });
  } else {
    form.form.markAllAsTouched();
    this.machineService.showMessage('Preencha todos os campos obrigatórios.');
  }
}


  cancel(): void {
    this.router.navigate(['/machines']);
  }
}
