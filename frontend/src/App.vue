<template>
  <div>
    <button @click="addMovie">Dodaj</button>
    <table>
      <tr v-for="(movie, index) in movies" :key="index">
        <td>{{ movie.title }}</td>
        <td><button @click="editMovie(index)">Edytuj</button></td>
        <td><button @click="deleteMovie(index)">Usuń</button></td>
      </tr>
    </table>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      movies: [] // Tutaj będą przechowywane dane o filmach
    }
  },
  created() {
    axios.get('http://localhost:5120/api/movies')
      .then(response => {
        this.movies = response.data;
      })
      .catch(error => {
        console.error(error);
      });
  },
  methods: {
    addMovie() {
      // Tutaj powinna być logika dodawania filmu
    },
    editMovie(index) {
      // Tutaj powinna być logika edycji filmu
    },
    deleteMovie(index) {
      if (confirm('Czy na pewno chcesz usunąć ten film?')) {
        this.movies.splice(index, 1);
      }
    }
  }
}
</script>

<style scoped>

</style>