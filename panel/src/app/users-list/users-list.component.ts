import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RolesModalComponent } from '../modals/roles-modal/roles-modal.component';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users: Partial<User[]> = [];
  bsModalRef: BsModalRef;
  constructor(private accountService: AccountService,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService) { }
  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.accountService.getUsersWithRoles().subscribe(users => {
      this.users = users;
    });
  }
  openRolesModal(user: User) {
    const config = {
      class: "modal-dailog-centered",
      initialState: {
        user,
        roles: this.getUserRoles(user)
      }
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);

    this.bsModalRef.content.updateSelectedRoles.subscribe(values => {
      const rolesToUpdate = {
        roles: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      this.accountService.updateRoles(user.username, rolesToUpdate.roles).subscribe(() => {
        user.roles = [...rolesToUpdate.roles];
      })
    });
  }
  private getUserRoles(user: User) {
    const roles = [];
    let avialableRoles: any[] = [
      { name: 'AdminLevel', value: 'مدير الموقع' },
      { name: 'NormalLevel', value: 'مستخدم الاخبار' },
      { name: 'MobFaLevel', value: 'مستخدم المبادرات' },
      { name: 'AdsLevel', value: 'مستخدم الاعلانات' },
      { name: 'SurveyLevel', value: 'مستخدم استبيان' }
    ];
    avialableRoles.forEach(role => {
      let isMatch = false;
      for (const userRole of user.roles) {
        if (role.name === userRole) {
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }
      }
      if (!isMatch) {
        role.checked = false;
        roles.push(role);
      }
    });
    return roles;
  }
  lockUser(user: User) {
    this.accountService.lockUser(user.username).subscribe(() => {
      const msg = user.locked ? 'تفعيل' : 'تعطيل';
      this.toastr.success('تم ' + msg + ' المستخدم بنجاح');
      user.locked = !user.locked;
    })
  }

}
