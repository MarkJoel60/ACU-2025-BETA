// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INScanSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INScanSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Carts for Transferring", FieldClass = "Carts", Visible = false)]
  public virtual bool? UseCartsForTransfers { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Explicit Line Confirmation")]
  public virtual bool? ExplicitLineConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity in Receipts")]
  public virtual bool? UseDefaultQtyInReceipt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity in Issues")]
  public virtual bool? UseDefaultQtyInIssue { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity in Transfers")]
  public virtual bool? UseDefaultQtyInTransfer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity in PI Counts")]
  public virtual bool? UseDefaultQtyInCount { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Default Reason Code in Receipts")]
  public virtual bool? UseDefaultReasonCodeInReceipt { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Default Reason Code in Issues")]
  public virtual bool? UseDefaultReasonCodeInIssue { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Default Reason Code in Transfers")]
  public virtual bool? UseDefaultReasonCodeInTransfer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Lot/Serial Nbr. in Transfers")]
  public virtual bool? UseDefaultLotSerialNbrInTransfer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item in Receipts")]
  public virtual bool? RequestLocationForEachItemInReceipt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item in Issues")]
  public virtual bool? RequestLocationForEachItemInIssue { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item in Transfers")]
  public virtual bool? RequestLocationForEachItemInTransfer { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Warehouse from User Profile")]
  public virtual bool? DefaultWarehouse { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Inventory Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintInventoryLabelsAutomatically { get; set; }

  [PXDefault("IN619200")]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Inventory Labels Report ID", FieldClass = "DeviceHub")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.in_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXUIEnabled(typeof (INScanSetup.printInventoryLabelsAutomatically))]
  [PXUIRequired(typeof (Where<INScanSetup.printInventoryLabelsAutomatically, Equal<True>, And<FeatureInstalled<FeaturesSet.deviceHub>>>))]
  public virtual string InventoryLabelsReportID { get; set; }

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

  public class PK : PrimaryKeyOf<INScanSetup>.By<INScanSetup.branchID>
  {
    public static INScanSetup Find(PXGraph graph, int? branchID, PKFindOptions options = 0)
    {
      return (INScanSetup) PrimaryKeyOf<INScanSetup>.By<INScanSetup.branchID>.FindBy(graph, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INScanSetup>.By<INScanSetup.branchID>
    {
    }

    public class InventoryLabelsReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.ForeignKeyOf<INScanSetup>.By<INScanSetup.inventoryLabelsReportID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INScanSetup.branchID>
  {
  }

  public abstract class useCartsForTransfers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useCartsForTransfers>
  {
  }

  public abstract class explicitLineConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.explicitLineConfirmation>
  {
  }

  public abstract class useDefaultQtyInReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultQtyInReceipt>
  {
  }

  public abstract class useDefaultQtyInIssue : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultQtyInIssue>
  {
  }

  public abstract class useDefaultQtyInTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultQtyInTransfer>
  {
  }

  public abstract class useDefaultQtyInCount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultQtyInCount>
  {
  }

  public abstract class useDefaultReasonCodeInReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultReasonCodeInReceipt>
  {
  }

  public abstract class useDefaultReasonCodeInIssue : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultReasonCodeInIssue>
  {
  }

  public abstract class useDefaultReasonCodeInTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultReasonCodeInTransfer>
  {
  }

  public abstract class useDefaultLotSerialNbrInTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.useDefaultLotSerialNbrInTransfer>
  {
  }

  public abstract class requestLocationForEachItemInReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.requestLocationForEachItemInReceipt>
  {
  }

  public abstract class requestLocationForEachItemInIssue : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.requestLocationForEachItemInIssue>
  {
  }

  public abstract class requestLocationForEachItemInTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.requestLocationForEachItemInTransfer>
  {
  }

  public abstract class defaultWarehouse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.defaultWarehouse>
  {
  }

  public abstract class printInventoryLabelsAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanSetup.printInventoryLabelsAutomatically>
  {
  }

  public abstract class inventoryLabelsReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanSetup.inventoryLabelsReportID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INScanSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INScanSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INScanSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INScanSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INScanSetup.lastModifiedDateTime>
  {
  }
}
