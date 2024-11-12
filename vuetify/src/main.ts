import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
loadFonts()

createApp(App)
  .use(router)
  .use(vuetify)
  .use(createPinia())
  .use(PrimeVue, {
    theme: {
      preset: Aura,
    },
  }).mount('#app')
