// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectInventoryTransferProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

public class ProjectInventoryTransferProcess : PXGraph<
#nullable disable
ProjectInventoryTransferProcess>
{
  public PXFilter<ProjectInventoryTransferFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXProcessingViewOf<ProjectInventoryStatus>.BasedOn<SelectFromBase<ProjectInventoryStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProjectInventoryTransferFilter.projectID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  ProjectInventoryTransferFilter.projectID>, IBqlInt>.IsEqual<
  #nullable disable
  ProjectInventoryStatus.projectID>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProjectInventoryTransferFilter.projectTaskID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  ProjectInventoryTransferFilter.projectTaskID>, IBqlInt>.IsEqual<
  #nullable disable
  ProjectInventoryStatus.taskID>>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ProjectInventoryTransferFilter.inventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  ProjectInventoryTransferFilter.inventoryID>, IBqlInt>.IsEqual<
  #nullable disable
  ProjectInventoryStatus.inventoryID>>>>>>.FilteredBy<ProjectInventoryTransferFilter> Items;
  public PXCancel<ProjectInventoryTransferFilter> Cancel;
  public PXAction<ProjectInventoryStatus> ViewInventoryItem;
  public PXAction<ProjectInventoryStatus> ViewProject;

  public ProjectInventoryTransferProcess()
  {
    ((PXProcessingBase<ProjectInventoryStatus>) this.Items).SetSelected<ProjectInventoryStatus.selected>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<ProjectInventoryStatus>) this.Items).SetProcessDelegate(ProjectInventoryTransferProcess.\u003C\u003Ec.\u003C\u003E9__2_0 ?? (ProjectInventoryTransferProcess.\u003C\u003Ec.\u003C\u003E9__2_0 = new PXProcessingBase<ProjectInventoryStatus>.ProcessListDelegate((object) ProjectInventoryTransferProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__2_0))));
  }

  public virtual void _(
    PX.Data.Events.RowSelected<ProjectInventoryTransferFilter> e)
  {
    if (e.Row == null || !e.Row.DisableProjectSelection.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<ProjectInventoryTransferFilter.projectID>(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<ProjectInventoryTransferFilter>) this.Filter).Current, false);
    PXUIFieldAttribute.SetVisible<ProjectInventoryStatus.totalCost>(((PXSelectBase) this.Items).Cache, (object) null, ((PXSelectBase<PMProject>) new FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<BqlField<ProjectInventoryTransferFilter.projectID, IBqlInt>.FromCurrent>>, PMProject>.View((PXGraph) this)).SelectSingle(Array.Empty<object>())?.AccountingMode == "P");
  }

  public static void TransferProjectsInventory(
    List<ProjectInventoryStatus> projectsInventoryItemsToTransfer)
  {
    foreach (IGrouping<int?, ProjectInventoryStatus> first in projectsInventoryItemsToTransfer.GroupBy<ProjectInventoryStatus, int?>((Func<ProjectInventoryStatus, int?>) (i => i.SiteID)))
    {
      try
      {
        INTransferEntry instance = PXGraph.CreateInstance<INTransferEntry>();
        INRegister inRegister = ((PXSelectBase<INRegister>) instance.transfer).Insert();
        inRegister.SiteID = first.Key;
        inRegister.ToSiteID = first.Key;
        inRegister.TranDesc = "Transfer of the project stock to the free stock";
        ((PXSelectBase<INRegister>) instance.transfer).Update(inRegister);
        HashSet<ProjectInventoryStatus> second = new HashSet<ProjectInventoryStatus>();
        bool flag = true;
        foreach (ProjectInventoryStatus projectInventoryStatus in (IEnumerable<ProjectInventoryStatus>) first)
        {
          INLocation inLocation = INLocation.PK.Find((PXGraph) instance, projectInventoryStatus.LocationID);
          if (!inLocation.TransfersValid.GetValueOrDefault())
          {
            PXProcessing<ProjectInventoryStatus>.SetError(projectsInventoryItemsToTransfer.IndexOf(projectInventoryStatus), PXMessages.LocalizeFormatNoPrefixNLA("You cannot transfer the items to the {0} location. Either change the location, or select the Transfers Allowed check box on the Warehouses (IN204000) form for this location.", new object[1]
            {
              (object) inLocation.LocationCD
            }));
            second.Add(projectInventoryStatus);
          }
          else
          {
            INTran inTran = ((PXSelectBase<INTran>) instance.transactions).Insert();
            try
            {
              inTran.InventoryID = projectInventoryStatus.InventoryID;
              inTran.LocationID = projectInventoryStatus.LocationID;
              inTran.ToLocationID = projectInventoryStatus.LocationID;
              inTran.CostLayerType = projectInventoryStatus.CostLayerType;
              inTran.ToCostLayerType = "N";
              inTran.InventorySource = "P";
              inTran.ToInventorySource = "F";
              inTran.ProjectID = projectInventoryStatus.ProjectID;
              inTran.ToProjectID = ProjectDefaultAttribute.NonProject();
              inTran.TaskID = projectInventoryStatus.TaskID;
              inTran.ToTaskID = new int?();
              inTran.Qty = projectInventoryStatus.QtyOnHand;
              ((PXSelectBase<INTran>) instance.transactions).Update(inTran);
              flag = false;
            }
            catch (Exception ex)
            {
              PXProcessing<ProjectInventoryStatus>.SetError(projectsInventoryItemsToTransfer.IndexOf(projectInventoryStatus), ex);
              second.Add(projectInventoryStatus);
              ((PXSelectBase<INTran>) instance.transactions).Delete(inTran);
            }
          }
        }
        if (!flag)
        {
          ((PXAction) instance.Save).Press();
          ((PXAction) instance.releaseFromHold).Press();
          ((PXAction) instance.release).Press();
          foreach (ProjectInventoryStatus projectInventoryStatus in first.Except<ProjectInventoryStatus>((IEnumerable<ProjectInventoryStatus>) second))
            PXProcessing<ProjectInventoryStatus>.SetInfo(projectsInventoryItemsToTransfer.IndexOf(projectInventoryStatus), PXMessages.LocalizeFormatNoPrefixNLA("The {0} inventory transfer has been created successfully.", new object[1]
            {
              (object) ((PXSelectBase<INRegister>) instance.transfer).Current.RefNbr
            }));
        }
      }
      catch (Exception ex)
      {
        foreach (ProjectInventoryStatus projectInventoryStatus in (IEnumerable<ProjectInventoryStatus>) first)
          PXProcessing<ProjectInventoryStatus>.SetError(projectsInventoryItemsToTransfer.IndexOf(projectInventoryStatus), ex);
      }
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewInventoryItem(PXAdapter adapter)
  {
    if (((PXSelectBase<ProjectInventoryStatus>) this.Items).Current != null)
    {
      InventoryItemMaint instance = PXGraph.CreateInstance<InventoryItemMaint>();
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) instance.Item).Search<PX.Objects.IN.InventoryItem.inventoryID>((object) ((PXSelectBase<ProjectInventoryStatus>) this.Items).Current.InventoryID, Array.Empty<object>()));
      if (inventoryItem != null)
      {
        ((PXSelectBase<PX.Objects.IN.InventoryItem>) instance.Item).Current = inventoryItem;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewProject(PXAdapter adapter)
  {
    if (((PXSelectBase<ProjectInventoryStatus>) this.Items).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<ProjectInventoryStatus>) this.Items).Current.ProjectID);
    return adapter.Get();
  }
}
