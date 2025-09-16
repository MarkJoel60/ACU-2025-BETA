// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Consolidation Branch")]
[Serializable]
public class GLConsolBranch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SetupID;
  protected 
  #nullable disable
  string _BranchCD;
  protected string _LedgerCD;
  protected string _Description;

  [PXDBInt(IsKey = true)]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDefault]
  public virtual string BranchCD
  {
    get => this._BranchCD;
    set => this._BranchCD = value;
  }

  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [PXDefault]
  public virtual string OrganizationCD { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  public virtual string LedgerCD
  {
    get => this._LedgerCD;
    set => this._LedgerCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsOrganization { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField]
  [PXFormula(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLConsolBranch.organizationCD, IsNotNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLConsolBranch.isOrganization, IsNull>>>>.Or<BqlOperand<GLConsolBranch.isOrganization, IBqlBool>.IsEqual<False>>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<GLConsolBranch.organizationCD, HyphenSpace>>, IBqlString>.Concat<GLConsolBranch.branchCD>, GLConsolBranch.branchCD>))]
  public virtual string DisplayName { get; set; }

  public class PK : PrimaryKeyOf<GLConsolBranch>.By<GLConsolBranch.setupID, GLConsolBranch.branchCD>
  {
    public static GLConsolBranch Find(
      PXGraph graph,
      int? setupID,
      string branchCD,
      PKFindOptions options = 0)
    {
      return (GLConsolBranch) PrimaryKeyOf<GLConsolBranch>.By<GLConsolBranch.setupID, GLConsolBranch.branchCD>.FindBy(graph, (object) setupID, (object) branchCD, options);
    }
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolBranch.setupID>
  {
  }

  public abstract class branchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolBranch.branchCD>
  {
  }

  public abstract class organizationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLConsolBranch.organizationCD>
  {
  }

  public abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolBranch.ledgerCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolBranch.description>
  {
  }

  public abstract class isOrganization : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLConsolBranch.isOrganization>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolBranch.displayName>
  {
  }
}
