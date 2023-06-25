export enum ExerciseType {
  Compound,
  Accessory,
}

export interface ExerciseChoice {
  id: string;
  name: string;
  imageUrl: string | null;
  exerciseType: ExerciseType;
  isFavorite: boolean;
}
