export class UserProfileDto {
	emailAddress: string = "";
	userPosts = [];
	userPostRatings = [];

	constructor(
		EmailAddress: string = "",
		UserPosts: [] = [],
		UserPostRatings: [] = []
	) {
		this.emailAddress = EmailAddress;
		this.userPosts = UserPosts;
		this.userPostRatings = UserPostRatings;
	}
}
