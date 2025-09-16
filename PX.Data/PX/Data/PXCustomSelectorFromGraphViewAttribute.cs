// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomSelectorFromGraphViewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;

#nullable disable
namespace PX.Data;

public class PXCustomSelectorFromGraphViewAttribute : PXCustomSelectorAttribute
{
  private readonly string _viewName;

  public PXCustomSelectorFromGraphViewAttribute(System.Type selectorField, string viewName)
    : base(selectorField)
  {
    this._viewName = viewName;
  }

  protected virtual IEnumerable GetRecords()
  {
    return (IEnumerable) this._Graph.Views[this._viewName].SelectMulti();
  }
}
