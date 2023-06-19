import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { NavbarComponent } from './navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HttpClientModule } from '@angular/common/http';
import { LoginNavMenuComponent } from './navbar/login-nav-menu/login-nav-menu.component';
import { LoginManagerService } from 'src/services/login-manager.service';
import { BingoCardListViewComponent } from './bingo/bingo-card-list-view/bingo-card-list-view.component';
import { BingoCardService } from 'src/services/bingo-card.service';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        LoginNavMenuComponent,
        BingoCardListViewComponent
    ],
    providers: [ LoginManagerService,BingoCardService],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        MatSlideToggleModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        BrowserAnimationsModule,
        HttpClientModule,
    ]
})
export class AppModule { }
