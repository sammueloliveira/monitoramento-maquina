import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Machine } from '../../../interfaces/machine.module';
import { MachineService } from '../../../services/machine.service';
import { EnumStatus } from '../../../enums/enum-status';


@Component({
  selector: 'app-Machine-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, MatButtonModule, MatCardModule],
  templateUrl: './machine-detail.component.html',
  styleUrls: ['./machine-detail.component.css'],
})
export class MachineDetailComponent implements OnInit {
  machine!: Machine;
  enumStatus = EnumStatus; 

  constructor(
    private route: ActivatedRoute,
    private machineService: MachineService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.machineService.readById(id).subscribe((machine) => {
        this.machine = machine;
        this.machineService.showMessage('Informações carregadas!');
      });
    }
  }
   
  cancel(): void {
    
    window.history.back();
  }
}
