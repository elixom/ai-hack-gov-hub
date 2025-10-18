<template>
    <div class="flex items-center justify-center mt-20">
        <div class="w-full max-w-md p-6 bg-white shadow-2xl rounded-xl">
            <!-- Error Message -->
            <div
                v-if="error"
                class="p-3 mb-4 text-sm text-red-700 border border-red-200 rounded-lg bg-red-50"
            >
                {{ error }}
            </div>

            <!-- Login Form -->
            <form
                @submit.prevent="handleLogin"
                @keyup="delete this.errors[$event.target.id]"
                class="space-y-6"
            >
                <div>
                    <text-input
                        id="email"
                        name="email"
                        type="email"
                        v-model="loginForm.email"
                        placeholder="Email Address"
                        :error="errors.email"
                    />
                    <error-message :errors="errors.email" />
                </div>

                <div class="relative">
                    <text-input
                        id="password"
                        name="password"
                        :type="showPassword ? 'text' : 'password'"
                        v-model="loginForm.password"
                        placeholder="Password"
                        :error="errors.password"
                    />
                    <button
                        type="button"
                        @click="togglePasswordVisibility"
                        class="absolute p-1 top-2 right-2"
                    >
                        <svg
                            v-if="showPassword"
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 14 14"
                            fill="none"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            class="w-5 h-5 text-gray-500 stroke-current stroke-1"
                        >
                            <g>
                                <path
                                    d="M13.23,6.33a1,1,0,0,1,0,1.34C12.18,8.8,9.79,11,7,11S1.82,8.8.77,7.67a1,1,0,0,1,0-1.34C1.82,5.2,4.21,3,7,3S12.18,5.2,13.23,6.33Z"
                                ></path>
                                <circle cx="7" cy="7" r="2"></circle>
                            </g>
                        </svg>
                        <svg
                            v-else
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 14 14"
                            fill="none"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            class="w-5 h-5 text-gray-500 stroke-current stroke-1"
                        >
                            <g>
                                <path
                                    d="M12.29,5.4c.38.34.7.67.94.93a1,1,0,0,1,0,1.34C12.18,8.8,9.79,11,7,11H6.6"
                                ></path>
                                <path
                                    d="M3.87,10.13A12.4,12.4,0,0,1,.77,7.67a1,1,0,0,1,0-1.34C1.82,5.2,4.21,3,7,3a6.56,6.56,0,0,1,3.13.87"
                                ></path>
                                <line
                                    x1="12.5"
                                    y1="1.5"
                                    x2="1.5"
                                    y2="12.5"
                                ></line>
                                <path
                                    d="M5.59,8.41A2,2,0,0,1,5,7,2,2,0,0,1,7,5a2,2,0,0,1,1.41.59"
                                ></path>
                                <path d="M8.74,8A2,2,0,0,1,8,8.73"></path>
                            </g>
                        </svg>
                    </button>
                    <error-message :errors="errors.password" />
                </div>

                <action-button
                    @click="handleLogin"
                    :processing="processingRequest"
                    class="w-full text-base btn-gray"
                >
                    Sign In
                </action-button>
            </form>

            <!-- Forgot Password Link -->
            <div class="mt-4 text-center">
                <router-link
                    :to="{ name: 'auth.forgot-password' }"
                    class="text-sm text-gray-600. capitalize"
                >
                    Forgot your password?
                </router-link>
            </div>

            <!-- Demo Credentials -->
            <div class="p-4 mt-6 rounded-lg bg-gray-50">
                <h3 class="mb-2 text-sm font-medium text-gray-700">
                    Demo Credentials:
                </h3>
                <div class="space-y-3 text-xs text-gray-600">
                    <p
                        class="cursor-pointer"
                        @click="fillDemoCredentials('admin')"
                    >
                        maria.rodriguez@ccp.com / password123
                    </p>
                    <p
                        class="cursor-pointer"
                        @click="fillDemoCredentials('nurse')"
                    >
                        jessica.thompson@ccp.com / password123
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Auth } from "@/models";
import TextInput from "@/components/form/TextInput.vue";
import ErrorMessage from "@/components/form/ErrorMessage.vue";
export default {
    components: {
        TextInput,
        ErrorMessage,
    },

    data() {
        return {
            error: "",
            errors: {},
            loginForm: {
                email: "",
                password: "",
            },
            showPassword: false,
            processingRequest: false,
        };
    },

    methods: {
        fillDemoCredentials(role) {
            const demoUsers = {
                admin: {
                    email: "maria.rodriguez@ccp.com",
                    password: "password123",
                },
                nurse: {
                    email: "jessica.thompson@ccp.com",
                    password: "password123",
                },
            };

            if (demoUsers[role]) {
                this.loginForm.email = demoUsers[role].email;
                this.loginForm.password = demoUsers[role].password;
            }
        },

        togglePasswordVisibility() {
            this.showPassword = !this.showPassword;
        },

        validationPassed() {
            this.errors = {};

            let validation = new this.Validator(this.loginForm, {
                email: "required|email",
                password: "required|min:6",
            });

            if (!validation.passes()) {
                this.errors = validation.errors.errors;

                document
                    .getElementById(`${Object.keys(this.errors)[0]}`)
                    ?.scrollIntoView({
                        behavior: "smooth",
                        block: "center",
                        inline: "start",
                    });
                return false;
            }

            return true;
        },

        handleLogin() {
            if (!this.validationPassed()) {
                return;
            }

            this.processingRequest = true;

            Auth.login(this.loginForm)
                .then((response) => {
                    this.$store.setUser(response.data.user);

                    this.$router.push({ name: "app.chat" });
                })
                .catch((error) => {
                    if (error.response && error.response.status === 400) {
                        this.error = error.response.data.message;
                        return;
                    }

                    this.error = "Login failed. Please check your credentials.";
                })
                .finally(() => {
                    this.processingRequest = false;
                });
        },
    },
};
</script>
