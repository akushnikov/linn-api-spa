import {Component} from "@angular/core";
import {MatDialogRef} from "@angular/material";
import {FormControl, Validators} from "@angular/forms";
import {CategoryService} from "../../../services";
import "rxjs/add/operator/finally";

@Component({
  selector: 'linn-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.scss']
})
export class CreateDialogComponent {
  constructor(private categoryService: CategoryService, private dialogRef: MatDialogRef<CreateDialogComponent>) {

  }

  categoryName = new FormControl('', [Validators.required]);
  loading = false;

  getErrorMessage() {
    return this.categoryName.hasError('required')
      ? 'Provide not empty name'
      : this.categoryName.hasError('categoryName')
        ? this.categoryName.getError('categoryName')
        : '';
  }

  create() {
    this.loading = true;
    this.categoryService.create(this.categoryName.value)
      .finally(() => this.loading = false)
      .subscribe(ok => {
        this.dialogRef.close(ok.data);
      }, err => {
        this.categoryName.setErrors({'categoryName': err.error.data});
      });
  }
}
