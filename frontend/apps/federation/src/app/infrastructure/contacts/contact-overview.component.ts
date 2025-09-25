import { AsyncPipe, JsonPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ContactsDataService } from './contacts-data.service';
import { OrganizationFormComponent } from './contact-form.component';
import { DataTableComponent } from './data-table.component';

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
      <div class="p-4 flex gap-4">
        <pre class="h-[50vh] scroll-overflow container-lg">
 {{ contacts | json }}</pre
        >
        <app-contact-form />
      </div>
        <app-data-table [data]="contactsData() ?? []" />
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    RouterLink,
    AsyncPipe,
    JsonPipe,
    OrganizationFormComponent,
    DataTableComponent,
  ],
  standalone: true,
})
export default class ContactOverviewComponent {
  //   readonly RouteLink = RouteLink;
  readonly Object = Object;
  private readonly contactsDataService = inject(ContactsDataService);

  readonly contactsData = this.contactsDataService.getContacts();
}
