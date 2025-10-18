<template>
    <div v-if="errors" class="mt-1 text-red-500">
        {{ errors[0] }}
    </div>
</template>
<script>
export default {
    props: ["errors"],
};
</script>
