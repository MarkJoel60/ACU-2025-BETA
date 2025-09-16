// Decompiled with JetBrains decompiler
// Type: PX.Data.PXScreenToSiteMapAddHelperBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data;

public abstract class PXScreenToSiteMapAddHelperBase
{
  public abstract 
  #nullable disable
  IEnumerable<PX.SM.SiteMap> Cached { get; }

  public abstract bool IsSiteMapAltered { get; }

  public event PXRowPersisted RowPersisted;

  protected void RaiseRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXRowPersisted rowPersisted = this.RowPersisted;
    if (rowPersisted == null)
      return;
    rowPersisted(sender, e);
  }

  [PXHidden]
  [PXPortalMapTableName]
  [Serializable]
  public class PortalMap : PX.SM.SiteMap
  {
  }

  protected sealed class PrevScreenIdHelper : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public int? IntValue { get; set; }

    public abstract class intValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXScreenToSiteMapAddHelperBase.PrevScreenIdHelper.intValue>
    {
    }
  }
}
