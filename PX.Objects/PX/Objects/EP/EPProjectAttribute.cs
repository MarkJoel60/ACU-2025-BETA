// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new Type[] {typeof (PMProject.contractCD)})]
[PXAttributeFamily(typeof (PXEntityAttribute))]
public class EPProjectAttribute : PXEntityAttribute
{
  protected Type OwnerFieldType;

  public EPProjectAttribute(Type ownerFieldType) => this.Initialize(ownerFieldType, false);

  public EPProjectAttribute(Type ownerFieldType, bool IsCustomErrorMessage)
  {
    this.Initialize(ownerFieldType, IsCustomErrorMessage);
  }

  private void Initialize(Type ownerFieldType, bool IsCustomErrorMessage)
  {
    this.OwnerFieldType = ownerFieldType;
    PXDimensionSelectorAttribute selectorAttribute = new PXDimensionSelectorAttribute("PROJECT", this.BuildSearchClause(), typeof (PMProject.contractCD), new Type[3]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.status)
    });
    selectorAttribute.DescriptionField = typeof (PMProject.description);
    selectorAttribute.ValidComboRequired = true;
    selectorAttribute.CacheGlobal = true;
    if (IsCustomErrorMessage)
      selectorAttribute.CustomMessageValueDoesntExistOrNoRights = PXMessages.LocalizeFormat("The {0} employee has no access to the {1} project. To provide access, add the employee to the list of project employees on the Employees tab of the Projects (PM301000) form.", new object[2]
      {
        (object) PXAccess.GetUserName(),
        (object) "{1}"
      });
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    this.Enabled = ProjectAttribute.IsPMVisible("TA");
  }

  protected virtual Type BuildSearchClause()
  {
    return BqlCommand.Compose(new Type[5]
    {
      typeof (Search2<,,,>),
      typeof (PMProject.contractID),
      this.BuildJoinClause(),
      this.BuildWhereClause(),
      this.BuildOrderByClause()
    });
  }

  protected virtual Type BuildJoinClause()
  {
    return BqlCommand.Compose(new Type[8]
    {
      typeof (LeftJoin<,,>),
      typeof (EPEmployee),
      typeof (On<,>),
      typeof (EPEmployee.defContactID),
      typeof (Equal<>),
      typeof (Current2<>),
      this.OwnerFieldType,
      typeof (LeftJoin<EPEmployeeContract, On<EPEmployeeContract.contractID, Equal<PMProject.contractID>, And<EPEmployeeContract.employeeID, Equal<EPEmployee.bAccountID>>>>)
    });
  }

  protected virtual Type BuildWhereClause()
  {
    return typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And2<Where<PMProject.restrictToEmployeeList, Equal<False>, Or<EPEmployeeContract.employeeID, IsNotNull>>, And2<Match<Current<AccessInfo.userName>>, And<Where<PMProject.visibleInTA, Equal<True>, Or<PMProject.nonProject, Equal<True>>>>>>>);
  }

  protected virtual Type BuildOrderByClause() => typeof (OrderBy<Desc<PMProject.contractCD>>);
}
