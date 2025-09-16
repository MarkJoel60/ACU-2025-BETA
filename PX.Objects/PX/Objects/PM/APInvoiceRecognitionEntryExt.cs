// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APInvoiceRecognitionEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class APInvoiceRecognitionEntryExt : PXGraphExtension<APInvoiceRecognitionEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  [PXOverride]
  public virtual HashSet<string> GetAlwaysDefaultDetailFields()
  {
    return new HashSet<string>()
    {
      "ProjectID",
      "TaskID",
      "CostCodeID"
    };
  }

  [PXOverride]
  public virtual void EnsureTransactions(
    APInvoiceRecognitionEntryExt.EnsureTransactionsDelegate baseMethod)
  {
    PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
    if (cache.Cached.Cast<object>().Any<object>())
    {
      foreach (PX.Objects.AP.APTran tran in cache.Cached)
        this.ValidateDetailLine(tran);
    }
    baseMethod();
  }

  [PXMergeAttributes]
  [PXUIField]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLineS.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  [PXSelector(typeof (PMTask.taskID), SubstituteKey = typeof (PMTask.taskCD))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLineS.taskID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLineS.costCodeID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedTran> e)
  {
    if (e.Row == null)
      return;
    bool flag = e.Row.PONbr == null;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APRecognizedTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APRecognizedTran>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APRecognizedTran>>) e).Cache, (object) e.Row, flag);
  }

  protected virtual void ValidateDetailLine(PX.Objects.AP.APTran tran)
  {
    PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
    if (!tran.ProjectID.HasValue)
    {
      PXSetPropertyException<PX.Objects.AP.APTran.projectID> propertyException = new PXSetPropertyException<PX.Objects.AP.APTran.projectID>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APTran.projectID>(cache)
      });
      cache.RaiseExceptionHandling<PX.Objects.AP.APTran.projectID>((object) tran, (object) tran.TaskID, (Exception) propertyException);
      throw propertyException;
    }
    bool flag1 = ProjectDefaultAttribute.IsNonProject(tran.ProjectID);
    if (!flag1 && !tran.TaskID.HasValue)
    {
      PXSetPropertyException<PX.Objects.AP.APTran.taskID> propertyException = new PXSetPropertyException<PX.Objects.AP.APTran.taskID>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APTran.taskID>(cache)
      });
      cache.RaiseExceptionHandling<PX.Objects.AP.APTran.taskID>((object) tran, (object) tran.TaskID, (Exception) propertyException);
      throw propertyException;
    }
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.costCodes>();
    if (!flag1 & flag2 && !tran.CostCodeID.HasValue)
    {
      PXSetPropertyException<PX.Objects.AP.APTran.costCodeID> propertyException = new PXSetPropertyException<PX.Objects.AP.APTran.costCodeID>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APTran.costCodeID>(cache)
      });
      cache.RaiseExceptionHandling<PX.Objects.AP.APTran.costCodeID>((object) tran, (object) tran.CostCodeID, (Exception) propertyException);
      throw propertyException;
    }
  }

  public delegate HashSet<string> GetAlwaysDefaultDetailFieldsDelegate();

  public delegate void EnsureTransactionsDelegate();
}
