<template>
  <header class="header">
    <div class="logo">
      メグミ
      <!-- Tampilkan sapaan jika pengguna sudah login -->
      <span v-if="isAuthenticated && user?.name" class="greeting">
        Hello, {{ user.name }}
      </span>
    </div>
    <nav>
      <router-link to="/">Home</router-link>
      <router-link to="/search">Search</router-link>
      <router-link to="/watchlist">Watchlist</router-link>
      <router-link to="/profile">Profile</router-link>
      <router-link to="/upload">Upload</router-link>
    </nav>
    <div class="auth-buttons">
      <!-- Tombol Login/Register hanya muncul jika belum login -->
      <button v-if="!isAuthenticated" @click="handleLogin">Login/Sign Up</button>
      <!-- <button v-if="!isAuthenticated" @click="handleSignup">Sign Up</button> -->
      <!-- Tombol Logout hanya muncul jika sudah login -->
      <button v-if="isAuthenticated" class="logout-button" @click="handleLogout">
        Log Out
      </button>
    </div>
  </header>
</template>

<script setup>
import { useRouter } from "vue-router";
import { useUserStore } from "../store/userStore";
import { computed, onMounted } from "vue";

const router = useRouter();
const userStore = useUserStore();

// Ambil status login dan informasi pengguna dari store
const isAuthenticated = computed(() => userStore.isAuthenticated);
const user = computed(() => userStore.user);

const handleLogin = () => {
  router.push("/login");
};

const handleLogout = async () => {
  await userStore.logout();
  router.push("/login");
};
// const handleSignup = () => {
//   router.push("/signup");
// };

// Muat data pengguna dari localStorage saat komponen dimuat
onMounted(() => {
  if (!isAuthenticated.value) {
    console.log("Loading user data from localStorage...");
    userStore.loadFromLocalStorage();
  }
});
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px;
  background-color: #141414;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.8);
}

.logo {
  font-size: 24px;
  color: #e50914;
  font-weight: bold;
  display: flex;
  align-items: center;
}

.greeting {
  margin-left: 10px;
  font-size: 18px;
  color: #fff;
}

nav a {
  margin: 0 15px;
  color: white;
  text-decoration: none;
  font-size: 16px;
}

nav a:hover {
  text-decoration: underline;
}

.auth-buttons button {
  background-color: #e50914;
  border: none;
  color: white;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  margin-left: 10px;
  transition: background-color 0.3s;
}

.auth-buttons button:hover {
  background-color: #f40612;
}
</style>
