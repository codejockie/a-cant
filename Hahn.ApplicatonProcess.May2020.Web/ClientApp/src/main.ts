import { PLATFORM } from "aurelia-pal"
import Backend from "i18next-xhr-backend"
import { Aurelia } from "aurelia-framework"
import { TCustomAttribute } from "aurelia-i18n"
import * as environment from "../config/environment.json"
import "bootstrap/dist/css/bootstrap.css"

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName("resources/index"))
    .plugin(PLATFORM.moduleName("aurelia-i18n"), (instance) => {
      let aliases = ["t", "i18n"]
      TCustomAttribute.configureAliases(aliases)
      instance.i18next.use(Backend)
      return instance.setup({
        backend: {
          loadPath: "./locales/{{lng}}/{{ns}}.json",
        },
        attributes: aliases,
        lng: "en-GB",
        fallbackLng: "de-DE",
        debug: false,
        skipTranslationOnMissingKey: true,
      })
    })
    .plugin(PLATFORM.moduleName("aurelia-validation"))
    .plugin(PLATFORM.moduleName("aurelia-dialog"))

  aurelia.use.developmentLogging(environment.debug ? "debug" : "warn")

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName("aurelia-testing"))
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName("app")))
}
