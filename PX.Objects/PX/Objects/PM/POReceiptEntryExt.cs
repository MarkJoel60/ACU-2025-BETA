// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POReceiptEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class POReceiptEntryExt : CommitmentTracking<POReceiptEntry>
{
  public PXSetup<PMSetup> Setup;
  public PXAction<PX.Objects.PO.POReceipt> createAPDocument;

  public new static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.projectID> e)
  {
    PX.Objects.PO.POReceiptLine row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("PO"))
      return;
    if (this.IsDefaultFromLocation(row))
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
      if (inLocation == null || !inLocation.ProjectID.HasValue)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.projectID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) inLocation.ProjectID;
    }
    else
    {
      PX.Objects.PO.POLine poLine = PX.Objects.PO.POLine.PK.Find((PXGraph) this.Base, row.POType, row.PONbr, row.POLineNbr);
      if (poLine == null || !poLine.ProjectID.HasValue)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.projectID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) poLine.ProjectID;
    }
  }

  private bool IsDefaultFromLocation(PX.Objects.PO.POReceiptLine row)
  {
    if (!this.IsStockItem(row))
      return false;
    int? nullable = row.ProjectID;
    if (!nullable.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return true;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
    if (pmProject != null && pmProject.AccountingMode == "L")
    {
      nullable = row.LocationID;
      if (nullable.HasValue)
        return true;
    }
    return false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine> e)
  {
    PX.Objects.PO.POReceiptLine row = e.Row;
    if (row == null)
      return;
    bool flag1 = !string.IsNullOrEmpty(row.POType) && !string.IsNullOrEmpty(row.PONbr) && row.POLineNbr.HasValue;
    if (row.Released.GetValueOrDefault())
      return;
    bool flag2 = ProjectAttribute.IsPMVisible("PO");
    bool flag3 = false;
    if (this.IsStockItem(e.Row))
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
      if (inLocation != null)
        flag3 = inLocation.ProjectID.HasValue;
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) row, flag2 && !flag1 && !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) row, flag2 && !flag3);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID> e)
  {
    PX.Objects.PO.POReceiptLine row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("PO") || !row.SiteID.HasValue || !row.ProjectID.HasValue)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
    if (!this.IsStockItem(row) || pmProject == null || !pmProject.NonProject.HasValue)
      return;
    if (pmProject.AccountingMode == "L")
    {
      if (!row.LocationID.HasValue || !ProjectDefaultAttribute.IsProject((PXGraph) this.Base, row.ProjectID))
        return;
      PXResultset<PMTask> pxResultset = PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<INLocation.projectID>>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PMTask.visibleInPO, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INLocation.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) row.ProjectID,
        (object) row.SiteID,
        (object) row.LocationID
      });
      HashSet<int> source1 = new HashSet<int>();
      HashSet<int> source2 = new HashSet<int>();
      foreach (PXResult<PMTask, INLocation> pxResult in pxResultset)
      {
        PMTask pmTask = PXResult<PMTask, INLocation>.op_Implicit(pxResult);
        INLocation inLocation = PXResult<PMTask, INLocation>.op_Implicit(pxResult);
        HashSet<int> intSet1 = source1;
        int? taskId1 = pmTask.TaskID;
        int num1 = taskId1.Value;
        intSet1.Add(num1);
        taskId1 = pmTask.TaskID;
        int? taskId2 = inLocation.TaskID;
        if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue)
        {
          HashSet<int> intSet2 = source2;
          taskId2 = pmTask.TaskID;
          int num2 = taskId2.Value;
          intSet2.Add(num2);
        }
      }
      PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) row.POType,
        (object) row.PONbr,
        (object) row.POLineNbr
      }));
      if (poLine != null && poLine.TaskID.HasValue && source2.Contains(poLine.TaskID.Value))
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) poLine.TaskID;
      else if (source2.Count > 0)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) source2.First<int>();
      }
      else
      {
        if (source1.Count <= 0)
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) source1.First<int>();
      }
    }
    else
    {
      PX.Objects.PO.POLine poLine = PX.Objects.PO.POLine.PK.Find((PXGraph) this.Base, row.POType, row.PONbr, row.POLineNbr);
      if (poLine == null || !poLine.TaskID.HasValue)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) poLine.TaskID;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID> e)
  {
    PX.Objects.PO.POReceiptLine row = e.Row;
    if (row == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue is int))
      return;
    this.CheckOrderTaskRule(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>>) e).Cache, row, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.taskID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID> e)
  {
    PX.Objects.PO.POReceiptLine row = e.Row;
    if (row == null)
      return;
    this.CheckLocationTaskRule(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>>) e).Cache, row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID> e)
  {
    if (!this.IsStockItem(e.Row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>>) e).Cache.SetDefaultExt<PX.Objects.PO.POReceiptLine.projectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>>) e).Cache.GetValuePending<PX.Objects.PO.POReceiptLine.taskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>>) e).Cache.SetValuePending<PX.Objects.PO.POReceiptLine.taskID>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>>) e).Cache.SetDefaultExt<PX.Objects.PO.POReceiptLine.taskID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine> e)
  {
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    this.CheckForSingleLocation(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine>>) e).Cache, e.Row);
    this.CheckLocationTaskRule(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine>>) e).Cache, e.Row, (object) e.Row.LocationID);
    this.CheckOrderTaskRule(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine>>) e).Cache, e.Row, e.Row.TaskID);
  }

  [PXOverride]
  public virtual void Copy(
    PX.Objects.PO.POReceiptLine aDest,
    PX.Objects.PO.POLine aSrc,
    Decimal aQtyAdj,
    Decimal aBaseQtyAdj,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, Decimal, Decimal> baseMethod)
  {
    baseMethod(aDest, aSrc, aQtyAdj, aBaseQtyAdj);
    if (!POLineType.IsStockNonDropShip(aDest.LineType) || !aSrc.TaskID.HasValue)
      return;
    this.DeriveLocationFromSourceForStockItem(aDest, aSrc);
  }

  protected virtual void DeriveLocationFromSourceForStockItem(PX.Objects.PO.POReceiptLine target, PX.Objects.PO.POLine source)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, target.ProjectID);
    if (pmProject?.AccountingMode == "L" && !pmProject.NonProject.GetValueOrDefault())
    {
      this.DeriveLocationFromSource(target, source);
    }
    else
    {
      INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this.Base, target.InventoryID, target.SiteID);
      if (inItemSite == null)
        return;
      target.LocationID = inItemSite.DfltReceiptLocationID;
    }
  }

  protected virtual void CheckLocationTaskRule(
    PXCache sender,
    PX.Objects.PO.POReceiptLine row,
    object newLocationID)
  {
    if (newLocationID == null || !POLineType.IsStockNonDropShip(row.LineType) || !row.SiteID.HasValue || !row.ProjectID.HasValue)
      return;
    PMProject pmProject1 = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
    if (pmProject1 == null || pmProject1.NonProject.GetValueOrDefault())
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, (int?) newLocationID);
    if (pmProject1.AccountingMode == "L")
    {
      int? nullable1 = (int?) inLocation?.ProjectID;
      int? nullable2 = nullable1 ?? ProjectDefaultAttribute.NonProject();
      if (inLocation != null)
      {
        int? projectId = row.ProjectID;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        int? nullable4 = nullable2;
        if (!EnumerableExtensions.IsIn<int?>(projectId, nullable3, nullable4))
          throw new PXSetPropertyException("The selected location is not assigned to the {0} project that is specified in the purchase order. Please specify another location.", (PXErrorLevel) 4, new object[1]
          {
            sender.GetValueExt<PX.Objects.PO.POReceiptLine.projectID>((object) row)
          })
          {
            ErrorValue = (object) inLocation.LocationCD
          };
      }
      if (row.POType == null || row.PONbr == null)
        return;
      nullable1 = row.POLineNbr;
      if (!nullable1.HasValue)
        return;
      PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) row.POType,
        (object) row.PONbr,
        (object) row.POLineNbr
      }));
      if (poLine == null)
        return;
      nullable1 = poLine.TaskID;
      if (!nullable1.HasValue || inLocation == null)
        return;
      nullable1 = inLocation.ProjectID;
      int? nullable5 = poLine.ProjectID;
      if (nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue)
      {
        nullable5 = inLocation.TaskID;
        nullable1 = poLine.TaskID;
        if (nullable5.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable5.HasValue == nullable1.HasValue)
          return;
        nullable1 = inLocation.TaskID;
        if (!nullable1.HasValue)
          return;
      }
      if (((PXSelectBase<POSetup>) this.Base.posetup).Current.OrderRequestApproval.GetValueOrDefault())
        throw new PXSetPropertyException("The specified warehouse location is linked to the project and project task which differ from those in the corresponding purchase order line. Select a warehouse location that is linked to the {0} project and the {1} project task.", (PXErrorLevel) 4, new object[2]
        {
          (object) PMProject.PK.Find((PXGraph) this.Base, row.ProjectID)?.ContractCD,
          (object) PMTask.PK.Find((PXGraph) this.Base, row.ProjectID, row.TaskID)?.TaskCD
        })
        {
          ErrorValue = (object) inLocation.LocationCD
        };
      sender.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.locationID>((object) row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The specified warehouse location is linked to the project and project task which differ from those in the corresponding purchase order line. Select a warehouse location that is linked to the {0} project and the {1} project task.", (PXErrorLevel) 2));
    }
    else
    {
      if (!inLocation.ProjectID.HasValue)
        return;
      int? projectId1 = inLocation.ProjectID;
      int? projectId2 = row.ProjectID;
      if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue))
      {
        PMProject pmProject2 = PMProject.PK.Find((PXGraph) this.Base, inLocation.ProjectID);
        PMProject pmProject3 = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
        throw new PXSetPropertyException("The items cannot be received to the warehouse location {0} because it is linked to the {1} project. To be able to receive the items for the {2} project that is selected in the purchase receipt line, select a warehouse location that is not linked to any project.", (PXErrorLevel) 4, new object[3]
        {
          (object) inLocation.LocationCD,
          (object) pmProject2?.ContractCD,
          (object) pmProject3?.ContractCD
        })
        {
          ErrorValue = (object) inLocation.LocationCD
        };
      }
    }
  }

  protected virtual void CheckOrderTaskRule(PXCache sender, PX.Objects.PO.POReceiptLine row, int? newTaskID)
  {
    if (row.POType == null || row.PONbr == null || !row.POLineNbr.HasValue || POLineType.IsStock(row.LineType) && !EnumerableExtensions.IsIn<string>(row.LineType, "GP", "PG"))
      return;
    PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) row.POType,
      (object) row.PONbr,
      (object) row.POLineNbr
    }));
    if (poLine == null || !poLine.TaskID.HasValue)
      return;
    int? taskId = poLine.TaskID;
    int? nullable = newTaskID;
    if (taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this.Base, row.ProjectID, row.TaskID);
    string str1;
    if (dirty == null)
    {
      str1 = (string) null;
    }
    else
    {
      string str2 = str1 = dirty.TaskCD;
    }
    string str3 = str1;
    if (((PXSelectBase<POSetup>) this.Base.posetup).Current.OrderRequestApproval.GetValueOrDefault())
      throw new PXSetPropertyException("Given Task differs from the Project Task selected on Purchase Order line. Since Purchase Order document explicitly requires approval and was approved Project Task cannot be changed on the Receipt.", (PXErrorLevel) 4)
      {
        ErrorValue = (object) str3
      };
    sender.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.taskID>((object) row, (object) str3, (Exception) new PXSetPropertyException("Given Task differs from the Project Task selected on Purchase Order line.", (PXErrorLevel) 2));
  }

  protected virtual bool CheckForSingleLocation(PXCache sender, PX.Objects.PO.POReceiptLine row)
  {
    if (POLineType.IsStockNonDropShip(row.LineType))
    {
      int? nullable = row.TaskID;
      if (nullable.HasValue)
      {
        nullable = row.LocationID;
        if (!nullable.HasValue)
        {
          Decimal? baseReceiptQty = row.BaseReceiptQty;
          Decimal num = 0M;
          if (baseReceiptQty.GetValueOrDefault() > num & baseReceiptQty.HasValue)
          {
            sender.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.locationID>((object) row, (object) null, (Exception) new PXSetPropertyException("When posting to Project Location must be the same for all splits."));
            return false;
          }
        }
      }
    }
    return true;
  }

  private void DeriveLocationFromSource(PX.Objects.PO.POReceiptLine target, PX.Objects.PO.POLine source)
  {
    PXResultset<INLocation> pxResultset = PXSelectBase<INLocation, PXSelectReadonly<INLocation, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.projectID, Equal<Required<INLocation.projectID>>, And<INLocation.taskID, Equal<Required<INLocation.taskID>>, And<INLocation.active, Equal<True>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) source.SiteID,
      (object) source.ProjectID,
      (object) source.TaskID
    });
    if (pxResultset.Count == 0)
    {
      INLocation inLocation = PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXSelectReadonly<INLocation, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.projectID, Equal<Required<INLocation.projectID>>, And<INLocation.taskID, IsNull, And<INLocation.active, Equal<True>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) source.SiteID,
        (object) source.ProjectID
      }));
      if (inLocation != null)
      {
        target.LocationID = inLocation.LocationID;
      }
      else
      {
        target.LocationID = new int?();
        target.ProjectID = new int?();
        target.TaskID = new int?();
      }
    }
    else if (pxResultset.Count == 1)
    {
      target.LocationID = PXResult<INLocation>.op_Implicit(pxResultset[0]).LocationID;
    }
    else
    {
      target.LocationID = new int?();
      target.ProjectID = new int?();
      target.TaskID = new int?();
    }
  }

  private bool IsStockItem(PX.Objects.PO.POReceiptLine row)
  {
    if (row == null || row.LineType == null)
      return false;
    return row.LineType == "GI" || row.LineType == "GS" || row.LineType == "GF" || row.LineType == "GR" || row.LineType == "GM" || row.LineType == "GP";
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateAPDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.Released.GetValueOrDefault())
    {
      Decimal? unbilledQty = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.UnbilledQty;
      Decimal num = 0M;
      if (!(unbilledQty.GetValueOrDefault() == num & unbilledQty.HasValue))
        this.ValidateLines();
    }
    return ((PXAction) this.Base.createAPDocument).Press(adapter);
  }

  public virtual void ValidateLines()
  {
    bool flag = false;
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
      if (poReceiptLine.TaskID.HasValue)
      {
        PMProject pmProject = PXSelectorAttribute.Select<PX.Objects.PO.POLine.projectID>(((PXSelectBase) this.Base.transactions).Cache, (object) poReceiptLine) as PMProject;
        if (!pmProject.IsActive.GetValueOrDefault())
        {
          PXUIFieldAttribute.SetError<PX.Objects.PO.POLine.projectID>(((PXSelectBase) this.Base.transactions).Cache, (object) poReceiptLine, "Project is not Active.", pmProject.ContractCD);
          flag = true;
        }
        else
        {
          PMTask pmTask = PXSelectorAttribute.Select<PX.Objects.PO.POLine.taskID>(((PXSelectBase) this.Base.transactions).Cache, (object) poReceiptLine) as PMTask;
          if (!pmTask.IsActive.GetValueOrDefault())
          {
            PXUIFieldAttribute.SetError<PX.Objects.PO.POLine.taskID>(((PXSelectBase) this.Base.transactions).Cache, (object) poReceiptLine, "Project Task is not Active.", pmTask.TaskCD);
            flag = true;
          }
        }
      }
    }
    if (flag)
      throw new PXException("One or more records in the document is invalid.");
  }
}
