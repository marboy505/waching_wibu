<template>
  <div class="modal-overlay" @click.self="closeModal">
    <div class="modal-content">
      <h2>Register</h2>
      <form @submit.prevent="register">
        <input v-model="name" placeholder="Name" type="text" required />
        <input v-model="email" placeholder="Email" type="email" required />
        <input
          v-model="password"
          placeholder="Password"
          type="password"
          required
        />
        <input
          v-model="confirmPassword"
          placeholder="Confirm Password"
          type="password"
          required
        />
        <button type="submit">Register</button>
      </form>
      <button class="close-btn" @click="closeModal">Close</button>
    </div>
  </div>
</template>

<script setup>
import { ref, defineEmits, defineProps } from "vue";
import { useRouter } from "vue-router"; // Import useRouter untuk navigasi
import { useUserStore } from "../store/userStore"; // Asumsi store untuk user sudah ada

const name = ref("");
const email = ref("");
const password = ref("");
const confirmPassword = ref("");
const userStore = useUserStore();
const emit = defineEmits(["close"]);
const props = defineProps({
  isOpen: Boolean, // menerima prop isOpen untuk menentukan apakah modal ditampilkan
});

const router = useRouter(); // Menginisialisasi router

const closeModal = () => {
  console.log("closeModal called");
  emit("close"); // Memancarkan event close ke komponen induk
  router.push("/"); // Pindah ke halaman "/"
};

const register = async () => {
  if (password.value !== confirmPassword.value) {
    alert("Passwords do not match!");
    return;
  }

  const success = await userStore.register({
    name: name.value,
    email: email.value,
    password: password.value,
  });

  if (success) {
    closeModal(); // Panggil fungsi closeModal setelah berhasil register
    router.push("/login"); // Pindah ke halaman login setelah berhasil register
  }
};
</script>

<style scoped>
.modal-overlay {
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

.modal-content {
  background-color: #222;
  padding: 40px;
  border-radius: 8px;
  width: 400px;
  max-width: 90vw;
}

input {
  display: block;
  width: 100%;
  margin-bottom: 15px;
  padding: 10px;
  border: none;
  border-radius: 4px;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #e50914;
  color: white;
  border: none;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #f40612;
}

.close-btn {
  margin-top: 10px;
  background-color: gray;
}
</style>
