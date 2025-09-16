// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickPackShipSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOPickPackShipSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Display the Pick Tab", Enabled = true)]
  public virtual bool? ShowPickTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display the Pack Tab", Enabled = true)]
  public virtual bool? ShowPackTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display the Ship Tab", Enabled = true)]
  public virtual bool? ShowShipTab { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Display the Return Tab", Enabled = true)]
  public virtual bool? ShowReturningTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display the Scan Log Tab", Enabled = true)]
  public virtual bool? ShowScanLogTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Carts for Picking", FieldClass = "Carts")]
  [PXUIEnabled(typeof (SOPickPackShipSetup.showPickTab))]
  [PXFormula(typeof (Switch<Case<Where<SOPickPackShipSetup.showPickTab, Equal<False>>, False>, SOPickPackShipSetup.useCartsForPick>))]
  public virtual bool? UseCartsForPick { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Explicit Line Confirmation")]
  public virtual bool? ExplicitLineConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity")]
  public virtual bool? UseDefaultQty { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [SOPickPackShipSetup.shortShipmentConfirmation.List]
  [PXUIField(DisplayName = "Short Shipment Confirmation")]
  public virtual string ShortShipmentConfirmation { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("PICK")]
  [SOPickPackShipSetup.shipmentLocationOrdering.List]
  [PXUIField(DisplayName = "Order Shipment Lines by Location's")]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>))]
  public virtual string ShipmentLocationOrdering { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirm Weight for Each Package")]
  public virtual bool? ConfirmEachPackageWeight { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirm Dimensions for Packages with Editable Dimensions")]
  public virtual bool? ConfirmEachPackageDimensions { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item")]
  public virtual bool? RequestLocationForEachItem { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirm Tote Selection on Wave Picking")]
  public virtual bool? ConfirmToteForEachItem { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Add Totes to Shipments on the Fly")]
  public virtual bool? AllowMultipleTotesPerShipment { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether a user can pick the pick list from the lowest to the highest location path and from the highest to the lowest location path.
  /// If the value is set to <see langword="true" />, a user can pick lists in both directions.
  /// If the value is set to <see langword="false" />, a user can pick lists only in one direction (from the lowest location path to the highest).
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Bidirectional Pick Lists")]
  public virtual bool? AllowBidirectionalPickLists { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Print Packing Slips with Pick Lists")]
  public virtual bool? PrintPickListsAndPackSlipsTogether { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Location")]
  public virtual bool? DefaultLocationFromShipment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Lot/Serial Nbr.")]
  public virtual bool? DefaultLotSerialFromShipment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Shipment Confirmation Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintShipmentConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Shipment Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintShipmentLabels { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Commercial Invoices Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintCommercialInvoices { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enter Length/Width/Height for Packages", Visible = false)]
  public virtual bool? EnterSizeForPackages { get; set; }

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

  public bool IsPackOnly
  {
    get
    {
      bool? showPickTab = this.ShowPickTab;
      bool flag = false;
      return showPickTab.GetValueOrDefault() == flag & showPickTab.HasValue && this.ShowPackTab.GetValueOrDefault();
    }
  }

  public class PK : PrimaryKeyOf<SOPickPackShipSetup>.By<SOPickPackShipSetup.branchID>
  {
    public static SOPickPackShipSetup Find(PXGraph graph, int? branchID, PKFindOptions options = 0)
    {
      return (SOPickPackShipSetup) PrimaryKeyOf<SOPickPackShipSetup>.By<SOPickPackShipSetup.branchID>.FindBy(graph, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<SOPickPackShipSetup>.By<SOPickPackShipSetup.branchID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickPackShipSetup.branchID>
  {
  }

  public abstract class showPickTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickPackShipSetup.showPickTab>
  {
  }

  public abstract class showPackTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickPackShipSetup.showPackTab>
  {
  }

  public abstract class showShipTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickPackShipSetup.showShipTab>
  {
  }

  public abstract class showReturningTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.showReturningTab>
  {
  }

  public abstract class showScanLogTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.showScanLogTab>
  {
  }

  public abstract class useCartsForPick : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.useCartsForPick>
  {
  }

  public abstract class explicitLineConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.explicitLineConfirmation>
  {
  }

  public abstract class useDefaultQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.useDefaultQty>
  {
  }

  public abstract class shortShipmentConfirmation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickPackShipSetup.shortShipmentConfirmation>
  {
    public const string Forbid = "F";
    public const string Warn = "W";

    public class forbid : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickPackShipSetup.shortShipmentConfirmation.forbid>
    {
      public forbid()
        : base("F")
      {
      }
    }

    public class warn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickPackShipSetup.shortShipmentConfirmation.warn>
    {
      public warn()
        : base("W")
      {
      }
    }

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Forbid = "Forbid";
      public const string Warn = "Allow with Warning";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("F", "Forbid"),
          PXStringListAttribute.Pair("W", "Allow with Warning")
        })
      {
      }
    }
  }

  public abstract class shipmentLocationOrdering : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickPackShipSetup.shipmentLocationOrdering>
  {
    public const string Pick = "PICK";
    public const string Path = "PATH";

    public class pick : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickPackShipSetup.shipmentLocationOrdering.pick>
    {
      public pick()
        : base("PICK")
      {
      }
    }

    public class path : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickPackShipSetup.shipmentLocationOrdering.path>
    {
      public path()
        : base("PATH")
      {
      }
    }

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Pick = "Pick Priority";
      public const string Path = "Path Priority";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("PICK", "Pick Priority"),
          PXStringListAttribute.Pair("PATH", "Path Priority")
        })
      {
      }
    }
  }

  public abstract class confirmEachPackageWeight : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.confirmEachPackageWeight>
  {
  }

  public abstract class confirmEachPackageDimensions : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.confirmEachPackageDimensions>
  {
  }

  public abstract class requestLocationForEachItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.requestLocationForEachItem>
  {
  }

  public abstract class confirmToteForEachItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.confirmToteForEachItem>
  {
  }

  public abstract class allowMultipleTotesPerShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.allowMultipleTotesPerShipment>
  {
  }

  public abstract class allowBidirectionalPickLists : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.allowBidirectionalPickLists>
  {
  }

  public abstract class printPickListsAndPackSlipsTogether : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.printPickListsAndPackSlipsTogether>
  {
  }

  public abstract class defaultLocationFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.defaultLocationFromShipment>
  {
  }

  public abstract class defaultLotSerialFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.defaultLotSerialFromShipment>
  {
  }

  public abstract class printShipmentConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.printShipmentConfirmation>
  {
  }

  public abstract class printShipmentLabels : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.printShipmentLabels>
  {
  }

  public abstract class printCommercialInvoices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.printCommercialInvoices>
  {
  }

  public abstract class enterSizeForPackages : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipSetup.enterSizeForPackages>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPickPackShipSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPickPackShipSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickPackShipSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickPackShipSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickPackShipSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickPackShipSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickPackShipSetup.lastModifiedDateTime>
  {
  }
}
