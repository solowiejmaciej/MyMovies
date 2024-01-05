<template>
    <button @click="deleteMovie" class="btn btn-danger">Delete</button>
</template>

<script>
import { BASE_URL } from '../../const/apiConfig';

export default {
    props: {
        movieId: {
            required: true
        }
    },
    emits: ['moviesChanged', 'somethingWentWrong'],
    methods: {
        deleteMovie() {
            if (confirm('Are you sure you want to delete this movie?')) {
                fetch(`${BASE_URL}/movies/${this.movieId}`, {
                    method: 'DELETE'
                })
                    .then(response => {
                        if (response.ok) {
                            this.$emit('moviesChanged');
                        } else {
                            this.$emit('somethingWentWrong');
                        }
                    })
                    .catch(error => {
                        console.error(error);
                        this.$emit('somethingWentWrong');
                    });
            }
        }
    }
}
</script>