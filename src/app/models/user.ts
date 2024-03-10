export class User {
    username?: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    bio?: string;
    country?: string;
  
    constructor(username?: string, password?: string, firstName?: string, lastName?: string, bio?: string, country?: string) {
      this.username = username;
      this.password = password;
      this.firstName = firstName;
      this.lastName = lastName;
      this.bio = bio;
      this.country = country;
    }
}