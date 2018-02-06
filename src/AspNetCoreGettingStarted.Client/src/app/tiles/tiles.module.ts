import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";

import { SharedModule } from "../shared/shared.module";

import { TilesService } from "./tiles.service";

const declarations = [];

const providers = [
    TilesService
];

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        SharedModule
    ],
    declarations,
    providers,
    exports:declarations
})
export class TilesModule { }