import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Component({
    templateUrl: "./app-master-page.component.html",
    styleUrls: ["./app-master-page.component.css"],
    selector: "ce-app-master-page"
})
export class AppMasterPageComponent { 

    private _ngUnsubscribe: Subject<void> = new Subject<void>();

    ngOnDestroy() {
         this._ngUnsubscribe.next();	
    }
}
