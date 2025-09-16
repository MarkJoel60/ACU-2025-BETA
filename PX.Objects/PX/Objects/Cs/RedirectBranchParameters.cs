// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RedirectBranchParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXHidden]
[Serializable]
public class RedirectBranchParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? OrganizationID { get; set; }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    RedirectBranchParameters.organizationID>
  {
  }
}
