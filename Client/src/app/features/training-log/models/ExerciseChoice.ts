export enum ExerciseType {
  Compound = 1,
  Accessory = 2,
}

export interface ExerciseChoice {
  id: string;
  name: string;
  imageUrl: string | null;
  exerciseType: ExerciseType;
  isFavorite: boolean;
}
