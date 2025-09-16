// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class POOrderEntryExt : CommitmentTracking<POOrderEntry>
{
  public PXSetup<PMSetup> Setup;
  public bool SkipProjectLockCommitmentsVerification;
  private readonly string[] OrderLineTypesToValidate = new string[4]
  {
    "SV",
    "NS",
    "PN",
    "PG"
  };
  private readonly string[] RegularOrderLineTypesToValidateCOGSAccount = new string[2]
  {
    "NS",
    "GI"
  };
  private readonly string[] ProjectDropShipOrderLineTypesToValidateCOGSAccount = new string[1]
  {
    "PN"
  };

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public new static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [Project]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderLine.projectID> e)
  {
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) this.Base).OnBeforePersist += new Action<PXGraph>(this.SetBehaviorBasedOnLines);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID> e)
  {
    if (!this.SkipProjectLockCommitmentsVerification)
      this.VerifyProjectLockCommitments((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>, PX.Objects.PO.POLine, object>) e).NewValue, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>>) e).Cache);
    this.VerifyExchangeRateExistsForProject((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>, PX.Objects.PO.POLine, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.projectID> e)
  {
    if (!this.SkipProjectLockCommitmentsVerification)
      this.VerifyProjectLockCommitments((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.projectID>, PX.Objects.PO.POOrder, object>) e).NewValue, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.projectID>>) e).Cache);
    this.VerifyExchangeRateExistsForProject((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.projectID>, PX.Objects.PO.POOrder, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.PO.POLine, PX.Objects.PO.POLine.lineType> e)
  {
    if (e.Row == null || !(e.Row.LineType == "DN"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POLine, PX.Objects.PO.POLine.lineType>>) e).Cache.SetValueExt<PX.Objects.PO.POLine.projectID>((object) e.Row, (object) ProjectDefaultAttribute.NonProject());
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Change Order Description")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMChangeOrder.description> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Line Description")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMChangeOrderLine.description> e)
  {
  }

  public virtual void VerifyProjectLockCommitments(int? newProjectID, PXCache cache)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.changeOrder>())
      return;
    Type baseType = cache.Graph.GetType().BaseType;
    PMProject project;
    if (!ProjectDefaultAttribute.IsProject((PXGraph) this.Base, newProjectID, out project) || !project.LockCommitments.GetValueOrDefault())
      return;
    if (baseType == typeof (SubcontractEntry))
      throw new PXSetPropertyException("To be able to create a subcontract for the {0} project, open the project on the Projects (PM301000) form and use the Unlock Commitments command on the More menu.", new object[1]
      {
        (object) project.ContractCD
      })
      {
        ErrorValue = (object) project.ContractCD
      };
    if (baseType == typeof (POOrderEntry))
      throw new PXSetPropertyException("To be able to create a purchase order for the {0} project, open the project on the Projects (PM301000) form and use the Unlock Commitments command on the More menu.", new object[1]
      {
        (object) project.ContractCD
      })
      {
        ErrorValue = (object) project.ContractCD
      };
    throw new PXSetPropertyException("To be able to create original purchase order commitments for this project, perform the Unlock Commitments action for the project on the Projects (PM301000) form.")
    {
      ErrorValue = (object) project.ContractCD
    };
  }

  public virtual void VerifyExchangeRateExistsForProject(int? newProjectID)
  {
    PMProject project;
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() || !this.IsCommitmentsEnabled() || !ProjectDefaultAttribute.IsProject((PXGraph) this.Base, newProjectID, out project) || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this.Base, project, ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.CuryID, ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderDate, new Decimal?(100M));
  }

  public static bool IsCommitmentsEnabled(PXGraph graph)
  {
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select(graph, Array.Empty<object>()));
    return pmSetup != null && pmSetup.CostCommitmentTracking.GetValueOrDefault();
  }

  private bool IsCommitmentsEnabled()
  {
    PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
    return current != null && current.CostCommitmentTracking.GetValueOrDefault();
  }

  public virtual void SetBehaviorBasedOnLines(PXGraph obj)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.changeOrder>())
      return;
    bool flag = false;
    if (((PXSelectBase<PMChangeOrderLine>) new PXSelect<PMChangeOrderLine, Where<PMChangeOrderLine.pOOrderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PMChangeOrderLine.pOOrderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>()) != null)
      flag = true;
    if (flag && ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Behavior != "C")
    {
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Behavior = "C";
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Update(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
    }
    else
    {
      if (flag || !(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Behavior == "C"))
        return;
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Behavior = "S";
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Update(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
    }
  }

  [PXOverride]
  public virtual IEnumerable CreateAPInvoice(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseHandler)
  {
    this.ValidatePOLinesBeforeAPInvoiceCreated();
    return baseHandler(adapter);
  }

  protected virtual void ValidatePOLinesBeforeAPInvoiceCreated()
  {
    bool flag = false;
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, poLine.ExpenseAcctID);
      if ((account != null ? (!account.AccountGroupID.HasValue ? 1 : 0) : 1) != 0 && poLine.LineType == "SV" && ProjectDefaultAttribute.IsProject((PXGraph) this.Base, poLine.ProjectID))
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, poLine.InventoryID);
        if (inventoryItem == null || !(inventoryItem.PostToExpenseAccount == "S"))
        {
          PXTrace.WriteError("The account specified in the project-related line must be mapped to an account group. Assign an account group to the {0} account or select the non-project code in the line.", new object[1]
          {
            (object) account.AccountCD
          });
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXException("One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.");
  }

  [PXOverride]
  public virtual void ValidateBeforeOpen() => this.ValidatePOLinesBeforeOpen();

  public virtual void ValidatePOLinesBeforeOpen()
  {
    if (!this.IsCommitmentsEnabled())
      return;
    List<PXException> source = new List<PXException>();
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
      PXException pxException1 = this.ValidateProjectRelatedServiceLineAccount(((PXSelectBase) this.Base.Transactions).Cache, line);
      if (pxException1 != null)
        source.Add(pxException1);
      PXException pxException2 = this.ValidateProjectRelatedLineCOGSAccount(((PXSelectBase) this.Base.Transactions).Cache, ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current, line);
      if (pxException2 != null)
        source.Add(pxException2);
    }
    if (source.Any<PXException>())
      throw new PXException("A project commitment cannot be created for at least one document line. For details, see the trace log.");
  }

  public virtual PXException ValidateProjectRelatedServiceLineAccount(PXCache cache, PX.Objects.PO.POLine line)
  {
    return !((IEnumerable<string>) this.OrderLineTypesToValidate).Contains<string>(line.LineType) ? (PXException) null : POOrderEntryExt.ValidateProjectRelatedLineExpenseAccount((PXGraph) this.Base, cache, line);
  }

  public static PXException ValidateProjectRelatedLineExpenseAccount(
    PXGraph graph,
    PXCache cache,
    PX.Objects.PO.POLine line)
  {
    if (!line.ExpenseAcctID.HasValue)
      return (PXException) null;
    if (!ProjectDefaultAttribute.IsProject(graph, line.ProjectID))
      return (PXException) null;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(graph, (int?) line?.ExpenseAcctID);
    if (account == null || account.AccountGroupID.HasValue)
      return (PXException) null;
    PMProject pmProject = PMProject.PK.Find(graph, line.ProjectID);
    PXSetPropertyException<PX.Objects.PO.POLine.expenseAcctID> propertyException = new PXSetPropertyException<PX.Objects.PO.POLine.expenseAcctID>("The account specified in the project-related line must be mapped to an account group. Assign an account group to the {0} account and recalculate the project balance for the {1} project.", (PXErrorLevel) 4, new object[2]
    {
      (object) account.AccountCD,
      (object) pmProject.ContractCD
    });
    PXTrace.WriteError((Exception) propertyException);
    cache.RaiseExceptionHandling<PX.Objects.PO.POLine.expenseAcctID>((object) line, (object) account.AccountCD, (Exception) propertyException);
    return (PXException) propertyException;
  }

  public virtual PXException ValidateProjectRelatedLineCOGSAccount(
    PXCache cache,
    PX.Objects.PO.POOrder order,
    PX.Objects.PO.POLine line)
  {
    if (order.OrderType == "RO")
    {
      if (!((IEnumerable<string>) this.RegularOrderLineTypesToValidateCOGSAccount).Contains<string>(line.LineType))
        return (PXException) null;
    }
    else
    {
      if (!(order.OrderType == "PD"))
        return (PXException) null;
      if (!((IEnumerable<string>) this.ProjectDropShipOrderLineTypesToValidateCOGSAccount).Contains<string>(line.LineType))
        return (PXException) null;
    }
    return POOrderEntryExt.ValidateProjectRelatedLineCOGSAccount((PXGraph) this.Base, cache, line);
  }

  public static PXException ValidateProjectRelatedLineCOGSAccount(
    PXGraph graph,
    PXCache cache,
    PX.Objects.PO.POLine line)
  {
    if (!line.InventoryID.HasValue || !ProjectDefaultAttribute.IsProject(graph, line.ProjectID))
      return (PXException) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, line.InventoryID);
    if (inventoryItem == null)
      return (PXException) null;
    if (inventoryItem.StkItem.GetValueOrDefault() && inventoryItem.COGSAcctID.HasValue)
    {
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(graph, inventoryItem.COGSAcctID);
      if (account != null && !account.AccountGroupID.HasValue)
      {
        PMProject pmProject = PMProject.PK.Find(graph, line.ProjectID);
        PXSetPropertyException<PX.Objects.PO.POLine.expenseAcctID> propertyException = new PXSetPropertyException<PX.Objects.PO.POLine.expenseAcctID>("The COGS account specified in the item used in a project-related line must be mapped to an account group. Assign an account group to the {0} COGS account and recalculate the project balance for the {1} project.", (PXErrorLevel) 5, new object[2]
        {
          (object) account.AccountCD,
          (object) pmProject.ContractCD
        });
        PXTrace.WriteError((Exception) propertyException);
        cache.RaiseExceptionHandling<PX.Objects.PO.POLine.expenseAcctID>((object) line, (object) account.AccountCD, (Exception) propertyException);
        return (PXException) propertyException;
      }
    }
    return (PXException) null;
  }
}
