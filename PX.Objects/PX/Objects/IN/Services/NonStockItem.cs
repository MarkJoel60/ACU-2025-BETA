// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Services.NonStockItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.Services;

[PXHidden]
public class NonStockItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected int? _COGSAcctID;
  protected int? _COGSSubID;

  [PXInt]
  [PXUIField(DisplayName = "Expense Accrual Account")]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Expense Accrual Sub.")]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Expense Account")]
  public virtual int? COGSAcctID
  {
    get => this._COGSAcctID;
    set => this._COGSAcctID = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Expense Sub.")]
  public virtual int? COGSSubID
  {
    get => this._COGSSubID;
    set => this._COGSSubID = value;
  }

  public abstract class invtAcctID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  NonStockItem.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NonStockItem.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NonStockItem.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NonStockItem.cOGSSubID>
  {
  }
}
