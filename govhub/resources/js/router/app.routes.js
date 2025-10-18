export default [
    {
        path: "/",
        name: "app.home",
        component: () => import("@/pages/Home.vue"),
        meta: {
            title: "Home",
            layout: "GuestLayout",
            requiresAuth: false,
            permission: "*",
        },
    },
    {
        path: "/chat",
        name: "app.chat",
        component: () => import("@/pages/Chat.vue"),
        meta: {
            title: "Chat",
            layout: "DefaultLayout",
            requiresAuth: true,
            permission: "*",
        },
    },
    // {
    //     path: "/403",
    //     name: "app.access-denied",
    //     component: () => import("@/pages/AccessDenied.vue"),
    //     meta: {
    //         title: "Access Denied",
    //         layout: "DefaultLayout",
    //         requiresAuth: true,
    //         permission: "*",
    //     },
    // },
    {
        path: "/:pathMatch(.*)*",
        name: "app.not-found",
        component: () => import("@/pages/NotFound.vue"),
        meta: {
            title: "Page Not Found",
            layout: "DefaultLayout",
            requiresAuth: true,
            permission: "*",
        },
    },
];
