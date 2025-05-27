import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router/index.js'
import { userdataStore } from './stores/userdata'

const app = createApp(App)

app.use(router)
app.use(createPinia());

const store = userdataStore();

app.mount('#app')

store.host = import.meta.env.VITE_API_HOST;
const params = {
    method: 'GET',
    mode: 'cors',
    credentials: 'include'
};

fetch(`${store.host}/User/authfetch`, params)
.then(res => {
    switch (res.status) {
        case 200:
            console.log('[main][authfetch] 200 OK');
            store.isLogged = true;
            break;
        case 401:
            console.log('[main][authfetch] 401 UNAUTHORIZED');
            break;
        default:
            console.log(`[main][authfetch] ${res.status}`);
    }
})
.catch(error => {
    console.log('[main][authfetch] ??? UNKNOWN');
})