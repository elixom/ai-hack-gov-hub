<template>
    <div
        class="fixed inset-y-0 left-0 z-30 flex flex-col text-white transition-all duration-300 transform shadow-xl w-72 bg-gradient-to-b from-gray-800 to-gray-900 md:translate-x-0"
        :class="{
            '-translate-x-full': !sidebarOpen,
            'md:w-24': compressed,
            'md:w-64': !compressed,
        }"
    >
        <div
            class="flex items-center justify-center flex-shrink-0 border-b border-blue-100/50"
        >
            <h1 class="py-4 text-2xl font-bold text-white drop-shadow-md">
                GovHub
            </h1>

            <button
                @click="$emit('close-sidebar')"
                class="text-white md:hidden focus:outline-none"
            >
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    class="size-6"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        stroke-width="2"
                        d="M6 18L18 6M6 6l12 12"
                    />
                </svg>
            </button>
        </div>
        <nav
            @click="$emit('close-sidebar')"
            class="flex-1 min-h-0 py-4 overflow-hidden overflow-y-auto"
        >
            <div class="px-2 space-y-5">
                <router-link
                    v-for="item in menuItems"
                    :key="item.route"
                    :to="{ name: item.route }"
                    class="flex items-center w-full p-2 font-medium text-gray-100 transition-all duration-200 rounded-lg group hover:bg-blue-400/30 hover:border-l-4 hover:border-sky-300"
                    :class="{
                        'bg-sky-500/70 border-l-4 border-sky-300':
                            isActive(item),
                        'md:text-xl md:justify-center': compressed,
                    }"
                >
                    <i :class="`${item.icon}  mr-2`"></i>
                    <span :class="{ 'md:hidden': compressed }">{{
                        item.name
                    }}</span>
                </router-link>

                <button
                    @click="$store.logout()"
                    class="flex items-center w-full gap-2 p-2 font-medium text-gray-100 transition-all duration-200 rounded-lg group hover:bg-blue-400/30 hover:border-l-4 hover:border-sky-300"
                    :class="{
                        'md:text-xl md:justify-center': compressed,
                    }"
                >
                    <i class="fas fa-sign-out-alt"></i>
                    Logout
                </button>
            </div>
        </nav>

        <button
            @click="$emit('toggleCompress')"
            class="hidden p-4 text-right md:block"
            :class="{
                'md:text-center': compressed,
            }"
        >
            <i
                class="text-xl transition-all transform fas fa-chevron-left"
                :class="{ 'rotate-180': compressed }"
            ></i>
        </button>
    </div>
</template>

<script>
export default {
    props: {
        sidebarOpen: {
            type: Boolean,
            default: false,
        },
        compressed: {
            type: Boolean,
            default: false,
        },
    },

    data() {
        return {
            menuItems: [
                // {
                //     name: "Dashboard",
                //     route: "dashboard",
                //     key: "dashboard",
                //     icon: "fas fa-home",
                //     permission: "",
                // },
            ],
        };
    },

    methods: {
        isActive(item) {
            // Handle route name exactly
            if (this.$route.name === item.route) {
                return true;
            }

            // For other routes, check if current route starts with menu route
            if (this.$route.path.includes(item.key)) {
                return true;
            }

            return false;
        },
    },
};
</script>
