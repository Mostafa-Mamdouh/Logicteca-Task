import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServerErrorComponent } from './core/server-error/server-error.component';

const routes: Routes = [

  {
    path: 'server-error',
    component: ServerErrorComponent,
    data: { breadcrumb: 'Server Errors' },
  },
  {
    path: '',
    loadChildren: () =>
      import('./features/inventory/inventory.module').then((mod) => mod.InventoryModule),
    data: {
      breadcrumb: 'Products'
    },
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
