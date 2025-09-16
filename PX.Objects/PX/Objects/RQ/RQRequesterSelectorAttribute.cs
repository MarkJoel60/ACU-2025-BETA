// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequesterSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.RQ;

/// <summary>
/// Selector.<br />
/// Show list of employees or list of customers according to definition of request class.
/// </summary>
public class RQRequesterSelectorAttribute : PXDimensionSelectorAttribute
{
  public RQRequesterSelectorAttribute()
    : this((System.Type) null)
  {
  }

  /// <summary>Constructor.</summary>
  /// <param name="reqClassID">Request class field.</param>
  public RQRequesterSelectorAttribute(System.Type reqClassID)
    : base("BIZACCT", typeof (Search2<PX.Objects.CR.BAccount.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[6]
    {
      typeof (PX.Objects.CR.BAccount.acctCD),
      typeof (PX.Objects.CR.BAccount.acctName),
      typeof (PX.Objects.CR.BAccount.vStatus),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    })
  {
    this.DescriptionField = typeof (PX.Objects.CR.BAccount.acctName);
    ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new RQRequesterSelectorAttribute.CustomSelector(reqClassID);
  }

  /// <summary>Internal selector used only for dimension.</summary>
  public class CustomSelector : PXCustomSelectorAttribute
  {
    protected System.Type reqClassID;
    protected PXView viewEmployees;
    protected PXView viewCustomers;
    protected PXView viewClass;

    public CustomSelector(System.Type reqClassID)
      : base(typeof (Search2<BAccountR.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>>>>), new System.Type[6]
      {
        typeof (BAccountR.acctCD),
        typeof (BAccountR.acctName),
        typeof (BAccountR.vStatus),
        typeof (PX.Objects.CR.Contact.phone1),
        typeof (PX.Objects.CR.Address.city),
        typeof (PX.Objects.CR.Address.countryID)
      })
    {
      ((PXSelectorAttribute) this).SubstituteKey = typeof (BAccountR.acctCD);
      ((PXSelectorAttribute) this).DescriptionField = typeof (BAccountR.acctName);
      this.reqClassID = reqClassID;
      ((PXSelectorAttribute) this)._ViewName = "_Requester_";
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this.viewEmployees = new PXView(sender.Graph, true, BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Search5<BAccountR.bAccountID, InnerJoin<CREmployee, On<CREmployee.bAccountID, Equal<BAccountR.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<CREmployee.parentBAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<CREmployee.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<CREmployee.parentBAccountID>, And<PX.Objects.CR.Address.addressID, Equal<CREmployee.defAddressID>>>, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.contactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>, Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>, Aggregate<GroupBy<BAccountR.bAccountID>>>)
      }));
      this.viewCustomers = new PXView(sender.Graph, true, BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Search2<BAccountR.bAccountID, InnerJoin<Customer, On<Customer.bAccountID, Equal<BAccountR.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<Customer.defAddressID>>>>>>, Where<Match<Customer, Current<AccessInfo.userName>>>>)
      }));
      PXGraph graph = sender.Graph;
      System.Type[] typeArray1 = new System.Type[1];
      System.Type[] typeArray2 = new System.Type[7]
      {
        typeof (Search<,>),
        typeof (RQRequestClass.reqClassID),
        typeof (Where<,>),
        typeof (RQRequestClass.reqClassID),
        typeof (Equal<>),
        typeof (Optional<>),
        null
      };
      System.Type type1 = this.reqClassID;
      if ((object) type1 == null)
        type1 = typeof (RQRequestClass.reqClassID);
      typeArray2[6] = type1;
      typeArray1[0] = BqlCommand.Compose(typeArray2);
      BqlCommand instance = BqlCommand.CreateInstance(typeArray1);
      this.viewClass = new PXView(graph, true, instance);
      if (this.reqClassID != (System.Type) null)
      {
        // ISSUE: method pointer
        sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.reqClassID), this.reqClassID.Name, new PXFieldUpdated((object) this, __methodptr(ReqClassFieldUpdated)));
      }
      PXUIFieldAttribute.SetDisplayName<BAccountR.acctCD>(sender.Graph.Caches[typeof (BAccountR)], "Requester");
      PXUIFieldAttribute.SetDisplayName<BAccountR.acctName>(sender.Graph.Caches[typeof (BAccountR)], "Requester Name");
      PXUIFieldAttribute.SetDisplayName<BAccountR.vStatus>(sender.Graph.Caches[typeof (BAccountR)], "Employee Status");
      System.Type[] typeArray3 = new System.Type[6]
      {
        typeof (BAccountR.acctCD),
        typeof (BAccountR.acctName),
        typeof (BAccountR.vStatus),
        typeof (PX.Objects.CR.Contact.phone1),
        typeof (PX.Objects.CR.Address.city),
        typeof (PX.Objects.CR.Address.countryID)
      };
      string[] strArray1 = new string[typeArray3.Length];
      string[] strArray2 = new string[typeArray3.Length];
      for (int index = 0; index < typeArray3.Length; ++index)
      {
        System.Type type2 = typeArray3[index];
        System.Type itemType = BqlCommand.GetItemType(type2);
        PXCache cach = sender.Graph.Caches[itemType];
        strArray1[index] = itemType.IsAssignableFrom(typeof (BAccountR)) || type2.Name == typeof (BAccountR.acctCD).Name || type2.Name == typeof (BAccountR.acctName).Name ? type2.Name : $"{itemType.Name}__{type2.Name}";
        strArray2[index] = PXUIFieldAttribute.GetDisplayName(cach, type2.Name);
      }
      PXSelectorAttribute.SetColumns(sender, ((PXEventSubscriberAttribute) this)._FieldName, strArray1, strArray2);
    }

    private void ReqClassFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      string oldValue = (string) e.OldValue;
      string str = (string) sender.GetValue(e.Row, this.reqClassID.Name);
      object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
      if (!(oldValue != str))
        return;
      RQRequestClass rqRequestClass1 = (RQRequestClass) this.viewClass.SelectSingle(new object[1]
      {
        (object) str
      });
      RQRequestClass rqRequestClass2 = (RQRequestClass) this.viewClass.SelectSingle(new object[1]
      {
        (object) oldValue
      });
      bool? customerRequest1;
      if (rqRequestClass1 != null && rqRequestClass2 != null)
      {
        bool? customerRequest2 = rqRequestClass1.CustomerRequest;
        customerRequest1 = rqRequestClass2.CustomerRequest;
        if (!(customerRequest2.GetValueOrDefault() == customerRequest1.GetValueOrDefault() & customerRequest2.HasValue == customerRequest1.HasValue))
        {
          sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
          sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          return;
        }
      }
      if (rqRequestClass1 == null || obj == null)
        return;
      PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        obj
      }));
      if (baccount == null)
        return;
      if (baccount.Type != "CU")
      {
        customerRequest1 = rqRequestClass1.CustomerRequest;
        if (customerRequest1.GetValueOrDefault())
          goto label_11;
      }
      if (!(baccount.Type != "EP"))
        return;
      customerRequest1 = rqRequestClass1.CustomerRequest;
      if (customerRequest1.GetValueOrDefault())
        return;
label_11:
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
    }

    protected virtual IEnumerable GetRecords()
    {
      RQRequestClass rqRequestClass = this.reqClassID != (System.Type) null ? (RQRequestClass) this.viewClass.SelectSingle(Array.Empty<object>()) : (RQRequestClass) null;
      int startRow = PXView.StartRow;
      int num = 0;
      List<object> records = (rqRequestClass == null || !rqRequestClass.CustomerRequest.GetValueOrDefault() ? this.viewEmployees : this.viewCustomers).Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
      PXView.StartRow = 0;
      return (IEnumerable) records;
    }

    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (e.NewValue == null)
        return;
      BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.acctCD, Equal<Required<BAccountR.acctCD>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
      {
        e.NewValue
      }));
      if (baccountR != null)
      {
        e.NewValue = (object) baccountR.BAccountID;
        ((CancelEventArgs) e).Cancel = true;
      }
      else if (e.NewValue.GetType() == typeof (int))
        baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
        {
          e.NewValue
        }));
      if (baccountR == null)
        throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", new object[2]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName,
          e.NewValue
        }));
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      object returnValue = e.ReturnValue;
      e.ReturnValue = (object) null;
      ((PXSelectorAttribute) this).FieldSelecting(sender, e);
      BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
      {
        returnValue
      }));
      if (baccountR != null)
      {
        e.ReturnValue = (object) baccountR.AcctCD;
      }
      else
      {
        if (e.Row == null)
          return;
        e.ReturnValue = (object) null;
      }
    }

    protected virtual PXView GetUnconditionalView(PXCache cache)
    {
      return cache.Graph.TypedViews.GetView(((PXSelectorAttribute) this)._UnconditionalSelect, !((PXSelectorAttribute) this).DirtyRead);
    }
  }
}
