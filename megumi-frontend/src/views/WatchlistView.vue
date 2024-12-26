<template>
  <div class="watchlist-view">
    <h1>My Watchlist</h1>
    <div v-if="watchlist.length > 0" class="movies-grid">
      <MovieCard v-for="movie in watchlist" :key="movie.id" :movie="movie" />
    </div>
    <p v-else>Your watchlist is empty.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import MovieCard from "../components/MovieCard.vue";

const watchlist = ref([]);

onMounted(async () => {
  // Fetch watchlist from API
  // watchlist.value = await fetchWatchlist()
});
</script>

<style scoped>
.watchlist-view {
  color: white;
  padding: 20px;
}

.movies-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}
</style>
