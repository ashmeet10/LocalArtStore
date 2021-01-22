import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router"

@Component({
    selector: "login",
    templateUrl: "login.component.html"
})

export class Login {

    constructor(private data: DataService, private router: Router) { }

    errMessage: string = "";

    public creds = {
        username: "",
        password: ""
    };

    onLogin() {
        //call login service
      //  debugger;
        //alert(this.creds.username);
        //this.creds.username += "!";
        this.data.login(this.creds)
            .subscribe(success => {
                if (this.data.order.items.length == 0) {
                    this.router.navigate(["/"]);
                } else {
                    this.router.navigate(["checkout"]);
                }
            }, error => this.errMessage = "Failed to login");
    }
}

