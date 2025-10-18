<template>
    <div class="min-h-screen bg-gradient-to-br from-slate-50 to-blue-50">
        <!-- Sidebar -->
        <div class="z-40">
            <!-- button to close sidebar -->
            <template v-if="sidebarOpen">
                <div
                    class="fixed inset-0 z-20 bg-blue-900 bg-opacity-75 md:hidden"
                ></div>
                <div class="relative inset-0 z-40 md:hidden">
                    <button
                        type="button"
                        @click="sidebarOpen = false"
                        class="fixed flex items-center justify-center right-8 top-8"
                    >
                        <i class="text-2xl text-white fas fa-close"></i>
                    </button>
                </div>
            </template>

            <Sidebar
                :sidebarOpen="sidebarOpen"
                :compressed="compressed"
                @close-sidebar="sidebarOpen = false"
                @toggleCompress="toggleCompress"
            />
        </div>

        <!-- Content area -->
        <div
            class="flex flex-col flex-1"
            :class="{ 'md:ml-24': compressed, 'md:ml-64': !compressed }"
        >
            <main class="flex-1">
                <slot></slot>
            </main>
        </div>
    </div>
</template>

<script>
import Sidebar from "@/components/Sidebar.vue";
import Header from "@/components/Header.vue";
export default {
    components: {
        Sidebar,
        Header,
    },

    data() {
        return {
            sidebarOpen: false,
            compressed: false,
        };
    },

    methods: {
        toggleCompress() {
            this.compressed = !this.compressed;
        },
    },
};
</script>
