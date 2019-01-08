_entitiesList.add({
    name: 'inventario',
    urlGet: `inventarios/obtenerListar`,
    urlCreate: `inventarios/create`,
    urlUpdate: `inventarios/edit`,
    urlGetPartial: `inventarios/ObtenerListarPorArea`,
    GetPartialKey: function () {
        return Id.value || null;
    },
    load: function (page) {

        $('#perfiles').loadData(this, page);

    }, 
    models: [
        {
            id: 'Id',
            requiredSave: () => true,
            messageEmpty: 'El Id es requerido',
            key: true
        },       
        {
            id: 'Nombre',
            requiredSave: () => true,
            messageEmpty: 'El nombre es requerido',   
            type: stores.types.editable
            
        }, {
            id: 'Descripcion',
            type: stores.types.editable,
            requiredSave: () => true,
            messageEmpty: 'La escripcion es requerida',  
        }, {
            id: 'UmId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'La unidad de medida es requerida'
        }, {
            id: 'FamiliaId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'La familia es requerida'
        }, {
            id: 'PresentacionId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'La presentacion es requerida'
        }, {
            id: 'EstadoId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El campo es requerido'
        }
    ]
});
