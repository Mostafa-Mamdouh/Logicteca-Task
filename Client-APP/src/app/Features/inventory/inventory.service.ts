import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { InventoryParams } from 'src/app/shared/models/inventory-params';
import {  Pagination } from 'src/app/shared/models/pagination';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  baseUrl = environment.apiUrl;
  pagination = new Pagination();
  constructor(private http: HttpClient) {}


  getInventory(inventoryParams: InventoryParams) {
    let params = new HttpParams();


    if (inventoryParams.search) {
      params = params.append('search', inventoryParams.search);
    }

    params = params.append('pageIndex', inventoryParams.pageNumber.toString());
    params = params.append('pageSize', inventoryParams.pageSize.toString());
    return this.http
      .get<Pagination>(
        this.baseUrl +
          'product',
        {
          observe: 'response',
          params,
        }
      )
      .pipe(
        map((response) => {
          this.pagination = response.body!=null?response.body:this.pagination;
          return this.pagination;
        })
      );
  }
  exportInventory( inventoryParams: InventoryParams) {
    let params = new HttpParams();


    if (inventoryParams.search) {
      params = params.append('search', inventoryParams.search);
    }
    return this.http
      .get(
        this.baseUrl +
          `product/export`,
        {
          responseType: 'blob',
          params: params,
        }
      )
      .pipe();
  }

}
