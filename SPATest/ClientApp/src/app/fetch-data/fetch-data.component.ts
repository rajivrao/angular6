import { Component, Inject } from '@angular/core';
import { HttpService } from '../services/http.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public products: Product[];

  myAppUrl: string = "";
  constructor(private _http: HttpService, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
    
    _http.get(baseUrl + 'api/SampleData/Products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));    
  }

  //initializing page number to one
  p: number = 1;
}

interface Product {
  id: number;
  name: string;
  price: number;  
}
