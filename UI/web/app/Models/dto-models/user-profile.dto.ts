/**
 * UserProfileDto is a Data Transfer Object (DTO) that represents the user's profile information, including their email address, posts, and post ratings.
 */
export class UserProfileDto {
	emailAddress: string = "";
	userPosts = [];
	userPostRatings = [];
}
