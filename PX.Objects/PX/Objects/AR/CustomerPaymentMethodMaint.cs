// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.PaymentProfile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.AR;

public class CustomerPaymentMethodMaint : 
  PXGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>,
  ICaptionable
{
  public PXAction<PX.Objects.AR.CustomerPaymentMethod> viewBillAddressOnMap;
  public PXAction<PX.Objects.AR.CustomerPaymentMethod> validateAddresses;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.AR.CustomerPaymentMethod.customerCCPID), typeof (PX.Objects.AR.CustomerPaymentMethod.expirationDate), typeof (PX.Objects.AR.CustomerPaymentMethod.descr)})]
  public PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>>> CustomerPaymentMethod;
  public PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>> CurrentCPM;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CustomerPaymentMethodDetail.value)})]
  public PXSelectJoinOrderBy<CustomerPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethodDetail, On<PX.Objects.CA.PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>>>, OrderBy<Asc<PX.Objects.CA.PaymentMethodDetail.orderIndex>>> Details;
  public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethodDetail, On<PX.Objects.CA.PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>, And<PX.Objects.CA.PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>, And<PX.Objects.CA.PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>, OrderBy<Asc<PX.Objects.CA.PaymentMethodDetail.orderIndex>>> DetailsAll;
  public PXSelect<PX.Objects.CA.PaymentMethodDetail, Where<PX.Objects.CA.PaymentMethodDetail.paymentMethodID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.CA.PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>> PMDetails;
  public PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>>> PaymentMethodDef;
  public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PX.Objects.CA.PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PX.Objects.CA.PaymentMethodDetail.detailID>, And<PX.Objects.CA.PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<PX.Objects.CA.PaymentMethodDetail.isCCProcessingID, Equal<True>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>> ccpIdDet;
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.billAddressID>>>> BillAddress;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.billContactID>>>> BillContact;
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>>> Customer;
  public PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>>>>> CustomerProcessingID;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  private int? bAccountID;
  private string mergedPaymentMethod;

  [InjectDependency]
  public ICCDisplayMaskService CCDisplayMaskService { get; set; }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBillAddressOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    PX.Objects.AR.CustomerPaymentMethod current1 = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
    if (current1 != null && current1.BillAddressID.HasValue)
    {
      PX.Objects.CR.Address current2 = ((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Current;
      if (current2 != null)
      {
        bool? isValidated = current2.IsValidated;
        bool flag = false;
        if (isValidated.GetValueOrDefault() == flag & isValidated.HasValue)
          PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this, current2, true, true);
      }
    }
    return adapter.Get();
  }

  public CustomerPaymentMethodMaint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodDetail.detailID>(((PXSelectBase) this.Details).Cache, (object) null, false);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(CustomerPaymentMethodMaint.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (CustomerPaymentMethodMaint.\u003C\u003Ec.\u003C\u003E9__8_0 = new PXFieldDefaulting((object) CustomerPaymentMethodMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__8_0))));
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  protected virtual void Customer_AcctCD_CacheAttached(PXCache sender)
  {
  }

  public IEnumerable billAddress()
  {
    PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
    if (current != null)
    {
      int? nullable = current.BAccountID;
      if (nullable.HasValue)
      {
        nullable = current.BillAddressID;
        return nullable.HasValue ? (IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.BillAddressID
        }) : (IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.defBillAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.BAccountID
        });
      }
    }
    return (IEnumerable) null;
  }

  public IEnumerable billContact()
  {
    PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
    if (current != null)
    {
      int? nullable = current.BAccountID;
      if (nullable.HasValue)
      {
        nullable = current.BillContactID;
        return nullable.HasValue ? (IEnumerable) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.BillContactID
        }) : (IEnumerable) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.defBillContactID, Equal<PX.Objects.CR.Contact.contactID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.BAccountID
        });
      }
    }
    return (IEnumerable) null;
  }

  public IEnumerable details()
  {
    return CCProcessingHelper.GetPMdetails((PXGraph) this, ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current);
  }

  protected virtual void CustomerPaymentMethod_RowInserting(
    PXCache cache,
    PXRowInsertingEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (row == null)
      return;
    cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>((object) row);
  }

  protected virtual void CustomerPaymentMethod_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.CustomerPaymentMethod row1))
      return;
    int? nullable1 = row1.BAccountID;
    if (nullable1.HasValue)
      this.bAccountID = row1.BAccountID;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Details).Cache, (string) null, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.CustomerPaymentMethod.descr>(cache, (object) row1, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>(cache, (object) row1, string.IsNullOrEmpty(row1.Descr));
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.expirationDate>(cache, (object) row1, false);
    bool flag1 = CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this, ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.PMInstanceID, true);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() & flag1;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = cache.GetStatus((object) row1) == 2 || !flag1;
    PXAction<PX.Objects.AR.CustomerPaymentMethod> validateAddresses = this.validateAddresses;
    bool? nullable2 = row1.HasBillingInfo;
    int num1 = nullable2.GetValueOrDefault() ? 1 : 0;
    ((PXAction) validateAddresses).SetEnabled(num1 != 0);
    if (!string.IsNullOrEmpty(row1.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
      {
        (object) row1.PaymentMethodID
      }));
      nullable2 = row1.IsActive;
      bool flag3 = nullable2.Value && paymentMethod.PaymentType == "CCD" && PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && !string.IsNullOrEmpty(row1.CCProcessingCenterID);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.availableOnPortals>(cache, (object) row1, flag3);
      nullable2 = paymentMethod.ARIsOnePerCustomer;
      bool valueOrDefault = nullable2.GetValueOrDefault();
      bool flag4 = false;
      PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(Array.Empty<object>()));
      if (!valueOrDefault)
      {
        foreach (PXResult<PX.Objects.CA.PaymentMethodDetail> pxResult in ((PXSelectBase<PX.Objects.CA.PaymentMethodDetail>) this.PMDetails).Select(new object[1]
        {
          (object) row1.PaymentMethodID
        }))
        {
          PX.Objects.CA.PaymentMethodDetail paymentMethodDetail = PXResult<PX.Objects.CA.PaymentMethodDetail>.op_Implicit(pxResult);
          nullable2 = paymentMethodDetail.IsIdentifier;
          if (nullable2.GetValueOrDefault() && !string.IsNullOrEmpty(paymentMethodDetail.DisplayMask))
          {
            flag4 = true;
            break;
          }
        }
      }
      if (!(valueOrDefault | flag4 | flag2))
        PXUIFieldAttribute.SetEnabled<PX.Objects.AR.CustomerPaymentMethod.descr>(cache, (object) row1, true);
      bool flag5 = EnumerableExtensions.IsIn<string>(paymentMethod.PaymentType, "EFT", "CCD");
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.displayCardType>(cache, (object) row1, flag5);
      PXCache pxCache = cache;
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = row1;
      int num2;
      if (!flag5)
      {
        nullable2 = paymentMethod.IsAccountNumberRequired;
        num2 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 1;
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.descr>(pxCache, (object) customerPaymentMethod, num2 != 0);
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.CustomerPaymentMethod.descr>(cache, (object) row1, flag4 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
      row1.HasBillingInfo = paymentMethod.ARHasBillingInfo;
      int num3;
      if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>())
      {
        nullable2 = paymentMethod.ARIsProcessingRequired;
        num3 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 0;
      bool flag6 = num3 != 0;
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>(cache, (object) row1, ((flag6 ? 1 : (row1.CCProcessingCenterID != null ? 1 : 0)) & (flag5 ? 1 : 0)) != 0);
      bool flag7 = ((string.IsNullOrEmpty(row1.CCProcessingCenterID) ? 0 : (flag2 ? 1 : (row1.CustomerCCPID != null ? 1 : 0))) & (flag5 ? 1 : 0)) != 0;
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>(cache, (object) row1, flag7);
      if (!string.IsNullOrEmpty(row1.CCProcessingCenterID))
        PXUIFieldAttribute.SetEnabled<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>(cache, (object) row1, flag6 && (!flag1 || string.IsNullOrEmpty(row1.Descr)));
      if (paymentMethod.PaymentType.Equals("EFT"))
        UIState.RaiseOrHideError<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>(cache, (object) row1, true, "By continuing this operation, you confirm that the customer's bank account details were obtained legally and with the permission of the account holder. All payments that will be made using this Customer Payment Method must be authorized by the account holder. If this is not the case, this operation must be terminated.", (PXErrorLevel) 2, (object) paymentMethod);
      bool flag8 = paymentMethod.PaymentType == "CCD";
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.expirationDate>(cache, (object) row1, flag8);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.descr>(cache, (object) row1, false);
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.displayCardType>(cache, (object) row1, false);
    }
    bool flag9 = cache.GetStatus(e.Row) == 2;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>(cache, (object) row1, flag9 || string.IsNullOrEmpty(row1.PaymentMethodID));
    if (!flag9 && !string.IsNullOrEmpty(row1.PaymentMethodID))
    {
      if (!((PXGraph) this).IsContractBasedAPI)
        this.MergeDetailsWithDefinition(row1);
      bool flag10 = ExternalTranHelper.HasTransactions((PXGraph) this, row1.PMInstanceID);
      ((PXSelectBase) this.Details).Cache.AllowDelete = !flag10;
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Details).Cache, (string) null, !flag10);
    }
    nullable1 = row1.BAccountID;
    if (nullable1.HasValue)
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row1.BAccountID
      }));
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod1 = row1;
      nullable1 = row1.BillContactID;
      int num4;
      if (nullable1.HasValue)
      {
        nullable1 = customer.DefBillContactID;
        int? billContactId = row1.BillContactID;
        num4 = nullable1.GetValueOrDefault() == billContactId.GetValueOrDefault() & nullable1.HasValue == billContactId.HasValue ? 1 : 0;
      }
      else
        num4 = 1;
      bool? nullable3 = new bool?(num4 != 0);
      customerPaymentMethod1.IsBillContactSameAsMain = nullable3;
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod2 = row1;
      int? nullable4 = row1.BillAddressID;
      int num5;
      if (nullable4.HasValue)
      {
        nullable4 = customer.DefBillAddressID;
        nullable1 = row1.BillAddressID;
        num5 = nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue ? 1 : 0;
      }
      else
        num5 = 1;
      bool? nullable5 = new bool?(num5 != 0);
      customerPaymentMethod2.IsBillAddressSameAsMain = nullable5;
    }
    nullable1 = row1.CashAccountID;
    if (nullable1.HasValue)
    {
      PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row1.CashAccountID,
        (object) row1.PaymentMethodID
      }));
      PXCache pxCache = cache;
      object row2 = e.Row;
      string str;
      if (paymentMethodAccount != null)
        str = (string) null;
      else
        str = PXMessages.LocalizeFormatNoPrefixNLA("The specified cash account is not configured for use in AR for the {0} payment method.", new object[1]
        {
          (object) row1.PaymentMethodID
        });
      PXUIFieldAttribute.SetWarning<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>(pxCache, row2, str);
    }
    ((PXSelectBase<CCProcessingCenter>) new PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    nullable2 = row1.IsActive;
    bool flag11 = false;
    if (nullable2.GetValueOrDefault() == flag11 & nullable2.HasValue)
    {
      if (PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.GL.Schedule, On<PX.Objects.GL.Schedule.scheduleID, Equal<ARInvoice.scheduleID>, And<PX.Objects.GL.Schedule.active, Equal<True>>>>, Where<ARInvoice.scheduled, Equal<True>, And<ARInvoice.pMInstanceID, Equal<Required<ARInvoice.pMInstanceID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row1.PMInstanceID
      })) != null)
        cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.isActive>((object) row1, (object) row1.IsActive, (Exception) new PXSetPropertyException("This Customer Payment method is inactive, but there are scheduled invoices using it. You need to correct them in order to avoid invoice processing interruptions.", (PXErrorLevel) 2));
      else
        cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.isActive>((object) row1, (object) null, (Exception) null);
    }
    else
      cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.isActive>((object) row1, (object) null, (Exception) null);
    nullable2 = row1.IsActive;
    if (nullable2.GetValueOrDefault())
    {
      bool flag12 = GraphHelper.RowCast<CustomerPaymentMethodDetail>((IEnumerable) ((PXSelectBase<CustomerPaymentMethodDetail>) this.DetailsAll).Select(Array.Empty<object>())).Any<CustomerPaymentMethodDetail>((Func<CustomerPaymentMethodDetail, bool>) (i => !string.IsNullOrEmpty(i.Value)));
      if (!flag9 && !flag12 && !string.IsNullOrEmpty(row1.CCProcessingCenterID) && !string.IsNullOrEmpty(row1.Descr))
        cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.isActive>((object) row1, (object) null, (Exception) new PXSetPropertyException("The customer payment method details were deleted. Integrated processing is not supported.", (PXErrorLevel) 2));
      else
        cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.isActive>((object) row1, (object) null, (Exception) null);
    }
    if (row1.CCProcessingCenterID != null && CCPluginTypeHelper.CheckProcessingCenterPlugin((PXGraph) this, row1.CCProcessingCenterID) != CCPluginCheckResult.Ok)
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Details).Cache, (string) null, false);
    foreach (PXResult<CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<CustomerPaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
    {
      CustomerPaymentMethodDetail paymentMethodDetail = PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult);
      ((PXSelectBase) this.Details).Cache.RaiseRowSelected((object) paymentMethodDetail);
      if (((PXSelectBase) this.Details).Cache.GetStatus((object) paymentMethodDetail) == null)
        ((PXSelectBase) this.Details).Cache.SetStatus((object) paymentMethodDetail, (PXEntryStatus) 5);
    }
  }

  protected virtual void CustomerPaymentMethod_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 3)
    {
      PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
      PX.Objects.CA.PaymentMethod paymentMethod1 = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
      {
        (object) row.PaymentMethodID
      }));
      if (paymentMethod1 != null && paymentMethod1.ARIsOnePerCustomer.GetValueOrDefault() && e.Operation == 2)
      {
        if (PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, NotEqual<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) row.BAccountID,
          (object) row.PaymentMethodID,
          (object) row.PMInstanceID
        })) != null)
          throw new PXException("You cannot add more than one Payment Method of this type for the Customer.");
      }
      if (row == null)
        return;
      if (row.CCProcessingCenterID != null && row.IsActive.GetValueOrDefault())
      {
        switch (CCPluginTypeHelper.CheckProcessingCenterPlugin((PXGraph) this, row.CCProcessingCenterID))
        {
          case CCPluginCheckResult.Empty:
            throw new PXException("Plug-in type is not selected for the {0} processing center.", new object[1]
            {
              (object) row.CCProcessingCenterID
            });
          case CCPluginCheckResult.Missing:
            throw new PXException("The {0} processing center configured for the selected customer payment method references a missing plug-in.", new object[1]
            {
              (object) row.CCProcessingCenterID
            });
          case CCPluginCheckResult.Unsupported:
            throw new PXException("The {0} processing center configured for the selected customer payment method uses an unsupported plug-in.", new object[1]
            {
              (object) row.CCProcessingCenterID
            });
        }
      }
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row.BAccountID
      }));
      if (customer != null && customer.DefPaymentMethodID == row.PaymentMethodID)
      {
        PX.Objects.CA.PaymentMethod paymentMethod2 = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(Array.Empty<object>()));
        if (paymentMethod2 != null && paymentMethod2.ARIsOnePerCustomer.GetValueOrDefault())
        {
          customer.DefPMInstanceID = row.PMInstanceID;
          ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Update(customer);
        }
      }
      if (string.IsNullOrEmpty(row.CustomerCCPID))
        return;
      if (PXResultset<CustomerProcessingCenterID>.op_Implicit(((PXSelectBase<CustomerProcessingCenterID>) this.CustomerProcessingID).Select(new object[1]
      {
        (object) row.CustomerCCPID
      })) != null)
        return;
      ((PXSelectBase<CustomerProcessingCenterID>) this.CustomerProcessingID).Insert(new CustomerProcessingCenterID()
      {
        BAccountID = row.BAccountID,
        CCProcessingCenterID = row.CCProcessingCenterID,
        CustomerCCPID = row.CustomerCCPID
      });
    }
    else
    {
      PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
      if (row == null)
        return;
      PX.Objects.AR.Customer customer1 = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row.BAccountID
      }));
      if (customer1 == null || !(customer1.DefPaymentMethodID == row.PaymentMethodID))
        return;
      int? defPmInstanceId = customer1.DefPMInstanceID;
      int? nullable1 = row.PMInstanceID;
      if (defPmInstanceId.GetValueOrDefault() == nullable1.GetValueOrDefault() & defPmInstanceId.HasValue == nullable1.HasValue)
      {
        PX.Objects.AR.Customer customer2 = customer1;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        customer2.DefPMInstanceID = nullable2;
        customer1.DefPaymentMethodID = (string) null;
        ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Update(customer1);
      }
      else
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
        {
          (object) row.PaymentMethodID
        }));
        if (paymentMethod == null)
          return;
        if (paymentMethod.ARIsOnePerCustomer.GetValueOrDefault())
        {
          customer1.DefPMInstanceID = paymentMethod.PMInstanceID;
          ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Update(customer1);
        }
        else
        {
          if (((IQueryable<PXResult<PX.Objects.AR.CustomerPaymentMethod>>) PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.PaymentMethodID
          })).Any<PXResult<PX.Objects.AR.CustomerPaymentMethod>>())
            return;
          PX.Objects.AR.Customer customer3 = customer1;
          nullable1 = new int?();
          int? nullable3 = nullable1;
          customer3.DefPMInstanceID = nullable3;
          customer1.DefPaymentMethodID = (string) null;
          ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Update(customer1);
        }
      }
    }
  }

  protected virtual void CustomerPaymentMethod_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = e.Row as PX.Objects.AR.CustomerPaymentMethod;
    if (row.IsActive.GetValueOrDefault() || cache.ObjectsEqual<PX.Objects.AR.CustomerPaymentMethod.isActive>(e.OldRow, e.Row))
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
    {
      (object) row.BAccountID
    }));
    if (customer == null)
      return;
    int? defPmInstanceId = customer.DefPMInstanceID;
    int? pmInstanceId = row.PMInstanceID;
    if (!(defPmInstanceId.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & defPmInstanceId.HasValue == pmInstanceId.HasValue))
      return;
    customer.DefPaymentMethodID = (string) null;
    customer.DefPMInstanceID = new int?();
    ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Update(customer);
  }

  protected virtual void CustomerPaymentMethod_BAccountID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (!this.bAccountID.HasValue)
      return;
    e.NewValue = (object) this.bAccountID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CustomerPaymentMethod_PaymentMethodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    this.ClearDetails();
    this.AddDetails(row.PaymentMethodID);
    cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>(e.Row);
    PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
    {
      (object) row.PaymentMethodID
    }));
    bool? nullable;
    if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && paymentMethod != null)
    {
      nullable = paymentMethod.ARIsProcessingRequired;
      if (nullable.GetValueOrDefault())
      {
        cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>(e.Row);
        goto label_4;
      }
    }
    cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>(e.Row, (object) null);
label_4:
    if (paymentMethod != null)
    {
      nullable = paymentMethod.ARIsOnePerCustomer;
      if (nullable.GetValueOrDefault())
      {
        cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) row, (object) paymentMethod.Descr);
        goto label_8;
      }
    }
    cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) row);
label_8:
    if (paymentMethod?.PaymentType == "CCD" && paymentMethod != null)
    {
      nullable = paymentMethod.ARIsProcessingRequired;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.cardType>((object) row, (object) "OTH");
        goto label_14;
      }
    }
    if (paymentMethod?.PaymentType == "EFT")
      cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.cardType>((object) row, (object) "EFT");
    else
      cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.cardType>((object) row);
label_14:
    ((PXSelectBase) this.Details).View.RequestRefresh();
  }

  protected virtual void CustomerPaymentMethod_CCProcessingCenterID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.CustomerPaymentMethod))
      return;
    cache.SetDefaultExt<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>(e.Row);
  }

  protected virtual void CustomerPaymentMethod_CustomerCCPID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.CustomerPaymentMethod row) || row.CustomerCCPID == e.NewValue?.ToString())
      return;
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXViewOf<PX.Objects.AR.CustomerPaymentMethod>.BasedOn<SelectFromBase<PX.Objects.AR.CustomerPaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.CustomerPaymentMethod.bAccountID, NotEqual<P.AsInt>>>>>.And<BqlOperand<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BAccountID,
      e.NewValue
    }));
    if (customerPaymentMethod == null)
      return;
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) customerPaymentMethod.BAccountID
    }));
    cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("The {0} customer profile ID cannot be added to the selected payment method because it is already used for the {1} customer.", new object[3]
    {
      e.NewValue,
      (object) baccount.AcctCD,
      (object) (PXErrorLevel) 4
    }));
    e.NewValue = (object) null;
  }

  protected virtual void CustomerPaymentMethod_Descr_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
    {
      (object) row.PaymentMethodID
    })).ARIsOnePerCustomer.GetValueOrDefault())
      return;
    if (PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, NotEqual<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>, And<PX.Objects.AR.CustomerPaymentMethod.descr, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.descr>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) row.BAccountID,
      (object) row.PaymentMethodID,
      (object) row.PMInstanceID,
      (object) row.Descr
    })) == null)
      return;
    cache.RaiseExceptionHandling<PX.Objects.AR.CustomerPaymentMethod.descr>((object) row, (object) row.Descr, (Exception) new PXSetPropertyException("A card with this card number is already registered for the customer.", (PXErrorLevel) 2));
  }

  protected virtual void CustomerPaymentMethod_IsBillAddressSameAsMain_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (row == null)
      return;
    if (row.IsBillAddressSameAsMain.GetValueOrDefault())
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row.BAccountID
      }));
      int? nullable1 = row.BillAddressID;
      if (nullable1.HasValue)
      {
        PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Select(new object[1]
        {
          (object) row.BillAddressID
        }));
        if (address != null)
        {
          nullable1 = address.AddressID;
          int? nullable2 = customer.DefBillAddressID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            nullable2 = address.AddressID;
            nullable1 = customer.DefAddressID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              ((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Delete(address);
          }
        }
      }
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = row;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      customerPaymentMethod.BillAddressID = nullable3;
    }
    else
    {
      int? nullable = row.BillAddressID;
      if (!nullable.HasValue)
      {
        nullable = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
        {
          (object) row.BAccountID
        })).DefBillAddressID;
        nullable = new int?();
      }
      PX.Objects.CR.Address address1 = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Select(new object[1]
      {
        (object) nullable
      }));
      if (address1 == null)
        return;
      PX.Objects.CR.Address copy = (PX.Objects.CR.Address) ((PXSelectBase) this.BillAddress).Cache.CreateCopy((object) address1);
      copy.AddressID = new int?();
      PX.Objects.CR.Address address2 = ((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Insert(copy);
      row.BillAddressID = address2.AddressID;
    }
  }

  protected virtual void CustomerPaymentMethod_IsBillContactSameAsMain_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (row == null)
      return;
    if (row.IsBillContactSameAsMain.GetValueOrDefault())
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row.BAccountID
      }));
      int? nullable1 = row.BillContactID;
      if (nullable1.HasValue)
      {
        PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Select(new object[1]
        {
          (object) row.BillContactID
        }));
        if (contact != null)
        {
          nullable1 = contact.ContactID;
          int? nullable2 = customer.DefBillContactID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            nullable2 = contact.ContactID;
            nullable1 = customer.DefContactID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              ((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Delete(contact);
          }
        }
      }
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = row;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      customerPaymentMethod.BillContactID = nullable3;
    }
    else
    {
      int? nullable = row.BillContactID;
      if (!nullable.HasValue)
        nullable = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
        {
          (object) row.BAccountID
        })).DefBillContactID;
      PX.Objects.CR.Contact contact1 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Select(new object[1]
      {
        (object) nullable
      }));
      if (contact1 == null)
        return;
      PX.Objects.CR.Contact copy = (PX.Objects.CR.Contact) ((PXSelectBase) this.BillContact).Cache.CreateCopy((object) contact1);
      copy.ContactID = new int?();
      PX.Objects.CR.Contact contact2 = ((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Insert(copy);
      row.BillContactID = contact2.ContactID;
    }
  }

  protected virtual void CustomerPaymentMethod_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    PX.Objects.AR.CustomerPaymentMethod row = (PX.Objects.AR.CustomerPaymentMethod) e.Row;
    if (row == null)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
    {
      (object) row.BAccountID
    }));
    ((PXSelectBase) this.CustomerPaymentMethod).Cache.GetStatus(e.Row);
    ARPayment payment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelectReadonly<ARPayment, Where<ARPayment.customerID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<ARPayment.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BAccountID,
      (object) row.PMInstanceID
    }));
    if (payment != null)
    {
      if (CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this, row.PMInstanceID, true) || row.CCProcessingCenterID == null)
      {
        string violationMessage = this.GetReferentialIntegrityViolationMessage(row, payment);
        PXTrace.WriteWarning(violationMessage);
        throw new PXException(violationMessage);
      }
      string str;
      new ARDocType.ListAttribute().ValueLabelDic.TryGetValue(payment.DocType, out str);
      if (((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CurrentCPM).Ask(PXMessages.LocalizeFormatNoPrefix("This customer payment method is used in at least one document: {0} {1}. Are you sure you want to delete it?", new object[2]
      {
        (object) str,
        (object) payment.RefNbr
      }), (MessageButtons) 4) != 6)
      {
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    int? nullable1 = row.BillContactID;
    int? nullable2;
    if (nullable1.HasValue)
    {
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Select(new object[1]
      {
        (object) row.BillContactID
      }));
      if (contact != null)
      {
        nullable1 = contact.ContactID;
        int? defBillContactId = customer.DefBillContactID;
        if (!(nullable1.GetValueOrDefault() == defBillContactId.GetValueOrDefault() & nullable1.HasValue == defBillContactId.HasValue))
        {
          nullable2 = contact.ContactID;
          nullable1 = customer.DefContactID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            ((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).Delete(contact);
        }
      }
    }
    nullable1 = row.BillAddressID;
    if (!nullable1.HasValue)
      return;
    PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Select(new object[1]
    {
      (object) row.BillAddressID
    }));
    if (address == null)
      return;
    nullable1 = address.AddressID;
    nullable2 = customer.DefBillAddressID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    nullable2 = address.AddressID;
    nullable1 = customer.DefAddressID;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      return;
    ((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Delete(address);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.AR.CustomerPaymentMethod.cashAccountID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, object, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, object, object>) e).NewValue is int newValue) || string.IsNullOrEmpty(e.Row is PX.Objects.AR.CustomerPaymentMethod row ? row.CCProcessingCenterID : (string) null))
      return;
    CCProcessingCenter processingCenter = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CCProcessingCenterID
    }));
    if (!((int?) processingCenter?.CashAccountID).HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount1 = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, processingCenter.CashAccountID);
    PX.Objects.CA.CashAccount cashAccount2 = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, new int?(newValue));
    if (!string.IsNullOrEmpty(cashAccount1.CuryID) && !string.IsNullOrEmpty(cashAccount2.CuryID) && !cashAccount1.CuryID.Equals(cashAccount2.CuryID))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, object, object>) e).NewValue = (object) cashAccount2.CashAccountCD;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>>) e).Cancel = true;
      throw new PXSetPropertyException<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>("The currency of the {0} processing center ({1}) differs from the currency of the {2} cash account ({3}). Select a cash account denominated in {1} to process transactions with the {0} processing center.", new object[5]
      {
        (object) processingCenter.ProcessingCenterID,
        (object) cashAccount1.CuryID,
        (object) cashAccount2.CashAccountCD,
        (object) cashAccount2.CuryID,
        (object) (PXErrorLevel) 4
      });
    }
  }

  protected virtual void CustomerPaymentMethodDetail_RowDeleted(
    PXCache cache,
    PXRowDeletedEventArgs e)
  {
    PX.Objects.CA.PaymentMethodDetail template = this.FindTemplate((CustomerPaymentMethodDetail) e.Row);
    if (template == null || !template.IsIdentifier.GetValueOrDefault())
      return;
    ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.Descr = (string) null;
  }

  protected virtual void CustomerPaymentMethodDetailRowPersisting(
    PX.Data.Events.RowPersisting<CustomerPaymentMethodDetail> e)
  {
    if (e.Row == null)
      return;
    CustomerPaymentMethodDetail row = e.Row;
    PX.Objects.CA.PaymentMethodDetail template = this.FindTemplate(row);
    if (template == null || !template.IsRequired.GetValueOrDefault() || !string.IsNullOrWhiteSpace(row.Value))
      return;
    string paymentMethodId = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current?.PaymentMethodID;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CustomerPaymentMethodDetail>>) e).Cache.RaiseExceptionHandling("value", (object) e.Row, (object) null, (Exception) new PXSetPropertyException("The {0} detail is marked as required for customer payment methods of the {1} payment method but cannot be filled in automatically. Review the settings of the {1} payment method.", new object[2]
    {
      (object) row.DetailID,
      (object) paymentMethodId
    }));
  }

  protected virtual void CustomerPaymentMethodDetail_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    CustomerPaymentMethodDetail row = (CustomerPaymentMethodDetail) e.Row;
    if (row == null || string.IsNullOrEmpty(row.PaymentMethodID))
      return;
    PX.Objects.CA.PaymentMethodDetail template = this.FindTemplate(row);
    if (template != null)
    {
      bool? nullable = template.IsRequired;
      bool valueOrDefault = nullable.GetValueOrDefault();
      PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethodDetail.value>(cache, (object) row, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      nullable = template.IsEncrypted;
      bool flag = !nullable.GetValueOrDefault();
      PXDBCryptStringAttribute.SetDecrypted<CustomerPaymentMethodDetail.value>(cache, (object) row, flag);
    }
    else
      PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethodDetail.value>(cache, (object) row, (PXPersistingCheck) 2);
  }

  protected virtual void CustomerPaymentMethodDetail_Value_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    CustomerPaymentMethodDetail row = e.Row as CustomerPaymentMethodDetail;
    PX.Objects.CA.PaymentMethodDetail template = this.FindTemplate(row);
    if (template == null || !template.IsCCProcessingID.GetValueOrDefault())
      return;
    PXResult<CustomerPaymentMethodDetail> pxResult = ((IEnumerable<PXResult<CustomerPaymentMethodDetail>>) PXGraph.CreateInstance<CCCustomerInformationManagerGraph>().GetAllCustomersCardsInProcCenter((PXGraph) this, ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.BAccountID, ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.CCProcessingCenterID)).AsEnumerable<PXResult<CustomerPaymentMethodDetail>>().FirstOrDefault<PXResult<CustomerPaymentMethodDetail>>((Func<PXResult<CustomerPaymentMethodDetail>, bool>) (result => PXResult<CustomerPaymentMethodDetail>.op_Implicit(result).Value == (string) e.NewValue));
    if (pxResult != null)
    {
      PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult);
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = ((PXResult) pxResult).GetItem<PX.Objects.AR.CustomerPaymentMethod>();
      throw new PXSetPropertyException("The Token ID {0} cannot be added to the selected payment method because it is already used in another customer payment method ({1})", new object[2]
      {
        (object) row.Value,
        (object) customerPaymentMethod.Descr
      });
    }
  }

  protected virtual void CustomerPaymentMethodDetail_Value_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CustomerPaymentMethodDetail row = e.Row as CustomerPaymentMethodDetail;
    PX.Objects.CA.PaymentMethodDetail template = this.FindTemplate(row);
    if (template == null)
      return;
    bool? nullable = template.IsIdentifier;
    if (nullable.GetValueOrDefault())
    {
      string aMaskedID = !CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this, ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.PMInstanceID, true) ? this.CCDisplayMaskService.UseDisplayMaskForCardNumber(row.Value, template.DisplayMask) : this.CCDisplayMaskService.UseAdjustedDisplayMaskForCardNumber(row.Value, template.DisplayMask);
      if (!((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.Descr.Contains(aMaskedID))
      {
        PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
        ((PXSelectBase) this.CustomerPaymentMethod).Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) current, (object) CustomerPaymentMethodMaint.FormatDescription(current.CardType ?? "OTH", aMaskedID));
        ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Update(current);
      }
    }
    nullable = template.IsExpirationDate;
    if (!nullable.GetValueOrDefault() || string.IsNullOrEmpty(row.Value))
      return;
    PX.Objects.AR.CustomerPaymentMethod current1 = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
    ((PXSelectBase) this.CustomerPaymentMethod).Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.expirationDate>((object) current1, (object) CustomerPaymentMethodMaint.ParseExpiryDate((PXGraph) this, current1, row.Value));
    ((PXSelectBase) this.CustomerPaymentMethod).Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.lastNotificationDate>((object) current1, (object) null);
    ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Update(current1);
  }

  protected virtual void Address_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.Address row = (PX.Objects.CR.Address) e.Row;
    bool flag1 = false;
    if (row != null)
    {
      PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
      if (current != null)
      {
        int? billAddressId = current.BillAddressID;
        int? addressId = row.AddressID;
        if (billAddressId.GetValueOrDefault() == addressId.GetValueOrDefault() & billAddressId.HasValue == addressId.HasValue)
        {
          bool? addressSameAsMain = current.IsBillAddressSameAsMain;
          bool flag2 = false;
          if (addressSameAsMain.GetValueOrDefault() == flag2 & addressSameAsMain.HasValue)
            flag1 = true;
        }
      }
    }
    PXUIFieldAttribute.SetEnabled(cache, (object) row, (string) null, flag1);
  }

  protected virtual void Contact_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.Contact row = (PX.Objects.CR.Contact) e.Row;
    bool flag1 = false;
    if (row != null)
    {
      PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
      if (current != null)
      {
        int? billContactId = current.BillContactID;
        int? contactId = row.ContactID;
        if (billContactId.GetValueOrDefault() == contactId.GetValueOrDefault() & billContactId.HasValue == contactId.HasValue)
        {
          bool? contactSameAsMain = current.IsBillContactSameAsMain;
          bool flag2 = false;
          if (contactSameAsMain.GetValueOrDefault() == flag2 & contactSameAsMain.HasValue)
            flag1 = true;
        }
      }
    }
    PXUIFieldAttribute.SetEnabled(cache, (object) row, (string) null, flag1);
  }

  protected virtual PX.Objects.CA.PaymentMethodDetail FindTemplate(CustomerPaymentMethodDetail aDet)
  {
    return PXResultset<PX.Objects.CA.PaymentMethodDetail>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethodDetail, PXSelect<PX.Objects.CA.PaymentMethodDetail, Where<PX.Objects.CA.PaymentMethodDetail.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethodDetail.paymentMethodID>>, And<PX.Objects.CA.PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PX.Objects.CA.PaymentMethodDetail.detailID, Equal<Required<PX.Objects.CA.PaymentMethodDetail.detailID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aDet.PaymentMethodID,
      (object) aDet.DetailID
    }));
  }

  protected virtual void ClearDetails()
  {
    foreach (PXResult<CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<CustomerPaymentMethodDetail>) this.DetailsAll).Select(Array.Empty<object>()))
      ((PXSelectBase<CustomerPaymentMethodDetail>) this.DetailsAll).Delete(PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult));
  }

  protected virtual void AddDetails(string aPaymentMethodID)
  {
    if (string.IsNullOrEmpty(aPaymentMethodID))
      return;
    foreach (PXResult<PX.Objects.CA.PaymentMethodDetail> pxResult in ((PXSelectBase<PX.Objects.CA.PaymentMethodDetail>) this.PMDetails).Select(new object[1]
    {
      (object) aPaymentMethodID
    }))
    {
      PX.Objects.CA.PaymentMethodDetail it = PXResult<PX.Objects.CA.PaymentMethodDetail>.op_Implicit(pxResult);
      if (!this.SkipPaymentProfileDetail(it))
        ((PXSelectBase<CustomerPaymentMethodDetail>) this.Details).Insert(new CustomerPaymentMethodDetail()
        {
          DetailID = it.DetailID
        });
    }
  }

  protected virtual void MergeDetailsWithDefinition(PX.Objects.AR.CustomerPaymentMethod row)
  {
    string paymentMethodId = row.PaymentMethodID;
    if (!(paymentMethodId != this.mergedPaymentMethod))
      return;
    List<PX.Objects.CA.PaymentMethodDetail> paymentMethodDetailList = new List<PX.Objects.CA.PaymentMethodDetail>();
    foreach (PXResult<PX.Objects.CA.PaymentMethodDetail> pxResult1 in ((PXSelectBase<PX.Objects.CA.PaymentMethodDetail>) this.PMDetails).Select(new object[1]
    {
      (object) paymentMethodId
    }))
    {
      PX.Objects.CA.PaymentMethodDetail it = PXResult<PX.Objects.CA.PaymentMethodDetail>.op_Implicit(pxResult1);
      if (!this.SkipPaymentProfileDetail(it))
      {
        CustomerPaymentMethodDetail paymentMethodDetail1 = (CustomerPaymentMethodDetail) null;
        foreach (PXResult<CustomerPaymentMethodDetail> pxResult2 in ((PXSelectBase<CustomerPaymentMethodDetail>) this.Details).Select(Array.Empty<object>()))
        {
          CustomerPaymentMethodDetail paymentMethodDetail2 = PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult2);
          if (paymentMethodDetail2.DetailID == it.DetailID)
          {
            paymentMethodDetail1 = paymentMethodDetail2;
            break;
          }
        }
        if (paymentMethodDetail1 == null && (!(it.DetailID == "CVV") || !row.CVVVerifyTran.HasValue))
          paymentMethodDetailList.Add(it);
      }
    }
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.Details).Cache
    }))
    {
      foreach (PX.Objects.CA.PaymentMethodDetail paymentMethodDetail in paymentMethodDetailList)
        ((PXSelectBase<CustomerPaymentMethodDetail>) this.Details).Insert(new CustomerPaymentMethodDetail()
        {
          DetailID = paymentMethodDetail.DetailID
        });
      if (paymentMethodDetailList.Count > 0)
        ((PXSelectBase) this.Details).View.RequestRefresh();
    }
    this.mergedPaymentMethod = paymentMethodId;
  }

  protected virtual bool SkipPaymentProfileDetail(PX.Objects.CA.PaymentMethodDetail it)
  {
    PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
    {
      (object) it.PaymentMethodID
    }));
    bool? nullable;
    int num1;
    if (paymentMethod.PaymentType == "CCD")
    {
      nullable = paymentMethod.ARIsProcessingRequired;
      if (nullable.GetValueOrDefault())
      {
        nullable = paymentMethod.UseForAR;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool flag = num1 != 0;
    int num2;
    if (!PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>())
    {
      nullable = it.IsCCProcessingID;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    int num3 = flag ? 1 : 0;
    return (num2 & num3) != 0;
  }

  public string Caption()
  {
    PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current;
    PXResultset<PX.Objects.AR.Customer> pxResultset;
    if (current == null)
      pxResultset = (PXResultset<PX.Objects.AR.Customer>) null;
    else
      pxResultset = ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) current.BAccountID
      });
    string acctCd = PXResultset<PX.Objects.AR.Customer>.op_Implicit(pxResultset)?.AcctCD;
    string str = current != null ? $"{current.Descr} {current.ExpirationDate:MM/yy}".Trim() : (string) null;
    if (string.IsNullOrWhiteSpace(str))
      return acctCd ?? PXMessages.Localize("New Record");
    return acctCd != null ? $"{acctCd} - {str}" : str;
  }

  public static string FormatDescription(string cardType, string aMaskedID)
  {
    return !string.IsNullOrEmpty(cardType) ? $"{CardType.GetDisplayName(cardType)}:{aMaskedID}" : aMaskedID;
  }

  public static DateTime? ParseExpiryDate(PXGraph graph, PX.Objects.AR.CustomerPaymentMethod cpm, string aValue)
  {
    return CustomerPaymentMethodMaint.ParseExpiryDate(graph, (ICCPaymentProfile) cpm, aValue);
  }

  public static DateTime? ParseExpiryDate(PXGraph graph, ICCPaymentProfile cpm, string aValue)
  {
    return CustomerPaymentMethodMaint.ParseExpiryDate(graph, cpm?.CCProcessingCenterID, aValue);
  }

  public static DateTime? ParseExpiryDate(PXGraph graph, string procCenterId, string value)
  {
    string format = (string) null;
    if (graph != null && procCenterId != null)
      format = CCProcessingHelper.GetExpirationDateFormat(graph, procCenterId);
    DateTime result;
    return !(string.IsNullOrEmpty(format) ? DateTime.TryParseExact(value, "Myyyy", (IFormatProvider) null, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "Myy", (IFormatProvider) null, DateTimeStyles.None, out result) : DateTime.TryParseExact(value, format, (IFormatProvider) null, DateTimeStyles.None, out result)) ? new DateTime?() : new DateTime?(result);
  }

  private string GetReferentialIntegrityViolationMessage(
    PX.Objects.AR.CustomerPaymentMethod row,
    ARPayment payment)
  {
    return PXLocalizer.LocalizeFormat("{0} cannot be deleted because it is referenced in the following record: {1}.", new object[2]
    {
      (object) this.GetRecordInfo((object) row),
      (object) this.GetRecordInfo((object) payment)
    });
  }

  private string GetRecordInfo(object entity)
  {
    return $"{EntityHelper.GetFriendlyEntityName(entity.GetType(), entity)} ({new EntityHelper((PXGraph) this).GetEntityRowID(entity.GetType(), entity, ", ")})";
  }

  [Obsolete]
  public static class IDObfuscator
  {
    private const char CS_UNDERSCORE = '_';
    private const char CS_DASH = '-';
    private const char CS_DOT = '.';
    private const char CS_MASKER = '*';
    private const char CS_NUMBER_MASK_0 = '#';
    private const char CS_NUMBER_MASK_1 = '0';
    private const char CS_NUMBER_MASK_2 = '9';
    private const char CS_ANY_CHAR_0 = '&';
    private const char CS_ANY_CHAR_1 = 'C';
    private const char CS_ALPHANUMBER_MASK_0 = 'a';
    private const char CS_ALPHANUMBER_MASK_1 = 'A';
    private const char CS_ALPHA_MASK_0 = 'L';
    private const char CS_ALPHA_MASK_1 = '?';

    [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
    public static string MaskID(string aID, string aEditMask, string aDisplayMask)
    {
      if (string.IsNullOrEmpty(aID) || string.IsNullOrWhiteSpace(aDisplayMask))
        return aID;
      if (string.IsNullOrEmpty(aEditMask))
        return CustomerPaymentMethodMaint.IDObfuscator.MaskID(aID, aDisplayMask);
      int length1 = aEditMask.Length;
      int length2 = aDisplayMask.Length;
      int length3 = aID.Length;
      char[] charArray1 = aEditMask.ToCharArray();
      char[] charArray2 = aDisplayMask.ToCharArray();
      char[] charArray3 = aID.ToCharArray();
      int index1 = 0;
      int index2 = 0;
      StringBuilder stringBuilder = new StringBuilder(length1);
      for (int index3 = 0; index3 < length1 && index1 < length3; ++index3)
      {
        if (index2 >= length2)
          stringBuilder.Append('*');
        else if (CustomerPaymentMethodMaint.IDObfuscator.IsSymbol(charArray1[index3]))
        {
          if (CustomerPaymentMethodMaint.IDObfuscator.IsSymbol(charArray2[index2]))
            stringBuilder.Append(charArray3[index1]);
          else
            stringBuilder.Append('*');
          ++index1;
          ++index2;
        }
        else if (CustomerPaymentMethodMaint.IDObfuscator.IsSeparator(charArray1[index3]) && CustomerPaymentMethodMaint.IDObfuscator.IsSeparator(charArray2[index2]))
        {
          stringBuilder.Append(charArray2[index2]);
          ++index2;
        }
      }
      return stringBuilder.ToString();
    }

    [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
    public static string MaskID(string aID, string aDisplayMask)
    {
      if (string.IsNullOrEmpty(aID) || string.IsNullOrEmpty(aDisplayMask) || string.IsNullOrEmpty(aDisplayMask.Trim()))
        return aID;
      int length1 = aDisplayMask.Length;
      int length2 = aID.Length;
      char[] charArray1 = aDisplayMask.ToCharArray();
      char[] charArray2 = aID.ToCharArray();
      int index1 = 0;
      StringBuilder stringBuilder = new StringBuilder(length1);
      for (int index2 = 0; index2 < length1 && index1 < length2; ++index2)
      {
        if (CustomerPaymentMethodMaint.IDObfuscator.IsSymbol(charArray1[index2]))
        {
          stringBuilder.Append(charArray2[index1]);
          ++index1;
        }
        else if (CustomerPaymentMethodMaint.IDObfuscator.IsSeparator(charArray1[index2]))
        {
          stringBuilder.Append(charArray1[index2]);
        }
        else
        {
          stringBuilder.Append('*');
          ++index1;
        }
      }
      return stringBuilder.ToString();
    }

    [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
    public static string AdjustedMaskID(string aID, string aDisplayMask)
    {
      string aID1 = aID;
      int totalWidth = ((IEnumerable<char>) aDisplayMask.ToArray<char>()).Where<char>((Func<char, bool>) (symbol => !CustomerPaymentMethodMaint.IDObfuscator.IsSeparator(symbol))).Count<char>();
      if (aID.Length > totalWidth)
        aID1 = aID.Substring(aID.Length - totalWidth);
      else if (aID.Length < totalWidth)
        aID1 = aID.PadLeft(totalWidth, '0');
      return CustomerPaymentMethodMaint.IDObfuscator.MaskID(aID1, aDisplayMask);
    }

    public static string GetMaskByID(
      CCPaymentHelperGraph graph,
      string aID,
      string aDisplayMask,
      int? PMInstanceID)
    {
      return CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) graph, PMInstanceID, true) ? graph.CCDisplayMaskService.UseAdjustedDisplayMaskForCardNumber(aID, aDisplayMask) : graph.CCDisplayMaskService.UseDisplayMaskForCardNumber(aID, aDisplayMask);
    }

    public static string RestoreToMasked(
      string aValue,
      string aDisplayMask,
      string aMissedValuePlaceholder,
      bool aRemoveSeparators)
    {
      return CustomerPaymentMethodMaint.IDObfuscator.RestoreToMasked(aValue, aDisplayMask, aMissedValuePlaceholder, aRemoveSeparators, false, '*'.ToString(), false);
    }

    public static string RestoreToMasked(
      string aValue,
      string aDisplayMask,
      string aMissedValuePlaceholder,
      bool aSkipSeparators,
      bool aReplaceMaskChars,
      string aNewMasker,
      bool aMergeNonValue)
    {
      char[] chArray1 = aDisplayMask != null ? aDisplayMask.ToCharArray() : string.Empty.ToCharArray();
      char[] chArray2 = aValue != null ? aValue.ToCharArray() : string.Empty.ToCharArray();
      int index1 = 0;
      int length1 = aDisplayMask != null ? aDisplayMask.Length : 0;
      int length2 = aValue != null ? aValue.Length : 0;
      StringBuilder stringBuilder = new StringBuilder(length1);
      bool flag = false;
      for (int index2 = 0; index2 < length1; ++index2)
      {
        if (CustomerPaymentMethodMaint.IDObfuscator.IsSymbol(chArray1[index2]))
        {
          if (index1 < length2 && !char.IsWhiteSpace(chArray2[index1]))
            stringBuilder.Append(chArray2[index1]);
          else
            stringBuilder.Append(aMissedValuePlaceholder);
          ++index1;
          flag = true;
        }
        else if (!aSkipSeparators || CustomerPaymentMethodMaint.IDObfuscator.IsMasked(chArray1[index2]))
        {
          if (aReplaceMaskChars)
          {
            if (!aMergeNonValue | flag)
              stringBuilder.Append(aNewMasker);
          }
          else
            stringBuilder.Append(chArray1[index2]);
          flag = false;
        }
      }
      return stringBuilder.ToString();
    }

    private static bool IsSeparator(char aCh) => aCh == '_' || aCh == '-' || aCh == '.';

    private static bool IsMasked(char aCh) => aCh == '*';

    private static bool IsSymbol(char aCh)
    {
      switch (aCh)
      {
        case '#':
        case '&':
        case '0':
        case '9':
        case '?':
        case 'A':
        case 'C':
        case 'L':
        case 'a':
          return true;
        default:
          return false;
      }
    }
  }

  public class PaymentProfileHostedForm : 
    PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>
  {
    protected override PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.CustomerPaymentMethodMapping GetCustomerPaymentMethodMapping()
    {
      return new PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.CustomerPaymentMethodMapping(typeof (PX.Objects.AR.CustomerPaymentMethod));
    }

    protected override PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.CustomerPaymentMethodDetailMapping GetCusotmerPaymentMethodDetailMapping()
    {
      return new PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.CustomerPaymentMethodDetailMapping(typeof (CustomerPaymentMethodDetail));
    }

    protected override PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.PaymentmethodDetailMapping GetPaymentMethodDetailMapping()
    {
      return new PaymentProfileGraph<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod>.PaymentmethodDetailMapping(typeof (PX.Objects.CA.PaymentMethodDetail));
    }

    protected override void RowSelected(PX.Data.Events.RowSelected<PX.Objects.AR.CustomerPaymentMethod> e)
    {
      base.RowSelected(e);
      PX.Objects.AR.CustomerPaymentMethod row = e.Row;
      if (row == null)
        return;
      int? pmInstanceId = ((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>) this.CustomerPaymentMethod).Current.PMInstanceID;
      bool visible = CCProcessingHelper.IsHFPaymentMethod((PXGraph) this.Base, pmInstanceId, false);
      bool flag = CCProcessingHelper.IsCCPIDFilled((PXGraph) this.Base, pmInstanceId);
      this.RefreshCreatePaymentAction(visible && !flag, visible);
      this.RefreshSyncPaymentAction(true);
      this.RefreshManagePaymentAction(visible && !string.IsNullOrEmpty(row.Descr), visible);
    }

    protected override void MapViews(CustomerPaymentMethodMaint graph)
    {
      this.CustomerPaymentMethodDetail = new PXSelectExtension<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail>((PXSelectBase) graph.DetailsAll);
    }
  }

  /// <exclude />
  public class CustomerPaymentMethodMaintAddressLookupExtension : 
    AddressLookupExtension<CustomerPaymentMethodMaint, PX.Objects.AR.CustomerPaymentMethod, PX.Objects.CR.Address>
  {
    protected override string AddressView => "BillAddress";

    protected override string ViewOnMap => "viewBillAddressOnMap";
  }
}
