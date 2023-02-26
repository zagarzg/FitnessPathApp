import { Exercise } from './Exercise';

export interface TrainingLog {
  id: string | null;
  date: Date;
  exercises: Exercise[];
  userId: string;
}
