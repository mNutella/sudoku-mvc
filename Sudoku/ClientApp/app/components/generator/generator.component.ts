import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { IGameBoardData } from '../home/home.component';

@Component({
    selector: 'generator',
    templateUrl: './generator.component.html',
    styleUrls: ['../home/home.component.css']
})
export class GeneratorComponent {
    public idGameBoard: number;
    public filledGameBoard: IGameBoardData[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        
    }

    public onClickGenerate() {
        this.http.post(this.baseUrl + 'api/v1/GameBoard/CreateBoard', null).subscribe(
            data => {
                let _tempId = data.json().id;
                this.idGameBoard = _tempId;
                this.filledGameBoard = JSON.parse(data.json().filledBoard);
            },
            error => {
                console.log(error);
            }
        )
    }
}