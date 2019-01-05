_entitiesList.add({
    name: 'area',
    urlCreate: `areas/create`,
    urlUpdate: `areas/edit`,
    render: function () {
        return `<table><thead><tr><th>#</th><th>Descripcion</th></tr></thead><tbody></tbody></table>`;
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