<template>
    <div class="flex items-center justify-center py-20">
        <div class="text-center">
            <img class="h-[30rem]" src="/img/looking_for_me.gif" alt="" />

            <template>
                <div
                    class="flex items-center justify-center w-24 h-24 mx-auto mb-6 bg-gray-200 rounded-full"
                >
                    <i
                        class="text-3xl text-gray-500 fas fa-exclamation-triangle"
                    ></i>
                </div>
                <h1 class="mb-4 text-4xl font-bold text-gray-900">404</h1>
                <h2 class="mb-4 text-xl font-medium text-gray-700">
                    Page Not Found
                </h2>
                <p class="max-w-md mx-auto mb-8 text-gray-600">
                    The page you're looking for doesn't exist or has been moved.
                </p>
                <router-link
                    to="/chat"
                    class="inline-flex items-center px-6 py-3 space-x-2 text-white bg-blue-600 rounded-lg hover:bg-blue-700"
                >
                    <i class="fas fa-home"></i>
                    <span>Go to Chat</span>
                </router-link>
            </template>
        </div>
    </div>
</template>

<script setup>
// No additional logic needed
</script>
