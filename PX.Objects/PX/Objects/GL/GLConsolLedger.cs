// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolLedger
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Consolidation Ledger")]
[Serializable]
public class GLConsolLedger : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SetupID;
  protected 
  #nullable disable
  string _LedgerCD;
  protected bool? _PostInterCompany;
  protected string _Description;

  [PXDBInt(IsKey = true)]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXDefault]
  public virtual string LedgerCD
  {
    get => this._LedgerCD;
    set => this._LedgerCD = value;
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXDBBool]
  [PXUIField(DisplayName = "Generates Inter-Branch Transactions", Enabled = false)]
  public virtual bool? PostInterCompany
  {
    get => this._PostInterCompany;
    set => this._PostInterCompany = value;
  }

  /// <summary>
  /// The type of balance of the ledger in the source company.
  /// </summary>
  /// <value>
  /// For more info see the <see cref="P:PX.Objects.GL.Ledger.BalanceType" /> field.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [LedgerBalanceType.List]
  [PXUIField(DisplayName = "Balance Type", Enabled = false)]
  public virtual string BalanceType { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public class PK : PrimaryKeyOf<GLConsolHistory>.By<GLConsolLedger.setupID, GLConsolLedger.ledgerCD>
  {
    public static GLConsolHistory Find(
      PXGraph graph,
      int? setupID,
      string ledgerCD,
      PKFindOptions options = 0)
    {
      return (GLConsolHistory) PrimaryKeyOf<GLConsolHistory>.By<GLConsolLedger.setupID, GLConsolLedger.ledgerCD>.FindBy(graph, (object) setupID, (object) ledgerCD, options);
    }
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolLedger.setupID>
  {
  }

  public abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolLedger.ledgerCD>
  {
  }

  public abstract class postInterCompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLConsolLedger.postInterCompany>
  {
  }

  public abstract class balanceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolLedger.balanceType>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolLedger.description>
  {
  }
}
