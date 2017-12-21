import {Component, ChangeDetectorRef, OnInit, OnDestroy} from "@angular/core";
import {MediaMatcher} from '@angular/cdk/layout';

@Component({
  selector: 'app-secured',
  templateUrl: './secured.component.html',
  styleUrls: ['./secured.component.scss']
})
export class SecuredComponent implements OnDestroy {

  private _mobileQueryListener: () => void;
  mobileQuery: MediaQueryList;

  constructor(private changeDetectorRef: ChangeDetectorRef, private media: MediaMatcher) {
    this.mobileQuery = this.media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

}
