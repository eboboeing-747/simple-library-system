<script setup>
import { userdataStore } from '@/stores/userdata';
import { ErrorHandler } from '@/helpers/errorHandler';
import { onMounted } from 'vue';
import { ref } from 'vue';

const store = userdataStore();
let errorHandler = null;

// const image = ref('');
const name = ref('');
const address = ref('');
const description = ref('');

async function create() {

    let formData = {
        // imagePath: image.value,
        name: name.value,
        address: address.value,
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
        let res = await fetch(`${store.host}/Subsidiary/create`, params);
        if (res.ok)
            errorHandler.displaySuccess();
    } catch(error) {
        console.error(error);
    }
}

onMounted(() => {
    const errorDisplay = document.getElementById('error-display');
    errorHandler = new ErrorHandler(errorDisplay);
})
</script>
<template>
    <form v-on:submit.prevent="create">
        <!--
        <img
            v-bind:src="image"
        >

        <input
            v-bind:value="image"
            v-on:blur="(event) => image = event.target.value"
            placeholder="https://path.com/to/your/image.png"
            type="text"
        >
        -->

        <input
            v-bind:value="name"
            v-on:input="(event) => name = event.target.value"
            placeholder="name"
            type="text"
            required
        >

        <input
            v-bind:value="address"
            v-on:input="(event) => address = event.target.value"
            id="address"
            placeholder="address"
            type="text"
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