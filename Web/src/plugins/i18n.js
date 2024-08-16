import { createI18n } from 'vue-i18n'
import translations from './translations'

const i18n = createI18n({
  allowComposition: true, // to use with Composition API
  legacy: true, // so that VueI18n still works with Options API
  globalInjection: true, // to inject in the component
  messages: translations,
  // fallbackLocale: 'uz'
})

export default i18n
