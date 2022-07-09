using System.Reflection;

namespace AutoMapp
{
    public class Mapper : IMapper
    {
        public TTarget MapTo<TSource, TTarget>(TSource source) where TTarget : new()
        {
            TTarget target = new TTarget();
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            foreach (var property in sourceType.GetProperties(bindingFlags))
            {
                var value = property.GetValue(source, null);
                var targetProperty = targetType.GetProperty(property.Name, bindingFlags);
                if (targetProperty != null)
                {
                    targetProperty.SetValue(target, value);
                }
            }

            return target;
        }
    }
}