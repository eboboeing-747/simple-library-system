<script setup>
import Header from '@/components/Header.vue'
import Explore from '@/components/Explore.vue';
import Welcome from '@/components/Welcome.vue';
import { ref } from 'vue';

const searchQuery = ref(null);
const explore = ref(null);
</script>

<template>
    <Header
        v-on:query-update="(query, unitType) => {searchQuery = query; explore.find(query, unitType);}"
    >
    </Header>

    <main>
        <Explore ref="explore" v-show="searchQuery !== null"></Explore>
        
        <Welcome v-show="searchQuery === null"></Welcome>
    </main>
</template>

<style scoped>
main {
    display: flex;
    justify-content: center;
}
</style>