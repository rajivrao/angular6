import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Component({
  selector: 'app-add-data',
  templateUrl: './add-data.component.html'
})
export class AddDataComponent implements OnInit {
  productForm: FormGroup;
  title: string = "Add Product";
  id: number;
  errorMessage: any;
  myAppUrl: string = "";  

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string,
     private _fb: FormBuilder, private _router: Router) {

    this.myAppUrl = baseUrl;
    
    this.productForm = this._fb.group({
      id: 0,
      name: ['', [Validators.required]],
      price: ['', [Validators.required]]
    })
  }

  ngOnInit() {
  }
  
  save() {
    if (!this.productForm.valid) {
      return;
    }
    
    return this._http.post(this.myAppUrl + 'api/SampleData', this.productForm.value).subscribe((data) => {
      this._router.navigate(['/fetch-data']);
    }, error => this.errorMessage = error)
  }
  
}
