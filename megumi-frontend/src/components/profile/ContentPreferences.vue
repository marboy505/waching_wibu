<template>
  <div class="content-preferences">
    <h2>Content Preferences</h2>
    <form @submit.prevent="savePreferences">
      <div class="form-group">
        <label for="favorite-genres">Favorite Genres</label>
        <select v-model="favoriteGenres" id="favorite-genres" multiple>
          <option v-for="genre in genres" :key="genre" :value="genre">
            {{ genre }}
          </option>
        </select>
      </div>
      <div class="form-group">
        <label for="watch-history">Watch History</label>
        <ul>
          <li v-for="item in watchHistory" :key="item">
            {{ item }} <button @click="removeFromHistory(item)">Remove</button>
          </li>
        </ul>
      </div>
      <button type="submit" class="btn-primary">Save Changes</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";

const genres = ["Action", "Comedy", "Drama", "Horror", "Sci-Fi"];
const favoriteGenres = ref([]);
const watchHistory = ref(["Inception", "The Matrix", "Interstellar"]);

const savePreferences = () => {
  // Implement API call to save content preferences
  alert("Content preferences updated");
};

const removeFromHistory = (item) => {
  watchHistory.value = watchHistory.value.filter(
    (historyItem) => historyItem !== item
  );
};
</script>

<style scoped>
.content-preferences {
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

ul {
  list-style: none;
  padding: 0;
}

ul li {
  display: flex;
  justify-content: space-between;
  padding: 5px 0;
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
