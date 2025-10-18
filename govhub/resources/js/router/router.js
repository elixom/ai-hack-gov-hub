import { createWebHistory, createRouter } from "vue-router";
import routes from "./index";
import { useAuthStore } from "@/stores/auth";

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
    scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
            return savedPosition;
        } else {
            return { top: 0 };
        }
    },
});

// Add a global navigation guard
router.beforeEach((to, from, next) => {
    // Set document title
    document.title = to.meta.title;

    // Get auth state from store
    const store = useAuthStore();
    const isAuthenticated = store.isAuthenticated;

    // Handle authentication requirements
    if (to.meta.requiresAuth && !isAuthenticated) {
        next({ name: "auth.login" });
        return;
    }

    // Handle accessing public routes
    if (!to.meta.requiresAuth && isAuthenticated) {
        next({ name: "app.chat" });
        return;
    }

    next();
});

export default router;
