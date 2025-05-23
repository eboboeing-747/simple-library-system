<script>
export default {
    data() {
        return {
            errorDisplay: 0,
            authWrapper: 0
        }
    },
    computed: {},
    methods: {
        async login() {
            this.hideErrors();

            let host = import.meta.env.VITE_API_HOST;

            let usernameInput = document.getElementById('username');
            let username = usernameInput.value;
            let passwordInput = document.getElementById('password');
            let password = passwordInput.value;

            if (username.length < 4) {
                this.displayError('username can not subceed 4 characters', usernameInput);
                return;
            }

            if (password.length < 4) {
                this.displayError('password can not subceed 4 characters', passwordInput);
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

            try {
                let res = await fetch(`${host}/User/login`, params);
                this.$router.push('/');
            } catch (error) {
                this.displayError('failed to rich the server', this.authWrapper);
            }
        },
        displayError(errorMessage, element) {
            element.classList.add('error');
            this.errorDisplay.textContent = errorMessage;
            this.errorDisplay.classList.add('error');
        },
        hideErrors() {
            let errors = document.getElementsByClassName('error');
            this.errorDisplay.textContent = '';

            for (let i = 0; i < errors.length; i++) {
                errors[i].classList.remove('error');
            }
        }
    },
    mounted() {
        this.errorDisplay = document.getElementById('error-display');
        this.authWrapper = document.getElementById('auth-wrapper');

        console.log('[AuthView] mounted call');
    }
}
</script>

<template>
    <div class="page">
        <div class="spacer"></div>

        <!--
        <form class="auth-wrapper" id="login-form">
            <h1 class="title">login</h1>

            <input id="username" class="action-field" name="username" type="text" placeholder="username" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>

            <button v-on:click="login" type="submit" class="action-field">login</button>

            <a class="title">dont have an account? Register</a>
        </form>
        -->

        <div id="auth-wrapper" class="auth-wrapper">
            <h1 class="title">login</h1>

            <input id="username" class="action-field" name="username" type="text" placeholder="username" required>
            <input id="password" class="action-field" name="password" type="password" placeholder="password" required>

            <button v-on:click="login" class="action-field">login</button>

            <a class="title">dont have an account? register</a>
            <p id="error-display" class="error-display"></p>
        </div>
    </div>
</template>

<!--
    <form class="auth-form" id="register-form">
        <label for="username" class="auth-form-text">username:</label>
        <input class="auth-form-unit" type="text" name="username" id="register-username" required>

        <label for="password" id="password" class="auth-form-text">password:</label>
        <input class="auth-form-unit" type="password" name="password" id="register-password" required>

        <label for="verify-password" id="verify-password" class="auth-form-text">verify password:</label>
        <input class="auth-form-unit" type="password" name="verify-password" id="register-verify-password" required>
        
        <button type="submit" class="submit-button" id="register-submit-button">register</button>
        <button type="reset" class="auth-form-unit">reset</button>
    </form>
-->

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
    height: 20%;
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

.error {
    border-color: red;
}

.error-display {
    color: red;
    height: 30px;
    text-align: center;
}
</style>