// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.WMS.RPAScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PO.WMS;

/// <exclude />
public sealed class RPAScanHeader : PXCacheExtension<
#nullable disable
WMSScanHeader, QtyScanHeader, ScanHeader>
{
  [Location(typeof (WMSScanHeader.siteID))]
  public int? DefaultLocationID { get; set; }

  [Location(typeof (WMSScanHeader.siteID))]
  public int? PutAwayToLocationID { get; set; }

  [PXBool]
  [PXUnboundDefault(false, typeof (PX.Objects.PO.POReceipt.released))]
  [PXFormula(typeof (Default<WMSScanHeader.refNbr>))]
  public bool? Released { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public bool? ForceInsertLine { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public bool? AddTransferMode { get; set; }

  [PXInt]
  public int? PrevInventoryID { get; set; }

  [PXDecimal(6)]
  public Decimal? BaseExcessQty { get; set; }

  [PXDBString(2, IsFixed = true)]
  public string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Transfer Ref Nbr.", Enabled = false)]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.INRegister.docType, IBqlString>.IsEqual<INDocType.transfer>>.Order<By<BqlField<PX.Objects.IN.INRegister.refNbr, IBqlString>.Desc>>, PX.Objects.IN.INRegister>.SearchFor<PX.Objects.IN.INRegister.refNbr>), Filterable = true)]
  [PXUIVisible(typeof (Where<BqlOperand<ScanHeader.mode, IBqlString>.IsEqual<ReceivePutAway.PutAwayMode.value>>))]
  public string TransferRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "PO Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POOrder.orderType, IBqlString>.IsEqual<POOrderType.regularOrder>>.Order<By<BqlField<PX.Objects.PO.POOrder.orderNbr, IBqlString>.Desc>>, PX.Objects.PO.POOrder>.SearchFor<PX.Objects.PO.POOrder.orderNbr>), Filterable = true)]
  [PXUIVisible(typeof (Where<BqlOperand<ScanHeader.mode, IBqlString>.IsEqual<ReceivePutAway.ReceiveMode.value>>))]
  public string PONbr { get; set; }

  public Dictionary<int, Decimal?> LimitedInventoryIds { get; set; }

  public abstract class defaultLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RPAScanHeader.defaultLocationID>
  {
  }

  public abstract class putAwayToLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RPAScanHeader.putAwayToLocationID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RPAScanHeader.released>
  {
  }

  public abstract class forceInsertLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RPAScanHeader.forceInsertLine>
  {
  }

  public abstract class addTransferMode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RPAScanHeader.addTransferMode>
  {
  }

  public abstract class prevInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RPAScanHeader.prevInventoryID>
  {
  }

  public abstract class baseExcessQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RPAScanHeader.baseExcessQty>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RPAScanHeader.receiptType>
  {
  }

  public abstract class transferRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RPAScanHeader.transferRefNbr>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RPAScanHeader.transferRefNbr>
  {
  }

  public abstract class limitedInventoryIds : IBqlField, IBqlOperand
  {
  }
}
