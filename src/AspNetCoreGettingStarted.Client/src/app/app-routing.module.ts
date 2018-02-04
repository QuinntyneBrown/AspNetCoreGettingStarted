import { Routes, RouterModule } from '@angular/router';
import { LoginModule } from "./login/login.module";
import { AppMasterPageComponent } from "./app-master-page.component";
import { AuthGuardService } from "./shared/services/auth-guard.service";

const canActivate = [
    AuthGuardService
];

export const routes: Routes = [
    {
        path: 'login',
        loadChildren: "./login/login.module#LoginModule"
    },
    {
        path: 'digitalassets',
        canActivate,
        component: AppMasterPageComponent,
        children: [
            {
                path: '',
                loadChildren: "./digital-assets/digital-assets.module#DigitalAssetsModule"
            }
        ]
    },
    {
        path: 'products',
        canActivate,
        component: AppMasterPageComponent,
        children: [
            {
                path: '',
                loadChildren: "./products/products.module#ProductsModule"
            }
        ]
    }
];

export const routing = RouterModule.forRoot(routes, { useHash: false });