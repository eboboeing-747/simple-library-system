<script setup>
import Header from '@/components/Header.vue'
import Explore from '@/components/Explore.vue';
import Welcome from '@/components/Welcome.vue';
import { ref } from 'vue';

const searchQuery = ref(null);
</script>

<template>
    <Header
        v-on:query-update="(query, selectedOption) => {console.log(query, selectedOption);searchQuery = query}"
    >
    </Header>

    <main>
        <div>{{ searchQuery }}</div>

        <Explore v-if="searchQuery !== null"></Explore>
        
        <Welcome v-else></Welcome>
    </main>
</template>

<style scoped>
main {
    display: flex;
    justify-content: center;
}
</style>