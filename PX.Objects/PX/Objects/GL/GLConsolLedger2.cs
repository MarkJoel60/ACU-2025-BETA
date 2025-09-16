// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolLedger2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.GL;

[PXProjection(typeof (Select<GLConsolLedger>))]
public class GLConsolLedger2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SetupID;
  protected 
  #nullable disable
  string _LedgerCD;

  [PXDBInt(IsKey = true, BqlField = typeof (GLConsolLedger.setupID))]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, BqlField = typeof (GLConsolLedger.ledgerCD))]
  [PXDefault]
  public virtual string LedgerCD
  {
    get => this._LedgerCD;
    set => this._LedgerCD = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (GLConsolLedger.balanceType))]
  public virtual string BalanceType { get; set; }

  public class PK : 
    PrimaryKeyOf<GLConsolHistory>.By<GLConsolLedger2.setupID, GLConsolLedger2.ledgerCD>
  {
    public static GLConsolHistory Find(
      PXGraph graph,
      int? setupID,
      string ledgerCD,
      PKFindOptions options = 0)
    {
      return (GLConsolHistory) PrimaryKeyOf<GLConsolHistory>.By<GLConsolLedger2.setupID, GLConsolLedger2.ledgerCD>.FindBy(graph, (object) setupID, (object) ledgerCD, options);
    }
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolLedger2.setupID>
  {
  }

  public abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolLedger2.ledgerCD>
  {
  }

  public abstract class balanceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolLedger2.balanceType>
  {
  }
}
