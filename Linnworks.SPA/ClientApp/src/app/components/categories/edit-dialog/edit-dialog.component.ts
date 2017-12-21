import {Component} from "@angular/core";
import {MatDialogRef} from "@angular/material";
import {CategoryService} from "../../../services";

@Component({
  selector: 'linn-create-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.scss']
})
export class EditDialogComponent {
  constructor(private categoryService: CategoryService, private dialogRef: MatDialogRef<EditDialogComponent>){

  }


}
