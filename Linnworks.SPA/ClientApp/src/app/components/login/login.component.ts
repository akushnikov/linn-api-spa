import {Component} from "@angular/core";
import {FormControl, Validators} from '@angular/forms';
import {AuthService} from "../../services";

@Component({
  selector: 'linn-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  token = new FormControl('', [Validators.required]);
  loading: boolean = false;

  constructor(private authService: AuthService) {

  }

  getErrorMessage() {
    return this.token.hasError('required')
      ? 'You must provide a token'
      : this.token.hasError('token')
        ? 'Invalid token'
        : '';
  }

  checkToken() {
    this.loading = true;
    this.authService.validate(this.token.value)
      .subscribe(ok => {
        this.loading = false;
      }, err => {
        this.loading = false;
        this.token.setErrors({"token": true});
      });
  }
}
