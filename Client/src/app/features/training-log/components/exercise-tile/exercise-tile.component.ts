import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Exercise } from '../../models/Exercise';

@Component({
  selector: 'app-exercise-tile',
  templateUrl: './exercise-tile.component.html',
  styleUrls: ['./exercise-tile.component.scss'],
})
export class ExerciseTileComponent {
  @Input() exercise!: Exercise;
  @Output() deleteExerciseEvent: EventEmitter<string> =
    new EventEmitter<string>();
  @Output() updateExerciseEvent: EventEmitter<Exercise> =
    new EventEmitter<Exercise>();

  exerciseDelete() {
    this.deleteExerciseEvent.emit(this.exercise.id);
  }

  exerciseUpdate() {
    this.updateExerciseEvent.emit(this.exercise);
  }
}
