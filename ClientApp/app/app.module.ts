import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductList } from "./shop/productList.component";
import { DataService } from "./shared/dataService";
import { Cart } from "./shop/cart.component"

import { APP_BASE_HREF } from '@angular/common';

@NgModule({
  declarations: [
        AppComponent,
        ProductList,
        Cart
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
    HttpClientModule
  ],
    providers: [{ provide: APP_BASE_HREF, useValue: '/App/Shop' },DataService] ,
    //providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
