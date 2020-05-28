import "whatwg-fetch"
import { inject } from "aurelia-dependency-injection"
import { HttpClient, json } from "aurelia-fetch-client"
import { Applicant } from "resources/models/Applicant"

@inject(HttpClient)
export class Http {
  constructor(private httpClient: HttpClient) {
    this.httpClient = new HttpClient()
    this.httpClient.configure((config) => {
      config.withBaseUrl("https://localhost:6131/api/").withDefaults({
        credentials: "same-origin",
        headers: {
          Accept: "application/json",
          "X-Requested-With": "Fetch",
        },
      })
    })
  }

  createApplicant(applicant: Applicant) {
    return this.httpClient
      .fetch("applicant", {
        method: "post",
        body: json(applicant),
      })
      .then((response: Response) => response.json())
  }

  getApplicant(applicantId: number) {
    return this.httpClient
      .fetch(`applicant/${applicantId}`)
      .then((response: Response) => response.json())
  }
}
