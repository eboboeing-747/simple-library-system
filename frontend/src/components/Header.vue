<script>
export default {
    data() {
        return {
            isLogged: false,
            isVisibleMp: false,
            pfp: '../../public/empty-pfp.png'
        }
    },
    computed: {},
    methods: {
        handleMp(event) {
            if (!this.isVisibleMp)
                return;

            let miniprofilePane = document.getElementById('miniprofile-pane');
            if (!miniprofilePane.contains(event.target))
                this.isVisibleMp = false;
        },
        logout() {
            const host = import.meta.env.VITE_API_HOST;
            const params = {
                method: 'GET',
                mode: 'cors'
            }

            fetch (`${host}/User/logout`, params)
            .then(res => {
                this.$router.push('/login');
            })
            .catch(error => {
                console.log(error);
            })
        }
    },
    mounted() {
        this.isLogged = localStorage.getItem('logged') == true;
        window.addEventListener('click', this.handleMp, true);

        if (this.isLogged)
            this.pfp = 'https://media1.tenor.com/m/jfJjRLN19CwAAAAd/9impulse-impulse.gif';
    }
};
</script>

<template>
    <header>
        <a href="/" class="action-area focusable">
            <img class="image" src="https://img.favpng.com/22/3/15/computer-icons-public-library-icon-design-png-favpng-f6uRzdiZz2pY5w7F5nMML571M.jpg">
            <div class="spacer"></div>
            <div>libraryName</div>
        </a>

        <div class="search-bar-area">
            <input class="search-bar">
            <button class="search-icon-area">
                <img class="image" style="width: 40px; height: 40px;" src="https://img.icons8.com/ios_filled/512/FFFFFF/search--v3.png">
            </button>
        </div>

        <div class="action-area">
            <div v-if="this.isLogged">UserName</div>
            <a v-else href="/login" class="action-title">login</a>
            <div class="spacer"></div>
            <img v-on:click="this.isVisibleMp = !this.isVisibleMp" v-bind:src="this.pfp" class="pfp focusable">
        </div>

        <!--
            <div v-if="this.isLogged" v-click-outside class="action-area">
                <div>UserName</div>
                <div class="spacer"></div>
                <img v-on:click="this.isVisibleMp = !this.isVisibleMp" class="pfp focusable" src="https://media1.tenor.com/m/jfJjRLN19CwAAAAd/9impulse-impulse.gif">
            </div>

            <a v-else href="/login" class="action-area focusable">
                <div>login</div>
                <div class="spacer"></div>
                <img class="pfp" src="../../public/empty-pfp.png">
            </a>
        -->
    </header>

    <div v-if="this.isVisibleMp" class="miniprofile-pane" id="miniprofile-pane">
        <button v-if="this.isLogged" v-on:click="this.logout()" class="miniprofile-button">
            <div>
                logout
            </div>
        </button>
        <button v-else class="miniprofile-button">
            <a href="/login" class="action-title">
                login
            </a>
        </button>
        <button v-if="!this.isLogged" class="miniprofile-button">
            <a href="/register" class="action-title">
                register
            </a>
        </button>
    </div>
</template>

<style scoped>
header {
    top: 0;
    display: flex;
    flex-direction: row;
    position: sticky;
    padding: 10px;
    justify-content: space-between;
    align-items: center;
    background-color: var(--color-background);
}

.action-area {
    display: flex;
    flex-direction: row;
    align-items: center;
    border-radius: 16px;
    padding: 8px 16px;
    text-decoration: none;
    color: white;
}

.action-title {
    color: white;
    text-decoration: none;
}

.spacer {
    width: 20px;
}

.search-bar-area {
    width: min(40%, 600px);
    display: flex;
    flex-direction: row;
    height: 50px;
}

.search-bar {
    width: 100%;
    border: 2px solid grey;
    border-radius: 30px 0px 0px 30px;
    background-color: transparent;
    padding-left: 30px;
    font-size: 150%;
    color: white;
}

.search-icon-area {
    min-width: 70px;
    width: 70px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-style: solid;
    border-width: 2px 2px 2px 0px;
    border-color: gray;
    border-radius: 0px 30px 30px 0px;
    background-color: transparent;
}

.focusable {
    cursor: pointer;
}

.focusable:hover {
    background-color: rgba(0, 0, 0, 0.5);
}

.image {
    display: block;
    max-width: 100%;
    max-height: 100%;
    width: auto;
    height: 50px;
    border-radius: 16px;
}

.pfp {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    border: 1px solid grey;
}

.miniprofile-pane {
    display: flex;
    flex-direction: column;
    position: absolute;
    top: 80px;
    right: 26px;
    background-color: rgba(0, 0, 0, 0.5);
    border-radius: 8px;
}

.miniprofile-button {
    display: flex;
    justify-content: space-between;
    font-size: 16px;
    background: transparent;
    border-width: 0px;
    padding: 5px 16px;
    color: white;
    margin: 8px;
}
</style>