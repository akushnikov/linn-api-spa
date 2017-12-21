import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ICategoryListModel, ICategoryModel, IResponse} from "../models";
import {Observable} from "rxjs/Observable";

@Injectable()
export class CategoryService {
  private route = '/api/category';

  constructor(private http: HttpClient) {

  }

  getList(): Observable<IResponse<ICategoryListModel[]>> {
    return this.http.get<IResponse<ICategoryListModel[]>>(this.route);
  }

  get(id: string): Observable<IResponse<ICategoryModel>> {
    return this.http.get<IResponse<ICategoryModel>>(`${this.route}/${id}`);
  }

  create(name: string) {
    let params = new HttpParams();
    params = params.set('name', name);
    return this.http.post<IResponse<ICategoryModel>>(this.route, params);
  }

  update(entity: ICategoryModel) {
    let params = new HttpParams();
    params = Object.entries(entity).reduce((p, [key, value]) => p.set(key, value), params);
    return this.http.put<IResponse<null>>(this.route, params);
  }

  delete(id: string) {
    return this.http.delete<IResponse<null>>(`${this.route}/${id}`);
  }
}
