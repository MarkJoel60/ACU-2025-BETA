// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Extensions.ObjectExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.CN.Common.Extensions;

public static class ObjectExtensions
{
  public static T[] CreateArray<T>(this T item)
  {
    return new T[1]{ item };
  }

  public static T SingleOrNull<T>(this IEnumerable<T> enumerable)
  {
    List<T> list = enumerable.ToList<T>();
    // ISSUE: reference to a compiler-generated field
    if (ObjectExtensions.\u003C\u003Eo__1<T>.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ObjectExtensions.\u003C\u003Eo__1<T>.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, T>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (T), typeof (ObjectExtensions)));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ObjectExtensions.\u003C\u003Eo__1<T>.\u003C\u003Ep__0.Target((CallSite) ObjectExtensions.\u003C\u003Eo__1<T>.\u003C\u003Ep__0, list.IsSingleElement<T>() ? (object) list.Single<T>() : (object) null);
  }

  public static T GetPropertyValue<T>(this object entity, string propertyName)
  {
    object obj = entity.GetType().GetProperty(propertyName)?.GetValue(entity, (object[]) null);
    return obj != null ? (T) obj : default (T);
  }

  public static T Cast<T>(this object entity)
  {
    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity));
  }

  public static object Cast(this object entity, Type resultType)
  {
    return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(entity), resultType);
  }
}
