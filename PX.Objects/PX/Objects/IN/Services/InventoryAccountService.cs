// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Services.InventoryAccountService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.IN.Services;

public class InventoryAccountService : IInventoryAccountService
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public virtual int? GetAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return this.GetAcctID<Field>(graph, AcctDefault, InventoryAccountServiceHelper.Params(item, site, postclass));
  }

  public virtual int? GetAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    InventoryAccountServiceParams @params)
    where Field : IBqlField
  {
    AcctDefault = InventoryAccountService.UseDifferentDefaultIfProjectIsNonProjOrNull<Field>(AcctDefault, @params.Postclass, @params.Project, @params.Task);
    switch (AcctDefault)
    {
      case "W":
        PXCache cach1 = graph.Caches[typeof (INSite)];
        try
        {
          return new int?((int) cach1.GetValue<Field>((object) @params.Site));
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach1.GetStateExt<INSite.siteCD>((object) @params.Site);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Warehouse",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach1),
            stateExt
          });
        }
      case "P":
        PXCache cach2 = graph.Caches[typeof (INPostClass)];
        try
        {
          return new int?((int) cach2.GetValue<Field>((object) @params.Postclass));
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach2.GetStateExt<INPostClass.postClassID>((object) @params.Postclass);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Posting Class",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach2),
            stateExt
          });
        }
      case "J":
        PXCache cach3 = graph.Caches[typeof (PMProject)];
        try
        {
          if (typeof (Field) == typeof (INPostClass.salesAcctID))
            return new int?(@params.Project.DefaultSalesAccountID.Value);
          if (typeof (Field) == typeof (INPostClass.cOGSAcctID))
            return new int?(@params.Project.DefaultExpenseAccountID.Value);
          throw new NullReferenceException();
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach3.GetStateExt<PMProject.contractID>((object) @params.Project);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Project",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach3),
            stateExt
          });
        }
      case "T":
        PXCache cach4 = graph.Caches[typeof (PMTask)];
        try
        {
          if (typeof (Field) == typeof (INPostClass.salesAcctID))
            return new int?(@params.Task.DefaultSalesAccountID.Value);
          if (typeof (Field) == typeof (INPostClass.cOGSAcctID))
            return new int?(@params.Task.DefaultExpenseAccountID.Value);
          throw new NullReferenceException();
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach4.GetStateExt<PMTask.taskID>((object) @params.Task);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Project Task",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach4),
            stateExt
          });
        }
      default:
        PXCache cach5 = graph.Caches[typeof (PX.Objects.IN.InventoryItem)];
        try
        {
          return new int?((int) cach5.GetValue<Field>((object) @params.Item));
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach5.GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) @params.Item);
          if (@params.Item.StkItem.GetValueOrDefault())
            throw new PXMaskArgumentException(new object[3]
            {
              (object) "Inventory Item",
              (object) PXUIFieldAttribute.GetDisplayName<Field>(cach5),
              stateExt
            });
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Inventory Item",
            (object) this.GetSubstFieldDesr<Field>(graph.Caches[typeof (NonStockItem)]),
            stateExt
          });
        }
    }
  }

  private static string UseDifferentDefaultIfProjectIsNonProjOrNull<Field>(
    string AcctDefault,
    INPostClass postclass,
    IProjectAccountsSource project,
    IProjectTaskAccountsSource task)
    where Field : IBqlField
  {
    int? expenseAccountId1 = (int?) project?.DefaultExpenseAccountID;
    int? expenseAccountId2 = (int?) task?.DefaultExpenseAccountID;
    int? nullable1;
    int? nullable2;
    if (AcctDefault == "J")
    {
      nullable1 = (int?) project?.ProjectID;
      nullable2 = InventoryAccountService.GetNonProjectCode();
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue || project == null || !expenseAccountId1.HasValue)
        AcctDefault = "P";
    }
    if (AcctDefault == "T")
    {
      nullable2 = (int?) project?.ProjectID;
      nullable1 = InventoryAccountService.GetNonProjectCode();
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue || project == null || task == null || !expenseAccountId2.HasValue)
        AcctDefault = "P";
    }
    return AcctDefault;
  }

  public virtual string GetSubstFieldDesr<Field>(PXCache cache) where Field : IBqlField
  {
    if (typeof (Field) == typeof (INPostClass.invtAcctID))
      return PXUIFieldAttribute.GetDisplayName<NonStockItem.invtAcctID>(cache);
    return typeof (Field) == typeof (INPostClass.cOGSAcctID) ? PXUIFieldAttribute.GetDisplayName<NonStockItem.cOGSAcctID>(cache) : PXUIFieldAttribute.GetDisplayName<Field>(cache);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public virtual int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    INTran tran)
    where Field : IBqlField
  {
    return this.GetSubID<Field>(graph, AcctDefault, SubMask, InventoryAccountServiceHelper.Params(item, site, postclass, tran));
  }

  public virtual int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    InventoryAccountServiceParams @params)
    where Field : IBqlField
  {
    if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.UseTransaction && @params.Postclass.COGSSubFromSales.GetValueOrDefault())
    {
      PXCache cach = graph.Caches[typeof (INTran)];
      object valueExt = cach.GetValueExt<INTran.subID>((object) @params.INTran);
      object subId = valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
      cach.RaiseFieldUpdating<Field>((object) @params.INTran, ref subId);
      return (int?) subId;
    }
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    int? nullable3 = new int?();
    int? project_SubID = new int?();
    int? projectTask_SubID = new int?();
    int? nullable4;
    int? nullable5;
    int? class_SubID;
    if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.Postclass.COGSSubFromSales.GetValueOrDefault())
    {
      nullable4 = (int?) graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) @params.Item);
      nullable5 = (int?) graph.Caches[typeof (INSite)].GetValue<INSite.salesSubID>((object) @params.Site);
      class_SubID = (int?) graph.Caches[typeof (INPostClass)].GetValue<INPostClass.salesSubID>((object) @params.Postclass);
    }
    else
    {
      nullable4 = (int?) graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<Field>((object) @params.Item);
      nullable5 = (int?) graph.Caches[typeof (INSite)].GetValue<Field>((object) @params.Site);
      class_SubID = (int?) graph.Caches[typeof (INPostClass)].GetValue<Field>((object) @params.Postclass);
    }
    InventoryAccountService.GetProjectAndTaskSub<Field>(graph, ref SubMask, @params, class_SubID, out project_SubID, out projectTask_SubID);
    object subId1 = (object) null;
    try
    {
      if (@params.Item.StkItem.GetValueOrDefault() && typeof (Field) == typeof (INPostClass.invtSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.invtSubMask>(graph, SubMask, @params.Item.StkItem, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (PX.Objects.IN.InventoryItem.invtSubID),
          typeof (INSite.invtSubID),
          typeof (INPostClass.invtSubID)
        });
      if (!@params.Item.StkItem.GetValueOrDefault() && typeof (Field) == typeof (INPostClass.invtSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.invtSubMask>(graph, SubMask, @params.Item.StkItem, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (NonStockItem.invtSubID),
          typeof (INSite.invtSubID),
          typeof (INPostClass.invtSubID)
        });
      if (@params.Item.StkItem.GetValueOrDefault() && typeof (Field) == typeof (INPostClass.cOGSSubID))
        subId1 = (object) SalesCOGSSubAccountMaskAttribute.MakeSub<INPostClass.cOGSSubMask>(graph, SubMask, @params.Item.StkItem, new object[5]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID,
          (object) project_SubID,
          (object) projectTask_SubID
        }, new Type[5]
        {
          typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
          typeof (INSite.cOGSSubID),
          typeof (INPostClass.cOGSSubID),
          typeof (PMProject.defaultExpenseSubID),
          typeof (PMTask.defaultExpenseSubID)
        });
      if (!@params.Item.StkItem.GetValueOrDefault() && typeof (Field) == typeof (INPostClass.cOGSSubID))
        subId1 = (object) SalesCOGSSubAccountMaskAttribute.MakeSub<INPostClass.cOGSSubMask>(graph, SubMask, @params.Item.StkItem, new object[5]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID,
          (object) project_SubID,
          (object) projectTask_SubID
        }, new Type[5]
        {
          typeof (NonStockItem.cOGSSubID),
          typeof (INSite.cOGSSubID),
          typeof (INPostClass.cOGSSubID),
          typeof (PMProject.defaultExpenseSubID),
          typeof (PMTask.defaultExpenseSubID)
        });
      if (typeof (Field) == typeof (INPostClass.salesSubID))
        subId1 = (object) SalesCOGSSubAccountMaskAttribute.MakeSub<INPostClass.salesSubMask>(graph, SubMask, new object[5]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID,
          (object) project_SubID,
          (object) projectTask_SubID
        }, new Type[5]
        {
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (INSite.salesSubID),
          typeof (INPostClass.salesSubID),
          typeof (PMProject.defaultExpenseSubID),
          typeof (PMTask.defaultExpenseSubID)
        });
      if (typeof (Field) == typeof (INPostClass.stdCstVarSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.stdCstVarSubMask>(graph, SubMask, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (PX.Objects.IN.InventoryItem.stdCstVarSubID),
          typeof (INSite.stdCstVarSubID),
          typeof (INPostClass.stdCstVarSubID)
        });
      if (typeof (Field) == typeof (INPostClass.stdCstRevSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.stdCstRevSubMask>(graph, SubMask, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (PX.Objects.IN.InventoryItem.stdCstRevSubID),
          typeof (INSite.stdCstRevSubID),
          typeof (INPostClass.stdCstRevSubID)
        });
      if (typeof (Field) == typeof (INPostClass.pOAccrualSubID))
        throw new NotImplementedException();
      if (typeof (Field) == typeof (INPostClass.pPVSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.pPVSubMask>(graph, SubMask, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (PX.Objects.IN.InventoryItem.pPVSubID),
          typeof (INSite.pPVSubID),
          typeof (INPostClass.pPVSubID)
        });
      if (typeof (Field) == typeof (INPostClass.lCVarianceSubID))
        subId1 = (object) PX.Objects.IN.SubAccountMaskAttribute.MakeSub<INPostClass.lCVarianceSubMask>(graph, SubMask, new object[3]
        {
          (object) nullable4,
          (object) nullable5,
          (object) class_SubID
        }, new Type[3]
        {
          typeof (PX.Objects.IN.InventoryItem.lCVarianceSubID),
          typeof (INSite.lCVarianceSubID),
          typeof (INPostClass.lCVarianceSubID)
        });
    }
    catch (PXMaskArgumentException ex)
    {
      object stateExt;
      switch (ex.SourceIdx)
      {
        case 1:
          stateExt = graph.Caches[typeof (INSite)].GetStateExt<INSite.siteCD>((object) @params.Site);
          break;
        case 2:
          stateExt = graph.Caches[typeof (INPostClass)].GetStateExt<INPostClass.postClassID>((object) @params.Postclass);
          break;
        case 3:
          stateExt = graph.Caches[typeof (PMProject)].GetStateExt<PMProject.contractCD>((object) @params.Project);
          break;
        case 4:
          stateExt = graph.Caches[typeof (PMTask)].GetStateExt<PMTask.taskCD>((object) @params.Task);
          break;
        default:
          stateExt = graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) @params.Item);
          break;
      }
      throw new PXMaskArgumentException(ex, new object[1]
      {
        stateExt
      });
    }
    switch (AcctDefault)
    {
      case "W":
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (INSite)], (object) @params.Site, ref subId1);
        break;
      case "P":
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (INPostClass)], (object) @params.Postclass, ref subId1);
        break;
      case "J":
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.Postclass.COGSSubFromSales.GetValueOrDefault() || typeof (Field) == typeof (INPostClass.salesSubID))
          InventoryAccountService.RaiseFieldUpdating<PMProject.defaultSalesSubID>(graph.Caches[typeof (PMProject)], (object) @params.Project, ref subId1);
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && !@params.Postclass.COGSSubFromSales.GetValueOrDefault())
        {
          InventoryAccountService.RaiseFieldUpdating<PMProject.defaultExpenseSubID>(graph.Caches[typeof (PMProject)], (object) @params.Project, ref subId1);
          break;
        }
        break;
      case "T":
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.Postclass.COGSSubFromSales.GetValueOrDefault() || typeof (Field) == typeof (INPostClass.salesSubID))
          InventoryAccountService.RaiseFieldUpdating<PMTask.defaultSalesSubID>(graph.Caches[typeof (PMTask)], (object) @params.Task, ref subId1);
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && !@params.Postclass.COGSSubFromSales.GetValueOrDefault())
        {
          InventoryAccountService.RaiseFieldUpdating<PMTask.defaultExpenseSubID>(graph.Caches[typeof (PMTask)], (object) @params.Task, ref subId1);
          break;
        }
        break;
      default:
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) @params.Item, ref subId1);
        break;
    }
    return (int?) subId1;
  }

  private static void GetProjectAndTaskSub<Field>(
    PXGraph graph,
    ref string SubMask,
    InventoryAccountServiceParams @params,
    int? class_SubID,
    out int? project_SubID,
    out int? projectTask_SubID)
    where Field : IBqlField
  {
    project_SubID = new int?();
    projectTask_SubID = new int?();
    if (!InventoryAccountService.ProjectsEnabled() && SubMask != null)
      SubMask = SubMask.Replace("J", "P").Replace("T", "P");
    int? nullable1;
    int? nullable2;
    if (InventoryAccountService.ProjectsEnabled())
    {
      nullable1 = (int?) @params.Project?.ProjectID;
      nullable2 = InventoryAccountService.GetNonProjectCode();
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && @params.Project != null)
      {
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.Postclass.COGSSubFromSales.GetValueOrDefault() || typeof (Field) == typeof (INPostClass.salesSubID))
        {
          ref int? local = ref project_SubID;
          IProjectAccountsSource project = @params.Project;
          int? nullable3;
          if (project == null)
          {
            nullable1 = new int?();
            nullable3 = nullable1;
          }
          else
            nullable3 = project.DefaultSalesSubID;
          nullable2 = nullable3;
          int? nullable4 = nullable2 ?? class_SubID;
          local = nullable4;
        }
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && !@params.Postclass.COGSSubFromSales.GetValueOrDefault())
        {
          ref int? local = ref project_SubID;
          IProjectAccountsSource project = @params.Project;
          int? nullable5;
          if (project == null)
          {
            nullable1 = new int?();
            nullable5 = nullable1;
          }
          else
            nullable5 = project.DefaultExpenseSubID;
          nullable2 = nullable5;
          int? nullable6 = nullable2 ?? class_SubID;
          local = nullable6;
          goto label_15;
        }
        goto label_15;
      }
    }
    project_SubID = class_SubID;
label_15:
    if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      nullable2 = (int?) @params.Project?.ProjectID;
      nullable1 = InventoryAccountService.GetNonProjectCode();
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) && @params.Project != null && @params.Task != null)
      {
        if (typeof (Field) == typeof (INPostClass.cOGSSubID) && @params.Postclass.COGSSubFromSales.GetValueOrDefault() || typeof (Field) == typeof (INPostClass.salesSubID))
        {
          ref int? local = ref projectTask_SubID;
          nullable1 = @params.Task.DefaultSalesSubID;
          int? nullable7 = nullable1 ?? class_SubID;
          local = nullable7;
        }
        if (!(typeof (Field) == typeof (INPostClass.cOGSSubID)) || @params.Postclass.COGSSubFromSales.GetValueOrDefault())
          return;
        ref int? local1 = ref projectTask_SubID;
        nullable1 = @params.Task.DefaultExpenseSubID;
        int? nullable8 = nullable1 ?? class_SubID;
        local1 = nullable8;
        return;
      }
    }
    projectTask_SubID = class_SubID;
  }

  private static bool ProjectsEnabled()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  private static int? GetNonProjectCode() => ProjectDefaultAttribute.NonProject();

  public static void RaiseFieldUpdating<Field>(PXCache cache, object item, ref object value) where Field : IBqlField
  {
    try
    {
      cache.RaiseFieldUpdating<Field>(item, ref value);
    }
    catch (PXSetPropertyException ex)
    {
      string name = typeof (Field).Name;
      string itemName = PXUIFieldAttribute.GetItemName(cache);
      string str = PXUIFieldAttribute.GetDisplayName(cache, name);
      string message = ((Exception) ex).Message;
      if (str != null && name != str)
      {
        int startIndex = message.IndexOf(name, StringComparison.OrdinalIgnoreCase);
        if (startIndex >= 0)
          message.Remove(startIndex, name.Length).Insert(startIndex, str);
      }
      else
        str = name;
      throw new PXSetPropertyException("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) $"{itemName} {str}",
        value
      });
    }
  }

  public virtual int? GetPOAccrualAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField
  {
    switch (AcctDefault)
    {
      case "W":
        PXCache cach1 = graph.Caches[typeof (INSite)];
        try
        {
          return (int?) cach1.GetValue<Field>((object) site);
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach1.GetStateExt<INSite.siteCD>((object) site);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Warehouse",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach1),
            stateExt
          });
        }
      case "P":
        PXCache cach2 = graph.Caches[typeof (INPostClass)];
        try
        {
          return (int?) cach2.GetValue<Field>((object) postclass);
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach2.GetStateExt<INPostClass.postClassID>((object) postclass);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Posting Class",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach2),
            stateExt
          });
        }
      case "V":
        PXCache cach3 = graph.Caches[typeof (PX.Objects.AP.Vendor)];
        try
        {
          return (int?) cach3.GetValue<Field>((object) vendor);
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach3.GetStateExt<PX.Objects.AP.Vendor.bAccountID>((object) vendor);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Vendor",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach3),
            stateExt
          });
        }
      default:
        PXCache cach4 = graph.Caches[typeof (PX.Objects.IN.InventoryItem)];
        try
        {
          return (int?) cach4.GetValue<Field>((object) item);
        }
        catch (NullReferenceException ex)
        {
          object stateExt = cach4.GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) item);
          throw new PXMaskArgumentException(new object[3]
          {
            (object) "Inventory Item",
            (object) PXUIFieldAttribute.GetDisplayName<Field>(cach4),
            stateExt
          });
        }
    }
  }

  public virtual int? GetPOAccrualSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField
  {
    int? nullable1 = (int?) graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<Field>((object) item);
    int? nullable2 = (int?) graph.Caches[typeof (INSite)].GetValue<Field>((object) site);
    int? nullable3 = (int?) graph.Caches[typeof (INPostClass)].GetValue<Field>((object) postclass);
    int? nullable4 = (int?) graph.Caches[typeof (PX.Objects.AP.Vendor)].GetValue<Field>((object) vendor);
    object poAccrualSubId;
    try
    {
      poAccrualSubId = (object) POAccrualSubAccountMaskAttribute.MakeSub<INPostClass.pOAccrualSubMask>(graph, SubMask, new object[4]
      {
        (object) nullable1,
        (object) nullable2,
        (object) nullable3,
        (object) nullable4
      }, new Type[4]
      {
        typeof (PX.Objects.IN.InventoryItem.pOAccrualSubID),
        typeof (INSite.pOAccrualSubID),
        typeof (INPostClass.pOAccrualSubID),
        typeof (PX.Objects.AP.Vendor.pOAccrualSubID)
      });
    }
    catch (PXMaskArgumentException ex)
    {
      object stateExt;
      switch (ex.SourceIdx)
      {
        case 1:
          stateExt = graph.Caches[typeof (INSite)].GetStateExt<INSite.siteCD>((object) site);
          break;
        case 2:
          stateExt = graph.Caches[typeof (INPostClass)].GetStateExt<INPostClass.postClassID>((object) postclass);
          break;
        case 3:
          stateExt = graph.Caches[typeof (PX.Objects.AP.Vendor)].GetStateExt<PX.Objects.AP.Vendor.bAccountID>((object) vendor);
          break;
        default:
          stateExt = graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) item);
          break;
      }
      throw new PXMaskArgumentException(ex, new object[1]
      {
        stateExt
      });
    }
    switch (AcctDefault)
    {
      case "W":
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (INSite)], (object) site, ref poAccrualSubId);
        break;
      case "P":
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (INPostClass)], (object) postclass, ref poAccrualSubId);
        break;
      case "V":
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (PX.Objects.AP.Vendor)], (object) vendor, ref poAccrualSubId);
        break;
      default:
        InventoryAccountService.RaiseFieldUpdating<Field>(graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) item, ref poAccrualSubId);
        break;
    }
    return (int?) poAccrualSubId;
  }
}
