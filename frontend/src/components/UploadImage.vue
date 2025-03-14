<script setup lang="ts">

const props = defineProps({
  fabric: String,
  color: String
});

const uploadFile = (event: any) => {
  let imageBase64 = null
  const file = event.target.files[0];
  if (file) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      imageBase64 = reader.result;
      console.log(props, imageBase64)
    }
    reader.onerror = (error) => {
      console.error("Error reading file:", error);
    }
  }
}
</script>

<template>
  <section>
    <input type="file" id="file" class="file-input" @change="uploadFile" />
    <label for="file" id="file-label" class="file-label">Add</label>
  </section>
</template>