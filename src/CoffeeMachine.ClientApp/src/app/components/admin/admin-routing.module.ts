import { PurchasesResolver } from './resolvers/purchases.resolver';
import { StatisticsResolver } from './resolvers/statistics.resolver';
import { CoffeesResolver } from './resolvers/coffees.resolver';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { CoffeesComponent } from './components/coffees/coffees.component';
import { PurchasesComponent } from './components/purchases/purchases.component';
import { StatisticsComponent } from './components/statistics/statistics.component';

const routes: Routes = [
  {
    path: '',
    component: AdminDashboardComponent,
    children: [
      {
        path: 'purchases',
        component: PurchasesComponent,
        resolve: {
          purchases: PurchasesResolver,
        },
      },
      {
        path: 'statistics',
        component: StatisticsComponent,
        resolve: {
          statistics: StatisticsResolver,
        },
      },
      {
        path: 'coffees',
        component: CoffeesComponent,
        resolve: {
          coffees: CoffeesResolver,
        },
      },
      { path: '', redirectTo: 'coffees', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
