// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorPriceUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[Serializable]
public class POVendorPriceUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _PendingDate;

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Max. Pending Date")]
  public virtual DateTime? PendingDate
  {
    get => this._PendingDate;
    set => this._PendingDate = value;
  }

  public abstract class pendingDate : 
    BqlType<IBqlDateTime, DateTime>.Field<
    #nullable disable
    POVendorPriceUpdate.pendingDate>
  {
  }
}
