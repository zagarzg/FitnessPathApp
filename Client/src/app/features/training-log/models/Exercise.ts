import { ExerciseChoice } from './ExerciseChoice';

export interface Exercise {
  id: string;
  weight: number;
  sets: number;
  reps: number;
  trainingLogId: string;
  exerciseChoice: ExerciseChoice | null;
  exerciseChoiceId: string;
}
