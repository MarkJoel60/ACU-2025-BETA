// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryAllocDetEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryAllocDetEnqResult : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IQtyPlanned
{
  protected 
  #nullable disable
  string _Module;
  protected string _QADocType;
  protected Guid? _RefNoteID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected bool? _LocNotAvailable;
  protected int? _SiteID;
  protected string _LotSerialNbr;
  protected bool? _Expired;
  protected string _AllocationType;
  protected DateTime? _PlanDate;
  protected Decimal? _PlanQty;
  protected bool? _Reverse;
  protected long? _PlanID;
  protected int? _BAccountID;
  protected string _AcctName;
  protected int? _GridLineNbr;

  [PXString(2)]
  [PXUIField]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXString(100, IsUnicode = true)]
  [PXUIField]
  [PXStringList]
  public virtual string QADocType
  {
    get => this._QADocType;
    set => this._QADocType = value;
  }

  [PXGuid]
  [PXUIField]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXString]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [SubItem]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Location(typeof (InventoryAllocDetEnqResult.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? LocNotAvailable
  {
    get => this._LocNotAvailable;
    set => this._LocNotAvailable = value;
  }

  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? Expired
  {
    get => this._Expired;
    set => this._Expired = value;
  }

  [PXString(50)]
  [PXUIField]
  [QtyAllocType.List]
  public virtual string AllocationType
  {
    get => this._AllocationType;
    set => this._AllocationType = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Allocation Date")]
  public virtual DateTime? PlanDate
  {
    get => this._PlanDate;
    set => this._PlanDate = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? PlanQty
  {
    get => this._PlanQty;
    set => this._PlanQty = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "Reverse")]
  public virtual bool? Reverse
  {
    get => this._Reverse;
    set => this._Reverse = value;
  }

  [PXDBLong]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBInt]
  public int? ExcludePlanLevel { get; set; }

  [PXBool]
  public virtual bool? Hold { get; set; }

  [PXInt]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField(DisplayName = "Account ID")]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name")]
  public virtual string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  /// <exclude />
  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Cost Layer Type", FieldClass = "CostLayerType")]
  [PX.Objects.IN.CostLayerType.List]
  public virtual string CostLayerType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr
  {
    get => this._GridLineNbr;
    set => this._GridLineNbr = value;
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.module>
  {
  }

  public abstract class qADocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.qADocType>
  {
  }

  public abstract class refNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.refNoteID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.refNbr>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.subItemID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.locationID>
  {
  }

  public abstract class locNotAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.locNotAvailable>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.siteID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.lotSerialNbr>
  {
  }

  public abstract class expired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.expired>
  {
  }

  public abstract class allocationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.allocationType>
  {
  }

  public abstract class planDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.planDate>
  {
  }

  public abstract class planQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.planQty>
  {
  }

  public abstract class reverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.reverse>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.planID>
  {
  }

  public abstract class excludePlanLevel : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.excludePlanLevel>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryAllocDetEnqResult.hold>
  {
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.bAccountID>
  {
  }

  public abstract class acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.acctName>
  {
  }

  /// <exclude />
  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.costLayerType>
  {
  }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqResult.gridLineNbr>
  {
  }
}
