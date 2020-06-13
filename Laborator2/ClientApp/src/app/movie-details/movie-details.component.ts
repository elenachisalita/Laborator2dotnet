import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

    private movie: MovieWithDetails;

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL')
        private baseUrl: string,
        private route: ActivatedRoute) {

       
    }

    loadMovie(movieId: string) {
        this.http.get<MovieWithDetails>(this.baseUrl + 'api/Movies/' + movieId).subscribe(result => {
            this.movie = result;
            console.log(this.movie);
        }, error => console.error(error));
    }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.loadMovie(params.get('movieId'));
        });
  }

}

interface Comment {
    
    author: string,
    text: string
}

interface MovieWithDetails {

    title: string;
    description: string;
    genre: string;
    duration: number;
    yearOfRelease: number;
    director: string;
    dateAdded: Date;
    rating: number;
    watched: boolean;
    comments: Comment[];
}
