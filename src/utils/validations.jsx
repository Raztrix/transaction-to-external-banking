export const fullNameValidationHeb = (input) => {
    const regex = /^[\u0590-\u05FF\s"-]{1,20}$/;
    return regex.test(input);
}

export const fullNameValidationEng = (input) => {
    const regex = /^[a-zA-Z\s"-]{1,15}$/;
    return regex.test(input);
}

export const validateBirthDate = (input) => {
    const regex = /^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$/;
    return regex.test(input);
};

export const validateSSN = (input) => {
    const regex = /^\d{9}$/;
    return regex.test(input);
}

export const validateAmount = (input) => {
    const regex = /^\d{10}$/;
    return regex.test(input);
}

export const validateAccountNumber = (input) => {
    const regex = /^\d{10}$/;
    return regex.test(input);
} 