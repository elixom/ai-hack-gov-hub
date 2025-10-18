<template>
    <div>
        <!-- Hero Section -->
        <section class="px-4 pt-32 pb-20">
            <div class="mx-auto max-w-7xl">
                <div class="grid items-center gap-12 md:grid-cols-2">
                    <div class="animate-fade-in-up">
                        <h1
                            class="mb-6 text-5xl font-bold leading-tight text-gray-900 md:text-6xl"
                        >
                            Navigate Jamaica's Government Services with
                            <span
                                class="text-transparent bg-gradient-to-r from-green-600 to-emerald-600 bg-clip-text"
                            >
                                AI Assistance
                            </span>
                        </h1>
                        <p class="mb-8 text-xl leading-relaxed text-gray-600">
                            Get step-by-step guidance for passport applications,
                            driver's licenses, business registrations, and more.
                            GovHub simplifies every government process.
                        </p>
                        <div class="flex flex-col gap-4 sm:flex-row">
                            <a
                                href="/register"
                                class="px-8 py-4 text-lg font-semibold text-center text-gray-900 transition-all transform rounded-full bg-gradient-to-r from-yellow-500 to-yellow-600 hover:shadow-2xl hover:scale-105"
                            >
                                Get Started Free
                            </a>
                            <button
                                @click="scrollToFeatures"
                                class="px-8 py-4 text-lg font-semibold text-center text-yellow-400 transition-all transform bg-gray-900 border-2 border-gray-900 rounded-full hover:shadow-xl hover:scale-105"
                            >
                                Learn More
                            </button>
                        </div>
                    </div>
                    <div class="relative animate-float">
                        <div
                            class="absolute inset-0 transform bg-gradient-to-r from-green-400 to-emerald-400 rounded-3xl rotate-3 opacity-20"
                        ></div>
                        <div
                            class="relative p-8 bg-white shadow-2xl rounded-3xl"
                        >
                            <div class="space-y-4">
                                <div
                                    class="flex items-start p-4 space-x-3 bg-yellow-50 rounded-xl"
                                >
                                    <div
                                        class="flex items-center justify-center flex-shrink-0 w-8 h-8 font-bold text-gray-900 bg-yellow-500 rounded-full"
                                    >
                                        1
                                    </div>
                                    <div>
                                        <h3 class="font-semibold text-gray-900">
                                            Select Your Process
                                        </h3>
                                        <p class="text-sm text-gray-600">
                                            Choose from dozens of government
                                            services
                                        </p>
                                    </div>
                                </div>
                                <div
                                    class="flex items-start p-4 space-x-3 bg-green-50 rounded-xl"
                                >
                                    <div
                                        class="flex items-center justify-center flex-shrink-0 w-8 h-8 font-bold text-white bg-green-600 rounded-full"
                                    >
                                        2
                                    </div>
                                    <div>
                                        <h3 class="font-semibold text-gray-900">
                                            AI Guides You
                                        </h3>
                                        <p class="text-sm text-gray-600">
                                            Get personalized step-by-step
                                            instructions
                                        </p>
                                    </div>
                                </div>
                                <div
                                    class="flex items-start p-4 space-x-3 bg-gray-100 rounded-xl"
                                >
                                    <div
                                        class="flex items-center justify-center flex-shrink-0 w-8 h-8 font-bold text-yellow-400 bg-gray-900 rounded-full"
                                    >
                                        3
                                    </div>
                                    <div>
                                        <h3 class="font-semibold text-gray-900">
                                            Complete with Ease
                                        </h3>
                                        <p class="text-sm text-gray-600">
                                            Navigate the process confidently
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Features Section -->
        <section id="features" class="px-4 py-20 bg-white">
            <div class="mx-auto max-w-7xl">
                <div class="mb-16 text-center">
                    <h2 class="mb-4 text-4xl font-bold text-gray-900">
                        Why Choose GovHub?
                    </h2>
                    <p class="text-xl text-gray-600">
                        Simplifying government processes for every Jamaican
                    </p>
                </div>
                <div class="grid gap-8 md:grid-cols-3">
                    <div
                        class="p-8 transition-shadow bg-gradient-to-br from-blue-50 to-indigo-50 rounded-2xl hover:shadow-xl"
                    >
                        <div
                            class="flex items-center justify-center mb-4 bg-indigo-600 w-14 h-14 rounded-xl"
                        >
                            <svg
                                class="w-8 h-8 text-gray-900"
                                fill="none"
                                stroke="currentColor"
                                viewBox="0 0 24 24"
                            >
                                <path
                                    stroke-linecap="round"
                                    stroke-linejoin="round"
                                    stroke-width="2"
                                    d="M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z"
                                ></path>
                            </svg>
                        </div>
                        <h3 class="mb-3 text-2xl font-bold text-gray-900">
                            AI-Powered Guidance
                        </h3>
                        <p class="text-gray-600">
                            Our intelligent system understands your needs and
                            provides tailored instructions for each government
                            process.
                        </p>
                    </div>
                    <div
                        class="p-8 transition-shadow bg-gradient-to-br from-purple-50 to-pink-50 rounded-2xl hover:shadow-xl"
                    >
                        <div
                            class="flex items-center justify-center mb-4 bg-purple-600 w-14 h-14 rounded-xl"
                        >
                            <svg
                                class="w-8 h-8 text-white"
                                fill="none"
                                stroke="currentColor"
                                viewBox="0 0 24 24"
                            >
                                <path
                                    stroke-linecap="round"
                                    stroke-linejoin="round"
                                    stroke-width="2"
                                    d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253"
                                ></path>
                            </svg>
                        </div>
                        <h3 class="mb-3 text-2xl font-bold text-gray-900">
                            Comprehensive Database
                        </h3>
                        <p class="text-gray-600">
                            Access information on all major Jamaican government
                            services, from Tax Registration to Birth
                            Certificates.
                        </p>
                    </div>
                    <div
                        class="p-8 transition-shadow bg-gradient-to-br from-green-50 to-teal-50 rounded-2xl hover:shadow-xl"
                    >
                        <div
                            class="flex items-center justify-center mb-4 bg-green-600 w-14 h-14 rounded-xl"
                        >
                            <svg
                                class="w-8 h-8 text-white"
                                fill="none"
                                stroke="currentColor"
                                viewBox="0 0 24 24"
                            >
                                <path
                                    stroke-linecap="round"
                                    stroke-linejoin="round"
                                    stroke-width="2"
                                    d="M13 10V3L4 14h7v7l9-11h-7z"
                                ></path>
                            </svg>
                        </div>
                        <h3 class="mb-3 text-2xl font-bold text-gray-900">
                            Fast & Reliable
                        </h3>
                        <p class="text-gray-600">
                            Get instant answers and up-to-date information. No
                            more waiting in lines for basic questions.
                        </p>
                    </div>
                </div>
            </div>
        </section>

        <!-- CTA Section -->
        <section
            class="px-4 py-20 bg-gradient-to-r from-green-600 to-emerald-600"
        >
            <div class="max-w-4xl mx-auto text-center">
                <h2 class="mb-6 text-4xl font-bold text-white">
                    Ready to Simplify Your Government Tasks?
                </h2>
                <p class="mb-8 text-xl text-green-100">
                    Join thousands of Jamaicans who trust GovHub for their
                    government service needs.
                </p>
                <a
                    href="/register"
                    class="inline-block px-10 py-4 text-lg font-bold text-gray-900 transition-all transform bg-yellow-400 rounded-full hover:shadow-2xl hover:scale-105"
                >
                    Start Using GovHub Today
                </a>
            </div>
        </section>
    </div>
</template>
<style>
@keyframes float {
    0%,
    100% {
        transform: translateY(0px);
    }
    50% {
        transform: translateY(-20px);
    }
}

.animate-float {
    animation: float 3s ease-in-out infinite;
}

@keyframes fade-in-up {
    from {
        opacity: 0;
        transform: translateY(30px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}

.animate-fade-in-up {
    animation: fade-in-up 0.8s ease-out forwards;
}
</style>
