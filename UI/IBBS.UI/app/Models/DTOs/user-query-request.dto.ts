export class UserQueryRequestDTO {
    userQuery: string = "";

    constructor(UserQuery: string) {
        this.userQuery = UserQuery;
    }
}
