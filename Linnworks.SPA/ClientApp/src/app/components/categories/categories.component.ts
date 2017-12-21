import {Component, OnInit, ViewChild} from "@angular/core";
import {CategoryService} from "../../services";
import {ICategoryListModel} from "../../models";
import {MatSort, MatTableDataSource} from "@angular/material";

@Component({
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  constructor(private categoryService: CategoryService) {

  }

  defaultGuid = '00000000-0000-0000-0000-000000000000';

  categories: ICategoryListModel[];
  dataSource: MatTableDataSource<ICategoryListModel>;
  loading = false;

  gridColumns = ['name', 'count', 'actions'];

  @ViewChild(MatSort)
  sort: MatSort;

  ngOnInit(): void {
    this.loading = true;
    this.categoryService.getList()
      .subscribe(res => {
        this.categories = res.data;
        this.dataSource = new MatTableDataSource<ICategoryListModel>(this.categories);
        this.dataSource.sort = this.sort;
      }, err => {
        console.log(err);
      }, () => {
        this.loading = false;
      });
  }

  applyFilter(value: string) {
    value = value.trim().toLowerCase();
    this.dataSource.filter = value;
  }

  onEditClick(id: string) {
    debugger;
  }

  onDeleteClick(id: string) {
    debugger;
  }

}
