import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";


import { SharedModule } from "../shared/shared.module";

const declarations: Array<any> = [];

const providers: Array<any> = [];


@NgModule({
    declarations,
    providers,
    imports: [
        CommonModule,
        SharedModule
    ],
    exports:declarations
})
export class ProductsModule {}
