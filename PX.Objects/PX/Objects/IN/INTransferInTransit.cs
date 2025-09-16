// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferInTransit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>.AggregateTo<GroupBy<INTransitLineStatus.transferNbr>>))]
public class INTransferInTransit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTransitLineStatus.transferNbr))]
  public virtual 
  #nullable disable
  string TransferNbr { get; set; }

  [PXNote(BqlField = typeof (INTransitLineStatus.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  public abstract class transferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferInTransit.transferNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransferInTransit.refNoteID>
  {
  }
}
