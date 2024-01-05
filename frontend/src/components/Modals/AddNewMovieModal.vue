<template>
  <div class="backdrop">
    <div class="myModal">
      <button class="close-button" @click="toggleModal"> &times; </button>
      <input type="text" class="form-control" placeholder="Title" v-model="title">
      <div v-if="v$.title.$error" class="error">Title is required and it can't be longer than 200 characters</div>
      <input type="text" class="form-control" placeholder="Director" v-model="director">
      <input type="number" class="form-control" placeholder="Year" v-model="year">
      <div v-if="v$.year.$error" class="error">Year must be between 1900 and 2200.</div>
      <input type="text" class="form-control" placeholder="Rate" v-model="rate">
      <br>
      <button class="btn btn-primary" @click="addMovie" >Add</button>
    </div>
  </div>
</template>

<script>
import { useVuelidate } from '@vuelidate/core'
import { required, maxLength, between } from '@vuelidate/validators'
import axios from 'axios';
import { BASE_URL } from '../../const/apiConfig';

export default {
  setup () {
    return { v$: useVuelidate() }
  },
  data() {
    return {
      title: '',
      director: '',
      year: '',
      rate: ''
    }
  },
  validations() {
    return {
      title: { required, maxLength: maxLength(200) },
      year: { required, between: between(1900, 2200) }
    }
  },
  emits: ['toggleModal', 'moviesChanged', 'somethingWentWrong'],
  methods: {
    addMovie() {
      this.v$.$validate()
      if (!this.v$.$error) {
        const movieData = {
          title: this.title,
          year: this.year,
        };

        if (this.director) {
          movieData.director = this.director;
        }

        if (this.rate) {
          movieData.rate = this.rate;
        }
        axios.post(`${BASE_URL}/movies`, movieData)
        .then(response => {
          this.$emit('moviesChanged');
          this.toggleModal();
        })
        .catch(error => {
          this.$emit('somethingWentWrong');
        });
      }
    },
    toggleModal() {
      this.$emit('toggleModal');
    }
  },
  created() {
    this.v$.$autoDirty = true;  // Enable automatic dirty state
  }
}
</script>

<style scoped>
.myModal {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  width: 300px;
  height: 380px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.close-button {
  position: absolute;
  right: 5px;
  top: 1px;
  border: none;
  background: none;
  font-size: 1.8em;
}

.error {
  color: red;
}
</style>