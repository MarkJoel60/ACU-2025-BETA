// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.QueryParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;

#nullable enable
namespace PX.Objects.Common.DAC;

public class QueryParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false)]
  public int? OrganizationID { get; set; }

  [Branch(null, null, true, false, true)]
  public int? BranchID { get; set; }

  public abstract class organizationID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  QueryParameters.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  QueryParameters.branchID>
  {
  }
}
