// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceivePutAwaySetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
public class POReceivePutAwaySetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXUIField(DisplayName = "Display the Receive Tab", Enabled = true)]
  public virtual bool? ShowReceivingTab { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Display the Return Tab", Enabled = true)]
  public virtual bool? ShowReturningTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display the Put Away Tab", Enabled = true)]
  public virtual bool? ShowPutAwayTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display the Scan Log Tab", Enabled = true)]
  public virtual bool? ShowScanLogTab { get; set; }

  /// <summary>
  /// Indicates (if true) that the Receive Transfer tab is shown on the Receive and Put Away (PO302020) form.
  /// If the check box is cleared, the Receive Transfer tab is not shown.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Display the Receive Transfer Tab", Enabled = true)]
  public virtual bool? ShowReceiveTransferTab { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Carts for Putting Away", FieldClass = "Carts")]
  [PXUIEnabled(typeof (POReceivePutAwaySetup.showPutAwayTab))]
  [PXFormula(typeof (Switch<Case<Where<POReceivePutAwaySetup.showPutAwayTab, Equal<False>>, False>, POReceivePutAwaySetup.useCartsForPutAway>))]
  public virtual bool? UseCartsForPutAway { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Explicit Line Confirmation")]
  public virtual bool? ExplicitLineConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Quantity")]
  public virtual bool? UseDefaultQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item on Receiving")]
  public virtual bool? RequestLocationForEachItemInReceive { get; set; }

  /// <summary>
  ///  Indicates whether to replace release of PO Receipts on RPA
  ///  screen with sending processed documents for management review.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Verify Receipts Before Release")]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceivePutAwaySetup.showReceivingTab, Equal<False>>>>>.And<BqlOperand<POReceivePutAwaySetup.showReceiveTransferTab, IBqlBool>.IsEqual<False>>>.Else<POReceivePutAwaySetup.verifyReceiptsBeforeRelease>))]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceivePutAwaySetup.showReceivingTab, Equal<True>>>>>.Or<BqlOperand<POReceivePutAwaySetup.showReceiveTransferTab, IBqlBool>.IsEqual<True>>))]
  public virtual bool? VerifyReceiptsBeforeRelease { get; set; }

  /// <summary>
  ///  Indicates that system will retain lines with a Receipt Qty of 0.00 on the
  ///  purchase receipt after the warehouse worker clicks the Confirm Receipt button.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (BqlOperand<POReceivePutAwaySetup.keepZeroLinesOnReceiptConfirmation, IBqlBool>.When<BqlOperand<POReceivePutAwaySetup.verifyReceiptsBeforeRelease, IBqlBool>.IsEqual<True>>.Else<False>))]
  [PXUIEnabled(typeof (BqlOperand<POReceivePutAwaySetup.verifyReceiptsBeforeRelease, IBqlBool>.IsEqual<True>))]
  [PXUIField(DisplayName = "Keep Zero Lines on Receipt Confirmation")]
  public virtual bool? KeepZeroLinesOnReceiptConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item on Putting Away")]
  public virtual bool? RequestLocationForEachItemInPutAway { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Location for Each Item on Returning")]
  public virtual bool? RequestLocationForEachItemInReturn { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Receiving Location")]
  public virtual bool? DefaultReceivingLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Default Auto-Generated Lot/Serial Nbr.")]
  public virtual bool? DefaultLotSerialNumber { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Default Expiration Date")]
  public virtual bool? DefaultExpireDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Single Receiving Location", FieldClass = "INLOCATION")]
  public virtual bool? SingleLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Inventory Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintInventoryLabelsAutomatically { get; set; }

  [PXDefault("IN619200")]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Inventory Labels Report ID", FieldClass = "DeviceHub")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.in_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXUIEnabled(typeof (POReceivePutAwaySetup.printInventoryLabelsAutomatically))]
  [PXUIRequired(typeof (Where<POReceivePutAwaySetup.printInventoryLabelsAutomatically, Equal<True>, And<FeatureInstalled<FeaturesSet.deviceHub>>>))]
  public virtual string InventoryLabelsReportID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Purchase Receipts Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintPurchaseReceiptAutomatically { get; set; }

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

  public class PK : PrimaryKeyOf<POReceivePutAwaySetup>.By<POReceivePutAwaySetup.branchID>
  {
    public static POReceivePutAwaySetup Find(PXGraph graph, int? branchID, PKFindOptions options = 0)
    {
      return (POReceivePutAwaySetup) PrimaryKeyOf<POReceivePutAwaySetup>.By<POReceivePutAwaySetup.branchID>.FindBy(graph, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class InventoryLabelsReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.ForeignKeyOf<POReceivePutAwaySetup>.By<POReceivePutAwaySetup.inventoryLabelsReportID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceivePutAwaySetup.branchID>
  {
  }

  public abstract class showReceivingTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.showReceivingTab>
  {
  }

  public abstract class showReturningTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.showReturningTab>
  {
  }

  public abstract class showPutAwayTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.showPutAwayTab>
  {
  }

  public abstract class showScanLogTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.showScanLogTab>
  {
  }

  public abstract class showReceiveTransferTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.showReceiveTransferTab>
  {
  }

  public abstract class useCartsForPutAway : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.useCartsForPutAway>
  {
  }

  public abstract class explicitLineConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.explicitLineConfirmation>
  {
  }

  public abstract class useDefaultQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.useDefaultQty>
  {
  }

  public abstract class requestLocationForEachItemInReceive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.requestLocationForEachItemInReceive>
  {
  }

  public abstract class verifyReceiptsBeforeRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.verifyReceiptsBeforeRelease>
  {
  }

  public abstract class keepZeroLinesOnReceiptConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.keepZeroLinesOnReceiptConfirmation>
  {
  }

  public abstract class requestLocationForEachItemInPutAway : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.requestLocationForEachItemInPutAway>
  {
  }

  public abstract class requestLocationForEachItemInReturn : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.requestLocationForEachItemInReturn>
  {
  }

  public abstract class defaultReceivingLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.defaultReceivingLocation>
  {
  }

  public abstract class defaultLotSerialNumber : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.defaultLotSerialNumber>
  {
  }

  public abstract class defaultExpireDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.defaultExpireDate>
  {
  }

  public abstract class singleLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.singleLocation>
  {
  }

  public abstract class printInventoryLabelsAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.printInventoryLabelsAutomatically>
  {
  }

  public abstract class inventoryLabelsReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceivePutAwaySetup.inventoryLabelsReportID>
  {
  }

  public abstract class printPurchaseReceiptAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwaySetup.printPurchaseReceiptAutomatically>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POReceivePutAwaySetup.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceivePutAwaySetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceivePutAwaySetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceivePutAwaySetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceivePutAwaySetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceivePutAwaySetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceivePutAwaySetup.lastModifiedDateTime>
  {
  }
}
