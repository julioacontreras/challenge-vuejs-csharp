<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { type Product } from '../types/Product'
import type { Column } from '../types/Column';
import type { Row } from '../types/Row';
import { useEditProduct } from '../compositions/useEditProduct'

const route = useRoute()
const product = ref<Product>();
const loading = ref(true);
const columns = ref<Column[]>([]);
const rows = ref<Row[]>([]);
const {
  uploadFile,
  loadProduct,
  deleteImage
} = useEditProduct(
  route,
  product,
  loading,
  columns,
  rows
)

onMounted(async () => {
  await loadProduct()
})
</script>

<template>
  <section class="w-full">
    <a href="/" class="py-6 text-xl text-gray-400 font-semibold pl-3 hover:text-blue-600"> Back </a>
    <div v-if="loading">
      Loading...
    </div>
    <div v-else-if="product">
      <div class="py-6 text-xl font-semibold pl-3"> #{{ product.id }} - {{ product.name }}</div>
      <div class="table">
        <div class="table_header">
          <div v-for="col in columns" :style="{ width: col.width }" :class="col.class">
            <p class="text-default">{{ col.title }}</p>
          </div>
        </div>
        <div class="table_body">
          <div v-for="(row, idxRow) in rows" :key="idxRow + 'row'">
            <div class="flex justify-between">
              <div v-for="(col, idx) in columns" :style="{ width: col.width }" :key="idx + 'col'">
                <p v-if="idx === 0" class="text-default capitalize">{{ row[idx] }}</p>
                <div v-else>
                  <div class="w-full pb-2">
                    <div v-for="(image, imageIdx) in row[idx]" :key="imageIdx + 'img'">
                      <img class="img" :src="image" alt="Image"
                        @click="deleteImage(row[0], columns[idx].title, imageIdx)">
                    </div>
                  </div>
                  <input
                    :data-url="`/api/product/addImage/${route.params.id}/${rows[idxRow][0]}/${columns[idx]?.title}`"
                    type="file" :id="`file-${idx}-${idxRow}`" class="file-input" @change="(ev) => uploadFile(ev)" />
                  <label :for="`file-${idx}-${idxRow}`" :id="`file-label-${idx}-${idxRow}`"
                    class="file-label">Add</label>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>