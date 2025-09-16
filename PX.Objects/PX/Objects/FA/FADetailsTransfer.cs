// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADetailsTransfer
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

[PXProjection(typeof (Select2<FADetails, LeftJoin<FABookBalanceUpdateGL, On<FABookBalanceUpdateGL.assetID, Equal<FADetails.assetID>>, LeftJoin<FABookBalanceNonUpdateGL, On<FABookBalanceNonUpdateGL.assetID, Equal<FADetails.assetID>>, LeftJoin<FABookBalanceTransfer, On<FABookBalanceTransfer.assetID, Equal<FADetails.assetID>, And<FABookBalanceTransfer.bookID, Equal<IsNull<FABookBalanceUpdateGL.bookID, FABookBalanceNonUpdateGL.bookID>>>>>>>>), new Type[] {typeof (FADetails)})]
[PXCacheName("FA Details")]
public class FADetailsTransfer : FADetails
{
  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FixedAsset" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the fixed asset.
  /// It is a required value.
  /// By default, the value is set to the current fixed asset identifier.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  public override int? AssetID { get; set; }

  /// <summary>
  /// The identifier of the transfer period.
  /// This is an unbound service field that is used to pass the parameter to transfer processing.
  /// </summary>
  [PXDBString(6, IsFixed = true, BqlField = typeof (FABookBalanceTransfer.transferPeriodID))]
  [PXUIField(DisplayName = "Transfer Period", Enabled = false)]
  [FinPeriodIDFormatting]
  public virtual 
  #nullable disable
  string TransferPeriodID { get; set; }

  /// <summary>
  /// The cost of the fixed asset in the current depreciation period.
  /// </summary>
  /// <value>
  /// The value is read-only and is selected from the appropriate <see cref="T:PX.Objects.FA.FABookHistoryRecon.ytdAcquired" /> field.
  /// </value>
  [PXDBBaseCury(null, null, BqlField = typeof (FADetails.currentCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Basis", Enabled = false)]
  public override Decimal? CurrentCost { get; set; }

  /// <summary>
  /// The reconciled part of the current cost of the fixed asset.
  /// </summary>
  /// <value>
  /// The value is read-only and is selected from the appropriate <see cref="T:PX.Objects.FA.FABookHistoryRecon.ytdReconciled" /> field.
  /// </value>
  [PXDBBaseCury(null, null, BqlField = typeof (FADetails.accrualBalance))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? AccrualBalance { get; set; }

  public new abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetailsTransfer.assetID>
  {
  }

  public abstract class transferPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetailsTransfer.transferPeriodID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetailsTransfer.status>
  {
  }

  public new abstract class locationRevID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADetailsTransfer.locationRevID>
  {
  }

  public new abstract class currentCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetailsTransfer.currentCost>
  {
  }

  public new abstract class accrualBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetailsTransfer.accrualBalance>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetailsTransfer.receiptDate>
  {
  }

  public new abstract class tagNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetailsTransfer.tagNbr>
  {
  }
}
