// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntry_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntry_ActivityDetailsExt : 
  PMActivityDetailsExt<ARInvoiceEntry, ARInvoice, ARInvoice.noteID>
{
  public override Type GetBAccountIDCommand()
  {
    return typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>);
  }

  public override Type GetEmailMessageTarget()
  {
    return typeof (Select<ARContact, Where<ARContact.contactID, Equal<Current<ARInvoice.billContactID>>>>);
  }
}
