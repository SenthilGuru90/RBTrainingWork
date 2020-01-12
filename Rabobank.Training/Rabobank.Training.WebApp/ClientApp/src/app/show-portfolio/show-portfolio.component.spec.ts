import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPortfolioComponent } from './show-portfolio.component';

describe('ShowPortfolioComponent', () => {
  let component: ShowPortfolioComponent;
  let fixture: ComponentFixture<ShowPortfolioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowPortfolioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowPortfolioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
