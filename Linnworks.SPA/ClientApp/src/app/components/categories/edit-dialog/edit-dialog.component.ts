import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {CategoryService} from "../../../services";
import {FormControl, Validators} from "@angular/forms";
import {ICategoryModel} from "../../../models";

@Component({
  selector: 'linn-create-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.scss']
})
export class EditDialogComponent {
  constructor(private categoryService: CategoryService, private dialogRef: MatDialogRef<EditDialogComponent>, @Inject(MAT_DIALOG_DATA) private data: ICategoryModel){

  }

  categoryName = new FormControl(this.data.categoryName, [Validators.required]);
  loading = false;

  getErrorMessage() {
    return this.categoryName.hasError('required')
      ? 'Provide not empty name'
      : this.categoryName.hasError('categoryName')
        ? this.categoryName.getError('categoryName')
        : '';
  }

  update(categoryName) {
    if (this.data.categoryName == categoryName)
    {
      this.dialogRef.close(false);
      return;
    }
    this.loading = true;
    let entity = Object.assign({}, this.data, {categoryName});
    this.categoryService.update(entity)
      .finally(() => this.loading = false)
      .subscribe(ok => {
        this.dialogRef.close(ok.success);
      }, err => {
        this.categoryName.setErrors({'categoryName': err.error.data});
      });
  }
}
