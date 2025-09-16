// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ActiveContractBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<Contract.baseType, NotEqual<CTPRType.projectTemplate>, And<Contract.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the contract instead.", new Type[] {typeof (Contract.contractCD)})]
[PXRestrictor(typeof (Where<Contract.baseType, Equal<CTPRType.contract>, Or<Contract.nonProject, Equal<True>>>), "{0} is reserved for a project. Specify the contract instead.", new Type[] {typeof (Contract.contractCD)})]
[PXRestrictor(typeof (Where<Contract.isCancelled, Equal<False>>), "The {0} contract is canceled.", new Type[] {typeof (Contract.contractCD)})]
[PXRestrictor(typeof (Where<Contract.isCompleted, Equal<False>>), "The {0} contract is completed.", new Type[] {typeof (Contract.contractCD)})]
[PXRestrictor(typeof (Where<Contract.isActive, Equal<True>, Or<Contract.nonProject, Equal<True>>>), "The {0} contract is inactive.", new Type[] {typeof (Contract.contractCD)})]
public class ActiveContractBaseAttribute : PXEntityAttribute, IPXFieldVerifyingSubscriber
{
  protected Type customerField;

  public ActiveContractBaseAttribute()
    : this((Type) null)
  {
  }

  public ActiveContractBaseAttribute(Type customerField)
  {
    this.customerField = customerField;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", typeof (Search2<Contract.contractID, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>, Where<Contract.nonProject, Equal<True>, Or<Match<Current<AccessInfo.userName>>>>>), typeof (Contract.contractCD), new Type[6]
    {
      typeof (Contract.contractCD),
      typeof (Contract.description),
      typeof (Contract.status),
      typeof (Contract.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (Contract.curyID)
    })
    {
      DescriptionField = typeof (Contract.description),
      ValidComboRequired = true,
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract>.Config>.Search<Contract.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (!(this.customerField != (Type) null) || contract == null || contract.NonProject.GetValueOrDefault())
      return;
    int? nullable = (int?) sender.GetValue(e.Row, this.customerField.Name);
    int? customerId = contract.CustomerID;
    if (nullable.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable.HasValue == customerId.HasValue)
      return;
    sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException("The customer in the selected project or contract differs from the customer in the current document.", (PXErrorLevel) 2));
  }
}
