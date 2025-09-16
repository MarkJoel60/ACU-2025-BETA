// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GraphExtensions.CABankTransactionsEnqSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA.GraphExtensions;

public class CABankTransactionsEnqSplit : PXGraphExtension<
#nullable disable
CABankTransactionsEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankTransactionSplits>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<CABankTransactionsEnq.CABankTranHistory>) this.Base.Result).OrderByNew<OrderBy<Asc<CABankTransactionsEnq.CABankTranHistory.extRefNbr, Asc<CABankTransactionsEnq.CABankTranHistory.sortOrder, Asc<CABankTransactionsEnq.CABankTranHistory.tranID>>>>>();
  }

  [PXOverride]
  public virtual Dictionary<Type, Type> GetMapperDictionary(
    CABankTransactionsEnqSplit.GetMapperDictionaryDelegate baseMethod)
  {
    Dictionary<Type, Type> mapperDictionary = baseMethod();
    mapperDictionary.Add(typeof (CABankTransactionsEnqSplit.CABankTranHistorySplit.splitted), typeof (CABankTranSplit.splitted));
    mapperDictionary.Add(typeof (CABankTransactionsEnqSplit.CABankTranHistorySplit.parentTranID), typeof (CABankTranSplit.parentTranID));
    mapperDictionary.Add(typeof (CABankTransactionsEnqSplit.CABankTranHistorySplit.splittedIcon), typeof (CABankTranSplit.splittedIcon));
    mapperDictionary.Add(typeof (CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigCreditAmt), typeof (CABankTranSplit.curyOrigCreditAmt));
    mapperDictionary.Add(typeof (CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigDebitAmt), typeof (CABankTranSplit.curyOrigDebitAmt));
    return mapperDictionary;
  }

  [PXDBCalced(typeof (IsNull<CABankTranSplit.parentTranID, CABankTran.tranID>), typeof (int))]
  public void _(Events.CacheAttached<CABankTran.sortOrder> e)
  {
  }

  public void _(
    Events.RowSelected<CABankTransactionsEnq.CABankTranHistory> e)
  {
    CABankTransactionsEnq.CABankTranHistory row = e.Row;
    CABankTransactionsEnqSplit.CABankTranHistorySplit extension = row != null ? PXCacheEx.GetExtension<CABankTransactionsEnqSplit.CABankTranHistorySplit>((IBqlTable) row) : (CABankTransactionsEnqSplit.CABankTranHistorySplit) null;
    bool flag = extension != null && extension.ParentTranID.HasValue;
    if (!flag)
      return;
    PXUIFieldAttribute.SetVisible<CABankTransactionsEnqSplit.CABankTranHistorySplit.splittedIcon>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTransactionsEnq.CABankTranHistory>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisibility<CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigCreditAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTransactionsEnq.CABankTranHistory>>) e).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigDebitAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTransactionsEnq.CABankTranHistory>>) e).Cache, (object) null, (PXUIVisibility) 3);
  }

  public sealed class CABankTranHistorySplit : 
    PXCacheExtension<CABankTransactionsEnq.CABankTranHistory>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankTransactionSplits>();

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is splitted.
    /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
    /// </summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Split", Visible = true, Enabled = false)]
    public bool? Splitted { get; set; }

    /// <summary>
    /// The unique identifier of the CA bank transaction.
    /// This field is the key field.
    /// </summary>
    [PXUIField(DisplayName = "ID", Visible = false)]
    [PXDBInt]
    public int? ParentTranID { get; set; }

    [PXUIField(DisplayName = "Split", IsReadOnly = true, Visible = false)]
    [PXImage]
    public string SplittedIcon { get; set; }

    /// <summary>
    /// The amount of the original receipt in the selected currency.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXCury(typeof (CABankTran.curyID))]
    [PXUIField(DisplayName = "Orig. Receipt", Enabled = false, Visible = false)]
    public Decimal? CuryOrigDebitAmt { get; set; }

    /// <summary>
    /// The amount of the original disbursement in the selected currency.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXCury(typeof (CABankTran.curyID))]
    [PXUIField(DisplayName = "Orig. Disbursement", Enabled = false, Visible = false)]
    public Decimal? CuryOrigCreditAmt { get; set; }

    public abstract class splitted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnqSplit.CABankTranHistorySplit.splitted>
    {
    }

    public abstract class parentTranID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnqSplit.CABankTranHistorySplit.parentTranID>
    {
    }

    public abstract class splittedIcon : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnqSplit.CABankTranHistorySplit.splittedIcon>
    {
    }

    public abstract class curyOrigDebitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigDebitAmt>
    {
    }

    public abstract class curyOrigCreditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnqSplit.CABankTranHistorySplit.curyOrigCreditAmt>
    {
    }
  }

  public delegate Dictionary<Type, Type> GetMapperDictionaryDelegate();
}
