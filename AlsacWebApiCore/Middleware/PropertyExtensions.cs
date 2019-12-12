using System.Reflection;

namespace AlsacWebApiCore.Middleware
{
    // Define a shortcut method that fetches a field of a particular name.
    internal static class PropertyExtensions
    {
        public static object GetProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetTypeInfo().GetDeclaredProperty(propertyName)?.GetValue(obj);
        }
    }
}
