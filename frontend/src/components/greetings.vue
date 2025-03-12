<script setup lang="ts">
defineProps<{ message: string }>();
</script>

<template>
  <p class="text-blue text-xl">{{ message }}</p>
</template>