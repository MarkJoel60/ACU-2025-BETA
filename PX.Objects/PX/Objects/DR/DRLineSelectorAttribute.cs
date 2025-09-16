// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRLineSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.SO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.DR;

public class DRLineSelectorAttribute : PXCustomSelectorAttribute
{
  private readonly Type moduleField;
  private readonly Type docTypeField;
  private readonly Type refNbrField;

  public DRLineSelectorAttribute(Type moduleField, Type docTypeField, Type refNbrField)
    : base(typeof (DRLineRecord.lineNbr))
  {
    if (moduleField == (Type) null)
      throw new ArgumentNullException(nameof (moduleField));
    if (docTypeField == (Type) null)
      throw new ArgumentNullException(nameof (docTypeField));
    if (refNbrField == (Type) null)
      throw new ArgumentNullException(nameof (refNbrField));
    if (BqlCommand.GetItemType(moduleField).Name != BqlCommand.GetItemType(docTypeField).Name || BqlCommand.GetItemType(moduleField).Name != BqlCommand.GetItemType(refNbrField).Name)
      throw new ArgumentException("All fields must belong to the same declaring type.");
    this.moduleField = moduleField;
    this.docTypeField = docTypeField;
    this.refNbrField = refNbrField;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual IEnumerable GetRecords()
  {
    DRLineSelectorAttribute selectorAttribute = this;
    PXCache cach = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute.moduleField)];
    if (cach.Current != null)
    {
      string str1 = (string) cach.GetValue(cach.Current, selectorAttribute.docTypeField.Name);
      string str2 = (string) cach.GetValue(cach.Current, selectorAttribute.refNbrField.Name);
      if ((string) cach.GetValue(cach.Current, selectorAttribute.moduleField.Name) == "AR")
      {
        PXSelectBase<ARTran> pxSelectBase = (PXSelectBase<ARTran>) new PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>(selectorAttribute._Graph);
        object[] objArray = new object[2]
        {
          (object) str1,
          (object) str2
        };
        foreach (PXResult<ARTran> pxResult in pxSelectBase.Select(objArray))
        {
          ARTran documentLine = PXResult<ARTran>.op_Implicit(pxResult);
          ARReleaseProcess.Amount salesPostingAmount = ARReleaseProcess.GetSalesPostingAmount(selectorAttribute._Graph, documentLine);
          yield return (object) new DRLineRecord()
          {
            CuryInfoID = documentLine.CuryInfoID,
            CuryTranAmt = salesPostingAmount.Cury,
            InventoryID = documentLine.InventoryID,
            LineNbr = documentLine.LineNbr,
            TranAmt = salesPostingAmount.Base,
            TranDesc = documentLine.TranDesc,
            BranchID = documentLine.BranchID
          };
        }
      }
      else
      {
        PXSelectBase<APTran> pxSelectBase = (PXSelectBase<APTran>) new PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<Where<APTran.lineType, IsNull, Or<APTran.lineType, NotEqual<SOLineType.discount>>>>>>>(selectorAttribute._Graph);
        object[] objArray = new object[2]
        {
          (object) str1,
          (object) str2
        };
        foreach (PXResult<APTran> pxResult in pxSelectBase.Select(objArray))
        {
          APTran documentLine = PXResult<APTran>.op_Implicit(pxResult);
          ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount(selectorAttribute._Graph, documentLine);
          yield return (object) new DRLineRecord()
          {
            CuryInfoID = documentLine.CuryInfoID,
            CuryTranAmt = expensePostingAmount.Cury,
            InventoryID = documentLine.InventoryID,
            LineNbr = documentLine.LineNbr,
            TranAmt = expensePostingAmount.Base,
            TranDesc = documentLine.TranDesc,
            BranchID = documentLine.BranchID
          };
        }
      }
    }
  }
}
