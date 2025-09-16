// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementUpdate_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.AR;

public class ARStatementUpdate_ActivityDetailsExt : 
  ActivityDetailsExt<ARStatementUpdate, Customer, Customer.noteID>
{
  public override System.Type GetBAccountIDCommand()
  {
    return typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARStatement.customerID>>>>);
  }

  public override System.Type GetEmailMessageTarget()
  {
    return typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Customer.defBillContactID>>>>);
  }
}
