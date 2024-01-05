<template>
  <div class="d-flex justify-content-center button-container">
    <button class="btn btn-primary m-3" @click="downloadMovies()">Download new movies</button>
  </div>
</template>

<script>
import axios from 'axios';
import { BASE_URL } from '../../const/apiConfig';

export default {
  emits: ['moviesChanged', 'somethingWentWrong'],
  methods: {
    downloadMovies() {
      axios.get(`${BASE_URL}/third-party-movies`)
          .then(response => {
            if (response.status !== 200){
              this.$emit('somethingWentWrong')
            } else {
              this.$emit('moviesChanged')
            }
          })
          .catch(error => {
            this.$emit('somethingWentWrong')
          });
    },
  },
}
</script>