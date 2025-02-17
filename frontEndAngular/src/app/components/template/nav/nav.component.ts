import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { HomeComponent } from "../../../views/home/home.component";

import {  RouterModule, RouterOutlet } from '@angular/router';
import { MachineComponent } from '../../../views/machine/machine.component';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [MatSidenavModule, MatListModule, HomeComponent, MachineComponent, RouterOutlet, RouterModule],
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

}
