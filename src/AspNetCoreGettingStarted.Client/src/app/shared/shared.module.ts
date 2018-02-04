import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpModule } from "@angular/http";

import { HeaderComponent } from "./components/header.component";
import { Storage } from "./services/storage.service";
import { RedirectService } from "./services/redirect.service";
import { AuthGuardService } from "./services/auth-guard.service";

const declarations: Array<any> = [
    HeaderComponent
];

const providers: Array<any> = [
    Storage,
    RedirectService,
    AuthGuardService
];

@NgModule({
    declarations,
    providers,
    imports: [
        CommonModule,
        HttpModule
    ],
    exports: declarations
})
export class SharedModule { }