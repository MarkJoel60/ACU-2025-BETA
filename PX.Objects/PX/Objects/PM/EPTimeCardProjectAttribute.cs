// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.EPTimeCardProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, NotEqual<True>>), "The {0} project or contract is completed.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.visibleInTA, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
public class EPTimeCardProjectAttribute : PXEntityAttribute
{
  public EPTimeCardProjectAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", typeof (Search2<PMProject.contractID, LeftJoin<EPEmployeeContract, On<EPEmployeeContract.contractID, Equal<PMProject.contractID>, And<EPEmployeeContract.employeeID, Equal<Current<EPTimeCard.employeeID>>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<PMProject.customerID>>, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>>>, Where<PMProject.baseType, Equal<CTPRType.project>, And2<Where<PMProject.restrictToEmployeeList, Equal<False>, Or<EPEmployeeContract.employeeID, IsNotNull>>, And<Match<Current<AccessInfo.userName>>>>>, OrderBy<Desc<PMProject.contractCD>>>), typeof (PMProject.contractCD), new System.Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (BAccountR.acctName),
      typeof (PMProject.locationID),
      typeof (PMProject.status),
      typeof (PMProject.ownerID),
      typeof (PMProject.startDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description),
      ValidComboRequired = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
    this.CacheGlobal = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (sender.Graph == null)
      throw new ArgumentNullException("graph");
    this.Visible = this.Enabled = ProjectAttribute.IsPMVisible("TA");
  }
}
