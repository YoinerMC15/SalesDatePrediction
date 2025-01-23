import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  handleError(error: any): void {
    // Filtrar errores específicos que deseas ocultar
    if (error.message && error.message.includes('No provider for _HttpClient')) {
      return; // Ignorar este error específico
    }

    // Manejo de otros errores o registro personalizado
    console.error('Unhandled Error:', error);
  }
}
