import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { WeightLog } from '../../models/WeightLog';

@Component({
  selector: 'app-weight-log-form',
  templateUrl: './weight-log-form.component.html',
  styleUrls: ['./weight-log-form.component.scss'],
})
export class WeightLogFormComponent implements OnInit {
  weightLogForm!: FormGroup;
  updateMode: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<WeightLogFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.weightLogForm = this._formBuilder.group({
      weight: ['', Validators.required],
    });

    if (this.data.weightLogId) {
      this.weightLogForm.patchValue({
        weightLogId: this.data.weightLogId,
      });
    }

    if (this.data.exercise) {
      this.updateMode = true;
      this.weightLogForm.patchValue({
        weight: this.data.exercise.weight,
      });
    }
  }

  onSubmit(form: FormGroup) {
    this.dialogRef.close(form.value as WeightLog);
  }
}
