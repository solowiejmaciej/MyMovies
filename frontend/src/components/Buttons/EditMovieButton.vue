<template>
  <button @click="openEditMovieModal" class="btn btn-primary">Edit</button>
  <div v-if="showModal">
    <Modal
        @disableModal="disableModal"
        :movieId="movieId"
        :title="title"
        :director="director"
        :rate="rate"
        :year="year"
        @moviesChanged="moviesChanged"
        @somethingWentWrong="somethingWentWrong"
        :mode="'edit'"
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
  emits: ['moviesChanged', 'somethingWentWrong'],
  components: {
    Modal
  },
  props: {
    movieId: {
      required: true
    },
    title: {
      required: true
    },
    director: {
      required: true
    },
    year: {
      required: true
    },
    rate: {
      required: true
    }

  },
  methods: {
    openEditMovieModal() {
      this.showModal = true;
    },
    disableModal() {
      this.showModal = false;
    },
    moviesChanged() {
      this.$emit('moviesChanged');
    },
    somethingWentWrong() {
      console.log("Something went wrong");
      this.$emit('somethingWentWrong');
    }
  }
}
</script>