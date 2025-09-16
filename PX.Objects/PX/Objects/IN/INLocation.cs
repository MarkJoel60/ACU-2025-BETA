// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
[Serializable]
public class INLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LocationID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _LocationCD;
  protected string _Descr;
  protected bool? _IsCosted;
  protected int? _CostSiteID;
  protected bool? _InclQtyAvail;
  protected bool? _AssemblyValid;
  protected short? _PickPriority;
  protected bool? _SalesValid;
  protected bool? _ReceiptsValid;
  protected bool? _TransfersValid;
  protected bool? _ProductionValid;
  protected string _PrimaryItemValid;
  protected int? _PrimaryItemID;
  protected int? _PrimaryItemClassID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected bool? _Active;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  public const string Main = "MAIN";

  [PXDBForeignIdentity(typeof (INCostSite))]
  [PXReferentialIntegrityCheck]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (INLocation.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationRaw(IsKey = true)]
  [PXDefault]
  public virtual string LocationCD
  {
    get => this._LocationCD;
    set => this._LocationCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cost Separately")]
  public virtual bool? IsCosted
  {
    get => this._IsCosted;
    set => this._IsCosted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Sort Location")]
  public virtual bool? IsSorting { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INLocation.isCosted, Equal<boolTrue>>, INLocation.locationID>, INLocation.siteID>), typeof (int))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include in Qty. Available")]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<INLocation.isSorting, IBqlBool>.IsEqual<True>>.Else<INLocation.inclQtyAvail>))]
  [PXUIEnabled(typeof (BqlOperand<INLocation.isSorting, IBqlBool>.IsEqual<False>))]
  public virtual bool? InclQtyAvail
  {
    get => this._InclQtyAvail;
    set => this._InclQtyAvail = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Assembly Allowed")]
  public virtual bool? AssemblyValid
  {
    get => this._AssemblyValid;
    set => this._AssemblyValid = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 999)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Pick Priority")]
  public virtual short? PickPriority
  {
    get => this._PickPriority;
    set => this._PickPriority = value;
  }

  [PXDBInt(MinValue = 0)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Path")]
  public virtual int? PathPriority { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Sales Allowed")]
  public virtual bool? SalesValid
  {
    get => this._SalesValid;
    set => this._SalesValid = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Receipts Allowed")]
  public virtual bool? ReceiptsValid
  {
    get => this._ReceiptsValid;
    set => this._ReceiptsValid = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Transfers Allowed")]
  public virtual bool? TransfersValid
  {
    get => this._TransfersValid;
    set => this._TransfersValid = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Production Allowed")]
  public virtual bool? ProductionValid
  {
    get => this._ProductionValid;
    set => this._ProductionValid = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [INPrimaryItemValid.List]
  [PXUIField(DisplayName = "Primary Item Validation")]
  public virtual string PrimaryItemValid
  {
    get => this._PrimaryItemValid;
    set => this._PrimaryItemValid = value;
  }

  [StockItem(DisplayName = "Primary Item")]
  [PXForeignReference(typeof (INLocation.FK.PrimaryInventoryItem))]
  public virtual int? PrimaryItemID
  {
    get => this._PrimaryItemID;
    set => this._PrimaryItemID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Primary Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual int? PrimaryItemClassID
  {
    get => this._PrimaryItemClassID;
    set => this._PrimaryItemClassID = value;
  }

  [Project(typeof (Where<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<INLocation.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (INLocation.projectID), AllowNull = true)]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INLocation>.By<INLocation.locationID>.Dirty
  {
    public static INLocation Find(PXGraph graph, int? locationID, PKFindOptions options = 0)
    {
      return (INLocation) PrimaryKeyOf<INLocation>.By<INLocation.locationID>.Dirty.FindBy(graph, (object) locationID, options != null || locationID.GetValueOrDefault() > 0 ? options : (PKFindOptions) (object) 1);
    }
  }

  public static class FK
  {
    public class CostSite : 
      PrimaryKeyOf<INCostSite>.By<INCostSite.costSiteID>.ForeignKeyOf<INLocation>.By<INLocation.costSiteID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INLocation>.By<INLocation.siteID>
    {
    }

    public class PrimaryInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INLocation>.By<INLocation.primaryItemID>
    {
    }

    public class PrimaryItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INLocation>.By<INLocation.primaryItemClassID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<INLocation>.By<INLocation.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<INLocation>.By<INLocation.projectID, INLocation.taskID>
    {
    }
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.locationID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.siteID>
  {
  }

  public abstract class locationCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLocation.locationCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLocation.descr>
  {
  }

  public abstract class isCosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.isCosted>
  {
  }

  public abstract class isSorting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.isSorting>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.costSiteID>
  {
  }

  public abstract class inclQtyAvail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.inclQtyAvail>
  {
  }

  public abstract class assemblyValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.assemblyValid>
  {
  }

  public abstract class pickPriority : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INLocation.pickPriority>
  {
  }

  public abstract class pathPriority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.pathPriority>
  {
  }

  public abstract class salesValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.salesValid>
  {
  }

  public abstract class receiptsValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.receiptsValid>
  {
  }

  public abstract class transfersValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.transfersValid>
  {
  }

  public abstract class productionValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.productionValid>
  {
  }

  public abstract class primaryItemValid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLocation.primaryItemValid>
  {
  }

  public abstract class primaryItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.primaryItemID>
  {
  }

  public abstract class primaryItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocation.primaryItemClassID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocation.taskID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocation.active>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLocation.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLocation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLocation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLocation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLocation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLocation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLocation.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INLocation.Tstamp>
  {
  }

  public class main : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLocation.main>
  {
    public main()
      : base("MAIN")
    {
    }
  }
}
