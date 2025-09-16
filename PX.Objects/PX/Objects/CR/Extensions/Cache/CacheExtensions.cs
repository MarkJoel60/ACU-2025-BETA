// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Cache.CacheExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Objects.CR.MassProcess;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.Cache;

[PXInternalUseOnly]
public static class CacheExtensions
{
  public static IEnumerable<string> GetFields_ContactInfo(this PXCache cache)
  {
    return cache.GetFields_WithAttribute<PXContactInfoFieldAttribute>();
  }

  public static IEnumerable<string> GetFields_MassUpdatable(this PXCache cache)
  {
    return cache.GetFields_WithAttribute<PXMassUpdatableFieldAttribute>();
  }

  public static IEnumerable<string> GetFields_MassMergable(this PXCache cache)
  {
    return cache.GetFields_WithAttribute<PXMassMergableFieldAttribute>();
  }

  public static IEnumerable<string> GetFields_DeduplicationSearch(this PXCache cache)
  {
    return cache.GetFields_WithAttribute<PXDeduplicationSearchFieldAttribute>();
  }

  public static IEnumerable<string> GetFields_DeduplicationSearch(
    this PXCache cache,
    string validationType)
  {
    return cache.GetFields_WithAttribute<PXDeduplicationSearchFieldAttribute>((Func<PXDeduplicationSearchFieldAttribute, bool>) (attribute => ((IEnumerable<string>) attribute.ValidationTypes).Any<string>((Func<string, bool>) (type => type == validationType))));
  }

  public static IEnumerable<string> GetFields_Udf(this PXCache cache)
  {
    return UDFHelper.GetUDFFields(cache.Graph).Select<KeyValueHelper.ScreenAttribute, string>((Func<KeyValueHelper.ScreenAttribute, string>) (attr => "Attribute" + attr.AttributeID));
  }

  public static (string FieldName, TAttribute Attribute) GetField_WithAttribute<TAttribute>(
    this PXCache cache)
  {
    return ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (field => cache.GetAttributesReadonly(field).OfType<TAttribute>().Any<TAttribute>())).Select<string, (string, TAttribute)>((Func<string, (string, TAttribute)>) (_ => (_, cache.GetAttributesReadonly(_).OfType<TAttribute>().Last<TAttribute>()))).FirstOrDefault<(string, TAttribute)>();
  }

  public static IEnumerable<string> GetFields_WithAttribute<TAttribute>(this PXCache cache)
  {
    return ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (field => cache.GetAttributesReadonly(field).OfType<TAttribute>().Any<TAttribute>()));
  }

  public static IEnumerable<string> GetFields_WithAttribute<TAttribute>(
    this PXCache cache,
    Func<TAttribute, bool> predicate)
  {
    return ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (field => cache.GetAttributesReadonly(field).OfType<TAttribute>().Any<TAttribute>(predicate)));
  }

  public static TAttribute GetFieldAttribute<TAttribute>(this PXCache cache, string field)
  {
    return cache.GetAttributesReadonly(field).OfType<TAttribute>().FirstOrDefault<TAttribute>();
  }
}
