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
      weightLogId: string;
      value: number | undefined;
    }
  ) {}

  ngOnInit(): void {
    this.weightLogForm = this._formBuilder.group({
      id: ['', Validators.nullValidator],
      date: ['', Validators.required],
      value: ['', Validators.required],
    });

    if (this.data.weightLogId !== undefined) {
      this.updateMode = true;
      this.weightLogForm.patchValue({
        date: this.data.date,
        id: this.data.weightLogId,
        value: this.data.value,
      });
    } else {
      this.weightLogForm.patchValue({
        date: this.data.date,
        id: '00000000-0000-0000-0000-000000000000',
      });
    }
  }

  onSubmit(form: FormGroup) {
    form.value.date = this.data.date;
    this.updateMode = false;
    this.dialogRef.close(form.value as WeightLog);
  }

  onDialogClose() {
    this.dialogRef.close();
  }
}
