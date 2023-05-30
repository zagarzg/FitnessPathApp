import { FoodItem } from './FoodItem';

export interface FoodLog {
  id: string;
  date: Date;
  foodItems: FoodItem[];
  userId: string;
}
