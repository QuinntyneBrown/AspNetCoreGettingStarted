import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { DashboardTilesModule } from "../dashboard-tiles";

import { DashboardPageComponent } from "./dashboard-page.component";
import { DashboardsService } from "./dashboards.service";
import { DashboardMasterPageComponent } from "./dashboard-master-page.component";
import { DashboardHeaderComponent } from "./dashboard-header.component";
import { TileSelectionModalComponent } from "../tiles/tile-selection-modal.component";
import { TileSelectionItemComponent } from "../tiles/tile-selection-item.component";

const providers = [
    DashboardsService
];

const declarations = [
    DashboardPageComponent,
    DashboardMasterPageComponent,
    DashboardHeaderComponent
];

const customElements = [
    TileSelectionModalComponent,
    TileSelectionItemComponent
];

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        DashboardTilesModule
    ],
    declarations,
    providers,
    exports: declarations
})
export class DashboardModule { }