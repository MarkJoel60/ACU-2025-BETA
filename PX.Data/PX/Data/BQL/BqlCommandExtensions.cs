// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlCommandExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

public static class BqlCommandExtensions
{
  internal static void RemoveCurrentAndOptional(
    List<System.Type> decomposedOn,
    Dictionary<string, System.Type> views)
  {
    for (int index = decomposedOn.Count - 1; index >= 0; --index)
    {
      System.Type type = decomposedOn[index];
      System.Type nextType = index < decomposedOn.Count - 1 ? decomposedOn[index + 1] : (System.Type) null;
      if (IsCurrentOrOptionalKey(type, nextType))
        decomposedOn.RemoveAt(index);
      else if (IsFbqlCurrentOrOptionalKey(type, nextType))
      {
        decomposedOn.RemoveAt(index);
        decomposedOn.RemoveAt(index + 1);
      }
    }

    string GetViewName(System.Type type)
    {
      return views.FirstOrDefault<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (c =>
      {
        System.Type declaringType = type.DeclaringType;
        return (object) declaringType != null && declaringType.IsAssignableFrom(c.Value);
      })).Key;
    }

    bool IsCurrentOrOptionalKey(System.Type type, System.Type nextType)
    {
      return nextType != (System.Type) null && typeof (IBqlField).IsAssignableFrom(nextType) && type.IsCurrentOrOptionalParameter() && !string.IsNullOrEmpty(GetViewName(nextType));
    }

    bool IsFbqlCurrentOrOptionalKey(System.Type type, System.Type nextType)
    {
      return nextType != (System.Type) null && typeof (IBqlField).IsAssignableFrom(nextType) && type.IsFbqlCurrentOrOptionalParameter() && !string.IsNullOrEmpty(GetViewName(nextType));
    }
  }

  internal static bool HasMatchParameter(IEnumerable<System.Type> decomposedOn)
  {
    return decomposedOn.Any<System.Type>((Func<System.Type, bool>) (t => t.IsMatchParameter()));
  }

  internal static bool HasUnknownViews(IEnumerable<System.Type> decomposedOn, params System.Type[] views)
  {
    return decomposedOn.Any<System.Type>((Func<System.Type, bool>) (c => typeof (IBqlField).IsAssignableFrom(c) && ((IEnumerable<System.Type>) views).All<System.Type>((Func<System.Type, bool>) (v =>
    {
      System.Type declaringType = c.DeclaringType;
      return (object) declaringType == null || !declaringType.IsAssignableFrom(v);
    }))));
  }

  internal static bool IsCurrentOrOptionalParameter(this System.Type type)
  {
    return typeof (Current<>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (Current2<>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (Optional<>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (Optional2<>).IsAssignableFrom(type.UnderlyingSystemType);
  }

  internal static bool IsFbqlCurrentOrOptionalParameter(this System.Type type)
  {
    return typeof (BqlField<,>.FromCurrent).IsAssignableFrom(type.UnderlyingSystemType) || typeof (BqlField<,>.FromCurrent.NoDefault).IsAssignableFrom(type.UnderlyingSystemType) || typeof (BqlField<,>.FromCurrent.Value).IsAssignableFrom(type.UnderlyingSystemType) || typeof (BqlField<,>.AsOptional).IsAssignableFrom(type.UnderlyingSystemType);
  }

  internal static bool IsMatchParameter(this System.Type type)
  {
    return typeof (Match<>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (Match<,>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (MatchUserFor<>).IsAssignableFrom(type.UnderlyingSystemType) || typeof (MatchUser).IsAssignableFrom(type.UnderlyingSystemType);
  }
}
