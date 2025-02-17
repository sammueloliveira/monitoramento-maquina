import { Component, OnInit, ViewChild } from '@angular/core';
import { Machine } from '../../../interfaces/machine.module';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { CustomPaginatorIntl } from '../../../services/custom-paginator-intl.service';
import { Router, RouterModule } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../confirm-dialog/confirm-dialog.component';
import { MachineService } from '../../../services/machine.service';
import { MatIconButton } from '@angular/material/button';
import { WebSocketService } from '../../../services/websocket.service';

@Component({
  selector: 'app-machine-read',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    RouterModule,
    MatDialogModule,
    MatIconButton
  ],
  providers: [{ provide: MatPaginatorIntl, useClass: CustomPaginatorIntl }],
  templateUrl: './machine-read.component.html',
  styleUrls: ['./machine-read.component.css'],
})
export class MachineReadComponent implements OnInit {
  statusMessage: string = '';
  
  machines!: Machine[];
  displayedColumns: string[] = [
  'name',
  'location',
  'status',
  'action',
  ];

  dataSource = new MatTableDataSource<Machine>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private machineService: MachineService,
    private router: Router,
    public dialog: MatDialog,
    private webSocketService: WebSocketService
  ) {}

  ngOnInit(): void {
   

    this.machineService.read().subscribe((machines) => {
      this.machines = machines;
      this.dataSource.data = machines;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  viewMachine(id: string): void {
    this.machineService.readById(id).subscribe((Machine) => {
      this.router.navigate(['/machines', 'detail', Machine.id]);
    });
  }
  
  openMap(latitude: number, longitude: number): void {
    const url = `https://www.google.com/maps?q=${latitude},${longitude}`;
    window.open(url, '_blank'); 
  }
  
  deleteMachine(id: string): void {
    if (id) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this.machineService.delete(id).subscribe(() => {
            this.machines = this.machines.filter(
              (machine) => machine.id !== id
            );
            this.dataSource.data = this.machines;
            this.machineService.showMessage(`Maquina deletada com sucesso!`);
          });
        }
      });
    } else {
      this.machineService.showMessage('Erro ao deletar produto!');
    }
  }
  
}
