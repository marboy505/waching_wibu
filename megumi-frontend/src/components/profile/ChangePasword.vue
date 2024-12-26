<template>
  <div class="account-settings">
    <h2>Akun</h2>

    <div class="form-group">
      <label for="current-email">Email Terdaftar</label>
      <input v-model="currentEmail" type="email" id="current-email" disabled />
    </div>

    <div class="social-connections">
      <h3>Hubungkan Akun</h3>
      <button class="social-btn facebook-btn" @click="connectToFacebook">
        Hubungkan ke Facebook
      </button>
      <button class="social-btn instagram-btn" @click="connectToInstagram">
        Hubungkan ke Instagram
      </button>
      <button class="social-btn email-btn" @click="connectToEmail">
        Hubungkan ke Email
      </button>
    </div>

    <div class="change-password">
      <h3>Ganti Kata Sandi</h3>
      <form @submit.prevent="changePassword">
        <div class="form-group">
          <label for="current-password">Kata Sandi Saat Ini</label>
          <input
            v-model="currentPassword"
            type="password"
            id="current-password"
            required
          />
        </div>
        <div class="form-group">
          <label for="new-password">Kata Sandi Baru</label>
          <input
            v-model="newPassword"
            type="password"
            id="new-password"
            required
          />
        </div>
        <div class="form-group">
          <label for="confirm-password">Konfirmasi Kata Sandi Baru</label>
          <input
            v-model="confirmPassword"
            type="password"
            id="confirm-password"
            required
          />
          <small v-if="passwordError">{{ passwordError }}</small>
        </div>
        <button type="submit" class="btn-primary">Simpan Perubahan</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";

const currentEmail = ref("user@example.com"); // Email yang terhubung
const currentPassword = ref("");
const newPassword = ref("");
const confirmPassword = ref("");
const passwordError = ref("");

const changePassword = () => {
  if (newPassword.value !== confirmPassword.value) {
    passwordError.value = "Kata sandi tidak cocok";
    return;
  }
  alert("Kata sandi berhasil diubah");
};

const connectToFacebook = () => {
  alert("Menghubungkan ke Facebook...");
  // Tambahkan logika untuk menghubungkan ke Facebook
};

const connectToInstagram = () => {
  alert("Menghubungkan ke Instagram...");
  // Tambahkan logika untuk menghubungkan ke Instagram
};

const connectToEmail = () => {
  alert("Menghubungkan ke Email...");
  // Tambahkan logika untuk menghubungkan ke akun Email
};
</script>

<style scoped>
.account-settings {
  background-color: #222;
  color: #fff;
  padding: 20px;
  border-radius: 5px;
  max-width: 600px;
  margin: 0 auto;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  border: 1px solid #333;
  border-radius: 5px;
  background-color: #333;
  color: #fff;
}

.social-connections {
  margin-bottom: 30px;
}

.social-connections h3 {
  margin-bottom: 10px;
}

.social-btn {
  background-color: #333;
  color: #fff;
  border: none;
  padding: 10px 15px;
  cursor: pointer;
  border-radius: 5px;
  margin-right: 10px;
  margin-bottom: 10px;
}

.facebook-btn {
  background-color: #4267b2;
}

.instagram-btn {
  background-color: #e1306c;
}

.email-btn {
  background-color: #333;
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
