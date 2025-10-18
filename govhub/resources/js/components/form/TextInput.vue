<template>
    <input
        :value="modelValue"
        @input="updateValue($event.target.value)"
        class="block w-full px-3 py-2 border rounded-md appearance-none focus:outline-none sm:text-sm focus:border-2"
        :class="{
            ' text-red-900 placeholder-red-300 border-red-300 focus:border-red-500 ':
                error,
            'placeholder-gray-400 text-gray-800 border-gray-300 focus:border-sky-400':
                !error,
        }"
    />
</template>

<script>
export default {
    props: ["modelValue", "error"],
    methods: {
        updateValue: function (value) {
            this.$emit("update:modelValue", value);
        },
    },
};
</script>
