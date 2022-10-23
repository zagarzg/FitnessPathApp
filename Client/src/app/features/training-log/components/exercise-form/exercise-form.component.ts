import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Exercise } from '../../models/Exercise';

@Component({
  selector: 'app-exercise-form',
  templateUrl: './exercise-form.component.html',
  styleUrls: ['./exercise-form.component.scss']
})
export class ExerciseFormComponent implements OnInit {

  exerciseForm!: FormGroup;
  updateMode: boolean = false;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<ExerciseFormComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.exerciseForm = this._formBuilder.group({
      name: ['', Validators.required],
      reps: ['', Validators.required],
      sets: ['', Validators.required],
      weight: ['', Validators.required],
      trainingLogId: ['', Validators.required],
    });

    if(this.data.trainingLogId) {
      this.exerciseForm.patchValue({
        trainingLogId: this.data.trainingLogId
      });
    }

    if(this.data.exercise) {
      this.updateMode = true;
      this.exerciseForm.patchValue({
        name: this.data.exercise.name,
        reps: this.data.exercise.reps,
        sets: this.data.exercise.sets,
        weight: this.data.exercise.weight
      });
    }
  }

  onSubmit(form: FormGroup) {
    this.dialogRef.close(form.value as Exercise);
  }

}
