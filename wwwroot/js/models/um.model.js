_entitiesList.add({
    name: 'um',
    urlGet: 'umes/obtenerListar',
    urlCreate: `umes/create`,
    urlUpdate: `umes/edit`,
    urlGetPartial: 'umes/obtenerListarCompletar',
    GetPartialKey: function () {
        return Id.value || null;
    },
    load: function (page) {
        $('#umes').loadData(this, page);
    },
    models: [
        {
            id: 'Id',
            requiredSave: () => true,
            messageEmpty: 'El Id es requerido',
            key: true
        },
        {
            id: 'Descripcion',
            type: stores.types.editable,
            requiredSave: () => true,
            messageEmpty: 'El Descripcion es requerido',
        },
        {
            id: 'EstadoId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El EstadoId es requerido',
        },
    ]
});