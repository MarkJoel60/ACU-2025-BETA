// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.GroupOrganizationLink
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
[Serializable]
public class GroupOrganizationLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? GroupID { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? OrganizationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  public virtual bool? PrimaryGroup { get; set; }

  public abstract class groupID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  GroupOrganizationLink.groupID>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GroupOrganizationLink.organizationID>
  {
  }

  public abstract class primaryGroup : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GroupOrganizationLink.primaryGroup>
  {
  }
}
