<template>
    <div id="app">
        <component :is="currentLayout">
            <router-view v-slot="{ Component }">
                <transition
                    enter-active-class="transition-all duration-300 ease-out"
                    leave-active-class="transition-all duration-200 ease-in"
                    enter-from-class="translate-x-6 opacity-0"
                    leave-to-class="-translate-x-6 opacity-0"
                    mode="out-in"
                >
                    <component :is="Component" />
                </transition>
            </router-view>
        </component>
    </div>
</template>

<script>
import DefaultLayout from "@/layouts/DefaultLayout.vue";
import GuestLayout from "@/layouts/GuestLayout.vue";

export default {
    components: {
        DefaultLayout,
        GuestLayout,
    },

    created() {
        this.$store.init();
    },

    computed: {
        currentLayout() {
            return this.$route.meta.layout;
        },
    },
};
</script>
