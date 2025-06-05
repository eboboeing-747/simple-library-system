<script setup>
const props = defineProps(['form'])

async function create() {
    let formData = {};

    props.form.fields.forEach((field) => {
        formData[field] = document.getElementById(field).value;
    })

    console.log({
        'fetch to ': `${'https://localhost:4545/'}${form.epPath}`,
        formData
    });
}
</script>

<template>
    <form v-on:submit.prevent="create">
        <input
            v-for="field in form.fields"
            v-bind:placeholder="field"
            v-bind:id="field"
            type="text"
        />

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
</style>