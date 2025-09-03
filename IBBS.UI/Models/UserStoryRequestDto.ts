/**
 * UserStoryRequestDtoModel class to represent the ai request model.
 */
class UserStoryRequestDtoModel {
    /**
     * The story.
     */
    story = "";

    /**
     * Creates an instance of UserStoryRequestDtoModel
     * @param {string} Story The story.
     */
    constructor(Story: string) {
        this.story = Story;
    }
}

export default UserStoryRequestDtoModel;
