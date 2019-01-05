_entitiesList.add({
    name: 'presentacion',
    urlGet: 'presentaciones/obtenerListar',
    urlGetPartial: 'presentaciones/obtenerListarCompletar',
    urlCreate: `presentaciones/create`,
    urlUpdate: `presentaciones/edit`,
    load: function (page) {
        $('#presentaciones').loadData(this, page);
    },
    models: [
        {
            id: 'Id',
            type: stores.types.editable
            requiredSave: () => true,
            messageEmpty: 'El Id es requerido',
            key: true,
        },
        {
            id: 'Descripcion',
            type: stores.types.editable
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