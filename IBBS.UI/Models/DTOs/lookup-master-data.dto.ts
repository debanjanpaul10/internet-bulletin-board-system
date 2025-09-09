export class LookupMasterDTO {
	id: number = 0;
	type: string = "";
	keyName: string = "";
	keyValue: string = "";

	constructor(Id: number, Type: string, KeyName: string, KeyValue: string) {
		this.id = Id;
		this.type = Type;
		this.keyName = KeyName;
		this.keyValue = KeyValue;
	}
}
