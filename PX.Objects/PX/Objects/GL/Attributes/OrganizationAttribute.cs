// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.OrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.DAC;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBInt]
[PXInt]
[PXUIField(DisplayName = "Company", FieldClass = "COMPANYBRANCH")]
public class OrganizationAttribute : PXEntityAttribute
{
  public const string _DimensionName = "COMPANY";
  protected static readonly Type _selectorSource = typeof (Search<PX.Objects.GL.DAC.Organization.organizationID, Where<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>, And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>>);
  protected static readonly Type _defaultingSource = typeof (Search2<PX.Objects.GL.DAC.Organization.organizationID, InnerJoin<Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>>, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>, And<MatchWithBranch<Branch.branchID>>>>);

  public OrganizationAttribute(bool onlyActive = true)
    : this(onlyActive, OrganizationAttribute._selectorSource, OrganizationAttribute._defaultingSource)
  {
  }

  public OrganizationAttribute(bool onlyActive, Type defaultingSource)
    : this(onlyActive, OrganizationAttribute._selectorSource, defaultingSource)
  {
  }

  public OrganizationAttribute(bool onlyActive, Type selectorSource, Type defaultingSource)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("COMPANY", selectorSource, typeof (PX.Objects.GL.DAC.Organization.organizationCD), new Type[2]
    {
      typeof (PX.Objects.GL.DAC.Organization.organizationCD),
      typeof (PX.Objects.GL.DAC.Organization.organizationName)
    })
    {
      ValidComboRequired = true,
      DescriptionField = typeof (PX.Objects.GL.DAC.Organization.organizationName)
    });
    ((PXAggregateAttribute) this)._Attributes.Add(defaultingSource != (Type) null ? (PXEventSubscriberAttribute) new PXDefaultAttribute(defaultingSource) : (PXEventSubscriberAttribute) new PXDefaultAttribute());
    if (onlyActive)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PX.Objects.GL.DAC.Organization.active, Equal<True>>), "The company is inactive.", Array.Empty<Type>()));
    this.Initialize();
  }
}
