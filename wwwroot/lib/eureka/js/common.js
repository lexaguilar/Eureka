

// Constructor.
var Interface = function (name, methods) {
    if (arguments.length != 2) {
        throw new Error('Interface constructor called with &quot; + arguments.length arguments, but expected exactly 2');
    }
    this.name = name;
    this.methods = [];
    for (var i = 0, len = methods.length; i < len; i++) {
        if (typeof methods[i] !== 'string') {
            throw new Error('Interface constructor expects method names to be passed in as a string.');
        }
        this.methods.push(methods[i]);
    }
};

Interface.ensureImplements = function (object) {
    if (arguments.length < 2) {
        throw new Error('Function Interface.ensureImplements called with ' + arguments.length + ' arguments, but expected at least 2');
    }
    for (var i = 1, len = arguments.length; i < len; i++) {
        var interface = arguments[i];
        if (interface.constructor !== Interface) {
            throw new Error('Function Interface.ensureImplements expects arguments two and above to be instances of Interface.');
        }
        for (var j = 0, methodsLen = interface.methods.length; j < methodsLen; j++) {
            var method = interface.methods[j];
            if (!object[method] || typeof object[method] !== 'function') {
                throw new Error('Function Interface.ensureImplements: object does not implement the ' + interface.name + ' interface. Method ' + method + ' was not found.');
            }
        }
    }
};

// Encuetra un item de las opciones de un selectize
// param options => datos donde se buscara
// param option => dato que se buscara
var findRecord = (options, option) => Object.entries(options).find(x => x[0] == option)[1];

var findSelectize = id => {
    let thisSelec = $selectizies.filter((n, v) => v.id == id);
    if (thisSelec.length) {
        return thisSelec[0];
    } else {
        throw Error(`No se encontró en la lista elemento con id ${id}, verifique si lo agregó`)
    }
}

// Setea los datos en un elemento html de tipo select
// param $select => el id del elemento
// param data => datos a setear
// param lock => establer si es control estara bloqueado
// param silent => establer si se activar el evento change del control cuando se seter un valor por defecto
var fillSelect = ($select, data, options, lock, silent = false) => {
    var control = $select.selectize;

    if (lock) control.lock(); else control.unlock();

    control.clearOptions();

    control.addOption(data);   
    var model = findModel(control.$input[0].id);
    model.onLoad(data);

    if (data.length == 1) {
        control.setValue(data[0].id, silent);
        control.lock();
    }
    else {
        if (options.defaultValue != null || options.defaultValue != undefined)
            control.setValue(options.defaultValue, silent);
        else if (options.open)
            control.open();
    }

    if (options.render) {
        control.clearCache();
        control.settings.render = options.render;
    }
}

//Encuentra la url de modelo
// param data => propiedades del model
var findUrl = model => {
    var ready = true;
    var url = model.url;

    if (url.length == 0)
        if (typeof model._url == 'function')
        {
            model.url  = model._url();
            return findUrl(model);
        }
        else
            throw new Error('No se encontro la url para el modelo ' + model.id);

    var parm;

    while (url.includes('{') || url.includes('}')) {

        parm = url.substring(url.indexOf('{'), url.indexOf('}') + 1);
        var modelParent = findModel(parm.replace('{', '').replace('}', ''));

        var value = modelParent.getValue();
        if (validate.validateField(value))
            url = url.replace(parm, value);
        else {
            showError(modelParent);
            ready = false;
            return -1;
        }

    }

    if (ready)
        return url;
    else
        return null;
}

var findEntity = name => {
    return _entities.find(x => x.name == name);
}

// Obtiene un modelo en la lista, sino lo encuentra retorna null
// param id => el id de model a buscar
var findModel = id => {

    var model = getModel(id);

    if (model == null)
        throw new Error(`No se encontró el model «${id}», verifique que se agregó a la coleccion _models`);

    return model;
}

// Obtiene un modelo en la lista, sino lo encuentra retorna null
// param id => el id de model a buscar
var getModel = id => {
    let model = null;

    for (var i = 0; i < _entities.length; i++) {
        if (!model)
            model = _entities[i].models.find(x => x.id == id);
    }

    if (model == undefined)
        return null;

    return model;
}

var findModelByColumnName = (columnName, name) => {

    var model = getModelByColumnName(columnName, name);

    if (model == null)
        throw new Error(`No se encontró el model «${id}» para la entidad «${name}», verifique que se configuro en la entidad «${name}»`);

    return model;
}

var getModelByColumnName = (columnName, name) => {
    let model = null;

    var _entity = _entities.find(x => x.name == name);
    if (!_entity)
        throw new Error(`No se encontró la entidad «${name}», verifique que se agregó a la coleccion _entities`);

    model = _entity.models.find(c => c.columnName == columnName || c.id == columnName);

    if (model == undefined)
        return null;

    return model;
}

var modelsToArray = entities => {
    let models = [];
    entities.forEach(e => {
        models.add(e.models);
    });
    return models.flat();
}

var filterModelsByPagePosition = position => _entities.getOnlyColumns().where(x => x.pagePosition == position);

var filterModelsByAction = action => _entities.getOnlyColumns().where(x => x[action].call());

var compareOr = obj => {
    return {
        with: (...obsj) => obsj.some(x => x == obj)
    }
}

var sum = (...obj) => obj.sum();

var getValueByTag = {
    email: function (id) { return this.input(id); },
    phone: function (id) { return this.input(id); },
    money: id => $elemt('#' + id).getValue(),
    check: id => $elemt('#' + id).checked,
    select: function (id) { return this.input(id); },
    date: function (id) {
        var value = this.input(id);
        var date = moment(value, 'DD-MM-YYYY');

        if (date._isValid)
            return moment(date, 'DD-MM-YYYY').utc().format();
        else
            return '';
    },
    input: id => $elemt('#' + id).value,
    td: id => $elemt('#' + id).getValue(),
    editable : function(id){
        return $('#'+ id).editable('getValue')[id];
    }
}

var setValueByTag = {
    email: function (id, value) {
        this.input(id, value);
    },
    phone: function (id, value) {
        this.input(id, value);
    },
    money: function (id, value) {
        $(id).setValue(value);
    },
    check: function (id, value) {
        $(id).prop('checked', value);
    },
    select: function (id, value) {
        $('#' + id).loadCatalog({defaultValue: value});
    },
    date: function (id, value) {

        var dp = moment(value);

        if (dp._isValid)
            $('#' + id).datepicker("setDate", dp.format('DD-MM-YYYY'));

    },
    input: function (id, value) {
        document.querySelector('#' + id).value = value;
    },
    td: function (id, value) {
        $elemt('#' + id).setValue(value);
    },
}

var clearValueByTag = {
    email: function (id) {
        this.input(id);
    },
    phone: function (id) {
        this.input(id);
    },
    money: function (id) {
        $(id).setValue(0);
    },
    check: function (id) {
        $(id).prop('checked',false);
    },
    select: function (id) {
        $('#' + id).clearCatalog();
    },
    date: function (id) {

        var now = moment();      
        $('#' + id).datepicker("setDate", now.format('DD-MM-YYYY'));

    },
    input: function (id) {
        document.querySelector('#' + id).value = '';
    },
    td: function (id) {
        $elemt('#' + id).setValue(0);
    },
}

var getDatePart = (date, part) => {
    var check = moment($('#' + date).val(), 'MM/YYYY');
    if (check._isValid)
        return check.format(part);
    else
        return 0;
}

var $elemt = selector => document.querySelector(selector);

var showError  = model =>
 {
     switch (model.type) {
         case stores.types.email:
         case stores.types.phone:
         case stores.types.money:
         case stores.types.date:
         case stores.types.input:
             $('#' + model.id).addClass('isEmpty').addError(model.messageEmpty);
             break;
         case stores.types.select:
             $('#' + model.id + '-selectized').parent().addClass('isEmpty');
             $('#' + model.id).addError(model.messageEmpty);
             break;
         default:
             break;
     }
}

var section = {
    next: function () {
        var currentSection = getCurrentSection();
        var models = filterModelsByPagePosition(currentSection);

        if(models.isValidTo('requiredNextPage'))
            this.move(1, true);
        else
            goToUp();

    },
    prev: function () {
        this.move(-1, false);
    },
    move: function (nextPage) {

        var currentSection = getCurrentSection();

        $($('.page')[currentSection]).fadeOut('fast', function () {
            $($('.page')[currentSection + nextPage]).fadeIn('fast');
            goToUp();
            if((currentSection + nextPage) == 1)
                $('#NumId').focus();
        });

    }
};

var getCurrentSection = function () {

    var position = 0;
    $('.page').filter((i, ele) => {
        if ($(ele).is(':visible'))
            position = i;
    });
    return position;

};



//retorna true si esta seleccionado una opcion del control select
//param control => el control a determinar su estado
//El elemento HTML debe renderizado con selectize
var hasItemSelected = control => control.items.length > 0;

//Suma los elementos de un arreglo
//param obj => arreglo
var sum = (...obj) => obj.sum();

//Retorna un valor redondenado a dos digitos
//param num => valor a redondear
var round = num => {
    var scale = 2;
    var result = 0;
    if (!("" + num).includes("e")) {
        result = +(Math.round(num + "e+" + scale) + "e-" + scale);
        return parseFloat(result).toFixed(2);
    } else {
        var arr = ("" + num).split("e");
        var sig = "";
        if (+arr[1] + scale > 0) {
            sig = "+";
        }
        result = +(Math.round(+arr[0] + "e" + sig + (+arr[1] + scale)) + "e-" + scale);
        return parseFloat(result).toFixed(2);
    }

}

var format = value => numeral(value).format('0,0.00'); // numeral(String(value).replace(',', '.')).format('0,0.00');

var allRows = () => $('#coberturas tbody tr');

var updateTD = function (row) {
    return function(indexTD, value)
    {
        if(!isTagSelect($(row),indexTD))
            $(row).find("td").eq(indexTD).html(value);
    }
}

var getEntity = obj => {

    let entity = _entities.find(m => m.name == obj.name); 

    var data = {
        name: obj.name,
        action: obj.action,
        key: function () {
            let _modelKey = entity.models.find(m => m.key);
            if (_modelKey) {
                return _modelKey.getValue();
            }
            if (!_modelKey)
                throw new Error(`No se ha establecido la propiedad key para la entidad ${obj.name}`);
            
        }.apply(),
        isValid: function () {
            if (obj.action)
                return entity.models.isValidTo(obj.action);
            else
                return false;
        }.apply(),
        source: function () {

            var newData = {};
            for (var i = 0; i < entity.models.length; i++) {
                const column = entity.models[i];
                newData[column.columnName || column.id] = column.getValue();
            }
            return newData;

        }.apply(),
        include: function (name) {

            var newEntity = getEntity({ name: name, action: this.action });

            this.isValid = this.isValid && newEntity.isValid;
            this[name] = newEntity;

            return this;

        },
        getData: function () {
            var newData = {};
            for (const key in this) {
                if (this.hasOwnProperty(key)) {
                    const elemt = this[key];

                    if (key == 'source') {
                        for (const i in elemt)
                            if (elemt.hasOwnProperty(i))
                                newData[i] = elemt[i];

                    } else if (typeof elemt != 'function' && typeof elemt == 'object' ) {
                        newData[key] = elemt.getData();
                    }

                }
            }
            return newData;
        },
        save: function () {
            if (this.key > 0)
                return this.update();
            else
                return this.create();
        },
        onSuccessCreate: function () {
            return entity.onSuccessCreate;
        }.apply(),
        onFailCreate: function () {
            return entity.onFailCreate;
        }.apply(),
        onSuccessUpdate: function () {
            return entity.onSuccessUpdate;
        }.apply(),
        onFailUpdate: function () {
            return entity.onFailUpdate;
        }.apply(),
        urlCreate: function () { return entity.urlCreate; }.apply(),
        urlUpdate: function () { return entity.urlUpdate; }.apply(),
        create: function () {
            return this.send(this.urlCreate, this.onSuccessCreate, this.onFailCreate, 'post');
        },
        update: function () {
            return this.send(this.urlUpdate, this.onSuccessUpdate, this.onFailUpdate, 'post');
        },
        send: function (url, myOnSuccess, myOnFail, type) {

            return new Promise((resolve, reject) => {

                var onSuccess = function (response) {
                    if (myOnSuccess)
                        myOnSuccess(response);
                    resolve(response);
                }

                var onError = function (response) {
                    if (myOnFail)
                        myOnFail(response);
                    reject(response);
                }

                ajax.full(`${url}`, this.getData(), onSuccess, onError, type);
            });

        },
        _request: function () {

        },    
    }

    return data;
}

var showNotify = function (mensaje, type, title) {
    $.niftyNoty({
        type: type,
        container: 'floating',
        title: title,
        message: mensaje,
        closeBtn: true,
        timer: 3000
    });
};

var isTagSelect = (row,index) => $($(row).find("td").eq(index)[0].firstChild)[0].tagName == "A"

// comprueba si un elemento de tipo select tiene opciones cargadas
// param select => elemnto a comprobar
var hasData = function (select) {
    var hasOptions = 0;
    for (k in select.options) {
        hasOptions++;
    }
    return hasOptions != 0;
}

function setHTMLFromEntity(entity, entityName = '', entityExcept = []) {

    for (const key in entity) {
        if (entity.hasOwnProperty(key)) {

            const value = entity[key];
            let model = getModelByColumnName(key, entityName);
            if (model) {
                if (!entityExcept.some(e => e == key)) {

                    model.clearValue();
                    if (model.type != stores.types.select)
                        model.setValue(value);
                    else
                        setValueSelect(model, entity);
                }
            }
        }
    }

}


function setValueSelect(model, entity) {

    if (model.hasDependency()) {

        let values = model.itemDependency.map(item => {
            let newModel = findModel(item);
            return entity[newModel.columnName || newModel.id]
        });

        if (!values.some(v => v == null)) {
            {
                let url = buildUrlWithValues(model.url, values);
                $('#' + model.id).loadCatalog({ defaultValue: entity[model.columnName || model.id], silent: true, url: url });
            }
        }
    } else {
        $('#' + model.id).loadCatalog({ defaultValue: entity[model.columnName || model.id], silent: true });
    }

}

var buildUrlWithValues = (url, values) => {
    var ready = true;
    let i = 0, parm = '';

    while (url.includes('{') || url.includes('}')) {

        parm = url.substring(url.indexOf('{'), url.indexOf('}') + 1);
        var value = values[i];
        if (value == undefined)
            throw new Error(`La cantidad de valores (${values.join(',')}) no coincide con los esperados en la url ${url}`);
        url = url.replace(parm, value);
        i++;
    }

    if (ready)
        return url;
    else
        return null;
}

var goToUp = function(){
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}

var showLoading = function(){
    $("#panel-updatable").loading({
        message: 'Guardando...'
    });
}

var stopLoading = function () {
    $("#panel-updatable").loading('stop');
}