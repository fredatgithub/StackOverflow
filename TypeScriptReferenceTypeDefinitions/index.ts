import m = Sql.Models;

export class Index {
    public Users: Array<Sql.Models.User>;
    public User: Sql.Models.User;

    constructor() {
        this.User = new m.User();
    }
}