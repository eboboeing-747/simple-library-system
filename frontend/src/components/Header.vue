<script setup>
import { userdataStore } from '@/stores/userdata.js';
import { onMounted } from 'vue';
import { ref } from 'vue';

const store = userdataStore();
const isVisibleMp = ref(false);

function handleMp(event) {
    if (!isVisibleMp.value)
        return;

    let miniprofilePane = document.getElementById('miniprofile-pane');
    if (!miniprofilePane.contains(event.target))
        isVisibleMp.value = false;
}

function logout() {
    const host = import.meta.env.VITE_API_HOST;
    const params = {
        method: 'GET',
        mode: 'cors'
    }

    fetch(`${host}/User/logout`, params)
        .then(res => {
            console.log(res.status);
            this.$router.push('/login');
        })
        .catch(error => {
            console.log(error);
        })
}

onMounted(() => {
    window.addEventListener('click', handleMp, true);
});
</script>

<template>
    <header>
        <a href="/" class="action-area focusable">
            <img class="image"
                src="https://img.favpng.com/22/3/15/computer-icons-public-library-icon-design-png-favpng-f6uRzdiZz2pY5w7F5nMML571M.jpg">
            <div class="spacer"></div>
            <div>libraryName</div>
        </a>

        <div class="search-bar-miniprofile-group">
            <div class="search-bar-area">
                <input class="search-bar">
                <button class="search-icon-area">
                    <img class="image" style="width: 40px; height: 40px;"
                        src="https://img.icons8.com/ios_filled/512/FFFFFF/search--v3.png">
                </button>
            </div>

            <div class="action-area">
                <div v-if="store.isLogged">{{ `${store.firstName} ${store.lastName}` }}</div>
                <a v-else href="/login" class="action-title">log in</a>
                <div class="spacer"></div>
                <img v-on:click="isVisibleMp = !isVisibleMp" v-bind:src="store.pfpPath" class="pfp focusable">
            </div>
        </div>
    </header>

    <div v-if="isVisibleMp" class="miniprofile-pane" id="miniprofile-pane">
        <div v-if="store.isLogged">
            <button class="miniprofile-button">
                <a href="/profile" class="action-title">
                    profile
                </a>
            </button>

            <button v-on:click="logout()" class="miniprofile-button">
                <div>
                    logout
                </div>
            </button>
        </div>

        <div v-else>
            <button class="miniprofile-button">
                <a href="/login" class="action-title">
                    log in
                </a>
            </button>

            <button class="miniprofile-button">
                <a href="/register" class="action-title">
                    register
                </a>
            </button>
        </div>
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

.search-bar-miniprofile-group {
    width: 60%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
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

.search-icon-area:hover {
    cursor: pointer;
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