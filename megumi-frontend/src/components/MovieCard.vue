<template>
  <div class="movie-card" @click="goToDetails">
    <img :src="movie.posterUrl" :alt="movie.title" class="poster" />
    <div class="movie-info">
      <h3>{{ movie.title }}</h3>
      <p class="rating">{{ movie.rating }}</p>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const props = defineProps({
  movie: Object,
});

const router = useRouter();

const goToDetails = () => {
  router.push(`/movie/${props.movie.id}`);
};
</script>

<style scoped>
.movie-card {
  cursor: pointer;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
  transition: transform 0.3s;
}

.movie-card:hover {
  transform: scale(1.05);
}

.poster {
  width: 100%;
  height: auto;
}

.movie-info {
  padding: 10px;
  background-color: #141414;
  color: white;
}

.movie-info h3 {
  margin: 0;
  font-size: 18px;
}

.rating {
  font-size: 14px;
  color: #e50914;
}
</style>
