// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARExpiredCardsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.GL;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARExpiredCardsProcess : PXGraph<
#nullable disable
ARExpiringCardsProcess>
{
  public PXFilter<ARExpiredCardsProcess.ARExpiredCardFilter> Filter;
  public PXCancel<ARExpiredCardsProcess.ARExpiredCardFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<CustomerPaymentMethod, ARExpiredCardsProcess.ARExpiredCardFilter, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>>>> Cards;
  public PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>>>, Where<CustomerPaymentMethod.isActive, Equal<True>, And<CustomerPaymentMethod.expirationDate, LessEqual<Required<CustomerPaymentMethod.expirationDate>>, And<CustomerPaymentMethod.expirationDate, GreaterEqual<Required<CustomerPaymentMethod.expirationDate>>>>>> CardsView;
  public PXAction<ARExpiredCardsProcess.ARExpiredCardFilter> viewCustomer;
  public PXAction<ARExpiredCardsProcess.ARExpiredCardFilter> viewPaymentMethod;

  public ARExpiredCardsProcess()
  {
    ((PXSelectBase) this.Cards).Cache.AllowInsert = false;
    ((PXSelectBase) this.Cards).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Cards).Cache.AllowDelete = false;
    ((PXProcessingBase<CustomerPaymentMethod>) this.Cards).SetSelected<CustomerPaymentMethod.selected>();
    // ISSUE: method pointer
    ((PXProcessingBase<CustomerPaymentMethod>) this.Cards).SetProcessDelegate<CustomerPaymentMethodMassProcess>(new PXProcessingBase<CustomerPaymentMethod>.ProcessItemDelegate<CustomerPaymentMethodMassProcess>((object) null, __methodptr(SetCardInactive)));
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Cards).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.selected>(((PXSelectBase) this.Cards).Cache, (object) null, true);
    ((PXProcessing<CustomerPaymentMethod>) this.Cards).SetProcessAllCaption("Deactivate all");
    ((PXProcessing<CustomerPaymentMethod>) this.Cards).SetProcessCaption("Deactivate");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Attention");
  }

  public virtual IEnumerable cards()
  {
    ARExpiredCardsProcess.ARExpiredCardFilter filter = ((PXSelectBase<ARExpiredCardsProcess.ARExpiredCardFilter>) this.Filter).Current;
    if (filter != null)
    {
      if (!string.IsNullOrEmpty(filter.CustomerClassID))
        ((PXSelectBase<CustomerPaymentMethod>) this.CardsView).WhereAnd<Where<Customer.customerClassID, Equal<Required<Customer.customerClassID>>>>();
      if (filter.DefaultOnly.GetValueOrDefault())
        ((PXSelectBase<CustomerPaymentMethod>) this.CardsView).WhereAnd<Where<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>>>();
      PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>>>, Where<CustomerPaymentMethod.isActive, Equal<True>, And<CustomerPaymentMethod.expirationDate, LessEqual<Required<CustomerPaymentMethod.expirationDate>>, And<CustomerPaymentMethod.expirationDate, GreaterEqual<Required<CustomerPaymentMethod.expirationDate>>>>>> cardsView = this.CardsView;
      object[] objArray = new object[3]
      {
        (object) filter.BeginDate,
        (object) filter.EndDate,
        (object) filter.CustomerClassID
      };
      foreach (PXResult<CustomerPaymentMethod, Customer, PX.Objects.CR.Contact> pxResult in ((PXSelectBase<CustomerPaymentMethod>) cardsView).Select(objArray))
      {
        CustomerPaymentMethod customerPaymentMethod = PXResult<CustomerPaymentMethod, Customer, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        if (!filter.NotificationSendOnly.GetValueOrDefault() || customerPaymentMethod.LastNotificationDate.HasValue)
          yield return (object) pxResult;
      }
    }
  }

  public virtual void ARExpiredCardFilter_BeginDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  public static void SetCardInactive(
    CustomerPaymentMethodMassProcess aGraph,
    CustomerPaymentMethod aCard)
  {
    aGraph.SetActive(aCard, false);
  }

  public virtual void ARExpiredCardFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARExpiredCardsProcess.ARExpiredCardFilter row))
      return;
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.selected>(((PXSelectBase) this.Cards).Cache, (object) null, true);
    if (row.TokenizedPMs == null)
    {
      string tokenizedPmsString = CCProcessingHelper.GetTokenizedPMsString((PXGraph) this);
      sender.SetValue<ARExpiredCardsProcess.ARExpiredCardFilter.tokenizedPMs>((object) row, (object) tokenizedPmsString);
    }
    else
    {
      if (!(row.TokenizedPMs != string.Empty))
        return;
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<ARExpiredCardsProcess.ARExpiredCardFilter.customerClassID>(e.Row, (object) ((ARExpiredCardsProcess.ARExpiredCardFilter) e.Row).CustomerClassID, (Exception) new PXSetPropertyException("Some cards for {0} payment method(s) are not shown here because their data is stored at processing center", (PXErrorLevel) 2, new object[1]
      {
        (object) row.TokenizedPMs
      }));
    }
  }

  public virtual void CustomerPaymentMethod_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.selected>(((PXSelectBase) this.Cards).Cache, e.Row, true);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCustomer(PXAdapter adapter)
  {
    if (((PXSelectBase<CustomerPaymentMethod>) this.Cards).Current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<CustomerPaymentMethod>) this.Cards).Current.BAccountID
      }));
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<Customer>) instance.BAccount).Current = customer;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "View Customer");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewPaymentMethod(PXAdapter adapter)
  {
    if (((PXSelectBase<CustomerPaymentMethod>) this.Cards).Current != null)
    {
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<CustomerPaymentMethod>) this.Cards).Current.PMInstanceID
      }));
      CustomerPaymentMethodMaint instance = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
      ((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Current = customerPaymentMethod;
      throw new PXPopupRedirectException((PXGraph) instance, "View Payment Method", true);
    }
    return adapter.Get();
  }

  [Serializable]
  public class ARExpiredCardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected int? _ExpireXDays;
    protected string _CustomerClassID;
    protected bool? _ExpiredOnly;
    protected bool? _DefaultOnly;
    protected bool? _NotificationSendOnly;
    protected string _TokenizedPMs;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Expiration Date")]
    public virtual DateTime? BeginDate
    {
      get => this._BeginDate;
      set => this._BeginDate = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? EndDate
    {
      get
      {
        if (!this._BeginDate.HasValue)
          return this._BeginDate;
        DateTime dateTime = this._BeginDate.Value;
        ref DateTime local = ref dateTime;
        int? expireXdays = this._ExpireXDays;
        double valueOrDefault = (double) (expireXdays.HasValue ? new int?(-expireXdays.GetValueOrDefault()) : new int?()).GetValueOrDefault();
        return new DateTime?(local.AddDays(valueOrDefault));
      }
      set
      {
      }
    }

    [PXDBInt(MinValue = 0, MaxValue = 10000)]
    [PXDefault(30)]
    [PXUIField(DisplayName = "Expired Within (Days)")]
    public virtual int? ExpireXDays
    {
      get => this._ExpireXDays;
      set => this._ExpireXDays = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Active Only ")]
    public virtual bool? ExpiredOnly
    {
      get => this._ExpiredOnly;
      set => this._ExpiredOnly = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Cards of Default Payment Method Only")]
    public virtual bool? DefaultOnly
    {
      get => this._DefaultOnly;
      set => this._DefaultOnly = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Cards of Notified Customers Only")]
    public virtual bool? NotificationSendOnly
    {
      get => this._NotificationSendOnly;
      set => this._NotificationSendOnly = value;
    }

    [PXString]
    public virtual string TokenizedPMs
    {
      get => this._TokenizedPMs;
      set => this._TokenizedPMs = value;
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.endDate>
    {
    }

    public abstract class expireXDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.expireXDays>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.customerClassID>
    {
    }

    public abstract class expiredOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.expiredOnly>
    {
    }

    public abstract class defaultOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.defaultOnly>
    {
    }

    public abstract class notificationSendOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.notificationSendOnly>
    {
    }

    public abstract class tokenizedPMs : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARExpiredCardsProcess.ARExpiredCardFilter.tokenizedPMs>
    {
    }
  }
}
