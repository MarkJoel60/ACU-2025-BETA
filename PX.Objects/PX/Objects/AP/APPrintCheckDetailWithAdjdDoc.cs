// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPrintCheckDetailWithAdjdDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<APPrintCheckDetail, LeftJoin<APInvoice, On<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.aPAdjustment>, And<APInvoice.docType, Equal<APPrintCheckDetail.adjdDocType>, And<APInvoice.refNbr, Equal<APPrintCheckDetail.adjdRefNbr>>>>, LeftJoin<PX.Objects.PO.POOrder, On<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.pOAdjustment>, And<PX.Objects.PO.POOrder.orderType, Equal<APPrintCheckDetail.adjdDocType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<APPrintCheckDetail.adjdRefNbr>>>>>>>), Persistent = false)]
[PXCacheName("Print Check Detail with Paid Document")]
[Serializable]
public class APPrintCheckDetailWithAdjdDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APPrintCheckDetail.adjgDocType))]
  [PXDefault]
  [APDocType.List]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string AdjgDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (APPrintCheckDetail.adjgRefNbr))]
  [PXDefault]
  public virtual string AdjgRefNbr { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (IIf<Where<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.outstandingBalance>>, APPrintCheckDetail.curyOutstandingBalance, APPrintCheckDetail.curyAdjgAmt>), typeof (Decimal))]
  public virtual Decimal? CuryAdjgAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (APPrintCheckDetail.curyAdjgDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgDiscAmt { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (APPrintCheckDetail.stubNbr))]
  public string StubNbr { get; set; }

  [PXString(15, IsKey = true, IsUnicode = true)]
  [PXDBCalced(typeof (IsNull<APInvoice.refNbr, PX.Objects.PO.POOrder.orderNbr>), typeof (string))]
  public virtual string AdjdRefNbr { get; set; }

  [PXString(3, IsKey = true)]
  [PXDBCalced(typeof (IsNull<APInvoice.docType, PX.Objects.PO.POOrder.orderType>), typeof (string))]
  [APPrintCheckDetailWithAdjdDoc.adjdDocType.List]
  public virtual string AdjdDocType { get; set; }

  [PXString(20)]
  [APPrintCheckDetailWithAdjdDoc.adjdPrintDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string AdjdPrintDocType
  {
    get => this.AdjdDocType;
    set
    {
    }
  }

  [PXString(40, IsUnicode = true)]
  [PXDBCalced(typeof (IsNull<APInvoice.invoiceNbr, PX.Objects.PO.POOrder.vendorRefNbr>), typeof (string))]
  public virtual string AdjdDocNbr { get; set; }

  [PXDate]
  [PXDBCalced(typeof (IIf<Where<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.outstandingBalance>>, APPrintCheckDetail.outstandingBalanceDate, IsNull<APInvoice.docDate, PX.Objects.PO.POOrder.orderDate>>), typeof (System.DateTime))]
  public virtual System.DateTime? AdjdDocDate { get; set; }

  [PXShort]
  [PXDBCalced(typeof (IIf<Where<APInvoice.docType, Equal<APDocType.debitAdj>>, shortMinus1, short1>), typeof (short))]
  public virtual short? AdjdInvtMult { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (IIf<Where<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.outstandingBalance>>, APPrintCheckDetail.curyOutstandingBalance, IsNull<APInvoice.curyOrigDocAmt, PX.Objects.PO.POOrder.orderTotal>>), typeof (Decimal))]
  public virtual Decimal? AdjdCuryOrigDocAmt { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Add<IsNull<APInvoice.curyDocBal, PX.Objects.PO.POOrder.curyUnprepaidTotal>, APPrintCheckDetail.curyExtraDocBal>), typeof (Decimal))]
  public virtual Decimal? AdjdCuryDocBal { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IsNull<APInvoice.suppliedByVendorID, PX.Objects.PO.POOrder.vendorID>), typeof (int))]
  public virtual int? AdjdSuppliedByVendorID { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (APPrintCheckDetail.source))]
  public string Source { get; set; }

  [PXShort]
  [PXDBCalced(typeof (Switch<Case<Where<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.aPAdjustment>>, short0, Case<Where<APPrintCheckDetail.source, Equal<AdjustmentGroupKey.AdjustmentType.pOAdjustment>>, short1>>, short2>), typeof (short))]
  public short? OrderBy { get; set; }

  public abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjgRefNbr>
  {
  }

  public abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.curyAdjgAmt>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.curyAdjgDiscAmt>
  {
  }

  public abstract class stubNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.stubNbr>
  {
  }

  public abstract class adjdRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdRefNbr>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdDocType>
  {
    public const int Length = 3;

    public class ListAttribute : LabelListAttribute
    {
      public ListAttribute()
        : base(new APDocType.ListAttribute().ValueLabelDic.Select<KeyValuePair<string, string>, ValueLabelPair>((Func<KeyValuePair<string, string>, ValueLabelPair>) (k => new ValueLabelPair(k.Key, k.Value))).Union<ValueLabelPair>((IEnumerable<ValueLabelPair>) new ValueLabelPair[2]
        {
          new ValueLabelPair("RO", "PO"),
          new ValueLabelPair("DP", "PO")
        }))
      {
      }
    }
  }

  public abstract class adjdPrintDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdPrintDocType>
  {
    public class PrintListAttribute : LabelListAttribute
    {
      public PrintListAttribute()
        : base(new APDocType.PrintListAttribute().ValueLabelDic.Select<KeyValuePair<string, string>, ValueLabelPair>((Func<KeyValuePair<string, string>, ValueLabelPair>) (k => new ValueLabelPair(k.Key, k.Value))).Union<ValueLabelPair>((IEnumerable<ValueLabelPair>) new ValueLabelPair[2]
        {
          new ValueLabelPair("RO", "PO"),
          new ValueLabelPair("DP", "PO")
        }))
      {
      }
    }
  }

  public abstract class adjdDocNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdDocNbr>
  {
  }

  public abstract class adjdDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdDocDate>
  {
  }

  public abstract class adjdInvtMult : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdInvtMult>
  {
  }

  public abstract class adjdCuryOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdCuryOrigDocAmt>
  {
  }

  public abstract class adjdCuryDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdCuryDocBal>
  {
  }

  public abstract class adjdSuppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.adjdSuppliedByVendorID>
  {
  }

  public abstract class source : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.source>
  {
  }

  public abstract class orderBy : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    APPrintCheckDetailWithAdjdDoc.orderBy>
  {
  }
}
