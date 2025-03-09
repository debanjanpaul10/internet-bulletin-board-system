/**
 * AddPostDtoModel class to represent the data for adding a post.
 */
class AddPostDtoModel {
	/**
	 * Creates an instance of AddPostDtoModel.
	 * @param {string} postTitle - The title of the post.
	 * @param {string} postContent - The content of the post.
	 * @param {string} postCreatedBy - The user who created the post.
	 */
	constructor(postTitle, postContent, postCreatedBy) {
		this.postTitle = postTitle;
		this.postContent = postContent;
		this.postCreatedBy = postCreatedBy;
	}
}

export default AddPostDtoModel;
