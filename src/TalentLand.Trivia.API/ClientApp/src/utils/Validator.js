
// ******************************
export const validator = (values, fieldName) => {
    let errors = {};
    switch (fieldName) {
        case "email":
            validateEmail(values.email, errors);
            break;       
        default:
            validateText(values[fieldName], fieldName, errors);
            break;
    }
    return errors;
};

// ******************************
function validateEmail(email, errors) {
    let result = true;

    if (!email) {
        errors.email = "El email es requerido";
        result = false;
    } else {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        result = re.test(String(email).toLowerCase());
        if (!result) errors.email = "El email es incorrecto";
    }
    return result;
}
// ******************************
function validateText(text, parameterName, errors) {
    let result = true;

    if (!text) {
        errors[parameterName] = `El campo es requerido`;
        result = false;
    } 

    return result;
}
