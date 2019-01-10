_entitiesList.add({
    name: 'familia',
    urlGet: 'familias/obtenerListar',
    urlCreate: `familias/create`,
    urlUpdate: `familias/edit`,
    urlGetPartial: 'familias/obtener-por/{parametro}',
    GetPartialKey: function () {
        return Id.value || null;
    },
    load: function (page) {
        $('#familias').loadData(this, page);
    },
    models: [
        {
            id: 'Id',
            requiredSave: () => true,
            messageEmpty: 'El Id es requerido',
            key: true
        },
        {
            id: 'Prefijo',
            requiredSave: () => true,
            messageEmpty: 'El prefijo es requerido a dos digitos',
            isValid: function () {
                var value = this.getValue();
                if (!validate.validateLength(value).iquals(2)) {
                    this.messageEmpty = 'El prefijo es requerido con dos digitos';
                    return false;
                }               
                if (!validate.isNumber(value)) {
                    this.messageEmpty = 'El prefijo debe ser númerico';
                    return false;
                }
                return true;
                
            },
            type: stores.types.editable
        }, {
            id: 'Descripcion',
            requiredSave: () => true,
            messageEmpty: 'La descripcion es requerido',
            type: stores.types.editable
        },
        {
            id: 'EstadoId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El estado es requerido'
        },
    ]
});