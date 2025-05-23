import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index.js'

const app = createApp(App)

app.use(router)

app.mount('#app')

const params = {
    method: 'GET',
    mode: 'cors',
    credentials: 'include',
    'Accept': '*/*'
};

let host = import.meta.env.VITE_API_HOST;
let res = await fetch(`${host}/User/authfetch`, params);
if (!res.ok)
    router.push('/auth');