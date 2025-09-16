// Decompiled with JetBrains decompiler
// Type: PX.Data.Licensing.Branch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Licensing;

[PXHidden]
[Serializable]
public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  public abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Branch.branchID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.bAccountID>
  {
  }
}
