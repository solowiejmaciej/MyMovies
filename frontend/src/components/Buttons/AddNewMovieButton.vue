<template>
  <div class="d-flex justify-content-center button-container">
    <button class="btn btn-primary m-3" @click="showMovieModal">Add new movie</button>
  </div>
  <div v-if="showModal">
    <AddNewMovieModal @toggleModal="toggleModal" @moviesChanged="handleMoviesChanged" @somethingWentWrong="somethingWentWrong"/>
  </div>
</template>

<script>
import AddNewMovieModal from '../Modals/AddNewMovieModal.vue';

export default {
    data() {
        return {
            showModal: false
        }
    },
    emits: ['moviesChanged' , 'somethingWentWrong'],
    components: {
        AddNewMovieModal
    },
    methods: {
        showMovieModal() {
            this.showModal = true;
        },
        toggleModal() {
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