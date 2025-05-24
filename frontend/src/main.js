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
    credentials: 'include'
};

let host = import.meta.env.VITE_API_HOST;
/*
let res = await fetch(`${host}/User/authfetch`, params);
if (!res.ok)
    router.push('/login');
*/

fetch(`${host}/User/authfetch`, params)
.then(
    res => {
        switch (res.status) {
            case 200:
                console.log('[main][authfetch] 200 OK');
                break;
            case 401:
                console.log('[main][authfetch] 401 UNAUTHORIZED');
                router.push('/login');
                break;
            default:
                console.log(`[main][authfetch] ${res.status}`);
        }
    }
)
.catch(error => {
    console.log('[main][authfetch] ??? UNKNOWN');
})