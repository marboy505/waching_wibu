<template>
  <div class="movie-details">
    <div class="movie-header">
      <img :src="movie.posterUrl" alt="Poster" class="poster" />
      <div class="movie-info">
        <h1 class="title">{{ movie.title }}</h1>
        <p class="description">{{ movie.description }}</p>
        <button @click="toggleWatchlist" class="watchlist-button">
          {{ inWatchlist ? "Remove from Watchlist" : "Add to Watchlist" }}
        </button>
      </div>
    </div>
    <div class="video-player">
      <iframe :src="movie.trailerUrl" frameborder="0" allowfullscreen></iframe>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import { useUserStore } from "../store/userStore";

const route = useRoute();
const userStore = useUserStore();
const movie = ref({});
const inWatchlist = ref(false);

onMounted(async () => {
  // Fetch movie details from API
  // movie.value = await fetchMovieDetails(route.params.id)
  // inWatchlist.value = await checkIfInWatchlist(route.params.id)
});

const toggleWatchlist = () => {
  // Add or remove movie from watchlist
  // updateWatchlist(movie.value.id)
  inWatchlist.value = !inWatchlist.value;
};
</script>

<style scoped>
.movie-details {
  color: white;
  padding: 20px;
}

.movie-header {
  display: flex;
  gap: 20px;
  align-items: flex-start;
}

.poster {
  width: 200px;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
}

.movie-info {
  max-width: 600px;
}

.title {
  font-size: 36px;
  margin: 0;
}

.description {
  font-size: 18px;
  margin-top: 10px;
}

.watchlist-button {
  background-color: #e50914;
  border: none;
  color: white;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  margin-top: 10px;
  transition: background-color 0.3s;
}

.watchlist-button:hover {
  background-color: #f40612;
}

.video-player iframe {
  width: 100%;
  height: 500px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
}
</style>
