// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ForbidConfirmationForShipmentsThatHavePickListInProgress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Duplicates disabling condition <see cref="P:PX.Objects.SO.SOShipmentEntry_Workflow.Conditions.IsNotHeldByPicking" /> that is used for <see cref="F:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension.confirmShipmentAction" />, but for mass processing
/// </summary>
public class ForbidConfirmationForShipmentsThatHavePickListInProgress : 
  PXGraphExtension<SOInvoiceShipment>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>() || PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>();
  }

  [PXOverride]
  public virtual void ApplyShipmentFilters(
    PXSelectBase<SOShipment> shCmd,
    SOShipmentFilter filter,
    Action<PXSelectBase<SOShipment>, SOShipmentFilter> base_ApplyShipmentFilters)
  {
    base_ApplyShipmentFilters(shCmd, filter);
    if (!(filter.Action == "SO302000$confirmShipmentAction"))
      return;
    shCmd.Join<LeftJoin<SOPickingWorksheet, On<SOShipment.FK.Worksheet>>>();
    shCmd.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.currentWorksheetNbr, IsNull>>>, Or<BqlOperand<SOShipment.picked, IBqlBool>.IsEqual<True>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, In3<SOPickingWorksheet.worksheetType.wave, SOPickingWorksheet.worksheetType.batch>>>>>.And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.picked, SOPickingWorksheet.status.completed>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, Equal<SOPickingWorksheet.worksheetType.single>>>>>.And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsNotEqual<SOPickingWorksheet.status.picking>>>>>();
  }
}
