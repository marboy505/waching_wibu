import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import SearchResultsView from "../views/SearchResultsView.vue";
import WatchlistView from "../views/WatchlistView.vue";
import ProfileView from "../views/ProfileView.vue";
import LoginView from "../components/LoginModal.vue";
// import RegisterView from "../components/RegisterModal.vue";
import UploadForm from "../views/UploadForm.vue";
import MediaGallery from "../views/MediaGallery.vue";

const routes = [
  { path: "/", component: HomeView },
  { path: "/search", component: SearchResultsView },
  {
    path: "/watchlist",
    component: WatchlistView,
    meta: { requiresAuth: true },
  },
  { path: "/profile", component: ProfileView, meta: { requiresAuth: true } },
  { path: "/login", component: LoginView },
  // { path: "/signup", component: RegisterView },
  { path: "/upload", component: UploadForm, meta: { requiresAuth: true } },
  { path: "/media-gallery", component: MediaGallery },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Variable untuk menyimpan elemen spinner
let loadingElement = null;

router.beforeEach((to, from, next) => {
  // Tambahkan spinner sebelum navigasi
  if (!loadingElement) {
    loadingElement = document.createElement("div");
    loadingElement.id = "loading-spinner";
    document.body.appendChild(loadingElement);
  }
  next();
});

router.afterEach(() => {
  // Hapus spinner setelah navigasi
  if (loadingElement) {
    document.body.removeChild(loadingElement);
    loadingElement = null;
  }
});

export default router;
