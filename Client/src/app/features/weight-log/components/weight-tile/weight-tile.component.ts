import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WeightLog } from '../../models/WeightLog';

@Component({
  selector: 'app-weight-tile',
  templateUrl: './weight-tile.component.html',
  styleUrls: ['./weight-tile.component.scss'],
})
export class WeightTileComponent {
  @Input() weightLog!: WeightLog | undefined;
  @Output() deleteWeightLogEvent: EventEmitter<string> =
    new EventEmitter<string>();
  @Output() updateWeightLogEvent: EventEmitter<WeightLog> =
    new EventEmitter<WeightLog>();

  weightLogUpdate() {
    this.updateWeightLogEvent.emit(this.weightLog);
  }

  weightLogDelete() {
    this.deleteWeightLogEvent.emit(this.weightLog?.id);
  }
}
