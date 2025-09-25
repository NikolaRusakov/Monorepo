import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ContactService } from './contact-service';

describe('ContactService', () => {
  let component: ContactService;
  let fixture: ComponentFixture<ContactService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactService],
    }).compileComponents();

    fixture = TestBed.createComponent(ContactService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
