import {createRouter, createWebHistory} from 'vue-router'
import i18n from '@/plugins/i18n'
import useAuthStore, {Roles} from "@/stores/authStore.js";

var locales = i18n.global.availableLocales;

const requiredRolePolicies = {
    Admin: [Roles.Admin],
    Client: [Roles.Client, Roles.Admin],
}

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
                },
                {
                    path: "dashboard",
                    name: 'Dashboard',
                    component: () => import('../components/layouts/GuardLayout.vue'),
                    children: [
                        {
                            name: "DashboardLayout",
                            path: "",
                            component: () => import('../components/layouts/DashboardLayout.vue'),
                            children: [
                                {
                                    path: '',
                                    name: 'MyInfo',
                                    component: () => import('../views/Dashboard/my-info.vue'),
                                    meta: {
                                        requiredRoles: requiredRolePolicies.Client
                                    }
                                },
                                {
                                    path: 'users',
                                    name: 'UserManagement',
                                    component: () => import('../views/Dashboard/user-management.vue'),
                                    meta: {
                                        requiredRoles: requiredRolePolicies.Admin
                                    }
                                },
                                {
                                    path: 'trip-management',
                                    name: 'TripManagement',
                                    component: () => import('../views/Dashboard/trip-management.vue'),
                                    meta: {
                                        requiredRoles: requiredRolePolicies.Admin
                                    }
                                },
                                {
                                    path: 'my-orders',
                                    name: 'MyInfo',
                                    component: () => import('../views/Dashboard/my-orders.vue'),
                                    meta: {
                                        requiredRoles: requiredRolePolicies.Client
                                    }
                                },
                            ]
                        }
                    ]
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
