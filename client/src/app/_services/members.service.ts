import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  memebers: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    //return this.http.get<Member[]>(this.baseUrl + "users", this.getHttpOptions());

    if(this.memebers.length > 0) return of(this.memebers);
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(
      map(members =>{
        this.memebers = members;
        return this.memebers;
      })
    );
  }

  getMember(username: string) {
    //return this.http.get<Member>(this.baseUrl + "users/" + username, this.getHttpOptions());

    const member = this.memebers.find(x => x.userName === username);
    if (member) return of(member);
    return this.http.get<Member>(this.baseUrl + "users/" + username);
  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(()=>{
        const index = this.memebers.indexOf(member);
        this.memebers[index] = {...this.memebers[index], ...member}
      })
    );
  }

  // getHttpOptions() {
  //   const userString = localStorage.getItem("user");
  //   if (!userString) return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: "Bearer " + user.token
  //     })
  //   }
  // }
}
