import { defineStore } from "pinia";
import { ref } from "vue";
import { loginUser, registerUser } from "../services/apiService";

export const useUserStore = defineStore("user", () => {
  const isAuthenticated = ref(false);
  const user = ref(null);

  // Fungsi untuk memuat data dari localStorage saat aplikasi dimulai
  const loadFromLocalStorage = () => {
    const storedUser = localStorage.getItem("user");
    const storedAuth = localStorage.getItem("isAuthenticated");

    if (storedUser && storedAuth === "true") {
      user.value = JSON.parse(storedUser);
      isAuthenticated.value = true;
    }
  };

 // Fungsi login
 const login = async ({ email, password }) => {
  try {
    const response = await loginUser(email, password);

    // Set status login dan simpan data pengguna
    isAuthenticated.value = true;
    user.value = {
      id: response.user.id, // Simpan ID pengguna
      name: response.user.name,
      email: response.user.email,
    };

    // Simpan ke localStorage
    localStorage.setItem("user", JSON.stringify(user.value));
    localStorage.setItem("isAuthenticated", "true");

    console.log("User logged in:", user.value);
    return { success: true };
  } catch (error) {
    console.error("Login failed:", error.message || "Unknown error");
    return { success: false };
  }
};
  
  // Fungsi register
  const register = async ({ name, email, password, confirmPassword }) => {
    try {
      console.log("Registering new user:", { name, email });

      // Panggil API register
      await registerUser(name, email, password, confirmPassword);

      console.log("Registration successful for:", email);
      return { success: true, message: "Registration successful. You can now log in." };
    } catch (error) {
      console.error("Registration failed:", error.message || "Unknown error");
      return { success: false, message: error.message || "Registration failed. Please try again." };
    }
  };

// Fungsi logout
const logout = () => {
  console.log("Logging out user:", user.value);

  // Reset status login dan data pengguna
  isAuthenticated.value = false;
  user.value = null;

  // Hapus dari localStorage
  localStorage.removeItem("user");
  localStorage.removeItem("isAuthenticated");

  console.log("Logout successful.");
};

// Panggil loadFromLocalStorage saat store dibuat
loadFromLocalStorage();

return { isAuthenticated, user, login, register, logout, loadFromLocalStorage };
});