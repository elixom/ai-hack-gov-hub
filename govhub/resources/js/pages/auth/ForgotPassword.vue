<template>
    <div class="w-full max-w-md mx-auto">
        <div class="p-8 space-y-6 bg-white shadow-lg rounded-xl">
            <div class="text-center">
                <h2 class="text-2xl font-semibold text-gray-900">
                    Reset your password
                </h2>
                <p class="mt-1 text-sm text-gray-600">
                    Enter the email associated with your account and we'll send
                    a reset link.
                </p>
            </div>

            <form @submit.prevent="onSubmit" class="space-y-4">
                <p
                    v-if="success"
                    class="p-2 mb-2 text-sm text-green-600 bg-green-100 rounded-md"
                >
                    <i class="mr-3 fas fa-check-circle"></i> A reset link has
                    been sent to your email.
                </p>

                <div>
                    <field-label>Email</field-label>
                    <text-input
                        v-model="form.email"
                        type="email"
                        autocomplete="off"
                        class="mt-1"
                    />
                </div>

                <!-- Error / success -->
                <p v-if="error" class="text-sm text-red-600">{{ error }}</p>

                <!-- Actions -->
                <div class="space-y-4">
                    <action-button
                        type="submit"
                        class="w-full btn-primary"
                        :processing="isSubmitting"
                    >
                        Send reset link
                    </action-button>

                    <div class="text-sm text-center text-gray-600 capitalize">
                        <router-link :to="{ name: 'auth.login' }">
                            Back to login
                        </router-link>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
import { Auth } from "@/models";
import TextInput from "@/components/form/TextInput.vue";
import FieldLabel from "@/components/form/FieldLabel.vue";
export default {
    components: {
        TextInput,
        FieldLabel,
    },

    data() {
        return {
            form: {},
            isSubmitting: false,
            error: this.initialError,
            success: this.initialSuccess,
        };
    },

    methods: {
        onSubmit() {
            this.error = "";
            this.success = false;

            let validation = new this.Validator(this.form, {
                email: "required|email",
            });

            if (!validation.passes()) {
                this.error = validation.errors.errors.email[0];
                return;
            }

            this.isSubmitting = true;

            Auth.requestPasswordReset(this.form)
                .then(() => {
                    this.success = true;
                    this.form.email = "";
                })
                .catch((err) => {
                    this.error = "An error occurred. Please try again.";
                })
                .finally(() => {
                    this.isSubmitting = false;
                });
        },
    },
};
</script>
