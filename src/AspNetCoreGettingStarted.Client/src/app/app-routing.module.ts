import { Routes, RouterModule } from '@angular/router';
import { LoginModule } from "./login/login.module";
import { AppMasterPageComponent } from "./app-master-page.component";
import { AuthGuardService } from "./shared/services/auth-guard.service";
import { TenantGuardService } from "./tenants/tenant-guard.service";
import { LoginMasterPageComponent } from "./login/login-master-page.component";
import { SetTenantMasterPageComponent } from "./tenants/set-tenant-master-page.component";


import { DigitalAssetEditPageComponent } from "./digital-assets/digital-asset-edit-page.component";
import { DigitalAssetListPageComponent } from "./digital-assets/digital-asset-list-page.component";
import { DigitalAssetUploadPageComponent } from "./digital-assets/digital-asset-upload-page.component";

const canActivate = [
    TenantGuardService,
    AuthGuardService
];

export const routes: Routes = [
    {
        path: 'tenants/set',
        component: SetTenantMasterPageComponent
    },
    {
        path: 'login',
        component: LoginMasterPageComponent,
        canActivate: [TenantGuardService]
    },
    {
        path: 'digitalassets',
        canActivate,
        component: AppMasterPageComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },
            {
                path: 'list',
                component: DigitalAssetListPageComponent
            },
            {
                path: 'edit/:id',
                component: DigitalAssetEditPageComponent
            },
            {
                path: 'upload',
                component: DigitalAssetUploadPageComponent
            }            
        ]
    },
    {
        path: '',
        pathMatch:'full',
        canActivate,
        component: AppMasterPageComponent,
        children: [
            {
                path: '',
                component: DigitalAssetUploadPageComponent
            }
        ]
    }
];

export const routing = RouterModule.forRoot(routes, { useHash: false });