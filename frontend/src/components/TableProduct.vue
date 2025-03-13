<script setup lang="ts">
const columns = [
  { title: '#', width: '50px', class: 'col_secondary' },
  { title: 'Name', width: '200px', class: 'col_secondary' },
  { title: 'Nº colors', width: '150px', class: 'col_secondary' },
  { title: 'Nº fabrics', width: '150px', class: 'col_secondary' },
  { title: 'Nº images', width: '150px', class: 'col_secondary' },
  { title: 'Action', width: '150px', class: 'col_secondary' }
]
const response = await fetch('http://localhost:5127/api/product')
const products = await response.json()
</script>

<template>
  <div class="table">
    <div class="table_header">
      <div v-for="col in columns" :style="{ width: col.width }" :class="col.class">
        <p class="text-default">{{ col.title }}</p>
      </div>
    </div>
    <div class="table_body">
      <div v-for="product in products">
        <div class="flex justify-between">
          <div class="col_secondary text-gray flex justify-start" :style="{ width: columns[0].width }"> {{ product.id }}
          </div>
          <div class="font-semibold" :style="{ width: columns[1].width }"> {{ product.name }} </div>
          <div class="col_secondary flex justify-start" :style="{ width: columns[2].width }"> {{ product.totalColors }}
          </div>
          <div class="col_secondary flex justify-start" :style="{ width: columns[3].width }"> {{ product.totalFabrics }}
          </div>
          <div class="col_secondary flex justify-start" :style="{ width: columns[4].width }"> {{ product.totalImages }}
          </div>
          <div> <button class="btn" :style="{ width: columns[5].width }">Edit images</button> </div>
        </div>
      </div>
    </div>
  </div>
</template>