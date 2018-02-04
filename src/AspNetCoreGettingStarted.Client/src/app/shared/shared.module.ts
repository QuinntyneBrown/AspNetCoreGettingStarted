import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { HeaderComponent } from "./components/header.component";
import { Storage } from "./services/storage.service";
import { RedirectService } from "./services/redirect.service";
import { AuthGuardService } from "./services/auth-guard.service";

import { TenantInterceptor } from "./interceptors";
import { AuthInterceptor } from "./interceptors";

const declarations: Array<any> = [
    HeaderComponent
];

const providers: Array<any> = [
    Storage,
    RedirectService,
    AuthGuardService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: TenantInterceptor,
        multi: true
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
    }
];

@NgModule({
    declarations,
    providers,
    imports: [
        CommonModule,
        HttpClientModule
    ],
    exports: declarations
})
export class SharedModule { }