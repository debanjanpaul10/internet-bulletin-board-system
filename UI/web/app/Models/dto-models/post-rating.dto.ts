/**
 * PostRatingDtoModel class to represent the post rating dto.
 */
export class PostRatingDtoModel {
	postId: string = "";
	isIncrement: boolean = false;

	constructor(postId: string, isIncrement: boolean) {
		this.postId = postId;
		this.isIncrement = isIncrement;
	}
}
