<template>
    <div>
        <div class="space-y-8">
            <!-- Personal Information Section -->
            <div
                class="overflow-hidden bg-white border border-blue-200 shadow-sm rounded-xl"
            >
                <div
                    class="px-6 py-4 border-b border-blue-200 bg-gradient-to-r from-blue-50 to-indigo-50"
                >
                    <div class="flex items-center space-x-3">
                        <div class="flex-shrink-0">
                            <div
                                class="flex items-center justify-center w-10 h-10 bg-blue-100 rounded-lg"
                            >
                                <svg
                                    class="w-6 h-6 text-blue-600"
                                    fill="currentColor"
                                    viewBox="0 0 24 24"
                                    stroke-width="1.5"
                                    stroke="currentColor"
                                >
                                    <path
                                        stroke-linecap="round"
                                        stroke-linejoin="round"
                                        d="M15.75 6a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0ZM4.501 20.118a7.5 7.5 0 0 1 14.998 0A17.933 17.933 0 0 1 12 21.75c-2.676 0-5.216-.584-7.499-1.632Z"
                                    />
                                </svg>
                            </div>
                        </div>
                        <div>
                            <h2 class="text-lg font-semibold text-gray-900">
                                Personal Information
                            </h2>
                            <p class="text-sm text-gray-600">
                                Your basic account details
                            </p>
                        </div>
                    </div>
                </div>

                <form
                    @submit.prevent
                    @keydown="handleFormChange"
                    @change="handleFormChange"
                    class="p-6"
                >
                    <div class="grid grid-cols-1 gap-6 lg:grid-cols-2">
                        <!-- Name -->
                        <div class="space-y-2">
                            <label
                                class="block text-sm font-medium text-gray-700"
                                >Full Name</label
                            >
                            <div
                                class="p-3 border border-gray-200 rounded-lg bg-gray-50"
                            >
                                <p>
                                    {{ user.first_name }}
                                    {{ user.last_name }}
                                </p>
                            </div>
                        </div>

                        <!-- Email -->
                        <div class="space-y-2">
                            <label
                                class="block text-sm font-medium text-gray-700"
                                >Email Address</label
                            >
                            <div
                                class="p-3 border border-gray-200 rounded-lg bg-gray-50"
                            >
                                <p class="text-gray-900">
                                    {{ user.email }}
                                </p>
                            </div>
                        </div>

                        <!-- Department -->
                        <div class="space-y-2">
                            <label
                                class="block text-sm font-medium text-gray-700"
                                >Department</label
                            >
                            <div
                                class="p-3 border border-gray-200 rounded-lg bg-gray-50"
                            >
                                <p class="text-gray-900">
                                    {{ user.department }}
                                </p>
                            </div>
                        </div>

                        <!-- Specialization -->
                        <div class="space-y-2">
                            <label
                                class="block text-sm font-medium text-gray-700"
                                >Specialization</label
                            >
                            <div
                                class="p-3 border border-gray-200 rounded-lg bg-gray-50"
                            >
                                <p class="text-gray-900">
                                    {{ user.specialization }}
                                </p>
                            </div>
                        </div>

                        <!-- Phone -->
                        <div class="space-y-2">
                            <label
                                class="flex items-center justify-between text-sm font-medium text-gray-700"
                            >
                                <span>Phone</span>
                                <button
                                    v-if="false"
                                    type="button"
                                    @click="toggleEdit('phone')"
                                    class="inline-flex items-center px-3 py-1 space-x-1 text-xs font-medium text-blue-600 transition-colors duration-200 rounded-md bg-blue-50 /20 hover:bg-blue-100"
                                >
                                    <svg
                                        class="w-3 h-3"
                                        fill="none"
                                        viewBox="0 0 24 24"
                                        stroke-width="1.5"
                                        stroke="currentColor"
                                    >
                                        <path
                                            stroke-linecap="round"
                                            stroke-linejoin="round"
                                            d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10"
                                        />
                                    </svg>
                                    <span>Edit</span>
                                </button>
                            </label>

                            <div
                                v-if="!editField.includes('phone')"
                                class="p-3 border border-gray-200 rounded-lg bg-gray-50"
                            >
                                <p class="text-gray-900">
                                    {{ user.phone || "Not provided" }}
                                </p>
                            </div>

                            <div v-else class="space-y-2">
                                <phone-number
                                    v-model="user.phone"
                                    :error="errors.phone"
                                    autocomplete="off"
                                    id="phone"
                                    name="phone"
                                    class="block w-full border-gray-300 rounded-lg shadow-sm focus:border-blue-500 focus:ring-blue-500"
                                />
                                <p
                                    v-if="errors.phone"
                                    class="text-sm text-red-600"
                                >
                                    {{ errors.phone[0] }}
                                </p>
                            </div>
                        </div>
                    </div>

                    <div
                        v-if="false"
                        class="flex justify-end pt-6 mt-8 border-t border-gray-200"
                    >
                        <action-button
                            :processing="processingProfileRequest"
                            @click="updateProfile"
                            class="px-6 py-2.5 btn-primary"
                        >
                            Save Changes
                        </action-button>
                    </div>
                </form>
            </div>

            <!-- Password Section -->
            <div
                class="overflow-hidden bg-white border shadow-sm border-amber-200 rounded-xl"
            >
                <div
                    class="px-6 py-4 border-b border-amber-200 bg-gradient-to-r from-amber-50 to-orange-50"
                >
                    <div class="flex items-center space-x-3">
                        <div class="flex-shrink-0">
                            <div
                                class="flex items-center justify-center w-10 h-10 rounded-lg bg-amber-100"
                            >
                                <svg
                                    class="w-6 h-6 text-amber-600"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    stroke-width="1.5"
                                    stroke="currentColor"
                                >
                                    <path
                                        stroke-linecap="round"
                                        stroke-linejoin="round"
                                        d="M15.75 5.25a3 3 0 0 1 3 3m3 0a6 6 0 0 1-7.029 5.912c-.563-.097-1.159.026-1.563.43L10.5 17.25H8.25v2.25H6v2.25H2.25v-2.818c0-.597.237-1.17.659-1.591l6.499-6.499c.404-.404.527-1 .43-1.563A6 6 0 1 1 21.75 8.25Z"
                                    />
                                </svg>
                            </div>
                        </div>
                        <div>
                            <h2 class="text-lg font-semibold text-gray-900">
                                Change Password
                            </h2>
                            <p class="text-sm text-gray-600">
                                Update your account password
                            </p>
                        </div>
                    </div>
                </div>

                <form
                    @submit.prevent
                    @keydown="handleFormChange"
                    @change="handleFormChange"
                    class="p-6"
                >
                    <div
                        class="p-4 mb-6 border border-blue-200 rounded-lg bg-blue-50 /20"
                    >
                        <p class="text-sm text-sky-600">
                            <strong>Security tip:</strong> Use a long, unique
                            password with a mix of letters, numbers, and special
                            characters.
                        </p>
                    </div>

                    <div class="space-y-6">
                        <!-- Current Password -->
                        <div class="max-w-md space-y-2">
                            <field-label for="current-password">
                                Current Password
                            </field-label>

                            <div class="relative">
                                <text-input
                                    v-model="form.password"
                                    :type="showPassword ? 'text' : 'password'"
                                    id="password"
                                    name="password"
                                    autocomplete="current-password"
                                    :error="errors.password"
                                    class="focus:border-amber-500 focus:ring-amber-500"
                                />

                                <button
                                    type="button"
                                    @click="togglePasswordVisibility"
                                    class="absolute p-1 top-1.5 right-2"
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
                                            <circle
                                                cx="7"
                                                cy="7"
                                                r="2"
                                            ></circle>
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
                                            <path
                                                d="M8.74,8A2,2,0,0,1,8,8.73"
                                            ></path>
                                        </g>
                                    </svg>
                                </button>
                            </div>
                            <error-message :errors="errors.password" />
                        </div>

                        <!-- New Password -->
                        <div class="max-w-md space-y-2">
                            <field-label for="new-password">
                                New Password
                            </field-label>

                            <div class="relative">
                                <text-input
                                    v-model="form.new_password"
                                    :type="showPassword ? 'text' : 'password'"
                                    id="new_password"
                                    name="new_password"
                                    autocomplete="new-password"
                                    :error="errors.new_password"
                                    class="focus:border-amber-500 focus:ring-amber-500"
                                />
                                <button
                                    type="button"
                                    @click="togglePasswordVisibility"
                                    class="absolute p-1 top-1.5 right-2"
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
                                            <circle
                                                cx="7"
                                                cy="7"
                                                r="2"
                                            ></circle>
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
                                            <path
                                                d="M8.74,8A2,2,0,0,1,8,8.73"
                                            ></path>
                                        </g>
                                    </svg>
                                </button>
                            </div>
                            <error-message :errors="errors.new_password" />
                        </div>
                    </div>

                    <div
                        class="flex justify-end pt-6 mt-8 border-t border-gray-200"
                    >
                        <action-button
                            :processing="processingPasswordRequest"
                            @click="updatePassword"
                            class="px-6 py-2.5 btn-warning"
                        >
                            Update Password
                        </action-button>
                    </div>
                </form>
            </div>

            <!-- Active Sessions Section -->
            <div
                class="overflow-hidden bg-white border border-purple-200 shadow-sm rounded-xl"
            >
                <div
                    class="px-6 py-4 border-b border-purple-200 bg-gradient-to-r from-purple-50 to-pink-50"
                >
                    <div class="flex items-center space-x-3">
                        <div class="flex-shrink-0">
                            <div
                                class="flex items-center justify-center w-10 h-10 bg-purple-100 rounded-lg"
                            >
                                <svg
                                    class="w-6 h-6 text-purple-600"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    stroke-width="1.5"
                                    stroke="currentColor"
                                >
                                    <path
                                        stroke-linecap="round"
                                        stroke-linejoin="round"
                                        d="M5.25 14.25h13.5m-13.5 0a3 3 0 0 1-3-3m3 3a3 3 0 1 0 0 6h13.5a3 3 0 1 0 0-6m-16.5-3a3 3 0 0 1 3-3h13.5a3 3 0 0 1 3 3m-19.5 0a4.5 4.5 0 0 1 .9-2.7L5.737 5.1a3.375 3.375 0 0 1 2.7-1.35h7.126c1.062 0 2.062.5 2.7 1.35l2.587 3.45a4.5 4.5 0 0 1 .9 2.7m0 0a3 3 0 0 1-3 3m0 3h.008v.008h-.008v-.008Zm0-6h.008v.008h-.008v-.008Zm-3 6h.008v.008h-.008v-.008Zm0-6h.008v.008h-.008v-.008Z"
                                    />
                                </svg>
                            </div>
                        </div>
                        <div>
                            <h2 class="text-lg font-semibold text-gray-900">
                                Active Sessions
                            </h2>
                            <p class="text-sm text-gray-600">
                                Manage your active sessions across devices
                            </p>
                        </div>
                    </div>
                </div>

                <div class="p-6">
                    <div
                        class="p-4 mb-6 border rounded-lg bg-amber-50 /20 border-amber-200"
                    >
                        <div class="flex items-start space-x-3">
                            <i
                                class="w-5 h-5 fas fa-exclamation-triangle text-amber-500"
                            ></i>
                            <div>
                                <p class="text-sm text-amber-800">
                                    If you notice any suspicious activity or
                                    unrecognized devices, log out of all other
                                    sessions immediately and change your
                                    password.
                                </p>
                            </div>
                        </div>
                    </div>

                    <!-- Sessions List -->
                    <div v-if="sessions.length > 0" class="space-y-4">
                        <div
                            v-for="(session, i) in sessions"
                            :key="i"
                            class="flex items-center justify-between p-4 border border-gray-200 rounded-lg bg-gray-50"
                        >
                            <div class="flex items-center space-x-4">
                                <div class="flex-shrink-0">
                                    <!-- Desktop Icon -->
                                    <div
                                        v-if="session.agent.is_desktop"
                                        class="flex items-center justify-center w-10 h-10 bg-blue-100 rounded-lg"
                                    >
                                        <svg
                                            class="w-6 h-6 text-blue-600"
                                            fill="none"
                                            viewBox="0 0 24 24"
                                            stroke-width="1.5"
                                            stroke="currentColor"
                                        >
                                            <path
                                                stroke-linecap="round"
                                                stroke-linejoin="round"
                                                d="M9 17.25v1.007a3 3 0 01-.879 2.122L7.5 21h9l-.621-.621A3 3 0 0115 18.257V17.25m6-12V15a2.25 2.25 0 01-2.25 2.25H5.25A2.25 2.25 0 013 15V5.25m18 0A2.25 2.25 0 0018.75 3H5.25A2.25 2.25 0 003 5.25m18 0V12a2.25 2.25 0 01-2.25 2.25H5.25A2.25 2.25 0 013 12V5.25"
                                            />
                                        </svg>
                                    </div>
                                    <!-- Phone Icon -->
                                    <div
                                        v-else
                                        class="flex items-center justify-center w-10 h-10 bg-green-100 rounded-lg"
                                    >
                                        <svg
                                            class="w-6 h-6 text-green-600"
                                            fill="none"
                                            viewBox="0 0 24 24"
                                            stroke-width="1.5"
                                            stroke="currentColor"
                                        >
                                            <path
                                                stroke-linecap="round"
                                                stroke-linejoin="round"
                                                d="M10.5 1.5H8.25A2.25 2.25 0 006 3.75v16.5a2.25 2.25 0 002.25 2.25h7.5A2.25 2.25 0 0018 20.25V3.75a2.25 2.25 0 00-2.25-2.25H13.5m-3 0V3h3V1.5m-3 0h3m-3 18.75h3"
                                            />
                                        </svg>
                                    </div>
                                </div>

                                <div class="flex-1">
                                    <div class="flex items-center space-x-2">
                                        <h4
                                            class="text-sm font-medium text-gray-900"
                                        >
                                            {{
                                                session.agent.platform ||
                                                "Unknown Platform"
                                            }}
                                        </h4>
                                        <span class="text-gray-400">•</span>
                                        <span class="text-sm text-gray-600">
                                            {{
                                                session.agent.browser ||
                                                "Unknown Browser"
                                            }}
                                        </span>
                                    </div>
                                    <div
                                        class="flex items-center mt-1 space-x-2"
                                    >
                                        <span class="text-xs text-gray-500">
                                            {{ session.ip_address }}
                                        </span>
                                        <span class="text-gray-400">•</span>
                                        <span
                                            v-if="session.is_current_device"
                                            class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800"
                                        >
                                            Current Device
                                        </span>
                                        <span
                                            v-else
                                            class="text-xs text-gray-500"
                                        >
                                            Last active
                                            {{ session.last_active }}
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div v-else class="py-8 text-center">
                        <svg
                            class="w-12 h-12 mx-auto text-gray-400"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke-width="1.5"
                            stroke="currentColor"
                        >
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                d="M5.25 14.25h13.5m-13.5 0a3 3 0 0 1-3-3m3 3a3 3 0 1 0 0 6h13.5a3 3 0 1 0 0-6m-16.5-3a3 3 0 0 1 3-3h13.5a3 3 0 0 1 3 3m-19.5 0a4.5 4.5 0 0 1 .9-2.7L5.737 5.1a3.375 3.375 0 0 1 2.7-1.35h7.126c1.062 0 2.062.5 2.7 1.35l2.587 3.45a4.5 4.5 0 0 1 .9 2.7m0 0a3 3 0 0 1-3 3m0 3h.008v.008h-.008v-.008Zm0-6h.008v.008h-.008v-.008Zm-3 6h.008v.008h-.008v-.008Zm0-6h.008v.008h-.008v-.008Z"
                            />
                        </svg>
                        <p class="mt-2 text-sm text-gray-500">
                            No active sessions found
                        </p>
                    </div>

                    <div
                        class="flex justify-end pt-6 mt-8 border-t border-gray-200"
                    >
                        <action-button
                            @click="logoutOtherSessions"
                            :processing="loggingOutSessions"
                            :disabled="sessions.length <= 1"
                            class="px-6 py-2.5 btn-danger"
                        >
                            Log Out Other Sessions
                        </action-button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Auth, Session } from "@/models";
import TextInput from "@/components/form/TextInput.vue";
import PhoneNumber from "@/components/form/PhoneNumber.vue";
import ErrorMessage from "@/components/form/ErrorMessage.vue";
import FieldLabel from "@/components/form/FieldLabel.vue";

export default {
    components: {
        PhoneNumber,
        TextInput,
        ErrorMessage,
        FieldLabel,
    },

    data() {
        return {
            user: {},
            form: {},
            sessions: [],
            errors: {},
            processingProfileRequest: false,
            processingPasswordRequest: false,
            loggingOutSessions: false,
            editField: [],
            showPassword: false,
        };
    },

    mounted() {
        this.fetchUser();
        this.fetchSessions();
    },

    methods: {
        togglePasswordVisibility() {
            this.showPassword = !this.showPassword;
        },

        fetchUser() {
            this.user = this.$store.user;
        },

        fetchSessions() {
            Session.getAll().then((response) => {
                if (response.redirected) return; // user was redirected due to 401/403
                this.sessions = response.data.sessions;
            });
        },

        logoutOtherSessions() {
            this.loggingOutSessions = true;

            Session.closeOthers()
                .then((response) => {
                    if (response.redirected) return; // user was redirected due to 401/403

                    this.$toast.success("Sessions have been closed");

                    this.sessions = this.sessions.filter(
                        (session) => session.is_current_device
                    );
                })
                .catch((error) => {
                    console.error(error);
                    this.$toast.error(
                        "Error logging out of other sessions. Please try again."
                    );
                })
                .finally(() => {
                    this.loggingOutSessions = false;
                });
        },

        toggleEdit(field) {
            if (this.editField.includes(field)) {
                return this.editField.pop(field);
            }

            return this.editField.push(field);
        },

        updateProfile() {
            this.processingProfileRequest = true;

            let validation = new this.Validator(this.user, this.profile_rules);

            if (validation.passes()) {
                this.errors = {};

                return Auth.updateProfile(this.user)
                    .then((response) => {
                        if (response.redirected) return; // user was redirected due to 401/403

                        this.$store.setUserInfo(response.data.user);
                        this.$store.setFormChangedStatus(false);

                        this.editField = [];

                        this.$toast.success(
                            "Your profile has been successfully updated"
                        );
                    })
                    .catch((error) => {
                        if (error.response.status === 422) {
                            return (this.errors = error.response.data.errors);
                        }

                        this.$toast.error(this.messages.general);
                    })
                    .finally(() => {
                        this.processingProfileRequest = false;
                    });
            }

            this.errors = validation.errors.errors;
            this.processingProfileRequest = false;
        },

        updatePassword() {
            this.processingPasswordRequest = true;

            let validation = new this.Validator(this.form, {
                password: "required|min:8",
                new_password: "required|min:8",
            });

            if (validation.passes()) {
                this.errors = {};

                return Auth.updatePassword(this.form)
                    .then((response) => {
                        if (response.redirected) return; // user was redirected due to 401/403

                        this.form = {};
                        this.$store.setFormChangedStatus(false);

                        this.$toast.success(
                            "Your password has been successfully updated"
                        );
                    })
                    .catch((error) => {
                        if (error.response.status === 422) {
                            return (this.errors = error.response.data.errors);
                        }

                        this.$toast.error(
                            "Error updating password. Please try again."
                        );
                    })
                    .finally(() => {
                        this.processingPasswordRequest = false;
                    });
            }

            this.errors = validation.errors.errors;
            this.processingPasswordRequest = false;
        },

        handleFormChange(event) {
            this.$store.setFormChangedStatus(true);
            delete this.errors[event.target.name];
        },
    },
};
</script>
