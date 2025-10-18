<template>
    <select
        v-model="model"
        class="block w-full px-3 py-2 border rounded-md appearance-none focus:outline-none focus:ring-0 focus:border-2 sm:text-sm disabled:bg-gray-200"
        :class="{
            ' text-red-900 placeholder-red-300 border-red-300 focus:border-red-500':
                error,
            '  border-gray-300  focus:border-sky-400 text-gray-800 ': !error,
        }"
    >
        <option
            v-if="includeDefault"
            value=""
            :selected="modelValue == undefined || modelValue == null"
        >
            {{ defaultLabel }}
        </option>
        <option
            v-for="(item, id) in options"
            :value="item.value"
            :key="id"
            :selected="modelValue == item.value"
        >
            {{ item.name }}
        </option>
    </select>
</template>

<script>
export default {
    props: {
        modelValue: {
            default: null,
        },
        error: {
            default: false,
        },
        options: {
            default: [],
        },
        includeDefault: {
            default: true,
        },
        defaultLabel: {
            default: "Please Choose One",
        },
    },

    computed: {
        model: {
            get() {
                return this.modelValue;
            },

            set(value) {
                this.$emit("update:modelValue", value);
                this.$emit("change", value);
            },
        },
    },
};
</script>
