// Decompiled with JetBrains decompiler
// Type: PX.CS.SizeTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.CS;

[PXDBShort]
[PXDefault(1)]
[SizeTypeList]
[PXUIField(DisplayName = "Size Type")]
public class SizeTypeAttribute : PXAggregateAttribute
{
  public bool Visible
  {
    get
    {
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
          return ((PXUIFieldAttribute) attribute).Visible;
      }
      return true;
    }
    set
    {
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
        {
          ((PXUIFieldAttribute) attribute).Visible = value;
          break;
        }
      }
    }
  }

  public string DisplayName
  {
    get
    {
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
          return ((PXUIFieldAttribute) attribute).DisplayName;
      }
      return this._FieldName;
    }
    set
    {
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
        {
          ((PXUIFieldAttribute) attribute).DisplayName = value;
          break;
        }
      }
    }
  }
}
