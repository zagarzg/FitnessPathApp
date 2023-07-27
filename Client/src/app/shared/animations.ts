import {
  animate,
  transition,
  keyframes,
  style,
  trigger,
} from '@angular/animations';

export const addAnimation = trigger('addTile', [
  transition('void => *', [
    style({ opacity: 1 }),
    animate(
      '500ms',
      keyframes([
        style({ transform: 'translateX(-500px)', opacity: 1, offset: 0 }),
        style({
          transform: 'translateX(0px)',
          opacity: 1,
          offset: 0.8,
        }),
        style({
          transform: 'translateX(30px)',
          opacity: 1,
          offset: 0.9,
        }),
        style({ transform: 'translateX(0px)', opacity: 1, offset: 1 }),
      ])
    ),
  ]),
]);

export const deleteAnimation = trigger('deleteTile', [
  transition('* => void', [
    style({ opacity: 1 }),
    animate(
      '700ms',
      keyframes([
        style({ opacity: 1, offset: 0, filter: 'blur(2px)' }),
        style({
          transform: 'translateX(30px)',
          filter: 'blur(2px)',
          opacity: 0.5,
          offset: 0.5,
        }),
        style({
          transform: 'translateX(-1000px)',
          filter: 'blur(2px)',
          opacity: 0,
          offset: 1,
        }),
      ])
    ),
  ]),
]);

export const deleteAnimation2 = transition('* => void', [
  animate(
    '1200ms',
    keyframes([
      style({ clipPath: 'inset(0px 0px 0px 0px)', offset: 0 }),
      style({ clipPath: 'inset(30px 0px 30px 0px)', offset: 0.5 }),
      style({ clipPath: 'inset(60px 0px 60px 0px)', offset: 1 }),
    ])
  ),
]);
