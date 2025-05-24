<script>
import { defineCustomElement } from 'vue';

export default {
    data() {
        return {
            errorDisplay: 0,
            authWrapper: 0
        }
    },
    computed: {},
    methods: {
        async register() {
            this.hideErrors();

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
                this.displayError('username can not subceed 4 characters', [usernameInput]);
                return;
            }

            if (password.length < 4) {
                this.displayError('password can not subceed 4 characters', [passwordInput]);
                return;
            }

            if (password !== verifyPassword) {
                this.displayError('passwords does not match', [passwordInput, verifyPasswordInput]);
                return;
            }

            let host = import.meta.env.VITE_API_HOST;

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
                    sex: sex,
                    userType: '3fa85f64-5717-4562-b3fc-2c963f66afa6'
                })
            };

            try {
                let res = await fetch(`${host}/User/register`, params);
                this.$router.push('/');
            } catch (error) {
                this.displayError('failed to rich the server', [this.authWrapper]);
            }
        },
        displayError(errorMessage, elements) {
            elements.forEach(element => {
                element.classList.add('error');
            });

            this.errorDisplay.textContent = errorMessage;
            this.errorDisplay.classList.add('error');
        },
        hideErrors() {
            this.errorDisplay.textContent = '';
            let errors = document.querySelectorAll('.error');

            for (let i = 0; i < errors.length; i++)
                errors[i].classList.remove('error');
        }
    },
    mounted() {
        this.errorDisplay = document.getElementById('error-display');
        this.authWrapper = document.getElementById('auth-wrapper');
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
            <input id="verify-password" class="action-field" name="verify-password" type="password" placeholder="verify password" required>

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

<style scoped>
.page {
    background: url("https://i.postimg.cc/RhwZhw9H/wallpaperflare-com-wallpaper.jpg");
    background-size: cover;
    background-attachment: fixed;

    display: flex;
    align-items: center;
    width: 100vw;
    height: 100vh;
    flex-direction: column;
}

.spacer {
    height: 25%;
}

.auth-wrapper {
    display: flex;
    flex-direction: column;
    width: 400px;
    backdrop-filter: blur(8px);
    padding: 16px;
    border-radius: 16px;
    border: 1px solid gray;
}

.title {
    text-align: center;
}

h1 {
    margin: 20px 0px;
    font-size: 32px;
    color: white;
}

a {
    color: white;
}

a:hover {
    background: transparent;
    text-decoration: underline;
    cursor: pointer;
}

input {
    border-radius: 1000px;
    border: 2px solid white;
    padding: 10px 16px;
    margin: 16px 8px;
    background: transparent;
    color: white;

    font-size: 24px;
}

.sex-picker {
    display: flex;
    justify-content: center;
}

button {
    border-radius: 1000px;
    border: 2px solid white;
    padding: 10px 16px;
    margin: 16px 8px;
    background: white;
    color: black;

    font-size: 24px;
}

button:hover {
    background: transparent;
    color: white;
}

button:active {
    background: white;
    color: black;
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

.error {
    border-color: red;
}

.error-display {
    color: red;
    height: 30px;
    text-align: center;
}
</style>