import { I18N } from "aurelia-i18n"
import { PLATFORM } from "aurelia-framework"
import { inject } from "aurelia-dependency-injection"
import { RouterConfiguration, Router } from "aurelia-router"

@inject(I18N)
export class App {
  languages = []
  locale: string
  router: Router
  showDropdown = false
  localeIdentifier: string

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
      this.setLocale(locale)
    }
    this.populateLanguages()
    this.setLocaleIdentifier(this.locale)
  }

  selectLanguage(locale: string) {
    if (this.locale == locale) return
    this.setLocale(locale)
    this.setLocaleIdentifier(locale)
    localStorage.setItem("locale", locale)
  }

  populateLanguages() {
    this.languages = [
      { key: "locales.en", lang: "English", locale: "en-GB", identifier: "EN" },
      { key: "locales.de", lang: "German", locale: "de-DE", identifier: "DE" },
      { key: "locales.ja", lang: "Japanese", locale: "ja-JP", identifier: "JA" },
    ]
  }

  setLocale(locale: string) {
    this.locale = locale
    this.i18n.setLocale(locale)
  }

  setLocaleIdentifier(locale: string = "en-GB") {
    this.localeIdentifier = this.languages.find(l => l.locale == locale)?.identifier
  }

  toggle() {
    this.showDropdown = !this.showDropdown
  }
}
