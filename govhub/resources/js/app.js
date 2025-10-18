import { createApp } from "vue";
import { createPinia } from "pinia";
import moment from "moment";
import Toast, { useToast } from "vue-toastification";
import "vue-toastification/dist/index.css";
import Validator from "validatorjs";
import val_messages from "../../lang/en/validation.json";

import App from "./App.vue";
import ActionButton from "@/components/ActionButton.vue";
import router from "./router/router";
import { useAuthStore } from "@/stores/auth";

// Create Pinia store
const pinia = createPinia();

// Create and mount Vue app
const app = createApp(App);

app.use(pinia);
app.use(router);
app.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 3,
    newestOnTop: true,
});
app.config.globalProperties.Validator = Validator.setMessages(
    "en",
    val_messages
);

app.config.globalProperties.moment = moment;
app.config.globalProperties.$store = useAuthStore();
app.config.globalProperties.$toast = useToast();

app.component("ActionButton", ActionButton);

app.mount("#app");
