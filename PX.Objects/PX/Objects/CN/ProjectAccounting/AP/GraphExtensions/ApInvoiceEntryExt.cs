// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.GraphExtensions.ApInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;
using PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes;
using PX.Objects.CN.ProjectAccounting.PM.Descriptor.Attributes.ProjectTaskWithType;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;

public class ApInvoiceEntryExt : PXGraphExtension<APInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && !SiteMapExtension.IsTaxBillsAndAdjustmentsScreenId();
  }

  public void _(PX.Data.Events.FieldDefaulting<APTran.inventoryID> args)
  {
    APInvoice current = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    if ((current != null ? (current.VendorID.HasValue ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran.inventoryID>, object, object>) args).NewValue = (object) this.GetVendorDefaultInventoryId();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (ActiveProjectTaskAttribute))]
  [ActiveProjectTaskWithType(typeof (APTran.projectID), ForceTaskUpdating = true, CheckMandatoryCondition = typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<APInvoice.isRetainageDocument>, NotEqual<True>>>>>.Or<BqlOperand<Current2<APInvoice.paymentsByLinesAllowed>, IBqlBool>.IsEqual<True>>>))]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<APTran.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>, And<PMTask.status, Equal<ProjectTaskStatus.active>>>>>>))]
  [ProjectTaskTypeValidation(ProjectIdField = typeof (APTran.projectID), ProjectTaskIdField = typeof (APTran.taskID), Message = "Project Task Type is not valid. Only Tasks of 'Cost Task' and 'Cost and Revenue Task' types are allowed.", WrongProjectTaskType = "Rev")]
  protected virtual void _(PX.Data.Events.CacheAttached<APTran.taskID> e)
  {
  }

  private int? GetVendorDefaultInventoryId()
  {
    PX.Objects.AP.Vendor vendor = this.GetVendor();
    return vendor != null ? PXCache<PX.Objects.AP.Vendor>.GetExtension<VendorExt>(vendor).VendorDefaultInventoryId : new int?();
  }

  private PX.Objects.AP.Vendor GetVendor()
  {
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) ((PXSelectBase<APInvoice>) this.Base.Document).Current.VendorID
    }));
  }
}
