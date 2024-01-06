<template>
  <div class="d-flex justify-content-center button-container">
    <button class="btn btn-primary m-3" @click="showMovieModal">Add new movie</button>
  </div>
  <div v-if="showModal">
    <Modal
        @disableModal="disableModal"
        @moviesChanged="handleMoviesChanged"
        @somethingWentWrong="somethingWentWrong"
        :mode="'add'"
    />
  </div>
</template>

<script>
import Modal from '../Modals/Modal.vue';

export default {
  data() {
    return {
      showModal: false
    }
  },
  emits: ['moviesChanged' , 'somethingWentWrong'],
  components: {
    Modal
  },
  methods: {
    showMovieModal() {
      this.showModal = true;
    },
    disableModal() {
      this.showModal = false;
    },
    handleMoviesChanged() {
      this.$emit('moviesChanged');
    },
    somethingWentWrong() {
      this.$emit('somethingWentWrong');
    }
  },
}
</script>