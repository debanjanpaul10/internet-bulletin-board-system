/**
 * UserProfileDtoModel class to represent the user profile dto model.
 */
class UserProfileDtoModel {
    /**
     * Gets or sets the user identifier.
     */
    userName: string = "";

    /**
     * Gets or sets the user posts.
     */
    userPosts: Array<Object> = [];

    /**
     * Creates an instance of UserProfileDtoModel
     * @param UserName The user identifier.
     * @param UserPosts The user posts.
     */
    constructor(UserName: string, UserPosts: Array<Object>) {
        this.userName = UserName;
        this.userPosts = UserPosts;
    }
}

export default UserProfileDtoModel;
