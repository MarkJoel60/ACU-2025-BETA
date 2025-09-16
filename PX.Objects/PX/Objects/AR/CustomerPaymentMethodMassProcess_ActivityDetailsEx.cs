// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodMassProcess_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.AR;

public class CustomerPaymentMethodMassProcess_ActivityDetailsExt : 
  ActivityDetailsExt<CustomerPaymentMethodMassProcess, CustomerPaymentMethod, CustomerPaymentMethod.noteID>
{
  public override System.Type GetBAccountIDCommand()
  {
    return typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>>);
  }

  public override System.Type GetEmailMessageTarget()
  {
    return typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CustomerPaymentMethod.billContactID>>>>);
  }
}
