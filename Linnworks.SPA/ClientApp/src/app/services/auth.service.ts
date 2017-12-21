import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";

import "rxjs/add/operator/do";

const DB_TOKEN_KEY = 'linn-api-token';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient) {

  }

  get token(): string {
    return localStorage.getItem(DB_TOKEN_KEY);
  }
  set token(value: string){
    localStorage.setItem(DB_TOKEN_KEY, value);
  }

  get isAuthenticated() {
    const token = this.token;
    return token != null && this.token != '';
  }

  validate(token: string) {
    return this.http.get(`/api/auth/validate/${token}`).do(() => {
      this.token = token;
    });
  }

  logout() {
    localStorage.removeItem(DB_TOKEN_KEY);
  }

}
