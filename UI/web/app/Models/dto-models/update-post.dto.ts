/**
 * UpdatePostDtoModel class to represent the data for updating an existing post.
 */
export class UpdatePostDtoModel {
	postId = "";
	postTitle = "";
	postContent = "";
	postRating = 0;
	isNSFW = false;
	genreTag = "";

	constructor(
		postId: string,
		postTitle: string,
		postContent: string,
		postRating: number,
		isNSFW: boolean,
		genreTag: string,
	) {
		this.postId = postId;
		this.postTitle = postTitle;
		this.postContent = postContent;
		this.postRating = postRating;
		this.isNSFW = isNSFW;
		this.genreTag = genreTag;
	}
}
