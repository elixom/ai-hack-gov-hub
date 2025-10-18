// Import routes
import auth from "./auth.routes.js";
import app from "./app.routes.js";

// Combine all routes
export default [
    ...auth,
    ...app,
];
