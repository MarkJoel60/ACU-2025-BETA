// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIClass
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

[PXCacheName("Physical Inventory Type")]
[Serializable]
public class INPIClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PIClassID;
  protected string _Descr;
  protected string _Method;
  protected string _CycleID;
  protected string _ABCCodeID;
  protected string _MovementClassID;
  protected string _SelectedMethod;
  protected bool? _ByFrequency;
  protected bool? _ByABCFrequency;
  protected bool? _ByMovementClassFrequency;
  protected bool? _ByCycleFrequency;
  protected int? _SiteID;
  protected string _NAO1;
  protected string _NAO2;
  protected string _NAO3;
  protected string _NAO4;
  protected short? _BlankLines;
  protected short? _RandomItemsLimit;
  protected short? _LastCountPeriod;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INPIClass.pIClassID>))]
  [PXDefault]
  public virtual string PIClassID
  {
    get => this._PIClassID;
    set => this._PIClassID = value;
  }

  [PXDefault]
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PIMethod.List]
  [PXDefault("F")]
  public virtual string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INPICycle.cycleID), DescriptionField = typeof (INPICycle.descr))]
  [PXUIField(DisplayName = "Cycle ID")]
  public virtual string CycleID
  {
    get => this._CycleID;
    set => this._CycleID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXSelector(typeof (INABCCode.aBCCodeID), DescriptionField = typeof (INABCCode.descr))]
  [PXUIField(DisplayName = "ABC Code")]
  public virtual string ABCCodeID
  {
    get => this._ABCCodeID;
    set => this._ABCCodeID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXSelector(typeof (INMovementClass.movementClassID), DescriptionField = typeof (INMovementClass.descr))]
  [PXUIField(DisplayName = "Movement Class ID")]
  public virtual string MovementClassID
  {
    get => this._MovementClassID;
    set => this._MovementClassID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PIInventoryMethod.List]
  [PXDefault("N")]
  public virtual string SelectedMethod
  {
    get => this._SelectedMethod;
    set => this._SelectedMethod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ByFrequency
  {
    get => this._ByFrequency;
    set => this._ByFrequency = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "By Frequency")]
  public virtual bool? ByABCFrequency
  {
    get => this._ByFrequency;
    set
    {
      if (!(this.Method == "A"))
        return;
      this._ByFrequency = value;
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "By Frequency")]
  public virtual bool? ByMovementClassFrequency
  {
    get => this._ByFrequency;
    set
    {
      if (!(this.Method == "M"))
        return;
      this._ByFrequency = value;
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "By Frequency")]
  public virtual bool? ByCycleFrequency
  {
    get => this._ByFrequency;
    set
    {
      if (!(this.Method == "Y"))
        return;
      this._ByFrequency = value;
    }
  }

  [Site]
  [PXForeignReference(typeof (INPIClass.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Items with Zero Book Quantity in PI")]
  public virtual bool? IncludeZeroItems { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hide Book Qty. on PI Count")]
  public virtual bool? HideBookQty { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PINumberAssignmentOrder.List]
  [PXDefault("LI")]
  public virtual string NAO1
  {
    get => this._NAO1;
    set => this._NAO1 = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PINumberAssignmentOrder.List]
  [PXDefault("II")]
  public virtual string NAO2
  {
    get => this._NAO2;
    set => this._NAO2 = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PINumberAssignmentOrder.List]
  [PXDefault("SI")]
  public virtual string NAO3
  {
    get => this._NAO3;
    set => this._NAO3 = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PINumberAssignmentOrder.List]
  [PXDefault("LS")]
  public virtual string NAO4
  {
    get => this._NAO4;
    set => this._NAO4 = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 10000)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Blank Lines To Append")]
  public virtual short? BlankLines
  {
    get => this._BlankLines;
    set => this._BlankLines = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 10000)]
  [PXUIField(DisplayName = "Max. Number of Items")]
  public virtual short? RandomItemsLimit
  {
    get => this._RandomItemsLimit;
    set => this._RandomItemsLimit = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 10000)]
  [PXUIField]
  public virtual short? LastCountPeriod
  {
    get => this._LastCountPeriod;
    set => this._LastCountPeriod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Unfreeze Stock When Counting Is Finished")]
  public virtual bool? UnlockSiteOnCountingFinish { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>
  {
    public static INPIClass Find(PXGraph graph, string pIClassID, PKFindOptions options = 0)
    {
      return (INPIClass) PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>.FindBy(graph, (object) pIClassID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INPIClass>.By<INPIClass.siteID>
    {
    }

    public class PICycle : 
      PrimaryKeyOf<INPICycle>.By<INPICycle.cycleID>.ForeignKeyOf<INPIClass>.By<INPIClass.cycleID>
    {
    }

    public class ABCCode : 
      PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>.ForeignKeyOf<INPIClass>.By<INPIClass.aBCCodeID>
    {
    }

    public class MovementClass : 
      PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.ForeignKeyOf<INPIClass>.By<INPIClass.movementClassID>
    {
    }
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.pIClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.descr>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.method>
  {
  }

  public abstract class cycleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.cycleID>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.aBCCodeID>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClass.movementClassID>
  {
  }

  public abstract class selectedMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.selectedMethod>
  {
  }

  public abstract class byFrequency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIClass.byFrequency>
  {
  }

  public abstract class byABCFrequency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIClass.byABCFrequency>
  {
  }

  public abstract class byMovementClassFrequency : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INPIClass.byMovementClassFrequency>
  {
  }

  public abstract class byCycleFrequency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIClass.byCycleFrequency>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIClass.siteID>
  {
  }

  public abstract class includeZeroItems : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIClass.includeZeroItems>
  {
  }

  public abstract class hideBookQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIClass.hideBookQty>
  {
  }

  public abstract class nAO1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.nAO1>
  {
  }

  public abstract class nAO2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.nAO2>
  {
  }

  public abstract class nAO3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.nAO3>
  {
  }

  public abstract class nAO4 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClass.nAO4>
  {
  }

  public abstract class blankLines : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPIClass.blankLines>
  {
  }

  public abstract class randomItemsLimit : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPIClass.randomItemsLimit>
  {
  }

  public abstract class lastCountPeriod : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPIClass.lastCountPeriod>
  {
  }

  public abstract class unlockSiteOnCountingFinish : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INPIClass.unlockSiteOnCountingFinish>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClass.lastModifiedDateTime>
  {
  }
}
