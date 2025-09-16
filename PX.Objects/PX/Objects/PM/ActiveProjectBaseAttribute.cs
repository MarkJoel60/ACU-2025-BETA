// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ActiveProjectBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project instead.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<Contract.baseType, Equal<CTPRType.project>, Or<Contract.nonProject, Equal<True>>>), "{0} is reserved for a contract. Specify the project instead.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<Contract.isCancelled, Equal<False>>), "The {0} project is canceled.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isCompleted, Equal<False>>), "The {0} project is completed.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The {0} project is inactive.", new Type[] {typeof (PMProject.contractCD)})]
public class ActiveProjectBaseAttribute : PXEntityAttribute, IPXFieldVerifyingSubscriber
{
  protected Type customerField;

  public ActiveProjectBaseAttribute()
    : this((Type) null)
  {
  }

  public ActiveProjectBaseAttribute(Type customerField)
  {
    this.customerField = customerField;
    List<Type> typeList = new List<Type>();
    typeList.AddRange((IEnumerable<Type>) new Type[14]
    {
      typeof (Search2<,,>),
      typeof (PMProject.contractID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (Equal<>),
      typeof (PMProject.customerID),
      typeof (LeftJoin<,>),
      typeof (PMSetup),
      typeof (On<,>),
      typeof (True),
      typeof (Equal<>),
      typeof (True)
    });
    if (customerField != (Type) null)
      typeList.AddRange((IEnumerable<Type>) new Type[32 /*0x20*/]
      {
        typeof (Where<,,>),
        typeof (PMProject.nonProject),
        typeof (Equal<>),
        typeof (True),
        typeof (Or2<,>),
        typeof (Where2<,>),
        typeof (Where<,,>),
        typeof (PMProject.customerID),
        typeof (Equal<>),
        typeof (Current<>),
        customerField,
        typeof (And<,,>),
        typeof (PMProject.restrictProjectSelect),
        typeof (Equal<>),
        typeof (PMRestrictOption.customerProjects),
        typeof (Or<,,>),
        typeof (PMProject.restrictProjectSelect),
        typeof (Equal<>),
        typeof (PMRestrictOption.allProjects),
        typeof (Or<,>),
        typeof (PMProject.baseType),
        typeof (Equal<CTPRType.contract>),
        typeof (Or<,>),
        typeof (Current<>),
        customerField,
        typeof (IsNull),
        typeof (And2<,>),
        typeof (Match<Current<AccessInfo.userName>>),
        typeof (Or<,>),
        typeof (PMProject.nonProject),
        typeof (Equal<>),
        typeof (True)
      });
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", BqlCommand.Compose(typeList.ToArray()), typeof (PMProject.contractCD), new Type[6]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.status),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description),
      ValidComboRequired = true,
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (!(this.customerField != (Type) null) || pmProject == null || pmProject.NonProject.GetValueOrDefault())
      return;
    int? nullable1 = (int?) sender.GetValue(e.Row, this.customerField.Name);
    int? nullable2 = nullable1;
    int? customerId = pmProject.CustomerID;
    if (nullable2.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable2.HasValue == customerId.HasValue || !nullable1.HasValue)
      return;
    sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The customer in the selected project or contract differs from the customer in the current document.", (PXErrorLevel) 2));
  }
}
