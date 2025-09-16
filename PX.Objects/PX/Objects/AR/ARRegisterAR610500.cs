// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterAR610500
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

[Serializable]
public class ARRegisterAR610500 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXSelector(typeof (Search2<ARRegister.refNbr, InnerJoinSingleTable<Customer, On<ARRegister.customerID, Equal<Customer.bAccountID>>>, Where<ARRegister.docType, Equal<Optional<ARRegister.docType>>, And2<Where<ARRegister.finPeriodID, GreaterEqual<Optional<ARRegister.finPeriodID>>, Or<Optional<ARRegister.closedFinPeriodID>, IsNull>>, And2<Where<ARRegister.finPeriodID, LessEqual<Optional<ARRegister.tranPeriodID>>, Or<Optional<ARRegister.closedTranPeriodID>, IsNull>>, And2<Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>>>, OrderBy<Desc<ARRegister.refNbr>>>), Filterable = true)]
  public string RefNbr { get; set; }
}
