<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, onMounted, type VNodeRef } from 'vue';
import { type Product } from '../types/Product'
import { type Fabric, type Settings } from '../types/Settings'
import type { Column } from '../types/Column';
import type { Row } from '../types/Row';

const route = useRoute()
const product = ref<Product>();
const loading = ref(true);
const columns = ref<Column[]>([]);
const rows = ref<Row[]>([]);

const deleteImage = async (fabric: string, color: string, imageIdx: number) => {
  await fetch(`/api/product/removeImage/${route.params.id}/${fabric}/${color}/${imageIdx}`, {
    method: 'DELETE'
  })
  await loadProduct()
}

const prepareHeaders = (settings: Settings): Column[] => {
  const columns: Column[] = []
  columns.push({
    idx: 0,
    title: 'Fabric',
    width: '100px',
    class: 'col_secondary'
  })
  settings.fabrics.map(f => {
    f.colors.map((c, index) => {
      const exist = columns.some(col => col.title.includes(c.name))
      if (!exist) {
        columns.push({
          idx: index + 1,
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

const addImage = async ({ url, source }: { url: string, source: string }) => {
  console.log(url)
  await fetch(url, {
    body: JSON.stringify({ source }),
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    }
  })
  await loadProduct()
}

const loadProduct = async () => {
  product.value = await fetch(`/api/product/${route.params.id}`).then((r) => r.json())
  loading.value = false
  if (product.value) {
    const settings: Settings = JSON.parse(product.value.settings)
    columns.value = prepareHeaders(settings)
    rows.value = prepareRows(settings)
  }
}

onMounted(async () => {
  await loadProduct()
})

const uploadFile = (event: any) => {
  const url = event.currentTarget.getAttribute('data-url')
  let imageBase64 = null
  const file = event.target.files[0];
  if (file) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = async () => {
      imageBase64 = reader.result;
      addImage({ url, source: imageBase64 as string })
    }
    reader.onerror = (error) => {
      console.error("Error reading file:", error);
    }
  }
}
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
          <div v-for="(row, idxRow) in rows" :key="idxRow + 'row'">
            <div class="flex justify-between">
              <div v-for="(col, idx) in columns" :style="{ width: col.width }" :key="idx + 'col'">
                <p v-if="idx === 0" class="text-default capitalize">{{ row[idx] }}</p>
                <div v-else>
                  <div class="w-full pb-2">
                    <div v-for="(image, imageIdx) in row[idx]" :key="imageIdx + 'img'">
                      <img :src="image" alt="Image" @click="deleteImage(row[0], columns[idx].title, imageIdx)">
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