import {Component, OnInit, ViewChild} from "@angular/core";
import {CategoryService} from "../../services";
import {ICategoryListModel} from "../../models";
import {MatDialog, MatSort, MatTableDataSource} from "@angular/material";
import {ConfirmDialogComponent} from "./delete-dialog/delete-dialog.component";
import {CreateDialogComponent} from "./create-dialog/create-dialog.component";
import "rxjs/add/operator/finally";

@Component({
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  constructor(private categoryService: CategoryService, private deleteDialog: MatDialog) {

  }

  defaultGuid = '00000000-0000-0000-0000-000000000000';

  categories: ICategoryListModel[];
  dataSource: MatTableDataSource<ICategoryListModel>;
  loading = false;

  gridColumns = ['name', 'count', 'actions'];

  @ViewChild(MatSort)
  sort: MatSort;

  ngOnInit(): void {
    this.loadRecords();
  }

  loadRecords() {
    this.loading = true;
    this.categoryService.getList()
      .finally(() => this.loading = false)
      .subscribe(res => {
        this.categories = res.data;
        this.dataSource = new MatTableDataSource<ICategoryListModel>(this.categories);
        this.dataSource.sort = this.sort;
      }, err => {
        console.log(err);
      });
  }

  applyFilter(value: string) {
    value = value.trim().toLowerCase();
    this.dataSource.filter = value;
  }

  onCreateClick() {
    let ref = this.deleteDialog.open(CreateDialogComponent, {
      width: '300px'
    });
    ref.afterClosed().subscribe(res => {
      if (!res) return;
      this.loadRecords();
    });
  }

  onEditClick(id: string) {
    debugger;
  }

  onDeleteClick(id: string) {
    let category = this.categories.find(item => item.categoryId == id);
    let ref = this.deleteDialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: category
    });
    ref.afterClosed().subscribe(res => {
      if (!res) return;

      this.categoryService.delete(id)
        .subscribe(ok => this.loadRecords(),
            err => console.log(err));
    })
  }

}
