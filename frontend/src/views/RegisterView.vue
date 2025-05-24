<script>
import { ErrorHandler } from '@/helpers/errorHandles';

export default {
    data() {
        return {
            errorHandler: 0,
            authWrapper: 0
        }
    },
    computed: {},
    methods: {
        async register() {
            this.errorHandler.hideErrors();

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
                this.errorHandler.displayError('username can not subceed 4 characters', [usernameInput]);
                return;
            }

            if (password.length < 8) {
                this.errorHandler.displayError('password can not subceed 8 characters', [passwordInput]);
                return;
            }

            if (password !== verifyPassword) {
                this.errorHandler.displayError('passwords does not match', [passwordInput, verifyPasswordInput]);
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
                this.errorHandler.displayError('failed to rich the server', [this.authWrapper]);
            }
        }
    },
    mounted() {
        let errorDisplay = document.getElementById('error-display');
        this.authWrapper = document.getElementById('auth-wrapper');
        this.errorHandler = new ErrorHandler(errorDisplay);
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

<style src="../assets/form.css" scoped>
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