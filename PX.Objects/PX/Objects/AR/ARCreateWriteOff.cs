// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCreateWriteOff
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARCreateWriteOff : PXGraph<
#nullable disable
ARCreateWriteOff>
{
  public PXCancel<ARWriteOffFilter> Cancel;
  public PXFilter<ARWriteOffFilter> Filter;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<ARWriteOffFilter> EditDetail;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (ARRegister.refNbr))]
  public PXFilteredProcessingJoin<ARCreateWriteOff.ARRegisterEx, ARWriteOffFilter, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>, And<Customer.smallBalanceAllow, Equal<True>>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARRegister.docType>, And<ARAdjust.adjdRefNbr, Equal<ARRegister.refNbr>, And<ARAdjust.released, Equal<False>, And<ARAdjust.voided, Equal<False>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARRegister.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARRegister.refNbr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>>>>>>>>, Where<Where2<MatchWithBranch<ARRegister.branchID>, And2<Match<Current<AccessInfo.userName>>, And<ARRegister.released, Equal<True>, And<ARRegister.hold, NotEqual<True>, And<ARRegister.openDoc, Equal<True>, And<ARRegister.pendingPPD, NotEqual<True>, And2<Where<ARCreateWriteOff.ARRegisterEx.docBal, Greater<decimal0>, Or<ARRegister.curyDocBal, Greater<decimal0>>>, And<ARCreateWriteOff.ARRegisterEx.docBal, LessEqual<Current<ARWriteOffFilter.wOLimit>>, And2<Where<ARRegister.branchID, InsideBranchesOf<Current<ARWriteOffFilter.orgBAccountID>>, Or<Where<Current2<ARWriteOffFilter.orgBAccountID>, IsNull, And<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>>, And2<Where<ARAdjust.adjgRefNbr, IsNull, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallBalanceWO>, And2<Where<ARRegister.docType, Equal<ARDocType.invoice>, Or<ARRegister.docType, Equal<ARDocType.debitMemo>, Or<ARRegister.docType, Equal<ARDocType.finCharge>>>>, Or<ARAdjust2.adjdRefNbr, IsNull, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallCreditWO>, And<Where<ARRegister.docType, Equal<ARDocType.payment>, Or<ARRegister.docType, Equal<ARDocType.creditMemo>, Or<ARRegister.docType, Equal<ARDocType.prepayment>>>>>>>>>>, And<Where<Current<ARWriteOffFilter.customerID>, IsNull, Or<Current<ARWriteOffFilter.customerID>, Equal<ARRegister.customerID>>>>>>>>>>>>>>>> ARDocumentList;
  public CMSetupSelect CMSetup;
  public PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Current<ARWriteOffFilter.customerID>>>> customer;
  public PXSelectReadonly<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<PX.Objects.AR.ARSetup.dfltCustomerClassID>>>> customerclass;
  public ARSetupNoMigrationMode ARSetup;
  public PXSelect<PX.Objects.GL.Sub> subs;
  public PXAction<ARWriteOffFilter> ShowCustomer;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public Customer CUSTOMER
  {
    get
    {
      return PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) this.customer).Select(Array.Empty<object>()));
    }
  }

  public CustomerClass CUSTOMERCLASS
  {
    get
    {
      return PXResultset<CustomerClass>.op_Implicit(((PXSelectBase<CustomerClass>) this.customerclass).Select(Array.Empty<object>()));
    }
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<ARCreateWriteOff.ARRegisterEx>) this.ARDocumentList).Current != null)
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.ARDocumentList).Cache, (object) ((PXSelectBase<ARCreateWriteOff.ARRegisterEx>) this.ARDocumentList).Current, "Document", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  public ARCreateWriteOff()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ARCreateWriteOff.\u003C\u003Ec.\u003C\u003E9__18_0 ?? (ARCreateWriteOff.\u003C\u003Ec.\u003C\u003E9__18_0 = new PXFieldDefaulting((object) ARCreateWriteOff.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__18_0))));
    OpenPeriodAttribute.SetValidatePeriod<ARWriteOffFilter.wOFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName.ToLower() == "filter" && values != null)
      values[(object) "SelTotal"] = PXCache.NotSetValue;
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Balance")]
  protected virtual void ARRegisterEx_CuryDocBal_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency Amount")]
  protected virtual void ARRegisterEx_CuryOrigDocAmt_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARRegisterEx_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARCreateWriteOff.ARRegisterEx row1))
      return;
    int? nullable1;
    int num;
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      nullable1 = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.OrgBAccountID;
      num = nullable1.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag1 = num != 0;
    string strA;
    if (flag1)
    {
      nullable1 = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.BranchID;
      if (nullable1.HasValue)
      {
        strA = this.FinPeriodRepository.GetByID(((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.WOFinPeriodID, PXAccess.GetParentOrganizationID(((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.BranchID)).MasterFinPeriodID;
        goto label_12;
      }
    }
    if (flag1)
    {
      nullable1 = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.OrganizationID;
      if (nullable1.HasValue)
      {
        strA = this.FinPeriodRepository.GetByID(((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.WOFinPeriodID, ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.OrganizationID).MasterFinPeriodID;
        goto label_12;
      }
    }
    strA = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.WOFinPeriodID;
label_12:
    bool flag2 = row1.DocType == "CRM" && row1.PaymentsByLinesAllowed.GetValueOrDefault();
    DateTime? nullable2 = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.WODate;
    DateTime t1 = nullable2.Value;
    nullable2 = ((ARRegister) e.Row).DocDate;
    DateTime t2 = nullable2.Value;
    bool flag3 = DateTime.Compare(t1, t2) >= 0;
    string tranPeriodId = ((ARRegister) e.Row).TranPeriodID;
    bool flag4 = string.Compare(strA, tranPeriodId) >= 0;
    PXUIFieldAttribute.SetEnabled<ARCreateWriteOff.ARRegisterEx.selected>(sender, e.Row, !flag2 & flag3 & flag4);
    PXCache pxCache1 = sender;
    ARCreateWriteOff.ARRegisterEx arRegisterEx = row1;
    string refNbr = row1.RefNbr;
    PXSetPropertyException propertyException1;
    if (!flag2)
      propertyException1 = (PXSetPropertyException) null;
    else
      propertyException1 = new PXSetPropertyException("The {0} credit memo is paid by line; write-offs are not supported for such credit memos. To write off the credit memo, create a debit memo and apply this credit memo to it on the Invoices and Memos (AR301000) form.", (PXErrorLevel) 5, new object[1]
      {
        (object) row1.RefNbr
      });
    pxCache1.RaiseExceptionHandling<ARRegister.refNbr>((object) arRegisterEx, (object) refNbr, (Exception) propertyException1);
    PXCache pxCache2 = sender;
    object row2 = e.Row;
    PXSetPropertyException propertyException2;
    if (flag3)
      propertyException2 = (PXSetPropertyException) null;
    else
      propertyException2 = new PXSetPropertyException("Write-Off {0} cannot be less than Document Date.", (PXErrorLevel) 5, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ARWriteOffFilter.wODate>(((PXSelectBase) this.Filter).Cache)
      });
    pxCache2.RaiseExceptionHandling<ARRegister.docDate>(row2, (object) null, (Exception) propertyException2);
    PXCache pxCache3 = sender;
    object row3 = e.Row;
    PXSetPropertyException propertyException3;
    if (flag4)
      propertyException3 = (PXSetPropertyException) null;
    else
      propertyException3 = new PXSetPropertyException("{0} cannot be less than Document Financial Period.", (PXErrorLevel) 5, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ARWriteOffFilter.wOFinPeriodID>(((PXSelectBase) this.Filter).Cache)
      });
    pxCache3.RaiseExceptionHandling<ARRegister.finPeriodID>(row3, (object) null, (Exception) propertyException3);
    if (!string.IsNullOrEmpty(row1.ReasonCode))
      return;
    row1.ReasonCode = ((PXSelectBase<ARWriteOffFilter>) this.Filter).Current.ReasonCode;
  }

  protected virtual void ARWriteOffFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARCreateWriteOff.\u003C\u003Ec__DisplayClass23_0 cDisplayClass230 = new ARCreateWriteOff.\u003C\u003Ec__DisplayClass23_0();
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.ARDocumentList).Cache, typeof (ARCreateWriteOff.ARRegisterEx.reasonCode).Name, true);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.filter = (ARWriteOffFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass230.filter != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (((PXSelectBase<Customer>) this.customer).Current != null && !object.Equals((object) cDisplayClass230.filter.CustomerID, (object) ((PXSelectBase<Customer>) this.customer).Current.BAccountID))
        ((PXSelectBase<Customer>) this.customer).Current = (Customer) null;
      ((PXProcessingBase<ARCreateWriteOff.ARRegisterEx>) this.ARDocumentList).SetAutoPersist(true);
      // ISSUE: method pointer
      ((PXProcessingBase<ARCreateWriteOff.ARRegisterEx>) this.ARDocumentList).SetProcessDelegate(new PXProcessingBase<ARCreateWriteOff.ARRegisterEx>.ProcessListDelegate((object) cDisplayClass230, __methodptr(\u003CARWriteOffFilter_RowSelected\u003Eb__0)));
    }
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<ARRegister.curyID>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARRegister.curyDocBal>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARRegister.curyOrigDocAmt>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, flag);
  }

  protected virtual void ARWriteOffFilter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (((PXSelectBase<CustomerClass>) this.customerclass).Current == null)
      return;
    ((ARWriteOffFilter) e.Row).WOLimit = ((PXSelectBase<CustomerClass>) this.customerclass).Current.SmallBalanceLimit;
  }

  protected virtual void ARWriteOffFilter_WOType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARWriteOffFilter row = (ARWriteOffFilter) e.Row;
    sender.SetDefaultExt<ARWriteOffFilter.reasonCode>(e.Row);
    if (((PXSelectBase<Customer>) this.customer).Current == null)
      return;
    int? baccountId = ((PXSelectBase<Customer>) this.customer).Current.BAccountID;
    int? customerId = row.CustomerID;
    if (baccountId.GetValueOrDefault() == customerId.GetValueOrDefault() & baccountId.HasValue == customerId.HasValue)
      return;
    ((PXSelectBase<Customer>) this.customer).Current = (Customer) null;
  }

  protected virtual void ARWriteOffFilter_ReasonCode_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((ARWriteOffFilter) e.Row).WOType == "SMB")
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.BalanceWriteOff;
    else
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.CreditWriteOff;
  }

  protected virtual void ARWriteOffFilter_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.ARWriteOffFilter_WOType_FieldUpdated(sender, e);
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      ((ARWriteOffFilter) e.Row).WOLimit = ((PXSelectBase<Customer>) this.customer).Current.SmallBalanceLimit;
    }
    else
    {
      if (((PXSelectBase<CustomerClass>) this.customerclass).Current == null)
        return;
      ((ARWriteOffFilter) e.Row).WOLimit = ((PXSelectBase<CustomerClass>) this.customerclass).Current.SmallBalanceLimit;
    }
  }

  protected virtual void ARWriteOffFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARWriteOffFilter row = e.Row as ARWriteOffFilter;
    ARWriteOffFilter oldRow = e.OldRow as ARWriteOffFilter;
    if (row == null || oldRow == null)
      return;
    if (!sender.ObjectsEqual<ARWriteOffFilter.woType, ARWriteOffFilter.branchID, ARWriteOffFilter.customerID>((object) row, (object) oldRow))
    {
      ((PXSelectBase) this.ARDocumentList).Cache.Clear();
      row.SelTotal = new Decimal?(0M);
    }
    else
    {
      if (!(row.ReasonCode != oldRow.ReasonCode))
        return;
      foreach (PXResult<ARCreateWriteOff.ARRegisterEx> pxResult in ((PXSelectBase<ARCreateWriteOff.ARRegisterEx>) this.ARDocumentList).Select(Array.Empty<object>()))
        ((PXSelectBase) this.ARDocumentList).Cache.SetValue<ARCreateWriteOff.ARRegisterEx.reasonCode>((object) PXResult<ARCreateWriteOff.ARRegisterEx>.op_Implicit(pxResult), (object) row.ReasonCode);
    }
  }

  public static void CreatePayments(List<ARRegister> list, ARWriteOffFilter filter)
  {
    if (string.IsNullOrEmpty(filter.ReasonCode))
      throw new PXException("Reason Code must be specified before running the process.");
    bool flag1 = false;
    IARWriteOffEntry arWriteOffEntry = !(filter.WOType == "SMB") ? (IARWriteOffEntry) PXGraph.CreateInstance<ARSmallCreditWriteOffEntry>() : (IARWriteOffEntry) PXGraph.CreateInstance<ARSmallBalanceWriteOffEntry>();
    list = new List<ARRegister>((IEnumerable<ARRegister>) list);
    PXCache cache = (arWriteOffEntry as PXGraph).Caches[typeof (ARCreateWriteOff.ARRegisterEx)];
    IFinPeriodRepository service = ((PXGraph) arWriteOffEntry).GetService<IFinPeriodRepository>();
    list = list.OrderBy<ARRegister, Tuple<string, string, string, string, string>>((Func<ARRegister, Tuple<string, string, string, string, string>>) (doc => new Tuple<string, string, string, string, string>((string) (cache.GetValueExt<ARRegister.branchID>((object) doc) as PXFieldState).Value, doc.CuryID, (string) (cache.GetValueExt<ARRegister.customerID>((object) doc) as PXFieldState).Value, doc.DocType, doc.RefNbr))).ToList<ARRegister>();
    foreach (ARCreateWriteOff.ARRegisterEx arRegisterEx in list)
    {
      PXProcessing<ARRegister>.SetCurrentItem((object) arRegisterEx);
      try
      {
        ReasonCode reasonCode = PXResultset<ReasonCode>.op_Implicit(PXSelectBase<ReasonCode, PXSelect<ReasonCode, Where<ReasonCode.reasonCodeID, Equal<Required<ReasonCode.reasonCodeID>>>>.Config>.Select((PXGraph) arWriteOffEntry, new object[1]
        {
          (object) (arRegisterEx.ReasonCode ?? filter.ReasonCode)
        }));
        if (reasonCode == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("No reason code with the given id was found in the system. Code: {0}.", new object[1]
          {
            (object) filter.ReasonCode
          }));
        PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) arWriteOffEntry, new object[2]
        {
          (object) arRegisterEx.CustomerID,
          (object) arRegisterEx.CustomerLocationID
        }));
        PX.Objects.CR.Standalone.Location location2 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) arWriteOffEntry, new object[1]
        {
          (object) arRegisterEx.BranchID
        }));
        if (!(reasonCode.Usage == "B") && !(reasonCode.Usage == "C"))
          throw new PXException("Invalid Reason Code Usage. Only Balance Write-Off or Credit Write-Off codes are expected.");
        object obj = (object) ReasonCodeSubAccountMaskAttribute.MakeSub<ReasonCode.subMask>((PXGraph) arWriteOffEntry, reasonCode.SubMask, new object[3]
        {
          (object) reasonCode.SubID,
          (object) location1.CSalesSubID,
          (object) location2.CMPSalesSubID
        }, new System.Type[3]
        {
          typeof (ReasonCode.subID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.CR.Standalone.Location.cMPSalesSubID)
        });
        ARReleaseProcess.EnsureNoUnreleasedVoidPaymentExists(arWriteOffEntry as PXGraph, (ARRegister) arRegisterEx, "written off");
        bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() && filter.OrgBAccountID.HasValue;
        string masterPeriodID = !flag2 || !filter.BranchID.HasValue ? (!flag2 || !filter.OrganizationID.HasValue ? filter.WOFinPeriodID : service.GetByID(filter.WOFinPeriodID, filter.OrganizationID).MasterFinPeriodID) : service.GetByID(filter.WOFinPeriodID, PXAccess.GetParentOrganizationID(filter.BranchID)).MasterFinPeriodID;
        arWriteOffEntry.PrepareWriteOff(reasonCode, obj.ToString(), filter.WODate, masterPeriodID, (ARRegister) arRegisterEx);
        List<Batch> externalPostList = new List<Batch>();
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          arWriteOffEntry.SaveWriteOff();
          ARDocumentRelease.ReleaseDoc(new List<ARRegister>()
          {
            arWriteOffEntry.ARDocument
          }, false, externalPostList);
          transactionScope.Complete();
        }
        ARReleaseProcess instance1 = PXGraph.CreateInstance<ARReleaseProcess>();
        PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
        try
        {
          for (int index = 0; index < externalPostList.Count; ++index)
          {
            Batch b = externalPostList[index];
            if (instance1.AutoPost)
            {
              ((PXGraph) instance2).Clear();
              instance2.PostBatchProc(b);
            }
          }
          PXProcessing<ARRegister>.SetProcessed();
        }
        catch (Exception ex)
        {
          throw new ARCreateWriteOff.BatchPostingException(ex);
        }
        PXProcessing<ARRegister>.SetProcessed();
      }
      catch (ARCreateWriteOff.BatchPostingException ex)
      {
        PXProcessing<ARRegister>.SetWarning(ex.InnerException);
        flag1 = true;
      }
      catch (Exception ex)
      {
        PXProcessing<ARRegister>.SetError(ex);
        flag1 = true;
      }
    }
    if (flag1)
    {
      PXProcessing<ARRegister>.SetCurrentItem((object) null);
      throw new PXException("One or more documents could not be released.");
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable showCustomer(PXAdapter adapter)
  {
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.customer).Cache, (object) this.CUSTOMER, "Customer", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  private class BatchPostingException : Exception
  {
    public BatchPostingException(Exception e)
      : base(string.Empty, e)
    {
    }

    protected BatchPostingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  [Serializable]
  public class ARRegisterEx : ARRegister
  {
    protected string _ReasonCode;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public override bool? Selected { get; set; }

    [PXDBBaseCury]
    [PXUIField]
    [PXParent(typeof (Select<ARWriteOffFilter>), UseCurrent = true)]
    [PXUnboundFormula(typeof (Switch<Case<Where<ARCreateWriteOff.ARRegisterEx.selected, Equal<True>>, ARCreateWriteOff.ARRegisterEx.docBal>, decimal0>), typeof (SumCalc<ARWriteOffFilter.selTotal>))]
    public override Decimal? DocBal { get; set; }

    [PXString(20, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (Search<ReasonCode.reasonCodeID, Where2<Where<ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallBalanceWO>>>, Or<Where<ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallCreditWO>>>>>>))]
    public virtual string ReasonCode
    {
      get => this._ReasonCode;
      set => this._ReasonCode = value;
    }

    [PXDBBaseCury(BqlField = typeof (ARRegister.origDocAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public override Decimal? OrigDocAmt { get; set; }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCreateWriteOff.ARRegisterEx.selected>
    {
    }

    public new abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCreateWriteOff.ARRegisterEx.docBal>
    {
    }

    public abstract class reasonCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCreateWriteOff.ARRegisterEx.reasonCode>
    {
    }

    public new abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCreateWriteOff.ARRegisterEx.origDocAmt>
    {
    }
  }
}
