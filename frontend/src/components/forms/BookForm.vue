<script setup>
import { ErrorHandler } from '@/helpers/errorHandler';
import { userdataStore } from '@/stores/userdata';
import { ref } from 'vue';
import { onMounted } from 'vue';

const store = userdataStore();
let errorHandler = null;
let isbnField = null;

const image = ref('');
const title = ref('');
const isbn = ref('');
const publishDate = ref(null);
const description = ref('');

async function create() {
    errorHandler.hideErrors();
    let isbnValid = validateIsbn(isbn.value);

    if (!isbnValid.isValid) {
        errorHandler.displayError('isbn is invalid', [isbnField]);
        return;
    }

    let formData = {
        imagePath: image.value,
        name: title.value,
        isbn: isbnValid.isbnStr,
        publishDate: new Date(publishDate.value).getTime(),
        description: description.value
    };

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
        if (res.ok)
            errorHandler.displaySuccess();
    } catch(error) {
        console.error(error);
    }
}

function validateIsbn(isbn) {
    const pattern = /^(?<g0>\d{3})(?:-|_| )?(?<g1>\d)(?:-|_| )?(?<g2>\d{3})(?:-|_| )?(?<g3>\d{5})(?:-|_| )?(?<g4>\d)$/;

    if (!pattern.test(isbn))
        return { isValid: false, isbnStr: null };

    const { g0, g1, g2, g3, g4 } = pattern.exec(isbn).groups;

    return { isValid: true, isbnStr: `${g0}-${g1}-${g2}-${g3}-${g4}` };
}

onMounted(() => {
    document.getElementById('date').valueAsDate = new Date();
    publishDate.value = new Date().getTime();
    isbnField = document.getElementById('isbn');
    const errorDisplay = document.getElementById('error-display');
    errorHandler = new ErrorHandler(errorDisplay);
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
            id="isbn"
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
        >
        </textarea>

        <input
            type="submit"
            class="submit"
        >
    </form>

    <div
        class="error-display"
        id="error-display"
    >
    </div>
</template>

<style src="@/assets/create.css" scoped>
</style>