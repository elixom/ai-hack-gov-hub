import axios from "axios";
import router from "@/router/router";
import { useAuthStore } from "@/stores/auth";

// Create a new Axios instance with custom config
const api = axios.create({
    baseURL: "/api",
    timeout: 60000, // 60 second timeout
    withCredentials: true,
});

// Add default headers
api.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
api.defaults.headers.common["Accept"] = "application/json";
api.defaults.headers.get["Content-Type"] = "application/json";
api.defaults.headers.post["Content-Type"] = "application/json";
api.defaults.headers.put["Content-Type"] = "application/json";
api.defaults.withCredentials = true;

// Request interceptor
api.interceptors.request.use(
    (config) => {
        const store = useAuthStore();

        // Add CSRF token if available
        const csrfToken = document
            .querySelector('meta[name="csrf-token"]')
            ?.getAttribute("content");
        if (csrfToken) {
            config.headers["X-CSRF-TOKEN"] = csrfToken;
        }

        if (store.activeLocation) {
            config.headers["X-Location"] = store.activeLocation.code;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor - handles auth errors and other common issues
api.interceptors.response.use(
    (response) => {
        return response;
    },
    async (error) => {
        const store = useAuthStore();

        // Handle 401 Unauthorized - token expired or invalid
        if (error.response.status === 401 || error.response.status === 419) {
            store.reset();

            // Redirect to login
            router.push({ name: "auth.login" });

            return Promise.resolve({ redirected: true });
        }

        // Handle 403 Forbidden - user doesn't have permission
        if (error.response && error.response.status === 403) {

            // General 403 redirect to access denied page
            if (
                error.response?.data?.message &&
                error.response.data.message == "This action is unauthorized."
            ) {
                router.push({ name: "app.access-denied" });
            }

            return Promise.resolve({ redirected: true });
        }

        return Promise.reject(error);
    }
);

export default api;
