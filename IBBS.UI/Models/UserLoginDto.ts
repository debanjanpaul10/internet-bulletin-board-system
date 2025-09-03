/**
 * UserLoginDtoModel class to represent the user login data.
 */
class UserLoginDtoModel {
    /** The user email. */
    userEmail: string = "";

    /** The user password. */
    userPassword: string = "";

    /**
     * Creates an instance of UserLoginDtoModel
     * @param UserEmail The user email.
     * @param UserPassword The user password.
     */
    constructor(UserEmail: string, UserPassword: string) {
        this.userEmail = UserEmail;
        this.userPassword = UserPassword;
    }
}

export default UserLoginDtoModel;
