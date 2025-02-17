import { Routes } from '@angular/router';
import { HomeComponent } from './views/home/home.component';
import { MachineComponent } from './views/machine/machine.component';
import { MachineDetailComponent } from './components/machine/machine-details/machine-detail.component';
import { MachineCreateComponent } from './components/machine/machine-create/machine-create.component';
import { MachineUpdateComponent } from './components/machine/machine-update/machine-update.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'machines', 
    component: MachineComponent
  },
  { path: 'machines/detail/:id', component: MachineDetailComponent },
  {
    path: 'machines/create', 
    component: MachineCreateComponent
  },
  { path: 'machines/update/:id', component: MachineUpdateComponent },
];