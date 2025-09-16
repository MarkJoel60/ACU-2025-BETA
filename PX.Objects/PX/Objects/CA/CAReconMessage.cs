// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CAReconMessage : InfoMessage
{
  protected int _KeyCashAccount;
  protected 
  #nullable disable
  string _KeyReconNbr;

  public CAReconMessage(
    int aCashAccountID,
    string aReconNbr,
    PXErrorLevel aLevel,
    string aMessage)
    : base(aLevel, aMessage)
  {
    this._KeyCashAccount = aCashAccountID;
    this._KeyReconNbr = aReconNbr;
  }

  public virtual int KeyCashAccount
  {
    get => this._KeyCashAccount;
    set => this._KeyCashAccount = value;
  }

  public virtual string KeyReconNbr
  {
    get => this._KeyReconNbr;
    set => this._KeyReconNbr = value;
  }

  public abstract class keyCashAccount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAReconMessage.keyCashAccount>
  {
  }
}
