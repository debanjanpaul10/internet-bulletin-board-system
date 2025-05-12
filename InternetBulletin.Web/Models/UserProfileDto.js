/**
 * UserProfileDtoModel class to represent the user profile dto model.
 */
class UserProfileDtoModel {
	/**
	 * Creates an instance of UserProfileDtoModel
	 * @param {number} userId The user id.
	 * @param {string} userEmail The user email.
	 * @param {string} userAlias The user alias.
	 * @param {string} userPassword The user password.
	 * @param {Array<Object>} userPosts The user posts.
	 */
	constructor(userId, userEmail, userAlias, userPassword, userPosts) {
		this.userId = userId;
		this.userEmail = userEmail;
		this.userAlias = userAlias;
		this.userPassword = userPassword;
		this.userPosts = userPosts;
	}
}

export default UserProfileDtoModel;
