import api from "../utils/api";

class Auth {
    /**
     * Logout all other user sessions
     * @returns {Promise} - Axios promise with logout result
     */
    static closeOthers() {
        return api.post("/auth/sessions");
    }

    /**
     * Get all user sessions
     * @returns {Promise} - Axios promise with user data
     */
    static getAll() {
        return api.get("/auth/sessions");
    }

    /**
     * Get user's current session
     * @returns {Promise} - Axios promise with user data
     */
    static current() {
        return api.get("/auth/sessions/current");
    }
}

export default Auth;
