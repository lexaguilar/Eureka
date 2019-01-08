_entitiesList.add({
    name: 'area',
    urlGet: 'areas/obtenerListar',
    urlCreate: `areas/create`,
    urlUpdate: `areas/edit`,
    urlGetPartial: 'areas/obtener/{parametro}',
    GetPartialKey: function () {
        return Id.value || null;
    },
    load: function (page) {
        $('#areas').loadData(this, page);
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
            messageEmpty: 'El campo es requerido',
            type : stores.types.editable        
        },
        {
            id: 'EstadoId',
            type : stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El campo es requerido'           
        }
    ]
});