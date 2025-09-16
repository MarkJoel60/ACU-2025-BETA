// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.FieldStateExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR.Extensions;

public static class FieldStateExtensions
{
  private static readonly MethodInfo CloneMethod = typeof (object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

  public static PXFieldState Copy(PXFieldState originalObject)
  {
    return FieldStateExtensions.InternalCopy((object) originalObject, (IDictionary<object, object>) new Dictionary<object, object>((IEqualityComparer<object>) new ReferenceEqualityComparer())) as PXFieldState;
  }

  private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
  {
    if (originalObject == null)
      return (object) null;
    System.Type type = originalObject.GetType();
    if (type.IsPrimitive())
      return originalObject;
    if (visited.ContainsKey(originalObject))
      return visited[originalObject];
    if (typeof (Delegate).IsAssignableFrom(type))
      return (object) null;
    object cloneObject = FieldStateExtensions.CloneMethod.Invoke(originalObject, (object[]) null);
    if (type.IsArray && !type.GetElementType().IsPrimitive())
    {
      Array clonedArray = (Array) cloneObject;
      clonedArray.ForEach((Action<Array, int[]>) ((array, indices) => array.SetValue(FieldStateExtensions.InternalCopy(clonedArray.GetValue(indices), visited), indices)));
    }
    visited.Add(originalObject, cloneObject);
    FieldStateExtensions.CopyFields(originalObject, visited, cloneObject, type);
    FieldStateExtensions.RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, type);
    return cloneObject;
  }

  private static void RecursiveCopyBaseTypePrivateFields(
    object originalObject,
    IDictionary<object, object> visited,
    object cloneObject,
    System.Type typeToReflect)
  {
    if (!(typeToReflect.BaseType != (System.Type) null))
      return;
    FieldStateExtensions.RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
    FieldStateExtensions.CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, (Func<FieldInfo, bool>) (info => info.IsPrivate));
  }

  private static void CopyFields(
    object originalObject,
    IDictionary<object, object> visited,
    object cloneObject,
    System.Type typeToReflect,
    BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy,
    Func<FieldInfo, bool> filter = null)
  {
    foreach (FieldInfo field in typeToReflect.GetFields(bindingFlags))
    {
      if ((filter == null || filter(field)) && !field.FieldType.IsPrimitive())
      {
        object obj = FieldStateExtensions.InternalCopy(field.GetValue(originalObject), visited);
        field.SetValue(cloneObject, obj);
      }
    }
  }

  private static bool IsPrimitive(this System.Type type)
  {
    return type == typeof (string) || type.IsValueType & type.IsPrimitive;
  }
}
