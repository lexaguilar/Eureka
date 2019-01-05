var ajax = {
    get: (url, data, callback, failCallback) =>{
        $.get((pathBase + url), data ? data : null, callback).fail(failCallback || ajax.failResult);
    },
    post: (url, data, callback, failCallback) => {
        $.post((pathBase + url), data ? data : null, callback).fail(failCallback || ajax.failResult);
    },
    full: (url, data, callback, failCallback, type = 'post') => {
        $.ajax({
            url: (pathBase + url),
            type: type,
            dataType: "json",
            data: data,
            success: callback,
            failure: failCallback || ajax.failResult,
            error: failCallback || ajax.failResult
        });
    },
    status: {
        BadRequest: 400,
        Unauthorized: 401,
        NotFound: 404,
        IServerError: 500
    },
    failResult: jqXHR => {
        if (jqXHR.status == ajax.status.Unauthorized)
            ajax.redirectToLogin();
        if (jqXHR.status == ajax.status.IServerError)
            ajax.showMessage({ responseJSON: 'Error interno de la aplicación' });
        else              
            ajax.showMessage(jqXHR);
        
    },
    redirectToLogin: () => { window.location.href = pathBase + 'cuenta/login' },
    showMessage: jqXHR => $.notify(jqXHR.responseJSON, { mouse_over: 'pause' }),
    showMessage: jqXHR => $.niftyNoty({
        type: 'success',
        container: 'floating',
        title: 'Error al guardar',
        message: jqXHR.responseJSON,
        closeBtn: true,
        timer: 2000
    })
};