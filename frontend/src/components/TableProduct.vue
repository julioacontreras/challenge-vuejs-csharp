<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { type ProductStats } from '../types/ProductStats'
const products = ref<ProductStats[]>([]);
const loading = ref(true);

onMounted(async () => {
  products.value = await fetch(`/api/product`).then((r) => r.json())
  loading.value = false
})

const columns = [
  { title: '#', width: '50px', class: 'col_secondary' },
  { title: 'Name', width: '200px', class: 'col_secondary' },
  { title: 'Nº colors', width: '150px', class: 'col_secondary' },
  { title: 'Nº fabrics', width: '150px', class: 'col_secondary' },
  { title: 'Nº images', width: '150px', class: 'col_secondary' },
  { title: 'Action', width: '150px', class: 'col_secondary flex justify-end pr-6' }
]

</script>

<template>
  <section class="w-full">
    <div v-if="loading">
      Loading...
    </div>
    <div v-else>
      <div class="table">
        <div class="table_header">
          <div v-for="col in columns" :style="{ width: col.width }" :class="col.class">
            <p class="text-default">{{ col.title }}</p>
          </div>
        </div>
        <div class="table_body">
          <div v-for="product in products">
            <div class="flex justify-between">
              <div class="col_secondary text-gray flex justify-start" :style="{ width: columns[0].width }"> {{
                product.id
                }}
              </div>
              <div class="font-semibold" :style="{ width: columns[1].width }"> {{ product.name }} </div>
              <div class="col_secondary flex justify-start" :style="{ width: columns[2].width }"> {{ product.totalColors
              }}
              </div>
              <div class="col_secondary flex justify-start" :style="{ width: columns[3].width }"> {{
                product.totalFabrics
                }}
              </div>
              <div class="col_secondary flex justify-start" :style="{ width: columns[4].width }"> {{ product.totalImages
              }}
              </div>
              <div> <a :href="`/edit/${product.id}`" class="btn" :style="{ width: columns[5].width }">Edit
                  images</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

  </section>
</template>