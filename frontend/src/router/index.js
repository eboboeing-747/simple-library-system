import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import AuthView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import ProfileView from '@/views/ProfileView.vue'
import BookView from '@/views/BookView.vue'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomeView
        },
        {
            path: '/login',
            name: 'login',
            component: AuthView
        },
        {
            path: '/register',
            name: 'register',
            component: RegisterView
        },
        {
            path: '/profile/:id',
            name: 'profile',
            component: ProfileView
        },
        {
            path: '/book/:id',
            name: 'book',
            component: BookView
        }
    ]
})

export default router