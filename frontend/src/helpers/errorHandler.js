export class ErrorHandler {
    #errorDisplay;
    #haveErrors;

    constructor (errorDisplay) {
        this.errorDisplay = errorDisplay;
        this.haveErrors = false;
        return this;
    }

    displayError(errorMessage, elements) {
        elements.forEach(element => {
            element.classList.add('error');
        });

        this.haveErrors = true;
        this.errorDisplay.style.color = 'red';
        this.errorDisplay.textContent = errorMessage;
        this.errorDisplay.classList.add('error');
    }

    displaySuccess() {
        this.errorDisplay.style.color = 'green';
        this.errorDisplay.textContent = 'success';
        this.errorDisplay.classList.add('success');
        console.log('success');
    }

    hideErrors() {
        if (this.haveErrors === false)
            return;

        this.errorDisplay.textContent = '';
        let errors = document.querySelectorAll('.error');

        for (let i = 0; i < errors.length; i++)
            errors[i].classList.remove('error');
    }
}