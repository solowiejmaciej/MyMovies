<script>
import MovieTable from './components/MovieTable.vue'
import NavBar from './components/NavBar.vue'
import AddNewMovieButton from './components/Buttons/AddNewMovieButton.vue';
import DownloadMoviesButton from './components/Buttons/DownloadMoviesButton.vue';
import ApiError from './components/ErrorMessages/ApiError.vue';
import axios from 'axios';
import {BASE_URL} from './const/apiConfig';


export default {
  data() {
    return {
      movies: [],
      hasError: false
    }
  },
  components: {
    MovieTable,
    NavBar,
    AddNewMovieButton,
    DownloadMoviesButton,
    ApiError
  },
  methods: {
    moviesChanged() {
      console.log("moviesChanged");
      axios.get(`${BASE_URL}/movies`)
        .then(response => {
          this.movies = response.data
        })
        .catch(error => {
          console.error(error)
        });
    },
    somethingWentWrong() {
      console.log("somethingWentWrong");
      this.hasError = true
    },
    turnOffSomethingWentWrong() {
      console.log("turnOffSomethingWentWrong");
      this.hasError = false
    },
    validationError() {
      console.log("validationError");
    }
  },
  created() {
    this.moviesChanged();
  },
}
</script>

<template>
  <NavBar />
  <div class="d-flex justify-content-center">
    <AddNewMovieButton @moviesChanged="moviesChanged" @somethingWentWrong="somethingWentWrong"/>
    <ApiError v-if="hasError" @turnOffSomethingWentWrong="turnOffSomethingWentWrong"/>
    <DownloadMoviesButton @moviesChanged="moviesChanged" @somethingWentWrong="somethingWentWrong" />
  </div>
  <MovieTable :movies="movies" @moviesChanged="moviesChanged" @somethingWentWrong="somethingWentWrong"/>
</template>

