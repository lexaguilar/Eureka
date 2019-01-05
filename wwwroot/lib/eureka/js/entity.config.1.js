var _entitiesList = [];
var _entities = [];

var entityConfig = {

    configurateColumns: function (options) {
        var settings = $.extend({
            routePrefix: 'api/catalogs/',
            urlApi : null,
            id: null,             
            url: '',
            _url: null,       
            elemt: '',
            entity: '',
            itemDependency: [],
            hasDependency: function () { return this.itemDependency.length > 0; },           
            messageEmpty: '',
            columnName: null,           
            inputMinLength: null,
            type: stores.types.input,            
            getValue: function () {
                return getValueByTag[this.type](this.elemt||this.id);//common.js
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
            onLoad: function(){
                //do somethings
            },
            mode: 'single',
            display: function () {
                return this.id;
            },            
            render: '',           
            requiredSave: () => false,
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
                $('#' + selector).addClass('isEmpty').addWarn(msg||this.messageEmpty);

            },
            onSaveCallback: function (data) {

                showNotify(`Se guardó correctamente Id: ${data.id}`,'success','Informacion');
                setHTMLFromEntityAsync({ Id: data.id});
                $("#btnGuardar").html('<i class="ion-checkmark ion-14px"></i> Actualizar');
                stopLoading();
            },
            onUpdateCallback: function (data) {

                showNotify(`Se actualizó correctamente Id: ${data.id}`,'info','Informacion');
                setHTMLFromEntityAsync(data);
                stopLoading();


            },
            onDeleteCallback: function (data) {

                showNotify(`Se eliminó correctamente el Id: ${data.id}`, 'danger','warning');

            },            
            onSaveFailCallback:function(jqXHR){
                ajax.failResult(jqXHR);
            }
        }, options);

        return settings;
    }
}

var createModel = function (entitiesList) {

    for (let index = 0; index < entitiesList.length; index++) {

        const entity = entitiesList[index];
        const entityName = entity.name;

        for (let i = 0; i < entity.columns.length; i++) {
            const column = entity.columns[i];

            if (entity.columns.some(x => x.id == column.id))
                throw new Error(`Ya existe una columna con el id ${column.id} en la entidad ${entityName}`);

            if (typeof column.url != "undefined" && String(column.url).length > 0 && column.type != stores.types.select)
                throw new Error(`Se estableció una url para el modelo ${column.id} pero el tipo de dato ${column.type} no es valido`);
 
            if (validateFuntion(column.requiredSave) && typeof column.messageEmpty == "undefined")
                throw new Error(setErrorValidation(column, 'guardar', entity));
           

            var cfg = {};
            for (const property in column) {
                if (column.hasOwnProperty(property)) {
                    const prop = column[property];
                    cfg[property] = prop;
                }
            }
            
            cfg['entity'] = entity;
            cfg['mode'] = model.mode;
            cfg['urlApi'] = model.urlApi;
            cfg['renderModel'] = model.renderModel;
            _models.add(
                function () {
                    return modelConfig.createModel(cfg);
                }.apply()
            )
        }
    }
};

var validateFuntion = f => (typeof f != "undefined" && typeof f == "function" /*&& f.call() == true*/);

var setErrorValidation = (model, action, entity) => `Se estableció la validación de ${action} para el modelo «${model.id}» pero no el mensaje de validación, establezca la propiedad «messageEmpty» para el modelo ${model.id} de la entidad «${entity}»`
