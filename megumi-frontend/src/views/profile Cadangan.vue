<template>
  <div class="profile-view">
    <h1>User Profile</h1>
    <div class="profile-info">
      <img :src="user.avatar" alt="Avatar" class="avatar" />
      <div class="details">
        <h2>{{ user.name }}</h2>
        <p>Email: {{ user.email }}</p>
        <p>Joined: {{ user.joinDate }}</p>
        <button class="settings-btn" @click="showSettings = true">
          Account Settings
        </button>
      </div>
    </div>
    <div class="watchlist">
      <h2>My Watchlist</h2>
      <div class="movies-grid">
        <MovieCard v-for="movie in watchlist" :key="movie.id" :movie="movie" />
      </div>
    </div>
    <Overlay v-if="showSettings" @close="showSettings = false" />
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useUserStore } from "../store/userStore";
import MovieCard from "../components/MovieCard.vue";
import Overlay from "../views/Overlay.vue";

const userStore = useUserStore();
const user = ref({});
const watchlist = ref([]);
const showSettings = ref(false);

onMounted(async () => {
  user.value = await userStore.fetchUserProfile();
  watchlist.value = await userStore.fetchWatchlist();
});
</script>

<style scoped>
.profile-view {
  color: white;
  padding: 20px;
}

.profile-info {
  display: flex;
  align-items: center;
  gap: 20px;
}

.avatar {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  border: 2px solid #e50914;
}

.details h2 {
  margin: 0;
  font-size: 28px;
}

.settings-btn {
  margin-top: 10px;
  padding: 10px;
  background-color: #e50914;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.settings-btn:hover {
  background-color: #f40612;
}

.watchlist {
  margin-top: 20px;
}

.movies-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}
</style>
