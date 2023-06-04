import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FoodItem } from '../../models/FoodItem';

@Component({
  selector: 'app-food-item-form',
  templateUrl: './food-item-form.component.html',
  styleUrls: ['./food-item-form.component.scss'],
})
export class FoodItemFormComponent implements OnInit {
  foodItemForm!: FormGroup;
  updateMode: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<FoodItemFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.foodItemForm = this._formBuilder.group({
      name: ['', Validators.required],
      carbs: ['', Validators.required],
      protein: ['', Validators.required],
      fat: ['', Validators.required],
      calories: ['', Validators.required],
      foodLogId: ['', Validators.required],
    });

    if (this.data.foodLogId) {
      this.foodItemForm.patchValue({
        foodLogId: this.data.foodLogId,
      });
    }

    if (this.data.foodItem) {
      this.updateMode = true;
      this.foodItemForm.patchValue({
        name: this.data.foodItem.name,
        carbs: this.data.foodItem.reps,
        protein: this.data.foodItem.protein,
        fat: this.data.foodItem.fat,
        calories: this.data.foodItem.calories,
      });
    }
  }

  onSubmit(form: FormGroup) {
    this.dialogRef.close(form.value as FoodItem);
  }
}
