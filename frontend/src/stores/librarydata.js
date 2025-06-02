import { defineStore } from 'pinia';

export const libraryStore = defineStore('librarydata', {
    state: () => {
        return {
            logoPath: null,
            name: null,
            description: null
        }
    },
    actions: {
        setLibData(lib) {
            this.logoPath = lib.logoPath;
            this.name = lib.name;
            this.description = lib.description;
        },
        getLibData() {
            return {
                logoPath: this.logoPath,
                name: this.name,
                description: this.description
            };
        }
    }
});