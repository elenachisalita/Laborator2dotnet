import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
    public movies: Movie[];

    public name: string = "test";

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.loadMovies();
    }


    loadMovies() {
        this.http.get<Movie[]>(this.baseUrl + 'api/Movies').subscribe(result => {
            this.movies = result;
            console.log(this.movies);
        }, error => console.error(error));
    }

    delete(movieId: string) {
        if (confirm('Are you sure you want to delete the movie with id ' + movieId + '?')) {
            this.http.delete(this.baseUrl + 'api/Movies/' + movieId)
                .subscribe
                (
                    result => {
                        alert('Movie successfully deleted!');
                        this.loadMovies();
                    },
                    error => alert('Cannot delete movie - maybe it has comments?')
                )
        }
    }

/*    submit() {

        var flower: Flower = <Flower>{};
        flower.name = this.name;
        flower.description = this.name;
        flower.dateAdded = new Date();
        flower.flowerUpkeepDifficulty = FlowerUpkeepDifficulty.Easy;
        flower.marketPrice = 5;

        this.http.post(this.baseUrl + 'api/Flowers', flower).subscribe(result => {
            console.log('success!');
            this.loadFlowers();
        },
        error => {    
            if (error.status == 400) {
                console.log(error.error.errors)
            }
        });*/
    //}

    submit() {

        var movie: Movie = <Movie>{};
        movie.title = this.name;
        movie.description = this.name;
        movie.genre = Genre.Action;
        movie.duration = 5;
        movie.yearOfRelease = 1990;
        movie.director = this.name;
        movie.dateAdded = new Date();
        movie.rating = 2;
        movie.watched = true;
        movie.dateAdded = new Date();
     

        this.http.post(this.baseUrl + 'api/Movies', movie).subscribe(result => {
            console.log('success!');
            this.loadMovies();
        },
            error => {
                if (error.status == 400) {
                    console.log(error.error.errors)
                }
            });
    }






}
/*
interface Flower {
    id: number;
    dateAdded: Date;
    name: string;
    description: string;
    marketPrice: number;
    flowerUpkeepDifficulty: FlowerUpkeepDifficulty;
}

enum FlowerUpkeepDifficulty {
    Easy = 0,
    Medium = 1,
    Hard = 2
}*/

interface Movie {
    id: number;
    title: string;
    description: string;
    genre: Genre;
    duration: number;
    yearOfRelease: number;
    director: string;
    dateAdded: Date;
    rating: number;
    watched: boolean;

}

enum Genre {
    Action,
    Comedy,
    Horror,
    Thriller
}
