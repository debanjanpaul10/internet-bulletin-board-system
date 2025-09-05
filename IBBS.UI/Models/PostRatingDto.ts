/**
 * PostRatingDtoModel class to represent the post rating dto.
 */
class PostRatingDtoModel {
    /**
     * Gets or sets the post id.
     */
    postId: string = "";

    /**
     * Gets or sets the incremented or decremented value
     */
    isIncrement: boolean = false;

    /**
     * Creates an instance of PostRatingDtoModel
     * @param {string} PostId The post id.
     * @param {boolean} IsIncrement The incremented or decremented value.
     */
    constructor(PostId: string, IsIncrement: boolean) {
        this.postId = PostId;
        this.isIncrement = IsIncrement;
    }
}

export default PostRatingDtoModel;
