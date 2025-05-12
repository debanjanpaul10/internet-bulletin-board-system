/**
 * UserLoginDtoModel class to represent the user login data.
 */
class UserLoginDtoModel {
	/**
	 * Creates an instance of UserLoginDtoModel
	 * @param {string} userEmail The user email.
	 * @param {string} userPassword The user password.
	 */
	constructor(userEmail, userPassword) {
		this.userEmail = userEmail;
		this.userPassword = userPassword;
	}
}

export default UserLoginDtoModel;
