import { MatTabsModule } from '@angular/material/tabs';
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
import {FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from '@angular/material/form-field';
import { NavbarTopComponent } from './navbar/navbar-top/navbar-top.component';
import { BingoGamePageComponent } from './bingo/bingo-game-page/bingo-game-page.component';
import { BingoActiveGameComponent } from './bingo/bingo-active-game/bingo-active-game.component';
import {MatSelectModule} from '@angular/material/select';
import { BingoSessionStatsComponent } from './bingo/bingo-session-stats/bingo-session-stats.component';
import { BingoSessionSlotComponent } from './bingo/bingo-session-slot/bingo-session-slot.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatGridListModule} from '@angular/material/grid-list';
import { BingoActiveGameStatsComponent } from './bingo/bingo-active-game-stats/bingo-active-game-stats.component';
import { TwitchigotchiPageComponent } from './twitchigotchi/twitchigotchi-page/twitchigotchi-page.component';
import { BingoGameService } from 'src/services/bingo-game.service';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        LoginNavMenuComponent,
        BingoCardListViewComponent,
        BingoCardDisplayComponent,
        BingoCardSlotComponent,
        NavbarTopComponent,
        BingoGamePageComponent,
        BingoActiveGameComponent,
        BingoSessionStatsComponent,
        BingoSessionSlotComponent,
        BingoActiveGameStatsComponent,
        TwitchigotchiPageComponent
    ],
    providers: [ LoginManagerService,BingoCardService, BingoGameService,
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { subscriptSizing: 'dynamic' } }],
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
        MatFormFieldModule,
        MatTabsModule,
        MatSelectModule,
        MatCheckboxModule,
        MatGridListModule,
        ReactiveFormsModule
    ]
})
export class AppModule { }


