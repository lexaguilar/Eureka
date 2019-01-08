
using Eureka.Models;
using Eureka.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Eureka { 
    public abstract class BusinessLogicInitializer<T> where T : class
    {
        internal abstract BadRequestViewModel ValidateForCreate(EurekaContext _context);
        internal abstract BadRequestViewModel ValidateForEdit(EurekaContext _context, T source);
        internal void CopyFrom(T source, Expression<Func<T, object>> expression)
        {
            NewExpression body = (NewExpression)expression.Body;

            foreach (MemberExpression arg in body.Arguments)
            {
                PropertyInfo property = (PropertyInfo)arg.Member;
                var type = property.PropertyType;

                if (!property.CanWrite)
                    continue;

                object value = property.GetValue(source);

                property.SetValue(this, value);
            }
        }
        
    }
}
