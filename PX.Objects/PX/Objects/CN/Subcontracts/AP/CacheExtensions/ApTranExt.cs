// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.ApTranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

public sealed class ApTranExt : PXCacheExtension<PX.Objects.AP.APTran>
{
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Subcontract Nbr.", Enabled = false, IsReadOnly = true)]
  public string SubcontractNbr
  {
    get => !(this.Base.POOrderType == "RS") ? (string) null : this.Base.PONbr;
  }

  [PXInt]
  [PXUIField(DisplayName = "Subcontract Line", Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POLine.lineNbr, Where<PX.Objects.PO.POLine.orderType, Equal<Current<PX.Objects.AP.APTran.pOOrderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<PX.Objects.AP.APTran.pONbr>>>>>), new Type[] {typeof (PX.Objects.PO.POLine.lineNbr), typeof (PX.Objects.PO.POLine.projectID), typeof (PX.Objects.PO.POLine.taskID), typeof (PX.Objects.PO.POLine.costCodeID), typeof (PX.Objects.PO.POLine.inventoryID), typeof (PX.Objects.PO.POLine.lineType), typeof (PX.Objects.PO.POLine.tranDesc), typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.orderQty), typeof (PX.Objects.PO.POLine.curyUnitCost), typeof (PX.Objects.PO.POLine.curyExtCost)})]
  public int? SubcontractLineNbr
  {
    get => !(this.Base.POOrderType == "RS") ? new int?() : this.Base.POLineNbr;
    set
    {
      if (!(this.Base.POOrderType == "RS"))
        return;
      this.Base.POLineNbr = value;
    }
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class subcontractNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class subcontractLineNbr : IBqlField, IBqlOperand
  {
  }
}
