var _entitiesList = [];
var _entities = [];

var entityConfig = {

    createModel: function (options) {

        var settings = $.extend({
            routePrefix: '',
            key: null,
            id: null,
            url: '',
            _url: null,
            itemDependency: [],
            hasDependency: function () { return this.itemDependency.length > 0; },
            pagePosition: null,
            tabName: null,
            messageEmpty: '',
            columnName: null,          
            inputMinLength: null,
            inputMaxLength: null,
            type: stores.types.input,
            getValue: function () {
                return getValueByTag[this.type](this.id);//common.js
            },
            setValue: function (value) {
                return setValueByTag[this.type](this.id, value); //common.js
            },
            clearValue: function () {
                return clearValueByTag[this.type](this.id);//common.js
            },
            isValid: function () {
                var value = this.getValue();
                return validate.validateField($.trim(value)); //validations.js
            },
            onChange: function (value, text) {
                //do somethings
            },
            onLoad: function () {
                //do somethings
            },
            //
            display: function () {
                return this.id;
            },
            render: '',
            requiredSave: () => false,
            requiredNextPage: () => false,
            showError: function () {

                var selector = this.elemt || this.id;

                if (this.type == stores.types.select) {
                    $('#' + selector + '-selectized').parent().addClass('isEmpty');
                    $('#' + selector).addError(this.messageEmpty);
                } else {
                    $('#' + selector).addClass('isEmpty').addError(this.messageEmpty);
                }

            },
            clearError: function () {
                var selector = this.elemt || this.id;

                if (this.type == stores.types.select) {
                    $('#' + selector + '-selectized').parent().removeClass('isEmpty');
                } else {
                    $('#' + selector).removeClass('isEmpty');
                }
            },
            showMessage: function (msg) {

                var selector = this.elemt || this.id;
                $('#' + selector).addClass('isEmpty').addWarn(msg || this.messageEmpty);

            },
            showMessageAsError: function (msg) {

                var selector = this.elemt || this.id;
                $('#' + selector).addClass('isEmpty').addError(msg || this.messageEmpty);

            },

        }, options);

        settings.columnName = settings.columnName || settings.id;

        return settings;

    }
}

var createEntities = entities => {

    entities.forEach(entity => {
        let models = [];

        for (let i = 0; i < entity.models.length; i++) {

            const model = entity.models[i];
            let cfg = {};

            for (var n = 0; n < _entities.where(e => e.name == entity.name).length; n++) {
                if (_entities[n].models.some(x => x.id == model.id))
                    throw new Error(`Ya existe un modelo con el id ${model.id} en la entidad ${entity.name}`);
            }

            if (typeof model.url != "undefined" && String(model.url).length > 0 && model.type != stores.types.select)
                throw new Error(`Se estableció una url para el modelo ${model.id} en la entidad ${entity.name} pero el tipo de dato ${model.type} no esta valido`);

            if (validateFuntion(model.requiredSave) && typeof model.messageEmpty == "undefined")
                throw new Error(setErrorValidation(model, 'guardar', entity.name));

            //valores 
            cfg.tabName = model.tabName;
            cfg.pagePosition = model.pagePosition >= 0 ? model.pagePosition : entity.pagePosition;

            if (validateFuntion(model.requiredNextPage) && typeof cfg.pagePosition == "undefined")
                throw new Error(`Se estableció la validación requiredNextPage para la columna ${model.id} en la entidad ${entity.name} pero no se establecio la propiedad «pagePosition»`);
            //

            for (const property in model) {
                if (model.hasOwnProperty(property)) {
                    const prop = model[property];
                    cfg[property] = prop;
                }
            }

            models.push(entityConfig.createModel(cfg));
        }

        if (_entities.some(m => m.name == entity.name)) {
            models.forEach(m => {
                _entities.find(e => e.name == entity.name).models.add(m);
            });

            if (entity.urlCreate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.urlCreate)
                    throw new Error(`Se esta agregando la propiedad urlCreate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.urlUpdate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.urlUpdate)
                    throw new Error(`Se esta agregando la propiedad urlUpdate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }           

            if (entity.onSuccessCreate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.onSuccessCreate)
                    throw new Error(`Se esta agregando la propiedad onSuccessCreate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.onFailCreate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.onFailCreate)
                    throw new Error(`Se esta agregando la propiedad onFailCreate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.onSuccessUpdate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.onSuccessUpdate)
                    throw new Error(`Se esta agregando la propiedad onSuccessUpdate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.onFailUpdate) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.onFailUpdate)
                    throw new Error(`Se esta agregando la propiedad onFailUpdate a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.urlGetPartial) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.urlGetPartial)
                    throw new Error(`Se esta agregando la propiedad urlGetPartial a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.load) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.load)
                    throw new Error(`Se esta agregando la propiedad load a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

            if (entity.GetPartialKey) {
                let tmpEntity = _entities.find(m => m.name == entity.name);
                if (tmpEntity.GetPartialKey)
                    throw new Error(`Se esta agregando la propiedad GetPartialKey a la entidad ${entity.name}, pero esta ya fue agregada previamente`);
            }

           
          

        } else {

            var onSuccessCreate = function (data) {
                showNotify(`Se guardó correctamente Id: ${data.Id}`, 'success', 'Informacion');
                setHTMLFromEntity({ Id: data.Id }, entity.name);
                $("#btnGuardar").html('<i class="ion-checkmark ion-14px"></i> Actualizar');
                stopLoading();
            }

            var onFailCreate = function (jqXHR) {
                ajax.failResult(jqXHR);
                stopLoading();
            }

            var onSuccessUpdate = function (data) {
                showNotify(`Se actualizó correctamente Id: ${data.Id}`, 'mint', 'Informacion');
                //setHTMLFromEntity(data, entity.name, ['Id']);
                stopLoading();
            }

            var onFailUpdate = function (jqXHR) {
                if (jqXHR.status == ajax.status.BadRequest) {
                    if (typeof jqXHR.responseJSON == 'object') {
                        var model = findModel(jqXHR.responseJSON.item);
                        model.showMessageAsError('Validar este campo');
                    }
                    
                    ajax.failResult(jqXHR);
                } else {
                    ajax.failResult(jqXHR);
                }
                stopLoading();
            }

            var load = function (parametro) {
                $('#' + parametro).loadData()
            }

            _entities.add({
                name: entity.name || 'indefinido',
                mode: entity.mode || 'single',
                urlCreate: entity.urlCreate,
                urlUpdate: entity.urlUpdate,
                onSuccessCreate: entity.onSuccessCreate || onSuccessCreate,
                onFailCreate: entity.onFailCreate || onFailCreate,
                onSuccessUpdate: entity.onSuccessUpdate || onSuccessUpdate,
                onFailUpdate: entity.onFailUpdate || onFailUpdate,
                urlGetPartial: entity.urlGetPartial,
                load: entity.load,
                GetPartialKey: entity.GetPartialKey,
                models: models
            });
        }
    })


};

var validateFuntion = f => (typeof f != "undefined" && typeof f == "function" /*&& f.call() == true*/);

var setErrorValidation = (model, action, name) => `Se estableció la validación de ${action} para el modelo «${model.id}» pero no el mensaje de validación, establezca la propiedad «messageEmpty» para el modelo ${model.id} de la entidad «${name}»`
