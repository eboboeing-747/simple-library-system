<script setup>
const props = defineProps({
    data: {
        name: 'title'
    }
});
</script>

<template>
    <a
        v-bind:href="`/profile/${props.data.id}`"
        class="book"
    >
        <img
            v-bind:src="props.data.pfpPath"
        >

        <div>
            {{ props.data.userType }}
        </div>

        <div>
            {{ props.data.login }}
        </div>

        <div>
            {{ props.data.firstName }} {{ props.data.lastName }}
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