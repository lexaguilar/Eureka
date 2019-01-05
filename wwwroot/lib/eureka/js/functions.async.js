
async function setHTMLFromEntityAsync(entity, entityName = '', entityExcept=[]) {
    
    for (const key in entity) {
        if (entity.hasOwnProperty(key)) {

            const value = entity[key];
            let model = getModel(key, entityName);           
            if (model) {
                if (entityExcept.some(e => e != key)) {

                    model.clearValue();
                    if (model.type != stores.types.select)
                        model.setValue(value);
                    else {
                        await setValueSelectAsync(model, entity);
                    }

                }                
            }            
        }
    }

}

