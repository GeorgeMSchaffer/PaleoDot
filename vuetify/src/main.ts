/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'
import Primevue from 'primevue/config'
// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'
import Aura from '@primevue/themes/aura';

const app = createApp(App)

registerPlugins(app)
app.use(Primevue, { 
  ripple: true,
  theme: {
    preset: Aura,
    options: {
    prefix: 'p',
    darkModeSelector: 'system',
    cssLayer: false
}
}
})
app.provide('$primevue', app.config.globalProperties.$primevue)

app.mount('#app')
