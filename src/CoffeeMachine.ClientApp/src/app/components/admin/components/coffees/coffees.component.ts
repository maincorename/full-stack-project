import { ICreatedCoffee } from './../../../../models/createdCoffee';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { CoffeeService } from './../../../../services/coffee.service';

import { ICoffee } from 'src/app/models/coffee';
import { Observable } from 'rxjs';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-coffees',
  templateUrl: './coffees.component.html',
  styleUrls: ['./coffees.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CoffeesComponent implements OnInit {
  @ViewChild('readOnlyTableHeadTemplate', { static: false })
  readOnlyTableHeadTemplate!: TemplateRef<any> | null;
  @ViewChild('readOnlyTemplate', { static: false }) readOnlyTemplate!: TemplateRef<any> | null;
  @ViewChild('editTemplate', { static: false }) editTemplate!: TemplateRef<any> | null;
  @ViewChild('editTableHeadTemplate', { static: false })
  editTableHeadTemplate!: TemplateRef<any> | null;

  coffeeList!: Observable<ICoffee[]>;
  editedCoffee: ICoffee | null = null;
  coffeeForm!: FormGroup;
  createdCoffee: ICreatedCoffee;

  constructor(private coffeeService: CoffeeService, private ref: ChangeDetectorRef) {
    this.editedCoffee = { id: '', name: '', price: 0 };
    this.createdCoffee = { name: '', price: 0 };
  }

  ngOnInit(): void {
    this.loadCoffees();
    this.coffeeForm = new FormGroup({
      coffeeName: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[-\][A-Za-zА-Яа-яЁё]{3,20}$/),
      ]),
      coffeePrice: new FormControl('', [
        Validators.required,
        Validators.pattern(/\d/),
        Validators.min(50),
        Validators.max(10000),
        this.validateNumField.bind(this),
      ]),
    });
  }

  validateNumField(control: AbstractControl): { [key: string]: any } | null {
    const rem = Number.parseFloat(control.value) % 50 === 0;
    return rem ? null : { error: 'Field is not valid!' };
  }
  loadCoffees() {
    this.coffeeList = this.coffeeService.GetAll();
  }

  loadTemplate(coffee: ICoffee) {
    if (this.editedCoffee && this.editedCoffee.id === coffee.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }

  loadHeadTemplate() {
    if (this.editedCoffee?.id === '') {
      return this.readOnlyTableHeadTemplate;
    } else {
      return this.editTableHeadTemplate;
    }
  }

  editCoffee(coffee: ICoffee) {
    this.editedCoffee!.id = coffee.id;
    this.editedCoffee!.name = coffee.name;
    this.editedCoffee!.price = coffee.price;
  }

  cancel() {
    this.editedCoffee = { id: '', name: '', price: 0 };
  }

  delete(coffeeId: string) {
    this.coffeeService.Delete(coffeeId).subscribe(() => {
      this.loadCoffees();
      this.ref.detectChanges();
    });
    this.ref.detectChanges();
  }

  save(coffee: ICoffee) {
    this.coffeeService.Update(coffee).subscribe(() => {
      this.loadCoffees();
      this.ref.detectChanges();
    });
    this.editedCoffee = { id: '', name: '', price: 0 };
  }

  add() {
    this.createdCoffee.name = this.coffeeForm.value['coffeeName'];
    this.createdCoffee.price = this.coffeeForm.value['coffeePrice'];

    this.coffeeService.Create(this.createdCoffee).subscribe(() => {
      this.loadCoffees();
      this.ref.detectChanges();
    });
  }
}
