// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AccountGroupAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Displays all AccountGroups sorted by SortOrder.</summary>
[PXDBInt]
[PXUIField]
public class AccountGroupAttribute : PXEntityAttribute
{
  public const string DimensionName = "ACCGROUP";
  protected Type showGLAccountGroups;

  public AccountGroupAttribute()
    : this(typeof (Where<PMAccountGroup.groupID, IsNotNull>))
  {
  }

  public AccountGroupAttribute(Type WhereType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("ACCGROUP", BqlCommand.Compose(new Type[4]
    {
      typeof (Search<,,>),
      typeof (PMAccountGroup.groupID),
      WhereType,
      typeof (OrderBy<Asc<PMAccountGroup.sortOrder>>)
    }), typeof (PMAccountGroup.groupCD), new Type[4]
    {
      typeof (PMAccountGroup.groupCD),
      typeof (PMAccountGroup.description),
      typeof (PMAccountGroup.type),
      typeof (PMAccountGroup.isActive)
    })
    {
      DescriptionField = typeof (PMAccountGroup.description),
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
