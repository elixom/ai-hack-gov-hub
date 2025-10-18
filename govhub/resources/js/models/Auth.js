import api from "../utils/api";

class Auth {
    /**
     * Login user
     * @param {Object} credentials - Login credentials (email, password)
     * @returns {Promise} - Axios promise with login result
     */
    static login(credentials) {
        return api.get("/sanctum/csrf-cookie").then(() => {
            return api.post("/auth/login", credentials);
        });
    }

    /**
     * Logout current user
     * @returns {Promise} - Axios promise with logout result
     */
    static logout() {
        return api.post("/auth/logout");
    }

    static updatePassword(data) {
        return api.put(`/auth/user/password`, data);
    }

    /**
     * Request password reset
     * @param {Object} data - Email data
     * @returns {Promise} - Axios promise with reset result
     */
    static requestPasswordReset(data) {
        return api.post("/auth/forgot-password", data);
    }
}

export default Auth;
