// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.PrimaryGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

[PXHidden]
[PXProjection(typeof (Select<GroupOrganizationLink, Where<BqlOperand<GroupOrganizationLink.primaryGroup, IBqlBool>.IsEqual<True>>>))]
public class PrimaryGroup : GroupOrganizationLink
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Primary Group")]
  [PXSelector(typeof (Search<Organization.organizationID, Where<BqlOperand<Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>), new Type[] {typeof (Organization.organizationCD), typeof (Organization.organizationName)}, SubstituteKey = typeof (Organization.organizationCD))]
  public override int? GroupID { get; set; }

  public new abstract class organizationID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.GL.DAC.PrimaryGroup.organizationID>
  {
  }

  public new abstract class groupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.GL.DAC.PrimaryGroup.groupID>
  {
  }
}
