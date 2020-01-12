import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-show-portfolio',
  templateUrl: './show-portfolio.component.html',
  styleUrls: ['./show-portfolio.component.css']
})
export class ShowPortfolioComponent {
  public fomArray: PositionVM[];  

  constructor(http: HttpClient, @Inject('BASE_URL') baseurl: string) {
    http.get<PositionVM[]>(baseurl + 'api/portfolio').subscribe(result => {
      this.fomArray = result;
    }, error => {
        console.error(error);
    });
    //this.fomArray = new Array({ code: 'NL0000292332', name: 'Rabobank Core Aandelen Fonds T2', value: '45678' });
  }  
}


interface PositionVM {
  code: string;
  name: string;
  value: string;
  mandates: {
    name: string,
    allocation: string,
    value: string
  }
}
