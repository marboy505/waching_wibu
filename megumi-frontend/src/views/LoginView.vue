<template>
  <div class="modal-overlay" @click.self="closeModal">
    <div :class="['modal-content', animationClass]">
      <h2 v-if="isRegister">Register</h2>
      <h2 v-else>Login</h2>
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
        <button type="submit">{{ isRegister ? "Register" : "Login" }}</button>
      </form>
      <button class="toggle-btn" @click="toggleForm">
        {{ isRegister ? "Switch to Login" : "Switch to Register" }}
      </button>
      <button class="close-btn" @click="closeModal">Close</button>
    </div>
  </div>
</template>

<script setup>
import { ref, defineEmits, defineProps } from "vue";
import { useRouter } from "vue-router";
import { useUserStore } from "../store/userStore";

const name = ref("");
const email = ref("");
const password = ref("");
const confirmPassword = ref("");
const isRegister = ref(true);
const animationClass = ref("");
const userStore = useUserStore();
const emit = defineEmits(["close"]);
const props = defineProps({
  isOpen: Boolean,
});

const router = useRouter();

const closeModal = () => {
  emit("close");
  router.push("/");
};

const toggleForm = () => {
  animationClass.value = "rotate-animation";
  setTimeout(() => {
    isRegister.value = !isRegister.value;
    animationClass.value = "";
  }, 500); // Sesuaikan durasi untuk mencocokkan durasi animasi CSS
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
    closeModal();
  }
};

const login = async () => {
  const success = await userStore.login({
    email: email.value,
    password: password.value,
  });
  if (success) {
    closeModal();
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
  transition: transform 0.5s ease;
}

.rotate-animation {
  transform: rotateY(360deg);
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

.toggle-btn {
  margin-top: 10px;
  background-color: #007bff;
}

.close-btn {
  margin-top: 10px;
  background-color: gray;
}
</style>
