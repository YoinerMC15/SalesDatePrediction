import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../app.config';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl: string = environment.apiBaseUrl; 
 
  constructor(private http: HttpClient) {}

  
  getSalesDatePredictions(): Observable<any> {
    return this.http.get(`${this.baseUrl}/orders/predictions`);
  }

  
  getOrdersByClient(clientId: number): Observable<any> {
    if (!clientId) {
      throw new Error('El clientId es obligatorio para esta petición.');
    }
    return this.http.get(`${this.baseUrl}/orders/client/${clientId}`);
  }

  createOrder(orderData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/orders`, orderData);
  }

  
  getAllEmployees(): Observable<any> {
    return this.http.get(`${this.baseUrl}/employees`);
  }


  getAllProducts(): Observable<any> {
    return this.http.get(`${this.baseUrl}/products`);
  }

  

  getAllShippers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/shippers`);
  }
}
