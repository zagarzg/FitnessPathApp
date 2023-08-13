import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FoodItem } from '../../models/FoodItem';

@Component({
  selector: 'app-food-tile',
  templateUrl: './food-tile.component.html',
  styleUrls: ['./food-tile.component.scss'],
})
export class FoodTileComponent {
  @Input() foodItem!: FoodItem;
  @Output() deleteFoodItemEvent: EventEmitter<string> =
    new EventEmitter<string>();
  @Output() updateFoodItemEvent: EventEmitter<FoodItem> =
    new EventEmitter<FoodItem>();

  foodItemDelete() {
    this.deleteFoodItemEvent.emit(this.foodItem.id);
  }

  foodItemUpdate() {
    this.updateFoodItemEvent.emit(this.foodItem);
  }
}
