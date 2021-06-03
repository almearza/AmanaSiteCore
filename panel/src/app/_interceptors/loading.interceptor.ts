import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, timer } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { delay, finalize, switchMap } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.busyService.busy();
    
    return timer(1000).pipe(         // <== Wait 1 Seconds
      switchMap(() => next.handle(request).pipe(
        //for developing stage
        finalize(() => {
          this.busyService.idle();
        })
      ))   // <== Switch to the Http Stream
    )
    // return next.handle(request).pipe(
    //   //for developing stage
    //   finalize(()=>{
    //     this.busyService.idle();
    //   })
    // )
  }
}
