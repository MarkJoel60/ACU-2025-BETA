// Decompiled with JetBrains decompiler
// Type: PX.Data.InterfaceWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace PX.Data;

internal static class InterfaceWrapper
{
  private static readonly Dictionary<(System.Type, PXExtensionManager.ListOfTypes), System.Type> InterfaceWrappers = new Dictionary<(System.Type, PXExtensionManager.ListOfTypes), System.Type>();
  private static readonly (string Get, string Set) AccessorPrefix = ("get_", "set_");

  public static void ClearCaches()
  {
    lock (InterfaceWrapper.InterfaceWrappers)
      InterfaceWrapper.InterfaceWrappers.Clear();
  }

  public static List<System.Type> GetExtensionInterfaceWrappers(System.Type tgraph, List<System.Type> extensions)
  {
    Dictionary<System.Type, (System.Type, System.Type)> interfaceWrappers = InterfaceWrapper.GetInterfaceWrappers(tgraph, extensions);
    return interfaceWrappers == null ? (List<System.Type>) null : interfaceWrappers.Where<KeyValuePair<System.Type, (System.Type, System.Type)>>((Func<KeyValuePair<System.Type, (System.Type, System.Type)>, bool>) (_ => extensions.Contains(_.Value.Item1))).Select<KeyValuePair<System.Type, (System.Type, System.Type)>, System.Type>((Func<KeyValuePair<System.Type, (System.Type, System.Type)>, System.Type>) (_ => _.Value.Item1)).ToList<System.Type>();
  }

  public static Dictionary<System.Type, (System.Type, System.Type)> GetInterfaceWrappers(
    System.Type exType,
    List<System.Type> extensions)
  {
    if (!extensions.Any<System.Type>())
      return (Dictionary<System.Type, (System.Type, System.Type)>) null;
    PXExtensionManager.ListOfTypes listOfTypes = new PXExtensionManager.ListOfTypes(extensions);
    bool flag = typeof (PXGraph).IsAssignableFrom(exType);
    Dictionary<System.Type, List<MethodInfo>> dictionary1 = new Dictionary<System.Type, List<MethodInfo>>();
    Dictionary<System.Type, HashSet<System.Type>> dictionary2 = new Dictionary<System.Type, HashSet<System.Type>>();
    foreach (System.Type type1 in extensions.Where<System.Type>((Func<System.Type, bool>) (ex => ex.IsAbstract)))
    {
      System.Type extendedType = PXExtensionManager.GetExtendedType(type1);
      System.Type type2 = type1.GetCustomAttribute<PXProtectedAccessAttribute>()?.TargetType;
      if ((object) type2 == null)
        type2 = extendedType;
      System.Type type3 = type2;
      foreach (MethodInfo element in ((IEnumerable<MethodInfo>) type1.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (_ => _.IsAbstract)))
      {
        IEnumerable<Attribute> customAttributes = element.GetCustomAttributes(typeof (PXProtectedAccessAttribute));
        if (customAttributes.Count<Attribute>() == 0 && (element.Name.StartsWith(InterfaceWrapper.AccessorPrefix.Get) || element.Name.StartsWith(InterfaceWrapper.AccessorPrefix.Set)))
          customAttributes = type1.GetProperty(element.Name.Substring(4), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetCustomAttributes(typeof (PXProtectedAccessAttribute));
        foreach (Attribute attribute in customAttributes)
        {
          if (!dictionary2.ContainsKey(type1))
            dictionary2.Add(type1, new HashSet<System.Type>());
          System.Type type4 = ((PXProtectedAccessAttribute) attribute).TargetType;
          if ((object) type4 == null)
            type4 = type3;
          System.Type key = type4;
          if (!dictionary1.ContainsKey(key))
            dictionary1[key] = new List<MethodInfo>();
          dictionary1[key].Add(element);
          dictionary2[type1].Add(key);
        }
      }
    }
    Dictionary<System.Type, (System.Type, System.Type)> dictionary3 = new Dictionary<System.Type, (System.Type, System.Type)>();
    lock (InterfaceWrapper.InterfaceWrappers)
    {
      ModuleBuilder moduleBuilder = (ModuleBuilder) null;
      foreach (KeyValuePair<System.Type, List<MethodInfo>> keyValuePair in dictionary1)
      {
        (System.Type, PXExtensionManager.ListOfTypes) key = (keyValuePair.Key, listOfTypes);
        if (!InterfaceWrapper.InterfaceWrappers.ContainsKey(key))
        {
          if ((Module) moduleBuilder == (Module) null)
          {
            AssemblyName name = new AssemblyName()
            {
              Name = exType.FullName + "_InterfaceContainer"
            };
            moduleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(name.Name);
          }
          string name1 = "DynamicInterface." + keyValuePair.Key.FullName;
          TypeBuilder typeBuilder = moduleBuilder.DefineType(name1, TypeAttributes.Public | TypeAttributes.ClassSemanticsMask | TypeAttributes.Abstract);
          foreach (MethodInfo method in EnumerableExtensions.Distinct<MethodInfo, string>((IEnumerable<MethodInfo>) keyValuePair.Value, (Func<MethodInfo, string>) (m => m.ToString())))
          {
            System.Type[] array = ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (p => p.ParameterType)).ToArray<System.Type>();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Abstract, method.ReturnType, array);
            if (method.IsGenericMethod)
              InterfaceWrapper.CloneGenericMethodSignature(methodBuilder, method, array);
          }
          System.Type type = typeBuilder.CreateType();
          InterfaceWrapper.InterfaceWrappers[key] = type;
        }
        if (flag || exType == keyValuePair.Key)
          dictionary3.Add(InterfaceWrapper.InterfaceWrappers[key], (keyValuePair.Key, keyValuePair.Key));
      }
      if (dictionary2.ContainsKey(exType))
      {
        foreach (System.Type type in dictionary2[exType])
          dictionary3.Add(InterfaceWrapper.InterfaceWrappers[(type, listOfTypes)], (exType, type));
      }
    }
    return dictionary3.Count <= 0 ? (Dictionary<System.Type, (System.Type, System.Type)>) null : dictionary3;
  }

  public static void Create(
    TypeBuilder typeBuilder,
    System.Type baseType,
    List<System.Type> extensions,
    Dictionary<System.Type, (System.Type, System.Type)> interfaces)
  {
    bool flag = typeof (PXGraph).IsAssignableFrom(baseType);
    foreach (KeyValuePair<System.Type, (System.Type, System.Type)> keyValuePair in interfaces)
    {
      System.Type type1;
      (System.Type, System.Type) valueTuple1;
      EnumerableExtensions.Deconstruct<System.Type, (System.Type, System.Type)>(keyValuePair, ref type1, ref valueTuple1);
      (System.Type, System.Type) valueTuple2 = valueTuple1;
      System.Type interfaceType = type1;
      System.Type type2 = valueTuple2.Item2;
      if (type2.IsAssignableFrom(baseType))
      {
        Wrap(interfaceType, true);
      }
      else
      {
        if (flag)
        {
          int num = extensions.IndexOf(type2);
          if (num >= 0)
          {
            Wrap(interfaceType, false, new int?(num));
            continue;
          }
        }
        if (!flag)
          Wrap(interfaceType, false);
      }
    }

    void Wrap(System.Type interfaceType, bool ownMethods, int? extIndex = null)
    {
      InterfaceWrapper.Wrap(typeBuilder, baseType, interfaceType, ownMethods, extIndex);
    }
  }

  private static void Wrap(
    TypeBuilder typeBuilder,
    System.Type baseType,
    System.Type interfaceType,
    bool ownMethods,
    int? extIndex)
  {
    bool flag = false;
    bool isAbstractClass = baseType.IsAbstract && !baseType.IsInterface;
    bool isAbstractMethod = isAbstractClass && !ownMethods;
    string protectedPrefix = baseType.Name + "__";
    foreach (MethodInfo method1 in interfaceType.GetMethods())
    {
      string actualMethodName;
      System.Type[] args;
      MethodInfo method2;
      if (ownMethods)
      {
        method2 = InterfaceWrapper.ExtractExactServiceMethod(baseType, method1, protectedPrefix, out actualMethodName, out args);
      }
      else
      {
        method2 = method1;
        actualMethodName = method1.Name;
        args = ((IEnumerable<ParameterInfo>) method1.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (p => p.ParameterType)).ToArray<System.Type>();
      }
      System.Reflection.FieldInfo exactServiceField = !ownMethods || !(method2 == (MethodInfo) null) ? (System.Reflection.FieldInfo) null : InterfaceWrapper.ExtractExactServiceField(baseType, isAbstractClass, protectedPrefix, actualMethodName);
      if (!(method2 == (MethodInfo) null) || !(exactServiceField == (System.Reflection.FieldInfo) null))
      {
        if (!flag && !isAbstractMethod)
        {
          flag = true;
          typeBuilder.AddInterfaceImplementation(interfaceType);
        }
        ILGenerator body = InterfaceWrapper.DefineInterfaceMethodImplementation(typeBuilder, interfaceType, method1, actualMethodName, args, isAbstractMethod);
        if (method2 != (MethodInfo) null)
        {
          if (!method2.IsStatic)
            body.Emit(OpCodes.Ldarg_0);
          if (!ownMethods)
            InterfaceWrapper.EmitCastToInterface(body, baseType, interfaceType, extIndex);
          InterfaceWrapper.EmitMethodCall(body, method2, args.Length);
        }
        else if (exactServiceField != (System.Reflection.FieldInfo) null)
        {
          body.Emit(OpCodes.Ldarg_0);
          if (!ownMethods)
            InterfaceWrapper.EmitCastToInterface(body, baseType, interfaceType, extIndex);
          InterfaceWrapper.EmitFieldAccess(body, exactServiceField, method1.Name.StartsWith(InterfaceWrapper.AccessorPrefix.Get));
        }
      }
    }
  }

  private static void CloneGenericMethodSignature(
    MethodBuilder methodBuilder,
    MethodInfo method,
    System.Type[] args)
  {
    System.Type[] genericArguments = method.GetGenericArguments();
    GenericTypeParameterBuilder[] first = methodBuilder.DefineGenericParameters(((IEnumerable<System.Type>) genericArguments).Select<System.Type, string>((Func<System.Type, string>) (p => p.Name)).ToArray<string>());
    Dictionary<System.Type, GenericTypeParameterBuilder> replacementMap = new Dictionary<System.Type, GenericTypeParameterBuilder>();
    System.Type[] second = genericArguments;
    foreach ((GenericTypeParameterBuilder parameterBuilder, System.Type key) in ((IEnumerable<GenericTypeParameterBuilder>) first).Zip<GenericTypeParameterBuilder, System.Type, (GenericTypeParameterBuilder, System.Type)>((IEnumerable<System.Type>) second, (Func<GenericTypeParameterBuilder, System.Type, (GenericTypeParameterBuilder, System.Type)>) ((own, other) => (own, other))))
    {
      (System.Type[] typeArray, System.Type baseTypeConstraint) = EnumerableExtensions.DisuniteBy<System.Type>((IEnumerable<System.Type>) key.GetGenericParameterConstraints(), (Func<System.Type, bool>) (t => t.IsInterface)).With<(IEnumerable<System.Type>, IEnumerable<System.Type>), (System.Type[], System.Type)>((Func<(IEnumerable<System.Type>, IEnumerable<System.Type>), (System.Type[], System.Type)>) (r => (r.Affirmatives.ToArray<System.Type>(), r.Negatives.SingleOrDefault<System.Type>())));
      if (baseTypeConstraint != (System.Type) null)
        parameterBuilder.SetBaseTypeConstraint(baseTypeConstraint);
      if (typeArray.Length != 0)
        parameterBuilder.SetInterfaceConstraints(typeArray);
      parameterBuilder.SetGenericParameterAttributes(key.GenericParameterAttributes);
      replacementMap.Add(key, parameterBuilder);
    }
    methodBuilder.SetReturnType(method.ReturnType.With<System.Type, System.Type>(new Func<System.Type, System.Type>(ReplaceGeneric)));
    methodBuilder.SetParameters(((IEnumerable<System.Type>) args).Select<System.Type, System.Type>(new Func<System.Type, System.Type>(ReplaceGeneric)).ToArray<System.Type>());

    System.Type ReplaceGeneric(System.Type orig)
    {
      if (orig.IsGenericType)
        return orig.GetGenericTypeDefinition().MakeGenericType(((IEnumerable<System.Type>) orig.GetGenericArguments()).Select<System.Type, System.Type>(new Func<System.Type, System.Type>(ReplaceGeneric)).ToArray<System.Type>());
      return replacementMap.ContainsKey(orig) ? (System.Type) replacementMap[orig] : orig;
    }
  }

  private static MethodInfo ExtractExactServiceMethod(
    System.Type serviceType,
    MethodInfo method,
    string protectedPrefix,
    out string actualMethodName,
    out System.Type[] args)
  {
    actualMethodName = method.Name;
    args = ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (info => info.ParameterType)).ToArray<System.Type>();
    MethodInfo exactServiceMethod;
    if (method.IsGenericMethod)
    {
      System.Type[] pars = args;
      System.Type[] genArgs = method.GetGenericArguments();
      exactServiceMethod = ((IEnumerable<MethodInfo>) serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (gm => (gm.Name == method.Name || method.Name.StartsWith(protectedPrefix) && gm.Name == method.Name.Substring(protectedPrefix.Length)) && gm.GetGenericArguments().Length == genArgs.Length && gm.GetParameters().Length == pars.Length && EnumerableExtensions.Zip<System.Type, System.Type>(((IEnumerable<ParameterInfo>) gm.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (p => p.ParameterType)), (IEnumerable<System.Type>) pars).All<Tuple<System.Type, System.Type>>((Func<Tuple<System.Type, System.Type>, bool>) (pair =>
      {
        if (pair.Item1 == pair.Item2)
          return true;
        return pair.Item1.IsGenericParameter && pair.Item2.IsGenericParameter;
      })))).FirstOrDefault<MethodInfo>();
      if (exactServiceMethod != (MethodInfo) null && actualMethodName.StartsWith(protectedPrefix))
        actualMethodName = actualMethodName.Substring(protectedPrefix.Length);
    }
    else
    {
      exactServiceMethod = serviceType.GetMethod(method.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, args, (ParameterModifier[]) null);
      if (exactServiceMethod == (MethodInfo) null && actualMethodName.StartsWith(protectedPrefix))
      {
        actualMethodName = actualMethodName.Substring(protectedPrefix.Length);
        exactServiceMethod = serviceType.GetMethod(actualMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, args, (ParameterModifier[]) null);
      }
      if (exactServiceMethod == (MethodInfo) null)
      {
        for (System.Type baseType = serviceType.BaseType; exactServiceMethod == (MethodInfo) null && baseType != typeof (object); baseType = baseType.BaseType)
          exactServiceMethod = baseType.GetMethod(method.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, args, (ParameterModifier[]) null);
      }
    }
    return exactServiceMethod;
  }

  private static System.Reflection.FieldInfo ExtractExactServiceField(
    System.Type serviceType,
    bool isAbstractClass,
    string protectedPrefix,
    string actualMethodName)
  {
    System.Reflection.FieldInfo exactServiceField = (System.Reflection.FieldInfo) null;
    if (actualMethodName.StartsWith(InterfaceWrapper.AccessorPrefix.Get) || actualMethodName.StartsWith(InterfaceWrapper.AccessorPrefix.Set))
    {
      string name = actualMethodName.Substring(4);
      string str = actualMethodName.Substring(0, 4);
      exactServiceField = serviceType.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (exactServiceField == (System.Reflection.FieldInfo) null && !isAbstractClass && name.StartsWith(protectedPrefix))
        exactServiceField = serviceType.GetField(str + name.Substring(protectedPrefix.Length), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
    return exactServiceField;
  }

  private static ILGenerator DefineInterfaceMethodImplementation(
    TypeBuilder typeBuilder,
    System.Type interfaceType,
    MethodInfo interfaceMethod,
    string methodName,
    System.Type[] args,
    bool isAbstractMethod)
  {
    MethodBuilder methodBuilder;
    if (isAbstractMethod)
    {
      methodBuilder = typeBuilder.DefineMethod(methodName, MethodAttributes.Public | MethodAttributes.Virtual, interfaceMethod.ReturnType, args);
      if (interfaceMethod.IsGenericMethod)
        InterfaceWrapper.CloneGenericMethodSignature(methodBuilder, interfaceMethod, args);
    }
    else
    {
      methodBuilder = typeBuilder.DefineMethod($"{interfaceType.FullName}.{methodName}", MethodAttributes.Public | MethodAttributes.Virtual, interfaceMethod.ReturnType, args);
      if (interfaceMethod.IsGenericMethod)
        InterfaceWrapper.CloneGenericMethodSignature(methodBuilder, interfaceMethod, args);
      typeBuilder.DefineMethodOverride((MethodInfo) methodBuilder, interfaceMethod);
    }
    return methodBuilder.GetILGenerator();
  }

  private static void EmitCastToInterface(
    ILGenerator body,
    System.Type baseType,
    System.Type interfaceType,
    int? extIndex)
  {
    if (extIndex.HasValue)
    {
      PropertyInfo property = baseType.GetProperty("GraphExtensions", BindingFlags.Instance | BindingFlags.NonPublic);
      body.Emit(OpCodes.Call, property.GetMethod);
      body.Emit(OpCodes.Ldc_I4, extIndex.Value);
      body.Emit(OpCodes.Ldelem_Ref);
    }
    else
    {
      PropertyInfo property = baseType.GetProperty("Base", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      body.Emit(OpCodes.Call, property.GetMethod);
    }
    body.Emit(OpCodes.Castclass, interfaceType);
  }

  private static void EmitMethodCall(ILGenerator body, MethodInfo method, int argsCount)
  {
    for (int index = 0; index < argsCount; ++index)
      body.Emit(OpCodes.Ldarg, index + 1);
    if (method.IsStatic)
      body.Emit(OpCodes.Call, method);
    else
      body.Emit(OpCodes.Callvirt, method);
    body.Emit(OpCodes.Ret);
  }

  private static void EmitFieldAccess(ILGenerator body, System.Reflection.FieldInfo field, bool isGet)
  {
    if (isGet)
    {
      body.Emit(OpCodes.Ldfld, field);
    }
    else
    {
      if (field.IsInitOnly)
        throw new InvalidOperationException($"The field {field.DeclaringType.FullName}::{field.Name} is a readonly field - protected access via the set method of the namesake property is not allowed. Remove the setter.");
      body.Emit(OpCodes.Ldarg_1);
      body.Emit(OpCodes.Stfld, field);
    }
    body.Emit(OpCodes.Ret);
  }
}
