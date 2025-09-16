// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaintExternalECMExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.SM;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaintExternalECMExt : PXGraphExtension<CustomerMaint>
{
  public PXSelect<TXSetup> TXSetupRecord;
  public PXFilter<ExemptCustomerFilter> CreateCustomerFilter;
  public PXFilter<RequestECMCertificateFilter> RequestCertificateFilter;
  [PXNotCleanable]
  [PXReadOnlyView]
  public PXSelect<ExemptionCertificate> ExemptionCertificates;
  [PXReadOnlyView]
  public PXSelect<CertificateTemplate> CertificateTemplates;
  [PXReadOnlyView]
  public PXSelect<CertificateReason> CertificateReasons;
  public PXAction<Customer> retrieveCertificate;
  public PXAction<Customer> requestCertificate;
  public PXAction<Customer> createCustomerInECM;
  public PXAction<Customer> updateCustomerInECM;
  public PXAction<Customer> refreshCertificates;
  public PXAction<ExemptionCertificate> viewCertificate;

  public virtual IEnumerable exemptionCertificates()
  {
    return ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current == null ? (IEnumerable) Array.Empty<ExemptionCertificate>() : ((PXSelectBase) this.ExemptionCertificates).Cache.Cached;
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.eCM>();

  protected virtual void _(PX.Data.Events.RowUpdated<Customer> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<Customer>>) e).Cache.ObjectsEqual<Customer.acctName>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase<Customer>) this.Base.BAccount).Current.IsECMValid = new bool?(false);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RetrieveCertificate(PXAdapter adapter)
  {
    this.ClearExemptionCertificateCache(((PXSelectBase) this.ExemptionCertificates).Cache);
    if (((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current != null)
    {
      this.GetExemptionCertificates(((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current);
      ((PXSelectBase<ExemptionCertificate>) this.ExemptionCertificates).AskExt();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RequestCertificate(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current != null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass12_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.customer = ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current;
      this.GetCertificateTemplates();
      this.GetCertificateReasons();
      PXCache cache = ((PXSelectBase) this.RequestCertificateFilter).Cache;
      if (((PXSelectBase<RequestECMCertificateFilter>) this.RequestCertificateFilter).AskExt() != 1)
        return adapter.Get();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.RequestCertificateParams = ((PXSelectBase<RequestECMCertificateFilter>) this.RequestCertificateFilter).Current;
      // ISSUE: reference to a compiler-generated field
      if (this.CheckForEmptyFields(cache, cDisplayClass120.RequestCertificateParams))
        return adapter.Get();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass120, __methodptr(\u003CRequestCertificate\u003Eb__0)));
    }
    ((PXSelectBase) this.RequestCertificateFilter).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateCustomerInECM(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass14_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.customer = ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass140.customer != null)
    {
      PXResultset<TaxPluginMapping> source = PXSelectBase<TaxPluginMapping, PXViewOf<TaxPluginMapping>.BasedOn<SelectFromBase<TaxPluginMapping, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxPlugin>.On<BqlOperand<TaxPluginMapping.taxPluginID, IBqlString>.IsEqual<TaxPlugin.taxPluginID>>>, FbqlJoins.Inner<TXSetup>.On<BqlOperand<TaxPlugin.taxPluginID, IBqlString>.IsEqual<TXSetup.eCMProvider>>>>.Aggregate<To<GroupBy<TaxPluginMapping.companyCode>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.companyCode = source.Count == 1 ? PXResult<TaxPluginMapping>.op_Implicit(source[0]).CompanyCode : string.Empty;
      if (((IQueryable<PXResult<TaxPluginMapping>>) source).Count<PXResult<TaxPluginMapping>>() > 1 && ((PXSelectBase<ExemptCustomerFilter>) this.CreateCustomerFilter).AskExt(true) == 1)
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass140.companyCode = ((PXSelectBase<ExemptCustomerFilter>) this.CreateCustomerFilter).Current.CompanyCode;
      }
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CCreateCustomerInECM\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UpdateCustomerInECM(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass16_0 cDisplayClass160 = new CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass16_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass160.customer = ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass160.customer != null)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass160, __methodptr(\u003CUpdateCustomerInECM\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable RefreshCertificates(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current != null)
    {
      this.ClearExemptionCertificateCache(((PXSelectBase) this.ExemptionCertificates).Cache);
      this.GetExemptionCertificates(((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCertificate(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass20_0 cDisplayClass200 = new CustomerMaintExternalECMExt.\u003C\u003Ec__DisplayClass20_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass200.certificate = ((PXSelectBase<ExemptionCertificate>) this.ExemptionCertificates).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass200.certificate != null)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass200, __methodptr(\u003CViewCertificate\u003Eb__0)));
    }
    return adapter.Get();
  }

  public void ClearExemptionCertificateCache(PXCache certificateCache)
  {
    foreach (ExemptionCertificate exemptionCertificate in certificateCache.Cached.Cast<ExemptionCertificate>().ToList<ExemptionCertificate>())
      certificateCache.Remove((object) exemptionCertificate);
  }

  public void GetExemptionCertificates(Customer customer)
  {
    if (customer == null)
      return;
    string[] strArray1;
    if (string.IsNullOrEmpty(customer.ECMCompanyCode))
      strArray1 = Array.Empty<string>();
    else
      strArray1 = ((IEnumerable<string>) customer.ECMCompanyCode.Split(',')).Select<string, string>((Func<string, string>) (p => p.Trim())).ToArray<string>();
    string[] strArray2 = strArray1;
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
    if (txSetup == null)
      return;
    try
    {
      IExemptionCertificateProvider ecmProvider = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider);
      foreach (string companyCode in strArray2)
      {
        string companyId = this.CompanyIdForCompanyCode(companyCode);
        GetCertificatesRequest certificatesRequest = this.BuildGetCertificatesRequest(customer, companyId);
        GetCertificatesResult certificates = ecmProvider.GetCertificates(certificatesRequest);
        if (((ResultBase) certificates).IsSuccess)
        {
          foreach (CertificateDetail certificate in certificates.Certificates)
            ((PXSelectBase<ExemptionCertificate>) this.ExemptionCertificates).Insert(new ExemptionCertificate()
            {
              CertificateID = certificate.CertificateID,
              ECMCompanyID = certificate.CompanyId,
              CompanyCode = companyCode,
              ExemptionReason = certificate.ExemptionReason,
              State = certificate.State,
              Status = certificate.Status,
              EffectiveDate = new DateTime?(certificate.SignedDate),
              ExpirationDate = new DateTime?(certificate.ExpirationDate)
            });
        }
        else
          this.LogMessages((ResultBase) certificates);
      }
    }
    catch (Exception ex)
    {
      throw new PXException(ex.Message.ToString());
    }
  }

  public virtual GetCertificatesRequest BuildGetCertificatesRequest(
    Customer customer,
    string companyId)
  {
    if (customer == null)
      throw new PXArgumentException(nameof (customer), "The argument cannot be null.");
    return new GetCertificatesRequest()
    {
      CompanyId = companyId,
      CustomerCode = customer.AcctCD
    };
  }

  public virtual void GetCertificateTemplates()
  {
    IEnumerable cached = ((PXGraph) this.Base).Caches[typeof (CertificateTemplate)].Cached;
    if (cached != null && cached.Count() != 0L)
      return;
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
    if (txSetup == null)
      return;
    GetCoverLettersResult coverLetters = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider).GetCoverLetters();
    if (((ResultBase) coverLetters).IsSuccess)
    {
      foreach (CoverLetter letter in coverLetters.Letters)
        ((PXGraph) this.Base).Caches[typeof (CertificateTemplate)].Insert((object) new CertificateTemplate()
        {
          TemplateID = letter.LetterId,
          TemplateName = letter.LetterName
        });
    }
    else
      this.LogMessages((ResultBase) coverLetters);
  }

  public virtual void GetCertificateReasons()
  {
    IEnumerable cached = ((PXGraph) this.Base).Caches[typeof (CertificateReason)].Cached;
    if (cached != null && cached.Count() != 0L)
      return;
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
    if (txSetup == null)
      return;
    GetExemptReasonsResult exemptReasons = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider).GetExemptReasons();
    if (((ResultBase) exemptReasons).IsSuccess)
    {
      foreach (ExemptReason reason in exemptReasons.Reasons)
        ((PXGraph) this.Base).Caches[typeof (CertificateReason)].Insert((object) new CertificateReason()
        {
          ReasonID = reason.ReasonId,
          ReasonName = reason.Reason
        });
    }
    else
      this.LogMessages((ResultBase) exemptReasons);
  }

  public bool CheckForEmptyFields(
    PXCache certificateViewCache,
    RequestECMCertificateFilter certificateParams)
  {
    bool flag = false;
    if (string.IsNullOrEmpty(certificateParams.CompanyCode))
    {
      flag = true;
      certificateViewCache.RaiseExceptionHandling<RequestECMCertificateFilter.companyCode>((object) certificateParams, (object) certificateParams.CompanyCode, (Exception) new PXSetPropertyException("{0} cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Customer Code"
      }));
    }
    if (string.IsNullOrEmpty(certificateParams.EmailId))
    {
      flag = true;
      certificateViewCache.RaiseExceptionHandling<RequestECMCertificateFilter.emailId>((object) certificateParams, (object) certificateParams.EmailId, (Exception) new PXSetPropertyException("{0} cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Email"
      }));
    }
    if (string.IsNullOrEmpty(certificateParams.Template))
    {
      flag = true;
      certificateViewCache.RaiseExceptionHandling<RequestECMCertificateFilter.template>((object) certificateParams, (object) certificateParams.Template, (Exception) new PXSetPropertyException("{0} cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Template"
      }));
    }
    if (string.IsNullOrEmpty(certificateParams.CountryID))
    {
      flag = true;
      certificateViewCache.RaiseExceptionHandling<RequestECMCertificateFilter.countryID>((object) certificateParams, (object) certificateParams.CountryID, (Exception) new PXSetPropertyException("{0} cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "Country ID"
      }));
    }
    if (string.IsNullOrEmpty(certificateParams.State))
    {
      flag = true;
      certificateViewCache.RaiseExceptionHandling<RequestECMCertificateFilter.state>((object) certificateParams, (object) certificateParams.State, (Exception) new PXSetPropertyException("{0} cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "State"
      }));
    }
    return flag;
  }

  public void RequestExemptionCertificate(Customer customer, RequestECMCertificateFilter filter)
  {
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
    if (txSetup == null)
      return;
    try
    {
      IExemptionCertificateProvider ecmProvider = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider);
      string companyId = this.CompanyIdForCompanyCode(filter.CompanyCode);
      RequestCertificateRequest certificateRequest = this.BuildRequestCertificateRequest(customer, filter, companyId);
      RequestCertificateResult result = ecmProvider.RequestCertificate(certificateRequest);
      if (!((ResultBase) result).IsSuccess)
        throw new PXException(this.LogMessages((ResultBase) result));
    }
    catch (Exception ex)
    {
      throw new PXException(ex.Message.ToString());
    }
  }

  public virtual RequestCertificateRequest BuildRequestCertificateRequest(
    Customer customer,
    RequestECMCertificateFilter filter,
    string companyId)
  {
    return new RequestCertificateRequest()
    {
      CompanyId = companyId,
      Recipient = filter.EmailId,
      CertificateTemplate = filter.Template,
      Country = filter.CountryID,
      State = filter.State,
      ExemptionReason = filter.ExemptReason,
      CustomerCode = customer.AcctCD
    };
  }

  public virtual void CreateECMCustomer(Customer customer, string companyCode, out string warning)
  {
    warning = string.Empty;
    if (customer == null)
      return;
    try
    {
      List<string> list1 = ((IEnumerable<string>) companyCode.Split(',')).Select<string, string>((Func<string, string>) (p => p.Trim())).ToList<string>();
      string[] source;
      if (string.IsNullOrEmpty(customer.ECMCompanyCode))
        source = Array.Empty<string>();
      else
        source = ((IEnumerable<string>) customer.ECMCompanyCode.Split(',')).Select<string, string>((Func<string, string>) (p => p.Trim())).ToArray<string>();
      List<string> list2 = ((IEnumerable<string>) source).ToList<string>();
      List<string> list3 = list1.Except<string>((IEnumerable<string>) list2).ToList<string>();
      List<string> list4 = list1.Intersect<string>((IEnumerable<string>) list2).ToList<string>();
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      bool flag1 = false;
      bool flag2 = false;
      TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
      if (txSetup == null)
        return;
      IExemptionCertificateProvider ecmProvider = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider);
      ((PXSelectBase<Customer>) this.Base.BAccount).Current = customer;
      Customer copy = (Customer) ((PXSelectBase) this.Base.BAccount).Cache.CreateCopy((object) customer);
      foreach (string companyCode1 in list3)
      {
        string companyID = this.CompanyIdForCompanyCode(companyCode1);
        CreateCustomerRequest customerRequest = this.BuildCreateCustomerRequest(customer, companyID);
        CreateCustomerResult customer1 = ecmProvider.CreateCustomer(customerRequest);
        if (((ResultBase) customer1).IsSuccess)
        {
          stringList.Add(companyCode1);
          flag2 = true;
        }
        else if (!((ResultBase) customer1).IsSuccess && customer1.IsCustomerExist)
        {
          list4.Add(companyCode1);
          flag2 = true;
        }
        else
        {
          stringBuilder.AppendLine(this.LogMessages((ResultBase) customer1));
          flag1 = true;
        }
      }
      if (list4.Count<string>() > 0 && stringList.Count<string>() == 0)
        warning = string.Join(",", list4.ToArray());
      if (flag2)
      {
        list2.AddRange((IEnumerable<string>) stringList);
        List<string> list5 = list4.Except<string>((IEnumerable<string>) list2).ToList<string>();
        list2.AddRange((IEnumerable<string>) list5);
        copy.ECMCompanyCode = string.Join(",", list2.ToArray());
        copy.IsECMValid = new bool?(true);
        ((PXSelectBase) this.Base.BAccount).Cache.Update((object) copy);
        ((PXGraph) this.Base).Actions.PressSave();
      }
      if (flag1)
        throw new PXException(stringBuilder.ToString());
    }
    catch (Exception ex)
    {
      throw new PXException(ex.Message.ToString());
    }
  }

  public virtual CreateCustomerRequest BuildCreateCustomerRequest(
    Customer customer,
    string companyID)
  {
    return customer != null ? new CreateCustomerRequest()
    {
      Customer = this.CreateCustomer(customer, companyID)
    } : throw new PXArgumentException(nameof (customer), "The argument cannot be null.");
  }

  public virtual ECMCustomer CreateCustomer(Customer customer, string companyID)
  {
    PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) ((PXGraph) this.Base).GetExtension<CustomerMaint.DefContactAddressExt>().DefAddress).Select(Array.Empty<object>()));
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this.Base).GetExtension<CustomerMaint.DefLocationExt>().DefLocation).Select(Array.Empty<object>()));
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) ((PXGraph) this.Base).GetExtension<CustomerMaint.PrimaryContactGraphExt>().PrimaryContactCurrent).Select(Array.Empty<object>()));
    return new ECMCustomer()
    {
      CompanyId = companyID,
      CustomerId = customer.BAccountID,
      CustomerCode = customer.AcctCD,
      CustomerName = customer.AcctName,
      TaxIdNumber = location?.TaxRegistrationID,
      Address = new TaxAddress()
      {
        AddressLine1 = address?.AddressLine1,
        AddressLine2 = address?.AddressLine2,
        City = address?.City,
        Region = address?.State,
        PostalCode = address?.PostalCode,
        Country = address?.CountryID
      },
      Contact = new TaxContact()
      {
        EMail = contact?.EMail,
        Fax = contact?.Fax,
        FullName = contact?.DisplayName,
        PhoneNumber = contact?.Phone1
      }
    };
  }

  public virtual void UpdateECMCustomer(Customer customer)
  {
    if (customer == null)
      return;
    try
    {
      bool flag = false;
      StringBuilder stringBuilder = new StringBuilder();
      string[] strArray1;
      if (string.IsNullOrEmpty(customer.ECMCompanyCode))
        strArray1 = Array.Empty<string>();
      else
        strArray1 = ((IEnumerable<string>) customer.ECMCompanyCode.Split(',')).Select<string, string>((Func<string, string>) (p => p.Trim())).ToArray<string>();
      string[] strArray2 = strArray1;
      TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
      if (txSetup == null)
        return;
      IExemptionCertificateProvider ecmProvider = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider);
      ((PXSelectBase<Customer>) this.Base.BAccount).Current = customer;
      Customer copy = (Customer) ((PXSelectBase) this.Base.BAccount).Cache.CreateCopy((object) customer);
      foreach (string companyCode in strArray2)
      {
        string companyID = this.CompanyIdForCompanyCode(companyCode);
        UpdateCustomerRequest updateCustomerRequest = this.BuildUpdateCustomerRequest(customer, companyID);
        UpdateCustomerResult result = ecmProvider.UpdateCustomer(updateCustomerRequest);
        if (!((ResultBase) result).IsSuccess)
        {
          stringBuilder.AppendLine(this.LogMessages((ResultBase) result));
          flag = true;
          break;
        }
      }
      if (flag)
        throw new PXException(stringBuilder.ToString());
      copy.IsECMValid = new bool?(true);
      ((PXSelectBase) this.Base.BAccount).Cache.Update((object) copy);
      ((PXGraph) this.Base).Actions.PressSave();
    }
    catch (Exception ex)
    {
      throw new PXException(ex.Message.ToString());
    }
  }

  public virtual UpdateCustomerRequest BuildUpdateCustomerRequest(
    Customer customer,
    string companyID)
  {
    return customer != null ? new UpdateCustomerRequest()
    {
      Customer = this.CreateCustomer(customer, companyID)
    } : throw new PXArgumentException(nameof (customer), "The argument cannot be null.");
  }

  public virtual void ViewExemptCertificate(ExemptionCertificate certificate)
  {
    if (certificate == null)
      return;
    TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(((PXSelectBase<TXSetup>) this.TXSetupRecord).Select(Array.Empty<object>()));
    if (txSetup == null)
      return;
    ViewCertificateResult result = TaxPluginMaint.CreateECMProvider((PXGraph) this.Base, txSetup.ECMProvider).ViewCertificate(this.BuildViewCertificateRequest(certificate));
    if (((ResultBase) result).IsSuccess)
      throw new PXRedirectToFileException(new FileInfo(result.Filename, (string) null, result.Data), false);
    this.LogMessages((ResultBase) result);
    throw new PXException("The certificate cannot be opened because no certificate file is attached.");
  }

  public virtual ViewCertificateRequest BuildViewCertificateRequest(ExemptionCertificate certificate)
  {
    return new ViewCertificateRequest()
    {
      CompanyId = this.CompanyIdForCompanyCode(certificate.CompanyCode),
      CertificateID = certificate.CertificateID
    };
  }

  public virtual string CompanyIdForCompanyCode(string companyCode)
  {
    return PXResultset<TaxPluginMapping>.op_Implicit(PXSelectBase<TaxPluginMapping, PXSelectReadonly2<TaxPluginMapping, InnerJoin<TaxPlugin, On<TaxPlugin.taxPluginID, Equal<TaxPluginMapping.taxPluginID>>, InnerJoin<TXSetup, On<TXSetup.eCMProvider, Equal<TaxPlugin.taxPluginID>>>>, Where<TaxPluginMapping.externalCompanyID, IsNotNull, And<TaxPluginMapping.companyCode, Equal<Required<TaxPluginMapping.companyCode>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) companyCode
    }))?.ExternalCompanyID;
  }

  protected virtual string LogMessages(ResultBase result)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string message in result.Messages)
    {
      stringBuilder.AppendLine(message);
      PXTrace.WriteError(message);
    }
    return stringBuilder.ToString();
  }
}
