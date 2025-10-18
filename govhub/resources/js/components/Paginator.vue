<template>
    <div
        v-if="meta.total > meta.per_page"
        class="items-center justify-between sm:flex sm:flex-row-reverse"
    >
        <div class="flex justify-end text-teal-700">
            <ul v-if="meta.last_page > 1" class="p-2 space-x-2">
                <li
                    v-if="meta.current_page != 1"
                    class="inline-flex transition-all duration-300 transform stroke-current hover:scale-105"
                >
                    <button
                        @click="onClickFirstPage"
                        :class="{
                            'text-gray-300 font-bold cursor-not-allowed':
                                meta.current_page == 1,
                        }"
                        class="px-2.5 py-0.5 mx-1 border-teal-500 font-bold no-underline transform scale-125 border rounded-lg focus:outline-none hover:bg-teal-500 hover:text-white"
                        type="button"
                        rel="prev"
                    >
                        &laquo;
                    </button>
                </li>

                <li
                    v-if="meta.current_page != 1"
                    class="inline-flex transition-all duration-300 transform stroke-current hover:scale-105"
                >
                    <button
                        type="button"
                        @click="onClickPreviousPage"
                        class="px-3 py-0.5 mx-1 font-bold border-teal-500 no-underline transform scale-125 border rounded-lg focus:outline-none hover:bg-teal-500 hover:text-white"
                    >
                        &lsaquo;
                    </button>
                </li>

                <li
                    v-for="page in pages"
                    class="inline-flex transition-all duration-300 transform stroke-current hover:scale-105"
                    :key="page.name"
                >
                    <button
                        type="button"
                        :class="{
                            'text-white font-bold bg-teal-500  cursor-not-allowed hover:bg-teal-500 hover:text-white':
                                page.name == meta.current_page,
                            'hover:bg-teal-500 hover:text-white':
                                page.name != meta.current_page,
                        }"
                        class="px-4 py-1.5 no-underline border border-teal-500 rounded-lg focus:outline-none"
                        @click="onClickPage(page.name)"
                        role="button"
                    >
                        {{ page.name }}
                    </button>
                </li>

                <li
                    v-if="meta.current_page < meta.last_page"
                    class="inline-flex transition-all duration-300 transform stroke-current hover:scale-105"
                >
                    <button
                        type="button"
                        @click="onClickNextPage"
                        :disabled="meta.current_page == meta.last_page"
                        class="px-3 py-0.5 mx-1 font-bold no-underline transform scale-125 border border-teal-500 rounded-lg focus:outline-none hover:bg-teal-500 hover:text-white"
                    >
                        &rsaquo;
                    </button>
                </li>

                <li
                    v-if="meta.current_page < meta.last_page"
                    class="inline-flex transition-all duration-300 transform stroke-current hover:scale-105"
                >
                    <button
                        :disabled="meta.current_page == meta.last_page"
                        :class="{
                            'text-gray-300 font-bold cursor-not-allowed':
                                meta.current_page == meta.last_page,
                        }"
                        class="px-2.5 py-0.5 mx-1 font-bold border-teal-500 no-underline transform scale-125 border rounded-lg focus:outline-none hover:bg-teal-500 hover:text-white"
                        type="button"
                        @click="onClickLastPage"
                        rel="next"
                    >
                        &raquo;
                    </button>
                </li>
            </ul>
        </div>
        <div class="space-x-1.5 text-right px-4">
            <span>Showing items</span>
            <span>{{ firstItemNumber }}</span>
            <span>-</span>
            <span>{{ lastItemNumber }}</span>
            <span>of</span>
            <span>{{ meta.total }}</span>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        maxVisibleButtons: {
            type: Number,
            required: false,
            default: 3,
        },

        meta: {
            type: Object,
            required: true,
        },
    },

    computed: {
        startPage() {
            if (this.meta.current_page === 1) {
                return 1;
            }

            if (this.meta.current_page === this.meta.last_page) {
                if (this.meta.last_page - this.maxVisibleButtons + 1 < 1) {
                    return 1;
                }
                return this.meta.last_page - this.maxVisibleButtons + 1;
            }

            return this.meta.current_page - 1;
        },

        endPage() {
            return Math.min(
                this.startPage + this.maxVisibleButtons - 1,
                this.meta.last_page
            );
        },

        pages() {
            const range = [];

            for (let i = this.startPage; i <= this.endPage; i += 1) {
                range.push({
                    name: i,
                    isDisabled: i === this.meta.current_page,
                });
            }

            return range;
        },

        firstItemNumber() {
            return this.meta.per_page * (this.meta.current_page - 1) + 1;
        },

        lastItemNumber() {
            let lastItem = this.meta.per_page * this.meta.current_page;

            if (this.meta.total > lastItem) {
                return lastItem;
            }

            return this.meta.total;
        },
    },

    methods: {
        onClickFirstPage() {
            this.$emit("goToPage", 1);
        },
        onClickPreviousPage() {
            this.$emit("goToPage", this.meta.current_page - 1);
        },
        onClickPage(page) {
            this.$emit("goToPage", page);
        },
        onClickNextPage() {
            this.$emit("goToPage", this.meta.current_page + 1);
        },
        onClickLastPage() {
            this.$emit("goToPage", this.meta.last_page);
        },
    },
};
</script>
