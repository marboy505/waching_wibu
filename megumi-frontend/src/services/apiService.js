import axios from "axios";

const API_URL = "http://localhost:5081/api"; // Sesuaikan dengan URL backend

// Fungsi untuk mengunggah file
export const uploadFile = async (formData) => {
  try {
    const response = await axios.post(`${API_URL}/upload`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to upload file" };
  }
};

// Ambil data media (jika diperlukan)
export const fetchMedia = async () => {
  try {
    const response = await axios.get(`${API_URL}/media`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to fetch media" };
  }
};

// Login pengguna
export const loginUser = async (email, password) => {
  try {
    const response = await axios.post(`${API_URL}/login`, { email, password });
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to login" };
  }
};

// Registrasi pengguna baru
export const registerUser = async (name, email, password, confirmPassword) => {
  try {
    const response = await axios.post(`${API_URL}/register`, {
      name,
      email,
      password,
      confirmPassword,
    });
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to register" };
  }
};

// Ambil semua pengguna
export const getAllUsers = async () => {
  try {
    const response = await axios.get(`${API_URL}/users`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to fetch users" };
  }
};

// Fungsi untuk mengambil data pengguna berdasarkan ID
export const getUserById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/users/${id}`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "User not found" };
  }
};

// Fungsi untuk memperbarui data pengguna
export const updateUser = async (id, updatedUser) => {
  try {
    const response = await axios.put(`${API_URL}/users/${id}`, updatedUser, {
      headers: {
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to update user" };
  }
};

// Hapus data pengguna berdasarkan ID
export const deleteUser = async (id) => {
  try {
    const response = await axios.delete(`${API_URL}/users/${id}`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to delete user" };
  }
};

// Ambil semua gambar berdasarkan User ID
export const getPicturesByUserId = async (userId) => {
  try {
    const response = await axios.get(`${API_URL}/users/${userId}/pictures`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to fetch pictures" };
  }
};

// Hapus gambar berdasarkan ID
export const deletePicture = async (pictureId) => {
  try {
    const response = await axios.delete(`${API_URL}/pictures/${pictureId}`);
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to delete picture" };
  }
};

// Tambahkan gambar ke database
export const uploadPicture = async (picture) => {
  try {
    const response = await axios.post(`${API_URL}/pictures`, picture, {
      headers: {
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    throw error.response?.data || { message: "Failed to upload picture" };
  }
};
