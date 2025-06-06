<script setup>
import { userdataStore } from '@/stores/userdata';
import { ref } from 'vue';
import { onMounted } from 'vue';

const store = userdataStore();

const image = ref('');
const title = ref('');
const isbn = ref('');
const publishDate = ref(null);
const description = ref('');

async function create() {
    let formData = {
        imagePath: image.value,
        name: title.value,
        isbn: isbn.value,
        publishDate: new Date(publishDate.value).getTime(),
        description: description.value
    };

    console.log(formData);

    const params = {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    };

    try {
        let res = await fetch(`${store.host}/Book/create`, params);
    } catch(error) {
        console.error(error);
    }
}

onMounted(() => {
    document.getElementById('date').valueAsDate = new Date();
    publishDate.value = new Date().getTime();
})
</script>

<template>
    <form v-on:submit.prevent="create">
        <img
            v-bind:src="image"
        >

        <input
            v-bind:value="image"
            v-on:blur="(event) => image = event.target.value"
            placeholder="https://path.com/to/your/image.png"
            type="text"
        >

        <input
            v-bind:value="title"
            v-on:blur="(event) => title = event.target.value"
            placeholder="title"
            type="text"
            required
        >

        <input
            v-bind:value="isbn"
            v-on:blur="(event) => isbn = event.target.value"
            placeholder="000-0-000-00000-0"
            type="text"
            required
        >

        <input
            v-on:blur="(event) => publishDate = event.target.value"
            type="date"
            id="date"
            required
        >

        <textarea
            v-bind:value="description"
            v-on:blur="(event) => description = event.target.value"
            placeholder="description"
            required
        ></textarea>

        <input
            type="submit"
            class="submit"
        >
    </form>
</template>

<style scoped>
form {
    display: flex;
    flex-direction: column;
}

input {
    background: transparent;
    color: white;
    border: 2px solid grey;
    border-radius: 1000px;
    margin: 10px;
    padding: 10px 20px;
    font-size: 20px;
}

    .submit:hover {
        background: white;
        color: black;
        cursor: pointer;
    }

    .submit:active {
        background: transparent;
        color: white;
        cursor: pointer;
    }

textarea {
    background: transparent;
    color: white;
    border: 2px solid grey;
    border-radius: 8px;
    margin: 10px;
    padding: 10px 20px;
    font-size: 20px;
}
</style>