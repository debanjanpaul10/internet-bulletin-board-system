/**
 * UpdatePostDtoModel class to represent the data for updating an existing post.
 */
class UpdatePostDtoModel {
    /**
     * Gets or sets the post identifier.
     */
    postId = "";

    /**
     * Gets or sets the post title.
     */
    postTitle = "";

    /**
     * Gets or sets the content of the post.
     */
    postContent = "";

    /**
     * Gets or sets the post rating.
     */
    postRating = 0;

    /**
     * Gets or sets the IsNSFW flag.
     */
    isNSFW = false;

    /**
     * Gets or sets the genre tag.
     */
    genreTag = "";

    /**
     * Creates an instance of UpdatePostDtoModel.
     * @param {string} PostId The post identifier.
     * @param {string} PostTitle The post title.
     * @param {string} PostContent The content of the post.
     * @param {number} PostRating The post rating.
     * @param {boolean} IsNSFW The NSFW rating.
     * @param {string} GenreTag The genre tag.
     */
    constructor(
        PostId: string,
        PostTitle: string,
        PostContent: string,
        PostRating: number,
        IsNSFW: boolean,
        GenreTag: string
    ) {
        this.postId = PostId;
        this.postTitle = PostTitle;
        this.postContent = PostContent;
        this.postRating = PostRating;
        this.isNSFW = IsNSFW;
        this.genreTag = GenreTag;
    }
}

export default UpdatePostDtoModel;
