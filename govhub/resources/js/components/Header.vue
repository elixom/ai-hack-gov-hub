<template>
    <div class="flex items-center justify-between w-full h-16 px-4">
        <div class="items-center hidden px-4 text-gray-800 md:flex">
            <button @click="$router.go(-1)">
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke-width="2"
                    stroke="currentColor"
                    class="w-6 mr-4 cursor-pointer"
                >
                    <path
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        d="M10.5 19.5 3 12m0 0 7.5-7.5M3 12h18"
                    />
                </svg>
            </button>
            <h2 class="text-lg font-semibold">
                {{ pageTitle }}
            </h2>
        </div>

        <div class="md:hidden"></div>

        <div class="flex items-center space-x-4">
            <!-- Notifications -->
            <button
                class="relative hidden p-2 text-gray-500 rounded-lg hover:text-gray-700 hover:bg-gray-100"
            >
                <i class="fas fa-bell"></i>
                <span
                    v-if="notificationCount > 0"
                    class="absolute top-0 right-0 block w-2 h-2 bg-red-400 rounded-full ring-2 ring-white"
                ></span>
            </button>

            <!-- User menu -->
            <div class="relative">
                <button
                    @click="toggleUserMenu"
                    class="flex items-center p-2 space-x-3 transition-colors duration-200 rounded-lg hover:bg-gray-100"
                >
                    <div
                        class="flex items-center justify-center w-8 h-8 bg-teal-300 rounded-full"
                    >
                        <i class="text-sm text-teal-600 fas fa-user"></i>
                    </div>
                    <div class="hidden text-left md:block">
                        <p class="text-sm font-medium text-gray-900">
                            {{ $store.user?.first_name }}
                            {{ $store.user?.last_name }}
                        </p>
                        <p class="text-xs text-gray-500 capitalize truncate">
                            {{
                                $store.activeLocation?.name
                            }}
                        </p>
                    </div>
                    <i class="text-xs text-gray-400 fas fa-chevron-down"></i>
                </button>

                <!-- Dropdown menu -->
                <div
                    v-if="showUserMenu"
                    @click="toggleUserMenu"
                    class="absolute right-0 z-50 w-48 py-1 mt-2 bg-white border border-gray-200 rounded-lg shadow-lg"
                >
                    <router-link
                        :to="{ name: 'auth.user-profile' }"
                        class="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    >
                        <i class="mr-3 fas fa-user"></i>
                        Profile
                    </router-link>
                    <router-link
                        :to="{ name: 'app.locations' }"
                        class="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    >
                        <i class="mr-2 fas fa-map"></i>
                        Change Location
                    </router-link>
                    <button
                        type="button"
                        @click="$store.logout()"
                        class="flex items-center w-full px-4 py-2 text-sm text-gray-700 cursor-pointer hover:bg-gray-100"
                    >
                        <i class="mr-3 fas fa-sign-out-alt"></i>
                        Logout
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            showUserMenu: false,
            clickListener: null,
        };
    },

    mounted() {
        this.clickListener = this.handleClickOutside;
        document.addEventListener("click", this.clickListener);
    },

    beforeUnmount() {
        document.removeEventListener("click", this.clickListener);
    },

    computed: {
        user() {
            return this.$store.user;
        },

        pageTitle() {
            return this.$route.meta.title;
        },

        notificationCount() {
            return 0; // Placeholder for notifications
        },
    },

    methods: {
        toggleUserMenu() {
            this.showUserMenu = !this.showUserMenu;
        },

        handleClickOutside(event) {
            const target = event.target;
            if (!target.closest(".relative")) {
                this.showUserMenu = false;
            }
        },
    },
};
</script>
