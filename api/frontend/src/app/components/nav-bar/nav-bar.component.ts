import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'nav-bar',
  standalone: true,
  imports: [MatMenuModule, CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  showMenu = false; // Initially hidden
  categories: string[] = ['Category 1', 'Category 2', 'Category 3']; // Placeholder categories

  toggleMenu(): void {
    this.showMenu = !this.showMenu;
    console.log(this.showMenu);
  }

  clickAddImage(): void {
    // Implement your logic for the "Add Image" click action
    console.log('Add Image clicked!');
  }
}
