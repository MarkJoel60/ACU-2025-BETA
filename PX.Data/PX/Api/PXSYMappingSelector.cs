// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYMappingSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

public class PXSYMappingSelector : PXCustomSelectorAttribute
{
  public PXSYMappingSelector()
    : base(typeof (SYMapping.name))
  {
  }

  public PXSYMappingSelector(System.Type t)
    : base(t)
  {
  }

  protected virtual IEnumerable GetRecords()
  {
    return (IEnumerable) PXSYMappingSelector.GetMappings<SYMapping>(new PXView(this._Graph, true, PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.mappingType, Equal<Current<SYMapping.mappingType>>>>.Config>.GetCommand()));
  }

  public static IEnumerable<T> GetMappings<T>(PXView selector, params object[] args) where T : SYMapping, new()
  {
    foreach (T mapping in selector.SelectMulti(args))
      yield return mapping;
  }
}
