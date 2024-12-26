<template>
  <div class="home-view">
    <div class="banner">
      <!-- <img src="/path/to/banner.jpg" alt="Banner" class="banner-image" /> -->
      <div class="banner-content">
        <h1>Discover Your Next Favorite Movie</h1>
        <p>Stream movies and TV shows anytime, anywhere.</p>
      </div>
    </div>
    <div class="recommendations">
      <h2>Top Picks for You</h2>
      <div class="movies-grid">
        <MovieCard v-for="movie in topPicks" :key="movie.id" :movie="movie" />
      </div>
    </div>
    <div class="test">
      <v-container>
        <v-row>
          <v-col>
            <v-parallax
              src="https://cdn.vuetifyjs.com/images/parallax/material.jpg"
            ></v-parallax>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </div>
</template>

<script setup>
import MovieCard from "../components/MovieCard.vue";
import { ref, onMounted } from "vue";

const topPicks = ref([]);

onMounted(async () => {
  // Fetch top picks from API
  // topPicks.value = await fetchTopPicks()
});
</script>

<style scoped>
.home-view {
  color: white;
}

.banner {
  position: relative;
  overflow: hidden;
}

.banner-image {
  width: 100%;
  height: 400px;
  object-fit: cover;
}

.banner-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: white;
  text-align: center;
}

.banner-content h1 {
  font-size: 48px;
  margin: 0;
}

.banner-content p {
  font-size: 24px;
  margin-top: 10px;
}

.recommendations {
  padding: 20px;
}

.movies-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}
</style>
