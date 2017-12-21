import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA} from "@angular/material";

@Component({
  selector: 'linn-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class ConfirmDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {

  }

}
