import {
  ValidationController,
  ControllerValidateResult,
} from "aurelia-validation"
import { I18N } from "aurelia-i18n"
import { DialogService } from "aurelia-dialog"
import { inject, NewInstance } from "aurelia-dependency-injection"
import { Prompt } from "components/modal"
import { BootstrapFormRenderer } from "resources/validation/validation-renderer"

@inject(NewInstance.of(ValidationController), I18N, DialogService)
export class App {
  languages = []
  locale: string
  public applicant = {
    address: "",
    age: "",
    countryOfOrigin: "",
    emailAddress: "",
    familyName: "",
    hired: "",
    name: "",
  }

  constructor(
    private controller: ValidationController,
    private i18n: any,
    private dialogService
  ) {
    this.i18n = i18n
    this.dialogService = dialogService
    this.controller.addRenderer(new BootstrapFormRenderer())
  }

  activate() {
    const locale = localStorage.getItem("locale")
    if (locale) {
      this.locale = locale
      this.i18n.setLocale(locale)
      this.populateLanguages()
    }
  }

  changeLanguage(event: any) {
    const { value } = event.target
    this.locale = value
    this.i18n.setLocale(value)
    localStorage.setItem("locale", value)
  }

  populateLanguages() {
    this.languages = [
      { key: "locales.en", lang: "English", locale: "en-GB" },
      { key: "locales.de", lang: "German", locale: "de-DE" },
      { key: "locales.ja", lang: "Japanese", locale: "ja-JP" },
    ]
  }

  get isValid() {
    const { age } = this.applicant
    const validAge = age ? +age >= 20 && +age < 61 : false

    if (
      !this.applicant.address ||
      !validAge ||
      !this.applicant.countryOfOrigin ||
      !this.applicant.emailAddress ||
      !this.applicant.familyName ||
      !this.applicant.name
    ) {
      return false
    }
    return this.controller.errors.length < 1
  }

  get disableReset() {
    if (
      !this.applicant.address &&
      !this.applicant.age &&
      !this.applicant.countryOfOrigin &&
      !this.applicant.emailAddress &&
      !this.applicant.familyName &&
      !this.applicant.name
    ) {
      return true
    }

    return false
  }

  reset() {
    this.dialogService
      .open({
        viewModel: Prompt
      })
      .whenClosed((response) => {
        if (!response.wasCancelled) {
          this.applicant.address = ""
          this.applicant.age = ""
          this.applicant.countryOfOrigin = ""
          this.applicant.emailAddress = ""
          this.applicant.familyName = ""
          this.applicant.hired = ""
          this.applicant.name = ""
          this.controller.reset()
        }
      })
  }

  send() {
    this.controller.validate().then((result: ControllerValidateResult) => {
      if (result.valid) {
        console.log("Validation successful!")
      } else {
        console.log("Validation failed!")
      }
    })
  }
}
