import { createApp } from 'vue'
import App from './App.vue'
import GAuth from 'vue3-google-oauth2'
import router from "./router"


const gauthOption = {
    clientId: '794246981306-m7rn1ev5b02nc7dl0r8bpurti8arc133.apps.googleusercontent.com',
    scope: 'profile email',
    prompt: 'consent',
    fetch_basic_profile: true
}

createApp(App).use(router).use(GAuth, gauthOption).mount('#app')

