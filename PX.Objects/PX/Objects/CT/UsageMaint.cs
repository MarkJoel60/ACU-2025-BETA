// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.UsageMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CT;

public class UsageMaint : PXGraph<
#nullable disable
UsageMaint>
{
  public PXSave<UsageMaint.UsageFilter> Save;
  public PXCancel<UsageMaint.UsageFilter> Cancel;
  public PXCopyPasteAction<UsageMaint.UsageFilter> CopyPaste;
  public PXSelect<PMRegister> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (UsageMaint.UsageFilter.contractID)})]
  public PXFilter<UsageMaint.UsageFilter> Filter;
  public PXSelect<Contract, Where<Contract.contractID, Equal<Current<UsageMaint.UsageFilter.contractID>>>> CurrentContract;
  [PXImport(typeof (UsageMaint.UsageFilter))]
  public PXSelectJoin<PMTran, LeftJoin<CRCase, On<CRCase.noteID, Equal<PMTran.origRefID>, Or<CRCase.caseCD, Equal<PMTran.caseCD>>>>, Where<PMTran.billed, Equal<False>, And<PMTran.projectID, Equal<Current<UsageMaint.UsageFilter.contractID>>>>> UnBilled;
  public PXSelectJoin<PMTran, LeftJoin<CRCase, On<CRCase.noteID, Equal<PMTran.origRefID>, Or<CRCase.caseCD, Equal<PMTran.caseCD>>>>, Where<PMTran.billed, Equal<True>, And<PMTran.projectID, Equal<Current<UsageMaint.UsageFilter.contractID>>>>> Billed;
  public PXSelect<ContractDetailAcum> ContractDetails;
  public PXSetup<ARSetup> arsetup;

  [NonStockItem]
  [PXDefault]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.kitItem, NotEqual<True>>), "A kit cannot be used on the [ScreenName] ([ScreenID]) form.", new System.Type[] {})]
  protected virtual void PMTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Selector<PMTran.inventoryID, PX.Objects.IN.InventoryItem.baseUnit>))]
  [PMUnit(typeof (PMTran.inventoryID))]
  protected virtual void PMTran_UOM_CacheAttached(PXCache sender)
  {
  }

  [CustomerAndProspect(null, null, null)]
  [PXDefault(typeof (Search<Contract.customerID, Where<Contract.contractID, Equal<Current<PMTran.projectID>>>>))]
  protected virtual void PMTran_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Quantity")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void PMTran_BillableQty_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (UsageMaint.UsageFilter.contractID))]
  protected virtual void PMTran_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [Branch(typeof (Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), null, true, true, true, IsDetail = false)]
  protected virtual void PMTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  protected virtual void PMTran_IsQtyOnly_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Type")]
  protected virtual void PMTran_ARTranType_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Reference Nbr.")]
  protected virtual void PMTran_ARRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Billing Date")]
  protected virtual void PMTran_BilledDate_CacheAttached(PXCache sender)
  {
  }

  protected virtual IEnumerable billed()
  {
    UsageMaint.UsageFilter current = ((PXSelectBase<UsageMaint.UsageFilter>) this.Filter).Current;
    if (current == null)
      return (IEnumerable) new List<PMTran>();
    PXSelectBase<PMTran> pxSelectBase = (PXSelectBase<PMTran>) new PXSelectJoin<PMTran, LeftJoin<CRCase, On<CRCase.noteID, Equal<PMTran.origRefID>, Or<CRCase.caseCD, Equal<PMTran.caseCD>>>>, Where<PMTran.billed, Equal<True>, And<PMTran.projectID, Equal<Current<UsageMaint.UsageFilter.contractID>>>>>((PXGraph) this);
    if (!string.IsNullOrEmpty(current.InvFinPeriodID))
    {
      MasterFinPeriod masterFinPeriod = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Equal<Current<UsageMaint.UsageFilter.invFinPeriodID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (masterFinPeriod != null)
      {
        pxSelectBase.WhereAnd<Where<PMTran.billedDate, Between<Required<MasterFinPeriod.startDate>, Required<MasterFinPeriod.endDate>>>>();
        return (IEnumerable) pxSelectBase.Select(new object[2]
        {
          (object) masterFinPeriod.StartDate,
          (object) masterFinPeriod.EndDate
        });
      }
    }
    return (IEnumerable) pxSelectBase.Select(Array.Empty<object>());
  }

  public UsageMaint()
  {
    ((PXSelectBase) this.UnBilled).GetAttribute<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.ImportAttributeMappingPropertiesHandler);
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      AutoNumberAttribute.SetNumberingId<PMRegister.refNbr>(((PXSelectBase) this.Document).Cache, ((PXSelectBase<ARSetup>) this.arsetup).Current.UsageNumberingID);
    this.EnsurePMDocumentCreated();
  }

  private void EnsurePMDocumentCreated()
  {
    if (((PXSelectBase) this.Document).Cache.Inserted.Cast<PMRegister>().Any<PMRegister>())
      return;
    ((PXSelectBase) this.Document).Cache.Insert();
    ((PXSelectBase) this.Document).Cache.IsDirty = false;
  }

  protected virtual void ImportAttributeMappingPropertiesHandler(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    e.Names.Clear();
    e.DisplayNames.Clear();
    System.Type[] source = new System.Type[8]
    {
      typeof (PMTran.branchID),
      typeof (PMTran.inventoryID),
      typeof (PMTran.description),
      typeof (PMTran.bAccountID),
      typeof (PMTran.uOM),
      typeof (PMTran.billableQty),
      typeof (PMTran.date),
      typeof (PMTran.caseCD)
    };
    IEnumerable<string> addPart1 = ((IEnumerable<System.Type>) source).Select<System.Type, string>((Func<System.Type, string>) (field => field.Name.Capitalize()));
    IEnumerable<string> addPart2 = ((IEnumerable<System.Type>) source).Select<System.Type, string>((Func<System.Type, string>) (field => PXUIFieldAttribute.GetDisplayName(((PXSelectBase) this.UnBilled).Cache, field.Name)));
    e.Names.Add<string>(addPart1);
    e.DisplayNames.Add<string>(addPart2);
  }

  protected virtual void UsageFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is UsageMaint.UsageFilter row))
      return;
    ((PXSelectBase) this.UnBilled).Cache.AllowInsert = row.ContractID.HasValue && (row.ContractStatus == "A" || row.ContractStatus == "U");
  }

  protected virtual void UsageFilter_ContractID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelectReadonly<Contract, Where<Contract.contractID, Equal<Required<CRCase.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (contract == null)
      return;
    Contract row1 = contract;
    DateTime? businessDate1 = ((PXGraph) this).Accessinfo.BusinessDate;
    DateTime businessDate2 = businessDate1.Value;
    if (ContractMaint.IsExpired(row1, businessDate2))
    {
      sender.RaiseExceptionHandling<UsageMaint.UsageFilter.contractID>(e.Row, (object) contract.ContractCD, (Exception) new PXSetPropertyException("Contract has expired.", (PXErrorLevel) 2));
    }
    else
    {
      Contract row2 = contract;
      businessDate1 = ((PXGraph) this).Accessinfo.BusinessDate;
      DateTime businessDate3 = businessDate1.Value;
      int num;
      ref int local = ref num;
      if (!ContractMaint.IsInGracePeriod(row2, businessDate3, out local))
        return;
      sender.RaiseExceptionHandling<UsageMaint.UsageFilter.contractID>(e.Row, (object) contract.ContractCD, (Exception) new PXSetPropertyException("Selected Contract is on the grace period. {0} day(s) left before the expiration.", (PXErrorLevel) 2, new object[1]
      {
        (object) num
      }));
    }
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this.EnsurePMDocumentCreated();
  }

  protected virtual void PMTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Contract contract = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.CurrentContract).Select(Array.Empty<object>()));
    if (contract == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMTran.bAccountID>(sender, e.Row, !contract.CustomerID.HasValue);
  }

  protected virtual void PMTran_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PMTran.locationID>(e.Row);
  }

  protected virtual void PMTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PMTran row = (PMTran) e.Row;
    int? nullable;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      nullable = row.InventoryID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PMTran.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryID
    }));
    if (inventoryItem == null)
      return;
    row.Description = inventoryItem.Descr;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMTran.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ProjectID
    }));
    if (pmProject == null)
      return;
    nullable = pmProject.CustomerID;
    if (!nullable.HasValue)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) pmProject.CustomerID
    }));
    if (customer == null || string.IsNullOrEmpty(customer.LocaleName))
      return;
    row.Description = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "Descr", customer.LocaleName);
  }

  protected virtual void PMTran_BillableQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMTran row))
      return;
    PXCache cache = sender;
    int? projectId = row.ProjectID;
    int? inventoryId = row.InventoryID;
    Decimal? nullable = row.BillableQty;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = (Decimal?) e.OldValue;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal used = valueOrDefault1 - valueOrDefault2;
    string uom = row.UOM;
    UsageMaint.AddUsage(cache, projectId, inventoryId, used, uom);
  }

  protected virtual void PMTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMTran row))
      return;
    Decimal? billableQty = row.BillableQty;
    Decimal num = 0M;
    if (billableQty.GetValueOrDefault() == num & billableQty.HasValue)
      return;
    PXCache sender1 = sender;
    int? projectId1 = row.ProjectID;
    int? inventoryId1 = row.InventoryID;
    billableQty = row.BillableQty;
    Decimal valueOrDefault1 = billableQty.GetValueOrDefault();
    string oldValue = (string) e.OldValue;
    UsageMaint.SubtractUsage(sender1, projectId1, inventoryId1, valueOrDefault1, oldValue);
    PXCache cache = sender;
    int? projectId2 = row.ProjectID;
    int? inventoryId2 = row.InventoryID;
    billableQty = row.BillableQty;
    Decimal valueOrDefault2 = billableQty.GetValueOrDefault();
    string uom = row.UOM;
    UsageMaint.AddUsage(cache, projectId2, inventoryId2, valueOrDefault2, uom);
  }

  protected virtual void PMTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is PMTran row))
      return;
    Decimal? billableQty = row.BillableQty;
    Decimal num = 0M;
    if (billableQty.GetValueOrDefault() == num & billableQty.HasValue)
      return;
    PXCache sender1 = sender;
    int? projectId = row.ProjectID;
    int? inventoryId = row.InventoryID;
    billableQty = row.BillableQty;
    Decimal valueOrDefault = billableQty.GetValueOrDefault();
    string uom = row.UOM;
    UsageMaint.SubtractUsage(sender1, projectId, inventoryId, valueOrDefault, uom);
  }

  public static void AddUsage(
    PXCache cache,
    int? contractID,
    int? inventoryID,
    Decimal used,
    string UOM)
  {
    if (!contractID.HasValue || !inventoryID.HasValue || !(used != 0M))
      return;
    Decimal num1 = INUnitAttribute.ConvertToBase(cache, inventoryID, UOM, used, INPrecision.QUANTITY);
    foreach (ContractDetailAcum contractDetailAcum in GraphHelper.RowCast<ContractDetailExt>((IEnumerable) PXSelectBase<ContractDetailExt, PXSelectJoin<ContractDetailExt, InnerJoin<Contract, On<ContractDetailExt.contractID, Equal<Contract.contractID>>, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetailExt.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.recurringItemID>>>>>, Where<ContractDetailExt.contractID, Equal<Required<ContractDetailExt.contractID>>, And<ContractDetailExt.revID, GreaterEqual<Contract.lastActiveRevID>, And<ContractItem.recurringItemID, Equal<Required<ContractItem.recurringItemID>>>>>>.Config>.Select(cache.Graph, new object[2]
    {
      (object) contractID,
      (object) inventoryID
    })).Select<ContractDetailExt, ContractDetailAcum>((Func<ContractDetailExt, ContractDetailAcum>) (detail =>
    {
      PXGraph graph = cache.Graph;
      return PXCache<ContractDetailAcum>.Insert(graph, new ContractDetailAcum()
      {
        ContractDetailID = detail.ContractDetailID
      });
    })))
    {
      Decimal? used1 = contractDetailAcum.Used;
      Decimal num2 = num1;
      contractDetailAcum.Used = used1.HasValue ? new Decimal?(used1.GetValueOrDefault() + num2) : new Decimal?();
      Decimal? usedTotal = contractDetailAcum.UsedTotal;
      Decimal num3 = num1;
      contractDetailAcum.UsedTotal = usedTotal.HasValue ? new Decimal?(usedTotal.GetValueOrDefault() + num3) : new Decimal?();
    }
  }

  public static void SubtractUsage(
    PXCache sender,
    int? contractID,
    int? inventoryID,
    Decimal used,
    string UOM)
  {
    UsageMaint.AddUsage(sender, contractID, inventoryID, -used, UOM);
  }

  public virtual void Persist()
  {
    if (!((PXSelectBase) this.UnBilled).Cache.Inserted.Cast<PMTran>().Any<PMTran>())
      ((PXSelectBase) this.Document).Cache.Clear();
    ((PXGraph) this).Persist();
  }

  public class MultiCurrency : PMTranMultiCurrency<UsageMaint>
  {
    protected override MultiCurrencyGraph<UsageMaint, PMTran>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<UsageMaint, PMTran>.CurySourceMapping(typeof (Contract))
      {
        CuryID = typeof (Contract.curyID)
      };
    }

    protected override string Module => "CT";

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.UnBilled
      };
    }

    [PXMergeAttributes]
    [PXDBLong]
    protected void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
    {
    }
  }

  [Serializable]
  public class UsageFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ContractID;
    protected string _InvFinPeriodID;
    protected string _ContractStatus;

    [PXDBInt]
    [PXSelector(typeof (Search<Contract.contractID, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.status, NotEqual<Contract.status.draft>, And<Contract.status, NotEqual<Contract.status.inApproval>>>>>), SubstituteKey = typeof (Contract.contractCD), DescriptionField = typeof (Contract.description))]
    [PXUIField(DisplayName = "Contract ID")]
    public virtual int? ContractID
    {
      get => this._ContractID;
      set => this._ContractID = value;
    }

    [FinPeriodSelector]
    [PXUIField]
    public virtual string InvFinPeriodID
    {
      get => this._InvFinPeriodID;
      set => this._InvFinPeriodID = value;
    }

    [PXDBString]
    [PXFormula(typeof (Selector<UsageMaint.UsageFilter.contractID, Contract.status>))]
    public virtual string ContractStatus
    {
      get => this._ContractStatus;
      set => this._ContractStatus = value;
    }

    public abstract class contractID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UsageMaint.UsageFilter.contractID>
    {
    }

    public abstract class invFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UsageMaint.UsageFilter.invFinPeriodID>
    {
    }

    public abstract class contractStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UsageMaint.UsageFilter.contractStatus>
    {
    }
  }
}
