// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PerUnitTax.PerUnitTaxesPostOnReleaseExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.Extensions.PerUnitTax;

/// <summary>
/// A per unit taxes post on AP/AR release graph extension base class.
/// </summary>
public abstract class PerUnitTaxesPostOnReleaseExt<TReleaseGraph, TDocument, TLine, TLineTax, TAggregatedTax> : 
  PXGraphExtension<TReleaseGraph>
  where TReleaseGraph : PXGraph<TReleaseGraph>
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
  where TLineTax : TaxDetail, IBqlTable
  where TAggregatedTax : TaxTran, new()
{
  protected static bool IsActiveBase()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.perUnitTaxSupport>();
  }

  protected virtual void AggregatedTax_RowPersisting(PX.Data.Events.RowPersisting<TAggregatedTax> e)
  {
    PX.Objects.TX.Tax tax = this.GetTax((TaxDetail) e.Row);
    if (tax == null)
      return;
    PXPersistingCheck persistingCheck = tax.TaxType != "Q" || tax.PerUnitTaxPostMode == "T" ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing;
    e.Cache.Adjust<PXDefaultAttribute>((object) e.Row).For<TaxTran.accountID>((System.Action<PXDefaultAttribute>) (a => a.PersistingCheck = persistingCheck)).SameFor<TaxTran.subID>();
  }

  /// <summary>
  /// Gets a tax from <see cref="T:PX.Objects.TX.TaxDetail" />.
  /// </summary>
  /// <param name="taxDetail">The taxDetail to act on.</param>
  /// <returns />
  private PX.Objects.TX.Tax GetTax(TaxDetail taxDetail)
  {
    if (taxDetail == null)
      return (PX.Objects.TX.Tax) null;
    return (PX.Objects.TX.Tax) PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) taxDetail.TaxID);
  }

  [PXOverride]
  public void PostPerUnitTaxAmounts(
    JournalEntry journalEntry,
    TDocument document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    TAggregatedTax perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
    if (!this.CheckInputDocument(document) || !this.CheckPerUnitTax(perUnitTax))
      return;
    this.CreateAndPostGLTransactions(journalEntry, document, newCurrencyInfo, perUnitAggregatedTax, perUnitTax, isDebitTaxTran);
  }

  protected virtual bool CheckInputDocument(TDocument document)
  {
    ExceptionExtensions.ThrowOnNull<TDocument>(document, nameof (document), (string) null);
    return true;
  }

  protected virtual bool CheckPerUnitTax(PX.Objects.TX.Tax perUnitTax)
  {
    return perUnitTax?.TaxCalcType == "I";
  }

  protected virtual void CreateAndPostGLTransactions(
    JournalEntry journalEntry,
    TDocument document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    TAggregatedTax perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
    switch (perUnitTax.PerUnitTaxPostMode)
    {
      case "T":
        this.CreateAndPostGLTransactionsOnTaxAccount(journalEntry, document, newCurrencyInfo, perUnitAggregatedTax, perUnitTax, isDebitTaxTran);
        break;
      case "L":
        this.CreateAndPostGLTransactionsOnLineAccounts(journalEntry, document, newCurrencyInfo, perUnitAggregatedTax, perUnitTax, isDebitTaxTran);
        break;
    }
  }

  protected abstract void CreateAndPostGLTransactionsOnTaxAccount(
    JournalEntry journalEntry,
    TDocument document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    TAggregatedTax perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran);

  private void CreateAndPostGLTransactionsOnLineAccounts(
    JournalEntry journalEntry,
    TDocument document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    TAggregatedTax perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
    foreach ((TLineTax lineTax, TLine line) in this.GetTaxWithLines(perUnitTax, perUnitAggregatedTax))
    {
      GLTran forPerUnitLineTax = this.CreateGLTranForPerUnitLineTax(document, newCurrencyInfo, perUnitAggregatedTax, line, lineTax, isDebitTaxTran);
      this.InsertNewGLTran(journalEntry, document, line, perUnitAggregatedTax, lineTax, forPerUnitLineTax);
    }
  }

  protected abstract IEnumerable<(TLineTax Tax, TLine Line)> GetTaxWithLines(
    PX.Objects.TX.Tax perUnitTax,
    TAggregatedTax perUnitAggregatedTax);

  protected virtual GLTran CreateGLTranForPerUnitLineTax(
    TDocument document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    TAggregatedTax perUnitAggregatedTax,
    TLine docLine,
    TLineTax perUnitLineTax,
    bool isDebitTaxTran)
  {
    GLTran forPerUnitLineTax = new GLTran()
    {
      CuryInfoID = newCurrencyInfo.CuryInfoID,
      TranType = perUnitAggregatedTax.TranType,
      TranClass = "T",
      RefNbr = perUnitAggregatedTax.RefNbr,
      TranDate = perUnitAggregatedTax.TranDate,
      TranDesc = perUnitLineTax.TaxID,
      Released = new bool?(true)
    };
    if (isDebitTaxTran)
    {
      forPerUnitLineTax.CuryCreditAmt = new Decimal?(0M);
      forPerUnitLineTax.CreditAmt = new Decimal?(0M);
    }
    else
    {
      forPerUnitLineTax.CuryDebitAmt = new Decimal?(0M);
      forPerUnitLineTax.DebitAmt = new Decimal?(0M);
    }
    return forPerUnitLineTax;
  }

  protected abstract GLTran InsertNewGLTran(
    JournalEntry journalEntry,
    TDocument document,
    TLine docLine,
    TAggregatedTax perUnitAggregatedTax,
    TLineTax perUnitLineTax,
    GLTran newGlTran);

  /// <summary>
  /// A hack to fill a delegate field of graph extension with appropriate delegate signature with a call to the protected method of the graph.
  /// </summary>
  /// <typeparam name="TDelegateType">Type of the delegate type.</typeparam>
  /// <param name="protectedMethodName">Name of the protected method of the graph to call.</param>
  /// <param name="protectedMethodField">[out] The delegate field of the graph extension.</param>
  protected void FillProtectedMethodDelegate<TDelegateType>(
    string protectedMethodName,
    out TDelegateType protectedMethodField)
    where TDelegateType : class
  {
    if (string.IsNullOrWhiteSpace(protectedMethodName))
    {
      protectedMethodField = default (TDelegateType);
    }
    else
    {
      System.Type type1 = typeof (TDelegateType);
      System.Type[] typeArray1 = Array.Empty<System.Type>();
      System.Type[] typeArray2;
      if (type1.IsGenericType)
      {
        typeArray2 = type1.GetGenericArguments();
        if (type1.Name.StartsWith("Func"))
          typeArray2 = ((IEnumerable<System.Type>) typeArray2).Take<System.Type>(typeArray2.Length - 1).ToArray<System.Type>();
      }
      else
      {
        MethodInfo method = type1.GetMethod("Invoke");
        typeArray2 = ((object) method != null ? ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (parameterInfo => parameterInfo.ParameterType)).ToArray<System.Type>() : (System.Type[]) null) ?? typeArray1;
      }
      System.Type type2 = this.Base.GetType();
      MethodInfo method1 = type2.GetMethod(protectedMethodName, BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, typeArray2, (ParameterModifier[]) null);
      if ((object) method1 == null)
        method1 = CustomizedTypeManager.GetTypeNotCustomized(type2)?.GetMethod(protectedMethodName, BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, typeArray2, (ParameterModifier[]) null);
      MethodInfo methodInfo = method1;
      protectedMethodField = methodInfo?.CreateDelegate(typeof (TDelegateType), (object) this.Base) as TDelegateType;
    }
  }
}
