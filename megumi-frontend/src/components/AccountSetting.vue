<template>
  <div class="account-settings">
    <h2>Account Settings</h2>
    <div class="settings-tabs">
      <button
        :class="{ active: currentTab === 'profile' }"
        @click="currentTab = 'profile'"
      >
        Profile
      </button>
      <button
        :class="{ active: currentTab === 'notifications' }"
        @click="currentTab = 'notifications'"
      >
        Notifications
      </button>
      <button
        :class="{ active: currentTab === 'privacy' }"
        @click="currentTab = 'privacy'"
      >
        Privacy
      </button>
    </div>
    <div class="settings-content">
      <!-- Profile Settings -->
      <div v-if="currentTab === 'profile'" class="profile-settings">
        <h3>Profile Settings</h3>
        <form @submit.prevent="saveProfileSettings">
          <div class="form-group">
            <label for="profileName">Name</label>
            <input
              v-model="profileName"
              type="text"
              id="profileName"
              placeholder="Enter your name"
              required
            />
          </div>
          <div class="form-group">
            <label for="profileEmail">Email</label>
            <input
              v-model="profileEmail"
              type="email"
              id="profileEmail"
              placeholder="Enter your email"
              required
            />
          </div>
          <div class="form-group">
            <label for="newPassword">New Password</label>
            <input
              v-model="newPassword"
              type="password"
              id="newPassword"
              placeholder="Enter new password"
            />
            <small v-if="passwordError" class="error">{{
              passwordError
            }}</small>
          </div>
          <div class="form-group">
            <label for="confirmPassword">Confirm Password</label>
            <input
              v-model="confirmPassword"
              type="password"
              id="confirmPassword"
              placeholder="Confirm new password"
            />
            <small v-if="confirmPasswordError" class="error">{{
              confirmPasswordError
            }}</small>
          </div>
          <div class="form-actions">
            <button type="submit" class="btn-primary" :disabled="isSaving">
              Save Profile Settings
            </button>
            <button type="button" @click="closeOverlay" class="btn-secondary">
              Cancel
            </button>
          </div>
        </form>
        <h3>Social Media</h3>
        <form @submit.prevent="saveSocialLinks">
          <div class="form-group">
            <label for="instagram">Instagram</label>
            <input
              v-model="socialLinks.instagram"
              type="url"
              id="instagram"
              placeholder="Enter Instagram URL"
            />
          </div>
          <div class="form-group">
            <label for="facebook">Facebook</label>
            <input
              v-model="socialLinks.facebook"
              type="url"
              id="facebook"
              placeholder="Enter Facebook URL"
            />
          </div>
          <div class="form-group">
            <label for="twitter">Twitter</label>
            <input
              v-model="socialLinks.twitter"
              type="url"
              id="twitter"
              placeholder="Enter Twitter URL"
            />
          </div>
          <div class="form-actions">
            <button type="submit" class="btn-primary" :disabled="isSaving">
              Save Social Links
            </button>
            <button type="button" @click="closeOverlay" class="btn-secondary">
              Cancel
            </button>
          </div>
        </form>
      </div>

      <!-- Notifications Settings -->
      <div v-if="currentTab === 'notifications'" class="notifications-settings">
        <h3>Notification Settings</h3>
        <form @submit.prevent="saveNotificationSettings">
          <div class="form-group">
            <label for="emailNotifications">Email Notifications</label>
            <input
              type="checkbox"
              v-model="emailNotifications"
              id="emailNotifications"
            />
            <label for="emailNotifications"
              >Receive notifications via email</label
            >
          </div>
          <div class="form-group">
            <label for="smsNotifications">SMS Notifications</label>
            <input
              type="checkbox"
              v-model="smsNotifications"
              id="smsNotifications"
            />
            <label for="smsNotifications">Receive notifications via SMS</label>
          </div>
          <div class="form-actions">
            <button type="submit" class="btn-primary" :disabled="isSaving">
              Save Notification Settings
            </button>
            <button type="button" @click="closeOverlay" class="btn-secondary">
              Cancel
            </button>
          </div>
        </form>
      </div>

      <!-- Privacy Settings -->
      <div v-if="currentTab === 'privacy'" class="privacy-settings">
        <h3>Privacy Settings</h3>
        <form @submit.prevent="savePrivacySettings">
          <div class="form-group">
            <label for="profileVisibility">Profile Visibility</label>
            <select v-model="profileVisibility" id="profileVisibility">
              <option value="public">Public</option>
              <option value="private">Private</option>
            </select>
          </div>
          <div class="form-actions">
            <button type="submit" class="btn-primary" :disabled="isSaving">
              Save Privacy Settings
            </button>
            <button type="button" @click="closeOverlay" class="btn-secondary">
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";

const currentTab = ref("profile");
const isSaving = ref(false);
const profileName = ref("");
const profileEmail = ref("");
const newPassword = ref("");
const confirmPassword = ref("");
const passwordError = ref("");
const confirmPasswordError = ref("");
const socialLinks = ref({ instagram: "", facebook: "", twitter: "" });
const emailNotifications = ref(false);
const smsNotifications = ref(false);
const profileVisibility = ref("public");

const saveProfileSettings = () => {
  isSaving.value = true;
  // Save profile settings logic
  // showNotificationMessage('Profile settings updated successfully!');
  isSaving.value = false;
};

const saveSocialLinks = () => {
  isSaving.value = true;
  // Save social links logic
  // showNotificationMessage('Social links updated successfully!');
  isSaving.value = false;
};

const saveNotificationSettings = () => {
  isSaving.value = true;
  // Save notification settings logic
  // showNotificationMessage('Notification settings updated successfully!');
  isSaving.value = false;
};

const savePrivacySettings = () => {
  isSaving.value = true;
  // Save privacy settings logic
  // showNotificationMessage('Privacy settings updated successfully!');
  isSaving.value = false;
};

const closeOverlay = () => {
  // Emit an event to notify the parent to close the overlay
  // emit('close');
};
</script>

<style scoped>
.account-settings {
  padding: 20px;
}
.settings-tabs {
  display: flex;
  margin-bottom: 20px;
}
.settings-tabs button {
  margin-right: 10px;
  padding: 10px 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background: #f8f9fa;
  cursor: pointer;
}
.settings-tabs button.active {
  background: #007bff;
  color: #fff;
}
.settings-content {
  border: 1px solid #ddd;
  padding: 20px;
  border-radius: 5px;
}
.form-group {
  margin-bottom: 15px;
}
label {
  display: block;
  margin-bottom: 5px;
}
input[type="text"],
input[type="email"],
input[type="password"],
select {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
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
