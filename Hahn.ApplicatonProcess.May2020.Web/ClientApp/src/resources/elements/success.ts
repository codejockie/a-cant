import { Router } from "aurelia-router"
import { inject, NewInstance } from "aurelia-framework"
import { Http } from "services/http"
import { Applicant } from "resources/models/Applicant"

@inject(NewInstance.of(Http), Router)
export class Success {
  applicant: any
  translationKeys: string[] = []

  constructor(private httpClient: Http, private router: Router) {
    this.httpClient = httpClient
    this.router = router
  }

  canActivate(params) {
    if (!params.id) {
      return this.router.navigateToRoute("")
    }
  }

  activate(params) {
    this.httpClient.getApplicant(params.id).then((response) => {
      if (response.status >= 400) {
        return this.router.navigateToRoute("")
      }
      this.applicant = response as Applicant
      this.translationKeys = Object.keys(response as Applicant)
    })
  }
}
