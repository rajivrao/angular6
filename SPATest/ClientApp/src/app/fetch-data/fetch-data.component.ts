import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public products: Product[];

  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;

    _http.get<Product[]>(baseUrl + 'api/SampleData/Products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));    
  }  
}

interface Product {
  id: number;
  name: string;
  price: number;  
}
