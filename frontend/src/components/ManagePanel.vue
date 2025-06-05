<script setup>
</script>

<template>
    <div class="nav-bar">
        <button class="nav-button"
        >
            library
        </button>
        <button class="nav-button"
        >
            users
        </button>
        <button class="nav-button"
        >
            subsidiaries
        </button>
        <button class="nav-button"
        >
            books
        </button>
        <button class="nav-button"
        >
            authors
        </button>
    </div>
</template>

<style scoped>
.nav-bar {
    display: flex;
    width: 100%;
    flex-direction: row;
    justify-content: center;
    align-items: center;
}

.nav-button {
    background: transparent;
    color: white;
    border: none;
    font-size: 20px;
    margin: 0px 10px;
}
</style>