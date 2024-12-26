<template>
  <div class="privacy-settings">
    <h2>Privacy Settings</h2>
    <form @submit.prevent="savePrivacySettings">
      <div class="form-group">
        <label for="profile-visibility">Profile Visibility</label>
        <select v-model="profileVisibility" id="profile-visibility">
          <option value="public">Public</option>
          <option value="private">Private</option>
        </select>
      </div>
      <div class="form-group">
        <label for="activity-visibility">Activity Visibility</label>
        <select v-model="activityVisibility" id="activity-visibility">
          <option value="everyone">Everyone</option>
          <option value="friends">Friends</option>
          <option value="nobody">Nobody</option>
        </select>
      </div>
      <button type="submit" class="btn-primary">Save Changes</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";

const profileVisibility = ref("public");
const activityVisibility = ref("everyone");

const savePrivacySettings = () => {
  // Implement API call to save privacy settings
  alert("Privacy settings updated");
};
</script>

<style scoped>
.privacy-settings {
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
