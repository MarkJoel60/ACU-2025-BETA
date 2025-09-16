// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Transact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class Transact : FATran
{
  protected Decimal? _DebitAmt;
  protected Decimal? _CreditAmt;
  protected int? _AccountID;
  protected int? _SubID;

  [PXBaseCury]
  [PXUIField(DisplayName = "Debit")]
  [PXDefault]
  public virtual Decimal? DebitAmt
  {
    get => this._DebitAmt;
    set => this._DebitAmt = value;
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Credit")]
  [PXDefault]
  public virtual Decimal? CreditAmt
  {
    get => this._CreditAmt;
    set => this._CreditAmt = value;
  }

  [Account(DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description), IsDBField = false)]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(DisplayName = "Subaccount", IsDBField = false)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  public abstract class debitAmt : BqlType<IBqlDecimal, Decimal>.Field<
  #nullable disable
  Transact.debitAmt>
  {
  }

  public abstract class creditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Transact.creditAmt>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Transact.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Transact.subID>
  {
  }
}
