(function ($) {

    $.fn.loadCatalog = function (settings) {

        var options = $.extend({
            verb: 'get',
            defaultValue: null,
            failCallback: undefined,
            lock: false,
            defaultValueLock: null,
            url: null,
            open: false,
            silent: false,
            render: '',
        }, settings);

        var that = this;

        return new Promise((resolve, reject) => {
            $(that).each(function () {
                var select = $(this);

                var model = findModel($(select).attr('id'));

                var $selectize = findSelectize(model.elemt || model.id);

                var render = options.render || model.render;

                $(select).loadingShow(true);

                var callback = result => {

                    fillSelect(
                        $selectize,
                        result,
                        {
                            render: render,
                            defaultValue: options.defaultValue,
                            open: options.open
                        },
                        options.lock,
                        options.silent);

                    $(select).loadingShow(false);

                    return resolve(result);
                };

                var url = options.url || findUrl(model);

                if (url.length > 0)
                    ajax.get(model.routePrefix+ url, null, callback, options.failCallback);
                else
                {
                    $(select).loadingShow(false);
                    if(url==-1)
                        return resolve(0);
                    else {
                        return reject('No contiene url el model: '+ model.id);
                    }
                }
            });
        });
    };

    $.fn.clearCatalog = function () {

        return $(this).each(function () {

            var select = $(this);

            var $selectize = findSelectize($(select).attr('id'));
            $selectize.selectize.clearOptions();

        });

    }

    $.fn.loadData = function (entity, pag, forEntity) {
        
        return new Promise(resolve => {
            this.each(function () {
                var div = $(this)
                var rows = $(div).attr('data-row') || 10;
                let page = pag || 1;
                let id = entity.GetPartialKey();

                let data = {
                    id,
                    page,
                    rows
                }

                var callback = response => {
                    if (response.total) {

                        var data = response.data;
                        var thead = '<thead><tr>';

                        for (var i in data[0]) {
                            thead += '<th>' + i + '</th>';
                        }
                        thead += '</tr></thead>';

                        var tbody = '<tbody>';
                        var value = '';
                        var l = 0;
                        for (var obj = 0; obj < data.length; obj++) {

                            for (const key in data[obj]) {
                                if (data[obj].hasOwnProperty(key)) {
                                    const element = data[obj][key];
                                    value += `<td>${element}</td>`
                                    l++
                                }
                            }
                            tbody += '<tr>' + value + '</tr>';
                        }
                        tbody += '</tbody>';

                        var tfoot = '';
                        var totalPages = 0;
                        var allRows = response.total;
                        if (allRows % rows > 0)
                            totalPages = Math.floor(allRows / rows) + 1;
                        else
                            totalPages = (allRows / rows);

                        if (allRows > rows) {
                            var RegistroInicio = (page * rows) - (rows - 1);
                            var RegistroHasta = (page * rows);
                            var info = '<p style="float:left;">Montrando de ' + RegistroInicio + ' a ' + (allRows < RegistroHasta ? allRows : RegistroHasta) + " de unos " + allRows + '</p>';

                            tfoot += '<tfoot><tr><td colspan="' + l + '">' + info + '<div class="btn-group" role="group" aria-label="..." style="float:right;">';
                            tfoot += createPaging(5, totalPages, page, entity.name);
                        }
                        tfoot += '</div></td></tr></tfoot>';

                        $(div).html('<table class="table">' + thead + tbody + tfoot + '</table>');
                        $(div).attr('data-row', 10);
                        var tabId = $(div).parent().attr('id');
                        $('a[href$="' + tabId + '"]').find('span.badge').html(data.length);
                      
                        resolve();
                    }
                   
                }
                var url = buildUrlWithValues(entity.urlGetPartial, [forEntity]);
                ajax.get(url, data, callback);
            });
        });
    }

    function createPaging(nButtons, Pages, btnAct, entityName) {
        var paging = '';
        if (Pages > nButtons) {
            var btnBefore = 1;
            var btnAfter = nButtons;
            var nButtonTemp = Math.floor(nButtons / 2);

            if (btnAct > 2 && btnAct <= (Pages - nButtonTemp)) {
                btnBefore = parseInt(btnAct) - nButtonTemp;
                btnAfter = parseInt(btnAct) + nButtonTemp;
            }
            if (btnAct > Pages - nButtonTemp) {
                btnBefore = Pages - nButtons + 1
                btnAfter = Pages;
            }

            paging += createLeftButton(i, btnAct, 'double-left', entityName);
            paging += createLeftButton(i, btnAct, 'left', entityName);
            for (var i = btnBefore; i <= btnAfter; i++) {
                paging += createButton(i, btnAct, entityName);
            }
            paging += createRightButton(i, btnAct, 'right', Pages, entityName);
            paging += createRightButton(i, btnAct, 'double-right', Pages, entityName);

        }
        else
            for (var i = 1; i <= Pages; i++)
                paging += createButton(i, btnAct, entityName);

        return paging;
    }

    function createLeftButton(i, btnAct, orientation, entityName) {
        var button = ''

        if (1 != btnAct)
            button = getBase()
                .replace('{act?}', '')
                .replace('{entity}', entityName)
                .replace('{pag}', (orientation == 'double-left' ? 1 : (parseInt(btnAct) - 1)))
                .replace('{pag}', '<span class="fa fa-angle-' + orientation + ' fa-color-black"></span>');

        return button
    }

    function createRightButton(i, btnAct, orientation, pages, entityName) {
        var button = ''
        if (pages != btnAct)
            button = getBase()
                .replace('{act?}', '')
                .replace('{entity}', entityName)
                .replace('{pag}', (orientation == 'double-right' ? pages : (parseInt(btnAct) + 1)))
                .replace('{pag}', '<span class="fa fa-angle-' + orientation + ' fa-color-black"></span>');

        return button
    }

    function createButton(i, btnAct, entityName) {
        return getBase()
            .replace('{act?}', (i == btnAct ? " active" : ""))
            .replace('{pag}', i)
            .replace('{entity}', entityName)
            .replace('{pag}', i);
    }

    function getBase() {
        return '<button type="button" class="btn {act?}" onclick="load({pag},{entity})">{pag}</button>';
    }

    $.fn.addRow = function (object) {
        return this.each(function () {
           
            var t = $(this);

            var row = $('<tr></tr>');

            for (var key in object) {
                if (object.hasOwnProperty(key)) {
                    const element = object[key];
                    $(row).append(`<td>${element}</td>`);
                }
            }

            $(t).find('tbody').append(row);

        });

    }

    $.fn.updateFooter = function (data) {
        return this.each(function () {
            var t = $(this);

            if (data.length) {
                var page = $(t).find('tfoot tr td').eq(0).attr('data-page');
                $(t).find('tfoot tr td').eq(0).attr('data-page', page + 1)
            } else {
                $(t).find('tfoot tr td').eq(0).html('');
            }           

        });

    }

    $.fn.loadingShow = function (action) {
        $(this).each(function () {
            var select = $(this);
            if (action)
                $(select).parent()
                    .children().find('div.selectize-input')
                    .append('<span><div class="inner" style="width: 16px;position: absolute;top: 5px;background:#ffffff00;"><div id="stock-preview-box" ld-spinner-preview="" ng-config="config" ng-ctrl="demo" ng-mod="mod" style="width:100%;height:100%;margin:0;padding:0;" class="ng-isolate-scope"><div class="lds-svg ng-scope"><svg width="100%" height="100%" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid" class="lds-dual-ring"><circle cx="50" cy="50" ng-attr-r="{{config.radius}}" ng-attr-stroke-width="{{config.width}}" ng-attr-stroke="{{config.stroke}}" ng-attr-stroke-dasharray="{{config.dasharray}}" fill="none" stroke-linecap="round" r="40" stroke-width="15" stroke="#8cd0e5" stroke-dasharray="62.83185307179586 62.83185307179586" transform="rotate(272.85 50 50)"><animateTransform attributeName="transform" type="rotate" calcMode="linear" values="0 50 50;360 50 50" keyTimes="0;1" dur="1s" begin="0s" repeatCount="indefinite"></animateTransform></circle></svg></div><div class="lds-css ng-scope"><div style="width:100%;height:100%" class="lds-dual-ring"><div></div></div></div></div></div></span>');
            else
                $(select).parent().find('div.selectize-control').find('div.selectize-input').find('span').remove();
        });
    }

    $.fn.addError = function (msg) {
        return this.each(function () {
            $(this).parent().find('label.error').remove();
            $(this).parent().find('label.warn').remove();

            let css = 'right : -3px;';
            if (this.tagName == 'A') {                
                css =`left : 3px;`;
            }
           
            $(`<label for="${this.id}" class="error" style="${css}display:none">${msg}</label>`).insertBefore(this);

            setTimeout(() => {
                $("label.error").fadeIn();
            }, 300);

            setTimeout(() => {
                $("label.error").fadeOut(function () {
                    $("label.error").remove();
                });
            }, 3000);

        });
    }

    $.fn.addWarn = function (msg) {
        return this.each(function () {
            $(this).parent().find('label.warn').remove();
            $(this).parent().find('label.error').remove();
            let css = 'right : -3px;';
            if (this.tagName == 'A') {
                css = `left : 3px;`;
            }
            $(`<label for="${this.id}" class="warn" style="${css}display:none">${msg}</label>`).insertBefore(this);

            setTimeout(() => {
                $("label.warn").fadeIn();
            }, 300);

            setTimeout(() => {
                $("label.warn").fadeOut(function () {
                    $("label.warn").remove();
                });
            }, 3000);

        });
    }

    $.fn.extend({
        animateCss: function (animationName, callback) {
            var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';

            this.addClass('animated ' + animationName).one(animationEnd, function () {
                $(this).removeClass('animated ' + animationName);
                if (callback)
                    callback();
            });

            return this;
        }
    });

    $.fn.btnLoading = function (msg) {
        return this.each(function () {
            var b = $(this);
            $(b).html((msg || 'Buscando...') + ' <span class="fa fa-spinner fa-pulse"></span>');
            $(b).attr('loading', true);
        });
    }

    $.fn.btnDefault = function (msg, icon) {
        return this.each(function () {
            var b = $(this);
            $(b).html((msg || 'Buscar...') + ' <span class="fa fa-' + (icon || 'search') + '"></span>');
            $(b).removeAttr('loading');
        });
    }

    $.fn.isLoading = function (msg) {
        let loading = false;
        this.each(function () {
            var b = $(this);
            if ($(b).attr('loading')) {
                $.notify({ message: msg || "Proceso en curso" }, { type: "warning " });
                loading = true;
            }
        });
        return loading;
    }

}(jQuery));

if (!Array.prototype.add) {
    (function () {
        Array.prototype.add = function (ob) {
            this.push(ob);
        }
    })();
}

if (!Array.prototype.getOnlyColumns) {
    (function () {
        Array.prototype.getOnlyColumns = function (ob) {

            var models = [];
            _entities.forEach(e => {
                models.add(e.models);
            });
            return modelss.flat();

        }
    })();
}

if (!Array.prototype.isValidTo) {
    (function () {
        Array.prototype.isValidTo = function (action) {
            var ok = true;
            let modelsWithError = [];
            for (let model of this) {
                if (model[action]()) {
                    if (!model.isValid()) {
                        model.showError();
                        ok = false;
                        modelsWithError.push(model);
                    }
                }

                if (ok) {
                    ok = validate.model(model);
                    if (!ok)
                        modelsWithError.push(model);
                }
            }

            if (!ok) {

                if (modelsWithError[0].tabName) {
                    $('a[href="#' + modelsWithError[0].tabName + '"').tab('show');
                }

            }

            return ok;
        }
    })();
}


HTMLElement.prototype.getValue = function () {
    return this.getAttribute('data-value');
}

HTMLElement.prototype.setValue = function (value) {
    let currency = String(value).money();

    this.innerText = currency
    this.value = currency;

    return this.setAttribute('data-value', value);
}


Number.prototype.if = function (condition) {
    var currentValue = this.valueOf()
    if (condition) {
        return{
            then :function(value){
                return +currentValue + value;
            }
        }
    }else{
        return{
            then :function(value){
                return +currentValue;
            }
        }
    }

}

Number.prototype.less = function (value) {
    var currentValue = this.valueOf()
    return {
        if: function (condition) {
            if (condition) {
                return +currentValue - value;
            } else {
                return +currentValue
            }
        }
    }
}

if (!Array.prototype.sum) {
    Array.prototype.sum = function (elemt) {
        if (elemt)
            return this.reduce((a, b) => (+a) + (+b[elemt]), 0);
        else
            return this.reduce((a, b) => (+a) + (+b), 0);
    };
}

if (!Array.prototype.update) {
    Array.prototype.update = function (data, conditions) {
        this.map(x => {

            var [col, , valor] = conditions;

            if (x[col] == valor) {
                for (var i = 0; i < data.length; i++) {
                    x[data[ i ][ 0 ]] = data[ i ][ 1 ];
                }
            }

            return x;
        })

    };
}

if (!Array.prototype.where) {
    Array.prototype.where = function (func, thisArg) {
        'use strict';
        if (!((typeof func === 'Function' || typeof func === 'function') && this))
            throw new TypeError();

        var len = this.length >>> 0,
            res = new Array(len), // preallocate array
            t = this, c = 0, i = -1;
        if (thisArg === undefined) {
            while (++i !== len) {
                // checks to see if the key was set
                if (i in this) {
                    if (func(t[i], i, t)) {
                        res[c++] = t[i];
                    }
                }
            }
        }
        else {
            while (++i !== len) {
                // checks to see if the key was set
                if (i in this) {
                    if (func.call(thisArg, t[i], i, t)) {
                        res[c++] = t[i];
                    }
                }
            }
        }

        res.length = c; // shrink down array to proper size
        return res;
    };
}

String.prototype.money = function () {
    var _config = currentConfiguration();
    return `${_config.moneda} ${this}`;
}



Number.prototype.percent = function(percent)
{
    var currentValue = this.valueOf()
    return currentValue*percent/100;
}

String.prototype.percent = function(percent)
{
    return parseFloat(this.valueOf()).percent(percent);
}

Number.prototype.between = function(min, max)
{
    var currentValue = this.valueOf();
    if(currentValue < min)
        return min;
    if(currentValue > max)
        return max;
    return currentValue;

}

String.prototype.between = function(min, max)
{
    var currentValue = this.valueOf();
    if(currentValue < min)
        return min;
    if(currentValue > max)
        return max;
    return currentValue;

}