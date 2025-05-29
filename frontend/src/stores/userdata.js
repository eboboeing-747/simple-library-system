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
            sex: false,
            pfpPath: '../../public/empty-pfp.png'
        }
    },
    actions: {
        setUserData(user) {
            this.login = user.login;
            this.status = user.userType;
            this.firstName = user.firstName;
            this.lastName = user.lastName;
            this.sex = user.sex;
            this.pfpPath = 'https://media1.tenor.com/m/jfJjRLN19CwAAAAd/9impulse-impulse.gif';
        }
    }
});