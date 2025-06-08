import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router/index.js'
import { userdataStore } from './stores/userdata'
import { libraryStore } from './stores/librarydata'

const app = createApp(App)

app.use(router)
app.use(createPinia());

const userstore = userdataStore();
const libstore = libraryStore();

userstore.host = import.meta.env.VITE_API_HOST;
const params = {
    method: 'GET',
    mode: 'cors',
    credentials: 'include'
};

try {
    const res = await fetch(`${userstore.host}/User/authorize`, params);
    switch (res.status) {
        case 200:
            break;
        case 401:
            console.log('[main][authfetch] 401 UNAUTHORIZED');
            throw new Error('failed to log in');
        default:
            console.log(`[main][authfetch] ${res.status}`);
            throw new Error('failed to log in');
    }

    const body = await res.json();
    userstore.isLogged = true;
    userstore.setUserData(body);
} catch (error) {
    console.error(error.message);
}

app.mount('#app')

try {
    const res = await fetch(`${userstore.host}/Library/get`, {method: 'GET', mode: 'cors'});
    const body = await res.json();
    libstore.setLibData(body);
} catch(error) {
    console.error(error);
}