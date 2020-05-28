import { inject } from "aurelia-framework"
import { DialogController } from "aurelia-dialog"

@inject(DialogController)
export class Error {
  errors: string[]

  constructor(private controller) {
    this.controller = controller
    controller.settings.centerHorizontalOnly = true
  }

  activate(errors) {
    this.errors = Object.keys(errors)
  }
}
