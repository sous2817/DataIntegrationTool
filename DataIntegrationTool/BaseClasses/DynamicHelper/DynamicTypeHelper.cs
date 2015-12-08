using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegrationTool.BaseClasses.DynamicHelper
{
    /// <summary>
    /// Dynamic type helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DynamicTypeHelper<T> : ICustomTypeProvider, INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicTypeHelper()
        {
            foreach (var property in GetCustomType().GetProperties())
                _customPropertyValues.Add(property.Name, null);
        }

        /// <summary>
        /// Adds a new property definition for a string type
        /// </summary>
        /// <param name="name"></param>
        public static void AddProperty(string name)
        {
            AddProperty(name, typeof(string));
        }

        /// <summary>
        /// Clears all the properties from a defined object type.
        /// </summary>
        public static void ClearProperties()
        {
            DynamicProperties.Clear();
        }

        /// <summary>
        /// Removes a specific property by name
        /// </summary>
        /// <param name="name">Property to remove</param>
        public static void RemoveProperty(string name)
        {
            var item = DynamicProperties.FirstOrDefault(cp => cp.Name == name);
            if (item != null)
                DynamicProperties.Remove(item);
        }

        /// <summary>
        /// Adds a new dynamic property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        public static void AddProperty(string name, Type propertyType)
        {
            if (!CheckIfNameExists(name))
                DynamicProperties.Add(new DynamicPropertyInfoHelper(name, propertyType, typeof(T)));
        }

        /// <summary>
        /// Adds a new property definition
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        /// <param name="attributes"></param>
        public static void AddProperty(string name, Type propertyType, List<Attribute> attributes)
        {
            if (!CheckIfNameExists(name))
                DynamicProperties.Add(new DynamicPropertyInfoHelper(name, propertyType, attributes, typeof(T)));
        }

        /// <summary>
        /// Sets a property value by name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetPropertyValue(string propertyName, object value)
        {
            DynamicPropertyInfoHelper propertyInfo = DynamicProperties.FirstOrDefault(prop => prop.Name == propertyName);
            if (propertyInfo == null || !_customPropertyValues.ContainsKey(propertyName))
                throw new Exception("There is no property with the name " + propertyName);

            if (_customPropertyValues[propertyName] == value) return;
            _customPropertyValues[propertyName] = value;
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Returns a specific property value by name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetPropertyValue(string propertyName)
        {
            return _customPropertyValues.ContainsKey(propertyName) ? _customPropertyValues[propertyName] : null;
        }

        /// <summary>
        /// Returns a cast property value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public TV GetPropertyValue<TV>(string propertyName)
        {
            return (TV)GetPropertyValue(propertyName);
        }

        /// <summary>
        /// Retrieve all the defined properties (both known and dynamic)
        /// </summary>
        /// <returns></returns>
        public PropertyInfo[] GetProperties()
        {
            return GetCustomType().GetProperties();
        }

        /// <summary>
        /// Gets the custom type provided by this object.
        /// </summary>
        /// <returns>
        /// The custom type. 
        /// </returns>
        public Type GetCustomType()
        {
            return _ctype.Value;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Raises a property change notification
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Validates the value for the given type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool ValidateValueType(object value, Type type)
        {

            return type.IsInstanceOfType(value);

        }

        //private static bool CheckIfNameExists(string name)
        //{
        //    if (DynamicProperties.Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase))
        //        || typeof(T).GetProperties().Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase)))
        //        throw new Exception("Property with this name already exists: " + name);

        //    return false;
        //}

        public static bool CheckIfNameExists(string name)
        {
            return (DynamicProperties.Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase))
                    ||
                    typeof (T).GetProperties()
                        .Any(p => 0 == string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase)));
        }

        #region Data
        private static readonly List<DynamicPropertyInfoHelper> DynamicProperties = new List<DynamicPropertyInfoHelper>();
        private readonly Dictionary<string, object> _customPropertyValues = new Dictionary<string, object>();
        private readonly Lazy<DynamicType> _ctype = new Lazy<DynamicType>(() => new DynamicType(typeof(T)));
        #endregion
    }
}
