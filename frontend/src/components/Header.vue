<script setup>
import CreateForm from './forms/FormHandler.vue';
import { libraryStore } from '@/stores/librarydata';
import { userdataStore } from '@/stores/userdata.js';
import { onMounted } from 'vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const libstore = libraryStore();
const userstore = userdataStore();
const router = useRouter();

const emit = defineEmits(['query-update']);

const isVisibleMp = ref(false);
const isVisibleForm = ref(false);
const selectedOption = ref('book');
const query = ref(null);
const queryInput = ref(null);

function handleMp(event) {
    if (!isVisibleMp.value)
        return;

    let miniprofilePane = document.getElementById('miniprofile-pane');
    if (!miniprofilePane.contains(event.target))
        isVisibleMp.value = false;
}

function search(event = null) {
    if (event !== null) {
        if (event.key !== 'Enter')
            return;
    }

    query.value = queryInput.value.value;
    emit('query-update', query.value, selectedOption.value)
}

function select(queryOption) {
    query.value = queryInput.value.value;
    selectedOption.value = queryOption;
    emit('query-update', query.value, selectedOption.value)
}

function logout() {
    const host = import.meta.env.VITE_API_HOST;
    const params = {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    };

    fetch(`${host}/User/logout`, params)
        .then(res => {
            console.log(res.status);
            router.push('/login');
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
        <div class="header-main">
            <div class="action-area-wrapper">
                <a href="/" class="action-area-left focusable">
                    <img class="image" v-bind:src="libstore.logoPath">
                    <div class="spacer"></div>
                    <div>{{ libstore.name }}</div>
                </a>
            </div>

            <div class="search-bar-area">
                <input
                    v-on:keyup="(event) => search(event)"
                    ref="queryInput"
                    class="search-bar"
                >
                <button
                    v-on:click="search()"
                    class="search-icon-area"
                >
                    <img
                        class="image"
                        style="width: 40px; height: 40px;"
                        src="https://img.icons8.com/ios_filled/512/FFFFFF/search--v3.png"
                    >
                </button>
            </div>

            <div class="action-area-right">
                <div v-if="userstore.isLogged">{{ `${userstore.firstName} ${userstore.lastName}` }}</div>
                <a v-else href="/login" class="action-title">log in</a>
                <div class="spacer"></div>
                <img v-on:click="isVisibleMp = !isVisibleMp" v-bind:src="userstore.pfpPath" class="pfp focusable">
            </div>
        </div>



        <div v-if="query !== null" class="query-options-bar">
            <button
                v-bind:class="{ 'active-option': selectedOption === 'book' }"
                v-on:click="select('book')"
                class="query-option"
            >
                book
            </button>

            <button
                v-bind:class="{ 'active-option': selectedOption === 'subsidiary' }"
                v-on:click="select('subsidiary')"
                class="query-option"
            >
                subsidiary
            </button>

            <button
                v-bind:class="{ 'active-option': selectedOption === 'user' }"
                v-on:click="select('user')"
                class="query-option"
            >
                user
            </button>
        </div>
    </header>



    <div v-if="isVisibleMp" class="miniprofile-pane" id="miniprofile-pane">
        <div v-if="userstore.isLogged">
            <button class="miniprofile-button">
                <a v-bind:href="`/profile/${userstore.id}`" class="action-title">
                    profile
                </a>
            </button>

            <button
                v-if="userstore.status !== 'user'"
                v-on:click="isVisibleForm = true"
                class="miniprofile-button"
            >
                <div>
                    manage
                </div>
            </button>

            <div class="line"></div>

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



    <CreateForm
        v-on:hide-form="isVisibleForm = false"
        v-if="isVisibleForm">
    </CreateForm>
</template>

<style scoped>
header {
    top: 0;
    position: sticky;
    background-color: var(--color-background);
    padding: 8px 10px;
}

.header-main {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
}

.action-area-wrapper {
    display: flex;
}

.action-area-left {
    display: flex;
    flex-direction: row;
    align-items: center;
    border-radius: 16px;
    padding: 8px 16px;
    text-decoration: none;
    color: white;
}

.search-bar-area {
    display: flex;
    flex-direction: row;
    height: 50px;
}

.action-area-right {
    display: flex;
    flex-direction: row;
    align-items: center;
    border-radius: 16px;
    padding: 8px 16px;
    text-decoration: none;
    color: white;
    justify-content: end;
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

/* QUERY OPTIONS */
.query-options-bar {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    width: 100%;
}

.query-option {
    background-color: transparent;
    color: white;
    border-radius: 1000px;
    border: 1px solid grey;
    margin: 0px 8px;
    padding: 4px 10px;
    font-size: 16px;
}

    .query-option:hover {
        background-color: white;
        color: black;
    }

    .query-option:active {
        background-color: transparent;
        color: white;
    }

.active-option {
    background-color: white;
    color: black;
}
/* END QUERY OPTIONS */

/* CONTEXT MENU */
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

.miniprofile-button:hover {
    cursor: pointer;
}

.line {
    background-color: grey;
    height: 2px;
    margin: 10px;
}
/* END CONTEXT MENU */
</style>