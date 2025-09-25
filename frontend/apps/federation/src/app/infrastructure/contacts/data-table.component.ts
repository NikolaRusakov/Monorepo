// data-table.component.ts
import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VCardDto } from '@doto-solutions/angular-contact-service';

interface Telephone {
  id: string;
  value: string;
  valueType: string;
  type: string[];
  preference: number;
  extension: string | null;
}

interface DataItem {
  id: string;
  language: {
    value: string;
    preference: number;
  };
  organization: {
    name: string;
    type: string;
  };
  address: {
    type: string;
    poBox: string;
    extendedAddress: string;
    streetAddress: string;
    locality: string;
    region: string;
    postalCode: string;
    country: string;
  };
  telephones: Telephone[];
  email: {
    type: string;
    address: string;
  };
  geography: {
    type: string;
    latitude: number;
    longitude: number;
  };
  publicKey: {
    type: string;
    valueType: string;
    uri: string;
  };
  timezone: string;
  url: {
    type: string;
    address: string;
  };
}

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [CommonModule],
  styleUrls: ['./data-table.component.scss'],
  template: `
    @let values = data();
    <div class="table-container">
      @if(values!=null && values.length > 0){
      <table class="values-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Language</th>
            <th>Organization</th>
            <th>Address</th>
            <th>Telephones</th>
            <th>Email</th>
            <th>Geography</th>
            <th>Public Key</th>
            <th>Timezone</th>
            <th>URL</th>
          </tr>
        </thead>
        <tbody>
          @for(item of values; track item.id) {
          <tr>
            <td class="id-cell">{{ item.id }}</td>
            <td>
              <div class="nested-data">
                <div><strong>Value:</strong> {{ item.language?.value }}</div>
                <div>
                  <strong>Preference:</strong> {{ item.language?.preference }}
                </div>
              </div>
            </td>
            <td>
              <div class="nested-data">
                <div><strong>Name:</strong> {{ item.organization?.name }}</div>
                <div><strong>Type:</strong> {{ item.organization?.type }}</div>
              </div>
            </td>
            <td>
              <div class="nested-data">
                <div><strong>Type:</strong> {{ item.address?.type }}</div>
                <div><strong>P.O. Box:</strong> {{ item.address?.poBox }}</div>
                <div>
                  <strong>Extended:</strong> {{ item.address?.extendedAddress }}
                </div>
                <div>
                  <strong>Street:</strong> {{ item.address?.streetAddress }}
                </div>
                <div><strong>City:</strong> {{ item.address?.locality }}</div>
                <div><strong>State:</strong> {{ item.address?.region }}</div>
                <div>
                  <strong>Postal:</strong> {{ item.address?.postalCode }}
                </div>
                <div><strong>Country:</strong> {{ item.address?.country }}</div>
              </div>
            </td>
            <td>
              <div class="telephone-list">
                <div
                  *ngFor="let phone of item.telephones"
                  class="telephone-item"
                >
                  <div><strong>Value:</strong> {{ phone.value }}</div>
                  <div><strong>Type:</strong> {{ phone.valueType }}</div>
                  <div>
                    <strong>Category:</strong> {{ phone.type?.join(', ') }}
                  </div>
                  <div><strong>Preference:</strong> {{ phone.preference }}</div>
                  <div *ngIf="phone.extension">
                    <strong>Ext:</strong> {{ phone.extension }}
                  </div>
                </div>
              </div>
            </td>
            <td>
              <div class="nested-data">
                <div><strong>Type:</strong> {{ item.email?.type }}</div>
                <div><strong>Address:</strong> {{ item.email?.address }}</div>
              </div>
            </td>
            <td>
              <div class="nested-data">
                <div><strong>Type:</strong> {{ item.geography?.type }}</div>
                <div><strong>Lat:</strong> {{ item.geography?.latitude }}</div>
                <div><strong>Lng:</strong> {{ item.geography?.longitude }}</div>
              </div>
            </td>
            <td>
              <div class="nested-data">
                <div><strong>Type:</strong> {{ item.publicKey?.type }}</div>
                <div>
                  <strong>Value Type:</strong> {{ item.publicKey?.valueType }}
                </div>
                <div class="uri-cell">
                  <strong>URI:</strong>
                  <a [href]="item.publicKey?.uri" target="_blank">{{
                    item.publicKey?.uri
                  }}</a>
                </div>
              </div>
            </td>
            <td>{{ item.timezone }}</td>
            <td>
              <div class="nested-data">
                <div><strong>Type:</strong> {{ item.url?.type }}</div>
                <div>
                  <strong>URL:</strong>
                  <a [href]="item.url?.address" target="_blank">{{
                    item.url?.address
                  }}</a>
                </div>
              </div>
            </td>
          </tr>
          }
        </tbody>
      </table>

      }@else{
      <div class="no-data">No data available.</div>
      }
    </div>
  `,
  
})
export class DataTableComponent {
  data = input<VCardDto[]>();
  //   data: DataItem[] = [
  //     {
  //       "id": "2e7f0cb9-2262-459f-9d93-918e159c4a1d",
  //       "language": {
  //         "value": "en-US",
  //         "preference": 1
  //       },
  //       "organization": {
  //         "name": "TechVision Solutions Inc.",
  //         "type": "Corporation"
  //       },
  //       "address": {
  //         "type": "work",
  //         "poBox": "P.O. Box 4567",
  //         "extendedAddress": "Suite 340",
  //         "streetAddress": "1247 Innovation Drive",
  //         "locality": "San Francisco",
  //         "region": "California",
  //         "postalCode": "94105",
  //         "country": "United States"
  //       },
  //       "telephones": [
  //         {
  //           "id": "56e9eb56-074d-4dce-b1b8-96eb0b35c8bd",
  //           "value": "+1-555-987-6543",
  //           "valueType": "fax",
  //           "type": [
  //             "work"
  //           ],
  //           "preference": 2,
  //           "extension": null
  //         },
  //         {
  //           "id": "7e825b6b-6262-4364-bcc8-06b5f6da8be1",
  //           "value": "+1-555-123-4567",
  //           "valueType": "voice",
  //           "type": [
  //             "work",
  //             "primary"
  //           ],
  //           "preference": 1,
  //           "extension": "340"
  //         }
  //       ],
  //       "email": {
  //         "type": "work",
  //         "address": "contact@techvisionsolutions.com"
  //       },
  //       "geography": {
  //         "type": "headquarters",
  //         "latitude": 37.7749,
  //         "longitude": -122.4194
  //       },
  //       "publicKey": {
  //         "type": "RSA",
  //         "valueType": "text",
  //         "uri": "https://keyserver.ubuntu.com/pks/lookup?op=get&search=0x123456789ABCDEF0"
  //       },
  //       "timezone": "America/Los_Angeles",
  //       "url": {
  //         "type": "work",
  //         "address": "https://www.techvisionsolutions.com"
  //       }
  //     }
  //   ];
}
