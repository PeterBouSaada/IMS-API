export class User {
    username: string;
    password: string;
    salt: string;

    constructor() {
        this.username = "";
        this.password = "";
        this.salt = "";
    }
}