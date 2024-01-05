<template>
  <div class="alert alert-danger alert-dismissible fade show" role="alert">
    <button class="close-button" @click="$emit('turnOffSomethingWentWrong')"> &times; </button>
    An error occurred while fetching data from the API.
    Try again later.
  </div>
</template>

<script>
export default {
  name: 'ApiError',
}
</script>

<style scoped>
.close-button {
  position: absolute;
  right: 5px;
  top: 1px;
  border: none;
  background: none;
  font-size: 1.8em;
}
</style>