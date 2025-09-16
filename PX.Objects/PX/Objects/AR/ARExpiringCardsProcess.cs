// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARExpiringCardsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.BQLConstants;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARExpiringCardsProcess : PXGraph<
#nullable disable
ARExpiringCardsProcess>
{
  public PXFilter<ARExpiringCardsProcess.ARExpiringCardFilter> Filter;
  public PXCancel<ARExpiringCardsProcess.ARExpiringCardFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  [PXViewName("Card Details")]
  public PXFilteredProcessingJoin<CustomerPaymentMethod, ARExpiringCardsProcess.ARExpiringCardFilter, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Where2<Where<CustomerPaymentMethod.billContactID, IsNull, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>, Or<Where<CustomerPaymentMethod.billContactID, IsNotNull, And<CustomerPaymentMethod.billContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>>>>> Cards;
  public PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Where2<Where<CustomerPaymentMethod.billContactID, IsNull, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>, Or<Where<CustomerPaymentMethod.billContactID, IsNotNull, And<CustomerPaymentMethod.billContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>>>>, Where<CustomerPaymentMethod.expirationDate, GreaterEqual<Required<CustomerPaymentMethod.expirationDate>>, And<CustomerPaymentMethod.expirationDate, LessEqual<Required<CustomerPaymentMethod.expirationDate>>>>> CardsView;
  public PXAction<ARExpiringCardsProcess.ARExpiringCardFilter> viewCustomer;
  public PXAction<ARExpiringCardsProcess.ARExpiringCardFilter> viewPaymentMethod;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;

  public ARExpiringCardsProcess()
  {
    ((PXSelectBase) this.Cards).Cache.AllowInsert = false;
    ((PXSelectBase) this.Cards).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Cards).Cache.AllowDelete = false;
    // ISSUE: method pointer
    ((PXProcessingBase<CustomerPaymentMethod>) this.Cards).SetProcessDelegate<CustomerPaymentMethodMassProcess>(new PXProcessingBase<CustomerPaymentMethod>.ProcessItemDelegate<CustomerPaymentMethodMassProcess>((object) null, __methodptr(MailExpiringNotification)));
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Cards).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.selected>(((PXSelectBase) this.Cards).Cache, (object) null, true);
    ((PXProcessingBase<CustomerPaymentMethod>) this.Cards).SetSelected<CustomerPaymentMethod.selected>();
    ((PXProcessing<CustomerPaymentMethod>) this.Cards).SetProcessAllCaption("Notify All");
    ((PXProcessing<CustomerPaymentMethod>) this.Cards).SetProcessCaption("Notify");
    PXUIFieldAttribute.SetDisplayName(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], typeof (PX.Objects.CR.Contact.salutation).Name, "Attention");
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  public virtual IEnumerable cards()
  {
    ARExpiringCardsProcess.ARExpiringCardFilter filter = ((PXSelectBase<ARExpiringCardsProcess.ARExpiringCardFilter>) this.Filter).Current;
    if (filter != null)
    {
      if (!string.IsNullOrEmpty(filter.CustomerClassID))
        ((PXSelectBase<CustomerPaymentMethod>) this.CardsView).WhereAnd<Where<Customer.customerClassID, Equal<Required<Customer.customerClassID>>>>();
      if (filter.DefaultOnly.GetValueOrDefault())
        ((PXSelectBase<CustomerPaymentMethod>) this.CardsView).WhereAnd<Where<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>>>();
      if (filter.ActiveOnly.Value)
        ((PXSelectBase<CustomerPaymentMethod>) this.CardsView).WhereAnd<Where<CustomerPaymentMethod.isActive, Equal<BitOn>>>();
      PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Customer.status, NotEqual<CustomerStatus.inactive>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<CustomerPaymentMethod.bAccountID>, And<Where2<Where<CustomerPaymentMethod.billContactID, IsNull, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>, Or<Where<CustomerPaymentMethod.billContactID, IsNotNull, And<CustomerPaymentMethod.billContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>>>>, Where<CustomerPaymentMethod.expirationDate, GreaterEqual<Required<CustomerPaymentMethod.expirationDate>>, And<CustomerPaymentMethod.expirationDate, LessEqual<Required<CustomerPaymentMethod.expirationDate>>>>> cardsView = this.CardsView;
      object[] objArray = new object[4]
      {
        (object) filter.BeginDate,
        (object) filter.EndDate,
        (object) filter.CustomerClassID,
        (object) filter.ActiveOnly
      };
      foreach (PXResult<CustomerPaymentMethod, Customer, PX.Objects.CR.Contact> pxResult in ((PXSelectBase<CustomerPaymentMethod>) cardsView).Select(objArray))
      {
        CustomerPaymentMethod customerPaymentMethod = PXResult<CustomerPaymentMethod, Customer, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        if (customerPaymentMethod.LastNotificationDate.HasValue && customerPaymentMethod.ExpirationDate.HasValue)
        {
          TimeSpan timeSpan = customerPaymentMethod.ExpirationDate.Value - customerPaymentMethod.LastNotificationDate.Value;
          int? nullable1;
          if (timeSpan.TotalDays > 0.0)
          {
            double totalDays = timeSpan.TotalDays;
            nullable1 = filter.NoteLeftLimit;
            double? nullable2 = nullable1.HasValue ? new double?((double) nullable1.GetValueOrDefault()) : new double?();
            double valueOrDefault = nullable2.GetValueOrDefault();
            if (totalDays < valueOrDefault & nullable2.HasValue)
              continue;
          }
          else
          {
            double num = Math.Abs(timeSpan.TotalDays);
            nullable1 = filter.NoteRightLimit;
            double? nullable3 = nullable1.HasValue ? new double?((double) nullable1.GetValueOrDefault()) : new double?();
            double valueOrDefault = nullable3.GetValueOrDefault();
            if (num < valueOrDefault & nullable3.HasValue)
              continue;
          }
        }
        yield return (object) pxResult;
      }
    }
  }

  public static void MailExpiringNotification(
    CustomerPaymentMethodMassProcess aGraph,
    CustomerPaymentMethod aCard)
  {
    aGraph.MailExpirationNotification(aCard);
  }

  public virtual void ARExpiringCardFilter_BeginDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void ARExpiringCardFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARExpiringCardsProcess.ARExpiringCardFilter row))
      return;
    PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.selected>(((PXSelectBase) this.Cards).Cache, (object) null, true);
    if (row.TokenizedPMs == null)
    {
      string tokenizedPmsString = CCProcessingHelper.GetTokenizedPMsString((PXGraph) this);
      sender.SetValue<ARExpiringCardsProcess.ARExpiringCardFilter.tokenizedPMs>((object) row, (object) tokenizedPmsString);
    }
    else
    {
      if (!(row.TokenizedPMs != string.Empty))
        return;
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<ARExpiringCardsProcess.ARExpiringCardFilter.customerClassID>(e.Row, (object) ((ARExpiringCardsProcess.ARExpiringCardFilter) e.Row).CustomerClassID, (Exception) new PXSetPropertyException("Some cards for {0} payment method(s) are not shown here because their data is stored at processing center", (PXErrorLevel) 2, new object[1]
      {
        (object) row.TokenizedPMs
      }));
    }
  }

  public virtual void ARExpiringCardFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Cards).Cache.Clear();
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
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Customer");
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
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "View Payment Method");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [Serializable]
  public class ARExpiringCardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected int? _ExpireXDays;
    protected string _CustomerClassID;
    protected bool? _ActiveOnly;
    protected bool? _DefaultOnly;
    protected int? _NoteLeftLimit;
    protected int? _NoteRightLimit;
    protected string _TokenizedPMs;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Starting")]
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
        return this._BeginDate.HasValue ? new DateTime?(this._BeginDate.Value.AddDays((double) this._ExpireXDays.GetValueOrDefault())) : this._BeginDate;
      }
      set
      {
      }
    }

    [PXDBInt(MinValue = 0, MaxValue = 10000)]
    [PXDefault(30)]
    [PXUIField(DisplayName = "Expire in (days)")]
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
    [PXUIField(DisplayName = "Active Only ")]
    public virtual bool? ActiveOnly
    {
      get => this._ActiveOnly;
      set => this._ActiveOnly = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Default Payment Method Only")]
    public virtual bool? DefaultOnly
    {
      get => this._DefaultOnly;
      set => this._DefaultOnly = value;
    }

    [PXDBInt]
    [PXDefault(60)]
    [PXUIField(DisplayName = "X Days Before")]
    public virtual int? NoteLeftLimit
    {
      get => this._NoteLeftLimit;
      set => this._NoteLeftLimit = value;
    }

    [PXDBInt]
    [PXDefault(180)]
    [PXUIField(DisplayName = "X Days After")]
    public virtual int? NoteRightLimit
    {
      get => this._NoteRightLimit;
      set => this._NoteRightLimit = value;
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
      ARExpiringCardsProcess.ARExpiringCardFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.endDate>
    {
    }

    public abstract class expireXDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.expireXDays>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.customerClassID>
    {
    }

    public abstract class activeOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.activeOnly>
    {
    }

    public abstract class defaultOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.defaultOnly>
    {
    }

    public abstract class noteLeftLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.noteLeftLimit>
    {
    }

    public abstract class noteRightLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.noteRightLimit>
    {
    }

    public abstract class tokenizedPMs : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARExpiringCardsProcess.ARExpiringCardFilter.tokenizedPMs>
    {
    }
  }
}
