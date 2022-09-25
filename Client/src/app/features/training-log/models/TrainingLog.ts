import { Exercise } from "./Exercise";

export interface TrainingLog {
    id: string,
    date: Date,
    exercises: Exercise[]
}