import { Component } from '@angular/core';
import { faDumbbell, faWeightScale, faUtensils, faHome } from '@fortawesome/free-solid-svg-icons';
import { IconDefinition } from "@fortawesome/free-solid-svg-icons";

interface ILink {
	url: string;
	icon: IconDefinition;
	title: string;
}

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})

export class SideNavComponent {

  faHome = faHome;
  faDumbell = faDumbbell;
  faWeightScale = faWeightScale;
  faUtensils = faUtensils;

  constructor() { }

  public links: Array<ILink> = [

    {
      url:'/home',
      icon: faHome,
      title: 'Home'
    },
    {
      url:'/traininglog',
      icon: faDumbbell,
      title: 'Training log'
    },
    {
      url:'/foodlog',
      icon: faUtensils,
      title: 'Food log'
    },
    {
      url:'/weightlog',
      icon: faWeightScale,
      title: 'Weight log'
    }
  ]

}
