import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { Sample3User } from './CodeSample3.interface';
import { Sample3UserService } from './CodeSample3.service';

@Component({
    selector: 'sample-3',
    templateUrl: './CodeSample3.component.html',
    styleUrls: ['./CodeSample3.component.css']
})
export class Sample3Component implements OnInit, OnDestroy {
    users: Sample3User[];
    displayedColumns: string[] = ['Name', 'Email', 'PhoneNumber', 'IsDisabled', 'Actions'];

    constructor(
        private userService: Sample3UserService,
        private snackBar: MatSnackBar,
        private http: HttpClient
    ) { }

    ngOnInit(): void {

    }

    private loadUsers(): void {
        this.userService.getUsers().subscribe(users => this.users = users);
    }

    disableUser(user: Sample3User): void {
        this.userService.disableUser(user.ID).subscribe(
            () => {
                this.snackBar.open(`User ${user.Name} has been disabled`, 'Close', {
                    duration: 2000,
                });
                this.loadUsers();
            },
            error => {
                this.snackBar.open(`Error disabling user ${user.Name}: ${error.message}`, 'Close');
            }
        );
    }

    enableUser(user: Sample3User): void {
        this.callEnableUser(user.ID).subscribe(() => this.loadUsers());
    }

    private callEnableUser(userId: number): Observable<Sample3User> {
        return this.http.post<Sample3User>(`${this.userService.apiUrl}/${userId}/enable`, null);
    }
}
