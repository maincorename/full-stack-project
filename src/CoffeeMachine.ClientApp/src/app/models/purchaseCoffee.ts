import { ICoffee } from 'src/app/models/coffee';

export interface IPurchaseCoffee {
  changeBanknotes: number[];
  purchasedCoffee: ICoffee;
}
