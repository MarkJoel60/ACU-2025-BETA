// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntryActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARPaymentEntryActivityDetailsExt : 
  PMActivityDetailsExt<ARPaymentEntry, ARPayment, ARPayment.noteID>
{
  public override Type GetBAccountIDCommand()
  {
    return typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARPayment.customerID>>>>);
  }
}
