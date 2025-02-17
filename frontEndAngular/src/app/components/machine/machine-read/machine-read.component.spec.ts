import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MachineReadComponent } from './machine-read.component';



describe('ProductReadComponent', () => {
  let component: MachineReadComponent;
  let fixture: ComponentFixture<MachineReadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MachineReadComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MachineReadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
