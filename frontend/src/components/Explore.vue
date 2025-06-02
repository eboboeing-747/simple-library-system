<script setup>
import Book from './Book.vue';

let books = [
    {
        title: 'title1',
        description: 'discription1',
    },
    {
        title: 'title2',
        description: 'discription2',
    },
    {
        title: 'title3',
        description: 'discription3',
    },
    {
        title: 'title4',
        description: 'discription4',
    },
    {
        title: 'title5',
        description: 'discription5',
    },
    {
        title: 'title6',
        description: 'discription6',
    },
    {
        title: 'title7',
        description: 'discription7',
    },
    {
        title: 'title8',
        description: 'discription8',
    },
    {
        title: 'title9',
        description: 'discription9',
    },
    {
        title: 'title10',
        description: 'discription10',
    },
];
</script>

<template>
    <div class="book-area">
        <Book v-for="book in books" v-bind:data="book"/>
    </div>
</template>

<style scoped>
.book-area {
    display: flex;
    flex-wrap: wrap;
    width: 50%;
    justify-content: center;
}
</style>