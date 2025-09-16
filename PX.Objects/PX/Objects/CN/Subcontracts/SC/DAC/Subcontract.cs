// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.DAC.Subcontract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CN.Subcontracts.SC.Descriptor.Attributes;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.SC.DAC;

[PXPrimaryGraph(new Type[] {typeof (SubcontractEntry)}, new Type[] {typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<Subcontract.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<Subcontract.orderNbr>>>>>)})]
[PXBreakInheritance]
[PXCacheName("Subcontract")]
[PXProjection(typeof (SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POOrder.orderType, IBqlString>.IsEqual<POOrderType.regularSubcontract>>))]
public class Subcontract : PX.Objects.PO.POOrder
{
  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.OrderDesc" />
  [PXMergeAttributes]
  [PXFieldDescription]
  public override 
  #nullable disable
  string OrderDesc { get; set; }

  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<PX.Objects.PO.POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>>>, OrderBy<Desc<PX.Objects.PO.POOrder.orderNbr>>>))]
  [SubcontractSearchable]
  public override Guid? NoteID { get; set; }

  public new abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Subcontract.orderNbr>
  {
  }

  public new abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Subcontract.orderType>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Subcontract.vendorID>
  {
  }

  public new abstract class orderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Subcontract.orderTotal>
  {
  }

  public new abstract class orderDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Subcontract.orderDesc>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Subcontract.noteID>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Subcontract.hasMultipleProjects>
  {
  }
}
