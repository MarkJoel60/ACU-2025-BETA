// Decompiled with JetBrains decompiler
// Type: PX.SM.Branch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _BranchCD;
  private string _RoleName;
  protected byte[] _tstamp;

  [PXDBIdentity]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDimensionSelector("BRANCH", typeof (Search<Branch.branchCD>), typeof (Branch.branchCD))]
  [PXUIField(DisplayName = "Branch", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string BranchCD
  {
    get => this._BranchCD;
    set => this._BranchCD = value;
  }

  [PXUIField(DisplayName = "Role Name")]
  [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true, InputMask = "")]
  public string RoleName
  {
    get => this._RoleName;
    set => this._RoleName = value;
  }

  [PXDBString(IsUnicode = true, InputMask = "")]
  public string LogoName { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  public string MainLogoName { get; set; }

  [PXDBInt]
  public virtual int? OrganizationID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<Branch>.By<Branch.branchID>
  {
    public static Branch Find(PXGraph graph, int? branchID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Branch>.By<Branch.branchID>.FindBy(graph, (object) branchID, options);
    }
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

  public abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.roleName>
  {
  }

  public abstract class logoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.logoName>
  {
  }

  public abstract class mainLogoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.mainLogoName>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.organizationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Branch.Tstamp>
  {
  }
}
