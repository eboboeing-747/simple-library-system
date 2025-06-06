<script setup>
import { userdataStore } from '@/stores/userdata';
import { ref } from 'vue';
import Form from './BookForm.vue';

const userdata = userdataStore();
const emit = defineEmits()

const selected = ref(0);
const forms = [
    {
        name: 'book',
        fields: ['name', 'author', 'isbn'],
        epPath: '/Book/create',
        image: true
    },
    {
        name: 'subsidiary',
        fields: ['name', 'address', 'idk'],
        epPath: '/Subsidiary/create'
    },
    {
        name: 'reading room',
        fields: ['name', 'id', 'idk'],
        epPath: '/ReadingRoom/create'
    }
];

function handleForm(event) {
    let formPane = document.getElementById('form');
    if (!formPane.contains(event.target))
        emit('hide-form');
}
</script>

<template>
    <div
        v-on:click="handleForm"
        class="form-wrapper"
        id="form-wrapper"
    >
        <div class="spacer"></div>

        <div
            class="form"
            id="form"
        >

            <div class="nav">
                <button
                    v-for="(form, index) in forms"
                    v-on:click="(event) => selected = index"
                    v-bind:class="{ 'active': selected === index }"
                    class="nav-button"
                >
                    {{ form.name }}
                </button>
            </div>

            <div>
                <Form
                    v-if="forms[selected].name === 'book'"
                    v-bind:form="forms[selected]"
                >
                </Form>
            </div>
        </div>
    </div>
</template>

<style scoped>
.form-wrapper {
    position: absolute;
    top: 0px;
    right: 0px;
    width: 100vw;
    height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    backdrop-filter: blur(4px);
    background: rgba(0, 0, 0, 0.35);
}

.form {
    border: 1px solid grey;
    border-radius: 8px;
}

.spacer {
    height: 300px;
}

.nav {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
}

.nav-button {
    background: transparent;
    color: white;
    border: none;
    font-size: 20px;
    margin: 8px 10px;
    border-radius: 8px;
    padding: 4px 16px;
}

.active {
    background: white;
    color: black;
}
</style>