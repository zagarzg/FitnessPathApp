import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { take } from 'rxjs/operators';
import { ExerciseChoice, ExerciseType } from '../../models/ExerciseChoice';
import { ExerciseChoiceService } from '../../services/exercise-choice.service';

@Component({
  selector: 'app-exercise-form',
  templateUrl: './exercise-form.component.html',
  styleUrls: ['./exercise-form.component.scss'],
})
export class ExerciseFormComponent implements OnInit {
  @ViewChild('filterInput') filterInput: any;
  exerciseType = ExerciseType;
  exerciseForm!: FormGroup;
  updateMode: boolean = false;
  selectedChip: string = '';

  availableExercises: ExerciseChoice[] = [];
  dataSource: ExerciseChoice[] = [];
  selectedExercise: { id: string; image: string | null } = {
    id: '',
    image: '',
  };

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ExerciseFormComponent>,
    private exerciseChoiceService: ExerciseChoiceService
  ) {}

  ngOnInit(): void {
    this.exerciseChoiceService
      .getAllExerciseChoices()
      .pipe(take(1))
      .subscribe((choices) => {
        this.availableExercises = choices;
        this.dataSource = this.availableExercises;
      });

    this.exerciseForm = this._formBuilder.group({
      reps: ['', Validators.required],
      sets: ['', Validators.required],
      weight: ['', Validators.required],
      trainingLogId: ['', Validators.required],
      exerciseChoiceId: ['', Validators.required],
      exerciseChoice: [{}, Validators.required],
    });

    if (this.data.trainingLogId) {
      this.exerciseForm.patchValue({
        trainingLogId: this.data.trainingLogId,
      });
    }

    if (this.data.exercise) {
      this.updateMode = true;
      this.exerciseForm.patchValue({
        reps: this.data.exercise.reps,
        sets: this.data.exercise.sets,
        weight: this.data.exercise.weight,
      });
      // Setting form controls like this is neccesarry since group patchValue does not work
      this.exerciseForm.controls.exerciseChoice.setValue(
        this.data.exercise.exerciseChoice
      );
      this.exerciseForm.controls.exerciseChoiceId.setValue(
        this.data.exercise.exerciseChoice.id
      );
      this.selectedExercise = {
        id: this.data.exercise.exerciseChoiceId,
        image: this.data.exercise.exerciseChoice.imageUrl,
      };
    }
  }

  onExerciseChange(exerciseId: string) {
    let exerciseChoice: ExerciseChoice | undefined =
      this.availableExercises.find(
        (exercise: ExerciseChoice) => exercise.id === exerciseId
      );

    if (exerciseChoice !== undefined) {
      this.selectedExercise = {
        id: exerciseId,
        image: exerciseChoice.imageUrl,
      };
      this.exerciseForm.controls.exerciseChoiceId.setValue(exerciseId);
    }
  }

  onSubmit(form: FormGroup) {
    let exerciseChoice = this.availableExercises.find(
      (exercise: ExerciseChoice) => exercise.id === form.value.exerciseChoiceId
    );
    form.controls.exerciseChoice.setValue(exerciseChoice);
    this.dialogRef.close(form.value);
  }

  applyFilter(filterEvent: any) {
    this.dataSource = this.availableExercises.filter(
      (exercise: ExerciseChoice) =>
        exercise.name.toLowerCase().includes(filterEvent.target.value)
    );
  }

  clearFilter() {
    this.filterInput.nativeElement.value = '';
    this.dataSource = this.availableExercises;
  }

  selectFilterChip(chip: string) {
    switch (chip) {
      case this.selectedChip:
        this.selectedChip = '';
        this.dataSource = this.availableExercises;
        break;
      case 'Compound':
        this.selectedChip = chip;

        this.dataSource = this.availableExercises.filter(
          (exercise: ExerciseChoice) =>
            exercise.exerciseType === this.exerciseType.Compound
        );
        break;
      case 'Accessory':
        this.selectedChip = chip;

        this.dataSource = this.availableExercises.filter(
          (exercise: ExerciseChoice) =>
            exercise.exerciseType === this.exerciseType.Accessory
        );
        break;
      case 'Favorite':
        this.selectedChip = chip;

        this.dataSource = this.availableExercises.filter(
          (exercise: ExerciseChoice) => exercise.isFavorite === true
        );
        break;
      default:
    }
  }

  toggleFavorite(id: string) {
    let exerciseIndex = this.availableExercises.findIndex(
      (exercise: ExerciseChoice) => exercise.id === id
    );
    let exerciseChoice = this.availableExercises[exerciseIndex];

    exerciseChoice.isFavorite = !exerciseChoice.isFavorite;
    this.exerciseChoiceService.updateExerciseChoice(exerciseChoice).subscribe();
    this.dataSource = this.availableExercises;
  }

  onDialogClose() {
    this.dialogRef.close();
  }
}
