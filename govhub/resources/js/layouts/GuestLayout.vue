<template>
    <div
        class="flex flex-col min-h-screen bg-gradient-to-br from-yellow-50 via-green-50 to-emerald-50"
    >
        <!-- Navigation -->
        <nav
            :class="[
                'fixed w-full top-0 z-50 transition-all duration-300',
                isScrolled
                    ? 'bg-white/90 backdrop-blur-lg shadow-md'
                    : 'bg-white/10 backdrop-blur-sm',
            ]"
        >
            <div class="px-4 mx-auto max-w-7xl sm:px-6 lg:px-8">
                <div class="flex items-center justify-between h-16">
                    <router-link
                        :to="{ name: 'app.home' }"
                        class="flex items-center space-x-2"
                    >
                        <div
                            class="flex items-center justify-center w-10 h-10 text-xl font-bold text-white rounded-lg bg-gradient-to-br from-yellow-400 to-green-500"
                        >
                            GH
                        </div>
                        <span class="text-2xl font-bold text-gray-800"
                            >GovHub</span
                        >
                    </router-link>
                    <div class="flex items-center space-x-4">
                        <router-link
                            :to="{ name: 'auth.login' }"
                            class="font-medium text-gray-700 transition-colors hover:text-indigo-600"
                        >
                            Login
                        </router-link>
                        <a
                            href="/register"
                            class="px-6 py-2 font-medium text-gray-900 transition-all transform rounded-full bg-gradient-to-r from-yellow-500 to-yellow-600 hover:shadow-lg hover:scale-105"
                        >
                            Register
                        </a>
                    </div>
                </div>
            </div>
        </nav>

        <div class="flex-1">
            <slot></slot>
        </div>

        <!-- Footer -->
        <footer class="px-4 py-12 text-gray-300 bg-gray-900">
            <div class="mx-auto text-center max-w-7xl">
                <div class="flex items-center justify-center mb-4 space-x-2">
                    <div
                        class="flex items-center justify-center w-10 h-10 text-xl font-bold text-white rounded-lg bg-gradient-to-br from-yellow-400 to-green-500"
                    >
                        GH
                    </div>
                    <span class="text-2xl font-bold text-white">GovHub</span>
                </div>
                <p class="mb-4 text-gray-400">
                    Simplifying government processes for all Jamaicans
                </p>
                <p class="text-sm text-gray-500">
                    &copy; 2025 GovHub. All rights reserved.
                </p>
            </div>
        </footer>
    </div>
</template>
<script>
export default {
    data() {
        return {
            isScrolled: false,
        };
    },
    mounted() {
        window.addEventListener("scroll", this.handleScroll);
    },
    beforeUnmount() {
        window.removeEventListener("scroll", this.handleScroll);
    },
    methods: {
        handleScroll() {
            this.isScrolled = window.scrollY > 20;
        },
        scrollToFeatures() {
            const featuresSection = document.getElementById("features");
            if (featuresSection) {
                featuresSection.scrollIntoView({ behavior: "smooth" });
            }
        },
    },
};
</script>
