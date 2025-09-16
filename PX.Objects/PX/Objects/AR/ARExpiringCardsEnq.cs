// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARExpiringCardsEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.BQLConstants;
using PX.Objects.GL;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARExpiringCardsEnq : PXGraph<
#nullable disable
ARExpiringCardsEnq>
{
  public PXFilter<ARExpiringCardsEnq.ARExpiringCardFilter> Filter;
  public PXCancel<ARExpiringCardsEnq.ARExpiringCardFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>>>> Cards;
  public PXAction<ARExpiringCardsEnq.ARExpiringCardFilter> viewCustomer;
  public PXAction<ARExpiringCardsEnq.ARExpiringCardFilter> viewPaymentMethod;

  public ARExpiringCardsEnq()
  {
    ((PXSelectBase) this.Cards).Cache.AllowInsert = false;
    ((PXSelectBase) this.Cards).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Cards).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetDisplayName(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], typeof (PX.Objects.CR.Contact.salutation).Name, "Attention");
  }

  public virtual IEnumerable cards()
  {
    ARExpiringCardsEnq.ARExpiringCardFilter current = ((PXSelectBase<ARExpiringCardsEnq.ARExpiringCardFilter>) this.Filter).Current;
    if (current == null)
      return (IEnumerable) null;
    PXSelectBase<CustomerPaymentMethod> pxSelectBase = (PXSelectBase<CustomerPaymentMethod>) new PXSelectJoin<CustomerPaymentMethod, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>>>, Where<CustomerPaymentMethod.expirationDate, GreaterEqual<Required<CustomerPaymentMethod.expirationDate>>, And<CustomerPaymentMethod.expirationDate, LessEqual<Required<CustomerPaymentMethod.expirationDate>>>>>((PXGraph) this);
    if (!string.IsNullOrEmpty(current.CustomerClassID))
      pxSelectBase.WhereAnd<Where<Customer.customerClassID, Equal<Required<Customer.customerClassID>>>>();
    if (current.ActiveOnly.Value)
      pxSelectBase.WhereAnd<Where<CustomerPaymentMethod.isActive, Equal<BitOn>>>();
    return (IEnumerable) pxSelectBase.Select(new object[3]
    {
      (object) current.BeginDate,
      (object) current.EndDate,
      (object) current.CustomerClassID
    });
  }

  public virtual void ARExpiringCardFilter_BeginDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXUIField]
  [PXLookupButton]
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
      throw new PXRedirectRequiredException((PXGraph) instance, "View Customer");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
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
  public class ARExpiringCardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected int? _ExpireXDays;
    protected string _CustomerClassID;
    protected bool? _ActiveOnly;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Start Date")]
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

    [PXDBInt]
    [PXDefault(30)]
    [PXUIField(DisplayName = "Duration")]
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
    public virtual bool? ActiveOnly
    {
      get => this._ActiveOnly;
      set => this._ActiveOnly = value;
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARExpiringCardsEnq.ARExpiringCardFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARExpiringCardsEnq.ARExpiringCardFilter.endDate>
    {
    }

    public abstract class expireXDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARExpiringCardsEnq.ARExpiringCardFilter.expireXDays>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARExpiringCardsEnq.ARExpiringCardFilter.customerClassID>
    {
    }

    public abstract class activeOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARExpiringCardsEnq.ARExpiringCardFilter.activeOnly>
    {
    }
  }
}
