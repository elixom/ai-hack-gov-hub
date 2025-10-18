export default [
    {
        path: "/login",
        name: "auth.login",
        component: () => import("@/pages/auth/Login.vue"),
        meta: {
            title: "Login",
            layout: "GuestLayout",
            requiresAuth: false,
            permission: "*",
        },
    },
    {
        path: "/forgot-password",
        name: "auth.forgot-password",
        component: () => import("@/pages/auth/ForgotPassword.vue"),
        meta: {
            title: "Forgot Password",
            layout: "GuestLayout",
            requiresAuth: false,
            permission: "*",
        },
    },
    {
        path: "/user-profile",
        name: "auth.user-profile",
        component: () => import("@/pages/auth/Profile.vue"),
        meta: {
            title: "User Profile",
            layout: "DefaultLayout",
            requiresAuth: true,
            permission: "*",
        },
    },
];
