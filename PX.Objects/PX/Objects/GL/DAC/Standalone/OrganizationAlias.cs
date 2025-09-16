// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.Standalone.OrganizationAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.DAC.Standalone;

[PXCacheName("Company")]
[Serializable]
public class OrganizationAlias : PX.Objects.GL.DAC.Organization
{
  public new abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    OrganizationAlias.organizationID>
  {
  }

  public new abstract class organizationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationAlias.organizationCD>
  {
  }

  public new abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationAlias.baseCuryID>
  {
  }
}
