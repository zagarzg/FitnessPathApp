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
    @Inject(MAT_DIALOG_DATA)
    public data: {
      date: Date | null;
      weightLogId: string | undefined;
    }
  ) {}

  ngOnInit(): void {
    console.log(`WeightLogId: ${this.data.weightLogId}`);
    this.weightLogForm = this._formBuilder.group({
      id: ['', Validators.nullValidator],
      date: ['', Validators.required],
      value: ['', Validators.required],
    });

    if (this.data.weightLogId?.length !== 0) {
      this.updateMode = true;
      this.weightLogForm.patchValue({
        date: this.data.date,
        id: this.data.weightLogId,
      });
    }
  }

  onSubmit(form: FormGroup) {
    form.value.date = this.data.date;
    this.updateMode = false;
    this.dialogRef.close(form.value as WeightLog);
  }
}
