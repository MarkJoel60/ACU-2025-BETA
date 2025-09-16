// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.EPEquipmentActiveProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isCompleted, Equal<False>>), "The {0} project or contract is completed.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
public class EPEquipmentActiveProjectAttribute : PXEntityAttribute, IPXFieldVerifyingSubscriber
{
  public EPEquipmentActiveProjectAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", typeof (Search2<PMProject.contractID, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProject.customerID>>, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>>, Where<PMProject.baseType, Equal<CTPRType.project>, And<Match<Current<AccessInfo.userName>>>>, OrderBy<Desc<PMProject.contractCD>>>), typeof (PMProject.contractCD), new Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
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
      DescriptionDisplayName = "Project Description",
      ValidComboRequired = true,
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (sender.Graph == null)
      throw new ArgumentNullException("graph");
    this.Visible = this.Enabled = ProjectAttribute.IsPMVisible("TA");
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    PMProject pmProject = PMProject.PK.Find(sender.Graph, (int?) e.NewValue);
    if (pmProject == null || !pmProject.RestrictToResourceList.GetValueOrDefault())
      return;
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelectJoin<PMProject, LeftJoin<EPEquipmentRate, On<EPEquipmentRate.projectID, Equal<PMProject.contractID>, And<EPEquipmentRate.equipmentID, Equal<Current<EPEquipmentTimeCard.equipmentID>>>>>, Where<EPEquipmentRate.projectID, Equal<Required<PMProject.contractID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) pmProject.ContractID
    })) == null)
      throw new PXSetPropertyException("This project is not available for current equipment");
  }
}
