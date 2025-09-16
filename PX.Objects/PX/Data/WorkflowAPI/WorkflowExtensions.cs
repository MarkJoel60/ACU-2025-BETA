// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.WorkflowExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data.WorkflowAPI;

public static class WorkflowExtensions
{
  /// <summary>
  /// Get a new instance of an anonymous class, that contains <see cref="T:PX.Data.WorkflowAPI.BoundedTo`2.Condition" />s,
  /// but where condition names are taken from their properties' names.
  /// </summary>
  /// <param name="conditionPack">An instance of an anonymous class, that contains only properties of <see cref="T:PX.Data.WorkflowAPI.BoundedTo`2.Condition" /> type.</param>
  public static T AutoNameConditions<T>(this T conditionPack) where T : class
  {
    if (!typeof (T).IsDefined(typeof (CompilerGeneratedAttribute), false))
      throw new InvalidOperationException("Only instances of anonymous types are allowed");
    return (T) Activator.CreateInstance(typeof (T), ((IEnumerable<PropertyInfo>) typeof (T).GetProperties()).Select<PropertyInfo, (object, MethodInfo, string, MethodInfo)>((Func<PropertyInfo, (object, MethodInfo, string, MethodInfo)>) (p => (p.GetValue((object) (T) conditionPack), p.PropertyType.GetMethod("WithSharedName"), p.Name, p.PropertyType.GetProperty("Name").GetMethod))).Select<(object, MethodInfo, string, MethodInfo), object>((Func<(object, MethodInfo, string, MethodInfo), object>) (p =>
    {
      if (p.GetName.Invoke(p.Target, Array.Empty<object>()) != null)
        return p.Target;
      return p.WithSharedName.Invoke(p.Target, new object[1]
      {
        (object) p.Name
      });
    })).ToArray<object>());
  }

  [PXHidden]
  private class Table : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
