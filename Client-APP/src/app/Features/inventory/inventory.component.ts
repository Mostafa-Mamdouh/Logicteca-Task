import {
  Component,
  ElementRef,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as FileSaver from 'file-saver';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { InventoryParams } from 'src/app/shared/models/inventory-params';
import { Product } from 'src/app/shared/models/product';
import { InventoryService } from './inventory.service';


@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss'],
})
export class InventoryComponent implements OnInit {
  public totalItems = 64;
  @ViewChild('search', { static: false }) searchTerm: ElementRef | undefined;

  inventoryParams = new InventoryParams();
  exportParams = new InventoryParams();
  totalCount: number | undefined;
  deletemodalRef: BsModalRef | undefined;
  products: Product[] = [];

  constructor(
    private inventoryService: InventoryService,
    private router: Router,
    private toastr: ToastrService,
    private activatedroute: ActivatedRoute
  ) {}

  reloadCurrentPage(){
    setTimeout(
      function() {
        window.location.reload();
      }, 3000);
  }
  ngOnInit(): void {
    this.getInventory();
  }

  uploadFile(e:any) {
   this.toastr.success("File Imported Successfully","Success") ;
    this.reloadCurrentPage();
  }
  getInventory() {
    this.inventoryService.getInventory(this.inventoryParams).subscribe(
      (res) => {
        this.products = res.data;

        this.inventoryParams.pageNumber = res.pageIndex;
        this.inventoryParams.pageSize = res.pageSize;
        this.totalCount = res.count;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onPageChanged(event: any) {
    if (this.inventoryParams.pageNumber !== event) {
      this.inventoryParams.pageNumber = event;
      this.getInventory();
    }
  }

  onSearch() {
    this.inventoryParams.search =this.searchTerm!=null? this.searchTerm.nativeElement.value:"";
    this.inventoryParams.pageNumber = 1;
    this.getInventory();
  }

  onFilter() {
    this.inventoryParams.pageNumber = 1;
    this.getInventory();
  }

  Export() {
    
      this.exportParams.pageNumber = 1;
      this.exportParams.pageSize = 1000000;

      this.inventoryService
        .exportInventory(this.inventoryParams)
        .subscribe(
          (data: Blob) => {
            var fileName =
                 'Cisco_Products_list_' + Date.now() + '';
            FileSaver.saveAs(data, fileName);
          },
          (err: any) => {
            console.log(`Unable to export file`);
          }
        );
    }
  }


