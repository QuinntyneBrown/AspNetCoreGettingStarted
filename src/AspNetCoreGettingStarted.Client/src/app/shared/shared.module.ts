import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { HeaderComponent } from "./components/header.component";
import { Storage } from "./services/storage.service";
import { RedirectService } from "./services/redirect.service";
import { AuthGuardService } from "./services/auth-guard.service";

import { TenantInterceptor } from "./interceptors";
import { AuthInterceptor } from "./interceptors";

import { ModalService, ModalServiceFactory } from "./services/modal.service";
import { PopoverService, PopoverServiceFactory } from "./services/popover.service";
import { Position } from "./services/position";
import { Ruler } from "./services/ruler";
import { Space } from "./services/space";

const declarations: Array<any> = [
    HeaderComponent
];

const providers: Array<any> = [
    Storage,
    RedirectService,
    AuthGuardService,
    {
        provide: ModalService,
        useFactory: ModalServiceFactory
    },
    {
        provide: PopoverService,
        useFactory: PopoverServiceFactory,
        deps: [Position]
    },
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