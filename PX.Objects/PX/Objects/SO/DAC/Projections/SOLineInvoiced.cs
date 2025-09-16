// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.SOLineInvoiced
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

[PXHidden]
[PXProjection(typeof (Select<ARTran>), Persistent = false)]
public class SOLineInvoiced : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARTran.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTran.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.sortOrder))]
  public virtual int? SortOrder { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (ARTran.sOOrderType))]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (ARTran.sOOrderNbr))]
  public virtual string SOOrderNbr { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.sOOrderLineNbr))]
  public virtual int? SOOrderLineNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (ARTran.sOOrderLineOperation))]
  public virtual string SOOrderLineOperation { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.soOrderSortOrder))]
  public virtual int? SOOrderSortOrder { get; set; }

  [PXDBShort(BqlField = typeof (ARTran.sOOrderLineSign))]
  public virtual short? SOOrderLineSign { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (ARTran.sOShipmentType))]
  public virtual string SOShipmentType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (ARTran.sOShipmentNbr))]
  public virtual string SOShipmentNbr { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.sOShipmentLineNbr))]
  public virtual int? SOShipmentLineNbr { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineInvoiced.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineInvoiced.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineInvoiced.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineInvoiced.sortOrder>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineInvoiced.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineInvoiced.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineInvoiced.sOOrderLineNbr>
  {
  }

  public abstract class sOOrderLineOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineInvoiced.sOOrderLineOperation>
  {
  }

  public abstract class soOrderSortOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineInvoiced.soOrderSortOrder>
  {
  }

  public abstract class sOOrderLineSign : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOLineInvoiced.sOOrderLineSign>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineInvoiced.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineInvoiced.sOShipmentNbr>
  {
  }

  public abstract class sOShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLineInvoiced.sOShipmentLineNbr>
  {
  }
}
