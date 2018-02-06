import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';


import { DigitalAssetsModule } from "./digital-assets/digital-assets.module";
import { LoginModule } from "./login/login.module";
import { ProductsModule } from "./products/products.module";
import { SharedModule } from "./shared/shared.module";
import { TenantsModule } from "./tenants/tenants.module";

import { PageNotFoundComponent } from "./page-not-found.component";

import "./rxjs-extensions";

import { AppComponent } from './app.component';
import { AppMasterPageComponent } from "./app-master-page.component";

import { routing } from "./app-routing.module";
import { constants } from "./shared/constants";

const declarations = [
    AppComponent,
    AppMasterPageComponent,
    PageNotFoundComponent
];

const providers = [
    { provide: constants.BASE_URL, useValue: "http://aspnetcoregettingstarted.azurewebsites.net" },
    { provide: constants.DEFAULT_PATH, useValue: "/" }
];

@NgModule({
    imports: [
        routing,
        BrowserModule,
        CommonModule,
        FormsModule,
        RouterModule,

        DigitalAssetsModule,
        LoginModule,
        ProductsModule,
        SharedModule,
        TenantsModule
    ],
    providers,
    declarations,
    exports: [declarations],
    bootstrap: [AppComponent]
})
export class AppModule { }
