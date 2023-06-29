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
import { BingoCardDisplayComponent } from './bingo/bingo-card-display/bingo-card-display.component';
import { BingoCardSlotComponent } from './bingo/bingo-card-slot/bingo-card-slot.component';
import {MatCardModule} from '@angular/material/card';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatSliderModule} from '@angular/material/slider';
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        LoginNavMenuComponent,
        BingoCardListViewComponent,
        BingoCardDisplayComponent,
        BingoCardSlotComponent
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
        MatCardModule,
        MatExpansionModule,
        MatSliderModule,
        MatInputModule,
        FormsModule,
        MatFormFieldModule
    ]
})
export class AppModule { }


