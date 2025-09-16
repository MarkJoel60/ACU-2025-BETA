// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTBillEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Parser;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.Repositories;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable enable
namespace PX.Objects.CT;

public class CTBillEngine : PXGraph<
#nullable disable
CTBillEngine>, IContractInformation
{
  public PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>> Contracts;
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Optional<Contract.contractID>>>> BillingSchedule;
  public PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>>> ContractDetails;
  public PXSelect<ContractDetailExt, Where<ContractDetailExt.contractID, Equal<Required<Contract.contractID>>>> ContractDetailsExt;
  public PXSelect<PMTran> Transactions;
  public PXSelect<ContractRenewalHistory> RenewalHistory;
  public PXSelect<ContractRenewalHistory, Where<ContractRenewalHistory.contractID, Equal<Required<Contract.contractID>>, And<ContractRenewalHistory.revID, Equal<Required<Contract.revID>>>>> CurrentRenewalHistory;
  public PXSelect<ContractBillingTrace, Where<ContractBillingTrace.contractID, Equal<Required<ContractBillingTrace.contractID>>>, OrderBy<Desc<ContractBillingTrace.recordID>>> BillingTrace;
  public PXSetupOptional<INSetup> insetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetupOptional<CMSetup> cmsetup;
  public PXSelect<ContractItem> contractItem;
  public PXSetup<Company> company;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ContractBillingSchedule.accountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ContractBillingSchedule.locationID>>>>> BillingLocation;
  protected List<PX.Objects.AR.ARRegister> doclist = new List<PX.Objects.AR.ARRegister>();
  protected Dictionary<int?, Decimal?> availableQty = new Dictionary<int?, Decimal?>();
  protected Dictionary<int?, Decimal?> availableDeposit = new Dictionary<int?, Decimal?>();
  protected Dictionary<int?, UsageData> depositUsage = new Dictionary<int?, UsageData>();
  protected List<UsageData> nonRefundableDepositedUsage = new List<UsageData>();
  protected Dictionary<int?, ContractItem> nonRefundableDeposits = new Dictionary<int?, ContractItem>();
  protected Dictionary<int?, ContractItem> refundableDeposits = new Dictionary<int?, ContractItem>();
  protected CustomerRepository customerRepository;

  [PXBool]
  [PXDBScalar(typeof (Search<ContractTemplate.detailedBilling, Where<ContractTemplate.contractID, Equal<Contract.templateID>>>))]
  protected virtual void Contract_DetailedBilling_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  protected virtual void ContractDetail_ContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID>), typeof (ContractItem.contractItemCD), new System.Type[] {typeof (ContractItem.contractItemCD), typeof (ContractItem.descr)})]
  protected virtual void ContractDetail_ContractItemID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractDetail.contractID>>>>))]
  [PXParent(typeof (Select<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<ContractDetail.contractID>>>>))]
  protected virtual void ContractDetail_RevID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void ContractDetail_LineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBLong]
  protected void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  public ContractBillingSchedule contractBillingSchedule
  {
    get
    {
      return PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(Array.Empty<object>()));
    }
  }

  [PXMergeAttributes]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public void ContractItem_BaseItemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public void ContractItem_RenewalItemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public void ContractItem_RecurringItemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetailUsagePrice, ContractDetail.usagePriceOption, Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, ContractDetail.usagePrice, ContractDetail.basePriceVal, ContractDetail.qty, IsNull<Parent<ContractBillingSchedule.nextDate>, Current<AccessInfo.businessDate>>>))]
  public void ContractDetail_UsagePriceVal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (GetItemPriceValue<ContractDetailExt.contractID, ContractDetailExt.contractItemID, ContractDetailType.ContractDetailUsagePrice, ContractDetail.usagePriceOption, Selector<ContractDetailExt.contractItemID, ContractItem.recurringItemID>, ContractDetail.usagePrice, ContractDetail.usagePrice, ContractDetail.qty, IsNull<Parent<ContractBillingSchedule.nextDate>, Current<AccessInfo.businessDate>>>))]
  public void ContractDetailExt_UsagePriceVal_CacheAttached(PXCache sender)
  {
  }

  protected virtual object ConvertFromExtValue(object extValue)
  {
    return extValue is PXFieldState pxFieldState ? pxFieldState.Value : extValue;
  }

  protected virtual object EvaluateAttribute(string attribute, Guid? refNoteID)
  {
    PXResultset<CSAnswers> pxResultset = PXSelectBase<CSAnswers, PXSelectJoin<CSAnswers, InnerJoin<CSAttribute, On<CSAttribute.attributeID, Equal<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>, And<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) refNoteID,
      (object) attribute
    });
    CSAnswers csAnswers = (CSAnswers) null;
    CSAttribute csAttribute = (CSAttribute) null;
    if (pxResultset.Count > 0)
    {
      csAnswers = (CSAnswers) ((PXResult) pxResultset[0])[0];
      csAttribute = (CSAttribute) ((PXResult) pxResultset[0])[1];
    }
    if (csAnswers == null || csAnswers.AttributeID == null)
    {
      csAttribute = PXResultset<CSAttribute>.op_Implicit(PXSelectBase<CSAttribute, PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) attribute
      }));
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    if (csAnswers != null)
    {
      if (csAnswers.Value != null)
        return (object) csAnswers.Value;
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    return (object) string.Empty;
  }

  object IContractInformation.Evaluate(
    CTObjectType objectName,
    string fieldName,
    string attribute,
    CTFormulaDescriptionContainer row)
  {
    switch (objectName)
    {
      case CTObjectType.Contract:
        Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ContractID
        }));
        if (contract != null)
          return attribute != null ? this.EvaluateAttribute(attribute, contract.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (Contract)].GetValueExt((object) contract, fieldName));
        break;
      case CTObjectType.ContractTemplate:
        ContractTemplate contractTemplate = PXResultset<ContractTemplate>.op_Implicit(PXSelectBase<ContractTemplate, PXSelect<ContractTemplate, Where<ContractTemplate.contractID, Equal<Required<ContractTemplate.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.ContractID
          })).TemplateID
        }));
        if (contractTemplate != null)
          return attribute != null ? this.EvaluateAttribute(attribute, contractTemplate.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (ContractTemplate)].GetValueExt((object) contractTemplate, fieldName));
        break;
      case CTObjectType.Customer:
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.CustomerID
        }));
        if (customer != null)
          return attribute != null ? this.EvaluateAttribute(attribute, customer.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.AR.Customer)].GetValueExt((object) customer, fieldName));
        break;
      case CTObjectType.Location:
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.CustomerLocationID
        }));
        if (location != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.CR.Location)].GetValueExt((object) location, fieldName));
        break;
      case CTObjectType.ContractItem:
        ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractItem.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ContractItemID
        }));
        if (contractItem != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (ContractItem)].GetValueExt((object) contractItem, fieldName));
        break;
      case CTObjectType.ContractDetail:
        ContractDetail contractDetail = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractDetailID, Equal<Required<ContractDetail.contractDetailID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ContractDetailID
        }));
        if (contractDetail != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (ContractDetail)].GetValueExt((object) contractDetail, fieldName));
        break;
      case CTObjectType.InventoryItem:
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.InventoryID
        }));
        if (inventoryItem != null)
          return attribute != null ? this.EvaluateAttribute(attribute, inventoryItem.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValueExt((object) inventoryItem, fieldName));
        break;
      case CTObjectType.PMTran:
        if (row.pmTranIDs.Count == 1)
        {
          PMTran pmTran = PMTran.PK.Find((PXGraph) this, row.pmTranIDs[0]);
          if (pmTran != null)
            return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMTran)].GetValueExt((object) pmTran, fieldName));
          break;
        }
        break;
      case CTObjectType.ContractBillingSchedule:
        ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Required<ContractBillingSchedule.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ContractID
        }));
        if (contractBillingSchedule != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (ContractBillingSchedule)].GetValueExt((object) contractBillingSchedule, fieldName));
        break;
      case CTObjectType.UsageData:
        if (row.usageData != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (UsageData)].GetValueExt((object) row.usageData, fieldName));
        break;
      case CTObjectType.AccessInfo:
        return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (AccessInfo)].GetValueExt((object) ((PXGraph) this).Accessinfo, fieldName));
    }
    return (object) null;
  }

  string IContractInformation.GetParametrActionInvoice(CTFormulaDescriptionContainer tran)
  {
    return tran.ActionInvoice;
  }

  string IContractInformation.GetParametrActionItem(CTFormulaDescriptionContainer tran)
  {
    return tran.ActionItem;
  }

  string IContractInformation.GetParametrInventoryPrefix(CTFormulaDescriptionContainer tran)
  {
    return tran.InventoryPrefix;
  }

  private string GetDescriptionMessageForInventory(
    CTBillEngine.InventoryAction action,
    PX.Objects.IN.InventoryItem inventory)
  {
    return PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventory);
  }

  private string GetDescriptionMessageForDetail(
    CTBillEngine.InventoryAction action,
    ContractDetail detail)
  {
    return PXDBLocalizableStringAttribute.GetTranslation<ContractDetail.description>(((PXGraph) this).Caches[typeof (ContractDetail)], (object) detail);
  }

  private string GetInvoiceDescription(
    string action,
    Contract contract,
    PX.Objects.AR.Customer customer,
    PX.Objects.AR.ARInvoice invoice)
  {
    string invoiceDescription = (string) null;
    CTFormulaDescriptionContainer descriptionContainer = new CTFormulaDescriptionContainer();
    descriptionContainer.ContractID = contract.ContractID;
    descriptionContainer.CustomerID = customer.BAccountID;
    descriptionContainer.CustomerLocationID = invoice.CustomerLocationID;
    using (new PXLocaleScope(customer.LocaleName))
    {
      switch (action)
      {
        case "S":
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix("Contract Setup");
          break;
        case "A":
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix(contract.IsPendingUpdate.GetValueOrDefault() ? "Contract Upgrade" : "Contract Activation");
          break;
        case "T":
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix("Contract Termination");
          break;
        case "B":
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix("Contract Billing");
          break;
        case "R":
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix("Contract Renewal");
          break;
        default:
          descriptionContainer.ActionInvoice = PXMessages.LocalizeNoPrefix("Contract Activation");
          break;
      }
    }
    ContractBillingSchedule contractBillingSchedule = PXResult<ContractBillingSchedule>.op_Implicit(((IQueryable<PXResult<ContractBillingSchedule>>) PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    })).FirstOrDefault<PXResult<ContractBillingSchedule>>());
    CTDataNavigator ctDataNavigator = new CTDataNavigator((IContractInformation) this, new List<CTFormulaDescriptionContainer>((IEnumerable<CTFormulaDescriptionContainer>) new CTFormulaDescriptionContainer[1]
    {
      descriptionContainer
    }));
    ExpressionNode expressionNode = CTExpressionParser.Parse((IContractInformation) this, contractBillingSchedule.InvoiceFormula);
    expressionNode.Bind((object) ctDataNavigator);
    object obj = expressionNode.Eval((object) descriptionContainer);
    if (obj != null)
      invoiceDescription += obj.ToString();
    return invoiceDescription;
  }

  /// <summary>
  /// Returns an transaction description with a prefix if it is or without otherwise
  /// </summary>
  /// <param name="prefix">Description prefix or empty string</param>
  /// <param name="description">Description</param>
  /// <returns>Description with prefix or without</returns>
  private string GetTransactionDescriptionWithPrefix(PX.Objects.AR.ARTran row, UsageData item)
  {
    string descriptionWithPrefix = (string) null;
    using (new PXLocaleScope(PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CustomerID
    })).LocaleName))
    {
      CTFormulaDescriptionContainer descriptionContainer = new CTFormulaDescriptionContainer();
      descriptionContainer.InventoryPrefix = item.Prefix;
      descriptionContainer.CustomerID = row.CustomerID;
      descriptionContainer.ContractID = row.ProjectID;
      descriptionContainer.InventoryID = row.InventoryID;
      descriptionContainer.ActionItem = item.ActionItem;
      descriptionContainer.ContractItemID = item.ContractItemID;
      descriptionContainer.ContractDetailID = item.ContractDetailID;
      descriptionContainer.pmTranIDs = item.TranIDs;
      descriptionContainer.usageData = item;
      ContractBillingSchedule contractBillingSchedule = PXResult<ContractBillingSchedule>.op_Implicit(((IQueryable<PXResult<ContractBillingSchedule>>) PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ProjectID
      })).FirstOrDefault<PXResult<ContractBillingSchedule>>());
      CTDataNavigator ctDataNavigator = new CTDataNavigator((IContractInformation) this, new List<CTFormulaDescriptionContainer>((IEnumerable<CTFormulaDescriptionContainer>) new CTFormulaDescriptionContainer[1]
      {
        descriptionContainer
      }));
      ExpressionNode expressionNode = CTExpressionParser.Parse((IContractInformation) this, contractBillingSchedule.TranFormula);
      expressionNode.Bind((object) ctDataNavigator);
      object obj = expressionNode.Eval((object) descriptionContainer);
      if (obj != null)
        descriptionWithPrefix += obj.ToString();
    }
    return descriptionWithPrefix;
  }

  private string GetInvoiceDescriptionRenewing(Contract contract, PX.Objects.AR.Customer customer)
  {
    using (new PXLocaleScope(customer.LocaleName))
      return string.Format(PXMessages.LocalizeNoPrefix("Contract Renew {0}: {1}."), (object) contract.ContractCD, (object) contract.Description);
  }

  private string GetInvoiceDescriptionBilling(Contract contract, PX.Objects.AR.Customer customer)
  {
    using (new PXLocaleScope(customer.LocaleName))
      return string.Format(PXMessages.LocalizeNoPrefix("Contract Billing {0}: {1}."), (object) contract.ContractCD, (object) PXDBLocalizableStringAttribute.GetTranslation<Contract.description>(((PXGraph) this).Caches[typeof (Contract)], (object) contract));
  }

  public virtual string PrimaryView => "Contracts";

  public CTBillEngine() => this.customerRepository = new CustomerRepository((PXGraph) this);

  protected int DecPlPrcCst
  {
    get
    {
      CommonSetup commonSetup = PXResultset<CommonSetup>.op_Implicit(PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return commonSetup == null || !commonSetup.DecPlPrcCst.HasValue ? 2 : Convert.ToInt32((object) commonSetup.DecPlPrcCst);
    }
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Contract.contractID), SubstituteKey = typeof (Contract.contractCD), ValidateValue = false)]
  public void _(
    PX.Data.Events.CacheAttached<ContractRenewalHistory.childContractID> e)
  {
  }

  protected virtual void CreateNewRevision(int? contractID, string action, string newStatus)
  {
    Contract copy1 = (Contract) ((PXSelectBase) this.Contracts).Cache.CreateCopy((object) ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contractID
    }));
    foreach (ContractDetail contractDetail1 in GraphHelper.RowCast<ContractDetail>((IEnumerable) ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(new object[1]
    {
      (object) copy1.ContractID
    })))
    {
      ContractDetail copy2 = ((PXSelectBase) this.ContractDetails).Cache.CreateCopy((object) contractDetail1) as ContractDetail;
      ((PXSelectBase) this.ContractDetails).Cache.Normalize();
      ((PXSelectBase) this.ContractDetails).Cache.Remove((object) copy2);
      ContractDetail contractDetail2 = copy2;
      int? revId = contractDetail2.RevID;
      contractDetail2.RevID = revId.HasValue ? new int?(revId.GetValueOrDefault() + 1) : new int?();
      copy2.NoteID = new Guid?();
      if (action == "S" || action == "M" || action == "A")
        ContractMaint.ValidateUniqueness((PXGraph) this, copy2, true);
      ContractDetail det = ((PXSelectBase<ContractDetail>) this.ContractDetails).Insert(copy2);
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.ContractDetails).Cache, (object) contractDetail1, ((PXSelectBase) this.ContractDetails).Cache, (object) det, (PXNoteAttribute.IPXCopySettings) null);
      if (action == "U")
      {
        ContractMaint.CalculateDiscounts(((PXSelectBase) this.ContractDetails).Cache, copy1, det);
        ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(det);
      }
    }
    ContractRenewalHistory contractRenewalHistory1 = ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).SelectSingle(new object[2]
    {
      (object) copy1.ContractID,
      (object) copy1.RevID
    });
    ContractRenewalHistory copy3 = ((PXSelectBase) this.CurrentRenewalHistory).Cache.CreateCopy((object) contractRenewalHistory1) as ContractRenewalHistory;
    copy3.ActionBusinessDate = new DateTime?();
    copy3.Status = newStatus;
    copy3.Action = action;
    ContractRenewalHistory contractRenewalHistory2 = copy3;
    int? revId1 = contractRenewalHistory2.RevID;
    contractRenewalHistory2.RevID = revId1.HasValue ? new int?(revId1.GetValueOrDefault() + 1) : new int?();
    ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).Insert(copy3);
    if (contractRenewalHistory1.Status == "A")
      copy1.LastActiveRevID = contractRenewalHistory1.RevID;
    if (copy3.Status == "A")
      copy1.LastActiveRevID = copy3.RevID;
    copy1.RevID = copy3.RevID;
    copy1.IsLastActionUndoable = new bool?(true);
    ((PXSelectBase<Contract>) this.Contracts).Update(copy1);
  }

  public static void UpdateContractHistoryEntry(
    ContractRenewalHistory history,
    Contract contract,
    ContractBillingSchedule schedule)
  {
    history.IsActive = contract.IsActive;
    history.IsCancelled = contract.IsCancelled;
    history.IsCompleted = contract.IsCompleted;
    history.IsPendingUpdate = contract.IsPendingUpdate;
    history.ExpireDate = contract.ExpireDate;
    history.EffectiveFrom = contract.EffectiveFrom;
    history.ActivationDate = contract.ActivationDate;
    history.StartDate = contract.StartDate;
    history.TerminationDate = contract.TerminationDate;
    history.DiscountID = contract.DiscountID;
    history.LastDate = schedule.LastDate;
    history.NextDate = schedule.NextDate;
    history.StartBilling = schedule.StartBilling;
    if (!(contract.Status != "D"))
      return;
    history.ChildContractID = new int?();
  }

  private void UpdateHistory(Contract contract)
  {
    Contract contract1 = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract.ContractID
    });
    ContractBillingSchedule schedule = ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).SelectSingle(new object[1]
    {
      (object) contract.ContractID
    });
    ContractRenewalHistory history = ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).SelectSingle(new object[2]
    {
      (object) contract1.ContractID,
      (object) contract1.RevID
    });
    CTBillEngine.UpdateContractHistoryEntry(history, contract1, schedule);
    ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).Update(history);
  }

  public static void ClearBillingTrace(int? contractID)
  {
    PXDatabase.Delete<ContractBillingTrace>(new PXDataFieldRestrict[1]
    {
      new PXDataFieldRestrict("ContractID", (PXDbType) 8, (object) contractID)
    });
  }

  private void ClearFuture(Contract contract)
  {
    if (contract == null || !contract.ContractID.HasValue || !contract.RevID.HasValue)
      return;
    this.ClearFutureDetails(contract);
    this.ClearFutureHistory(contract);
  }

  private void ClearFutureHistory(Contract contract)
  {
    PXDatabase.Delete<ContractRenewalHistory>(new PXDataFieldRestrict[2]
    {
      new PXDataFieldRestrict("ContractID", (PXDbType) 8, new int?(4), (object) contract.ContractID, (PXComp) 0),
      new PXDataFieldRestrict("RevID", (PXDbType) 8, new int?(4), (object) contract.RevID, (PXComp) 2)
    });
  }

  private void ClearFutureDetails(Contract contract)
  {
    PXDatabase.Delete<ContractDetail>(new PXDataFieldRestrict[2]
    {
      new PXDataFieldRestrict("ContractID", (PXDbType) 8, new int?(4), (object) contract.ContractID, (PXComp) 0),
      new PXDataFieldRestrict("RevID", (PXDbType) 8, new int?(4), (object) contract.RevID, (PXComp) 2)
    });
  }

  private void ClearState()
  {
    this.availableQty.Clear();
    this.availableDeposit.Clear();
    this.depositUsage.Clear();
    this.doclist.Clear();
    this.nonRefundableDepositedUsage.Clear();
    this.nonRefundableDeposits.Clear();
    this.refundableDeposits.Clear();
  }

  public bool IsFullyBilledContract(Contract contract)
  {
    if (contract.Status == "E")
      return true;
    List<UsageData> data;
    this.RecalcUsage(contract, out data, out Dictionary<int, List<TranNotePair>> _, out List<UsageData> _);
    return !data.Any<UsageData>((Func<UsageData, bool>) (d =>
    {
      Decimal? extPrice = d.ExtPrice;
      Decimal num1 = 0M;
      if (!(extPrice.GetValueOrDefault() == num1 & extPrice.HasValue))
        return true;
      Decimal? qty = d.Qty;
      Decimal num2 = 0M;
      return !(qty.GetValueOrDefault() == num2 & qty.HasValue);
    }));
  }

  public virtual void Setup(int? contractID, DateTime? date)
  {
    if (!contractID.HasValue)
      throw new ArgumentNullException(nameof (contractID));
    if (!date.HasValue)
      throw new ArgumentNullException(nameof (date));
    Contract contract = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }));
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    bool? nullable = contract.IsCompleted;
    if (nullable.GetValueOrDefault())
      throw new PXException("Contract is Completed/Expired and cannot be Set up.");
    nullable = !(contract.Status != "D") ? contract.IsCancelled : throw new PXException("Contract is already Set up.");
    if (nullable.GetValueOrDefault())
      throw new PXException("Contract is Terminated and cannot be Set up.");
    if (contract.ExpireDate.HasValue && date.Value > contract.ExpireDate.Value)
      throw new PXException("Activation Date of a contract cannot be greater than its Expiration Date");
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract, out customer, out location);
    this.ValidateCustomerInfo(contract, customer, location);
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    this.CreateNewRevision(contractID, "S", "P");
    contract.StartDate = date;
    ((PXSelectBase<Contract>) this.Contracts).Update(contract);
    Contract template = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract.TemplateID
    });
    contract.ScheduleStartsOn = template.ScheduleStartsOn;
    List<CTBillEngine.InvoiceData> invoices = new List<CTBillEngine.InvoiceData>();
    CTBillEngine.InvoiceData invoiceData = new CTBillEngine.InvoiceData(date.Value);
    using (new PXLocaleScope(customer.LocaleName))
      invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetSetupFee(contract));
    if (contract.ScheduleStartsOn == "S")
    {
      using (new PXLocaleScope(customer.LocaleName))
        invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetActivationFee(contract, date));
    }
    if (invoiceData.UsageData.Count > 0)
      invoices.Add(invoiceData);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (invoices.Count > 0)
      {
        using (new PXLocaleScope(customer.LocaleName))
          this.CreateInvoice(contract, template, invoices, customer, location, "S");
      }
      if (contract.ScheduleStartsOn == "S")
        contractBillingSchedule.LastDate = new DateTime?(date.Value);
      foreach (PX.Objects.AR.ARRegister arRegister in this.doclist)
        ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(new ContractBillingTrace()
        {
          ContractID = contractID,
          DocType = arRegister.DocType,
          RefNbr = arRegister.RefNbr,
          LastDate = contractBillingSchedule.LastDate,
          NextDate = contractBillingSchedule.NextDate
        });
      if (contract.ScheduleStartsOn == "S")
      {
        contractBillingSchedule.StartBilling = new DateTime?(date.Value);
        if (contractBillingSchedule.Type != "D")
          contractBillingSchedule.NextDate = this.GetNextBillingDateConsiderExpiration(contractBillingSchedule.Type, contract.CustomerID, date, date, contract.ExpireDate);
      }
      contract.EffectiveFrom = contract.StartDate;
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(contractBillingSchedule);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.SetupContract))).FireOn((PXGraph) this, contract);
      this.UpdateHistory(contract);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract);
  }

  public virtual void Activate(int? contractID, DateTime? date, bool setActivationDate = true)
  {
    if (!contractID.HasValue)
      throw new ArgumentNullException(nameof (contractID));
    if (!date.HasValue)
      throw new ArgumentNullException(nameof (date));
    Contract contract1 = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }));
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    if (contractBillingSchedule != null)
    {
      object accountId = (object) contractBillingSchedule.AccountID;
      ((PXSelectBase) this.BillingSchedule).Cache.RaiseFieldVerifying<ContractBillingSchedule.accountID>((object) contractBillingSchedule, ref accountId);
    }
    if (contract1.IsCompleted.GetValueOrDefault())
      throw new PXException("Contract is Completed/Expired and cannot be Activated.");
    if (contract1.IsActive.GetValueOrDefault() && !contract1.IsPendingUpdate.GetValueOrDefault())
      throw new PXException("Contract is already Active.");
    if (contract1.IsCancelled.GetValueOrDefault())
      throw new PXException("Contract is Terminated and cannot be Activated.");
    DateTime dateTime1 = date.Value;
    DateTime? nullable1 = contract1.StartDate;
    DateTime dateTime2 = nullable1.Value;
    if (dateTime1 < dateTime2)
      throw new PXException("Activation Date of the contract cannot be earlier than the Start Date of the contract");
    nullable1 = contract1.ExpireDate;
    if (nullable1.HasValue)
    {
      DateTime dateTime3 = date.Value;
      nullable1 = contract1.ExpireDate;
      DateTime dateTime4 = nullable1.Value;
      if (dateTime3 > dateTime4)
        throw new PXException("Activation Date of a contract cannot be greater than its Expiration Date");
    }
    if (contract1.IsPendingUpdate.GetValueOrDefault())
    {
      nullable1 = contractBillingSchedule.LastDate;
      DateTime dateTime5 = nullable1 ?? contract1.StartDate.Value;
      nullable1 = date;
      DateTime dateTime6 = dateTime5;
      if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < dateTime6 ? 1 : 0) : 0) == 0)
      {
        if (contractBillingSchedule.Type != "D")
        {
          nullable1 = date;
          DateTime dateTime7 = contractBillingSchedule.NextDate.Value;
          if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > dateTime7 ? 1 : 0) : 0) == 0)
            goto label_22;
        }
        else
          goto label_22;
      }
      throw new PXException("Update can only be activated if the effective date is between the Last Billing Date and Next Billing Date");
    }
label_22:
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract1))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    if (contract1.OriginalContractID.HasValue)
    {
      Contract contract2 = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
      {
        (object) contract1.OriginalContractID
      }));
      if (!this.IsFullyBilledContract(contract2))
        throw new PXException("The contract cannot be activated because the original contract '{0}' has unbilled usage or included fee. Run billing for the original contract first.", new object[1]
        {
          (object) contract2.ContractCD
        });
      contract2.IsCompleted = new bool?(true);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ExpireContract))).FireOn((PXGraph) this, contract2);
    }
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract1, out customer, out location);
    this.ValidateCustomerInfo(contract1, customer, location);
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    this.CreateNewRevision(contractID, "A", "A");
    if (setActivationDate)
    {
      contract1.ActivationDate = date;
      ((PXSelectBase<Contract>) this.Contracts).Update(contract1);
    }
    Contract template = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract1.TemplateID
    });
    List<CTBillEngine.InvoiceData> invoices = new List<CTBillEngine.InvoiceData>();
    CTBillEngine.InvoiceData invoiceData = new CTBillEngine.InvoiceData(date.Value);
    if (!contract1.IsPendingUpdate.GetValueOrDefault())
    {
      if (contract1.ScheduleStartsOn == "S")
      {
        if (contractBillingSchedule.Type != "D")
        {
          while (true)
          {
            DateTime? nextDate = contractBillingSchedule.NextDate;
            DateTime? nullable2 = date;
            if ((nextDate.HasValue & nullable2.HasValue ? (nextDate.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              contractBillingSchedule.LastDate = contractBillingSchedule.NextDate;
              contractBillingSchedule.NextDate = this.GetNextBillingDateConsiderExpiration(contractBillingSchedule.Type, contract1.CustomerID, contractBillingSchedule.LastDate, contractBillingSchedule.StartBilling, contract1.ExpireDate);
            }
            else
              break;
          }
        }
        else
        {
          contractBillingSchedule.LastDate = date;
          contractBillingSchedule.NextDate = new DateTime?();
        }
      }
      if (contract1.ScheduleStartsOn == "A")
      {
        using (new PXLocaleScope(customer.LocaleName))
          invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetActivationFee(contract1, date));
      }
      using (new PXLocaleScope(customer.LocaleName))
        invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetPrepayment(contract1, date, contractBillingSchedule.LastDate, contractBillingSchedule.NextDate));
    }
    else
    {
      using (new PXLocaleScope(customer.LocaleName))
        invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetUpgradeFee(contract1, contractBillingSchedule.LastDate, contractBillingSchedule.NextDate, date));
      contract1.EffectiveFrom = date;
    }
    if (invoiceData.UsageData.Count > 0)
      invoices.Add(invoiceData);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (invoices.Count > 0)
      {
        using (new PXLocaleScope(customer.LocaleName))
          this.CreateInvoice(contract1, template, invoices, customer, location, "A");
      }
      foreach (PX.Objects.AR.ARRegister arRegister in this.doclist)
        ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(new ContractBillingTrace()
        {
          ContractID = contractID,
          DocType = arRegister.DocType,
          RefNbr = arRegister.RefNbr,
          LastDate = contractBillingSchedule.LastDate,
          NextDate = contractBillingSchedule.NextDate
        });
      if (!contract1.IsPendingUpdate.GetValueOrDefault() && contract1.ScheduleStartsOn == "A")
      {
        if (contractBillingSchedule.Type != "D")
          contractBillingSchedule.NextDate = this.GetNextBillingDateConsiderExpiration(contractBillingSchedule.Type, contract1.CustomerID, date, date, contract1.ExpireDate);
        contractBillingSchedule.LastDate = new DateTime?(date.Value);
      }
      if (!contract1.IsPendingUpdate.GetValueOrDefault())
      {
        contract1.EffectiveFrom = contract1.StartDate;
        if (contract1.ScheduleStartsOn == "A")
          contractBillingSchedule.StartBilling = date;
      }
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(contractBillingSchedule);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ActivateContract))).FireOn((PXGraph) this, contract1);
      this.UpdateStatusFlags(contract1);
      contract1.ServiceActivate = new bool?(true);
      ((PXSelectBase<Contract>) this.Contracts).Update(contract1);
      this.UpdateHistory(contract1);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract1);
  }

  public virtual void SetupAndActivate(int? contractID, DateTime? date)
  {
    if (!contractID.HasValue)
      throw new ArgumentNullException(nameof (contractID));
    if (!date.HasValue)
      throw new ArgumentNullException(nameof (date));
    Contract contract1 = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }));
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    bool? nullable1 = contract1.IsCompleted;
    nullable1 = !nullable1.GetValueOrDefault() ? contract1.IsActive : throw new PXException("Contract once Cancelled can not be Activated.");
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = contract1.IsPendingUpdate;
      if (!nullable1.GetValueOrDefault())
        throw new PXException("Contract is already Active.");
    }
    nullable1 = contract1.IsCancelled;
    if (nullable1.GetValueOrDefault())
      throw new PXException("Contract is Terminated and cannot be Activated.");
    DateTime? nullable2;
    if (contract1.ExpireDate.HasValue)
    {
      DateTime dateTime1 = date.Value;
      nullable2 = contract1.ExpireDate;
      DateTime dateTime2 = nullable2.Value;
      if (dateTime1 > dateTime2)
        throw new PXException("Activation Date of a contract cannot be greater than its Expiration Date");
    }
    nullable1 = contract1.IsPendingUpdate;
    if (nullable1.GetValueOrDefault())
    {
      nullable2 = contractBillingSchedule.LastDate;
      DateTime dateTime3 = nullable2 ?? contract1.StartDate.Value;
      nullable2 = date;
      DateTime dateTime4 = dateTime3;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < dateTime4 ? 1 : 0) : 0) == 0)
      {
        nullable2 = date;
        DateTime dateTime5 = contractBillingSchedule.NextDate.Value;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > dateTime5 ? 1 : 0) : 0) == 0)
          goto label_18;
      }
      throw new PXException("Update can only be activated if the effective date is between the Last Billing Date and Next Billing Date");
    }
label_18:
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract1))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    if (contract1.OriginalContractID.HasValue)
    {
      Contract contract2 = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
      {
        (object) contract1.OriginalContractID
      }));
      if (!this.IsFullyBilledContract(contract2))
        throw new PXException("The contract cannot be activated because the original contract '{0}' has unbilled usage or included fee. Use Set Up Contract action or run billing for the original contract first.", new object[1]
        {
          (object) contract2.ContractCD
        });
      contract2.IsCompleted = new bool?(true);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ExpireContract))).FireOn((PXGraph) this, contract2);
      ((PXSelectBase<Contract>) this.Contracts).Update(contract2);
    }
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract1, out customer, out location);
    this.ValidateCustomerInfo(contract1, customer, location);
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    this.CreateNewRevision(contractID, "M", "A");
    contract1.ActivationDate = date;
    contract1.StartDate = date;
    ((PXSelectBase<Contract>) this.Contracts).Update(contract1);
    Contract template = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract1.TemplateID
    });
    contract1.ScheduleStartsOn = template.ScheduleStartsOn;
    List<CTBillEngine.InvoiceData> invoices = new List<CTBillEngine.InvoiceData>();
    CTBillEngine.InvoiceData invoiceData = new CTBillEngine.InvoiceData(date.Value);
    using (new PXLocaleScope(customer.LocaleName))
    {
      invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetSetupFee(contract1));
      invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetActivationFee(contract1, date));
      invoiceData.UsageData.AddRange((IEnumerable<UsageData>) this.GetPrepayment(contract1, date, new DateTime?(), new DateTime?()));
    }
    if (invoiceData.UsageData.Count > 0)
      invoices.Add(invoiceData);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (invoices.Count > 0)
      {
        using (new PXLocaleScope(customer.LocaleName))
          this.CreateInvoice(contract1, template, invoices, customer, location, "M");
      }
      contractBillingSchedule.LastDate = date;
      foreach (PX.Objects.AR.ARRegister arRegister in this.doclist)
        ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(new ContractBillingTrace()
        {
          ContractID = contractID,
          DocType = arRegister.DocType,
          RefNbr = arRegister.RefNbr,
          LastDate = contractBillingSchedule.LastDate,
          NextDate = contractBillingSchedule.NextDate
        });
      if (contractBillingSchedule.Type != "D")
        contractBillingSchedule.NextDate = this.GetNextBillingDateConsiderExpiration(contractBillingSchedule.Type, contract1.CustomerID, new DateTime?(date.Value), date, contract1.ExpireDate);
      contract1.EffectiveFrom = contract1.StartDate;
      contractBillingSchedule.StartBilling = date;
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(contractBillingSchedule);
      contract1.IsActive = new bool?(true);
      contract1.ServiceActivate = new bool?(true);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ActivateContract))).FireOn((PXGraph) this, contract1);
      this.UpdateHistory(contract1);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract1);
  }

  public virtual void ActivateUpgrade(int? contractID, DateTime? date)
  {
    this.Activate(contractID, date, false);
  }

  private static bool IsBillable(UsageData item)
  {
    bool? isTranData = item.IsTranData;
    bool flag = false;
    if (isTranData.GetValueOrDefault() == flag & isTranData.HasValue)
      return (item.Proportion ?? 1M) * item.ExtPrice.GetValueOrDefault() != 0M;
    return item.Qty.GetValueOrDefault() != 0M || (item.Proportion ?? 1M) * item.PriceOverride.GetValueOrDefault() != 0M;
  }

  private static DateTime? ShiftDateIfPeriodClosed(
    IFinPeriodRepository periodRepository,
    DateTime date,
    int? organizationID)
  {
    FinPeriod finPeriodByDate = periodRepository.GetFinPeriodByDate(new DateTime?(date), organizationID);
    return finPeriodByDate.ARClosed.GetValueOrDefault() ? periodRepository.FindFirstOpenFinPeriod(finPeriodByDate.FinPeriodID, organizationID, typeof (OrganizationFinPeriod.aRClosed)).StartDate : new DateTime?(date);
  }

  private void CreateInvoice(
    Contract contract,
    Contract template,
    List<CTBillEngine.InvoiceData> invoices,
    PX.Objects.AR.Customer customer,
    PX.Objects.CR.Location location,
    string action,
    Dictionary<int, List<TranNotePair>> sourceTran = null,
    List<UsageData> tranData = null)
  {
    ARInvoiceEntry invoiceGraph = CTBillEngine.CreateInvoiceGraph();
    foreach (CTBillEngine.InvoiceData invoice1 in invoices)
    {
      ((PXGraph) invoiceGraph).Clear((PXClearOption) 3);
      PX.Objects.AR.ARInvoice instance = (PX.Objects.AR.ARInvoice) ((PXSelectBase) invoiceGraph.Document).Cache.CreateInstance();
      instance.DocType = invoice1.GetDocType();
      int num1 = instance.DocType == "CRM" ? -1 : 1;
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Insert(instance);
      PX.Objects.AR.ARInvoice copy = (PX.Objects.AR.ARInvoice) ((PXSelectBase) invoiceGraph.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Current);
      copy.BranchID = location.CBranchID ?? ((PXGraph) this).Accessinfo.BranchID;
      copy.CustomerID = customer.BAccountID;
      copy.CustomerLocationID = location.LocationID;
      copy.DocDesc = this.GetInvoiceDescription(action, contract, customer, copy);
      copy.DocDate = CTBillEngine.ShiftDateIfPeriodClosed(invoiceGraph.FinPeriodRepository, invoice1.InvoiceDate, PXAccess.GetParentOrganizationID(copy.BranchID));
      PX.Objects.AR.ARInvoice arInvoice = ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Update(copy);
      ((PXSelectBase<PX.Objects.AR.Customer>) invoiceGraph.customer).Current.CreditRule = customer.CreditRule;
      arInvoice.ProjectID = contract.ContractID;
      arInvoice.CuryID = contract.CuryID;
      PX.Objects.AR.ARInvoice invoice2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Update(arInvoice);
      DateTime? nullable1;
      Decimal? nullable2;
      bool? nullable3;
      foreach (UsageData usageData in (IEnumerable<UsageData>) invoice1.UsageData.Where<UsageData>(new Func<UsageData, bool>(CTBillEngine.IsBillable)).OrderBy<UsageData, int?>((Func<UsageData, int?>) (item => item.ContractDetailsLineNbr)))
      {
        PX.Objects.AR.ARTran row = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Insert();
        nullable1 = usageData.TranDate;
        if (nullable1.HasValue)
          row.TranDate = usageData.TranDate;
        row.InventoryID = usageData.InventoryID;
        row.TranDesc = this.GetTransactionDescriptionWithPrefix(row, usageData);
        row.UOM = usageData.UOM;
        if (usageData.BranchID.HasValue)
          row.BranchID = usageData.BranchID;
        row.EmployeeID = usageData.EmployeeID;
        row.SalesPersonID = contract.SalesPersonID;
        row.CaseCD = usageData.CaseCD;
        PX.Objects.AR.ARTran arTran1 = row;
        nullable2 = usageData.Qty;
        Decimal num2 = (Decimal) num1;
        Decimal? nullable4 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num2) : new Decimal?();
        arTran1.Qty = nullable4;
        nullable3 = usageData.IsTranData;
        bool flag1 = false;
        if (!(nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue))
        {
          nullable3 = usageData.IsFree;
          if (nullable3.GetValueOrDefault())
          {
            row.CuryUnitPrice = new Decimal?(0M);
            row.CuryExtPrice = new Decimal?(0M);
          }
          else
          {
            nullable2 = usageData.PriceOverride;
            if (nullable2.HasValue)
            {
              PX.Objects.AR.ARTran arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(row);
              PX.Objects.AR.ARTran arTran3 = arTran2;
              nullable2 = usageData.PriceOverride;
              Decimal? nullable5 = new Decimal?(nullable2.Value);
              arTran3.CuryUnitPrice = nullable5;
              row = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(arTran2);
              nullable2 = row.CuryUnitPrice;
              Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
              nullable2 = usageData.PreciseQty;
              Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
              Decimal num3 = valueOrDefault1 * valueOrDefault2 * (Decimal) num1;
              nullable2 = usageData.Proportion;
              Decimal num4 = nullable2 ?? 1M;
              Decimal val = num3 * num4;
              row.CuryExtPrice = new Decimal?(((PXGraph) invoiceGraph).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(val));
            }
          }
        }
        else
        {
          row.Qty = new Decimal?(0M);
          row.UOM = (string) null;
          row.CuryUnitPrice = new Decimal?(0M);
          PX.Objects.AR.ARTran arTran4 = row;
          nullable2 = usageData.ExtPrice;
          Decimal num5 = (Decimal) num1;
          Decimal? nullable6 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num5) : new Decimal?();
          arTran4.CuryExtPrice = nullable6;
        }
        PX.Objects.AR.ARTran tran = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(row);
        nullable3 = usageData.IsTranData;
        bool flag2 = false;
        if (!(nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue))
        {
          nullable3 = usageData.IsFree;
          if (nullable3.GetValueOrDefault())
          {
            tran.CuryUnitPrice = new Decimal?(0M);
            tran.CuryExtPrice = new Decimal?(0M);
          }
        }
        this.SetDiscountsForTran(invoiceGraph, invoice2, tran, usageData.DiscountID);
        PX.Objects.AR.ARTran arTran5 = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(tran);
        nullable3 = usageData.IsTranData;
        bool flag3 = false;
        if (nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue)
        {
          arTran5.Qty = new Decimal?(0M);
          arTran5.UOM = (string) null;
          arTran5.CuryUnitPrice = new Decimal?(0M);
        }
        usageData.RefLineNbr = arTran5.LineNbr;
      }
      this.UpdateReferencePMTran2ARTran(invoiceGraph, sourceTran, tranData);
      nullable3 = template.AutomaticReleaseAR;
      if (nullable3.GetValueOrDefault())
      {
        ((PXGraph) invoiceGraph).Caches[typeof (PX.Objects.AR.ARInvoice)].SetValueExt<PX.Objects.AR.ARInvoice.hold>((object) invoice2, (object) false);
        invoice2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Update(invoice2);
      }
      ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
      {
        (object) contract.ContractID
      }));
      if (invoices.Count > 0)
      {
        if (action != "B" && contract.EffectiveFrom.HasValue)
        {
          DateTime? effectiveFrom = contract.EffectiveFrom;
          DateTime? nullable7 = contractBillingSchedule.NextDate;
          if ((effectiveFrom.HasValue == nullable7.HasValue ? (effectiveFrom.HasValue ? (effectiveFrom.GetValueOrDefault() == nullable7.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
          {
            nullable7 = contract.EffectiveFrom;
            nullable1 = contractBillingSchedule.LastDate;
            if ((nullable7.HasValue == nullable1.HasValue ? (nullable7.HasValue ? (nullable7.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
              goto label_31;
          }
          nullable2 = invoice2.CuryOrigDocAmt;
          Decimal num6 = 0M;
          if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
            continue;
        }
label_31:
        this.doclist.Add((PX.Objects.AR.ARRegister) ((PXGraph) invoiceGraph).Caches[typeof (PX.Objects.AR.ARInvoice)].Current);
        ((PXAction) invoiceGraph.Save).Press();
      }
    }
  }

  private void SetDiscountsForTran(
    ARInvoiceEntry invoiceEntry,
    PX.Objects.AR.ARInvoice invoice,
    PX.Objects.AR.ARTran tran,
    string discountID)
  {
    tran.ManualDisc = new bool?(true);
    tran.ManualPrice = new bool?(true);
    if (discountID == null || invoiceEntry.Transactions == null)
      return;
    PX.Objects.AR.ARTran copy = (PX.Objects.AR.ARTran) ((PXSelectBase) invoiceEntry.Transactions).Cache.CreateCopy((object) tran);
    tran.DiscountID = discountID;
    DiscountEngineProvider.GetEngineFor<PX.Objects.AR.ARTran, ARInvoiceDiscountDetail>().UpdateManualLineDiscount(((PXSelectBase) invoiceEntry.Transactions).Cache, (PXSelectBase<PX.Objects.AR.ARTran>) invoiceEntry.Transactions, tran, (PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails, invoice.BranchID, invoice.CustomerLocationID, invoice.DocDate, DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation);
    ((PXSelectBase) invoiceEntry.Transactions).Cache.RaiseRowUpdated((object) tran, (object) copy);
  }

  private void UndoInvoices(Contract contract)
  {
    List<PX.Objects.AR.ARInvoice> documents1 = new List<PX.Objects.AR.ARInvoice>();
    List<PX.Objects.AR.ARInvoice> documents2 = new List<PX.Objects.AR.ARInvoice>();
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    foreach (PX.Objects.AR.ARInvoice document in this.GetDocuments(instance, contract))
    {
      if (!document.Released.GetValueOrDefault())
      {
        documents1.Add(document);
      }
      else
      {
        if (!this.CanIgnoreInvoice(document))
          throw new PXException("The last action cannot be undone as the document has been released.");
        documents2.Add(document);
      }
    }
    CTBillEngine.Remove(instance, (IEnumerable<PX.Objects.AR.ARInvoice>) documents1);
    this.Unlink((IEnumerable<PX.Objects.AR.ARInvoice>) documents2);
  }

  private IEnumerable<PX.Objects.AR.ARInvoice> GetDocuments(
    ARInvoiceEntry invoicegraph,
    Contract contract)
  {
    return (IEnumerable<PX.Objects.AR.ARInvoice>) GraphHelper.RowCast<PX.Objects.AR.ARInvoice>((IEnumerable) PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<ContractBillingTrace, On<ContractBillingTrace.docType, Equal<PX.Objects.AR.ARInvoice.docType>, And<ContractBillingTrace.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>>, Where<ContractBillingTrace.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) invoicegraph, new object[1]
    {
      (object) contract.ContractID
    })).ToArray<PX.Objects.AR.ARInvoice>();
  }

  private static void Remove(ARInvoiceEntry invoiceEntry, IEnumerable<PX.Objects.AR.ARInvoice> documents)
  {
    if (!documents.Any<PX.Objects.AR.ARInvoice>())
      return;
    using (new ARInvoiceEntry.UnlinkContractUsagesOnDeleteScope())
    {
      foreach (PX.Objects.AR.ARInvoice document in documents)
      {
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceEntry.Document).Current = document;
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceEntry.Document).Delete(document);
        ((PXAction) invoiceEntry.Save).Press();
      }
    }
  }

  private void Unlink(IEnumerable<PX.Objects.AR.ARInvoice> documents)
  {
    if (!documents.Any<PX.Objects.AR.ARInvoice>())
      return;
    foreach (PMTran pmTran in documents.SelectMany<PX.Objects.AR.ARInvoice, PMTran>((Func<PX.Objects.AR.ARInvoice, IEnumerable<PMTran>>) (d => GraphHelper.RowCast<PMTran>((IEnumerable) PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.projectID, Equal<Current<Contract.contractID>>, And<PMTran.aRTranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PMTran.aRRefNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) d
    }, Array.Empty<object>())).Select<PMTran, PMTran>(new Func<PMTran, PMTran>(PXCache<PMTran>.CreateCopy)))))
    {
      pmTran.ARRefNbr = (string) null;
      pmTran.ARTranType = (string) null;
      pmTran.RefLineNbr = new int?();
      pmTran.Billed = new bool?(false);
      ((PXSelectBase<PMTran>) this.Transactions).Update(pmTran);
    }
  }

  private bool CanIgnoreInvoice(PX.Objects.AR.ARInvoice invoice)
  {
    if (!invoice.Released.GetValueOrDefault() || invoice.OpenDoc.GetValueOrDefault() || invoice.DocType != "INV" && invoice.DocType != "CRM")
      return false;
    string str = invoice.DocType == "INV" ? "CRM" : "DRM";
    List<PXResult<ARAdjust>> pxResultList = (List<PXResult<ARAdjust>>) null;
    if (invoice.DocType == "INV")
      pxResultList = ((IEnumerable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) new PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<ARAdjust.adjgDocType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<ARAdjust.adjgRefNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARAdjust.adjdRefNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, Equal<False>>>>>>((PXGraph) this)).Select(new object[2]
      {
        (object) invoice.DocType,
        (object) invoice.RefNbr
      })).ToList<PXResult<ARAdjust>>();
    else if (invoice.DocType == "CRM")
      pxResultList = ((IEnumerable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) new PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.ARRegister.refNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where<ARAdjust.adjgDocType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARAdjust.adjgRefNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, Equal<False>>>>>>((PXGraph) this)).Select(new object[2]
      {
        (object) invoice.DocType,
        (object) invoice.RefNbr
      })).ToList<PXResult<ARAdjust>>();
    if (pxResultList.Count != 1)
      return false;
    PX.Objects.AR.ARRegister arRegister = ((PXResult) pxResultList[0]).GetItem<PX.Objects.AR.ARRegister>();
    return arRegister.DocType == str && arRegister.OrigDocType == invoice.DocType && arRegister.OrigRefNbr == invoice.RefNbr;
  }

  private static ARInvoiceEntry CreateInvoiceGraph()
  {
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    AROpenPeriodAttribute.DefaultFirstOpenPeriod<PX.Objects.AR.ARInvoice.finPeriodID>(((PXSelectBase) instance.Document).Cache);
    ((PXSelectBase<ARSetup>) instance.ARSetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<ARSetup>) instance.ARSetup).Current.LineDiscountTarget = "E";
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.AR.ARInvoice.projectID>(CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_0 ?? (CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_0 = new PXFieldVerifying((object) CTBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateInvoiceGraph\u003Eb__77_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.AR.ARTran.projectID>(CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_1 ?? (CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_1 = new PXFieldVerifying((object) CTBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateInvoiceGraph\u003Eb__77_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldDefaulting.AddHandler<PX.Objects.AR.ARInvoice.retainageApply>(CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_2 ?? (CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_2 = new PXFieldDefaulting((object) CTBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateInvoiceGraph\u003Eb__77_2))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.AR.ARInvoice.retainageApply>(CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_3 ?? (CTBillEngine.\u003C\u003Ec.\u003C\u003E9__77_3 = new PXFieldVerifying((object) CTBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateInvoiceGraph\u003Eb__77_3))));
    return instance;
  }

  public virtual void Bill(int? contractID, DateTime? date = null)
  {
    Contract contract1 = contractID.HasValue ? PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    })) : throw new ArgumentNullException(nameof (contractID));
    ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    if (!contract1.IsActive.GetValueOrDefault())
      throw new PXException("Contract must be Active.");
    if (contract1.IsCancelled.GetValueOrDefault())
      throw new PXException("Contract is Terminated and cannot be Billed.");
    if (contract1.IsCompleted.GetValueOrDefault())
      throw new PXException("Contract is Completed/Expired and cannot be Billed.");
    if (schedule.Type == "D" && !date.HasValue)
      throw new PXException("Billing Date must be Set");
    DateTime? nullable1 = date;
    DateTime? nullable2 = contract1.ExpireDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("Billing Date is greater than Expiration Date");
    nullable2 = date;
    nullable1 = schedule.StartBilling;
    if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("Billing Date is less than Billing Schedule Start Date");
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract1))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    PX.Objects.AR.Customer customer1;
    PX.Objects.CR.Location location1;
    this.SetBillingTarget(contract1, out customer1, out location1);
    this.ValidateCustomerInfo(contract1, customer1, location1);
    ContractBillingTrace contractBillingTrace = new ContractBillingTrace()
    {
      ContractID = contractID,
      LastDate = schedule.LastDate,
      NextDate = schedule.NextDate
    };
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    if (schedule.Type == "D")
    {
      schedule.NextDate = date;
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(schedule);
    }
    Contract contract2 = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract1.TemplateID
    });
    this.availableQty = new Dictionary<int?, Decimal?>();
    if (this.IsLastBillBeforeExpiration(contract1, schedule))
    {
      this.RaiseErrorIfUnreleasedUsageExist(contract1);
      contract1.IsCompleted = new bool?(true);
      contract1.ServiceActivate = new bool?(false);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ExpireContract))).FireOn((PXGraph) this, contract1);
    }
    this.CreateNewRevision(contract1.ContractID, "B", contract1.Status);
    List<UsageData> data;
    Dictionary<int, List<TranNotePair>> sourceTran1;
    List<UsageData> tranData1;
    this.RecalcUsage(contract1, out data, out sourceTran1, out tranData1);
    DateTime? nullable3;
    if (schedule.Type == "D")
    {
      nullable3 = date;
    }
    else
    {
      nullable3 = schedule.NextDate;
      schedule.NextDate = this.GetNextBillingDateConsiderExpiration(schedule.Type, contract1.CustomerID, schedule.NextDate, schedule.StartBilling, contract1.ExpireDate);
    }
    ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(schedule);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (data.Count > 0 && data.Any<UsageData>(new Func<UsageData, bool>(CTBillEngine.IsBillable)))
      {
        CTBillEngine.InvoiceData invoiceData = new CTBillEngine.InvoiceData(nullable3.Value);
        invoiceData.UsageData.AddRange((IEnumerable<UsageData>) data);
        Contract contract3 = contract1;
        Contract template = contract2;
        List<CTBillEngine.InvoiceData> invoices = new List<CTBillEngine.InvoiceData>();
        invoices.Add(invoiceData);
        PX.Objects.AR.Customer customer2 = customer1;
        PX.Objects.CR.Location location2 = location1;
        Dictionary<int, List<TranNotePair>> sourceTran2 = sourceTran1;
        List<UsageData> tranData2 = tranData1;
        this.CreateInvoice(contract3, template, invoices, customer2, location2, "B", sourceTran2, tranData2);
        if (this.doclist.Any<PX.Objects.AR.ARRegister>())
        {
          contractBillingTrace.DocType = this.doclist[0].DocType;
          contractBillingTrace.RefNbr = this.doclist[0].RefNbr;
          ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(contractBillingTrace);
        }
      }
      if (schedule.Type == "D")
        schedule.NextDate = new DateTime?();
      schedule.LastDate = nullable3;
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(schedule);
      this.UpdateHistory(contract1);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract1);
  }

  public virtual void Renew(int? contractID, DateTime renewalDate)
  {
    Contract contract1 = contractID.HasValue ? PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    })) : throw new ArgumentNullException(nameof (contractID));
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    if (contractBillingSchedule != null)
    {
      object accountId = (object) contractBillingSchedule.AccountID;
      ((PXSelectBase) this.BillingSchedule).Cache.RaiseFieldVerifying<ContractBillingSchedule.accountID>((object) contractBillingSchedule, ref accountId);
    }
    if (contract1.IsCancelled.GetValueOrDefault())
      throw new PXException("Contract is Terminated and cannot be Activated.");
    if (!contract1.ExpireDate.HasValue)
      throw new PXException("Contract Expire date must be not null for a Contract that is being Renewed.");
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract1))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract1, out customer, out location);
    this.ValidateCustomerInfo(contract1, customer, location);
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    DateTime? expireDate = contract1.ExpireDate;
    DateTime date = expireDate.Value;
    date = date.Date;
    DateTime dateTime = date.AddDays(1.0);
    contract1.RenewalBillingStartDate = new DateTime?(dateTime);
    contract1.IsActive = new bool?(true);
    contract1.IsCompleted = new bool?(false);
    contract1.ServiceActivate = new bool?(true);
    Contract copy1 = PXCache<Contract>.CreateCopy(contract1);
    copy1.StartDate = new DateTime?(dateTime);
    contract1.ExpireDate = PXFormulaAttribute.Evaluate<Contract.expireDate>(((PXSelectBase) this.Contracts).Cache, (object) copy1) as DateTime?;
    ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ActivateContract))).FireOn((PXGraph) this, contract1);
    expireDate = contract1.ExpireDate;
    if (!expireDate.HasValue)
      throw new PXException("Can't calculate Expiration Date for Contract '{0}'.", new object[1]
      {
        (object) contract1.ContractCD
      });
    this.CreateNewRevision(contractID, "R", "A");
    if (contractBillingSchedule.Type != "D")
    {
      contractBillingSchedule.NextDate = new DateTime?(dateTime);
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(contractBillingSchedule);
    }
    Contract contract2 = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }));
    ContractRenewalHistory contractRenewalHistory = ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).SelectSingle(new object[2]
    {
      (object) contract2.ContractID,
      (object) contract2.RevID
    });
    contractRenewalHistory.RenewalDate = new DateTime?(renewalDate);
    ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).Update(contractRenewalHistory);
    Contract contract3 = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract2.TemplateID
    });
    this.availableQty = new Dictionary<int?, Decimal?>();
    List<UsageData> renewalUsage = this.GetRenewalUsage(contract2);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (renewalUsage.Count > 0)
      {
        ARInvoiceEntry invoiceGraph = CTBillEngine.CreateInvoiceGraph();
        PX.Objects.AR.ARInvoice invoice = ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Insert(new PX.Objects.AR.ARInvoice());
        PX.Objects.AR.ARInvoice copy2 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) invoiceGraph.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Current);
        copy2.DocType = "INV";
        copy2.DocDate = new DateTime?(renewalDate);
        copy2.BranchID = location.CBranchID ?? ((PXGraph) this).Accessinfo.BranchID;
        copy2.CustomerID = customer.BAccountID;
        copy2.CustomerLocationID = location.LocationID;
        copy2.ProjectID = contract2.ContractID;
        copy2.CuryID = contract2.CuryID;
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Update(copy2);
        invoice.DocDesc = this.GetInvoiceDescription("R", contract2, customer, invoice);
        ((PXSelectBase<PX.Objects.AR.Customer>) invoiceGraph.customer).Current.CreditRule = customer.CreditRule;
        foreach (UsageData usageData in (IEnumerable<UsageData>) renewalUsage.Where<UsageData>(new Func<UsageData, bool>(CTBillEngine.IsBillable)).OrderBy<UsageData, int?>((Func<UsageData, int?>) (item => item.ContractDetailsLineNbr)))
        {
          Decimal? nullable1 = usageData.Qty;
          Decimal num1 = 0M;
          if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
          {
            nullable1 = usageData.ExtPrice;
            Decimal num2 = 0M;
            if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
              continue;
          }
          PX.Objects.AR.ARTran row = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Insert();
          row.InventoryID = usageData.InventoryID;
          row.TranDesc = this.GetTransactionDescriptionWithPrefix(row, usageData);
          row.Qty = usageData.Qty;
          row.UOM = usageData.UOM;
          row.ProjectID = contract2.ContractID;
          if (usageData.BranchID.HasValue)
            row.BranchID = usageData.BranchID;
          row.EmployeeID = usageData.EmployeeID;
          row.SalesPersonID = contract2.SalesPersonID;
          row.CaseCD = usageData.CaseCD;
          row.Qty = usageData.Qty;
          bool? nullable2 = usageData.IsTranData;
          bool flag1 = false;
          if (!(nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue))
          {
            nullable2 = usageData.IsFree;
            if (nullable2.GetValueOrDefault())
            {
              row.CuryUnitPrice = new Decimal?(0M);
              row.CuryExtPrice = new Decimal?(0M);
            }
            else
            {
              nullable1 = usageData.PriceOverride;
              if (nullable1.HasValue)
              {
                PX.Objects.AR.ARTran arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(row);
                arTran.CuryUnitPrice = usageData.PriceOverride;
                row = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(arTran);
                nullable1 = row.CuryUnitPrice;
                Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
                nullable1 = usageData.PreciseQty;
                Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
                Decimal num3 = valueOrDefault1 * valueOrDefault2;
                nullable1 = usageData.Proportion;
                Decimal num4 = nullable1 ?? 1M;
                Decimal val = num3 * num4;
                row.CuryExtPrice = new Decimal?(((PXGraph) invoiceGraph).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(val));
              }
            }
          }
          else
          {
            row.Qty = new Decimal?(0M);
            row.UOM = (string) null;
            row.CuryUnitPrice = new Decimal?(0M);
            nullable1 = usageData.Proportion;
            Decimal num5 = nullable1 ?? 1M;
            nullable1 = usageData.ExtPrice;
            Decimal valueOrDefault = nullable1.GetValueOrDefault();
            Decimal val = num5 * valueOrDefault;
            row.CuryExtPrice = new Decimal?(((PXGraph) invoiceGraph).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(val));
          }
          PX.Objects.AR.ARTran tran = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(row);
          nullable2 = usageData.IsTranData;
          bool flag2 = false;
          if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
          {
            nullable2 = usageData.IsFree;
            if (nullable2.GetValueOrDefault())
            {
              tran.CuryUnitPrice = new Decimal?(0M);
              tran.CuryExtPrice = new Decimal?(0M);
            }
          }
          this.SetDiscountsForTran(invoiceGraph, invoice, tran, usageData.DiscountID);
          PX.Objects.AR.ARTran arTran1 = ((PXSelectBase<PX.Objects.AR.ARTran>) invoiceGraph.Transactions).Update(tran);
          nullable2 = usageData.IsTranData;
          bool flag3 = false;
          if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
          {
            arTran1.Qty = new Decimal?(0M);
            arTran1.UOM = (string) null;
            arTran1.CuryUnitPrice = new Decimal?(0M);
          }
          usageData.RefLineNbr = arTran1.LineNbr;
        }
        if (contract3.AutomaticReleaseAR.GetValueOrDefault())
        {
          ((PXGraph) invoiceGraph).Caches[typeof (PX.Objects.AR.ARInvoice)].SetValueExt<PX.Objects.AR.ARInvoice.hold>((object) invoice, (object) false);
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) invoiceGraph.Document).Update(invoice);
        }
        this.doclist.Add((PX.Objects.AR.ARRegister) ((PXGraph) invoiceGraph).Caches[typeof (PX.Objects.AR.ARInvoice)].Current);
        ((PXGraph) invoiceGraph).Actions.PressSave();
        ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(new ContractBillingTrace()
        {
          ContractID = contractID,
          LastDate = contractBillingSchedule.LastDate,
          NextDate = contractBillingSchedule.NextDate,
          DocType = this.doclist[0].DocType,
          RefNbr = this.doclist[0].RefNbr
        });
      }
      this.UpdateHistory(contract2);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract2);
  }

  public virtual void Terminate(int? contractID, DateTime? date)
  {
    if (!contractID.HasValue)
      throw new ArgumentNullException(nameof (contractID));
    if (!date.HasValue)
      throw new ArgumentNullException(nameof (date));
    Contract contract = PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }));
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    if (contract.IsCancelled.GetValueOrDefault())
      throw new PXException("Contract is already Terminated");
    DateTime? nullable = contractBillingSchedule.LastDate;
    DateTime dateTime1 = nullable ?? contract.StartDate.Value;
    if (date.Value < dateTime1)
      throw new PXException("Termination date of a Contract cannot be earlier than the Last Billing Date of the Contract");
    if (contractBillingSchedule.Type != "D")
    {
      nullable = contractBillingSchedule.NextDate;
      if (nullable.HasValue)
      {
        DateTime dateTime2 = date.Value;
        nullable = contractBillingSchedule.NextDate;
        DateTime dateTime3 = nullable.Value;
        if (dateTime2 > dateTime3)
          throw new PXException("Termination date of a Contract cannot be later than the Next Billing Date of the Contract");
      }
    }
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(contract))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    this.RaiseErrorIfUnreleasedUsageExist(contract);
    ContractBillingTrace contractBillingTrace = new ContractBillingTrace()
    {
      ContractID = contractID,
      LastDate = contractBillingSchedule.LastDate,
      NextDate = contractBillingSchedule.NextDate
    };
    this.ClearState();
    CTBillEngine.ClearBillingTrace(contractID);
    Contract template = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract.TemplateID
    });
    contract.IsCancelled = new bool?(true);
    contract.IsActive = new bool?(false);
    contract.TerminationDate = date;
    contract.ServiceActivate = new bool?(false);
    ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.CancelContract))).FireOn((PXGraph) this, contract);
    this.CreateNewRevision(contractID, "T", "C");
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract, out customer, out location);
    List<CTBillEngine.InvoiceData> invoices = new List<CTBillEngine.InvoiceData>();
    CTBillEngine.InvoiceData invoiceData = new CTBillEngine.InvoiceData(date.Value);
    Dictionary<int, List<TranNotePair>> sourceTran;
    List<UsageData> tranData;
    using (new PXLocaleScope(customer.LocaleName))
    {
      List<UsageData> terminationFee = this.GetTerminationFee(contract, contractBillingSchedule.LastDate, contractBillingSchedule.NextDate, date.Value, out sourceTran, out tranData);
      invoiceData.UsageData.AddRange((IEnumerable<UsageData>) terminationFee);
    }
    invoiceData.UsageData.RemoveAll((Predicate<UsageData>) (ud =>
    {
      Decimal? proportion = ud.Proportion;
      Decimal num = 0.0M;
      if (!(proportion.GetValueOrDefault() == num & proportion.HasValue))
        return false;
      bool? isTranData = ud.IsTranData;
      bool flag = false;
      return !(isTranData.GetValueOrDefault() == flag & isTranData.HasValue);
    }));
    if (invoiceData.UsageData.Count > 0)
      invoices.Add(invoiceData);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (invoices.Count > 0)
      {
        using (new PXLocaleScope(customer.LocaleName))
          this.CreateInvoice(contract, template, invoices, customer, location, "T", sourceTran, tranData);
      }
      foreach (PX.Objects.AR.ARRegister arRegister in this.doclist)
        ((PXSelectBase<ContractBillingTrace>) this.BillingTrace).Insert(new ContractBillingTrace()
        {
          ContractID = contractID,
          DocType = arRegister.DocType,
          RefNbr = arRegister.RefNbr,
          LastDate = contractBillingTrace.LastDate,
          NextDate = contractBillingTrace.NextDate
        });
      contractBillingSchedule.LastDate = date;
      contractBillingSchedule.NextDate = new DateTime?();
      ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(contractBillingSchedule);
      this.UpdateHistory(contract);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
    this.EnsureContractDetailTranslations();
    this.AutoReleaseInvoice(contract);
  }

  public virtual void UndoBilling(int? contractID)
  {
    Contract contract = contractID.HasValue ? PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    })) : throw new ArgumentNullException(nameof (contractID));
    if (!contract.IsLastActionUndoable.GetValueOrDefault())
      throw new PXException("Last action can not be undone.");
    ContractRenewalHistory history = ((PXSelectBase<ContractRenewalHistory>) new PXSelect<ContractRenewalHistory, Where<ContractRenewalHistory.contractID, Equal<Required<Contract.contractID>>, And<ContractRenewalHistory.revID, Equal<Required<Contract.revID>>>>>((PXGraph) this)).SelectSingle(new object[2]
    {
      (object) contract.ContractID,
      (object) (contract.RevID.GetValueOrDefault() - 1)
    });
    if (history == null)
      throw new PXException("Last action can not be undone.");
    ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    ContractRenewalHistory contractRenewalHistory = ((PXSelectBase<ContractRenewalHistory>) this.CurrentRenewalHistory).SelectSingle(new object[2]
    {
      (object) contract.ContractID,
      (object) contract.RevID
    });
    contract.LastActiveRevID = this.GetLastActiveRevisionID(contract);
    contract.RevID = history.RevID;
    contract.Status = history.Status;
    contract.IsLastActionUndoable = new bool?(false);
    ((PXSelectBase<Contract>) this.Contracts).Update(contract);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.UndoInvoices(contract);
      this.RestoreScheduleFromHistory(schedule, history);
      if (contractRenewalHistory.Action == "T")
        contract.TerminationDate = new DateTime?();
      if (contractRenewalHistory.Action == "R" && contractRenewalHistory.ChildContractID.HasValue)
      {
        ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
        ((PXGraph) instance).Clear();
        ((PXSelectBase<Contract>) instance.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) contractRenewalHistory.ChildContractID
        }));
        ((PXSelectBase<ContractBillingSchedule>) instance.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) instance.Billing).Select(Array.Empty<object>()));
        try
        {
          ((PXAction) instance.Delete).Press();
        }
        catch (Exception ex)
        {
          object[] objArray = Array.Empty<object>();
          throw new PXException(ex, "Cannot to delete Renewing Contract", objArray);
        }
      }
      this.RestoreFieldsFromHistory(contract, history);
      foreach (PXResult<ContractDetail> pxResult in PXSelectBase<ContractDetail, PXViewOf<ContractDetail>.BasedOn<SelectFromBase<ContractDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ContractDetail.contractID, Equal<P.AsInt>>>>>.And<BqlOperand<ContractDetail.revID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) contract.ContractID,
        (object) contractRenewalHistory.RevID
      }))
      {
        ContractDetail contractDetail1 = PXResult<ContractDetail>.op_Implicit(pxResult);
        ContractDetail contractDetail2 = (ContractDetail) PXResultset<ContractDetailExt>.op_Implicit(PXSelectBase<ContractDetailExt, PXViewOf<ContractDetailExt>.BasedOn<SelectFromBase<ContractDetailExt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ContractDetail.contractID, Equal<P.AsInt>>>>, And<BqlOperand<ContractDetail.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<ContractDetail.revID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
        {
          (object) contract.ContractID,
          (object) contractDetail1.LineNbr,
          (object) history.RevID
        }));
        ContractDetail contractDetail3 = contractDetail2;
        Decimal? usedTotal = contractDetail1.UsedTotal;
        Decimal? nullable1 = contractDetail2.UsedTotal;
        Decimal? nullable2 = usedTotal.HasValue & nullable1.HasValue ? new Decimal?(usedTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal? used = contractDetail2.Used;
        Decimal? nullable3;
        if (!(nullable2.HasValue & used.HasValue))
        {
          nullable1 = new Decimal?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new Decimal?(nullable2.GetValueOrDefault() + used.GetValueOrDefault());
        contractDetail3.Used = nullable3;
        contractDetail2.UsedTotal = contractDetail1.UsedTotal;
        ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(contractDetail2);
      }
      CTBillEngine.ClearBillingTrace(contract.ContractID);
      this.ClearFuture(contract);
      ((PXGraph) this).Actions.PressSave();
      transactionScope.Complete();
    }
  }

  private void RestoreFieldsFromHistory(Contract contract, ContractRenewalHistory history)
  {
    Contract copy = PXCache<Contract>.CreateCopy(contract);
    copy.EffectiveFrom = history.EffectiveFrom;
    copy.ActivationDate = history.ActivationDate;
    copy.StartDate = history.StartDate;
    copy.ExpireDate = history.ExpireDate;
    copy.IsActive = history.IsActive;
    copy.IsCancelled = history.IsCancelled;
    copy.IsCompleted = history.IsCompleted;
    copy.IsPendingUpdate = history.IsPendingUpdate;
    copy.DiscountID = history.DiscountID;
    ((PXSelectBase<Contract>) this.Contracts).Update(copy);
  }

  private void RestoreScheduleFromHistory(
    ContractBillingSchedule schedule,
    ContractRenewalHistory history)
  {
    schedule.LastDate = history.LastDate;
    schedule.NextDate = history.NextDate;
    schedule.StartBilling = history.StartBilling;
    ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(schedule);
  }

  private int? GetLastActiveRevisionID(Contract contract)
  {
    if (contract == null)
      return new int?();
    return ((PXSelectBase<ContractRenewalHistory>) new PXSelect<ContractRenewalHistory, Where<ContractRenewalHistory.contractID, Equal<Required<Contract.contractID>>, And<ContractRenewalHistory.status, Equal<Contract.status.active>, And<ContractRenewalHistory.revID, Less<Required<Contract.revID>>>>>, OrderBy<Desc<ContractRenewalHistory.revID>>>((PXGraph) this)).SelectSingle(new object[2]
    {
      (object) contract.ContractID,
      (object) contract.RevID
    })?.RevID;
  }

  private void UpdateStatusFlags(Contract contract)
  {
    contract.IsActive = new bool?(contract.Status == "A" || contract.Status == "U");
    contract.IsCancelled = new bool?(contract.Status == "C");
    contract.IsCompleted = new bool?(contract.Status == "F");
    contract.IsPendingUpdate = new bool?(contract.Status == "U");
  }

  public virtual void Upgrade(int? contractID)
  {
    if (!(((PXSelectBase) this.Contracts).Cache.CreateCopy((object) PXResultset<Contract>.op_Implicit(((PXSelectBase<Contract>) this.Contracts).Select(new object[1]
    {
      (object) contractID
    }))) is Contract copy))
      throw new ArgumentNullException(nameof (contractID));
    bool? nullable = copy.IsActive;
    nullable = nullable.GetValueOrDefault() ? copy.IsCancelled : throw new PXException("Contract must be Active.");
    nullable = !nullable.GetValueOrDefault() ? copy.IsCompleted : throw new PXException("Contract is Terminated and cannot be Upgraded.");
    if (nullable.GetValueOrDefault())
      throw new PXException("Contract is Completed/Expired and cannot be Upgraded.");
    foreach (PXResult<ContractItem, ContractDetail> contractDetail in this.GetContractDetails(copy))
    {
      ContractItem contractItem = PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail);
      string message;
      if (!ContractMaint.IsValidDetailPrice((PXGraph) this, PXResult<ContractItem, ContractDetail>.op_Implicit(contractDetail), out message))
        throw new PXException("{0} has no {1} in this Currency", new object[2]
        {
          (object) contractItem.ContractItemCD,
          (object) message
        });
    }
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contractID
    }));
    copy.IsPendingUpdate = new bool?(true);
    copy.EffectiveFrom = contractBillingSchedule.NextDate ?? ((PXGraph) this).Accessinfo.BusinessDate;
    Contract contract = ((PXSelectBase<Contract>) this.Contracts).Update(copy);
    ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.UpgradeContract))).FireOn((PXGraph) this, contract);
    this.CreateNewRevision(contractID, "U", "U");
    this.UpdateHistory(contract);
    ((PXGraph) this).Actions.PressSave();
    this.EnsureContractDetailTranslations();
  }

  protected virtual List<UsageData> GetSetupFee(Contract contract)
  {
    List<UsageData> setupFee = new List<UsageData>();
    foreach (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<ContractDetail, PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.baseItemID>>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.isBaseValid, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetail detail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      UsageData usageData1 = new UsageData();
      usageData1.ContractItemID = contractItem.ContractItemID;
      usageData1.ContractDetailID = detail.ContractDetailID;
      usageData1.ActionItem = PXMessages.LocalizeNoPrefix("Setup");
      usageData1.InventoryID = contractItem.BaseItemID;
      usageData1.ContractDetailsLineNbr = detail.LineNbr;
      usageData1.Description = this.GetDescriptionMessageForDetail(CTBillEngine.InventoryAction.Setup, detail);
      usageData1.UOM = inventoryItem.BaseUnit;
      usageData1.Qty = detail.Qty;
      usageData1.PriceOverride = detail.BasePriceVal;
      UsageData usageData2 = usageData1;
      Decimal? qty = usageData1.Qty;
      Decimal? basePriceVal = detail.BasePriceVal;
      Decimal? nullable = qty.HasValue & basePriceVal.HasValue ? new Decimal?(qty.GetValueOrDefault() * basePriceVal.GetValueOrDefault()) : new Decimal?();
      usageData2.ExtPrice = nullable;
      usageData1.DiscountID = detail.BaseDiscountID;
      usageData1.DiscountSeq = detail.BaseDiscountSeq;
      setupFee.Add(usageData1);
      if (contractItem.Deposit.GetValueOrDefault())
      {
        detail.DepositAmt = usageData1.ExtPrice;
        ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(detail);
      }
    }
    return setupFee;
  }

  protected virtual PXResultset<ContractItem> GetContractDetails(Contract contract)
  {
    return this.GetContractDetails(contract.ContractID, contract.RevID);
  }

  protected virtual PXResultset<ContractItem> GetContractDetails(int? contractID, int? revisionID)
  {
    return PXSelectBase<ContractItem, PXSelectJoin<ContractItem, InnerJoin<ContractDetail, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.revID, Equal<Required<Contract.revID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractID,
      (object) revisionID
    });
  }

  protected virtual List<UsageData> GetActivationFee(Contract contract, DateTime? date)
  {
    List<UsageData> activationFee = new List<UsageData>();
    foreach (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<ContractDetail>) new PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.renewalItemID>>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.isRenewalValid, Equal<True>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventory = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      if (contractItem.CollectRenewFeeOnActivation.GetValueOrDefault())
      {
        UsageData usageData1 = new UsageData();
        usageData1.ContractItemID = contractItem.ContractItemID;
        usageData1.ContractDetailID = contractDetail.ContractDetailID;
        usageData1.ActionItem = PXMessages.LocalizeNoPrefix("Activate/Renew");
        usageData1.InventoryID = inventory.InventoryID;
        usageData1.ContractDetailsLineNbr = contractDetail.LineNbr;
        usageData1.Description = this.GetDescriptionMessageForInventory(CTBillEngine.InventoryAction.ActivateRenew, inventory);
        usageData1.UOM = inventory.BaseUnit;
        usageData1.Qty = contractDetail.Qty;
        usageData1.PriceOverride = contractDetail.RenewalPriceVal;
        UsageData usageData2 = usageData1;
        Decimal? qty = contractDetail.Qty;
        Decimal? renewalPriceVal = contractDetail.RenewalPriceVal;
        Decimal? nullable = qty.HasValue & renewalPriceVal.HasValue ? new Decimal?(qty.GetValueOrDefault() * renewalPriceVal.GetValueOrDefault()) : new Decimal?();
        usageData2.ExtPrice = nullable;
        usageData1.DiscountID = contractDetail.RenewalDiscountID;
        usageData1.DiscountSeq = contractDetail.RenewalDiscountSeq;
        activationFee.Add(usageData1);
      }
    }
    return activationFee;
  }

  protected virtual void GetUpgradeSetup(
    IEnumerable<PXResult<ContractDetail, ContractItem>> details,
    List<UsageData> usagedata,
    Decimal? prorate)
  {
    foreach (PXResult<ContractDetail, ContractItem> detail in details)
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      Decimal? nullable1 = contractDetail.Change;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      int? nullable2 = contractItem.BaseItemID;
      if (nullable2.HasValue && valueOrDefault != 0M)
      {
        bool? nullable3;
        if (!(valueOrDefault > 0M))
        {
          nullable3 = contractItem.Refundable;
          if (!nullable3.GetValueOrDefault())
            continue;
        }
        PX.Objects.IN.InventoryItem inventory = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.BaseItemID);
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.RenewalItemID);
        UsageData usageData1 = new UsageData();
        usageData1.ContractItemID = contractItem.ContractItemID;
        usageData1.ContractDetailID = contractDetail.ContractDetailID;
        usageData1.ActionItem = PXMessages.LocalizeNoPrefix("Setup Upgrade");
        usageData1.InventoryID = contractItem.BaseItemID;
        usageData1.ContractDetailsLineNbr = contractDetail.LineNbr;
        usageData1.Description = this.GetDescriptionMessageForInventory(CTBillEngine.InventoryAction.SetupUpgrade, inventory);
        usageData1.UOM = inventory.BaseUnit;
        usageData1.Qty = new Decimal?(valueOrDefault);
        usageData1.PriceOverride = contractDetail.BasePriceVal;
        UsageData usageData2 = usageData1;
        nullable1 = usageData1.Qty;
        Decimal? nullable4 = contractDetail.BasePriceVal;
        Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
        usageData2.ExtPrice = nullable5;
        usageData1.DiscountID = contractDetail.BaseDiscountID;
        usageData1.DiscountSeq = contractDetail.BaseDiscountSeq;
        nullable3 = contractItem.ProrateSetup;
        usageData1.Proportion = !nullable3.GetValueOrDefault() ? new Decimal?((Decimal) 1) : prorate;
        usagedata?.Add(usageData1);
        nullable2 = contractItem.RenewalItemID;
        if (nullable2.HasValue)
        {
          nullable3 = contractItem.CollectRenewFeeOnActivation;
          if (nullable3.GetValueOrDefault())
          {
            UsageData usageData3 = new UsageData();
            usageData3.ContractItemID = contractItem.ContractItemID;
            usageData3.ContractDetailID = contractDetail.ContractDetailID;
            usageData3.ActionItem = PXMessages.LocalizeNoPrefix("Upgrade Activation");
            usageData3.InventoryID = contractItem.RenewalItemID;
            usageData3.ContractDetailsLineNbr = contractDetail.LineNbr;
            usageData3.Description = this.GetDescriptionMessageForInventory(CTBillEngine.InventoryAction.UpgradeActivation, inventory);
            usageData3.UOM = inventoryItem.BaseUnit;
            usageData3.Qty = new Decimal?(valueOrDefault);
            usageData3.PriceOverride = contractDetail.RenewalPriceVal;
            UsageData usageData4 = usageData3;
            nullable4 = usageData3.Qty;
            nullable1 = contractDetail.RenewalPriceVal;
            Decimal? nullable6 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
            usageData4.ExtPrice = nullable6;
            usageData3.DiscountID = contractDetail.RenewalDiscountID;
            usageData3.DiscountSeq = contractDetail.RenewalDiscountSeq;
            usageData3.Proportion = prorate;
            usagedata?.Add(usageData3);
          }
        }
      }
    }
  }

  protected virtual void GetUpgradeRecurring(
    IEnumerable<PXResult<ContractDetail, ContractItem>> details,
    List<UsageData> usagedata,
    Decimal? prorate)
  {
    foreach (PXResult<ContractDetail, ContractItem> detail in details)
    {
      ContractDetail det = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      Decimal? nullable1 = det.Change;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      int? nullable2 = contractItem.RecurringItemID;
      if (nullable2.HasValue)
      {
        nullable2 = contractItem.DepositItemID;
        if (!nullable2.HasValue && (contractItem.RecurringType == "P" || valueOrDefault < 0M))
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.RecurringItemID);
          string str = PXMessages.LocalizeNoPrefix(contractItem.RecurringType == "P" ? "Prepaid" : "Included");
          UsageData usageData1 = new UsageData();
          usageData1.InventoryID = contractItem.RecurringItemID;
          usageData1.ContractDetailsLineNbr = det.LineNbr;
          usageData1.ContractDetailID = det.ContractDetailID;
          usageData1.ContractItemID = contractItem.ContractItemID;
          usageData1.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
          usageData1.UOM = inventoryItem.BaseUnit;
          usageData1.Qty = new Decimal?(contractItem.RecurringType == "P" ? valueOrDefault : -valueOrDefault);
          usageData1.PriceOverride = det.FixedRecurringPriceVal;
          UsageData usageData2 = usageData1;
          Decimal num1 = contractItem.RecurringType == "P" ? valueOrDefault : -valueOrDefault;
          nullable1 = det.FixedRecurringPriceVal;
          Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
          usageData2.ExtPrice = nullable3;
          usageData1.Prefix = str;
          UsageData usageData3 = usageData1;
          Decimal? nullable4;
          if (!(contractItem.RecurringType == "P"))
          {
            Decimal num2 = (Decimal) 1;
            nullable1 = prorate;
            nullable4 = nullable1.HasValue ? new Decimal?(num2 - nullable1.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable4 = prorate;
          usageData3.Proportion = nullable4;
          usageData1.DiscountID = det.RecurringDiscountID;
          usageData1.DiscountSeq = det.RecurringDiscountSeq;
          usagedata.Add(usageData1);
        }
      }
      nullable2 = contractItem.RecurringItemID;
      if (nullable2.HasValue)
      {
        nullable2 = contractItem.DepositItemID;
        if (!nullable2.HasValue && contractItem.RecurringType == "U" && valueOrDefault > 0M)
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.RecurringItemID);
          string str = PXMessages.LocalizeNoPrefix("Included");
          UsageData usageData4 = new UsageData();
          usageData4.InventoryID = contractItem.RecurringItemID;
          usageData4.ContractDetailsLineNbr = det.LineNbr;
          usageData4.ContractDetailID = det.ContractDetailID;
          usageData4.ContractItemID = contractItem.ContractItemID;
          usageData4.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
          usageData4.UOM = inventoryItem.BaseUnit;
          usageData4.Qty = new Decimal?(-valueOrDefault);
          usageData4.PriceOverride = det.FixedRecurringPriceVal;
          UsageData usageData5 = usageData4;
          Decimal num3 = -valueOrDefault;
          nullable1 = det.FixedRecurringPriceVal;
          Decimal? nullable5 = nullable1.HasValue ? new Decimal?(num3 * nullable1.GetValueOrDefault()) : new Decimal?();
          usageData5.ExtPrice = nullable5;
          usageData4.Prefix = str;
          UsageData usageData6 = usageData4;
          Decimal num4 = (Decimal) 1;
          nullable1 = prorate;
          Decimal? nullable6 = nullable1.HasValue ? new Decimal?(num4 - nullable1.GetValueOrDefault()) : new Decimal?();
          usageData6.Proportion = nullable6;
          usageData4.DiscountID = det.RecurringDiscountID;
          usageData4.DiscountSeq = det.RecurringDiscountSeq;
          usagedata.Add(usageData4);
        }
      }
      this.UpdateAvailableQty(det, contractItem);
      this.AddDepositUsage(det, contractItem, usagedata);
    }
  }

  protected virtual void GetTerminateRecurring(
    IEnumerable<PXResult<ContractDetail, ContractItem>> details,
    List<UsageData> usagedata,
    Decimal? prorate)
  {
    foreach (PXResult<ContractDetail, ContractItem> detail in details)
    {
      ContractDetail det = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      int? nullable1 = contractItem.RecurringItemID;
      if (nullable1.HasValue)
      {
        nullable1 = contractItem.DepositItemID;
        if (!nullable1.HasValue)
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.RecurringItemID);
          string str = PXMessages.LocalizeNoPrefix(contractItem.RecurringType == "P" ? "Prepaid" : "Included");
          Decimal? nullable2 = det.Qty;
          Decimal num1 = nullable2 ?? 0M;
          nullable2 = det.Used;
          Decimal num2 = nullable2 ?? 0M;
          Decimal num3 = Math.Max(num1 - num2, 0.0M);
          Decimal? nullable3;
          if (num3 > 0M)
          {
            UsageData usageData1 = new UsageData();
            usageData1.InventoryID = contractItem.RecurringItemID;
            usageData1.ContractDetailsLineNbr = det.LineNbr;
            usageData1.ContractDetailID = det.ContractDetailID;
            usageData1.ContractItemID = contractItem.ContractItemID;
            usageData1.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
            usageData1.UOM = inventoryItem.BaseUnit;
            usageData1.Qty = new Decimal?(contractItem.RecurringType == "P" ? -num3 : num3);
            usageData1.PriceOverride = det.FixedRecurringPriceVal;
            UsageData usageData2 = usageData1;
            nullable2 = usageData1.Qty;
            nullable3 = det.FixedRecurringPriceVal;
            Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
            usageData2.ExtPrice = nullable4;
            usageData1.Prefix = str;
            UsageData usageData3 = usageData1;
            Decimal? nullable5;
            if (!(contractItem.RecurringType == "P"))
            {
              Decimal num4 = (Decimal) 1;
              nullable3 = prorate;
              if (!nullable3.HasValue)
              {
                nullable2 = new Decimal?();
                nullable5 = nullable2;
              }
              else
                nullable5 = new Decimal?(num4 - nullable3.GetValueOrDefault());
            }
            else
              nullable5 = prorate;
            usageData3.Proportion = nullable5;
            usageData1.DiscountID = det.RecurringDiscountID;
            usageData1.DiscountSeq = det.RecurringDiscountSeq;
            usagedata.Add(usageData1);
          }
          nullable3 = det.Qty;
          Decimal num5 = num3;
          Decimal? nullable6;
          if (!nullable3.HasValue)
          {
            nullable2 = new Decimal?();
            nullable6 = nullable2;
          }
          else
            nullable6 = new Decimal?(nullable3.GetValueOrDefault() - num5);
          Decimal? nullable7 = nullable6;
          nullable3 = nullable7;
          Decimal num6 = 0M;
          if (nullable3.GetValueOrDefault() > num6 & nullable3.HasValue && contractItem.RecurringType != "P")
          {
            UsageData usageData4 = new UsageData();
            usageData4.InventoryID = contractItem.RecurringItemID;
            usageData4.ContractDetailsLineNbr = det.LineNbr;
            usageData4.ContractDetailID = det.ContractDetailID;
            usageData4.ContractItemID = contractItem.ContractItemID;
            usageData4.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
            usageData4.UOM = inventoryItem.BaseUnit;
            usageData4.Qty = nullable7;
            usageData4.PriceOverride = det.FixedRecurringPriceVal;
            UsageData usageData5 = usageData4;
            nullable3 = usageData4.Qty;
            nullable2 = det.FixedRecurringPriceVal;
            Decimal? nullable8 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
            usageData5.ExtPrice = nullable8;
            usageData4.Prefix = str;
            usageData4.Proportion = new Decimal?(1.0M);
            usageData4.DiscountID = det.RecurringDiscountID;
            usageData4.DiscountSeq = det.RecurringDiscountSeq;
            usagedata.Add(usageData4);
          }
        }
      }
      this.UpdateAvailableQty(det, contractItem);
      this.AddDepositUsage(det, contractItem, usagedata);
    }
  }

  protected virtual void UpdateAvailableQty(ContractDetail det, ContractItem item)
  {
    Decimal valueOrDefault1 = det.Change.GetValueOrDefault();
    if (!item.RecurringItemID.HasValue || item.DepositItemID.HasValue || !(item.RecurringType == "P") && !(valueOrDefault1 <= 0M))
      return;
    Decimal? nullable1;
    if (item.ResetUsageOnBilling.GetValueOrDefault())
    {
      nullable1 = det.LastQty ?? det.Qty;
    }
    else
    {
      Decimal valueOrDefault2 = det.UsedTotal.GetValueOrDefault();
      Decimal? nullable2 = det.Used;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal num1 = valueOrDefault2 - valueOrDefault3;
      Decimal? nullable3 = det.LastQty;
      nullable2 = nullable3 ?? det.Qty;
      Decimal num2 = num1;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() - num2);
      nullable1 = nullable4;
    }
    this.availableQty.Add(item.RecurringItemID, new Decimal?(Math.Max(0M, nullable1.Value)));
  }

  protected virtual void AddDepositUsage(
    ContractDetail det,
    ContractItem item,
    List<UsageData> usagedata)
  {
    if (!item.BaseItemID.HasValue || !item.Deposit.GetValueOrDefault())
      return;
    ContractDetail contractDetail = det;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, item.BaseItemID);
    Dictionary<int?, Decimal?> availableDeposit = this.availableDeposit;
    int? contractItemId = contractDetail.ContractItemID;
    Decimal? depositAmt = contractDetail.DepositAmt;
    Decimal? depositUsedTotal = contractDetail.DepositUsedTotal;
    Decimal? nullable = depositAmt.HasValue & depositUsedTotal.HasValue ? new Decimal?(depositAmt.GetValueOrDefault() - depositUsedTotal.GetValueOrDefault()) : new Decimal?();
    availableDeposit[contractItemId] = nullable;
    UsageData usageData = new UsageData();
    usageData.InventoryID = inventoryItem.InventoryID;
    usageData.ContractDetailsLineNbr = det.LineNbr;
    usageData.ContractDetailID = det.ContractDetailID;
    usageData.ContractItemID = item.ContractItemID;
    usageData.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
    usageData.Qty = new Decimal?(0M);
    usageData.UOM = inventoryItem.BaseUnit;
    usageData.IsTranData = new bool?(false);
    usageData.PriceOverride = new Decimal?(0M);
    usageData.ExtPrice = new Decimal?(0M);
    usageData.IsFree = new bool?(false);
    usageData.Prefix = PXMessages.LocalizeNoPrefix("Prepaid Usage");
    usageData.IsDollarUsage = new bool?(true);
    usagedata.Add(usageData);
    this.depositUsage[contractDetail.ContractItemID] = usageData;
  }

  protected virtual void GetUpgradeUsage(
    Contract contract,
    IEnumerable<PXResult<ContractDetail, ContractItem>> details,
    List<UsageData> list,
    out Dictionary<int, List<TranNotePair>> sourceTran,
    out List<UsageData> tranData)
  {
    sourceTran = this.GetTransactions(contract);
    tranData = new List<UsageData>();
    foreach (KeyValuePair<int, List<TranNotePair>> keyValuePair in sourceTran)
    {
      KeyValuePair<int, List<TranNotePair>> kv = keyValuePair;
      PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res1 = details.Select(res => new
      {
        res = res,
        item = res
      }).Where(t =>
      {
        int? recurringItemId = PXResult<ContractDetail, ContractItem>.op_Implicit(t.item).RecurringItemID;
        int key = kv.Key;
        return recurringItemId.GetValueOrDefault() == key & recurringItemId.HasValue;
      }).Select(t => new
      {
        t = t,
        inventory = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, PXResult<ContractDetail, ContractItem>.op_Implicit(t.item).RecurringItemID)
      }).Select(t => new PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>(PXResult<ContractDetail, ContractItem>.op_Implicit(t.t.res), PXResult<ContractDetail, ContractItem>.op_Implicit(t.t.res), t.inventory)).FirstOrDefault<PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>>();
      if (res1 != null)
      {
        tranData.AddRange((IEnumerable<UsageData>) this.ProcessTransactions(contract, res1, kv.Value));
        foreach (TranNotePair tranNotePair in kv.Value)
        {
          tranNotePair.Tran.Billed = new bool?(true);
          tranNotePair.Tran.BilledDate = ((PXGraph) this).Accessinfo.BusinessDate;
          ((PXSelectBase<PMTran>) this.Transactions).Update(tranNotePair.Tran);
        }
      }
    }
    list.AddRange((IEnumerable<UsageData>) tranData);
  }

  protected virtual List<UsageData> GetUpgradeFee(
    Contract contract,
    DateTime? lastBillingDate,
    DateTime? nextBillingDate,
    DateTime? activationDate)
  {
    List<UsageData> upgradeFee = new List<UsageData>();
    Decimal num1 = 1M;
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contract.ContractID
    }));
    if (contract.ExpireDate.HasValue)
    {
      DateTime? startDate = contract.ScheduleStartsOn == "S" ? contract.StartDate : contract.ActivationDate;
      DateTime? endDate;
      ref DateTime? local = ref endDate;
      DateTime date = contract.ExpireDate.Value;
      date = date.Date;
      DateTime dateTime = date.AddDays(1.0);
      local = new DateTime?(dateTime);
      num1 = CTBillEngine.Prorate(activationDate, startDate, endDate);
    }
    Dictionary<int?, PXResult<ContractDetail, ContractItem>> dictionary1 = new Dictionary<int?, PXResult<ContractDetail, ContractItem>>();
    Dictionary<int?, PXResult<ContractDetail, ContractItem>> dictionary2 = new Dictionary<int?, PXResult<ContractDetail, ContractItem>>();
    foreach (PXResult<ContractDetailExt, ContractItem> pxResult in PXSelectBase<ContractDetailExt, PXSelectReadonly2<ContractDetailExt, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<Contract, On<Contract.contractID, Equal<ContractDetailExt.contractID>, And<Contract.lastActiveRevID, Equal<ContractDetailExt.revID>>>>>, Where<ContractDetailExt.contractID, Equal<Required<ContractDetailExt.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetailExt contractDetailExt1 = PXResult<ContractDetailExt, ContractItem>.op_Implicit(pxResult);
      ContractDetail copy = PXCache<ContractDetail>.CreateCopy((ContractDetail) PXResult<ContractDetailExt, ContractItem>.op_Implicit(pxResult));
      ContractItem contractItem = PXResult<ContractDetailExt, ContractItem>.op_Implicit(pxResult);
      ContractDetailExt contractDetailExt2 = contractDetailExt1;
      Decimal? qty = contractDetailExt1.Qty;
      Decimal? nullable = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
      contractDetailExt2.Change = nullable;
      dictionary1[contractDetailExt1.LineNbr] = new PXResult<ContractDetail, ContractItem>((ContractDetail) contractDetailExt1, contractItem);
      dictionary2[copy.LineNbr] = new PXResult<ContractDetail, ContractItem>(copy, contractItem);
    }
    foreach (PXResult<ContractDetail, ContractItem> pxResult in PXSelectBase<ContractDetail, PXSelectReadonly2<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      ContractDetail copy = PXCache<ContractDetail>.CreateCopy(PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult));
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      PXDBLocalizableStringAttribute.CopyTranslations<ContractDetail.description, ContractDetail.description>(((PXSelectBase) this.ContractDetails).Cache, (object) contractDetail1, ((PXSelectBase) this.ContractDetails).Cache, (object) copy);
      if (dictionary1.ContainsKey(contractDetail1.LineNbr))
        dictionary2.Remove(contractDetail1.LineNbr);
      ContractDetail contractDetail2 = (ContractDetail) ((PXGraph) this).Caches[typeof (ContractDetail)].Locate((object) contractDetail1);
      Decimal? nullable1;
      if (contractDetail2 != null && contractItem.Deposit.GetValueOrDefault())
      {
        ContractDetail contractDetail3 = contractDetail2;
        Decimal? qty = contractDetail2.Qty;
        nullable1 = contractDetail2.BasePriceVal;
        Decimal? nullable2 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
        contractDetail3.DepositAmt = nullable2;
        ((PXGraph) this).Caches[typeof (ContractDetail)].Update((object) contractDetail2);
      }
      dictionary1[contractDetail1.LineNbr] = pxResult;
      nullable1 = contractDetail1.Change;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
      {
        copy.Qty = copy.LastQty;
        dictionary2[copy.LineNbr] = new PXResult<ContractDetail, ContractItem>(copy, contractItem);
      }
    }
    this.GetUpgradeSetup((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary1.Values, upgradeFee, new Decimal?(num1));
    Decimal num3 = 1M;
    if (nextBillingDate.HasValue && lastBillingDate.HasValue)
    {
      DateTime? nullable = lastBillingDate;
      if (contractBillingSchedule?.Type == "S")
        nullable = this.GetStatementBillDates(contract.CustomerID, nullable).Start;
      num3 = CTBillEngine.Prorate(activationDate, nullable, nextBillingDate);
    }
    this.GetUpgradeRecurring((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary1.Values, upgradeFee, new Decimal?(num3));
    this.GetUpgradeUsage(contract, (IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary2.Values, upgradeFee, out Dictionary<int, List<TranNotePair>> _, out List<UsageData> _);
    return upgradeFee;
  }

  protected virtual List<UsageData> GetTerminationFee(
    Contract contract,
    DateTime? lastBillingDate,
    DateTime? nextBillingDate,
    DateTime terminationDate,
    out Dictionary<int, List<TranNotePair>> sourceTran,
    out List<UsageData> tranData)
  {
    List<UsageData> terminationFee = new List<UsageData>();
    Dictionary<int?, PXResult<ContractDetail, ContractItem>> dictionary1 = new Dictionary<int?, PXResult<ContractDetail, ContractItem>>();
    Dictionary<int?, PXResult<ContractDetail, ContractItem>> dictionary2 = new Dictionary<int?, PXResult<ContractDetail, ContractItem>>();
    foreach (PXResult<ContractDetail, ContractItem> pxResult in PXSelectBase<ContractDetail, PXSelectReadonly2<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      ContractDetail copy = PXCache<ContractDetail>.CreateCopy(PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult));
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      ContractDetail contractDetail2 = contractDetail1;
      Decimal? qty = contractDetail1.Qty;
      Decimal? nullable1;
      Decimal? nullable2;
      if (!qty.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(-qty.GetValueOrDefault());
      contractDetail2.Change = nullable2;
      PXDBLocalizableStringAttribute.CopyTranslations<ContractDetail.description, ContractDetail.description>(((PXSelectBase) this.ContractDetails).Cache, (object) contractDetail1, ((PXSelectBase) this.ContractDetails).Cache, (object) copy);
      dictionary1[contractDetail1.LineNbr] = new PXResult<ContractDetail, ContractItem>(contractDetail1, contractItem);
      dictionary2[copy.LineNbr] = new PXResult<ContractDetail, ContractItem>(copy, contractItem);
      ContractDetail contractDetail3 = (ContractDetail) ((PXGraph) this).Caches[typeof (ContractDetail)].Locate((object) contractDetail1);
      if (contractDetail3 != null && contractItem.Deposit.GetValueOrDefault())
      {
        ContractDetail contractDetail4 = contractDetail3;
        qty = contractDetail3.Qty;
        nullable1 = contractDetail3.BasePriceVal;
        Decimal? nullable3 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
        contractDetail4.DepositAmt = nullable3;
        ((PXGraph) this).Caches[typeof (ContractDetail)].Update((object) contractDetail3);
      }
      dictionary1[contractDetail1.LineNbr] = pxResult;
      nullable1 = contractDetail1.Change;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      {
        copy.Qty = copy.LastQty;
        dictionary2[copy.LineNbr] = new PXResult<ContractDetail, ContractItem>(copy, contractItem);
      }
    }
    this.MemorizeDeposits((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary1.Values);
    Decimal num1 = 1M;
    DateTime? nullable4 = contract.ExpireDate;
    TimeSpan timeSpan;
    if (nullable4.HasValue)
    {
      DateTime? nullable5 = contract.ScheduleStartsOn == "S" ? contract.StartDate : contract.ActivationDate;
      nullable4 = contract.ExpireDate;
      DateTime date = nullable4.Value;
      date = date.Date;
      timeSpan = date.Subtract(nullable5.Value.Date);
      Decimal days1 = (Decimal) timeSpan.Days;
      nullable4 = contract.ExpireDate;
      timeSpan = nullable4.Value.Date.Subtract(terminationDate.Date);
      Decimal days2 = (Decimal) timeSpan.Days;
      num1 = days1 == 0M ? 1M : days2 / days1;
    }
    Contract contract1 = ((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract.TemplateID
    });
    int num2;
    if (contract1.Refundable.GetValueOrDefault())
    {
      nullable4 = contract.StartDate;
      DateTime date = nullable4.Value;
      date = date.Date;
      num2 = date.AddDays((double) contract1.RefundPeriod.GetValueOrDefault()) >= terminationDate ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0;
    this.GetUpgradeSetup((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary1.Values, flag ? terminationFee : (List<UsageData>) null, new Decimal?(num1));
    Decimal? prorate = new Decimal?((Decimal) 1);
    if (nextBillingDate.HasValue && lastBillingDate.HasValue)
    {
      DateTime date = nextBillingDate.Value;
      date = date.Date;
      timeSpan = date.Subtract(lastBillingDate.Value.Date);
      Decimal days3 = (Decimal) timeSpan.Days;
      date = nextBillingDate.Value;
      date = date.Date;
      timeSpan = date.Subtract(terminationDate.Date);
      Decimal days4 = (Decimal) timeSpan.Days;
      prorate = days3 == 0M ? new Decimal?() : new Decimal?(days4 / days3);
    }
    if (prorate.HasValue)
      this.GetTerminateRecurring((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary1.Values, terminationFee, prorate);
    this.GetUpgradeUsage(contract, (IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary2.Values, terminationFee, out sourceTran, out tranData);
    foreach (UsageData usageData in this.depositUsage.Values)
      terminationFee.Remove(usageData);
    foreach (UsageData usageData in this.nonRefundableDepositedUsage)
      terminationFee.Remove(usageData);
    return terminationFee;
  }

  protected virtual void MemorizeDeposits(
    IEnumerable<PXResult<ContractDetail, ContractItem>> details)
  {
    foreach (PXResult<ContractDetail, ContractItem> detail in details)
    {
      ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
      if (contractItem.BaseItemID.HasValue)
      {
        bool? nullable = contractItem.Deposit;
        if (nullable.GetValueOrDefault())
        {
          nullable = contractItem.Refundable;
          if (nullable.GetValueOrDefault())
            this.refundableDeposits.Add(contractItem.ContractItemID, contractItem);
          else
            this.nonRefundableDeposits.Add(contractItem.ContractItemID, contractItem);
        }
      }
    }
  }

  protected virtual List<UsageData> GetRenewalUsage(Contract contract)
  {
    List<UsageData> renewalUsage = new List<UsageData>();
    PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.renewalItemID>>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.isRenewalValid, Equal<True>>>> pxSelectJoin = new PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.renewalItemID>>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.isRenewalValid, Equal<True>>>>((PXGraph) this);
    PX.Objects.AR.Customer byId = this.customerRepository.FindByID(contract.CustomerID);
    object[] objArray = new object[1]
    {
      (object) contract.ContractID
    };
    foreach (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<ContractDetail>) pxSelectJoin).Select(objArray))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      UsageData usageData1 = new UsageData();
      usageData1.InventoryID = inventoryItem.InventoryID;
      usageData1.ContractDetailsLineNbr = contractDetail.LineNbr;
      usageData1.ContractDetailID = contractDetail.ContractDetailID;
      usageData1.ContractItemID = contractItem.ContractItemID;
      usageData1.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, byId?.LocaleName);
      usageData1.UOM = inventoryItem.BaseUnit;
      usageData1.Qty = contractDetail.Qty;
      usageData1.PriceOverride = contractDetail.RenewalPriceVal;
      UsageData usageData2 = usageData1;
      Decimal? qty = contractDetail.Qty;
      Decimal? renewalPriceVal = contractDetail.RenewalPriceVal;
      Decimal? nullable = qty.HasValue & renewalPriceVal.HasValue ? new Decimal?(qty.GetValueOrDefault() * renewalPriceVal.GetValueOrDefault()) : new Decimal?();
      usageData2.ExtPrice = nullable;
      usageData1.DiscountID = contractDetail.RenewalDiscountID;
      usageData1.DiscountSeq = contractDetail.RenewalDiscountSeq;
      renewalUsage.Add(usageData1);
    }
    return renewalUsage;
  }

  protected static Decimal Prorate(DateTime? date, DateTime? startDate, DateTime? endDate)
  {
    DateTime date1 = date.Value.Date;
    DateTime date2 = startDate.Value.Date;
    DateTime date3 = endDate.Value.Date;
    Decimal days1 = (Decimal) date3.Subtract(date2).Days;
    Decimal days2 = (Decimal) date3.Subtract(date1).Days;
    return !(days1 == 0.0M) ? days2 / days1 : 1.0M;
  }

  protected static Decimal Prorate(DateTime? date, CTBillEngine.DatePair period)
  {
    return CTBillEngine.Prorate(date, period.Start, period.End);
  }

  protected virtual List<UsageData> GetPrepayment(
    Contract contract,
    DateTime? activationDate,
    DateTime? lastBillingDate,
    DateTime? nextBillingDate)
  {
    List<UsageData> prepayment = new List<UsageData>();
    Decimal num1 = 1M;
    ContractBillingSchedule contractBillingSchedule = ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).SelectSingle(new object[1]
    {
      (object) contract.ContractID
    });
    if (contractBillingSchedule != null && contractBillingSchedule.Type == "S")
    {
      CTBillEngine.DatePair statementBillDates = this.GetStatementBillDates(contract.CustomerID, activationDate);
      num1 = CTBillEngine.Prorate(activationDate, statementBillDates);
    }
    else if (nextBillingDate.HasValue && contract.ScheduleStartsOn == "S")
      num1 = CTBillEngine.Prorate(activationDate, lastBillingDate, nextBillingDate);
    if (contract.IsPendingUpdate.GetValueOrDefault())
      return prepayment;
    foreach (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<ContractDetail, PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.recurringItemID>>>>, Where<ContractDetail.contractID, Equal<Required<ContractDetail.contractID>>, And<ContractItem.recurringType, Equal<RecurringOption.prepay>, And<ContractItem.depositItemID, IsNull>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      Decimal? nullable1 = contractDetail.Qty;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      string str = PXMessages.LocalizeNoPrefix("Prepaid");
      UsageData usageData1 = new UsageData();
      usageData1.InventoryID = contractItem.RecurringItemID;
      usageData1.ContractDetailsLineNbr = contractDetail.LineNbr;
      usageData1.ContractDetailID = contractDetail.ContractDetailID;
      usageData1.ContractItemID = contractItem.ContractItemID;
      usageData1.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
      usageData1.UOM = inventoryItem.BaseUnit;
      usageData1.Qty = new Decimal?(valueOrDefault);
      usageData1.PriceOverride = contractDetail.FixedRecurringPriceVal;
      UsageData usageData2 = usageData1;
      Decimal num2 = valueOrDefault;
      nullable1 = contractDetail.FixedRecurringPriceVal;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      usageData2.ExtPrice = nullable2;
      usageData1.Prefix = str;
      usageData1.Proportion = new Decimal?(num1);
      usageData1.DiscountID = contractDetail.RecurringDiscountID;
      usageData1.DiscountSeq = contractDetail.RecurringDiscountSeq;
      prepayment.Add(usageData1);
    }
    return prepayment;
  }

  protected virtual List<UsageData> GetRecurringBilling(
    IEnumerable<PXResult<ContractDetail, ContractItem>> details,
    Contract contract)
  {
    ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contract.ContractID
    }));
    List<UsageData> recurringBilling = new List<UsageData>();
    CTBillEngine.Proportions proportions = new CTBillEngine.Proportions();
    int? nullable1;
    if (schedule != null && schedule.Type == "S")
    {
      nullable1 = contract.CustomerID;
      if (nullable1.HasValue)
      {
        proportions = this.CalculateStatementBasedRecurringProportions(contract, schedule);
        goto label_5;
      }
    }
    if (schedule != null && schedule.Type != "S" && schedule.Type != "D")
      proportions = this.CalculateRecurringProportions(contract, schedule);
label_5:
    PX.Objects.AR.Customer byId = this.customerRepository.FindByID(contract.CustomerID);
    using (new PXLocaleScope(byId?.LocaleName))
    {
      foreach (PXResult<ContractDetail, ContractItem> detail in details)
      {
        ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
        ContractItem contractItem = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
        nullable1 = contractItem.RecurringItemID;
        Decimal? nullable2;
        Decimal? nullable3;
        bool? nullable4;
        if (nullable1.HasValue)
        {
          nullable1 = contractItem.DepositItemID;
          if (!nullable1.HasValue)
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.RecurringItemID);
            Decimal num1 = 0M;
            UsageData usageData1 = new UsageData();
            usageData1.InventoryID = inventoryItem.InventoryID;
            usageData1.ContractDetailsLineNbr = contractDetail1.LineNbr;
            usageData1.ContractDetailID = contractDetail1.ContractDetailID;
            usageData1.ContractItemID = contractItem.ContractItemID;
            usageData1.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, byId?.LocaleName);
            usageData1.UOM = inventoryItem.BaseUnit;
            usageData1.Qty = contractDetail1.Qty;
            usageData1.PriceOverride = contractDetail1.FixedRecurringPriceVal;
            usageData1.Proportion = new Decimal?(contractItem.RecurringType == "U" ? 1.0M - proportions.Postpaid : proportions.Prepaid);
            nullable2 = contractDetail1.Qty;
            nullable3 = contractDetail1.FixedRecurringPriceVal;
            usageData1.ExtPrice = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
            usageData1.Prefix = PXMessages.LocalizeNoPrefix(contractItem.RecurringType == "U" ? "Included" : "Prepaid");
            usageData1.DiscountID = contractDetail1.RecurringDiscountID;
            usageData1.DiscountSeq = contractDetail1.RecurringDiscountSeq;
            UsageData usageData2 = usageData1;
            if (contractItem.RecurringType == "U")
            {
              nullable3 = contractDetail1.Qty;
              Decimal num2 = 0M;
              if (nullable3.GetValueOrDefault() > num2 & nullable3.HasValue)
              {
                if (!this.IsFirstBillAfterRenewalExpiredContractInGracePeriod(contract, schedule))
                  recurringBilling.Add(usageData2);
                nullable3 = contractDetail1.Qty;
                num1 = nullable3.GetValueOrDefault();
              }
            }
            if (contractItem.RecurringType == "P")
            {
              nullable3 = contractDetail1.Qty;
              Decimal num3 = 0M;
              if (nullable3.GetValueOrDefault() > num3 & nullable3.HasValue)
              {
                nullable4 = contract.IsCancelled;
                if (!nullable4.GetValueOrDefault() && !this.IsLastBillBeforeExpiration(contract, schedule))
                  recurringBilling.Add(usageData2);
                nullable3 = contractDetail1.Qty;
                num1 = nullable3.GetValueOrDefault();
              }
            }
            nullable4 = contractItem.ResetUsageOnBilling;
            Decimal val2;
            if (nullable4.GetValueOrDefault())
            {
              val2 = num1;
            }
            else
            {
              nullable3 = contractDetail1.UsedTotal;
              Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
              nullable3 = contractDetail1.Used;
              Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
              Decimal num4 = valueOrDefault1 - valueOrDefault2;
              val2 = num1 - num4;
            }
            this.availableQty.Add(contractItem.RecurringItemID, new Decimal?(Math.Max(0M, val2)));
          }
        }
        nullable1 = contractItem.BaseItemID;
        if (nullable1.HasValue)
        {
          nullable4 = contractItem.Deposit;
          if (nullable4.GetValueOrDefault())
          {
            ContractDetail contractDetail2 = PXResult<ContractDetail, ContractItem>.op_Implicit(detail);
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, contractItem.BaseItemID);
            Dictionary<int?, Decimal?> availableDeposit = this.availableDeposit;
            int? contractItemId = contractDetail2.ContractItemID;
            nullable3 = contractDetail2.DepositAmt;
            nullable2 = contractDetail2.DepositUsedTotal;
            Decimal? nullable5 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            availableDeposit[contractItemId] = nullable5;
            UsageData usageData = new UsageData()
            {
              InventoryID = inventoryItem.InventoryID,
              ContractDetailsLineNbr = contractDetail1.LineNbr,
              ContractDetailID = contractDetail1.ContractDetailID,
              ContractItemID = contractItem.ContractItemID,
              Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem),
              Qty = new Decimal?(0M),
              UOM = inventoryItem.BaseUnit,
              IsTranData = new bool?(false),
              PriceOverride = new Decimal?(0M),
              ExtPrice = new Decimal?(0M),
              IsFree = new bool?(false),
              Prefix = PXMessages.LocalizeNoPrefix("Prepaid Usage"),
              IsDollarUsage = new bool?(true)
            };
            recurringBilling.Add(usageData);
            this.depositUsage[contractDetail2.ContractItemID] = usageData;
          }
        }
      }
    }
    return recurringBilling;
  }

  protected virtual CTBillEngine.Proportions CalculateStatementBasedRecurringProportions(
    Contract contract,
    ContractBillingSchedule schedule)
  {
    CTBillEngine.Proportions recurringProportions = new CTBillEngine.Proportions();
    int num = this.IsFirstRegularBill(contract, schedule) ? 1 : 0;
    bool beforeExpiration = this.IsPrevToLastBillBeforeExpiration(contract, schedule);
    bool flag = this.IsLastBillBeforeExpiration(contract, schedule);
    if (num != 0 && contract.ActivationDate.HasValue)
    {
      CTBillEngine.DatePair statementBillDates = this.GetStatementBillDates(contract.CustomerID, contract.ActivationDate);
      recurringProportions.Postpaid = 1.0M - CTBillEngine.Prorate(contract.ActivationDate, statementBillDates);
    }
    if (beforeExpiration)
    {
      CTBillEngine.DatePair statementBillDates = this.GetStatementBillDates(contract.CustomerID, schedule.NextDate);
      recurringProportions.Prepaid = 1.0M - CTBillEngine.Prorate(new DateTime?(contract.ExpireDate.Value.AddDays(1.0)), statementBillDates);
    }
    if (flag)
    {
      CTBillEngine.DatePair statementBillDates = this.GetStatementBillDates(contract.CustomerID, schedule.NextDate);
      recurringProportions.Postpaid = CTBillEngine.Prorate(new DateTime?(contract.ExpireDate.Value.AddDays(1.0)), statementBillDates);
    }
    return recurringProportions;
  }

  protected virtual CTBillEngine.Proportions CalculateRecurringProportions(
    Contract contract,
    ContractBillingSchedule schedule)
  {
    CTBillEngine.Proportions recurringProportions = new CTBillEngine.Proportions();
    int num = this.IsPrevToLastBillBeforeExpiration(contract, schedule) ? 1 : 0;
    bool flag = this.IsLastBillBeforeExpiration(contract, schedule);
    if (num != 0)
    {
      DateTime? nextBillingDate = this.GetNextBillingDate(schedule.Type, contract.CustomerID, schedule.NextDate, schedule.StartBilling);
      recurringProportions.Prepaid = 1M - CTBillEngine.Prorate(new DateTime?(contract.ExpireDate.Value.AddDays(1.0)), schedule.NextDate, nextBillingDate);
    }
    if (flag)
    {
      DateTime? nextBillingDate = this.GetNextBillingDate(schedule.Type, contract.CustomerID, schedule.LastDate, schedule.StartBilling);
      recurringProportions.Postpaid = CTBillEngine.Prorate(new DateTime?(contract.ExpireDate.Value.AddDays(1.0)), schedule.LastDate, nextBillingDate);
    }
    return recurringProportions;
  }

  protected virtual DateTime DateSetDay(DateTime date, int day)
  {
    int day1 = DateTime.DaysInMonth(date.Year, date.Month);
    if (date.Day >= day || day1 <= date.Day)
      return date;
    return day1 < day ? new DateTime(date.Year, date.Month, day1) : new DateTime(date.Year, date.Month, day);
  }

  protected virtual DateTime? GetNextBillingDate(
    string scheduleType,
    int? customerID,
    DateTime? date,
    DateTime? startDate)
  {
    if (!date.HasValue)
      return new DateTime?();
    if (scheduleType != null && scheduleType.Length == 1)
    {
      switch (scheduleType[0])
      {
        case '6':
          DateTime dateTime1 = date.Value;
          DateTime date1 = dateTime1.AddMonths(6);
          dateTime1 = startDate.Value;
          int day1 = dateTime1.Day;
          return new DateTime?(this.DateSetDay(date1, day1));
        case 'A':
          DateTime dateTime2 = date.Value;
          DateTime date2 = dateTime2.AddYears(1);
          dateTime2 = startDate.Value;
          int day2 = dateTime2.Day;
          return new DateTime?(this.DateSetDay(date2, day2));
        case 'D':
          return new DateTime?();
        case 'M':
          DateTime dateTime3 = date.Value;
          DateTime date3 = dateTime3.AddMonths(1);
          dateTime3 = startDate.Value;
          int day3 = dateTime3.Day;
          return new DateTime?(this.DateSetDay(date3, day3));
        case 'Q':
          DateTime dateTime4 = date.Value;
          DateTime date4 = dateTime4.AddMonths(3);
          dateTime4 = startDate.Value;
          int day4 = dateTime4.Day;
          return new DateTime?(this.DateSetDay(date4, day4));
        case 'S':
          return this.GetStatementBillDates(customerID, date).End;
        case 'W':
          return new DateTime?(date.Value.AddDays(7.0));
      }
    }
    throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("The schedule type is invalid: {0}.", new object[1]
    {
      (object) (scheduleType ?? "null")
    }), nameof (scheduleType));
  }

  protected virtual DateTime? GetNextBillingDateConsiderExpiration(
    string scheduleType,
    int? customerID,
    DateTime? date,
    DateTime? startDate,
    DateTime? expireDate)
  {
    DateTime? considerExpiration = this.GetNextBillingDate(scheduleType, customerID, date, startDate);
    if (expireDate.HasValue)
    {
      DateTime? nullable1 = expireDate;
      DateTime? nullable2 = considerExpiration;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        considerExpiration = expireDate;
    }
    return considerExpiration;
  }

  protected virtual CTBillEngine.DatePair GetStatementBillDates(
    int? customerID,
    DateTime? dateInside)
  {
    if (!dateInside.HasValue)
      return new CTBillEngine.DatePair(new DateTime?(), new DateTime?());
    DateTime aBusinessDate = dateInside.Value.AddDays(1.0);
    ARStatementCycle aCycle = PXResultset<ARStatementCycle>.op_Implicit(PXSelectBase<ARStatementCycle, PXSelectJoin<ARStatementCycle, LeftJoin<PX.Objects.AR.Customer, On<ARStatementCycle.statementCycleId, Equal<PX.Objects.AR.Customer.statementCycleId>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) customerID
    }));
    if (aCycle == null)
      throw new PXSetPropertyException("Selected Customer do not have valid Statement Cycle assigned and cannot be configred for the Statement-Based billing. Please configure the Customer Statement Cycle or select different contract template.", (PXErrorLevel) 4);
    DateTime aBeforeDate = aBusinessDate;
    string prepareOn = aCycle.PrepareOn;
    short? nullable = aCycle.Day00;
    int? aDay00 = nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?();
    nullable = aCycle.Day01;
    int? aDay01 = nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?();
    int? dayOfWeek = aCycle.DayOfWeek;
    return new CTBillEngine.DatePair(new DateTime?(ARStatementProcess.CalcStatementDateBefore((PXGraph) this, aBeforeDate, prepareOn, aDay00, aDay01, dayOfWeek)), new DateTime?(ARStatementProcess.FindNextStatementDateAfter((PXGraph) this, aBusinessDate, aCycle)));
  }

  protected virtual void SetBillingTarget(
    Contract contract,
    out PX.Objects.AR.Customer customer,
    out PX.Objects.CR.Location location)
  {
    customer = (PX.Objects.AR.Customer) null;
    location = (PX.Objects.CR.Location) null;
    if (!contract.CustomerID.HasValue)
      return;
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contract.ContractID
    }));
    int? locationId;
    if (contractBillingSchedule != null && contractBillingSchedule.AccountID.HasValue)
    {
      customer = this.customerRepository.FindByID(contractBillingSchedule.AccountID);
      locationId = contractBillingSchedule.LocationID;
    }
    else
    {
      customer = this.customerRepository.FindByID(contract.CustomerID);
      locationId = contract.LocationID;
    }
    ref PX.Objects.CR.Location local = ref location;
    PXResultset<PX.Objects.CR.Location> pxResultset;
    if (!locationId.HasValue)
      pxResultset = PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.AR.Customer.defLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) customer.BAccountID,
        (object) customer.DefLocationID
      });
    else
      pxResultset = PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<ContractBillingSchedule.accountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<ContractBillingSchedule.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) customer.BAccountID,
        (object) locationId
      });
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(pxResultset);
    local = location1;
  }

  protected virtual void ValidateCustomerInfo(
    Contract contract,
    PX.Objects.AR.Customer customer,
    PX.Objects.CR.Location location)
  {
  }

  public virtual void RecalcUsage(
    Contract contract,
    out List<UsageData> data,
    out Dictionary<int, List<TranNotePair>> sourceTran,
    out List<UsageData> tranData)
  {
    Dictionary<int?, PXResult<ContractDetail, ContractItem>> dictionary = new Dictionary<int?, PXResult<ContractDetail, ContractItem>>();
    foreach (PXResult<ContractDetailExt, ContractItem> pxResult in PXSelectBase<ContractDetailExt, PXSelectReadonly2<ContractDetailExt, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<Contract, On<Contract.contractID, Equal<ContractDetailExt.contractID>, And<Contract.lastActiveRevID, Equal<ContractDetailExt.revID>>>>>, Where<ContractDetailExt.contractID, Equal<Required<ContractDetailExt.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    }))
    {
      ContractDetailExt contractDetailExt = PXResult<ContractDetailExt, ContractItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetailExt, ContractItem>.op_Implicit(pxResult);
      dictionary[contractDetailExt.LineNbr] = new PXResult<ContractDetail, ContractItem>((ContractDetail) contractDetailExt, contractItem);
    }
    data = this.GetRecurringBilling((IEnumerable<PXResult<ContractDetail, ContractItem>>) dictionary.Values, contract);
    sourceTran = this.GetTransactions(contract);
    using (new PXLocaleScope(this.customerRepository.FindByID(contract.CustomerID)?.LocaleName))
      tranData = this.ProcessTransactions(contract, sourceTran);
    data.AddRange((IEnumerable<UsageData>) tranData);
  }

  public virtual Decimal? RecalcDollarUsage(Contract contract)
  {
    List<UsageData> data;
    this.RecalcUsage(contract, out data, out Dictionary<int, List<TranNotePair>> _, out List<UsageData> _);
    return data.Where<UsageData>((Func<UsageData, bool>) (item => item.IsDollarUsage.GetValueOrDefault())).Aggregate<UsageData, Decimal?>(new Decimal?(0M), (Func<Decimal?, UsageData, Decimal?>) ((current, item) =>
    {
      Decimal? nullable1 = current;
      Decimal? nullable2;
      if (!item.IsTranData.GetValueOrDefault() || item.IsFree.GetValueOrDefault() || !item.PriceOverride.HasValue)
      {
        nullable2 = item.ExtPrice;
      }
      else
      {
        Decimal? qty = item.Qty;
        Decimal? priceOverride = item.PriceOverride;
        nullable2 = qty.HasValue & priceOverride.HasValue ? new Decimal?(qty.GetValueOrDefault() * priceOverride.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable3 = nullable2;
      return !(nullable1.HasValue & nullable3.HasValue) ? new Decimal?() : new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    }));
  }

  protected virtual Dictionary<int, List<TranNotePair>> GetTransactions(Contract contract)
  {
    ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
    {
      (object) contract.ContractID
    }));
    Dictionary<int, List<TranNotePair>> transactions = new Dictionary<int, List<TranNotePair>>();
    PXResultset<PMTran> pxResultset;
    if (!contract.IsCancelled.GetValueOrDefault() && schedule != null)
    {
      DateTime? nextDate = schedule.NextDate;
      if (nextDate.HasValue && !this.IsLastBillBeforeExpiration(contract, schedule))
      {
        nextDate = schedule.NextDate;
        DateTime date = nextDate.Value;
        date = date.Date;
        DateTime dateTime = date.AddDays(1.0);
        pxResultset = PXSelectBase<PMTran, PXSelectJoin<PMTran, LeftJoin<Note, On<PMTran.origRefID, Equal<Note.noteID>>>, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.billed, Equal<False>, And<PMTran.date, Less<Required<PMTran.date>>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) contract.ContractID,
          (object) dateTime
        });
        goto label_4;
      }
    }
    pxResultset = PXSelectBase<PMTran, PXSelectJoin<PMTran, LeftJoin<Note, On<PMTran.origRefID, Equal<Note.noteID>>>, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.billed, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ContractID
    });
label_4:
    foreach (PXResult<PMTran, Note> pxResult in pxResultset)
    {
      PMTran tran = PXResult<PMTran, Note>.op_Implicit(pxResult);
      Note note = PXResult<PMTran, Note>.op_Implicit(pxResult);
      Dictionary<int, List<TranNotePair>> dictionary1 = transactions;
      int? inventoryId = tran.InventoryID;
      int key1 = inventoryId.Value;
      if (dictionary1.ContainsKey(key1))
      {
        Dictionary<int, List<TranNotePair>> dictionary2 = transactions;
        inventoryId = tran.InventoryID;
        int key2 = inventoryId.Value;
        dictionary2[key2].Add(new TranNotePair(tran, note));
      }
      else
      {
        List<TranNotePair> tranNotePairList1 = new List<TranNotePair>()
        {
          new TranNotePair(tran, note)
        };
        Dictionary<int, List<TranNotePair>> dictionary3 = transactions;
        inventoryId = tran.InventoryID;
        int key3 = inventoryId.Value;
        List<TranNotePair> tranNotePairList2 = tranNotePairList1;
        dictionary3.Add(key3, tranNotePairList2);
      }
    }
    return transactions;
  }

  protected virtual List<UsageData> ProcessTransactions(
    Contract contract,
    Dictionary<int, List<TranNotePair>> transactions)
  {
    List<UsageData> usageDataList = new List<UsageData>();
    foreach (KeyValuePair<int, List<TranNotePair>> transaction in transactions)
    {
      ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Select(new object[1]
      {
        (object) contract.ContractID
      }));
      PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res = (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>) null;
      if (contract.CustomerID.HasValue)
        res = (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>) PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.recurringItemID>>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractItem.recurringItemID, Equal<Required<ContractDetail.inventoryID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) contract.ContractID,
          (object) transaction.Key
        }));
      if (res == null)
        res = new PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>((ContractDetail) null, (ContractItem) null, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, new int?(transaction.Key)));
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
      if (!this.IsFirstBillAfterRenewalExpiredContractInGracePeriod(contract, schedule) || contractItem == null || !(contractItem.RecurringType != "P"))
      {
        usageDataList.AddRange((IEnumerable<UsageData>) this.ProcessTransactions(contract, res, transaction.Value));
        foreach (TranNotePair tranNotePair in transaction.Value)
        {
          tranNotePair.Tran.Billed = new bool?(true);
          tranNotePair.Tran.BilledDate = schedule.NextDate;
          ((PXSelectBase<PMTran>) this.Transactions).Update(tranNotePair.Tran);
        }
      }
    }
    return usageDataList;
  }

  protected virtual List<UsageData> ProcessTransactions(
    Contract contract,
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res,
    List<TranNotePair> list)
  {
    ContractDetail contractDetail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    List<UsageData> addedData = new List<UsageData>();
    if (list.Count > 1)
    {
      if (contract.DetailedBilling.GetValueOrDefault() == 1)
      {
        foreach (TranNotePair tran in list)
          this.ProcessSingleRecord(contract, tran, res, addedData);
      }
      else
      {
        List<TranNotePair> trans = new List<TranNotePair>();
        foreach (TranNotePair tran in list)
        {
          if (contractDetail != null || tran.Tran.IsQtyOnly.GetValueOrDefault())
            trans.Add(tran);
          else
            this.ProcessSingleRecord(contract, tran, res, addedData);
        }
        this.ProcessSummaryUsageItems(contract, trans, res, addedData);
      }
    }
    else if (list.Count == 1)
      this.ProcessSingleRecord(contract, list[0], res, addedData);
    return addedData;
  }

  protected virtual bool ProcessDollarUsage(
    Contract contract,
    TranNotePair tran,
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res,
    List<UsageData> addedData)
  {
    return this.ProcessDollarUsage(contract, tran, tran.Tran.UOM, tran.Tran.BillableQty, tran.Tran.ResourceID, res, addedData);
  }

  protected virtual bool ProcessDollarUsage(
    Contract contract,
    TranNotePair tran,
    string UOM,
    Decimal? BillableQty,
    int? EmployeeID,
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res,
    List<UsageData> addedData)
  {
    ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    UsageData usageData1 = addedData[addedData.Count - 1];
    if (contractDetail1 != null && contractItem != null && contractItem.DepositItemID.HasValue)
    {
      ContractDetail copy1 = (ContractDetail) ((PXSelectBase) this.ContractDetails).Cache.CreateCopy((object) (ContractDetail) ((PXSelectBase) this.ContractDetails).Cache.Locate((object) new ContractDetail()
      {
        ContractID = contract.ContractID,
        ContractItemID = contractDetail1.ContractItemID,
        RevID = contractDetail1.RevID
      }));
      if (this.nonRefundableDeposits.ContainsKey(contractItem.DepositItemID))
        this.nonRefundableDepositedUsage.Add(usageData1);
      Decimal? nullable1;
      if (this.availableDeposit.TryGetValue(contractItem.DepositItemID, out nullable1))
      {
        Decimal num1 = this.ConvertUsage(((PXSelectBase) this.Transactions).Cache, tran.Tran.InventoryID, UOM, inventoryItem.BaseUnit, BillableQty);
        Decimal num2 = num1;
        Decimal? nullable2 = copy1.FixedRecurringPriceVal;
        Decimal num3 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, (nullable2.HasValue ? new Decimal?(num2 * nullable2.GetValueOrDefault()) : new Decimal?()).Value);
        nullable2 = nullable1;
        Decimal num4 = 0M;
        if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
        {
          Decimal num5 = num3;
          nullable2 = nullable1;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          if (num5 <= valueOrDefault & nullable2.HasValue)
          {
            usageData1.PriceOverride = copy1.FixedRecurringPriceVal;
            usageData1.IsFree = new bool?(false);
            usageData1.Prefix = PXMessages.LocalizeNoPrefix("Prepaid Usage");
            usageData1.IsDollarUsage = new bool?(true);
            nullable2 = nullable1;
            Decimal num6 = num3;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num6) : new Decimal?();
            goto label_13;
          }
        }
        nullable2 = nullable1;
        Decimal num7 = 0M;
        Decimal? nullable3;
        if (nullable2.GetValueOrDefault() > num7 & nullable2.HasValue && num1 > 0M)
        {
          nullable2 = nullable1;
          nullable3 = copy1.FixedRecurringPriceVal;
          Decimal num8 = (nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?()).Value;
          Decimal num9 = PXDBQuantityAttribute.Round(new Decimal?(num8));
          usageData1.UOM = inventoryItem.BaseUnit;
          usageData1.Qty = new Decimal?(num9);
          usageData1.PreciseQty = new Decimal?(num8);
          usageData1.PriceOverride = copy1.FixedRecurringPriceVal;
          usageData1.IsFree = new bool?(false);
          usageData1.Prefix = PXMessages.LocalizeNoPrefix("Prepaid Usage");
          usageData1.IsDollarUsage = new bool?(true);
          addedData.Add(new UsageData()
          {
            InventoryID = tran.Tran.InventoryID,
            ContractDetailsLineNbr = copy1.LineNbr,
            ContractDetailID = copy1.ContractDetailID,
            ContractItemID = contractItem.ContractItemID,
            Description = tran.Tran.Description,
            Qty = new Decimal?(num1 - num9),
            PreciseQty = new Decimal?(num1 - num8),
            UOM = inventoryItem.BaseUnit,
            EmployeeID = tran.Tran.ResourceID,
            IsTranData = new bool?(true),
            PriceOverride = copy1.UsagePriceVal,
            IsFree = new bool?(false),
            Prefix = PXMessages.LocalizeNoPrefix("Overused"),
            BranchID = tran.Tran.BranchID,
            IsDollarUsage = new bool?(true),
            DiscountID = copy1.RecurringDiscountID,
            DiscountSeq = copy1.RecurringDiscountSeq
          });
          Decimal num10 = num1 - num8;
          nullable2 = copy1.UsagePriceVal;
          Decimal? nullable4;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new Decimal?(num10 * nullable2.GetValueOrDefault());
          nullable3 = nullable4;
          nullable1 = new Decimal?(-PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable3.Value));
        }
        else
        {
          usageData1.PriceOverride = copy1.UsagePriceVal;
          usageData1.IsFree = new bool?(false);
          usageData1.Prefix = PXMessages.LocalizeNoPrefix("Overused");
          usageData1.IsDollarUsage = new bool?(true);
          usageData1.DiscountID = copy1.RecurringDiscountID;
          usageData1.DiscountSeq = copy1.RecurringDiscountSeq;
          Decimal num11 = num1;
          nullable2 = copy1.UsagePriceVal;
          Decimal num12 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, (nullable2.HasValue ? new Decimal?(num11 * nullable2.GetValueOrDefault()) : new Decimal?()).Value);
          nullable2 = nullable1;
          Decimal num13 = num12;
          nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num13) : new Decimal?();
        }
label_13:
        ContractDetail contractDetail2 = (ContractDetail) ((PXSelectBase) this.ContractDetails).Cache.Locate((object) new ContractDetail()
        {
          ContractID = contract.ContractID,
          ContractItemID = contractItem.DepositItemID,
          RevID = contract.RevID
        });
        Decimal? nullable5;
        Decimal? nullable6;
        if (contractDetail2 != null)
        {
          ContractDetail copy2 = (ContractDetail) ((PXSelectBase) this.ContractDetails).Cache.CreateCopy((object) contractDetail2);
          ContractDetail contractDetail3 = copy2;
          nullable2 = contractDetail3.DepositUsedTotal;
          nullable3 = this.availableDeposit[contractItem.DepositItemID];
          nullable5 = nullable1;
          Decimal num14 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, (nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?()).Value);
          contractDetail3.DepositUsedTotal = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num14) : new Decimal?();
          ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(copy2);
          nullable3 = this.availableDeposit[contractItem.DepositItemID];
          Decimal num15 = 0M;
          if (nullable3.GetValueOrDefault() > num15 & nullable3.HasValue && this.depositUsage.TryGetValue(contractItem.DepositItemID, out usageData1))
          {
            UsageData usageData2 = usageData1;
            nullable3 = usageData2.ExtPrice;
            nullable2 = this.availableDeposit[contractItem.DepositItemID];
            Decimal? nullable7 = nullable1;
            Decimal num16 = 0M;
            nullable6 = nullable7.GetValueOrDefault() > num16 & nullable7.HasValue ? nullable1 : new Decimal?(0M);
            nullable5 = nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable8;
            if (!(nullable3.HasValue & nullable5.HasValue))
            {
              nullable6 = new Decimal?();
              nullable8 = nullable6;
            }
            else
              nullable8 = new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault());
            usageData2.ExtPrice = nullable8;
          }
          this.availableDeposit[contractItem.DepositItemID] = nullable1;
        }
        ContractDetail contractDetail4 = copy1;
        nullable5 = contractDetail4.Used;
        nullable3 = tran.Tran.BillableQty;
        Decimal? nullable9;
        if (!(nullable5.HasValue & nullable3.HasValue))
        {
          nullable6 = new Decimal?();
          nullable9 = nullable6;
        }
        else
          nullable9 = new Decimal?(nullable5.GetValueOrDefault() - nullable3.GetValueOrDefault());
        contractDetail4.Used = nullable9;
        ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(copy1);
        return true;
      }
    }
    return false;
  }

  protected virtual void ProcessSingleRecord(
    Contract contract,
    TranNotePair tran,
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res,
    List<UsageData> addedData)
  {
    ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    UsageData tranData = new UsageData();
    tranData.TranIDs.Add(tran.Tran.TranID);
    tranData.InventoryID = tran.Tran.InventoryID;
    tranData.ContractDetailsLineNbr = new int?((int?) contractDetail1?.LineNbr ?? int.MaxValue);
    tranData.ContractDetailID = (int?) contractDetail1?.ContractDetailID;
    tranData.ContractItemID = (int?) contractItem?.ContractItemID;
    tranData.Description = tran.Tran.Description;
    tranData.Qty = tran.Tran.BillableQty;
    tranData.UOM = tran.Tran.UOM;
    tranData.EmployeeID = tran.Tran.ResourceID;
    tranData.IsTranData = new bool?(true);
    tranData.BranchID = tran.Tran.BranchID;
    tranData.TranDate = tran.Tran.Date;
    tranData.CaseCD = tran.Tran.CaseCD;
    addedData.Add(tranData);
    if (this.ProcessDollarUsage(contract, tran, res, addedData))
      return;
    if (contractDetail1 != null && contractItem != null)
      tranData.PriceOverride = contractDetail1.UsagePriceVal;
    else if (!tran.Tran.IsQtyOnly.GetValueOrDefault())
    {
      tranData.PriceOverride = tran.Tran.UnitRate;
    }
    else
    {
      tranData.PriceOverride = this.CalculatePrice(contract, tranData);
      if (!tranData.PriceOverride.HasValue)
        throw new PXException("{0} has no price in this Currency", new object[1]
        {
          (object) inventoryItem.InventoryCD
        });
    }
    if (contractDetail1 == null)
      return;
    ContractDetail contractDetail2;
    if ((contractDetail2 = ((PXSelectBase<ContractDetail>) this.ContractDetails).Locate(contractDetail1)) != null && contractDetail2 != contractDetail1)
      contractDetail1 = contractDetail2;
    Decimal? nullable1 = new Decimal?(0M);
    Dictionary<int?, Decimal?> availableQty1 = this.availableQty;
    int? key1 = tran.Tran.InventoryID;
    int? key2 = new int?(key1.Value);
    ref Decimal? local = ref nullable1;
    if (!availableQty1.TryGetValue(key2, out local))
      nullable1 = new Decimal?(0M);
    Decimal? billableQty = tran.Tran.BillableQty;
    Decimal? nullable2 = nullable1;
    Decimal? nullable3;
    if (billableQty.GetValueOrDefault() <= nullable2.GetValueOrDefault() & billableQty.HasValue & nullable2.HasValue)
    {
      tranData.IsFree = new bool?(true);
      tranData.Prefix = contractItem.RecurringType == "P" ? PXMessages.LocalizeNoPrefix("Prepaid Usage") : PXMessages.LocalizeNoPrefix("Included Usage");
      Dictionary<int?, Decimal?> availableQty2 = this.availableQty;
      key1 = new int?(tran.Tran.InventoryID.Value);
      Dictionary<int?, Decimal?> dictionary = availableQty2;
      int? key3 = key1;
      nullable2 = availableQty2[key1];
      nullable3 = tran.Tran.BillableQty;
      Decimal num = nullable3.Value;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() - num);
      dictionary[key3] = nullable4;
    }
    else
    {
      nullable2 = nullable1;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        tranData.Qty = nullable1;
        tranData.IsFree = new bool?(true);
        tranData.Prefix = contractItem.RecurringType == "P" ? PXMessages.LocalizeNoPrefix("Prepaid Usage") : PXMessages.LocalizeNoPrefix("Included Usage");
        tranData = new UsageData();
        tranData.InventoryID = tran.Tran.InventoryID;
        tranData.ContractDetailsLineNbr = contractDetail1.LineNbr;
        tranData.ContractDetailID = contractDetail1.ContractDetailID;
        tranData.ContractItemID = (int?) contractItem?.ContractItemID;
        tranData.Description = tran.Tran.Description;
        tranData.UOM = inventoryItem.BaseUnit;
        tranData.EmployeeID = tran.Tran.ResourceID;
        tranData.IsTranData = new bool?(true);
        tranData.PriceOverride = contractDetail1.UsagePriceVal;
        tranData.BranchID = tran.Tran.BranchID;
        addedData.Add(tranData);
      }
      UsageData usageData = tranData;
      nullable2 = tran.Tran.BillableQty;
      nullable3 = nullable1;
      Decimal? nullable5 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      usageData.Qty = nullable5;
      tranData.IsFree = new bool?(false);
      tranData.Prefix = PXMessages.LocalizeNoPrefix("Overused");
      tranData.DiscountID = contractDetail1.RecurringDiscountID;
      tranData.DiscountSeq = contractDetail1.RecurringDiscountSeq;
      this.availableQty[new int?(tran.Tran.InventoryID.Value)] = new Decimal?(0M);
    }
    ContractDetail contractDetail3 = contractDetail1;
    nullable3 = contractDetail3.Used;
    nullable2 = tran.Tran.BillableQty;
    contractDetail3.Used = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(contractDetail1);
  }

  public virtual Decimal? CalculatePrice(Contract contract, UsageData tranData)
  {
    PX.Objects.AR.Customer customer;
    PX.Objects.CR.Location location;
    this.SetBillingTarget(contract, out customer, out location);
    DateTime date = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    PXCache<PX.Objects.CM.CurrencyInfo> sender = GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) this);
    PX.Objects.CM.CurrencyInfo currencyinfo = sender.Insert(new PX.Objects.CM.CurrencyInfo()
    {
      CuryRateTypeID = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() ? customer.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.ARRateTypeDflt : ((PXSelectBase<CMSetup>) this.cmsetup).Current.ARRateTypeDflt,
      CuryID = contract.CuryID
    });
    currencyinfo.SetCuryEffDate((PXCache) sender, (object) date);
    sender.Update(currencyinfo);
    string taxCalcMode = location?.CTaxCalcMode ?? "T";
    Decimal? salesPrice = ARSalesPriceMaint.CalculateSalesPrice(((PXSelectBase) this.ContractDetails).Cache, location.CPriceClassID, contract.CustomerID, tranData.InventoryID, currencyinfo, tranData.UOM, tranData.PreciseQty, date, new Decimal?(), taxCalcMode);
    sender.Delete(currencyinfo);
    return salesPrice;
  }

  protected virtual void ProcessSummaryUsageItems(
    Contract contract,
    List<TranNotePair> trans,
    PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> res,
    List<UsageData> addedData)
  {
    ContractDetail contractDetail1 = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    PX.Objects.IN.InventoryItem inventoryItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(res);
    string baseUnit = inventoryItem.BaseUnit;
    Decimal num1 = this.SumUsage(trans, baseUnit);
    if (!(num1 > 0M))
      return;
    UsageData tranData = new UsageData();
    tranData.TranIDs = trans.Select<TranNotePair, long?>((Func<TranNotePair, long?>) (tnp => tnp.Tran.TranID)).ToList<long?>();
    HashSet<int> source1 = new HashSet<int>(trans.Select<TranNotePair, int?>((Func<TranNotePair, int?>) (tnp => tnp.Tran.ResourceID)).Where<int?>((Func<int?, bool>) (employeeID => employeeID.HasValue)).Cast<int>());
    HashSet<DateTime> source2 = new HashSet<DateTime>(trans.Select<TranNotePair, DateTime?>((Func<TranNotePair, DateTime?>) (tnp => tnp.Tran.Date)).Where<DateTime?>((Func<DateTime?, bool>) (date => date.HasValue)).Cast<DateTime>());
    int? nullable1;
    tranData.EmployeeID = nullable1 = source1.Count == 1 ? new int?(source1.FirstOrDefault<int>()) : new int?();
    tranData.TranDate = source2.Count == 1 ? new DateTime?(source2.FirstOrDefault<DateTime>()) : new DateTime?();
    UsageData usageData1 = tranData;
    int? key1 = trans[0].Tran.InventoryID;
    int? nullable2 = new int?(key1.Value);
    usageData1.InventoryID = nullable2;
    UsageData usageData2 = tranData;
    int? nullable3;
    if (contractDetail1 == null)
    {
      key1 = new int?();
      nullable3 = key1;
    }
    else
      nullable3 = contractDetail1.LineNbr;
    key1 = nullable3;
    int? nullable4 = new int?(key1 ?? int.MaxValue);
    usageData2.ContractDetailsLineNbr = nullable4;
    UsageData usageData3 = tranData;
    int? nullable5;
    if (contractDetail1 == null)
    {
      key1 = new int?();
      nullable5 = key1;
    }
    else
      nullable5 = contractDetail1.ContractDetailID;
    usageData3.ContractDetailID = nullable5;
    UsageData usageData4 = tranData;
    int? nullable6;
    if (contractItem == null)
    {
      key1 = new int?();
      nullable6 = key1;
    }
    else
      nullable6 = contractItem.ContractItemID;
    usageData4.ContractItemID = nullable6;
    tranData.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
    tranData.Qty = new Decimal?(num1);
    tranData.UOM = baseUnit;
    tranData.IsTranData = new bool?(true);
    tranData.CaseCD = trans[0].Tran.CaseCD;
    addedData.Add(tranData);
    if (this.ProcessDollarUsage(contract, trans[0], baseUnit, new Decimal?(num1), tranData.EmployeeID, res, addedData))
      return;
    if (contractItem != null)
    {
      tranData.PriceOverride = contractDetail1.UsagePriceVal;
    }
    else
    {
      tranData.PriceOverride = this.CalculatePrice(contract, tranData);
      if (!tranData.PriceOverride.HasValue)
        throw new PXException("{0} has no price in this Currency", new object[1]
        {
          (object) inventoryItem.InventoryCD
        });
    }
    if (contractDetail1 == null)
      return;
    tranData.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
    Decimal? nullable7 = this.availableQty[tranData.InventoryID];
    Decimal num2 = num1;
    Decimal? nullable8 = nullable7;
    Decimal valueOrDefault = nullable8.GetValueOrDefault();
    Decimal? nullable9;
    if (num2 <= valueOrDefault & nullable8.HasValue)
    {
      tranData.IsFree = new bool?(true);
      tranData.Prefix = contractItem.RecurringType == "P" ? PXMessages.LocalizeNoPrefix("Prepaid Usage") : PXMessages.LocalizeNoPrefix("Included Usage");
      Dictionary<int?, Decimal?> availableQty = this.availableQty;
      key1 = tranData.InventoryID;
      Dictionary<int?, Decimal?> dictionary = availableQty;
      int? key2 = key1;
      Decimal? nullable10 = availableQty[key1];
      Decimal num3 = num1;
      Decimal? nullable11 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - num3) : new Decimal?();
      dictionary[key2] = nullable11;
    }
    else
    {
      nullable9 = nullable7;
      Decimal num4 = 0M;
      if (nullable9.GetValueOrDefault() > num4 & nullable9.HasValue)
      {
        tranData.Qty = nullable7;
        tranData.IsFree = new bool?(true);
        tranData.Prefix = contractItem.RecurringType == "P" ? PXMessages.LocalizeNoPrefix("Prepaid Usage") : PXMessages.LocalizeNoPrefix("Included Usage");
        tranData = new UsageData();
        tranData.InventoryID = trans[0].Tran.InventoryID;
        tranData.ContractDetailsLineNbr = contractDetail1.LineNbr;
        tranData.ContractDetailID = contractDetail1.ContractDetailID;
        UsageData usageData5 = tranData;
        int? nullable12;
        if (contractItem == null)
        {
          key1 = new int?();
          nullable12 = key1;
        }
        else
          nullable12 = contractItem.ContractItemID;
        usageData5.ContractItemID = nullable12;
        tranData.Description = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem);
        tranData.UOM = baseUnit;
        tranData.EmployeeID = nullable1;
        tranData.IsTranData = new bool?(true);
        tranData.PriceOverride = contractDetail1.UsagePriceVal;
        addedData.Add(tranData);
      }
      UsageData usageData6 = tranData;
      Decimal num5 = num1;
      nullable9 = nullable7;
      Decimal? nullable13 = nullable9.HasValue ? new Decimal?(num5 - nullable9.GetValueOrDefault()) : new Decimal?();
      usageData6.Qty = nullable13;
      tranData.IsFree = new bool?(false);
      tranData.Prefix = PXMessages.LocalizeNoPrefix("Overused");
      tranData.DiscountID = contractDetail1.RecurringDiscountID;
      tranData.DiscountSeq = contractDetail1.RecurringDiscountSeq;
      this.availableQty[tranData.InventoryID] = new Decimal?(0M);
    }
    ContractDetail contractDetail2 = contractDetail1;
    nullable9 = contractDetail2.Used;
    Decimal num6 = num1;
    contractDetail2.Used = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - num6) : new Decimal?();
    ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(contractDetail1);
  }

  protected virtual Decimal SumUsage(List<TranNotePair> trans, string targetUOM)
  {
    return trans.Sum<TranNotePair>((Func<TranNotePair, Decimal>) (item => this.ConvertUsage(((PXSelectBase) this.Transactions).Cache, item.Tran.InventoryID, item.Tran.UOM, targetUOM, item.Tran.BillableQty)));
  }

  protected virtual Decimal SumUnbilledAmt(Contract contract, List<UsageData> data)
  {
    PX.Objects.AR.Customer customer;
    this.SetBillingTarget(contract, out customer, out PX.Objects.CR.Location _);
    ARInvoiceEntry arInvoiceEntry = (ARInvoiceEntry) null;
    Decimal num1 = 0M;
    foreach (UsageData usageData in data)
    {
      bool? nullable1 = usageData.IsTranData;
      Decimal? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = usageData.IsTranData;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = usageData.Proportion;
          Decimal num2 = nullable2 ?? 1M;
          nullable2 = usageData.ExtPrice;
          Decimal num3 = nullable2.Value;
          Decimal num4 = Math.Round(num2 * num3, 4, MidpointRounding.AwayFromZero);
          num1 += num4;
          continue;
        }
      }
      Decimal num5 = 0M;
      nullable1 = usageData.IsFree;
      if (!nullable1.GetValueOrDefault())
      {
        nullable2 = usageData.PriceOverride;
        if (nullable2.HasValue)
        {
          nullable2 = usageData.PriceOverride;
          num5 = nullable2.Value;
        }
        else
        {
          if (arInvoiceEntry == null)
          {
            arInvoiceEntry = CTBillEngine.CreateInvoiceGraph();
            ((PXSelectBase<ARSetup>) arInvoiceEntry.ARSetup).Current.LineDiscountTarget = "E";
            ((PXGraph) arInvoiceEntry).Clear();
            PX.Objects.AR.ARInvoice arInvoice = ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Insert();
            arInvoice.BranchID = new int?();
            arInvoice.CustomerID = customer.BAccountID;
            arInvoice.CustomerLocationID = contract.LocationID ?? customer.DefLocationID;
            arInvoice.ProjectID = contract.ContractID;
            ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Update(arInvoice);
          }
          PX.Objects.AR.ARTran instance = (PX.Objects.AR.ARTran) ((PXSelectBase) arInvoiceEntry.Transactions).Cache.CreateInstance();
          instance.InventoryID = usageData.InventoryID;
          instance.Qty = usageData.Qty;
          instance.UOM = usageData.UOM;
          instance.FreezeManualDisc = new bool?(true);
          instance.ManualDisc = new bool?(true);
          instance.ManualPrice = new bool?(true);
          instance.DiscountID = usageData.DiscountID;
          instance.DiscountSequenceID = usageData.DiscountSeq;
          nullable2 = ((PX.Objects.AR.ARTran) ((PXGraph) arInvoiceEntry).Caches[typeof (PX.Objects.AR.ARTran)].Update((object) instance)).UnitPrice;
          num5 = nullable2.GetValueOrDefault();
        }
      }
      Decimal num6 = num1;
      nullable2 = usageData.Qty;
      Decimal num7 = nullable2.Value * num5;
      num1 = num6 + num7;
    }
    return num1;
  }

  protected virtual Decimal ConvertUsage(
    PXCache sender,
    int? inventoryID,
    string fromUOM,
    string toUOM,
    Decimal? value)
  {
    if (!value.HasValue)
      return 0M;
    if (fromUOM == toUOM)
      return value.Value;
    Decimal num = INUnitAttribute.ConvertToBase(sender, inventoryID, fromUOM, value.Value, INPrecision.QUANTITY);
    return INUnitAttribute.ConvertFromBase(sender, inventoryID, toUOM, num, INPrecision.QUANTITY);
  }

  protected virtual bool IsLastBillBeforeExpiration(
    Contract contract,
    ContractBillingSchedule schedule)
  {
    return contract.ExpireDate.HasValue && schedule.NextDate.HasValue && schedule.NextDate.Value.Date >= contract.ExpireDate.Value.Date;
  }

  protected virtual bool IsFirstBillAfterRenewalExpiredContractInGracePeriod(
    Contract contract,
    ContractBillingSchedule schedule)
  {
    return schedule.NextDate.HasValue && schedule.LastDate.HasValue && (int) (schedule.NextDate.Value.Date - schedule.LastDate.Value.Date).TotalDays == 1;
  }

  protected virtual bool IsPrevToLastBillBeforeExpiration(
    Contract contract,
    ContractBillingSchedule schedule)
  {
    if (!contract.ExpireDate.HasValue || !schedule.NextDate.HasValue || !(schedule.NextDate.Value.Date < contract.ExpireDate.Value.Date))
      return false;
    DateTime? nextBillingDate = this.GetNextBillingDate(schedule.Type, contract.CustomerID, schedule.NextDate, schedule.StartBilling);
    return nextBillingDate.HasValue && nextBillingDate.Value.Date >= contract.ExpireDate.Value.Date;
  }

  protected virtual bool IsFirstRegularBill(Contract contract, ContractBillingSchedule schedule)
  {
    if (!contract.ActivationDate.HasValue)
      return false;
    if (!schedule.LastDate.HasValue)
      return true;
    DateTime? lastDate = schedule.LastDate;
    DateTime? activationDate = contract.ActivationDate;
    if (lastDate.HasValue != activationDate.HasValue)
      return false;
    return !lastDate.HasValue || lastDate.GetValueOrDefault() == activationDate.GetValueOrDefault();
  }

  protected virtual void UpdateReferencePMTran2ARTran(
    ARInvoiceEntry graph,
    Dictionary<int, List<TranNotePair>> sourceTran,
    List<UsageData> tranData)
  {
    if (sourceTran == null || tranData == null)
      return;
    Dictionary<long, PMTran> sourceTranDict = sourceTran.Values.SelectMany<List<TranNotePair>, TranNotePair>((Func<List<TranNotePair>, IEnumerable<TranNotePair>>) (list => (IEnumerable<TranNotePair>) list)).ToDictionary<TranNotePair, long, PMTran>((Func<TranNotePair, long>) (pair => pair.Tran.TranID.Value), (Func<TranNotePair, PMTran>) (pair => pair.Tran));
    foreach (UsageData usageData in tranData.Where<UsageData>(new Func<UsageData, bool>(CTBillEngine.IsBillable)))
    {
      foreach (PMTran pmTran in usageData.TranIDs.Cast<long>().Select<long, PMTran>((Func<long, PMTran>) (tranID => sourceTranDict[tranID])))
      {
        pmTran.ARTranType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Current.DocType;
        pmTran.ARRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Current.RefNbr;
        pmTran.RefLineNbr = usageData.RefLineNbr;
        ((PXSelectBase<PMTran>) graph.RefContractUsageTran).Update(pmTran);
      }
    }
  }

  protected virtual void AutoReleaseInvoice(Contract contract)
  {
    if (this.doclist.Count <= 0)
      return;
    if (!((PXSelectBase<Contract>) this.Contracts).SelectSingle(new object[1]
    {
      (object) contract.TemplateID
    }).AutomaticReleaseAR.GetValueOrDefault())
      return;
    try
    {
      ARDocumentRelease.ReleaseDoc(this.doclist, false);
    }
    catch (Exception ex)
    {
      throw new PXException("Auto-release of ARInvoice document created during billing failed. Please try to release this document manually.", ex);
    }
  }

  protected virtual void RaiseErrorIfUnreleasedUsageExist(Contract contract)
  {
    bool flag = false;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(PXLocalizer.Localize("List of unreleased billable cases:"));
    foreach (PXResult<CRCase> pxResult in ((PXSelectBase<CRCase>) new PXSelectJoin<CRCase, InnerJoin<CRCaseClass, On<CRCaseClass.caseClassID, Equal<CRCase.caseClassID>>>, Where<CRCase.contractID, Equal<Required<CRCase.contractID>>, And<CRCase.released, Equal<False>, And<CRCase.isBillable, Equal<True>, And<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perCase>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) contract.ContractID
    }))
    {
      CRCase crCase = PXResult<CRCase>.op_Implicit(pxResult);
      flag = true;
      stringBuilder.AppendLine(crCase.CaseCD);
    }
    stringBuilder.AppendLine(PXLocalizer.Localize("List of unreleased billable activities:"));
    foreach (PXResult<CRPMTimeActivity, CRCase, CRCaseClass> pxResult in ((PXSelectBase<CRPMTimeActivity>) new PXSelectJoin<CRPMTimeActivity, InnerJoin<CRCase, On<CRPMTimeActivity.refNoteID, Equal<CRCase.noteID>>, InnerJoin<CRCaseClass, On<CRCaseClass.caseClassID, Equal<CRCase.caseClassID>>>>, Where<CRCase.contractID, Equal<Required<CRCase.contractID>>, And<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>, And<CRPMTimeActivity.released, Equal<False>, And<CRPMTimeActivity.isBillable, Equal<True>, And<CRPMTimeActivity.billed, Equal<False>, And<CRPMTimeActivity.uistatus, NotEqual<ActivityStatusListAttribute.canceled>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) contract.ContractID
    }))
    {
      flag = true;
      stringBuilder.AppendFormat(PXLocalizer.LocalizeFormat("Case: {0} Activity: {1}{2}", new object[3]
      {
        (object) PXResult<CRPMTimeActivity, CRCase, CRCaseClass>.op_Implicit(pxResult).CaseCD,
        (object) PXResult<CRPMTimeActivity, CRCase, CRCaseClass>.op_Implicit(pxResult).Subject,
        (object) Environment.NewLine
      }));
    }
    if (flag)
    {
      PXTrace.WriteInformation(stringBuilder.ToString());
      throw new PXException(PXLocalizer.LocalizeFormat("During the last billing of the {0} contract all cases and activities must be already released. There exists at least one open or unreleased case or activity for this contract. The list of open cases or activities is recorded in the Trace.", new object[1]
      {
        (object) contract.ContractCD
      }));
    }
  }

  protected virtual void EnsureContractDetailTranslations()
  {
    PXDBLocalizableStringAttribute.EnsureTranslations((Func<string, bool>) (tableName => string.Equals(tableName, "ContractDetail", StringComparison.OrdinalIgnoreCase)));
  }

  public class SingleCurrency : SingleCurrencyGraph<CTBillEngine, Contract>
  {
    public static bool IsActive() => true;
  }

  /// <summary>
  /// Parameter which defines a variant of description for inventory in accordance with contract action
  /// </summary>
  private enum InventoryAction
  {
    Setup,
    ActivateRenew,
    SetupUpgrade,
    UpgradeActivation,
  }

  public class Proportions
  {
    public Proportions(Decimal prepaid, Decimal postpaid)
    {
      this.Prepaid = prepaid;
      this.Postpaid = postpaid;
    }

    public Proportions()
      : this(1M, 0M)
    {
    }

    public Decimal Prepaid { get; set; }

    public Decimal Postpaid { get; set; }
  }

  public class DatePair(DateTime? last, DateTime? next) : Tuple<DateTime?, DateTime?>(last, next)
  {
    public DateTime? Start => this.Item1;

    public DateTime? End => this.Item2;
  }

  protected class InvoiceData
  {
    public DateTime InvoiceDate { get; private set; }

    public List<UsageData> UsageData { get; private set; }

    public InvoiceData(DateTime date)
    {
      this.InvoiceDate = date;
      this.UsageData = new List<UsageData>();
    }

    public string GetDocType()
    {
      return !(this.UsageData.Sum<UsageData>((Func<UsageData, Decimal>) (data =>
      {
        bool? isTranData = data.IsTranData;
        bool flag = false;
        if (isTranData.GetValueOrDefault() == flag & isTranData.HasValue || data.IsFree.GetValueOrDefault() || !data.PriceOverride.HasValue)
          return data.ExtPrice.GetValueOrDefault();
        Decimal? qty = data.Qty;
        Decimal? nullable1 = data.PriceOverride;
        Decimal? nullable2 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
        nullable1 = data.Proportion;
        Decimal num = nullable1 ?? 1.0M;
        Decimal? nullable3;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new Decimal?(nullable2.GetValueOrDefault() * num);
        nullable1 = nullable3;
        return Math.Round(nullable1.GetValueOrDefault(), 4, MidpointRounding.AwayFromZero);
      })) < 0M) ? "INV" : "CRM";
    }
  }

  protected class Refund
  {
    public readonly Decimal? Amount;
    public readonly PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> Item;
    public readonly bool IsRenew;

    public Refund(
      Decimal? amount,
      PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> item,
      bool isRenew)
    {
      this.Amount = amount;
      this.Item = item;
      this.IsRenew = isRenew;
    }
  }

  [PXHidden]
  public class InventoryItemEx : PX.Objects.IN.InventoryItem
  {
    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CTBillEngine.InventoryItemEx.inventoryID>
    {
    }
  }
}
