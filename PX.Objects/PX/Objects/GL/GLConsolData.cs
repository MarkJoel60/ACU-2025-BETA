// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolData
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

[PXCacheName("GL Consolidation Data")]
[Serializable]
public class GLConsolData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AccountCD;
  protected string _MappedValue;
  protected string _FinPeriodID;
  protected Decimal? _ConsolAmtCredit;
  protected Decimal? _ConsolAmtDebit;

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Account")]
  public virtual string AccountCD
  {
    get => this._AccountCD;
    set => this._AccountCD = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Mapped Sub.")]
  public virtual string MappedValue
  {
    get => this._MappedValue;
    set => this._MappedValue = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, IsDBField = false)]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Credit Amount")]
  public virtual Decimal? ConsolAmtCredit
  {
    get => this._ConsolAmtCredit;
    set => this._ConsolAmtCredit = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Debit Amount")]
  public virtual Decimal? ConsolAmtDebit
  {
    get => this._ConsolAmtDebit;
    set => this._ConsolAmtDebit = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Mapped Sub. Length")]
  public virtual int? MappedValueLength { get; set; }

  public class PK : PrimaryKeyOf<GLConsolData>.By<GLConsolData.accountCD, GLConsolData.mappedValue>
  {
    public static GLConsolData Find(
      PXGraph graph,
      string accountCD,
      string mappedValue,
      PKFindOptions options = 0)
    {
      return (GLConsolData) PrimaryKeyOf<GLConsolData>.By<GLConsolData.accountCD, GLConsolData.mappedValue>.FindBy(graph, (object) accountCD, (object) mappedValue, options);
    }
  }

  public abstract class accountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolData.accountCD>
  {
  }

  public abstract class mappedValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolData.mappedValue>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolData.finPeriodID>
  {
  }

  public abstract class consolAmtCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLConsolData.consolAmtCredit>
  {
  }

  public abstract class consolAmtDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLConsolData.consolAmtDebit>
  {
  }

  public abstract class mappedValueLength : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLConsolData.mappedValueLength>
  {
  }
}
