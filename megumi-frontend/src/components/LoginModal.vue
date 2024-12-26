<template>
  <div class="modal-overlay" @click.self="closeModal">
    <div :class="['modal-content', animationClass]">
      <span class="close-icon" @click="closeModal">
        <i class="fas fa-times"></i>
      </span>
      <h2>{{ isRegister ? "Register" : "Login" }}</h2>
      <form @submit.prevent="isRegister ? register() : login()">
        <input
          v-if="isRegister"
          v-model="name"
          placeholder="Name"
          type="text"
          required
        />
        <input v-model="email" placeholder="Email" type="email" required />
        <input
          v-model="password"
          placeholder="Password"
          type="password"
          required
        />
        <input
          v-if="isRegister"
          v-model="confirmPassword"
          placeholder="Confirm Password"
          type="password"
          required
        />
        <button type="submit" :disabled="isLoading">
          <span v-if="isLoading">Processing...</span>
          <span v-else>{{ isRegister ? "Register" : "Login" }}</span>
        </button>
        <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
      </form>
      <span class="toggle-link" @click="toggleForm">
        {{ isRegister ? "Switch to Login" : "Switch to Register" }}
      </span>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useUserStore } from "../store/userStore";

const name = ref("");
const email = ref("");
const password = ref("");
const confirmPassword = ref("");
const isRegister = ref(true);
const animationClass = ref("");
const isLoading = ref(false);
const errorMessage = ref("");

const router = useRouter();
const userStore = useUserStore();

const closeModal = () => {
  // Reset semua form input saat modal ditutup
  name.value = "";
  email.value = "";
  password.value = "";
  confirmPassword.value = "";
  errorMessage.value = "";
  isRegister.value = true; // Default ke form register
  router.push("/"); // Arahkan kembali ke halaman utama
};

const toggleForm = () => {
  animationClass.value = "rotate-animation";
  setTimeout(() => {
    isRegister.value = !isRegister.value;
    animationClass.value = "";
    errorMessage.value = ""; // Reset error saat toggle
  }, 300);
};

const validateEmail = (email) => {
  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regex.test(email);
};

const register = async () => {
  errorMessage.value = ""; // Reset error message
  if (!name.value.trim()) {
    errorMessage.value = "Name is required!";
    return;
  }
  if (name.value.length < 3) {
    errorMessage.value = "Name must be at least 3 characters!";
    return;
  }
  if (!validateEmail(email.value)) {
    errorMessage.value = "Invalid email format!";
    return;
  }
  if (password.value.length < 6) {
    errorMessage.value = "Password must be at least 6 characters!";
    return;
  }
  if (password.value !== confirmPassword.value) {
    errorMessage.value = "Passwords do not match!";
    return;
  }
  isLoading.value = true;
  try {
    const result = await userStore.register({
      name: name.value.trim(),
      email: email.value.trim(),
      password: password.value,
      confirmPassword: confirmPassword.value,
    });
    if (result.success) {
      alert("Registration successful. Please login.");
      isRegister.value = false; // Alihkan ke form login
    } else {
      errorMessage.value = result.message || "Registration failed.";
    }
  } catch (error) {
    errorMessage.value = error.message || "Registration failed. Please try again.";
  } finally {
    isLoading.value = false;
  }
};

const login = async () => {
  errorMessage.value = ""; // Reset error message
  if (!validateEmail(email.value)) {
    errorMessage.value = "Invalid email format!";
    return;
  }
  if (password.value.length < 6) {
    errorMessage.value = "Password must be at least 6 characters!";
    return;
  }
  isLoading.value = true;
  try {
    const result = await userStore.login({
      email: email.value.trim(),
      password: password.value,
    });
    if (result.success) {
      alert("Login successful");
      closeModal(); // Tutup modal setelah login berhasil
    } else {
      errorMessage.value = result.message || "Invalid email or password.";
    }
  } catch (error) {
    errorMessage.value = error.message || "Login failed. Please try again.";
  } finally {
    isLoading.value = false;
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
  color: aliceblue;
}

.modal-content {
  background: linear-gradient(145deg, #1e1e1e, #272727);
  border-radius: 16px;
  padding: 40px;
  width: 400px;
  max-width: 90vw;
  position: relative;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
  transition: transform 0.3s ease;
}

.rotate-animation {
  transform: rotateY(180deg);
}

.close-icon {
  position: absolute;
  top: 10px;
  right: 15px;
  color: #e50914;
  font-size: 24px;
  cursor: pointer;
}

input {
  display: block;
  width: 100%;
  margin-bottom: 15px;
  padding: 10px;
  border: none;
  border-radius: 4px;
  background-color: #333;
  color: white;
}

button {
  width: 100%;
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

.toggle-link {
  display: inline-block;
  margin-top: 10px;
  color: #007bff;
  cursor: pointer;
  font-size: 14px;
  text-decoration: underline;
}

.error-message {
  color: #f00;
  margin-top: 10px;
  font-size: 14px;
}
</style>
