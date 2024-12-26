import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia"; // Import Pinia
import "./styles/global.css";

const pinia = createPinia(); // Inisialisasi Pinia

const app = createApp(App);
app.use(router);
app.use(pinia); // Gunakan Pinia
app.mount("#app");
