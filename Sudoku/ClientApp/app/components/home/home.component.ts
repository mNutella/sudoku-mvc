import { Component, Inject, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Http } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {

    public emptyGameBoard: IGameBoardData[];
    public filledGameBoard: IGameBoardData[];
    public cellClasses: string[][] = new Array();
    public idGameBoard: number;
    public time = new Date(2000, 1, 1, 1, 0, 0);

    private started = false;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.Init();
    }

    public onClickCell(e: any) {
        e.target.cursorPosVal = e.target.value.length;
    }

    public onChangeCell(e: any) {
        this.cellValidation(e);
    }

    public onClickCheck() {
        this.setToDefault();

        var tempArr = this.compareArr(this.emptyGameBoard, this.filledGameBoard);

        if (tempArr.length == 0) {
            alert('Successfully!');
        }
        else {
            alert('Some cells are missing or there are errors!');
            tempArr.forEach(item => {
                this.cellClasses[item.row][item.col] = 'app-input-fail';
            });
        }
    }

    public onClickNewGame() {
        this.Init();
    }

    public onClickHelp() {
        for (var _i = 0; _i < this.emptyGameBoard.length; _i++) {
            for (var _j = 0; _j < this.emptyGameBoard[_i].Row.length; _j++) {
                if (!this.emptyGameBoard[_i].Row[_j].ReadOnly)
                    if (this.emptyGameBoard[_i].Row[_j].Value != this.filledGameBoard[_i].Row[_j].Value) {
                        this.emptyGameBoard[_i].Row[_j].Value = this.filledGameBoard[_i].Row[_j].Value;
                        this.emptyGameBoard[_i].Row[_j].ReadOnly = true;
                        this.cellClasses[_i][_j] = '';
                        return;
                    }
            }
        }
    }

    // Private

    private Init() {
        let _previusGameId = 0;

        if (this.isBrowser()) {
            let tempStr = localStorage.getItem('previousGame') == null ? "" : localStorage.getItem('previousGame');
            _previusGameId = JSON.parse(tempStr != null ? tempStr == '' ? '1' : tempStr : '1');
        }

        // Getting game board
        this.http.get(this.baseUrl + 'api/v1/GameBoard/' + _previusGameId).subscribe(
            data => {
                let _tempId = data.json().id;
                this.idGameBoard = _tempId;
                this.emptyGameBoard = JSON.parse(data.json().emptyBoard);
                this.filledGameBoard = JSON.parse(data.json().filledBoard);

                // Fill array with classes of components
                this.setDefaultCellClasses();

                if (this.isBrowser()) {
                    localStorage.setItem('previousGame', JSON.stringify(_tempId));
                }
            },
            error => {
                console.log(error);
            }
        );
    }

    private isBrowser(): boolean {
        if (typeof window == 'object' && window != null && window.self == window)
            return true;
        else
            return false;
    }

    private setDefaultCellClasses() {
        for (var _i = 0; _i < 9; _i++) {
            this.cellClasses[_i] = [];
            for (var _j = 0; _j < 9; _j++) {
                if (!this.emptyGameBoard[_i].Row[_j].ReadOnly)
                    this.cellClasses[_i][_j] = 'app-input';
                else
                    this.cellClasses[_i][_j] = '';
            }
        }
    }

    private setToDefault() {
        for (var _i = 0; _i < this.cellClasses.length; _i++) {
            for (var _j = 0; _j < this.cellClasses[_i].length; _j++) {
                if (!this.emptyGameBoard[_i].Row[_j].ReadOnly)
                    this.cellClasses[_i][_j] = 'app-input';
            }
        }
    }

    private compareArr(firstArr: IGameBoardData[], secondArr: IGameBoardData[]): Array<WrongAnswer> {
        // Array for wrong answers
        let _tempArr = new Array<WrongAnswer>();

        for (var _i = 0; _i < firstArr.length; _i++) {
            for (var _j = 0; _j < firstArr[_i].Row.length; _j++) {
                if (firstArr[_i].Row[_j].Value != secondArr[_i].Row[_j].Value)
                    _tempArr.push(new WrongAnswer(_i, _j));
            }
        }

        return _tempArr;
    }

    private cellValidation(e: any) {
        let _value = e.target.value;
        if (_value[_value.length - 1].search(/[1-9]/i) != -1) {
            if (_value.length < 2) {
                e.target.value = parseInt(_value);
            }
            else {
                e.target.value = parseInt(_value[_value.length - 1]);
            }
        } else {
            if (_value >= 2)
                e.target.value = parseInt(_value[0]);
            else
                e.target.value = null;
        }
    }

    // Private End
}

export interface IGameBoardData {
    Row: {
        Value?: number | null,
        ReadOnly: boolean;
    }[]
}

class WrongAnswer {
    row: number;
    col: number;

    constructor(row: number, col: number) {
        this.row = row;
        this.col = col;
    }
}
