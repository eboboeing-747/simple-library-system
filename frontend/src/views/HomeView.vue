<script setup>
import Header from '@/components/Header.vue'
import Explore from '@/components/Explore.vue';
import { onMounted } from 'vue';

/*
 * nolib = no libraries created so far
 * nosub = no subsidiaries created so far
 * ok = functioning
 */
let libstatus = 'nolib';

onMounted(async () => {
    /*
    try {
        let res = await fetch(`${storeToRefs.host}/Library/get`);
        let body = await res.json();
    
        console.log(body);
    } catch(error) {
        console.error(error);
    }
    */
});
</script>

<template>
    <Header></Header>

    <main>
        <Explore v-if="libstatus === 'ok'"></Explore>
    
        <div v-else>
            <div class="spacer"></div>

            <div v-if="libstatus === 'nolib'">
                <div>library is not created so far</div>
            </div>

            <div v-else-if="libstatus === 'nosub'">
                <div>library name</div>
                <div>library discription</div>
            </div>
        </div>
    </main>
</template>

<style scoped>
.spacer {
    height: 300px;
}

main {
    display: flex;
    justify-content: center;
}
</style>