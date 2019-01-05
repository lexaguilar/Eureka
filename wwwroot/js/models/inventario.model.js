_entitiesList.add({
    name: 'inventario',
    urlApi:  `inventarios/`,   
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
            
        }, {
            id: 'Descripcion'
        }, {
            id: 'Um',
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
        }
    ]
});
