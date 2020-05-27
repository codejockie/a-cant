import { I18N } from "aurelia-i18n"
import { bindable } from "aurelia-framework"
import { ValidationRules } from "aurelia-validation"
import { inject } from "aurelia-dependency-injection"

@inject(I18N)
export class Applicant {
  @bindable
  public applicant
  heading = "Applicant"

  constructor(private i18n: any) {
    this.i18n = i18n
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
}