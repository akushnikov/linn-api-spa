import {Component, OnInit} from "@angular/core";
import {CategoryService} from "../../services";
import {ICategoryListModel} from "../../models";
import {MatTableDataSource} from "@angular/material";

@Component({
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  constructor(private categoryService: CategoryService) {

  }

  categories: ICategoryListModel[];
  dataSource: MatTableDataSource<ICategoryListModel>;
  loading = false;

  gridColumns = ['categoryName', 'count', 'actions'];

  ngOnInit(): void {
    this.loading = true;
    this.categoryService.getList()
      .subscribe(res => {
        this.categories = res.data;
        this.dataSource = new MatTableDataSource<ICategoryListModel>(this.categories);
      }, err => {
        console.log(err);
      });
  }

}
