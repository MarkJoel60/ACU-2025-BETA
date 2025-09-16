// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SOOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class SOOrderEntryExt : ProjectCostCenterBase<SOOrderEntry>, ICostCenterSupport<PX.Objects.SO.SOLine>
{
  public PXAction<PX.Objects.SO.SOOrder> reopenOrder;

  public int SortOrder => 200;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ReopenOrder(PXAdapter adapter)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.ProjectID);
    if (pmProject?.Status == "L")
      throw new PXException(PXMessages.LocalizeFormat("The {0} project is closed.", new object[1]
      {
        (object) pmProject.ContractCD.Trim()
      }));
    return ((PXAction) this.Base.reopenOrder).Press(adapter);
  }

  public virtual IEnumerable<System.Type> GetFieldsDependOn()
  {
    yield return typeof (PX.Objects.SO.SOLine.isSpecialOrder);
    yield return typeof (PX.Objects.SO.SOLine.siteID);
    yield return typeof (PX.Objects.SO.SOLine.projectID);
    yield return typeof (PX.Objects.SO.SOLine.taskID);
    yield return typeof (PX.Objects.SO.SOLine.inventorySource);
  }

  public bool IsSpecificCostCenter(PX.Objects.SO.SOLine line)
  {
    return !line.IsSpecialOrder.GetValueOrDefault() && line.InventorySource != "F" && this.IsSpecificCostCenter(line.SiteID, line.ProjectID, line.TaskID);
  }

  public virtual int GetCostCenterID(PX.Objects.SO.SOLine tran)
  {
    int? projectID = (int?) (tran.ProjectID ?? ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.ProjectID);
    return this.FindOrCreateCostCenter(tran.SiteID, projectID, tran.TaskID).CostCenterID.Value;
  }

  public virtual INCostCenter GetCostCenter(PX.Objects.SO.SOLine tran)
  {
    int? projectID = (int?) (tran.ProjectID ?? ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.ProjectID);
    return this.FindOrCreateCostCenter(tran.SiteID, projectID, tran.TaskID);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, e.Row.ProjectID);
    int num;
    if (e.Row.InventoryID.HasValue)
    {
      bool? nullable = e.Row.IsSpecialOrder;
      if (!nullable.GetValueOrDefault())
      {
        if (pmProject == null)
        {
          num = 0;
          goto label_7;
        }
        nullable = pmProject.AllowIssueFromFreeStock;
        num = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_7;
      }
    }
    num = 0;
label_7:
    bool flag = num != 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.inventorySource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) null, flag);
    PXCacheEx.Adjust<InventorySourceType.ListAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOLine.inventorySource>((Action<InventorySourceType.ListAttribute>) (a => a.AllowSpecialOrders = e.Row.IsSpecialOrder.GetValueOrDefault()));
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.GetAttributes<PX.Objects.SO.SOLine.inventorySource>((object) e.Row).OfType<InventorySourceType.ListAttribute>().FirstOrDefault<InventorySourceType.ListAttribute>()?.SetValues(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.projectID>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
      soLine.ProjectID = e.Row.ProjectID;
      ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID> e)
  {
    if (e.Row == null || !e.Row.ProjectID.HasValue || !e.Row.TaskID.HasValue || ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID))
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID>, PX.Objects.SO.SOLine, object>) e).NewValue as int?);
    if (account == null || account.AccountGroupID.HasValue)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 2, new object[1]
    {
      (object) account.AccountCD
    });
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.salesAcctID>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID>, PX.Objects.SO.SOLine, object>) e).NewValue, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID> e)
  {
    if (e.Row == null || e.Row.TaskID.HasValue || ((PXGraph) this.Base).IsCopyPasteContext)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.salesAcctID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.taskID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.projectID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.projectID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.inventorySource>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.inventorySource>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.inventorySource>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.inventorySource>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue == null)
      return;
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue == "P")
    {
      if (((IQueryable<PXResult<PX.Objects.SO.SOLineSplit>>) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Select(Array.Empty<object>())).Any<PXResult<PX.Objects.SO.SOLineSplit>>((Expression<Func<PXResult<PX.Objects.SO.SOLineSplit>, bool>>) (x => ((PX.Objects.SO.SOLineSplit) x).IsAllocated == (bool?) true)))
        throw new PXSetPropertyException<PX.Objects.SO.SOLine.inventorySource>("The Free Stock inventory source cannot be selected because the items in the line are allocated.");
    }
    else
    {
      if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue == "F") || ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID))
        return;
      if (e.Row.POCreate.GetValueOrDefault())
        throw new PXSetPropertyException<PX.Objects.SO.SOLine.inventorySource>("The Free Stock inventory source cannot be selected because the line is marked for production or purchasing.");
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, e.Row.ProjectID);
      if (pmProject != null && pmProject.AccountingMode != "L" && !pmProject.AllowIssueFromFreeStock.GetValueOrDefault())
        throw new PXSetPropertyException<PX.Objects.SO.SOLine.inventorySource>("The Free Stock inventory source cannot be used if the project selected in the line has the Allow Issue from Free Stock check box cleared on the Summary tab of the Projects (PM301000) form. Select a different inventory source or allow issuing from free stock for the {0} project.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.IsSpecialOrder;
    if (nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) "S";
    }
    else
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, e.Row.ProjectID);
      if (pmProject != null)
      {
        nullable = pmProject.NonProject;
        if (!nullable.GetValueOrDefault())
        {
          bool? issueFromFreeStock = pmProject.AllowIssueFromFreeStock;
          nullable = e.Row.POCreate;
          if (nullable.GetValueOrDefault() || !issueFromFreeStock.GetValueOrDefault())
          {
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) "P";
            return;
          }
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) (inventoryItem?.DefaultInventorySourceForProjects ?? "F");
          return;
        }
      }
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventorySource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) "F";
    }
  }

  protected virtual void RecalculateInventorySource(PX.Objects.SO.SOLine line)
  {
    PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    if (line.IsSpecialOrder.GetValueOrDefault())
      cache.SetValueExt<PX.Objects.SO.SOLine.inventorySource>((object) line, (object) "S");
    else if (pmProject != null && pmProject.NonProject.GetValueOrDefault())
    {
      cache.SetValueExt<PX.Objects.SO.SOLine.inventorySource>((object) line, (object) "F");
    }
    else
    {
      bool? issueFromFreeStock = (bool?) pmProject?.AllowIssueFromFreeStock;
      if (line.POCreate.GetValueOrDefault() || !issueFromFreeStock.GetValueOrDefault())
      {
        cache.SetValueExt<PX.Objects.SO.SOLine.inventorySource>((object) line, (object) "P");
      }
      else
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, line.InventoryID);
        if (inventoryItem == null)
          return;
        cache.SetValueExt<PX.Objects.SO.SOLine.inventorySource>((object) line, (object) inventoryItem.DefaultInventorySourceForProjects);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID> e)
  {
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>>) e).Cancel)
      return;
    int? nullable = e.Row.CustomerID;
    if (nullable.HasValue)
    {
      nullable = e.Row.CustomerLocationID;
      if (nullable.HasValue)
      {
        PX.Objects.CR.Location location = PX.Objects.CR.Location.PK.Find((PXGraph) this.Base, e.Row.CustomerID, e.Row.CustomerLocationID);
        if (location != null)
        {
          nullable = location.CDefProjectID;
          if (nullable.HasValue)
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) location.CDefProjectID;
        }
      }
    }
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) e.Row.ProjectID;
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>((PXGraph) this.Base, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue, Array.Empty<object>()));
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault())
      return;
    int? customerId1 = e.Row.CustomerID;
    int? customerId2 = pmProject.CustomerID;
    if (customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.projectID>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue, (Exception) new PXSetPropertyException<PX.Objects.SO.SOOrder.projectID>("Customer on the Document doesn't match the Customer on the Project or Contract.", (PXErrorLevel) 2));
  }
}
