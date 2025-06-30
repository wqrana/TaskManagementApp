import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
    {
      provide: 'API_BASE_URL',
      useFactory: () => {
        // Use /api for production, proxy to localhost:5001 in dev
        return window.location.hostname === 'localhost' 
          ? 'http://localhost:5001/api' 
          : '/api';
      }
    }
  ]
};
