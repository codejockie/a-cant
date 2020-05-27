import { RenderInstruction, ValidateResult } from "aurelia-validation"

export class BootstrapFormRenderer {
  render(instruction: RenderInstruction) {
    for (let { result, elements } of instruction.unrender) {
      for (let element of elements) {
        this.remove(element, result)
      }
    }

    for (let { result, elements } of instruction.render) {
      for (let element of elements) {
        this.add(element, result)
      }
    }
  }

  add(element: Element, result: ValidateResult) {
    const inputContainer = element.closest("div")
    if (!inputContainer) {
      return
    }

    if (!result.valid) {
      // add the has-error class to the enclosing form-group div
      element.classList.remove("is-valid")
      element.classList.add("is-invalid")

      // add help-block
      const message = document.createElement("span")
      message.className = "help-block invalid-feedback" // 'help-block validation-message';
      message.textContent = result.message
      message.id = `validation-message-${result.id}`
      inputContainer.appendChild(message)
    }
    // else {
    //     if (!element.classList.contains('is-invalid')) {
    //       element.classList.add('is-valid');
    //     }
    // }
  }

  remove(element: Element, result: ValidateResult) {
    const inputContainer = element.closest("div")
    if (!inputContainer) {
      return
    }

    if (result.valid) {
      if (element.classList.contains("is-invalid")) {
        element.classList.remove("is-invalid")
      }
    } else {
      // remove help-block
      const message = inputContainer.querySelector(
        `#validation-message-${result.id}`
      )
      if (message) {
        inputContainer.removeChild(message)

        // remove the has-error class from the enclosing form-group div
        if (
          inputContainer.querySelectorAll(".help-block.invalid-feedback")
            .length === 0
        ) {
          inputContainer.classList.remove("is-invalid")
        }
      }
    }
  }
}
