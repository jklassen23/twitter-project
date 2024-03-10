export class Post {
    postId?: string;
    username?: string;
    content?: string;
    dateTime?: string;
    

    constructor(id?: string, username?: string, content?: string, dateTime?: string) {
        this.postId = id;
        this.username = username;
        this.content = content;
        this.dateTime = dateTime;
    }
}
