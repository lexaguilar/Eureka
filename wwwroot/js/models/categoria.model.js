_entitiesList.add({
    name: 'categoria',
    urlGet: 'categorias/obtenerListar',
    urlCreate: `categorias/create`,
    urlUpdate: `categorias/edit`,
    urlGetPartial: 'categorias/obtenerListarCompletar',
    GetPartialKey: function () {
        return Id.value || null;
    },
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