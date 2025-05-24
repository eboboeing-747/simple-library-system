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
        async login() {
            this.errorHandler.hideErrors();

            let host = import.meta.env.VITE_API_HOST;

            let usernameInput = document.getElementById('username');
            let username = usernameInput.value;
            let passwordInput = document.getElementById('password');
            let password = passwordInput.value;

            if (username.length < 4) {
                this.errorHandler.displayError('username can not subceed 4 characters', [usernameInput]);
                return;
            }

            if (password.length < 4) {
                this.errorHandler.displayError('password can not subceed 4 characters', [passwordInput]);
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
                    password: password
                })
            }

            fetch(`${host}/User/login`, params)
            .then(res => {
                switch (res.status) {
                    case 200:
                        this.$router.push('/');
                        return;
                    case 401:
                        this.errorHandler.displayError('login or password is incorrect', [usernameInput, passwordInput]);
                        return;
                }
            })
            .catch(error => {
                this.errorHandler.displayError('failed to rich the server', [this.authWrapper]);
            });
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

        <form v-on:submit.prevent="login" id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">login</h1>

            <input id="username" class="action-field" name="username" type="text" placeholder="username" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>

            <button class="action-field">login</button>

            <a class="title" href="/register">dont have an account? register</a>
            <p id="error-display" class="error-display"></p>
        </form>
    </div>
</template>

<style src="../assets/form.css" scoped>
</style>