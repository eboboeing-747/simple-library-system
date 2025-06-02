<script setup>
defineProps({
    data: {
        id: null,
        title: null,
        description: null
    }
});
</script>

<template>
    <div class="book">
        <div>{{ data.title }}</div>
        <div>{{ data.description }}</div>
    </div>
</template>

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