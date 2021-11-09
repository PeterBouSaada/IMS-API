export class User {
    _id: string;
    username: string;
    password: string;
    salt: string;

    constructor() {
        this._id = "";
        this.username = "";
        this.password = "";
        this.salt = "";
    }
}