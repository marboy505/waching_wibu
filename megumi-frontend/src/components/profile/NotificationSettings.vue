<template>
  <div class="notification-settings">
    <h2>Notification Settings</h2>
    <form @submit.prevent="saveNotifications">
      <div class="form-group">
        <label>Email Notifications</label>
        <input type="checkbox" v-model="emailNotifications" />
      </div>
      <div class="form-group">
        <label>Push Notifications</label>
        <input type="checkbox" v-model="pushNotifications" />
      </div>
      <div class="form-group">
        <label>Reminder Settings</label>
        <input v-model="reminderTime" type="time" />
      </div>
      <button type="submit" class="btn-primary">Save Changes</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from "vue";

const emailNotifications = ref(true);
const pushNotifications = ref(true);
const reminderTime = ref("09:00");

const saveNotifications = () => {
  // Implement API call to save notification settings
  alert("Notification settings updated");
};
</script>

<style scoped>
.notification-settings {
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

.form-group input[type="checkbox"] {
  margin-right: 10px;
}

.form-group input[type="time"] {
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
