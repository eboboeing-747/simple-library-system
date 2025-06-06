<script setup>
import Header from '@/components/Header.vue'
import Explore from '@/components/Explore.vue';
import ManagePanel from '@/components/ManagePanel.vue';
import { onMounted } from 'vue';
import { libraryStore } from '@/stores/librarydata';
import { userdataStore } from '@/stores/userdata';

const libstore = libraryStore();
const userstore = userdataStore();

let searchQuery = null;

onMounted(async () => {
    /*
    try {
        let res = await fetch(`${storeToRefs.host}/Library/get`);
        let body = await res.json();
    
        console.log(body);
    } catch(error) {
        console.error(error);
    }
    */
});

async function update() {
    const lib = libstore.getLibData();

    const params = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(lib)
    };

    try {
        let res = await fetch(`${userstore.host}/Library/update`, params);
    } catch(error) {
        console.error(error);
    }
}
</script>

<template>
    <Header></Header>

    <main>
        <Explore v-if="searchQuery !== null"></Explore>

        <div v-else class="lib-info-wrapper">
            <div class="spacer"></div>

            <div class="lib-info">
                <input v-if="userstore.status === 'admin'"
                    class="info-field wide"
                    v-bind:value="libstore.logoPath"
                    v-on:blur="event => libstore.logoPath = event.target.value"
                    type="text"
                    placeholder="https://path.com/to/your/library-log.png"
                >

                <img v-bind:src="libstore.logoPath" class="logo">

                <div v-if="userstore.status !== 'admin'" class="lib-text-info">
                    <div>{{ libstore.name }}</div>
                    <div>{{ libstore.description }}</div>
                </div>

                <div v-else class="lib-text-info wide">
                    <input
                        class="info-field wide"
                        v-bind:value="libstore.name"
                        v-on:input="event => libstore.name = event.target.value"
                        type="text"
                        placeholder="name of your library"
                    >

                    <textarea
                    v-bind:value="libstore.description"
                    v-on:blur="event => libstore.description = event.target.value"
                    class="info-field description wide"
                    >{{ libstore.description }}</textarea>

                    <button v-on:click="update">save changes</button>
                </div>
            </div>
        </div>
    </main>
</template>

<style scoped>
main {
    display: flex;
    justify-content: center;
}

input {
    text-align: center;
}

button {
    background: transparent;
    color: white;
    border: 2px solid grey;
    margin: 10px;
    padding: 8px 16px;
    border-radius: 8px;
    font-size: 24px;
}

    button:hover {
        background-color: white;
        color: black;
    }

    button:active {
        background: transparent;
        color: white;
    }

.spacer {
    height: 200px;
}

.lib-info-wrapper {
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.logo {
    width: 400px;
    height: auto;
    border-radius: 8px;
    margin: 10px;
}

.lib-info {
    width: 50%;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.lib-text-info {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.wide {
    width: 100%;
}

.info-field {
    background: transparent;
    border-style: none;
    color: white;
    font-size: 24px;
    padding: 10px;
}

.description {
    resize: vertical;
    scrollbar-width: none;
    field-sizing: content;
}
</style>