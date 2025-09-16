// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class CustomerPaymentMethodMassProcess : PXGraph<CustomerPaymentMethodMassProcess>
{
  [PXViewName("Customer Payment Method")]
  public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>> cards;
  [PXViewName("Customer")]
  public PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>> customer;
  [PXViewName("Billing Contact")]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<Customer, On<Customer.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<Customer.defBillContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>> billContact;
  public string notificationCD = "CCEXPIRENOTE";
  protected const string FldName_CardType = "CardType";
  protected const string FldName_CardNumber = "CardNumber";
  protected const string FldName_ExpirationDate = "ExpirationDate";

  public virtual IEnumerable billcontact()
  {
    CustomerPaymentMethodMassProcess methodMassProcess = this;
    CustomerPaymentMethod current = ((PXSelectBase<CustomerPaymentMethod>) methodMassProcess.cards).Current;
    if (current != null)
    {
      if (current.BillContactID.HasValue)
      {
        foreach (PXResult<PX.Objects.CR.Contact> pxResult in PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CustomerPaymentMethod.billContactID>>>>.Config>.Select((PXGraph) methodMassProcess, Array.Empty<object>()))
          yield return (object) PXResult<PX.Objects.CR.Contact>.op_Implicit(pxResult);
      }
      else
      {
        foreach (PXResult<PX.Objects.CR.Contact> pxResult in PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<Customer, On<Customer.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<Customer.defBillContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>>.Config>.Select((PXGraph) methodMassProcess, Array.Empty<object>()))
          yield return (object) PXResult<PX.Objects.CR.Contact>.op_Implicit(pxResult);
      }
    }
  }

  public virtual void SetActive(CustomerPaymentMethod aCard, bool on)
  {
    ((PXGraph) this).Clear();
    CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.cards).Select(new object[1]
    {
      (object) aCard.PMInstanceID
    }));
    if (customerPaymentMethod != null)
    {
      bool? isActive = customerPaymentMethod.IsActive;
      bool flag = on;
      if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      {
        customerPaymentMethod.IsActive = new bool?(on);
        ((PXSelectBase<CustomerPaymentMethod>) this.cards).Update(customerPaymentMethod);
      }
    }
    ((PXGraph) this).Actions.PressSave();
  }

  public virtual void MailExpirationNotification(CustomerPaymentMethod aCard)
  {
    ((PXGraph) this).Clear();
    CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.cards).Select(new object[1]
    {
      (object) aCard.PMInstanceID
    }));
    ((PXSelectBase<CustomerPaymentMethod>) this.cards).Current = customerPaymentMethod;
    ((PXSelectBase<Customer>) this.customer).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) this.customer).Select(Array.Empty<object>()));
    Dictionary<string, string> parameters = new Dictionary<string, string>();
    parameters["CardType"] = customerPaymentMethod.PaymentMethodID;
    parameters["CardNumber"] = customerPaymentMethod.Descr;
    parameters["ExpirationDate"] = customerPaymentMethod.ExpirationDate.Value.ToString();
    try
    {
      ((PXGraph) this).GetExtension<CustomerPaymentMethodMassProcess_ActivityDetailsExt>().SendNotification("Customer", this.notificationCD, new int?(), (IDictionary<string, string>) parameters, true, (IList<Guid?>) null);
    }
    catch (PXException ex)
    {
      throw new PXException("Notification by E-mail failed: {0}", new object[1]
      {
        (object) ((Exception) ex).Message
      });
    }
    customerPaymentMethod.LastNotificationDate = new DateTime?(DateTime.Now);
    ((PXSelectBase<CustomerPaymentMethod>) this.cards).Update(customerPaymentMethod);
    ((PXGraph) this).Actions.PressSave();
  }
}
