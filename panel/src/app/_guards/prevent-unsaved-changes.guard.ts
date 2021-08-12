import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { HandleAdsComponent } from '../ads/handle-ads/handle-ads.component';
import { HandleServiceComponent } from '../AmanaServices/handle-service/handle-service.component';
import { HandleBaladyaComponent } from '../baladyat/handle-baladya/handle-baladya.component';
import { HandleMobComponent } from '../mobs/handle-mob/handle-mob.component';
import { NewUserComponent } from '../users/new-user/new-user.component';
import { HandleNewsComponent } from '../news/handle-news/handle-news.component';
import { HandleProjectComponent } from '../project/handle-project/handle-project.component';
import { UserEditComponent } from '../users/user-edit/user-edit.component';
import { HandleVideoComponent } from '../video/handle-video/handle-video.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  constructor(private confirmService: ConfirmService) {
  }
  canDeactivate(component: UserEditComponent | NewUserComponent | HandleNewsComponent | HandleAdsComponent
    | HandleServiceComponent | HandleVideoComponent | HandleBaladyaComponent | HandleProjectComponent): Observable<boolean> | boolean {
    if (component instanceof UserEditComponent) {
      if (component.editForm.dirty)
        return this.confirmService.confirm("تعديل مستخدم", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof NewUserComponent) {
      if (component.registerForm.dirty)
        return this.confirmService.confirm("إضافة مستخدم", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof HandleNewsComponent) {
      if (component.newsForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل خبر", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof HandleAdsComponent) {
      if (component.adsForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل اعلان", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof HandleServiceComponent) {
      if (component.serviceForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل خدمة", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof HandleVideoComponent) {
      if (component.videoForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل فيديو", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else if (component instanceof HandleBaladyaComponent){
      if (component.baladyaForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل بلدية", "هل تريد الخروج قبل حفظ هذه البيانات");
    } else {
      if (component.projectForm.dirty)
        return this.confirmService.confirm("إضافة/تعديل مشروع", "هل تريد الخروج قبل حفظ هذه البيانات");
    }
    // return confirm('if you move away any changes will be lost ?');
    return true;
  }

}
