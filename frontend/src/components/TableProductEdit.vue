<script setup lang="ts">
import { headers } from "../core/headers"

const params = new URLSearchParams(window.location.search);
const id = params.get("id")
const response = await fetch(`api/product/${id}`, {
  headers
})
const product = await response.json()
console.log(product)
</script>

<template>
  <div class="table">
    <div class="table_header">
    </div>
    <div class="table_body">
      {{ product.settings }}
    </div>
  </div>
</template>