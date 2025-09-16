// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractAR677000
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CT;

public class ContractAR677000 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDimensionSelector("CONTRACT", typeof (Search2<Contract.contractCD, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Where<Contract.templateID, Equal<Optional<Contract.templateID>>, Or<Optional<Contract.templateID>, IsNull>>>>>), typeof (Contract.contractCD), new Type[] {typeof (Contract.contractCD), typeof (Contract.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (Contract.locationID), typeof (Contract.description), typeof (Contract.status), typeof (Contract.expireDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (Contract.description), Filterable = true)]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public string ContractCD { get; set; }
}
