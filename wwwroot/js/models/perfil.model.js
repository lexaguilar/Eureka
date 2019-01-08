_entitiesList.add({
    name: 'perfil',
    urlGet: `perfiles/obtenerListar`, 
    urlCreate: `perfiles/create`,
    urlUpdate: `perfiles/edit`, 
    urlGetPartial: 'Perfiles/obtener-por/{parametro}',
    GetPartialKey: function () {
        return Id.value||null;
    },
    load: function (page, forEntity) {
        
        $('#perfiles').loadData(this, page, forEntity);

    },
    models: [
        {
            id: 'Username',
            requiredSave: () => true,
            messageEmpty: 'El Username es requerido',
            key: true
        },
        {
            id: 'Nombre',
            requiredSave: () => true,
            messageEmpty: 'El Nombre es requerido',
        },
        {
            id: 'FechaNacimiento',
            requiredSave: () => true,
            messageEmpty: 'El FechaNacimiento es requerido',
        },
        {
            id: 'RolId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El RolId es requerido'
        },
        {
            id: 'Correo',
            requiredSave: () => true,
            messageEmpty: 'El Correo es requerido'
        },
        {
            id: 'Telefono',
            requiredSave: () => true,
            messageEmpty: 'El Telefono es requerido'
        },
        {
            id: 'Contrasena',
            requiredSave: () => true,
            messageEmpty: 'El Contrasena es requerido'
        },
        {
            id: 'AreaId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El AreaId es requerido'
        },
        {
            id: 'UrlTemporal',
            requiredSave: () => true,
            messageEmpty: 'El UrlTemporal es requerido'
        },
        {
            id: 'UtcreadoEl',
        },
        {
            id: 'EstadoId',
            type: stores.types.select,
            requiredSave: () => true,
            messageEmpty: 'El EstadoId es requerido'
        },
    ]
});
