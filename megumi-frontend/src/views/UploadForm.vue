<template>
  <div class="upload-container">
    <h1>Upload Media</h1>
    <form @submit.prevent="handleUpload">
      <div class="form-group">
        <label for="title">Title:</label>
        <input type="text" v-model="title" id="title" required />
      </div>

      <div class="form-group">
        <label for="file">Upload File:</label>
        <input type="file" @change="handleFileChange" id="file" required />
      </div>

      <button type="submit">Upload</button>
    </form>

    <div v-if="previewUrl" class="media-preview">
      <h2>Preview</h2>
      <img v-if="isImage" :src="previewUrl" alt="Preview" class="preview-img" />
      <video v-if="isVideo" controls class="preview-video">
        <source :src="previewUrl" type="video/mp4" />
        Your browser does not support the video tag.
      </video>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { uploadFile } from "../services/apiService"; // Import service API untuk upload

const title = ref("");
const file = ref(null);
const previewUrl = ref("");
const isImage = ref(false);
const isVideo = ref(false);

const router = useRouter();

const handleFileChange = (event) => {
  const selectedFile = event.target.files[0];
  if (selectedFile) {
    file.value = selectedFile;
    previewUrl.value = URL.createObjectURL(selectedFile);
    isImage.value = selectedFile.type.startsWith("image/");
    isVideo.value = selectedFile.type.startsWith("video/");
  }
};

const handleUpload = async () => {
  const formData = new FormData();
  formData.append("title", title.value);
  formData.append("file", file.value);

  try {
    await uploadFile(formData); // Panggil API upload
    router.push("/media-gallery"); // Arahkan ke halaman galeri setelah upload
  } catch (error) {
    console.error(error);
  }
};
</script>

<style scoped>
.upload-container {
  max-width: 600px;
  margin: 50px auto;
  padding: 20px;
  background-color: #222;
  border-radius: 8px;
}

.form-group {
  margin-bottom: 20px;
}

label {
  color: white;
}

input[type="text"],
input[type="file"] {
  width: 100%;
  padding: 10px;
  margin-top: 5px;
  border-radius: 4px;
  background-color: #333;
  color: white;
  border: none;
}

button {
  background-color: #e50914;
  color: white;
  padding: 10px 20px;
  border: none;
  cursor: pointer;
  border-radius: 4px;
}

button:hover {
  background-color: #f40612;
}

.media-preview {
  margin-top: 20px;
}

.preview-img,
.preview-video {
  max-width: 100%;
  border-radius: 8px;
}
</style>
