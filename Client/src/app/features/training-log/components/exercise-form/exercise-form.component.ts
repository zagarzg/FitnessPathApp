import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExerciseChoice, ExerciseType } from '../../models/ExerciseChoice';
import { ExerciseChoiceService } from '../../services/exercise-choice.service';

@Component({
  selector: 'app-exercise-form',
  templateUrl: './exercise-form.component.html',
  styleUrls: ['./exercise-form.component.scss'],
})
export class ExerciseFormComponent implements OnInit {
  exerciseType = ExerciseType;
  exerciseForm!: FormGroup;
  updateMode: boolean = false;

  availableExercises: any = [];
  selectedExercise: { id: string; image: string } = { id: '', image: '' };

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ExerciseFormComponent>,
    private exerciseChoiceService: ExerciseChoiceService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.exerciseChoiceService.getAllExerciseChoices().subscribe((choices) => {
      this.availableExercises = choices;
    });

    this.exerciseForm = this._formBuilder.group({
      reps: ['', Validators.required],
      sets: ['', Validators.required],
      weight: ['', Validators.required],
      trainingLogId: ['', Validators.required],
      exerciseChoiceId: ['', Validators.required],
    });

    if (this.data.trainingLogId) {
      this.exerciseForm.patchValue({
        trainingLogId: this.data.trainingLogId,
      });
    }

    if (this.data.exercise) {
      this.updateMode = true;
      this.exerciseForm.patchValue({
        exerciseChoiceId: this.data.exerciseChoiceId,
        reps: this.data.exercise.reps,
        sets: this.data.exercise.sets,
        weight: this.data.exercise.weight,
      });
    }
  }

  onExerciseChange(exerciseId: string) {
    console.log(exerciseId);
    let exerciseChoice: ExerciseChoice = this.availableExercises.find(
      (exercise: ExerciseChoice) => exercise.id === exerciseId
    );
    this.selectedExercise = {
      id: exerciseId,
      image: 'data:image/jpeg;base64,' + exerciseChoice.imageData,
    };
  }

  onSubmit(form: FormGroup) {
    let exerciseChoice = this.availableExercises.find(
      (exercise: ExerciseChoice) => exercise.id === form.value.exerciseChoiceId
    );
    form.addControl('exerciseChoice', new FormControl(exerciseChoice));
    this.dialogRef.close(form.value);
  }

  onDialogClose() {
    this.dialogRef.close();
  }
}
