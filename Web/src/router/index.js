import { createRouter, createWebHistory } from 'vue-router'
import i18n from '@/plugins/i18n'
var locales = i18n.global.availableLocales
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Main',
      component: () => import('../components/layouts/MainLayout.vue'),
      children: [
        {
          path: '',
          name: 'Home',
          component: () => import('../views/home.vue')
        },
        {
          path: 'trip/:slug',
          name: 'TripView',
          component: () => import('../views/trip-view.vue')
        }
      ],
      beforeEnter: (to) => {
        i18n.global.locale = localStorage.getItem('lang') ?? 'uz'
      }
    }
  ],
  scrollBehavior: () => {
    return {
      top: 0,
      behavior: 'smooth'
    }
  }
})

export default router
