import { I18N } from "aurelia-i18n"
import { DialogService } from "aurelia-dialog"
import { inject, NewInstance } from "aurelia-dependency-injection"
import { ControllerValidateResult, ValidationController, ValidationRules } from "aurelia-validation"
import { Prompt } from "components/modal"
import { BootstrapFormRenderer } from "resources/validation/bootstrapFormRenderer"

@inject(I18N, DialogService, NewInstance.of(ValidationController))
export class Applicant {
  heading = "Applicant Form"
  applicant = {
    address: "",
    age: "",
    countryOfOrigin: "",
    emailAddress: "",
    familyName: "",
    hired: "",
    name: "",
  }

  constructor(
    private i18n: any,
    private dialogService,
    private controller: ValidationController,
  ) {
    this.i18n = i18n
    this.dialogService = dialogService
    this.controller.addRenderer(new BootstrapFormRenderer())
  }

  public bind() {
    const t = (key) => this.i18n.tr(key)

    ValidationRules
      .ensure("address").required().withMessage(t("errors.address"))
      .ensure("age").between(19, 61).withMessage(t("errors.invalidAge"))
      .ensure("countryOfOrigin").required().withMessage(t("errors.countryOfOrigin"))
      .ensure("emailAddress").required().withMessage(t("errors.email")).email().withMessage(t("errors.invalidEmail"))
      .ensure("familyName").required().withMessage(t("errors.familyName"))
      .ensure("name").required().withMessage(t("errors.name"))
      .on(this.applicant)
  }

  get isValid() {
    const { age } = this.applicant
    const isValidAge = +age >= 20 && +age < 61

    if (
      this.applicant.address &&
      isValidAge &&
      this.applicant.countryOfOrigin &&
      this.applicant.emailAddress &&
      this.applicant.familyName &&
      this.applicant.name
    ) {
      return false
    }

    return true
  }

  get disableReset() {
    if (
      this.applicant.address ||
      this.applicant.age ||
      this.applicant.countryOfOrigin ||
      this.applicant.emailAddress ||
      this.applicant.familyName ||
      this.applicant.name
    ) {
      return false
    }

    return true
  }

  reset() {
    this.dialogService
      .open({ viewModel: Prompt })
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