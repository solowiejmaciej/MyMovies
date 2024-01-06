<template>
  <div class="table-container">
    <table class="table table-striped table-dark w-40">
      <thead>
      <tr>
        <th scope="col">Id</th>
        <th scope="col">Title</th>
        <th scope="col">Director</th>
        <th scope="col">Rate</th>
        <th scope="col">Year</th>
        <th scope="col"></th>
        <th scope="col"></th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="(movie) in movies" :key="movie.id">
        <td>{{ movie.id }}</td>
        <td>{{ movie.title }}</td>
        <td>{{ movie.director }}</td>
        <td>{{ movie.rate }}</td>
        <td>{{ movie.year }}</td>
        <td><EditMovieButton :movieId="movie.id" :title="movie.title" :director="movie.director" :rate="movie.rate" :year="movie.year" @somethingWentWrong="handleSomethingWentWrong" @moviesChanged="handleMoviesChanged"/></td>
        <td><DeleteMovieButton :movieId="movie.id" @moviesChanged="handleMoviesChanged" @somethingWentWrong="handleSomethingWentWrong"/></td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>

import DeleteMovieButton from './Buttons/DeleteMovieButton.vue';
import EditMovieButton from './Buttons/EditMovieButton.vue';


export default {
  emits: ['moviesChanged', 'somethingWentWrong'],
  props: {
    movies: Array
  },
  components: {
    DeleteMovieButton,
    EditMovieButton
},

  methods: {
    handleMoviesChanged() {
      this.$emit('moviesChanged');
    },

    handleSomethingWentWrong() {
      this.$emit('somethingWentWrong');
    },
    
  }
}
</script>

<style scoped>
.table-container {
  width: 40%;
  margin: auto;
}
</style>

