// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitTranSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select<INTranSplit>), Persistent = true)]
[PXCacheName("IN Kit Split")]
public class INKitTranSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanINSource,
  IItemPlanSource
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; } = new bool?(false);

  [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (INTranSplit.docType))]
  [PXDefault(typeof (INKitRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTranSplit.origModule))]
  [PXDBDefault(typeof (INKitRegister.origModule))]
  public virtual string OrigModule { get; set; }

  [PXDBString(3, BqlField = typeof (INTranSplit.tranType))]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<INKitTranSplit.docType, Equal<INDocType.disassembly>>, INTranType.disassembly>, INTranType.assembly>))]
  public virtual string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTranSplit.refNbr))]
  [PXDBDefault(typeof (INKitRegister.refNbr))]
  [PXParent(typeof (Select<INKitRegister, Where<INKitRegister.docType, Equal<Current<INKitTranSplit.docType>>, And<INKitRegister.refNbr, Equal<Current<INKitTranSplit.refNbr>>, And<INKitRegister.kitLineNbr, Equal<Current<INKitTranSplit.lineNbr>>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INTranSplit.lineNbr))]
  [PXDefault(typeof (INKitRegister.kitLineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INTranSplit.splitLineNbr))]
  [PXLineNbr(typeof (INKitRegister.lineCntr))]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBDate(BqlField = typeof (INTranSplit.tranDate))]
  [PXDBDefault(typeof (INKitRegister.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  [PXDBShort(BqlField = typeof (INTranSplit.invtMult))]
  [PXDefault(typeof (INKitRegister.invtMult))]
  public virtual short? InvtMult { get; set; }

  [StockItem(Visible = false, BqlField = typeof (INTranSplit.inventoryID))]
  [PXDefault(typeof (INKitRegister.kitInventoryID))]
  public virtual int? InventoryID { get; set; }

  public bool? IsStockItem
  {
    get => new bool?(true);
    set
    {
    }
  }

  [SubItem(typeof (INKitTranSplit.inventoryID), BqlField = typeof (INTranSplit.subItemID))]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [Site(BqlField = typeof (INTranSplit.siteID))]
  [PXDefault(typeof (INKitRegister.siteID))]
  public virtual int? SiteID { get; set; }

  [LocationAvail(typeof (INKitTranSplit.inventoryID), typeof (INKitTranSplit.subItemID), typeof (CostCenter.freeStock), typeof (INKitTranSplit.siteID), typeof (INKitTranSplit.tranType), typeof (INKitTranSplit.invtMult), BqlField = typeof (INTranSplit.locationID))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [INLotSerialNbr(typeof (INKitTranSplit.inventoryID), typeof (INKitTranSplit.subItemID), typeof (INKitTranSplit.locationID), typeof (INKitRegister.lotSerialNbr), typeof (CostCenter.freeStock), BqlField = typeof (INTranSplit.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXString(10, IsUnicode = true)]
  public virtual string LotSerClassID { get; set; }

  [PXString(30, IsUnicode = true)]
  public virtual string AssignedNbr { get; set; }

  [INExpireDate(typeof (INKitTranSplit.inventoryID), BqlField = typeof (INTranSplit.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBBool(BqlField = typeof (INTranSplit.released))]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [INUnit(typeof (INKitTranSplit.inventoryID), DisplayName = "UOM", Enabled = false, BqlField = typeof (INTranSplit.uOM))]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INKitTranSplit.uOM), typeof (INKitTranSplit.baseQty), InventoryUnitType.BaseUnit, BqlField = typeof (INTranSplit.qty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBQuantity(BqlField = typeof (INTranSplit.baseQty))]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBLong(BqlField = typeof (INTranSplit.planID), IsImmutable = true)]
  public virtual long? PlanID { get; set; }

  [PXFormula(typeof (Selector<INKitTranSplit.locationID, INLocation.projectID>))]
  [PXInt]
  public virtual int? ProjectID { get; set; }

  [PXFormula(typeof (Selector<INKitTranSplit.locationID, INLocation.taskID>))]
  [PXInt]
  public virtual int? TaskID { get; set; }

  [PXDBBool(BqlField = typeof (INTranSplit.isIntercompany))]
  [PXDefault(false)]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBCreatedByID(BqlField = typeof (INTranSplit.createdByID))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (INTranSplit.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (INTranSplit.createdDateTime))]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (INTranSplit.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INTranSplit.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (INTranSplit.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public string TransferType
  {
    get => (string) null;
    set
    {
    }
  }

  public string OrigPlanType => (string) null;

  public string SOLineType => (string) null;

  public string POLineType => (string) null;

  public int? ToSiteID => new int?();

  public int? ToLocationID => new int?();

  public bool? IsFixedInTransit => new bool?();

  public INKitTranSplit()
  {
  }

  public INKitTranSplit(string LotSerialNbr, string AssignedNbr, string LotSerClassID)
    : this()
  {
    this.LotSerialNbr = LotSerialNbr;
    this.AssignedNbr = AssignedNbr;
    this.LotSerClassID = LotSerClassID;
  }

  public static INKitTranSplit FromINKitRegister(INKitRegister item)
  {
    return new INKitTranSplit()
    {
      DocType = item.DocType,
      TranType = item.TranType,
      RefNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      SplitLineNbr = new int?(1),
      InventoryID = item.KitInventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      LotSerialNbr = item.LotSerialNbr,
      ExpireDate = item.ExpireDate,
      Qty = item.Qty,
      UOM = item.UOM,
      TranDate = item.TranDate,
      BaseQty = item.BaseQty,
      InvtMult = item.InvtMult,
      Released = item.Released
    };
  }

  public class PK : 
    PrimaryKeyOf<INKitTranSplit>.By<INKitTranSplit.docType, INKitTranSplit.refNbr, INKitTranSplit.lineNbr, INKitTranSplit.splitLineNbr>
  {
    public static INKitTranSplit Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (INKitTranSplit) PrimaryKeyOf<INKitTranSplit>.By<INKitTranSplit.docType, INKitTranSplit.refNbr, INKitTranSplit.lineNbr, INKitTranSplit.splitLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Register : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.docType, INKitTranSplit.refNbr>
    {
    }

    public class Tran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.docType, INKitTranSplit.refNbr, INKitTranSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.locationID>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.planID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<INTranSplit>.By<INKitTranSplit.inventoryID, INKitTranSplit.subItemID, INKitTranSplit.siteID, INKitTranSplit.locationID, INKitTranSplit.lotSerialNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitTranSplit.selected>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.docType>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.origModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.splitLineNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INKitTranSplit.tranDate>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INKitTranSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitTranSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.assignedNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INKitTranSplit.expireDate>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitTranSplit.released>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitTranSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitTranSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitTranSplit.baseQty>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INKitTranSplit.planID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitTranSplit.taskID>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitTranSplit.isIntercompany>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitTranSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitTranSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitTranSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitTranSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitTranSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitTranSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitTranSplit.Tstamp>
  {
  }
}
