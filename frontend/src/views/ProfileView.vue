<script setup>
import Header from '@/components/Header.vue';
</script>

<script>
export default {
    data() {
        return {
            status: 'not logged in',
            login: 'heteroSWAGsual@gmail.com',
            firstName: 'first name',
            lastName: 'last name',
            pfpPath: '../../public/empty-pfp.png'
        }
    },
    methods: {
        collectProfileData() {
            console.log({
                status: this.status,
                login: this.login,
                fname: this.firstName,
                lname: this.lastName,
                pfpPath: this.pfpPath
            })
        }
    },
    mounted() {

    }
}
</script>

<template>
    <Header></Header>
    <div class="profile-wrap">
        <div class="profile-area">
            <div class="profile-info">
                <img class="pfp" v-bind:src="this.pfpPath">
                <div class="spacer"></div>

                <div class="profile-text">
                    <div class="info-field">{{ this.status }}</div>
                    <div class="info-field">{{ this.login }}</div>

                    <div class="info-setable">
                        <label class="info-field">profile picture</label>
                        <input
                            v-bind:value="this.pfpPath"
                            v-on:input="event => this.pfpPath = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="https://path.com/to/your/profile-picture.png"
                        >

                        <label class="info-field">first name</label>
                        <input
                            v-bind:value="this.firstName"
                            v-on:input="event => this.firstName = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="first name"
                        >

                        <label class="info-field">last name</label>
                        <input
                            v-bind:value="this.lastName"
                            v-on:input="event => this.lastName = event.target.value"
                            type="text"
                            class="info-field"
                            placeholder="last name"
                        >

                        <button v-on:click="this.collectProfileData" class="save-button">save</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.profile-wrap {
    display: flex;
    justify-content: center;
    width: 100%;
}

.profile-area {
    width: 50%;
    padding: 20px;
}

.pfp {
    width: 250px;
    height: 250px;
    border-radius: 50%;
    border: 2px solid grey;
}

.profile-info {
    display: flex;
    align-items: center;
    flex-direction: row;
}

.spacer {
    width: 30px;
}

.profile-text {
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.info-setable {
    display: grid;
    grid-template-columns: auto 1fr;
}

.info-field {
    font-size: 20px;
    background: transparent;
    border: none;
    color: white;
    margin: 5px 10px;
    padding: 0px;
}

.save-button {
    font-size: 20px;
    margin: 16px 10px 5px 10px;
    border: 1px solid white;
    border-radius: 8px;
    color: black;
    background-color: white;
}

.save-button:hover {
    background: transparent;
    color: white;
    cursor: pointer;
}

.save-button:active {
    background-color: white;
    color: black;
    cursor: pointer;
}
</style>