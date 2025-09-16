// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODetFSSODetSplit
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSSODet, LeftJoin<FSSODetSplit, On<FSSODetSplit.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSSODetSplit.refNbr, Equal<FSSODet.refNbr>, And<FSSODetSplit.lineNbr, Equal<FSSODet.lineNbr>>>>>>))]
[Serializable]
public class FSSODetFSSODetSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof (FSSODet.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "", BqlField = typeof (FSSODet.refNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FSSODet.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  /// <exclude />
  [PXDBInt(IsKey = true, BqlField = typeof (FSSODetSplit.splitLineNbr))]
  [PXUIField(DisplayName = "Allocation ID")]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSSODetSplit.inventoryID))]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual int? InventoryID { get; set; }

  [PXDBLong(IsImmutable = true, BqlField = typeof (FSSODetSplit.planID))]
  [PXUIField(DisplayName = "Plan ID")]
  public virtual long? PlanID { get; set; }

  /// <exclude />
  [PXDBString(2, IsFixed = true, BqlField = typeof (FSSODetSplit.pOType))]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  public virtual string POType { get; set; }

  /// <exclude />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSSODetSplit.pONbr))]
  [PXDBDefault(typeof (PX.Objects.PO.POOrder.orderNbr))]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  public virtual string PONbr { get; set; }

  /// <exclude />
  [PXDBInt(BqlField = typeof (FSSODetSplit.pOLineNbr))]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  public virtual int? POLineNbr { get; set; }

  [PXDBPriceCost(BqlField = typeof (FSSODet.unitPrice))]
  [PXUIField(DisplayName = "Base Unit Price")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBString(BqlField = typeof (FSSODet.uOM))]
  [PXUIField(DisplayName = "UOM")]
  public virtual string UOM { get; set; }

  public class PK : 
    PrimaryKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.srvOrdType, FSSODetFSSODetSplit.refNbr, FSSODetFSSODetSplit.lineNbr>
  {
    public static FSSODetFSSODetSplit Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSSODetFSSODetSplit) PrimaryKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.srvOrdType, FSSODetFSSODetSplit.refNbr, FSSODetFSSODetSplit.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.srvOrdType, FSSODetFSSODetSplit.refNbr>
    {
    }

    public class ServiceOrderLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.ForeignKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.srvOrdType, FSSODetFSSODetSplit.refNbr, FSSODetFSSODetSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.inventoryID>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<FSSODetFSSODetSplit>.By<FSSODetFSSODetSplit.planID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetFSSODetSplit.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetFSSODetSplit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetFSSODetSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSSODetFSSODetSplit.splitLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetFSSODetSplit.inventoryID>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSODetFSSODetSplit.planID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetFSSODetSplit.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetFSSODetSplit.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetFSSODetSplit.pOLineNbr>
  {
  }

  public abstract class unitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSODetFSSODetSplit.unitPrice>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetFSSODetSplit.uOM>
  {
  }
}
