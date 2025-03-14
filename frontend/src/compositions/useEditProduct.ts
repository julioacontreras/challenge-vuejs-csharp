import type { Ref } from 'vue'
import type { Column } from '../types/Column'
import type { Row } from '../types/Row'
import { type Fabric, type Settings } from '../types/Settings'
import type { RouteLocationNormalizedLoaded } from 'vue-router'

export const useEditProduct = (
  route: RouteLocationNormalizedLoaded,
  product: Ref,
  loading: Ref,
  columns: Ref,
  rows: Ref
) => {

  const addImage = async ({ url, source }: { url: string, source: string }) => {
    await fetch(url, {
      body: JSON.stringify({ source }),
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      }
    })
    await loadProduct()
  }

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
        loadProduct()
      }
      reader.onerror = (error) => {
        console.error("Error reading file:", error);
      }
    }
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

  const loadProduct = async () => {
    product.value = await fetch(`/api/product/${route.params.id}`).then((r) => r.json())
    loading.value = false
    if (product.value) {
      const settings: Settings = JSON.parse(product.value.settings)
      columns.value = prepareHeaders(settings)
      rows.value = prepareRows(settings)
    }
  }

  const deleteImage = async (fabric: string, color: string, imageIdx: number) => {
    await fetch(`/api/product/removeImage/${route.params.id}/${fabric}/${color}/${imageIdx}`, {
      method: 'DELETE'
    })
    await loadProduct()
  }

  return {
    addImage,
    uploadFile,
    prepareHeaders,
    prepareRow,
    prepareRows,
    loadProduct,
    deleteImage
  }
}

