<template>
  <div class="backdrop">
    <div class="myModal">
      <button class="close-button" @click="disableModal"> &times; </button>
      <input type="text" class="form-control" placeholder="Title" v-model="localTitle">
      <div v-if="v$.localTitle.$error" class="error">Title is required and it can't be longer than 200 characters</div>
      <input type="text" class="form-control" placeholder="Director" v-model="localDirector">
      <input type="number" class="form-control" placeholder="Year" v-model="localYear">
      <div v-if="v$.localYear.$error" class="error">Year must be between 1900 and 2200.</div>
      <input type="text" class="form-control" placeholder="Rate" v-model="localRate">
      <br>
      <button class="btn btn-primary" @click="editMovie" >Save</button>
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
  emits: ['disableModal', 'moviesChanged', 'somethingWentWrong'],
  validations() {
    return {
      localTitle: { required, maxLength: maxLength(200) },
      localYear: { required, between: between(1900, 2200) }
    }
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
  data() {
    return {
      localTitle: this.title,
      localDirector: this.director,
      localYear: this.year,
      localRate: this.rate
    }
  },
  methods: {
    editMovie() {
      this.v$.$validate()
      if (!this.v$.$error) {
        const movieData = {
          title: this.localTitle,
          year: this.localYear,
        };

        if (this.localDirector) {
          movieData.director = this.localDirector;
        }

        if (this.localRate) {
          movieData.rate = this.localRate;
        }

        axios.put(`${BASE_URL}/movies/` + this.movieId, movieData)
            .then(response => {
              this.$emit('moviesChanged')
              this.disableModal();
            })
            .catch(error => {
              this.$emit('somethingWentWrong')
            })
      }
    },
    disableModal() {
      this.$emit('disableModal');
    }
  },
}
</script>

<style scoped>
.backdrop{
  padding:0;
  margin:0;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(16, 15, 15, 0.7);
  width: 100%;
  height: 100%;
  z-index: 100;
  display: flex;
  justify-content: center;
  align-items: center;
}
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