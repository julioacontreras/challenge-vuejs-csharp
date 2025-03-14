import { createRouter, createWebHistory } from 'vue-router';
import ShowProducts from '../views/ShowProducts.vue';
import EditProduct from '../views/EditProduct.vue';

const routes = [
  {
    path: '/',
    name: 'ShowProducts',
    component: ShowProducts,
  },
  {
    path: '/edit/:id',
    name: 'EditProduct',
    component: EditProduct,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;