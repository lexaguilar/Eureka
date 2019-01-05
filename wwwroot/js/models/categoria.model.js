_entitiesList.add({
    name: 'categoria',
    urlGet: 'categorias/obtenerListar',
    urlGetPartial: 'categorias/obtenerListarCompletar',
    urlCreate: `categorias/create`,
    urlUpdate: `categorias/edit`,
    load: function (page) {
        $('#categorias').loadData(this, page);
    },
    models: [
        {
            id: 'Id',
            requiredSave: () => true,
            messageEmpty: 'El Id es requerido',
            key : true
        },
        {
            id: 'Descripcion',
            requiredSave: () => true,
            type: stores.types.editable,
            messageEmpty: 'El Descripcion es requerido',
        },
        {
            id: 'Nota',
            requiredSave: () => true,
            type: stores.types.editable,
            messageEmpty: 'El Nota es requerido',
        },
    ]
});