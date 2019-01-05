var validate = {

    validateField: value => (typeof (value) !== "undefined" && value != null && value != ''),
    validateFieldZero : value => (typeof (value) !== "undefined" && value != null && value != '' && value != '0'),
    validateChange: change => (typeof (change) !== "undefined" && change.delegateTarget.value != null && change.delegateTarget.value != '' && change.delegateTarget.value != '0'),
    isNumber: n => !isNaN(parseFloat(n)) && isFinite(n),
    isEmail: text => {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(text).toLowerCase());
    },
    validateLength: value => {
        var f = {
            iquals: _length => value.length == _length,
            between: (min, max) => value.length >= min && value.length <= max
        }
        return f;
    },
    hasAnyValue: value => value.length > 0,
    propertyIsValid: model => {
        var propertyIsOk = true;

        var value = model.getValue();

        if (model.inputMinLength)
        {
            var hasMask = $('#' + model.id).data('mask');
            var length = 0;

            if (hasMask)
                length = $('#' + model.id).cleanVal().length;
            else
                length = String(value).length;

            if (model.inputMinLength > length) {
                
                if(model.type == stores.types.phone)
                    model.showMessage(`Ingrese ${model.inputMinLength} dígitos`);
                else
                    model.showMessage(`Ingrese ${model.inputMinLength} dígitos mínimo`);

                propertyIsOk = false;

            }
        }

        if (model.type == stores.types.email)
            if (!validate.isEmail(value)) {
                model.showMessage('Solo se permite formato de correo');
                propertyIsOk = false;
            }

        return propertyIsOk;

    },

    model :model => {

        var ok = true;
        var value = model.getValue();
        if (validate.hasAnyValue($.trim(value))) {
            if (!validate.propertyIsValid(model)) {
                ok = false;
            } else {
                model.clearError();
            }
        } else model.clearError();

        return ok;

    }

}