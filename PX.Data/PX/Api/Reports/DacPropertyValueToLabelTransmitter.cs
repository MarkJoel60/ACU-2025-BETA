// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.DacPropertyValueToLabelTransmitter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Api.Reports;

internal static class DacPropertyValueToLabelTransmitter
{
  private static readonly ConcurrentDictionary<string, System.Type> DacsCache = new ConcurrentDictionary<string, System.Type>();
  private static readonly Lazy<Assembly> DacAssembly = new Lazy<Assembly>((Func<Assembly>) (() => Assembly.Load("PX.Objects")));
  private static readonly ConcurrentDictionary<System.Type, PXIntListAttribute> DacIntListAttributesCache = new ConcurrentDictionary<System.Type, PXIntListAttribute>();
  private static readonly ConcurrentDictionary<System.Type, PXStringListAttribute> DacStringListAttributeCache = new ConcurrentDictionary<System.Type, PXStringListAttribute>();

  public static string GetLabel(string label, string dacName, string dacField) => "";

  private static T GetAttribute<T>(System.Type target, string property) where T : Attribute
  {
    return ((IEnumerable<object>) target.GetProperty(property).GetCustomAttributes(typeof (T), false)).FirstOrDefault<object>() as T;
  }

  public static string GetValue(string value, string dacName, string dacProperty)
  {
    System.Type orAdd1 = DacPropertyValueToLabelTransmitter.DacsCache.GetOrAdd(dacName, (Func<string, System.Type>) (name =>
    {
      System.Type type = DacPropertyValueToLabelTransmitter.DacAssembly.Value.GetType(name);
      return (object) type != null ? type : DacPropertyValueToLabelTransmitter.DacAssembly.Value.GetType($"PX.Objects.{name.Substring(0, 2)}.{name}");
    }));
    if (orAdd1 == (System.Type) null)
      return value;
    PXStringListAttribute orAdd2 = DacPropertyValueToLabelTransmitter.DacStringListAttributeCache.GetOrAdd(orAdd1, (Func<System.Type, PXStringListAttribute>) (type => DacPropertyValueToLabelTransmitter.GetAttribute<PXStringListAttribute>(type, dacProperty)));
    if (orAdd2 == null || orAdd2.ValueLabelDic == null)
    {
      PXIntListAttribute orAdd3 = DacPropertyValueToLabelTransmitter.DacIntListAttributesCache.GetOrAdd(orAdd1, (Func<System.Type, PXIntListAttribute>) (type => DacPropertyValueToLabelTransmitter.GetAttribute<PXIntListAttribute>(type, dacProperty)));
      return orAdd3 == null || orAdd3.ValueLabelDic == null || !orAdd3.ValueLabelDic.ContainsValue(value) ? value : orAdd3.ValueLabelDic.First<KeyValuePair<int, string>>((Func<KeyValuePair<int, string>, bool>) (pair => pair.Value.ToString() == value)).Key.ToString();
    }
    return !orAdd2.ValueLabelDic.ContainsValue(value) ? value : orAdd2.ValueLabelDic.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (pair => pair.Value == value)).Key;
  }
}
