<template>
  <div class="overlay" @click.self="close">
    <div class="overlay-content">
      <button class="close-btn" @click="close">×</button>
      <div class="settings">
        <h2>Account Settings</h2>
        <div class="settings-tabs">
          <button @click="activeTab = 'account'">Account</button>
          <button @click="activeTab = 'notifications'">Notifications</button>
          <button @click="activeTab = 'privacy'">Privacy</button>
        </div>
        <div v-if="activeTab === 'account'">
          <h3>Account Settings</h3>
          <form @submit.prevent="saveAccountSettings">
            <label>Email:</label>
            <input v-model="editEmail" type="email" required />
            <label>Password:</label>
            <input v-model="editPassword" type="password" required />
            <button type="submit">Save</button>
          </form>
        </div>
        <div v-if="activeTab === 'notifications'">
          <h3>Notification Settings</h3>
          <form @submit.prevent="saveNotificationSettings">
            <label>
              <input v-model="notifications.email" type="checkbox" />
              Email Notifications
            </label>
            <label>
              <input v-model="notifications.sms" type="checkbox" />
              SMS Notifications
            </label>
            <button type="submit">Save</button>
          </form>
        </div>
        <div v-if="activeTab === 'privacy'">
          <h3>Privacy Settings</h3>
          <form @submit.prevent="savePrivacySettings">
            <label>
              <input v-model="privacySettings.showEmail" type="checkbox" />
              Show Email
            </label>
            <label>
              <input v-model="privacySettings.showProfile" type="checkbox" />
              Show Profile
            </label>
            <button type="submit">Save</button>
          </form>
        </div>
        <button class="logout-btn" @click="logout">Logout</button>
      </div>
    </div>
  </div>
</template>

// <script setup>
// import { ref } from "vue";
// import { useUserStore } from "../store/userStore";

// const userStore = useUserStore();
// const editEmail = ref("");
// const editPassword = ref("");
// const notifications = ref({
//   email: false,
//   sms: false,
// });
// const privacySettings = ref({
//   showEmail: false,
//   showProfile: false,
// });
// const activeTab = ref("account");

// const saveAccountSettings = async () => {
//   await userStore.updateAccountSettings({
//     email: editEmail.value,
//     password: editPassword.value,
//   });
//   // Provide feedback to user
// };

// const saveNotificationSettings = async () => {
//   await userStore.updateNotificationSettings(notifications.value);
//   // Provide feedback to user
// };

// const savePrivacySettings = async () => {
//   await userStore.updatePrivacySettings(privacySettings.value);
//   // Provide feedback to user
// };

// const logout = async () => {
//   await userStore.logout();
//   // Redirect to login page or perform logout action
// };

// const close = () => {
//   emit("close");
// };
// </script>

<style scoped>
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.overlay-content {
  background-color: #222;
  padding: 40px;
  border-radius: 8px;
  width: 400px;
  max-width: 90vw;
  position: relative;
}

.close-btn {
  position: absolute;
  top: 10px;
  right: 10px;
  background: #e50914;
  color: white;
  border: none;
  border-radius: 50%;
  width: 30px;
  height: 30px;
  cursor: pointer;
  font-size: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.settings-tabs {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}

.settings-tabs button {
  background-color: #e50914;
  color: white;
  border: none;
  padding: 10px;
  cursor: pointer;
}

.settings-tabs button:hover {
  background-color: #f40612;
}

form {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 10px;
}

input {
  display: block;
  width: 100%;
  margin-bottom: 10px;
  padding: 10px;
  border: none;
  border-radius: 4px;
}

button {
  padding: 10px;
  background-color: #e50914;
  color: white;
  border: none;
  cursor: pointer;
  font-size: 16px;
  border-radius: 4px;
}

button:hover {
  background-color: #f40612;
}

.logout-btn {
  background-color: #e50914;
  color: white;
  border: none;
  padding: 10px;
  cursor: pointer;
  font-size: 16px;
  border-radius: 4px;
  width: 100%;
}

.logout-btn:hover {
  background-color: #f40612;
}
</style>
