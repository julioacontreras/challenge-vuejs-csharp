<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { type Product } from '../types/Product'
import { type Fabric, type Settings } from '../types/Settings'
import type { Column } from '../types/Column';
import type { Row } from '../types/Row';
import UploadImage from './UploadImage.vue'

const route = useRoute()
const product = ref<Product>();
const loading = ref(true);
const columns = ref<Column[]>([]);
const rows = ref<Row[]>([]);

const deleteImage = (fabric: string, color: string, imageIdx: number) => {
  console.log(fabric, color, imageIdx)
}

const prepareHeaders = (settings: Settings): Column[] => {
  const columns: Column[] = []
  columns.push({
    title: 'Fabric',
    width: '100px',
    class: 'col_secondary'
  })
  settings.fabrics.map(f => {
    f.colors.map(c => {
      const exist = columns.some(col => col.title.includes(c.name))
      if (!exist) {
        columns.push({
          title: c.name,
          width: '100px',
          class: 'col_secondary capitalize'
        })
      }
    })
  })
  return columns
}

const prepareRow = (fabric: Fabric) => {
  const row: Row = {
    0: fabric.name
  }
  fabric.colors.map((color, idx) => {
    row[`${idx + 1}`] = color.images
  })
  return row
}

const prepareRows = (settings: Settings): object[] => {
  const rows: object[] = []
  settings.fabrics.map(f => {
    rows.push(prepareRow(f))
  })
  return rows
}

onMounted(async () => {
  product.value = await fetch(`/api/product/${route.params.id}`).then((r) => r.json())
  loading.value = false
  if (product.value) {
    const settings: Settings = JSON.parse(product.value.settings)
    columns.value = prepareHeaders(settings)
    rows.value = prepareRows(settings)
  }
})
</script>

<template>
  <section class="w-full">
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
          <div v-for="row in rows">
            <div class="flex justify-between">
              <div v-for="(col, idx) in columns" :style="{ width: col.width }">
                <p v-if="idx === 0" class="text-default capitalize">{{ row[idx] }}</p>
                <div v-else>
                  <div class="w-full pb-2">
                    <div v-for="(image, imageIdx) in row[idx]">
                      <img :src="image" alt="Image" :click="deleteImage(row[0], columns[idx].title, imageIdx)">
                    </div>
                  </div>
                  <UploadImage :fabric="row[0]" :color="columns[idx].title" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>