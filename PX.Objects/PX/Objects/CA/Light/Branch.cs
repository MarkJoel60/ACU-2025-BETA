// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.Branch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

/// <exclude />
[PXHidden]
[Serializable]
public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  [PXDBInt]
  public virtual int? OrganizationID { get; set; }

  [PXDBIdentity]
  public virtual int? BranchID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual 
  #nullable disable
  string BranchCD { get; set; }

  [PXDBBool]
  public bool? Active { get; set; }

  [PXDBInt]
  public virtual int? LedgerID { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string CountryID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string BaseCuryID { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.bAccountID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.branchID>
  {
  }

  public abstract class branchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.branchCD>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.active>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.ledgerID>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.countryID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.baseCuryID>
  {
  }
}
