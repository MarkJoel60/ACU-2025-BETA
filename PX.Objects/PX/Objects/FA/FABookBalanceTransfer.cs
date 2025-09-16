// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookBalanceTransfer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select2<PX.Objects.FA.Standalone.FABookBalance, LeftJoin<FABookBalanceNext, On<FABookBalanceNext.assetID, Equal<PX.Objects.FA.Standalone.FABookBalance.assetID>, And<FABookBalanceNext.bookID, Equal<PX.Objects.FA.Standalone.FABookBalance.bookID>>>, LeftJoin<FABook, On<FABook.bookID, Equal<PX.Objects.FA.Standalone.FABookBalance.bookID>>>>, Where<PX.Objects.FA.Standalone.FABookBalance.currDeprPeriod, IsNotNull, Or<PX.Objects.FA.Standalone.FABookBalance.lastDeprPeriod, IsNotNull>>>))]
[PXHidden]
[Serializable]
public class FABookBalanceTransfer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected bool? _UpdateGL;

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.assetID))]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.bookID))]
  [PXDefault]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBBool(BqlField = typeof (FABook.updateGL))]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  [PXDBCalced(typeof (IsNull<FABookBalance.currDeprPeriod, FABookBalanceNext.finPeriodID>), typeof (string))]
  [FinPeriodIDFormatting]
  public virtual 
  #nullable disable
  string TransferPeriodID { get; set; }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceTransfer.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceTransfer.bookID>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookBalanceTransfer.updateGL>
  {
  }

  public abstract class transferPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalanceTransfer.transferPeriodID>
  {
  }
}
