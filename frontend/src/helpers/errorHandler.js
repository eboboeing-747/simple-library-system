export class ErrorHandler {
    #errorDisplay;

    constructor (errorDisplay) {
        this.errorDisplay = errorDisplay;
        return this;
    }

    displayError(errorMessage, elements) {
        elements.forEach(element => {
            element.classList.add('error');
        });

        this.errorDisplay.textContent = errorMessage;
        this.errorDisplay.classList.add('error');
    }

    hideErrors() {
        this.errorDisplay.textContent = '';
        let errors = document.querySelectorAll('.error');

        for (let i = 0; i < errors.length; i++)
            errors[i].classList.remove('error');
    }
}