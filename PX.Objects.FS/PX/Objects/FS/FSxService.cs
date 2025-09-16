// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxService
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxService : PXCacheExtension<
#nullable disable
PX.Objects.IN.InventoryItem>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXUIVisible(typeof (Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>>))]
  [PXDefault(typeof (Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>, ListField_BillingRule.FlatRate, Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>>>, ListField_BillingRule.FlatRate>>, ListField_BillingRule.Time>))]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [PXDefault(0)]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXDBTimeSpanLong]
  public virtual int? EstimatedDuration { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [ListField_Service_Action_Type.List]
  [PXUIField]
  public virtual string ActionType { get; set; }

  [PXInt]
  [PXDefault]
  [PXUIField(DisplayName = "Pending Base Price", Enabled = false, Visible = false)]
  public virtual int? PendingBasePrice { get; set; }

  [PXDate]
  [PXDefault]
  [PXUIField(DisplayName = "Pending Base Price Date", Enabled = false, Visible = false)]
  public virtual int? PendingBasePriceDate { get; set; }

  [PXDate]
  [PXDefault]
  [PXUIField(DisplayName = "Base Price Date", Enabled = false, Visible = false)]
  public virtual int? BasePriceDate { get; set; }

  [PXInt]
  [PXDefault]
  [PXUIField(DisplayName = "Last Base Price", Enabled = false, Visible = false)]
  public virtual int? LastBasePrice { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (Search<EPSetup.regularHoursType>))]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string DfltEarningType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (Where<Current<PX.Objects.IN.InventoryItem.itemType>, Equal<INItemTypes.serviceItem>>))]
  [PXUIEnabled(typeof (Where<Current<PX.Objects.IN.InventoryItem.itemType>, Equal<INItemTypes.serviceItem>>))]
  [PXUIField(DisplayName = "Is a Travel Item")]
  public virtual bool? IsTravelItem { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class estimatedDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxService.estimatedDuration>
  {
  }

  public abstract class actionType : ListField_Service_Action_Type
  {
  }

  public abstract class pendingBasePrice : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxService.pendingBasePrice>
  {
  }

  public abstract class pendingBasePriceDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxService.pendingBasePriceDate>
  {
  }

  public abstract class basePriceDate : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxService.basePriceDate>
  {
  }

  public abstract class lastBasePrice : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxService.lastBasePrice>
  {
  }

  public abstract class dfltEarningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxService.dfltEarningType>
  {
  }

  public abstract class isTravelItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxService.isTravelItem>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxService.ChkServiceManagement>
  {
  }
}
