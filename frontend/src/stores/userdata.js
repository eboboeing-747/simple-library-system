import { defineStore } from 'pinia';

export const userdataStore = defineStore('userdata', {
    state: () => {
        return {
            host: '',
            isLogged: false,
            status: 'not logged in',
            login: 'heteroSWAGsual@gmail.com',
            firstName: 'first name',
            lastName: 'last name',
            pfpPath: '../../public/empty-pfp.png'
        }
    }
});