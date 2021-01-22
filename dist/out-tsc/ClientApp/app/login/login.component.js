import { __decorate } from "tslib";
import { Component } from "@angular/core";
let Login = class Login {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.errMessage = "";
        this.creds = {
            username: "",
            password: ""
        };
    }
    onLogin() {
        //call login service
        //  debugger;
        //alert(this.creds.username);
        //this.creds.username += "!";
        this.data.login(this.creds)
            .subscribe(success => {
            if (this.data.order.items.length == 0) {
                this.router.navigate(["/"]);
            }
            else {
                this.router.navigate(["checkout"]);
            }
        }, error => this.errMessage = "Failed to login");
    }
};
Login = __decorate([
    Component({
        selector: "login",
        templateUrl: "login.component.html"
    })
], Login);
export { Login };
//# sourceMappingURL=login.component.js.map