_entitiesList.add({
    name: 'Estado',
    urlGet: 'Estados/obtenerListar',
    urlCreate: `Estados/create`,
    urlUpdate: `Estados/edit`,
    urlGetPartial: 'Estados/obtener-por/{parametro}',
    GetPartialKey: function () {
        return Id.value || null;
    },
    load: function (page, forEntity) {
        $('#Estados').loadData(this, page, forEntity);
    },
    models: [
        {
            id: 'Id',
            key: true,
            requiredSave: () => true,
            messageEmpty: 'El id es requerido(a)',
        },
        {
            id: 'Descripcion',
            requiredSave: () => true,
            messageEmpty: 'La descripcion es requerida',
        },

    ]
});