using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Mapper
    {
        public static TTarget Map<TSource, TTarget>(TSource model)
        where TTarget : new()
        {
            if (model == null)
            {
                return default(TTarget);
            }

            TTarget entity = new TTarget();

            foreach (var modelProperty in typeof(TSource).GetProperties())
            {
                var entityProperty = typeof(TTarget).GetProperty(modelProperty.Name);

                if (entityProperty != null && entityProperty.CanWrite)
                {
                    var value = modelProperty.GetValue(model);
                    entityProperty.SetValue(entity, value);
                }
            }

            return entity;
        }
    }
}
