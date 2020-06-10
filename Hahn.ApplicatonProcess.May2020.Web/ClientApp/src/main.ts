import { PLATFORM } from "aurelia-pal"
import Backend from "i18next-xhr-backend"
import { AppRouter } from "aurelia-router"
import { Aurelia } from "aurelia-framework"
import { TCustomAttribute } from "aurelia-i18n"
import { EventAggregator } from "aurelia-event-aggregator"
import * as environment from "../config/environment.json"

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName("resources/index"))
    .plugin(PLATFORM.moduleName("aurelia-i18n"), (i18n) => {
      let aliases = ["t", "i18n"]
      TCustomAttribute.configureAliases(aliases)
      i18n.i18next.use(Backend)

      return i18n
        .setup({
          backend: {
            loadPath: "./locales/{{lng}}/{{ns}}.json",
          },
          attributes: aliases,
          lng: "en-GB",
          fallbackLng: "de-DE",
          debug: false,
          skipTranslationOnMissingKey: true,
        })
        .then(() => {
          const router = aurelia.container.get(AppRouter)
          router.transformTitle = (title) => i18n.tr(title)

          const eventAggregator = aurelia.container.get(EventAggregator)
          eventAggregator.subscribe("i18n:locale:changed", () => {
            router.updateTitle()
          })
        })
    })
    .plugin(PLATFORM.moduleName("aurelia-validation"))
    .plugin(PLATFORM.moduleName("aurelia-dialog"), (config) => {
      config.useDefaults()
      config.useCSS("")
    })

  aurelia.use.developmentLogging(environment.debug ? "debug" : "warn")

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName("aurelia-testing"))
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName("app")))
}
