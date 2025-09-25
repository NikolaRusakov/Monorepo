import { AsyncPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ContactsDataService } from './contacts-data.service';

@Component({
  selector: 'app-table',
  template: `
    <div>
      @let contacts = contactsData(); @if (contacts && contacts.length > 0){
      <table>
        <thead>
          <tr></tr>
        </thead>
        <tbody>
          @for(contact of contacts; track contact.id ){
          <div>
            @for(kv of Object.entries(contact); track kv[1]){
            <th></th>
            }
          </div>
          }
        </tbody>
      </table>

      }@else{
      <div>No contacts</div>
      }
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink, AsyncPipe],
})
export default class TableComponent {
  //   readonly RouteLink = RouteLink;
  readonly Object = Object;
  private readonly contactsDataService = inject(ContactsDataService);

  readonly contactsData = this.contactsDataService.getContacts();
}
