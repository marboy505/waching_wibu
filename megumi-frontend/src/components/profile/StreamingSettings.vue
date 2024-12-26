<template>
  <div class="streaming-settings">
    <h2>Streaming Settings</h2>
    <form @submit.prevent="saveStreamingSettings">
      <div class="form-group">
        <label for="video-quality">Video Quality</label>
        <select v-model="videoQuality" id="video-quality">
          <option value="low">Low</option>
          <option value="standard">Standard</option>
          <option value="high">High</option>
        </select>
      </div>
      <div class="form-group">
        <label for="subtitle-language">Subtitle Language</label>
        <select v-model="subtitleLanguage" id="subtitle-language">
          <option value="en">English</option>
          <option value="es">Spanish</option>
          <option value="fr">French</option>
        </select>
      </div>
      <button type="submit" class="btn-primary">Save Changes</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";

const videoQuality = ref("standard");
const subtitleLanguage = ref("en");

const saveStreamingSettings = () => {
  // Implement API call to save streaming settings
  alert("Streaming settings updated");
};
</script>

<style scoped>
.streaming-settings {
  background-color: #222;
  color: #fff;
  padding: 20px;
  border-radius: 5px;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
}

.form-group select {
  width: 100%;
  padding: 10px;
  border: 1px solid #333;
  border-radius: 5px;
}

.btn-primary {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
}
</style>
