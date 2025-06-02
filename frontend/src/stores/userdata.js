import { defineStore } from 'pinia';

export const userdataStore = defineStore('userdata', {
    state: () => {
        return {
            host: null,
            isLogged: false,
            id: 'not logged in',
            login: 'heteroSWAGsual@gmail.com',
            status: 'not logged in',
            firstName: 'not logged in',
            lastName: 'not logged in',
            sex: false,
            pfpPath: '../../public/empty-pfp.png'
        }
    },
    actions: {
        setUserData(user) {
            this.id = user.id;
            this.login = user.login;
            this.status = user.userType;
            this.firstName = user.firstName;
            this.lastName = user.lastName;
            this.sex = user.sex;
            this.pfpPath = user.pfpPath;
        }
    }
});