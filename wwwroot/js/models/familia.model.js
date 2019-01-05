_entitiesList.add({
    name: 'familia',
    urlCreate: `familias/create`,
    urlUpdate: `familias/edit`,
    urlGet: 'familias/obtenerListar',
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