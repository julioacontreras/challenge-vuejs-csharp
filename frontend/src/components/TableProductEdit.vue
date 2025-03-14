<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';
import { type Product } from '../types/Product'
import { type Fabric, type Settings } from '../types/Settings'
import type { Column } from '../types/Column';
import type { Row } from '../types/Row';
const route = useRoute()
const product = ref<Product>();
const loading = ref(true);
const columns = ref<Column[]>([]);
const rows = ref<Row[]>([]);

const prepareHeaders = (settings: Settings): Column[] => {
  const columns: Column[] = []
  columns.push({
    title: 'Fabric',
    width: '200px'
  })
  settings.fabrics.map(f => {
    f.colors.map(c => {
      columns.push({
        title: c.name,
        width: '100px'
      })
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
  <section>
    <div v-if="loading">
      Loading...
    </div>
    <div v-else-if="product" class="table">
      <div class="table_header">
        <div v-for="col in columns" :style="{ width: col.width }" :class="col.class">
          <p class="text-default">{{ col.title }}</p>
        </div>
      </div>
      <div class="table_body">

        <div v-for="row in rows">
          <div class="flex justify-between">
            <div v-for="(col, idx) in columns" :style="{ width: col.width }">

              <p v-if="idx === 0" class="text-default">{{ row[idx] }}</p>

              <div v-else>
                <div v-for="image in row[idx]" :style="{ width: col.width }">
                  <img :src="`data:image/png;base64, ${image}`" alt="Image">
                </div>
                <button>Add</button>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </section>
</template>