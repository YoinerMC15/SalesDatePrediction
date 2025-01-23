import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../services/api.service';
import { provideHttpClient } from '@angular/common/http';
import { OrdersModalComponent } from '../../orders-modal/orders-modal/orders-modal.component';
import { NewOrderFormComponent } from '../../new-order-forms/new-order-form/new-order-form.component';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions']; 
  dataSource = new MatTableDataSource<any>([]); 
  isLoading = true; 

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort; 

  constructor(private apiService: ApiService, private dialog: MatDialog) { } 

  ngOnInit(): void {
    this.loadCustomers(); 
  }


  loadCustomers(): void {
    this.isLoading = true;
    this.apiService.getSalesDatePredictions().subscribe({
      next: (data) => {
        this.dataSource.data = data; 
        this.dataSource.paginator = this.paginator; 
        this.dataSource.sort = this.sort; 
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar las predicciones:', err);
        this.isLoading = false;
      }
    });
  }

   applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase(); 

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage(); 
    }
  }

 

  viewOrders(custId: number): void {
    const dialogRef = this.dialog.open(OrdersModalComponent, {
      width: '600px', 
      data: { custId }, 
      disableClose: false, 
      hasBackdrop: true, 
      autoFocus: false, 
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('El modal fue cerrado:', result);
    });
  }



  
  createOrder(custId: number): void {
    this.dialog.open(NewOrderFormComponent, {
      width: '600px',
      data: { custId: custId } 
    });
  }
}
