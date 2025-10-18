<template>
    <label class="block mb-2 text-sm font-medium text-gray-700">
        <div class="flex items-center space-x-1">
            <slot />
            <required-label v-if="required" />

            <span v-if="tip" class="relative group">
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    class="w-4 h-4 cursor-pointer text-blue-500"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                >
                    <circle cx="12" cy="12" r="10" fill="currentColor" />
                    <text
                        x="12"
                        y="16"
                        text-anchor="middle"
                        font-size="12"
                        fill="white"
                        font-family="Arial, sans-serif"
                        font-weight="bold"
                    >
                        i
                    </text>
                </svg>

                <span
                    class="absolute left-1/2 -translate-x-1/2 mt-1 w-48 bg-blue-100 text-blue-900 text-xs rounded-md p-2 shadow-lg opacity-0 group-hover:opacity-100 transition-opacity duration-200 z-10 whitespace-normal"
                >
                    {{ tip }}
                </span>
            </span>
        </div>
    </label>
</template>

<script>
import RequiredLabel from "@/components/form/RequiredLabel.vue";

export default {
    props: {
        required: Boolean,
        tip: String, // optional tooltip text
    },
    components: { RequiredLabel },
};
</script>
