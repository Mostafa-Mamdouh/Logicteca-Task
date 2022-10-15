import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryComponent } from './inventory.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { InventoryRoutingModule } from './inventory-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoreModule } from 'src/app/core/core.module';



@NgModule({
  declarations: [InventoryComponent],
  imports: [
    CommonModule,
    SharedModule,
    InventoryRoutingModule,
    CoreModule,
    PaginationModule.forRoot(),
  ]
})
export class InventoryModule { }
