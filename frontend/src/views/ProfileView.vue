<script setup>
import Header from '@/components/Header.vue';
import { userdataStore } from '@/stores/userdata';
import { ref } from 'vue';

const store = userdataStore();

const fetchMessage = ref('');
const statusClass = ref('success');

async function update() {
    statusClass.value = '';
    fetchMessage.value = 'updaing...';

    const params = {
        method: 'PUT',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "firstName": store.firstName,
            "lastName": store.lastName,
            "pfpPath": store.pfpPath
        })
    };

    try {
        const res = await fetch(`${store.host}/User/update`, params);
        statusClass.value = 'success';
        fetchMessage.value = 'profile has successfully been updated';
    } catch(error) {
        console.log(error);
        statusClass.value = 'error';
        fetchMessage.value = 'an error occured while udpating profile';
    }
}

function copyToClipboard() {
    navigator.clipboard.writeText(store.id);
}
</script>

<template>
    <Header></Header>
    <div class="profile-wrap">
        <div class="profile-area">
            <div class="profile-info">
                <img class="pfp" v-bind:src="store.pfpPath">
                <div class="spacer"></div>

                <div class="profile-text">
                    <div class="info-field">{{ store.status }}</div>
                    <div class="info-table">
                        <div class="info-field">{{ store.login }}</div>
                        <button class="info-field id-copy">
                            <div v-on:click="copyToClipboard" class="id">{{ store.id }}</div>
                            <div style="width: 10px;"></div>
                            <img class="action-icon" src="/copy.png">
                        </button>

                        <label class="info-field">profile picture</label>
                        <input
                            v-bind:value="store.pfpPath"
                            v-on:blur="event => store.pfpPath = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="https://path.com/to/your/profile-picture.png"
                        >

                        <label class="info-field">first name</label>
                        <input
                            v-bind:value="store.firstName"
                            v-on:input="event => store.firstName = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="first name"
                        >

                        <label class="info-field">last name</label>
                        <input
                            v-bind:value="store.lastName"
                            v-on:input="event => store.lastName = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="last name"
                        >

                        <button v-on:click="update" class="save-button">save</button>
                        <div class="info-field error-display" v-bind:class="statusClass">{{ fetchMessage }}</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.profile-wrap {
    display: flex;
    justify-content: center;
    width: 100%;
}

.profile-area {
    width: 50%;
    padding: 20px;
}

.id-copy {
    display: flex;
    flex-direction: row;
    font-family: monospace;
    align-items: center;
    cursor: pointer;
}

.id {
    text-align: left;
}

.action-icon {
    width: 30px;
    height: auto;
}

.pfp {
    width: 250px;
    height: 250px;
    border-radius: 50%;
    border: 2px solid grey;
}

.profile-info {
    display: flex;
    align-items: center;
    flex-direction: row;
}

.spacer {
    width: 30px;
}

.profile-text {
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.info-table {
    display: grid;
    grid-template-columns: auto 1fr;
}

.info-field {
    font-size: 20px;
    background: transparent;
    border: none;
    color: white;
    margin: 5px 10px;
    padding: 0px;
}

.save-button {
    font-size: 20px;
    margin: 16px 10px 5px 10px;
    border: 1px solid white;
    border-radius: 8px;
    color: black;
    background-color: white;
}

.save-button:hover {
    background: transparent;
    color: white;
    cursor: pointer;
}

.save-button:active {
    background-color: white;
    color: black;
    cursor: pointer;
}

.error-display {
    margin: 13px 10px 8px 10px;
}

.success {
    color: green;
}

.error {
    color: red;
}
</style>