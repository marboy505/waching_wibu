<template>
  <div class="search-results">
    <input
      v-model="searchQuery"
      @input="search"
      placeholder="Search for movies and TV shows"
      class="search-input"
    />
    <div v-if="results.length > 0" class="movies-grid">
      <MovieCard v-for="movie in results" :key="movie.id" :movie="movie" />
    </div>
    <p v-else>No results found</p>
  </div>
</template>

<script setup>
import { ref } from "vue";
import MovieCard from "../components/MovieCard.vue";

const searchQuery = ref("");
const results = ref([]);

const search = async () => {
  // Fetch search results from API
  // results.value = await fetchSearchResults(searchQuery.value)
};
</script>

<style scoped>
.search-results {
  color: white;
  padding: 20px;
}

.search-input {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  margin-bottom: 20px;
  border: 1px solid #e50914;
  border-radius: 4px;
  background-color: #141414;
  color: white;
}

.movies-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}
</style>
