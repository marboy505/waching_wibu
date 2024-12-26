<template>
  <div class="profile-page">
    <header class="profile-header">
    <div class="profile-picture">
      <img :src="user.avatar || 'default-avatar.png'" alt="Profile Picture" />
    </div>
    <div class="profile-info">
      <h1>{{ user.name }}</h1>
      <p>Email: {{ user.email }}</p>
    </div>
  </header>


  <!-- Overlay for Editing Profile -->
  <div v-if="showOverlay" class="overlay">
    <div class="overlay-content">
      <button class="close-overlay" @click="showOverlay = false">×</button>
      <h2>Edit Profile</h2>
      <div class="overlay-form">
        <input
          type="file"
          ref="fileInput"
          @change="onFileChange"
          accept="image/*"
          style="display: none"
        />
        <button class="edit-photo-button" @click="triggerFileInput">
          Edit Photo
        </button>
        <div v-if="photoSelected" class="photo-preview">
          <img :src="user.avatar || 'default-avatar.png'" alt="Profile Picture" />
        </div>
        <input v-model="user.name" placeholder="Edit Name" />
        <input
          v-model="user.email"
          placeholder="Edit Email"
          @blur="updateEmail"
        />
        <button class="save-button" @click="saveProfile">Save Changes</button>
      </div>
    </div>
  </div>
    <button class="edit-profile-button" @click="showOverlay = true">
       Edit Profile
    </button>
    <div class="settings-container">
      <button class="settings-button" @click="toggleSettings">
        {{ showSettings ? "Hide Settings" : "Show Settings" }}
      </button>

      <div v-if="showSettings" class="settings-tabs">
        <button
          @click="currentTab = 'password'"
          :class="{ active: currentTab === 'password' }"
        >
          Change Password
        </button>
        <button
          @click="currentTab = 'privacy'"
          :class="{ active: currentTab === 'privacy' }"
        >
          Privacy Settings
        </button>
        <button
          @click="currentTab = 'content'"
          :class="{ active: currentTab === 'content' }"
        >
          Content Preferences
        </button>
        <button
          @click="currentTab = 'notifications'"
          :class="{ active: currentTab === 'notifications' }"
        >
          Notification Settings
        </button>
        <button
          @click="currentTab = 'streaming'"
          :class="{ active: currentTab === 'streaming' }"
        >
          Streaming Settings
        </button>
        <button
          @click="currentTab = 'subscription'"
          :class="{ active: currentTab === 'subscription' }"
        >
          Subscription Info
        </button>
        <button
          @click="currentTab = 'help'"
          :class="{ active: currentTab === 'help' }"
        >
          Help & Support
        </button>
      </div>

      <div v-if="showSettings" class="settings-content">
        <ChangePassword v-if="currentTab === 'password'" />
        <PrivacySettings v-if="currentTab === 'privacy'" />
        <ContentPreferences v-if="currentTab === 'content'" />
        <NotificationSettings v-if="currentTab === 'notifications'" />
        <StreamingSettings v-if="currentTab === 'streaming'" />
        <SubscriptionInfo v-if="currentTab === 'subscription'" />
        <HelpSupport v-if="currentTab === 'help'" />
      </div>
    </div>
    <button class="logout-button" @click="logout">Log Out</button>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useUserStore } from "../store/userStore";
import { getUserById, updateUser, uploadFile } from "../services/apiService";

const userStore = useUserStore();
const showOverlay = ref(false);
const photoSelected = ref(false);
const fileInput = ref(null);

const onFileChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    if (!file.type.startsWith("image/")) {
      alert("Please upload a valid image file.");
      return;
    }

    if (file.size > 2 * 1024 * 1024) { // Maksimal 2MB
      alert("File size exceeds 2MB. Please upload a smaller file.");
      return;
    }

    user.value.avatar = URL.createObjectURL(file); // Preview lokal
    photoSelected.value = true;
  }
};

// Ambil ID user dari store
const userId = computed(() => userStore.user?.id);

// State lokal untuk menyimpan data user
const user = ref({
  name: "",
  email: "",
  avatar: "default-avatar.png",
});

// Fungsi untuk mengambil data user berdasarkan ID
const fetchUserData = async () => {
  try {
    if (!userId.value) {
      console.error("User ID not found");
      return;
    }

    const userData = await getUserById(userId.value);
    user.value.name = userData.name;
    user.value.email = userData.email;
    user.value.avatar = userData.avatar || "default-avatar.png";
  } catch (error) {
    console.error("Failed to fetch user data:", error.message);
  }
};

onMounted(fetchUserData);


// Fungsi untuk menyimpan perubahan profil
const saveProfile = async () => {
  try {
    if (!userId.value) {
      console.error("User ID not found");
      return;
    }

    let uploadedAvatarUrl = user.value.avatar;

    // Jika foto baru dipilih, unggah ke server
    if (photoSelected.value && fileInput.value?.files[0]) {
      const formData = new FormData();
      formData.append("file", fileInput.value.files[0]);

      try {
          const uploadResponse = await uploadFile(formData);
          uploadedAvatarUrl = uploadResponse.url;
        } catch (error) {
          console.error("Failed to upload file:", error.message);
          alert("Failed to upload file. Please try again.");
          return; // Keluar jika unggahan gagal
        }
    }

    // Simpan data profil ke server
    await updateUser(userId.value, {
      name: user.value.name,
      email: user.value.email,
      avatar: uploadedAvatarUrl, // Gunakan URL avatar dari server
    });

    alert("Profile updated successfully");
    showOverlay.value = false;
    photoSelected.value = false;
    fetchUserData(); // Refresh data profil
  } catch (error) {
    console.error("Failed to update profile:", error.message);
    alert("Failed to update profile");
  }
};


// Fungsi untuk menangani input file avatar
const triggerFileInput = () => {
  if (fileInput.value) {
    fileInput.value.click();
  }
};

// Fungsi logout
const logout = () => {
  userStore.logout();
  alert("Logged out");
};

// Panggil fetchUserData saat komponen dimuat
onMounted(fetchUserData);
</script>


<style scoped>
.profile-page {
  background-color: #141414;
  color: #fff;
  padding: 20px;
  min-height: 100vh;
}

.profile-header {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
}

.profile-picture {
  position: relative;
  margin-bottom: 10px;
}

.profile-picture img {
  border-radius: 50%;
  width: 120px;
  height: 120px;
  object-fit: cover;
}

.profile-info h1 {
  margin: 10px 0;
  font-size: 1.5em;
}

.profile-info p {
  margin: 0;
}

.profile-info input {
  background: #333;
  color: #fff;
  border: 1px solid #555;
  border-radius: 5px;
  padding: 5px;
}

.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
}

.overlay-content {
  background: #222;
  padding: 20px;
  border-radius: 10px;
  position: relative;
  width: 80%;
  max-width: 600px;
}

.close-overlay {
  position: absolute;
  top: 10px;
  right: 10px;
  background: #e50914;
  color: #fff;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 50%;
}

.overlay-form {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.edit-photo-button {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
}

.photo-preview img {
  width: 150px;
  height: 150px;
  border-radius: 50%;
  object-fit: cover;
  margin-top: 10px;
}

.save-button {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
  margin-top: 10px;
}

.edit-profile-button {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
  margin-top: 20px;

  /* Flexbox to center the button horizontally */
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 0 auto; /* Center the button horizontally */
}

.settings-container {
  margin-top: 20px;
}

.settings-button {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
  margin-bottom: 10px;
}

.settings-tabs {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.settings-tabs button {
  background-color: #333;
  border: none;
  color: #fff;
  padding: 10px;
  cursor: pointer;
  border-radius: 5px;
}

.settings-tabs button.active {
  background-color: #e50914;
}

.settings-content {
  margin-top: 20px;
}

.logout-button {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
  position: fixed;
  bottom: 20px;
  right: 20px;
}

@media (max-width: 768px) {
  .profile-header {
    flex-direction: column;
    align-items: center;
  }

  .profile-info h1 {
    font-size: 1.2em;
  }

  .profile-picture img {
    width: 100px;
    height: 100px;
  }

  .settings-tabs {
    flex-direction: column;
    align-items: center;
  }

  .settings-tabs button {
    width: 100%;
  }
}
</style>
