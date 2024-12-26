<template>
  <MainLayout>
    <router-view />
    <transition name="fade">
      <div v-if="overlayVisible" class="overlay" @click.self="closeOverlay">
        <div class="overlay-content">
          <button
            class="close-overlay"
            @click="closeOverlay"
            aria-label="Close Overlay"
          >
            ✕
          </button>
          <component :is="currentOverlayComponent" />
        </div>
      </div>
    </transition>
  </MainLayout>
</template>

<script setup>
import { ref, computed } from "vue";
import MainLayout from "./layouts/MainLayout.vue";
import EditProfile from "./components/EditProfile.vue";
import AccountSettings from "./components/AccountSetting.vue";

const overlayVisible = ref(false);
const activeOverlay = ref("");

const currentOverlayComponent = computed(() => {
  switch (activeOverlay.value) {
    case "edit":
      return EditProfile;
    case "settings":
      return AccountSettings;
    default:
      return null;
  }
});

const openOverlay = (type) => {
  activeOverlay.value = type;
  overlayVisible.value = true;
};

const closeOverlay = () => {
  overlayVisible.value = false;
  activeOverlay.value = "";
};
</script>

<style>
/* Common styles */
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}
.overlay-content {
  background: #fff;
  border-radius: 5px;
  padding: 20px;
  width: 80%;
  max-width: 600px;
  position: relative;
}
.close-overlay {
  position: absolute;
  top: 10px;
  right: 10px;
  background: none;
  border: none;
  font-size: 1.5em;
  cursor: pointer;
}
</style>
