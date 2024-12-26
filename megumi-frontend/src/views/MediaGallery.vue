<template>
  <div class="gallery-container">
    <h1>Media Gallery</h1>
    <div class="media-grid">
      <div v-for="media in mediaList" :key="media.id" class="media-item">
        <img v-if="media.type === 'image'" :src="media.url" alt="Media" />
        <video v-if="media.type === 'video'" controls>
          <source :src="media.url" type="video/mp4" />
          Your browser does not support the video tag.
        </video>
        <p>{{ media.title }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { fetchMedia } from "../services/apiService"; // Service API untuk fetch media

const mediaList = ref([]);

onMounted(async () => {
  try {
    mediaList.value = await fetchMedia(); // Fetch media dari backend
  } catch (error) {
    console.error(error);
  }
});
</script>

<style scoped>
.gallery-container {
  padding: 20px;
  background-color: #141414;
  color: white;
}

.media-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 20px;
}

.media-item img,
.media-item video {
  width: 100%;
  border-radius: 8px;
}
</style>
