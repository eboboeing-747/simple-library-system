<script setup>
import Book from './cards/Book.vue';
import { userdataStore } from '@/stores/userdata';
import { ref } from 'vue';

const userstore = userdataStore();
const books = ref(null);
const unitType = ref(null);

async function find(query, type) {
    unitType.value = type;

    const params = {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    try {
        let res = await fetch(`${userstore.host}/Book/find?query=${query}`);

        switch (res.status) {
            case 200:
                break;
            case 404:
                books.value = null;
                return;
            default:
                books.value = null;
                return;
        }

        books.value = await res.json();
        console.log(books.value);
    } catch(error) {
        console.error(error);
    }
}

defineExpose({ find });
</script>

<template>
    <div class="book-area">
        <Book
            v-if="unitType === 'book'"
            v-for="book in books"
            v-bind:data="book"
        />
    </div>
</template>

<style scoped>
.book-area {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr;
    width: 50%;
    justify-content: center;
}
</style>