<script setup>
import { ErrorHandler } from '@/helpers/errorHandler';
import { userdataStore } from '@/stores/userdata';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const store = userdataStore();
const router = useRouter();

let errorHandler = null;
let authWrapper = null;

onMounted(() => {
    authWrapper = document.getElementById('auth-wrapper');
    const errorDisplay = document.getElementById('error-display');
    errorHandler = new ErrorHandler(errorDisplay);
});

async function register() {
    errorHandler.hideErrors();

    let usernameInput = document.getElementById('username');
    let username = usernameInput.value;
    let passwordInput = document.getElementById('password');
    let password = passwordInput.value;
    let verifyPasswordInput = document.getElementById('verify-password');
    let verifyPassword = verifyPasswordInput.value;
    let firstName = document.getElementById('first-name').value;
    let lastName = document.getElementById('last-name').value;
    let sex = document.getElementById('male').checked;

    if (username.length < 4) {
        errorHandler.displayError('username can not subceed 4 characters', [usernameInput]);
        return;
    }

    if (password.length < 4) {
        errorHandler.displayError('password can not subceed 4 characters', [passwordInput]);
        return;
    }

    if (password !== verifyPassword) {
        errorHandler.displayError('passwords does not match', [passwordInput, verifyPasswordInput]);
        return;
    }

    let params = {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login: username,
            password: password,
            firstName: firstName,
            lastName: lastName,
            sex: sex
        })
    };
    
    try {
        let res = await fetch(`${store.host}/User/register`, params);

        console.log(res.status, res.ok);

        switch (res.status) {
            case 201:
                router.push('/');
                break;
            case 409:
                errorHandler.displayError('user with such login already exists', [authWrapper]);
                break;
            default:
                let body = res.json();
                errorHandler.displayError(body.error, [authWrapper]);
        }
    } catch (error) {
        errorHandler.displayError('unexpected error occured', [authWrapper]);
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <form v-on:submit.prevent="register" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">register</h1>

            <input id="username" class="action-field" name="username" type="text" placeholder="username" required>
            <input id="first-name" class="action-field" name="first-name" type="text" placeholder="first name" required>
            <input id="last-name" class="action-field" name="last-name" type="text" placeholder="last name" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>
            <input id="verify-password" class="action-field" name="verify-password" type="password"
                placeholder="verify password" required>

            <div class="sex-picker">
                <input id="male" type="radio" name="sex" checked>
                <label for="male">male</label>
                <input id="female" type="radio" name="sex">
                <label for="female">female</label>
            </div>

            <button type="submit" class="action-field">register</button>

            <p id="error-display" class="error-display"></p>
        </form>
    </div>
</template>

<style src="../assets/form.css" scoped>
</style>

<style scoped>
.sex-picker {
    display: flex;
    justify-content: center;
}

input[type="radio"] {
    accent-color: white;
    background: white;
    width: 15px;
    height: 15px;
}

label {
    font-size: 24px;
    color: white;
}
</style>