// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.ObjectExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class ObjectExtensions
{
  public static bool IsComplex(this object obj)
  {
    return !obj.GetType().IsPrimitive && obj.GetType() != typeof (string) && obj.GetType().IsAssignableFrom(typeof (Decimal)) && obj.GetType().IsAssignableFrom(typeof (DateTime)) && obj.GetType().IsAssignableFrom(typeof (Guid));
  }

  public static TObject[] SingleToArray<TObject>(this TObject obj)
  {
    return new TObject[1]{ obj };
  }

  public static TObject[] SingleToArrayOrNull<TObject>(this TObject obj)
  {
    if ((object) obj == null)
      return (TObject[]) null;
    return new TObject[1]{ obj };
  }

  public static List<TObject> SingleToListOrNull<TObject>(this TObject obj)
  {
    if ((object) obj == null)
      return (List<TObject>) null;
    return new List<TObject>() { obj };
  }

  public static object[] SingleToObjectArray<TObject>(this TObject obj, bool dontCreateForNull = true)
  {
    if ((object) obj == null & dontCreateForNull)
      return (object[]) null;
    return new object[1]{ (object) obj };
  }

  public static List<TObject> SingleToList<TObject>(this TObject obj)
  {
    return new List<TObject>() { obj };
  }
}
