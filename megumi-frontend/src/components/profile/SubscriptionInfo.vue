<template>
  <div class="subscription-info">
    <h2>Subscription Information</h2>
    <p>Plan: {{ subscription.plan }}</p>
    <p>Price: ${{ subscription.price }}</p>
    <p>Valid Until: {{ subscription.validUntil }}</p>
    <button @click="updatePaymentMethod" class="btn-primary">
      Update Payment Method
    </button>
  </div>
</template>

<script setup>
import { ref } from "vue";

const subscription = ref({
  plan: "Premium",
  price: "15.99",
  validUntil: "2024-12-31",
});

const updatePaymentMethod = () => {
  // Implement API call to update payment method
  alert("Update payment method");
};
</script>

<style scoped>
.subscription-info {
  background-color: #222;
  color: #fff;
  padding: 20px;
  border-radius: 5px;
}

.btn-primary {
  background-color: #e50914;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
}
</style>
