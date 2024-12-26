<template>
  <div class="edit-profile">
    <h2>Edit Profile</h2>
    <form @submit.prevent="saveProfile">
      <div class="form-group">
        <label for="avatar">Avatar</label>
        <input
          type="file"
          id="avatar"
          accept="image/*"
          @change="onAvatarChange"
        />
        <img
          :src="avatarPreview"
          alt="Avatar Preview"
          class="avatar-preview"
          v-if="avatarPreview"
        />
      </div>
      <div class="form-group">
        <label for="name">Name</label>
        <input
          v-model="editName"
          type="text"
          id="name"
          placeholder="Enter your name"
          required
        />
        <small v-if="nameError" class="error">{{ nameError }}</small>
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <input
          v-model="editEmail"
          type="email"
          id="email"
          placeholder="Enter your email"
          required
        />
        <small v-if="emailError" class="error">{{ emailError }}</small>
      </div>
      <div class="form-actions">
        <button type="submit" class="btn-primary" :disabled="isSaving">
          Save Changes
        </button>
        <button type="button" @click="closeOverlay" class="btn-secondary">
          Cancel
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";

const avatarPreview = ref("");
const editName = ref("");
const editEmail = ref("");
const nameError = ref("");
const emailError = ref("");
const isSaving = ref(false);

const onAvatarChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    const reader = new FileReader();
    reader.onload = (e) => {
      avatarPreview.value = e.target.result;
    };
    reader.readAsDataURL(file);
  }
};

const saveProfile = () => {
  isSaving.value = true;
  // Save profile logic
  // showNotificationMessage("Profile updated successfully!");
  closeOverlay();
  isSaving.value = false;
};

const closeOverlay = () => {
  // Emit an event to notify the parent to close the overlay
  // emit('close');
};
</script>

<style scoped>
.edit-profile {
  padding: 20px;
}
.form-group {
  margin-bottom: 15px;
}
.label {
  display: block;
  margin-bottom: 5px;
}
input[type="text"],
input[type="email"] {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}
.avatar-preview {
  width: 100px;
  height: 100px;
  object-fit: cover;
  margin-top: 10px;
}
.error {
  color: red;
  font-size: 0.875em;
}
.form-actions {
  margin-top: 20px;
}
.btn-primary {
  background-color: #007bff;
  color: #fff;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
}
.btn-secondary {
  background-color: #6c757d;
  color: #fff;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  margin-left: 10px;
}
</style>
