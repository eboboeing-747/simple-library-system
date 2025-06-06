<script setup>
import { ref } from 'vue';

const props = defineProps({
    data: {
        title: 'title',
        description: 'description'
    }
});

const book = ref({
    title: props.data.title,
    description: props.data.discription,
    imagePath: 'https://d1csarkz8obe9u.cloudfront.net/posterpreviews/old-books-cover-design-template-528851dfc1b6ed275212cd110a105122_screen.jpg?ts=1698687093'
});
</script>

<template>
    <div class="book">
        <img
            v-bind:src="book.imagePath"
        >

        <input
            v-bind:value="book.imagePath"
            v-on:blur="event => book.imagePath = event.target.value"
            type="text"
            placeholder="https://path.com/to/your/image.png"
        >

        <input
            v-bind:value="book.title"
            type="text"
            placeholder="title"
        >

        <textarea
            v-bind:value="book.description"
            placeholder="description"
        >{{ book.description }}</textarea>

        <input
            type="date"
        >

        <input
            type="text"
            placeholder="000-0-000-00000-0"
        >
    </div>
</template>

<style src="@/assets/card.css" scoped>
</style>

<style scoped>
.book {
    display: flex;
    flex-direction: column;
    border: 1px solid grey;
    border-radius: 8px;
    margin: 10px;
    padding: 10px;
}
</style>