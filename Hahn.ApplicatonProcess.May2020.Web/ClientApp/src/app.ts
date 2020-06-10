import { I18N } from "aurelia-i18n"
import { PLATFORM } from "aurelia-framework"
import { inject } from "aurelia-dependency-injection"
import { RouterConfiguration, Router } from "aurelia-router"

@inject(I18N)
export class App {
  languages = []
  locale: string
  router: Router

  constructor(private i18n: I18N) {
    this.i18n = i18n
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "a-cant"
    config.map([
      { route: "", name: "applicant", moduleId: "resources/elements/applicant", title: "pages.applicant" },
      { route: "success/:id?", name: "success", moduleId: PLATFORM.moduleName("resources/elements/success"), title: "pages.success" },
    ])
    config.mapUnknownRoutes(PLATFORM.moduleName("resources/elements/applicant"))
    this.router = router
  }

  activate() {
    const locale = localStorage.getItem("locale")
    if (locale) {
      this.locale = locale
      this.i18n.setLocale(locale)
    }
    this.populateLanguages()
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
}
