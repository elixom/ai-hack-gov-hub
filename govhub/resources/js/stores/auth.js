import { defineStore } from "pinia";
import { Auth } from "@/models";
import router from "@/router/router";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        user: null,
        activeLocation: null,
        isLoading: false,
        error: null,
        formChanged: false,
        intendedRoute: null,
        handler: function (e) {
            // Cancel the event
            e.preventDefault();
            // Chrome requires returnValue to be set
            e.returnValue = "";
        },
    }),

    getters: {
        isAuthenticated: (state) => !!state.user,
        userRole: (state) => state.user?.role || null,
        userName: (state) =>
            state.user
                ? `${state.user.first_name} ${state.user.last_name}`
                : null,
    },

    actions: {
        init() {
            this.user = JSON.parse(localStorage.getItem("user"));
            this.activeLocation = JSON.parse(
                localStorage.getItem("activeLocation")
            );
        },

        setUser(userData) {
            this.user = userData;
            localStorage.setItem("user", JSON.stringify(userData));
        },

        setActiveLocation(location) {
            if (location != null) {
                this.activeLocation = location;
                localStorage.setItem(
                    "activeLocation",
                    JSON.stringify(location)
                );
            } else {
                this.activeLocation = null;
                localStorage.removeItem("activeLocation");
            }
        },

        reset() {
            this.user = null;
            this.activeLocation = null;
            this.isLoading = false;
            this.error = null;
            this.formChanged = false;
            this.intendedRoute = null;

            localStorage.clear();
        },

        async fetchUser() {
            if (this.isLoading) return;

            this.isLoading = true;
            try {
                const response = await Auth.me();

                if (response.data.success) {
                    this.setUser(response.data.user);
                    return response.data.user;
                } else {
                    this.reset();
                }
            } catch (error) {
                console.error("Failed to fetch user:", error);
                this.reset();
            } finally {
                this.isLoading = false;
            }
        },

        logout() {
            Auth.logout().finally(() => {
                this.reset();
                router.push({ name: "auth.login" });
            });
        },

        setFormChangedStatus(status) {
            this.formChanged = status;

            if (status === true) {
                window.addEventListener("beforeunload", this.handler);
            } else {
                window.removeEventListener("beforeunload", this.handler);
            }
        },
    },
});
