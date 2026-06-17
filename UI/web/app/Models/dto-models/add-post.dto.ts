/**
 * AddPostDtoModel class to represent the data for adding a post.
 */
export class AddPostDtoModel {
	postTitle = "";
	postContent = "";
	postCreatedBy = "";
	isNSFW = false;
	genreTag = "";

	constructor(
		postTitle: string,
		postContent: string,
		postCreatedBy: string,
		isNSFW: boolean,
		genreTag: string,
	) {
		this.postTitle = postTitle;
		this.postContent = postContent;
		this.postCreatedBy = postCreatedBy;
		this.isNSFW = isNSFW;
		this.genreTag = genreTag;
	}
}
