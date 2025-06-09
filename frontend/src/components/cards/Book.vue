<script setup>
const props = defineProps({
    data: {
        name: 'title',
        description: 'description'
    }
});
</script>

<template>
    <a
        v-bind:href="`/book/${props.data.id}`"
        class="book"
    >
        <img
            v-bind:src="props.data.imagePath"
        >

        <!--
        <input
            v-bind:value="book.imagePath"
            v-on:blur="event => book.imagePath = event.target.value"
            type="text"
            placeholder="https://path.com/to/your/image.png"
        >
        -->

        <div>
            {{ props.data.name }}
        </div>

        <div>
            {{ props.data.description }}
        </div>

        <div type="date">
            {{ new Date(props.data.publishDate).toDateString() }}
        </div>

        <div>
            {{ props.data.isbn }}
        </div>
    </a>
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