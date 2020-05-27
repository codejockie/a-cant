import {
  ValidationController,
  ControllerValidateResult,
} from "aurelia-validation"
import { I18N } from "aurelia-i18n"
import { DialogService } from "aurelia-dialog"
import { inject, NewInstance } from "aurelia-dependency-injection"
import { Prompt } from "components/modal"
import { Applicant } from "resources/models/Applicant"
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
    private validationController: ValidationController,
    private i18n: any,
    private dialogService
  ) {
    this.i18n = i18n
    this.dialogService = dialogService
    this.validationController.addRenderer(new BootstrapFormRenderer())
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
    this.populateLanguages()
  }

  populateLanguages() {
    this.languages = [
      { locale: "en-GB", lang: this.i18n.tr("locales.en") },
      { locale: "de-DE", lang: this.i18n.tr("locales.de") },
      { locale: "ja-JP", lang: this.i18n.tr("locales.ja") },
    ]
  }

  get isValid() {
    const {
      address,
      age,
      countryOfOrigin,
      emailAddress,
      familyName,
      name,
    } = this.applicant

    const validAge = age ? +age >= 20 && +age < 61 : false

    if (
      !address ||
      !validAge ||
      !countryOfOrigin ||
      !emailAddress ||
      !familyName ||
      !name
    ) {
      return false
    }
    return this.validationController.errors.length < 1
  }

  get disableReset() {
    const {
      address,
      age,
      countryOfOrigin,
      emailAddress,
      familyName,
      name,
    } = this.applicant

    const validAge = age ? +age >= 20 && +age < 61 : false

    if (
      !address &&
      !validAge &&
      !countryOfOrigin &&
      !emailAddress &&
      !familyName &&
      !name
    ) {
      return true
    }

    return false
  }

  reset() {
    this.dialogService
      .open({
        viewModel: Prompt,
        model: this.i18n.tr("dialogs.resetMessage"),
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
          this.validationController.reset()
        }
      })
  }

  send() {
    const {
      address,
      age,
      countryOfOrigin,
      emailAddress,
      familyName,
      hired,
      name,
    } = this.applicant

    const applicant: Applicant = {
      address,
      age: +age,
      countryOfOrigin,
      emailAddress,
      familyName,
      hired: Boolean(hired),
      name,
    }

    this.validationController
      .validate()
      .then((result: ControllerValidateResult) => {
        if (result.valid) {
          console.log("Validation successful!")
        } else {
          console.log("Validation failed!")
        }
      })
  }
}
