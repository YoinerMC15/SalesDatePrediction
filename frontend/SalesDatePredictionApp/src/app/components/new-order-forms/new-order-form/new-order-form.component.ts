import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-new-order-form',
  templateUrl: './new-order-form.component.html',
  styleUrls: ['./new-order-form.component.css'],
})
export class NewOrderFormComponent implements OnInit {
  orderForm!: FormGroup;
  employees: any[] = [];
  shippers: any[] = [];
  products: any[] = [];
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    public dialogRef: MatDialogRef<NewOrderFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { clientId: number; clientName: string }
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadFormData();
  }

  initializeForm(): void {
    this.orderForm = this.fb.group({
      employeeId: [null, Validators.required],
      shipperId: [null, Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      shipCountry: ['', Validators.required],
      orderDate: [null, Validators.required],
      requiredDate: [null, Validators.required],
      shippedDate: [null],
      freight: [null, Validators.required],
      productId: [null, Validators.required],
      unitPrice: [null, Validators.required],
      quantity: [null, Validators.required],
      discount: [null, Validators.required],
    });
  }

  loadFormData(): void {
    this.isLoading = true;
    Promise.all([
      this.apiService.getAllEmployees().toPromise(),
      this.apiService.getAllShippers().toPromise(),
      this.apiService.getAllProducts().toPromise(),
    ])
      .then(([employees, shippers, products]) => {
        this.employees = employees;
        this.shippers = shippers;
        this.products = products;
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  submitForm(): void {
    if (this.orderForm.invalid) {
      return;
    }

    const orderData = { ...this.orderForm.value, clientId: this.data.clientId };

    this.apiService.createOrder(orderData).subscribe({
      next: () => this.dialogRef.close(true),
      error: (err) => console.error('Error:', err),
    });
  }

  closeModal(): void {
    this.dialogRef.close(false);
  }
}
