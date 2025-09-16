// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INComponent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Deferred Revenue Components")]
[Serializable]
public class INComponent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _ComponentID;
  protected 
  #nullable disable
  string _DeferredCode;
  protected Decimal? _DefaultTerm;
  protected string _DefaultTermUOM;
  protected Decimal? _Percentage;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _FixedAmt;
  protected string _AmtOption;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (INComponent.FK.InventoryItem))]
  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [Inventory(Filterable = true, IsKey = true, DisplayName = "Inventory ID")]
  [PXForeignReference(typeof (INComponent.FK.ComponentInventoryItem))]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXRestrictor(typeof (Where<DRDeferredCode.multiDeliverableArrangement, NotEqual<boolTrue>>), "Multi-Deliverable Arrangement codes can't be used on components", new Type[] {})]
  [PXSelector(typeof (DRDeferredCode.deferredCodeID))]
  public virtual string DeferredCode
  {
    get => this._DeferredCode;
    set => this._DeferredCode = value;
  }

  [PXDBDecimal(0, MinValue = 0.0, MaxValue = 10000.0)]
  [PXUIField(DisplayName = "Default Term")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DefaultTerm
  {
    get => this._DefaultTerm;
    set => this._DefaultTerm = value;
  }

  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Default Term UOM")]
  [DRTerms.UOMList]
  [PXDefault("Y")]
  public virtual string DefaultTermUOM
  {
    get => this._DefaultTermUOM;
    set => this._DefaultTermUOM = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Default Term", Enabled = false)]
  public virtual bool? OverrideDefaultTerm { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Percentage")]
  [PXFormula(null, typeof (SumCalc<InventoryItem.totalPercentage>))]
  public virtual Decimal? Percentage
  {
    get => this._Percentage;
    set => this._Percentage = value;
  }

  [PXDefault]
  [Account]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [PXDefault]
  [SubAccount(typeof (INComponent.salesAcctID))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [INUnit(typeof (INComponent.componentID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBQuantity]
  [PXUIField]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Fixed Amount")]
  public virtual Decimal? FixedAmt
  {
    get => this._FixedAmt;
    set => this._FixedAmt = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("P")]
  [INAmountOption.List]
  public virtual string AmtOption
  {
    get => this._AmtOption;
    set => this._AmtOption = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("F")]
  [INAmountOptionASC606.List]
  public virtual string AmtOptionASC606 { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<INComponent>.By<INComponent.inventoryID, INComponent.componentID>
  {
    public static INComponent Find(
      PXGraph graph,
      int? inventoryID,
      int? componentID,
      PKFindOptions options = 0)
    {
      return (INComponent) PrimaryKeyOf<INComponent>.By<INComponent.inventoryID, INComponent.componentID>.FindBy(graph, (object) inventoryID, (object) componentID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INComponent>.By<INComponent.inventoryID>
    {
    }

    public class ComponentInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INComponent>.By<INComponent.componentID>
    {
    }

    public class DeferredCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<INComponent>.By<INComponent.deferredCode>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INComponent>.By<INComponent.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INComponent>.By<INComponent.salesSubID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponent.inventoryID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponent.componentID>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponent.deferredCode>
  {
  }

  public abstract class defaultTerm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponent.defaultTerm>
  {
  }

  public abstract class defaultTermUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponent.defaultTermUOM>
  {
  }

  public abstract class overrideDefaultTerm : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INComponent.overrideDefaultTerm>
  {
  }

  public abstract class percentage : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponent.percentage>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponent.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponent.salesSubID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponent.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponent.qty>
  {
  }

  public abstract class fixedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponent.fixedAmt>
  {
  }

  public abstract class amtOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponent.amtOption>
  {
  }

  public abstract class amtOptionASC606 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponent.amtOptionASC606>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INComponent.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INComponent.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponent.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INComponent.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INComponent.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponent.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INComponent.lastModifiedDateTime>
  {
  }
}
