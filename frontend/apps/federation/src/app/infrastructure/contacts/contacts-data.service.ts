import { inject, Injectable } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  ContactsService,
  CreateVCardRequest,
} from '@doto-solutions/angular-contact-service';

@Injectable({ providedIn: 'root' })
export class ContactsDataService {
  protected contactsApi = inject(ContactsService);
  getContacts = () => toSignal(this.contactsApi.getAllContacts());
  createContact = (request: CreateVCardRequest) =>
    toSignal(this.contactsApi.createContact(request));
}
