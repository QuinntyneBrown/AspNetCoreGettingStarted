﻿import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";

import { DigitalAssetListPageComponent } from "./digital-asset-list-page.component";
import { DigitalAssetsService } from "./digital-assets.service";
import { DigitalAssetItemComponent } from "./digital-asset-item.component";
import { DigitalAssetUploadPageComponent } from "./digital-asset-upload-page.component";
import { DigitalAssetEditPageComponent } from "./digital-asset-edit-page.component";

const declarables = [
    DigitalAssetListPageComponent,
    DigitalAssetItemComponent,
    DigitalAssetUploadPageComponent,
    DigitalAssetEditPageComponent
];

const providers = [
    DigitalAssetsService
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        SharedModule
    ],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class DigitalAssetsModule { }
