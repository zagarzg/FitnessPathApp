import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  faCog,
  faDumbbell,
  faUtensils,
  faWeightScale,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';

interface ILink {
  url: string;
  icon: IconDefinition;
  title: string;
}

@Component({
  selector: 'app-curved-outside-bar',
  templateUrl: './curved-outside-bar.component.html',
  styleUrls: ['./curved-outside-bar.component.scss'],
})
export class CurvedOutsideBarComponent implements OnInit {
  @ViewChild('navigation') navigationBarRef!: ElementRef;
  @ViewChild('menuToggle') menuToggleElementRef!: ElementRef;

  faDumbell = faDumbbell;
  faWeightScale = faWeightScale;
  faUtensils = faUtensils;
  faCog = faCog;

  constructor() {}

  public links: ILink[] = [
    {
      url: '/training-log',
      icon: faDumbbell,
      title: 'Training log',
    },
    {
      url: '/food-log',
      icon: faUtensils,
      title: 'Food log',
    },
    {
      url: '/weight-log',
      icon: faWeightScale,
      title: 'Weight log',
    },
    {
      url: '/settings',
      icon: faCog,
      title: 'Settings',
    },
  ];

  ngOnInit(): void {
    // let list = document.querySelectorAll('.list');
    // function activeLink(activeItem: any) {
    //   list.forEach((item: any) => item.classList.remove('active'));
    //   activeItem.classList.add('active');
    // }
    // list.forEach((item: any) =>
    //   item.addEventListener('click', function () {
    //     activeLink(item);
    //   })
    // );
  }

  onMenuToggle() {
    this.navigationBarRef.nativeElement.classList.toggle('active');
  }
}
