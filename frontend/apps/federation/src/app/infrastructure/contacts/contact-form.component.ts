import { CommonModule, JsonPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormArray,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ContactsDataService } from './contacts-data.service';
import { faker } from '@faker-js/faker';

@Component({
  selector: 'app-contact-form',
  imports: [JsonPipe, ReactiveFormsModule, CommonModule],
  standalone: true,
  template: `
    <div class="organization-form-container">
      <h2>Organization Information Form</h2>

      <form
        [formGroup]="organizationForm"
        (ngSubmit)="onSubmit()"
        class="max-h-[75vh] overflow-y-auto"
      >
        <!-- Basic Information -->
        <div class="form-section">
          <h3>Basic Information</h3>

          <div class="form-group">
            <label for="id">ID:</label>
            <input
              type="text"
              id="id"
              formControlName="id"
              class="form-control"
            />
          </div>

          <div class="form-row">
            <div class="form-group">
              <label for="languageValue">Language:</label>
              <select
                id="languageValue"
                formControlName="languageValue"
                class="form-control"
              >
                <option value="en-US">English (US)</option>
                <option value="en-GB">English (GB)</option>
                <option value="es-ES">Spanish</option>
                <option value="fr-FR">French</option>
                <option value="de-DE">German</option>
              </select>
            </div>
            <div class="form-group">
              <label for="languagePreference">Language Preference:</label>
              <input
                type="number"
                id="languagePreference"
                formControlName="languagePreference"
                class="form-control"
                min="1"
              />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group">
              <label for="organizationName">Organization Name *:</label>
              <input
                type="text"
                id="organizationName"
                formControlName="organizationName"
                class="form-control"
                [class.error]="
                  organizationForm.get('organizationName')?.invalid &&
                  organizationForm.get('organizationName')?.touched
                "
              />
              <div
                class="error-message"
                *ngIf="
                  organizationForm.get('organizationName')?.invalid &&
                  organizationForm.get('organizationName')?.touched
                "
              >
                Organization name is required
              </div>
            </div>
            <div class="form-group">
              <label for="organizationType">Organization Type:</label>
              <select
                id="organizationType"
                formControlName="organizationType"
                class="form-control"
              >
                <option value="Corporation">Corporation</option>
                <option value="LLC">LLC</option>
                <option value="Partnership">Partnership</option>
                <option value="Non-Profit">Non-Profit</option>
                <option value="Government">Government</option>
              </select>
            </div>
          </div>
        </div>

        <!-- Address Information -->
        <div class="form-section" formGroupName="address">
          <h3>Address Information</h3>

          <div class="form-row">
            <div class="form-group">
              <label for="addressType">Address Type:</label>
              <select
                id="addressType"
                formControlName="type"
                class="form-control"
              >
                <option value="work">Work</option>
                <option value="home">Home</option>
                <option value="other">Other</option>
              </select>
            </div>
            <div class="form-group">
              <label for="poBox">P.O. Box:</label>
              <input
                type="text"
                id="poBox"
                formControlName="poBox"
                class="form-control"
              />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group">
              <label for="streetAddress">Street Address:</label>
              <input
                type="text"
                id="streetAddress"
                formControlName="streetAddress"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <label for="extendedAddress">Suite/Unit:</label>
              <input
                type="text"
                id="extendedAddress"
                formControlName="extendedAddress"
                class="form-control"
              />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group">
              <label for="locality">City:</label>
              <input
                type="text"
                id="locality"
                formControlName="locality"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <label for="region">State/Region:</label>
              <input
                type="text"
                id="region"
                formControlName="region"
                class="form-control"
              />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group">
              <label for="postalCode">Postal Code:</label>
              <input
                type="text"
                id="postalCode"
                formControlName="postalCode"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <label for="country">Country:</label>
              <input
                type="text"
                id="country"
                formControlName="country"
                class="form-control"
              />
            </div>
          </div>
        </div>

        <!-- Telephone Numbers -->
        <div class="form-section">
          <h3>Telephone Numbers</h3>
          <div formArrayName="telephones">
            <div
              *ngFor="let telephone of getTelephones().controls; let i = index"
              [formGroupName]="i"
              class="telephone-group"
            >
              <div class="form-row">
                <div class="form-group">
                  <label>Phone Number:</label>
                  <input
                    type="tel"
                    formControlName="value"
                    class="form-control"
                    placeholder="+1-555-123-4567"
                  />
                </div>
                <div class="form-group">
                  <label>Type:</label>
                  <select formControlName="valueType" class="form-control">
                    <option value="voice">Voice</option>
                    <option value="fax">Fax</option>
                    <option value="mobile">Mobile</option>
                  </select>
                </div>
              </div>
              <div class="form-row">
                <div class="form-group">
                  <label>Category:</label>
                  <select
                    formControlName="typeArray"
                    class="form-control"
                    multiple
                  >
                    <option value="work">Work</option>
                    <option value="home">Home</option>
                    <option value="primary">Primary</option>
                    <option value="secondary">Secondary</option>
                  </select>
                </div>
                <div class="form-group">
                  <label>Extension:</label>
                  <input
                    type="text"
                    formControlName="extension"
                    class="form-control"
                  />
                </div>
                <div class="form-group">
                  <label>Preference:</label>
                  <input
                    type="number"
                    formControlName="preference"
                    class="form-control"
                    min="1"
                  />
                </div>
              </div>
              <button
                type="button"
                class="btn btn-danger btn-sm"
                (click)="removeTelephone(i)"
              >
                Remove Phone
              </button>
            </div>
          </div>
          <button
            type="button"
            class="btn btn-secondary"
            (click)="addTelephone()"
          >
            Add Phone Number
          </button>
        </div>

        <!-- Email -->
        <div class="form-section" formGroupName="email">
          <h3>Email Information</h3>
          <div class="form-row">
            <div class="form-group">
              <label for="emailType">Email Type:</label>
              <select
                id="emailType"
                formControlName="type"
                class="form-control"
              >
                <option value="work">Work</option>
                <option value="home">Home</option>
                <option value="other">Other</option>
              </select>
            </div>
            <div class="form-group">
              <label for="emailAddress">Email Address *:</label>
              <input
                type="email"
                id="emailAddress"
                formControlName="address"
                class="form-control"
                [class.error]="
                  organizationForm.get('email.address')?.invalid &&
                  organizationForm.get('email.address')?.touched
                "
              />
              <div
                class="error-message"
                *ngIf="
                  organizationForm.get('email.address')?.invalid &&
                  organizationForm.get('email.address')?.touched
                "
              >
                Valid email address is required
              </div>
            </div>
          </div>
        </div>

        <!-- Geography -->
        <div class="form-section" formGroupName="geography">
          <h3>Geographic Information</h3>
          <div class="form-row">
            <div class="form-group">
              <label for="geographyType">Type:</label>
              <select
                id="geographyType"
                formControlName="type"
                class="form-control"
              >
                <option value="headquarters">Headquarters</option>
                <option value="branch">Branch</option>
                <option value="warehouse">Warehouse</option>
                <option value="office">Office</option>
              </select>
            </div>
            <div class="form-group">
              <label for="latitude">Latitude:</label>
              <input
                type="number"
                id="latitude"
                formControlName="latitude"
                class="form-control"
                step="any"
              />
            </div>
            <div class="form-group">
              <label for="longitude">Longitude:</label>
              <input
                type="number"
                id="longitude"
                formControlName="longitude"
                class="form-control"
                step="any"
              />
            </div>
          </div>
        </div>

        <!-- Public Key -->
        <div class="form-section" formGroupName="publicKey">
          <h3>Public Key Information</h3>
          <div class="form-row">
            <div class="form-group">
              <label for="publicKeyType">Key Type:</label>
              <select
                id="publicKeyType"
                formControlName="type"
                class="form-control"
              >
                <option value="RSA">RSA</option>
                <option value="DSA">DSA</option>
                <option value="ECDSA">ECDSA</option>
              </select>
            </div>
            <div class="form-group">
              <label for="publicKeyValueType">Value Type:</label>
              <select
                id="publicKeyValueType"
                formControlName="valueType"
                class="form-control"
              >
                <option value="text">Text</option>
                <option value="binary">Binary</option>
              </select>
            </div>
          </div>
          <div class="form-group">
            <label for="publicKeyUri">Key URI:</label>
            <input
              type="url"
              id="publicKeyUri"
              formControlName="uri"
              class="form-control"
            />
          </div>
        </div>

        <!-- Timezone and URL -->
        <div class="form-section">
          <h3>Additional Information</h3>
          <div class="form-row">
            <div class="form-group">
              <label for="timezone">Timezone:</label>
              <select
                id="timezone"
                formControlName="timezone"
                class="form-control"
              >
                <option value="America/Los_Angeles">America/Los_Angeles</option>
                <option value="America/New_York">America/New_York</option>
                <option value="America/Chicago">America/Chicago</option>
                <option value="America/Denver">America/Denver</option>
                <option value="Europe/London">Europe/London</option>
                <option value="Europe/Paris">Europe/Paris</option>
                <option value="Asia/Tokyo">Asia/Tokyo</option>
              </select>
            </div>
          </div>

          <div class="form-group" formGroupName="url">
            <div class="form-row">
              <div class="form-group">
                <label for="urlType">URL Type:</label>
                <select
                  id="urlType"
                  formControlName="type"
                  class="form-control"
                >
                  <option value="work">Work</option>
                  <option value="personal">Personal</option>
                  <option value="social">Social</option>
                </select>
              </div>
              <div class="form-group">
                <label for="urlAddress">Website URL:</label>
                <input
                  type="url"
                  id="urlAddress"
                  formControlName="address"
                  class="form-control"
                  placeholder="https://www.example.com"
                />
              </div>
            </div>
          </div>
        </div>

        <!-- Form Actions -->
        <div class="form-actions sticky bottom-0 bg-white p-4 border-t">
          <button
            type="submit"
            class="btn btn-primary"
            [disabled]="!organizationForm.valid"
          >
            Save Organization
          </button>
          <button type="button" class="btn btn-secondary" (click)="resetForm()">
            Reset Form
          </button>
          <button type="button" class="btn btn-info" (click)="loadSampleData()">
            Load Sample Data
          </button>
          <button
            type="button"
            class="btn btn-warning"
            (click)="regenerateFakeData()"
          >
            Regenerate Fake Data
          </button>
        </div>

        <!-- Debug Information -->
        <div class="debug-section" *ngIf="showDebug">
          <h4>Form Status</h4>
          <p>Form Valid: {{ organizationForm.valid }}</p>
          <p>Form Value:</p>
          <pre>{{ getFormValue() | json }}</pre>
        </div>
      </form>

      <button
        type="button"
        class="btn btn-link"
        (click)="showDebug = !showDebug"
      >
        {{ showDebug ? 'Hide' : 'Show' }} Debug Info
      </button>
    </div>
  `,
  styleUrls: ['./contact-form.component.scss'],
})
export class OrganizationFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  organizationForm: FormGroup = this.fb.group({});
  showDebug = false;
  contactsDataService = inject(ContactsDataService);
  ngOnInit() {
    this.initializeForm();
    this.loadSampleData();
  }

  initializeForm() {
    this.organizationForm = this.fb.group({
      id: [this.generateId()],
      languageValue: ['en-US'],
      languagePreference: [1, [Validators.min(1)]],
      organizationName: ['', Validators.required],
      organizationType: ['Corporation'],
      address: this.fb.group({
        type: ['work'],
        poBox: [''],
        extendedAddress: [''],
        streetAddress: [''],
        locality: [''],
        region: [''],
        postalCode: [''],
        country: [''],
      }),
      telephones: this.fb.array([]),
      email: this.fb.group({
        type: ['work'],
        address: ['', [Validators.required, Validators.email]],
      }),
      geography: this.fb.group({
        type: ['headquarters'],
        latitude: [null],
        longitude: [null],
      }),
      publicKey: this.fb.group({
        type: ['RSA'],
        valueType: ['text'],
        uri: [''],
      }),
      timezone: ['America/Los_Angeles'],
      url: this.fb.group({
        type: ['work'],
        address: [''],
      }),
    });
  }

  getTelephones(): FormArray {
    return this.organizationForm.get('telephones') as FormArray;
  }

  addTelephone() {
    const telephoneGroup = this.fb.group({
      value: [''],
      valueType: ['voice'],
      typeArray: [['work']],
      preference: [1],
      extension: [''],
    });
    this.getTelephones().push(telephoneGroup);
  }

  removeTelephone(index: number) {
    this.getTelephones().removeAt(index);
  }

  generateId(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(
      /[xy]/g,
      function (c) {
        const r = (Math.random() * 16) | 0;
        const v = c == 'x' ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      }
    );
  }

  loadSampleData() {
    // Add sample telephone numbers
    while (this.getTelephones().length > 0) {
      this.getTelephones().removeAt(0);
    }

    this.addTelephone();
    this.addTelephone();

    this.organizationForm.patchValue({
      id: this.generateId(),
      languageValue: 'en-US',
      languagePreference: 1,
      organizationName: 'TechVision Solutions Inc.',
      organizationType: 'Corporation',
      address: {
        type: 'work',
        poBox: 'P.O. Box 4567',
        extendedAddress: 'Suite 340',
        streetAddress: '1247 Innovation Drive',
        locality: 'San Francisco',
        region: 'California',
        postalCode: '94105',
        country: 'United States',
      },
      email: {
        type: 'work',
        address: 'contact@techvisionsolutions.com',
      },
      geography: {
        type: 'headquarters',
        latitude: 37.7749,
        longitude: -122.4194,
      },
      publicKey: {
        type: 'RSA',
        valueType: 'text',
        uri: 'https://keyserver.ubuntu.com/pks/lookup?op=get&search=0x123456789ABCDEF0',
      },
      timezone: 'America/Los_Angeles',
      url: {
        type: 'work',
        address: 'https://www.techvisionsolutions.com',
      },
    });

    // Set telephone values
    const telephones = this.getTelephones();
    telephones.at(0)?.patchValue({
      value: '+1-555-123-4567',
      valueType: 'voice',
      typeArray: ['work', 'primary'],
      preference: 1,
      extension: '340',
    });

    telephones.at(1)?.patchValue({
      value: '+1-555-987-6543',
      valueType: 'fax',
      typeArray: ['work'],
      preference: 2,
      extension: '',
    });
  }

  regenerateFakeData() {
    this.organizationForm.patchValue({
      id: faker.string.uuid(),
      languageValue:
        faker.location.countryCode('alpha-2') +
        '-' +
        faker.location.countryCode('alpha-2'),
      languagePreference: faker.number.int({ min: 1, max: 5 }),
      organizationName: faker.company.name(),
      organizationType: faker.helpers.arrayElement([
        'Corporation',
        'LLC',
        'Partnership',
        'Non-profit',
        'Sole Proprietorship',
      ]),
      address: {
        type: 'work',
        poBox: 'P.O. Box ' + faker.number.int({ min: 1000, max: 9999 }),
        extendedAddress:
          faker.helpers.arrayElement(['Suite', 'Apt', 'Unit', 'Floor']) +
          ' ' +
          faker.number.int({ min: 100, max: 999 }),
        streetAddress: faker.location.streetAddress(),
        locality: faker.location.city(),
        region: faker.location.state(),
        postalCode: faker.location.zipCode(),
        country: faker.location.country(),
      },
      email: {
        type: 'work',
        address: faker.internet.email(),
      },
      geography: {
        type: 'headquarters',
        latitude: faker.location.latitude(),
        longitude: faker.location.longitude(),
      },
      publicKey: {
        type: faker.helpers.arrayElement(['RSA', 'DSA', 'ECDSA', 'Ed25519']),
        valueType: 'text',
        uri:
          faker.internet.url() +
          '/pks/lookup?op=get&search=0x' +
          faker.string
            .hexadecimal({ length: 16, casing: 'upper' })
            .substring(2),
      },
      timezone: faker.date.timeZone(),
      url: {
        type: 'work',
        address: faker.internet.url(),
      },
    });
  }
  getFormValue() {
    const formValue = this.organizationForm.value;

    // Transform the form value to match the original JSON structure
    return {
      id: formValue.id,
      language: {
        value: formValue.languageValue,
        preference: formValue.languagePreference,
      },
      organization: {
        name: formValue.organizationName,
        type: formValue.organizationType,
      },
      address: formValue.address,
      telephones: formValue.telephones.map((tel: any) => ({
        value: tel.value,
        valueType: tel.valueType,
        type: tel.typeArray,
        preference: tel.preference,
        extension: tel.extension || null,
      })),
      email: formValue.email,
      geography: formValue.geography,
      publicKey: formValue.publicKey,
      timezone: formValue.timezone,
      url: formValue.url,
    };
  }
  private reloadData = () => this.contactsDataService.getContacts();
  onSubmit() {
    if (this.organizationForm.valid) {
      const organizationData = this.getFormValue();
      console.log('Form Submitted:', organizationData);
      this.contactsDataService.createContact(organizationData);
      this.reloadData();
      // Here you would typically send the data to a service
      alert('Organization data saved successfully!');
    } else {
      console.log('Form is invalid');
      this.markFormGroupTouched(this.organizationForm);
    }
  }

  resetForm() {
    this.organizationForm.reset();
    this.initializeForm();
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      } else if (control instanceof FormArray) {
        control.controls.forEach((arrayControl) => {
          if (arrayControl instanceof FormGroup) {
            this.markFormGroupTouched(arrayControl);
          } else {
            arrayControl.markAsTouched();
          }
        });
      } else {
        control?.markAsTouched();
      }
    });
  }
}
