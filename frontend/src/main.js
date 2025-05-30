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

try {
    const res = await fetch(`${store.host}/User/authorize`, params);
    switch (res.status) {
        case 200:
            break;
        case 401:
            console.log('[main][authfetch] 401 UNAUTHORIZED');
            throw new Error();
        default:
            console.log(`[main][authfetch] ${res.status}`);
            throw new Error();
    }

    const body = await res.json();
    store.isLogged = true;
    store.setUserData(body);
} catch (error) {
    console.error(error.message);
}