<script setup>
import Header from '@/components/Header.vue';
import { userdataStore } from '@/stores/userdata';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { ref } from 'vue';

const router = useRouter();
const userstore = userdataStore();
const bookId = router.currentRoute.value.params.id;

const fetchMessage = ref('');
const statusClass = ref('success');

async function getBookData() {
    const params = {
        method: 'GET',
        mode: 'cors'
    };

    try {
        let res = await fetch(`${userstore.host}/Book/get/${bookId}`, params);
        
        switch(res.status) {
            case 200:
                break;
            case 404:
                console.error('404 NOT FOUND');
                return null;
            default:
                console.error('FAILED TO FETCH');
                return null;
        }

        let book = await res.json();
        return book;
    } catch(error) {
        console.error(error);
    }
}

async function take() {
    statusClass.value = '';
    fetchMessage.value = 'updaing...';

    const params = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        }
    };

    try {
        const res = await fetch(`${userstore.host}/Book/take/${bookData.value.id}`, params);

        switch(res.status) {
            case 201:
                statusClass.value = 'success';
                fetchMessage.value = 'book taken successfully';
                break;
            case 409:
                statusClass.value = 'success';
                fetchMessage.value = 'you already have this book';
                break;
            default:
                throw new Error();
        }
    } catch(error) {
        console.error(error);
        statusClass.value = 'error';
        fetchMessage.value = 'an error occured while taking book';
    }
}

function copyToClipboard() {
    navigator.clipboard.writeText(bookData.value.id);
}

let bookData = ref({
    id: null,
    name: null,
    description: null,
    imagePath: null,
    publishDate: null,
    isbn: null
});

const isAuthorized = userstore.status === 'admin' || userstore.status === 'librarian';
onMounted(async () => {
    bookData.value = await getBookData();
})
</script>

<template>
    <Header></Header>
    <div class="view-wrap">
        <div class="content-area">
            <div class="main-info">
                <img
                    v-bind:src="bookData.imagePath"
                >
                <div class="spacer"></div>

                <div>
                    <div class="info-table">
                        <div class="info-field">id:</div>
                        <button
                            v-on:click="copyToClipboard"
                            class="info-field id-copy"
                        >
                            <div class="id">{{ bookData.id }}</div>
                            <div style="width: 10px;"></div>
                            <img class="action-icon" src="/copy.png">
                        </button>

                        <div class="info-field">title:</div>
                        <input
                            v-bind:value="bookData.name"
                            v-bind:readonly="!isAuthorized"
                            class="info-field"
                            type="text"
                        >

                        <div class="info-field">published:</div>
                        <input
                            v-bind:value="new Date(bookData.publishDate).toDateString()"
                            v-bind:readonly="!isAuthorized"
                            class="info-field"
                            type="datetime"
                        >

                        <div class="info-field">isbn:</div>
                        <input
                            v-bind:value="bookData.isbn"
                            v-bind:readonly="!isAuthorized"
                            class="info-field"
                            type="text"
                        >

                        <button
                            v-if="userstore.isLogged"
                            v-on:click="take"
                            class="save-button"
                        >
                            take
                        </button>
                        <div class="info-field error-display" v-bind:class="statusClass">{{ fetchMessage }}</div>
                    </div>
                </div>
            </div>
            <textarea
                class="info-field description wide"
            >{{ bookData.description }}</textarea>
        </div>
    </div>
</template>

<style scoped>
.view-wrap {
    display: flex;
    justify-content: center;
    width: 100%;
}

.content-area {
    display: flex;
    flex-direction: column;
    padding: 20px;
}

.main-info {
    display: flex;
    flex-direction: row;
}

.spacer {
    width: 30px;
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

.description {
    resize: vertical;
    scrollbar-width: none;
    field-sizing: content;
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