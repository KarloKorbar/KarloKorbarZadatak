import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public forecasts: Person[] = [];
  public personUrl: string = "";
  public person_http: HttpClient;
  public valid: boolean = true;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.personUrl = baseUrl + 'person';
    this.person_http = http;

  }
  get() {
    this.person_http.get<Person[]>(this.personUrl).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  post() {
    this.person_http.post<any>(this.personUrl, this.forecasts).subscribe();
    console.log("got through post method");
  }

  validate(input: string):boolean {
    const regex = new RegExp('^[0-9]*$');
    let res = regex.test(input);
    return res;
  }
}

interface Person {
  ime: string;
  prezime: string;
  grad: string;
  postanskiBr: string;
  telefon: string;
}
