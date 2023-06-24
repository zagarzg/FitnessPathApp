export enum ExerciseType {
  Compound,
  Accessory,
}

export interface ExerciseChoice {
  id: string;
  name: string;
  imageData: string | null;
  exerciseType: ExerciseType;
  isFavorite: boolean;
}
