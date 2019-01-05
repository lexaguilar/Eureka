_entitiesList.add({
    name: 'perfil',
    urlGet: `perfiles/obtenerListar`, 
    urlGetPartial: `perfiles/ObtenerListarPorArea`,
    GetPartialKey: function () {
        return Id.value||null;
    },
    urlCreate: `perfiles/create`,    
    urlUpdate: `perfiles/update`, 
    load: function (page) {
        
        $('#perfiles').loadData(this, page);

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
