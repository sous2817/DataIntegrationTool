using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataIntegrationTool.BaseClasses.DynamicHelper
{
    partial class DynamicTypeHelper<T>
    {
        internal class DynamicType : Type
        {
            private readonly Type _baseType;

            public DynamicType(Type delegatingType)
            {
                if (delegatingType == null)
                    throw new ArgumentNullException(nameof(delegatingType));
                _baseType = delegatingType;
            }

            public override Assembly Assembly => _baseType.Assembly;

            public override string AssemblyQualifiedName => _baseType.AssemblyQualifiedName;

            public override Type BaseType => _baseType.BaseType;

            public override string FullName => _baseType.FullName;

            public override Guid GUID => _baseType.GUID;

            protected override TypeAttributes GetAttributeFlagsImpl()
            {
                throw new NotImplementedException();
            }

            protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers) => _baseType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);

            public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => _baseType.GetConstructors(bindingAttr);

            public override Type GetElementType() => _baseType.GetElementType();

            public override EventInfo GetEvent(string name, BindingFlags bindingAttr) => _baseType.GetEvent(name, bindingAttr);

            public override EventInfo[] GetEvents(BindingFlags bindingAttr) => _baseType.GetEvents(bindingAttr);

            public override FieldInfo GetField(string name, BindingFlags bindingAttr) => _baseType.GetField(name, bindingAttr);

            public override FieldInfo[] GetFields(BindingFlags bindingAttr) => _baseType.GetFields(bindingAttr);

            public override Type GetInterface(string name, bool ignoreCase) => _baseType.GetInterface(name, ignoreCase);

            public override Type[] GetInterfaces() => _baseType.GetInterfaces();

            public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => _baseType.GetMembers(bindingAttr);

            protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo[] GetMethods(BindingFlags bindingAttr) => _baseType.GetMethods(bindingAttr);

            public override Type GetNestedType(string name, BindingFlags bindingAttr) => _baseType.GetNestedType(name, bindingAttr);

            public override Type[] GetNestedTypes(BindingFlags bindingAttr) => _baseType.GetNestedTypes(bindingAttr);

            public override PropertyInfo[] GetProperties(BindingFlags bindingAttr) => _baseType.GetProperties(bindingAttr)
    .Concat(DynamicProperties).ToArray();

            // Look for the CLR property with this name first.
            protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers) => GetProperties(bindingAttr).FirstOrDefault(prop => prop.Name == name)
                // and then for a custom property
                ?? DynamicProperties.FirstOrDefault(prop => prop.Name == name);

            protected override bool HasElementTypeImpl()
            {
                throw new NotImplementedException();
            }

            public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters) => _baseType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);

            protected override bool IsArrayImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsByRefImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsCOMObjectImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPointerImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPrimitiveImpl() => _baseType.IsPrimitive;

            public override Module Module => _baseType.Module;

            public override string Namespace => _baseType.Namespace;

            public override Type UnderlyingSystemType => _baseType.UnderlyingSystemType;

            public override object[] GetCustomAttributes(bool inherit) => _baseType.GetCustomAttributes(inherit);

            public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _baseType.GetCustomAttributes(inherit);

            public override bool IsDefined(Type attributeType, bool inherit) => _baseType.IsDefined(attributeType, inherit);

            public override string Name => _baseType.Name;

            public override bool ContainsGenericParameters => _baseType.ContainsGenericParameters;

            public override IEnumerable<CustomAttributeData> CustomAttributes => _baseType.CustomAttributes;

            public override MethodBase DeclaringMethod => _baseType.DeclaringMethod;

            public override Type DeclaringType => _baseType.DeclaringType;

            public override bool Equals(Type o) => _baseType == o;

            public override Type[] FindInterfaces(TypeFilter filter, object filterCriteria) => _baseType.FindInterfaces(filter, filterCriteria);

            public override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria) => _baseType.FindMembers(memberType, bindingAttr, filter, filterCriteria);

            public override GenericParameterAttributes GenericParameterAttributes => _baseType.GenericParameterAttributes;

            public override int GenericParameterPosition => _baseType.GenericParameterPosition;

            public override Type[] GenericTypeArguments => _baseType.GenericTypeArguments;

            public override int GetArrayRank() => _baseType.GetArrayRank();

            public override IList<CustomAttributeData> GetCustomAttributesData() => _baseType.GetCustomAttributesData();

            public override MemberInfo[] GetDefaultMembers() => _baseType.GetDefaultMembers();

            public override string GetEnumName(object value) => _baseType.GetEnumName(value);

            public override string[] GetEnumNames() => _baseType.GetEnumNames();

            public override Type GetEnumUnderlyingType() => _baseType.GetEnumUnderlyingType();

            public override Array GetEnumValues() => _baseType.GetEnumValues();

            public override EventInfo[] GetEvents() => _baseType.GetEvents();

            public override Type[] GetGenericArguments() => _baseType.GetGenericArguments();

            public override Type[] GetGenericParameterConstraints() => _baseType.GetGenericParameterConstraints();

            public override Type GetGenericTypeDefinition() => _baseType.GetGenericTypeDefinition();

            public override InterfaceMapping GetInterfaceMap(Type interfaceType) => _baseType.GetInterfaceMap(interfaceType);

            public override MemberInfo[] GetMember(string name, BindingFlags bindingAttr) => _baseType.GetMember(name, bindingAttr);

            public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr) => _baseType.GetMember(name, type, bindingAttr);

            public override bool IsAssignableFrom(Type c) => _baseType.IsAssignableFrom(c);

            public override bool IsConstructedGenericType => _baseType.IsConstructedGenericType;

            public override bool IsEnum => _baseType.IsEnum;

            public override bool IsEnumDefined(object value) => _baseType.IsEnumDefined(value);

            public override bool IsEquivalentTo(Type other) => _baseType.IsEquivalentTo(other);

            public override bool IsGenericParameter => _baseType.IsGenericParameter;

            public override bool IsGenericType => _baseType.IsGenericType;

            public override bool IsGenericTypeDefinition => _baseType.IsGenericTypeDefinition;

            public override bool IsInstanceOfType(object o) => _baseType.IsInstanceOfType(o);

            public override bool IsSerializable => _baseType.IsSerializable;

            public override bool IsSubclassOf(Type c) => _baseType.IsSubclassOf(c);

            public override RuntimeTypeHandle TypeHandle => _baseType.TypeHandle;

            public override Type MakeArrayType() => _baseType.MakeArrayType();

            public override Type MakeArrayType(int rank) => _baseType.MakeArrayType(rank);

            public override Type MakeByRefType() => _baseType.MakeByRefType();

            public override Type MakeGenericType(params Type[] typeArguments) => _baseType.MakeGenericType(typeArguments);

            public override Type MakePointerType() => _baseType.MakePointerType();

            public override MemberTypes MemberType => _baseType.MemberType;

            public override int MetadataToken => _baseType.MetadataToken;

            public override Type ReflectedType => _baseType.ReflectedType;

            public override System.Runtime.InteropServices.StructLayoutAttribute StructLayoutAttribute => _baseType.StructLayoutAttribute;

            public override string ToString() => _baseType.ToString();
        }

    }
}
