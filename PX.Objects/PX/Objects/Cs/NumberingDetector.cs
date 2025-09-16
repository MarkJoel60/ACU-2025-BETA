// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NumberingDetector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.EP;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.RQ;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CS;

public class NumberingDetector
{
  private readonly Dictionary<Type, PXSelectBase> setupNumberingSequences;
  private readonly PXGraph parentGraph;

  public NumberingDetector(PXGraph graph, ApplicationAreas areas)
  {
    this.parentGraph = graph;
    this.setupNumberingSequences = new Dictionary<Type, PXSelectBase>();
    if (areas.HasFlag((Enum) ApplicationAreas.StandardFinance))
    {
      PXSelect<ARSetup> pxSelect1 = new PXSelect<ARSetup>(graph);
      PXSelect<APSetup> pxSelect2 = new PXSelect<APSetup>(graph);
      PXSelect<CASetup> pxSelect3 = new PXSelect<CASetup>(graph);
      PXSelect<GLSetup> pxSelect4 = new PXSelect<GLSetup>(graph);
      PXSelect<TXSetup> pxSelect5 = new PXSelect<TXSetup>(graph);
      this.setupNumberingSequences.Add(typeof (ARSetup.batchNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.creditAdjNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.debitAdjNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.finChargeNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.invoiceNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.paymentNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.priceWSNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.usageNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.writeOffNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (ARSetup.prepaymentInvoiceNumberingID), (PXSelectBase) pxSelect1);
      this.setupNumberingSequences.Add(typeof (APSetup.batchNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (APSetup.creditAdjNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (APSetup.debitAdjNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (APSetup.checkNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (APSetup.invoiceNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (APSetup.priceWSNumberingID), (PXSelectBase) pxSelect2);
      this.setupNumberingSequences.Add(typeof (CASetup.batchNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (CASetup.cABatchNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (CASetup.cAStatementNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (CASetup.registerNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (CASetup.transferNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (CASetup.corpCardNumberingID), (PXSelectBase) pxSelect3);
      this.setupNumberingSequences.Add(typeof (GLSetup.batchNumberingID), (PXSelectBase) pxSelect4);
      this.setupNumberingSequences.Add(typeof (GLSetup.allocationNumberingID), (PXSelectBase) pxSelect4);
      this.setupNumberingSequences.Add(typeof (GLSetup.docBatchNumberingID), (PXSelectBase) pxSelect4);
      this.setupNumberingSequences.Add(typeof (GLSetup.scheduleNumberingID), (PXSelectBase) pxSelect4);
      this.setupNumberingSequences.Add(typeof (GLSetup.tBImportNumberingID), (PXSelectBase) pxSelect4);
      this.setupNumberingSequences.Add(typeof (TXSetup.taxAdjustmentNumberingID), (PXSelectBase) pxSelect5);
      this.setupNumberingSequences.Add(typeof (PX.Objects.CA.CashAccount.reconNumberingID), (PXSelectBase) new PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.reconNumberingID, Equal<Required<Numbering.numberingID>>>>(graph));
    }
    if (areas.HasFlag((Enum) ApplicationAreas.AdvancedFinance))
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>())
      {
        PXSelect<FASetup> pxSelect = new PXSelect<FASetup>(graph);
        this.setupNumberingSequences.Add(typeof (FASetup.registerNumberingID), (PXSelectBase) pxSelect);
        this.setupNumberingSequences.Add(typeof (FASetup.assetNumberingID), (PXSelectBase) pxSelect);
        this.setupNumberingSequences.Add(typeof (FASetup.batchNumberingID), (PXSelectBase) pxSelect);
        this.setupNumberingSequences.Add(typeof (FASetup.tagNumberingID), (PXSelectBase) pxSelect);
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      {
        PXSelect<CMSetup> pxSelect = new PXSelect<CMSetup>(graph);
        this.setupNumberingSequences.Add(typeof (CMSetup.batchNumberingID), (PXSelectBase) pxSelect);
        this.setupNumberingSequences.Add(typeof (CMSetup.translNumberingID), (PXSelectBase) pxSelect);
        this.setupNumberingSequences.Add(typeof (CMSetup.extRefNbrNumberingID), (PXSelectBase) pxSelect);
      }
    }
    if (areas.HasFlag((Enum) ApplicationAreas.Distribution) && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      PXSelect<POSetup> pxSelect6 = new PXSelect<POSetup>(graph);
      this.setupNumberingSequences.Add(typeof (POSetup.standardPONumberingID), (PXSelectBase) pxSelect6);
      this.setupNumberingSequences.Add(typeof (POSetup.regularPONumberingID), (PXSelectBase) pxSelect6);
      if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      {
        this.setupNumberingSequences.Add(typeof (POSetup.receiptNumberingID), (PXSelectBase) pxSelect6);
        PXSelect<INSetup> pxSelect7 = new PXSelect<INSetup>(graph);
        this.setupNumberingSequences.Add(typeof (INSetup.batchNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.receiptNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.issueNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.adjustmentNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.kitAssemblyNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.pINumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.replenishmentNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (INSetup.manufacturingNumberingID), (PXSelectBase) pxSelect7);
        this.setupNumberingSequences.Add(typeof (SOSetup.shipmentNumberingID), (PXSelectBase) new PXSelect<SOSetup>(graph));
      }
      PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.invoiceNumberingID, Equal<Required<Numbering.numberingID>>>> pxSelect8 = new PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.invoiceNumberingID, Equal<Required<Numbering.numberingID>>>>(graph);
      PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderNumberingID, Equal<Required<Numbering.numberingID>>>> pxSelect9 = new PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderNumberingID, Equal<Required<Numbering.numberingID>>>>(graph);
      this.setupNumberingSequences.Add(typeof (PX.Objects.SO.SOOrderType.invoiceNumberingID), (PXSelectBase) pxSelect8);
      this.setupNumberingSequences.Add(typeof (PX.Objects.SO.SOOrderType.orderNumberingID), (PXSelectBase) pxSelect9);
      PXSelect<RQSetup> pxSelect10 = new PXSelect<RQSetup>(graph);
      this.setupNumberingSequences.Add(typeof (RQSetup.requestNumberingID), (PXSelectBase) pxSelect10);
      this.setupNumberingSequences.Add(typeof (RQSetup.requisitionNumberingID), (PXSelectBase) pxSelect10);
    }
    if (areas.HasFlag((Enum) ApplicationAreas.Project) && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      PXSelect<PMSetup> pxSelect = new PXSelect<PMSetup>(graph);
      this.setupNumberingSequences.Add(typeof (PMSetup.tranNumbering), (PXSelectBase) pxSelect);
      this.setupNumberingSequences.Add(typeof (PMSetup.batchNumberingID), (PXSelectBase) pxSelect);
    }
    if (!areas.HasFlag((Enum) ApplicationAreas.TimeAndExpenses))
      return;
    PXSelect<EPSetup> pxSelect11 = new PXSelect<EPSetup>(graph);
    this.setupNumberingSequences.Add(typeof (EPSetup.claimNumberingID), (PXSelectBase) pxSelect11);
    this.setupNumberingSequences.Add(typeof (EPSetup.receiptNumberingID), (PXSelectBase) pxSelect11);
    this.setupNumberingSequences.Add(typeof (EPSetup.timeCardNumberingID), (PXSelectBase) pxSelect11);
    this.setupNumberingSequences.Add(typeof (EPSetup.equipmentTimeCardNumberingID), (PXSelectBase) pxSelect11);
  }

  /// <summary>
  /// Checks whether subsequences of two numbering sequences can intersect.
  /// </summary>
  /// <returns>If at least one subsequence of the first numbering sequence can intersect
  /// with a subsequence of the second numbering sequence, the method returns <tt>True</tt>;
  /// otherwise, the method returns <tt>False</tt>.</returns>
  public static bool CanNumberingIntersect(
    string firstNumberingID,
    string secondNumberingID,
    PXGraph graph)
  {
    foreach (PXResult<NumberingSequence> pxResult1 in PXSelectBase<NumberingSequence, PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select(graph, new object[1]
    {
      (object) firstNumberingID
    }))
    {
      NumberingSequence firstSequence = PXResult<NumberingSequence>.op_Implicit(pxResult1);
      foreach (PXResult<NumberingSequence> pxResult2 in PXSelectBase<NumberingSequence, PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select(graph, new object[1]
      {
        (object) secondNumberingID
      }))
      {
        NumberingSequence secondSequence = PXResult<NumberingSequence>.op_Implicit(pxResult2);
        if (NumberingDetector.CanSequencesIntersect(firstSequence, secondSequence))
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Checks whether the specified numbering subsequences can intersect.
  /// </summary>
  /// <returns>If the specified numbering subsequences can intersect, the method returns <tt>True</tt>;
  /// otherwise, the method returns <tt>False</tt>.</returns>
  protected static bool CanSequencesIntersect(
    NumberingSequence firstSequence,
    NumberingSequence secondSequence)
  {
    string nbr1 = (string) null;
    string str1 = NumberingDetector.MakeMask(firstSequence.StartNbr, ref nbr1);
    string nbr2 = (string) null;
    string str2 = NumberingDetector.MakeMask(secondSequence.StartNbr, ref nbr2);
    if (str1 != str2)
      return false;
    string nbr3 = (string) null;
    NumberingDetector.MakeMask(firstSequence.EndNbr, ref nbr3);
    string nbr4 = (string) null;
    NumberingDetector.MakeMask(secondSequence.EndNbr, ref nbr4);
    long num1 = long.Parse(nbr1);
    long num2 = long.Parse(nbr3);
    long num3 = long.Parse(nbr2);
    long num4 = long.Parse(nbr4);
    return num1 >= num3 && num1 <= num4 || num3 >= num1 && num3 <= num2;
  }

  /// <summary>Extracts a mask from the specified string.</summary>
  /// <param name="str">An input string.</param>
  /// <param name="nbr">A number to be extracted from the input string.</param>
  /// <returns>The method returns a string with the extracted mask, such as <c>abc999</c>.</returns>
  public static string MakeMask(string str, ref string nbr)
  {
    bool flag = true;
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    for (int length = str.Length; length > 0; --length)
    {
      if (Regex.IsMatch(str.Substring(length - 1, 1), "[^0-9]"))
        flag = false;
      if (flag)
      {
        stringBuilder1.Append(Regex.Replace(str.Substring(length - 1, 1), "[0-9]", "9"));
        stringBuilder2.Append(str.Substring(length - 1, 1));
      }
      else
        stringBuilder1.Append(str[length - 1]);
    }
    char[] charArray1 = stringBuilder2.ToString().ToCharArray();
    Array.Reverse((Array) charArray1);
    nbr = new string(charArray1);
    char[] charArray2 = stringBuilder1.ToString().ToCharArray();
    Array.Reverse((Array) charArray2);
    return new string(charArray2);
  }

  /// <summary>
  /// Searches for a reference to <paramref name="numberingID" /> in Setup tables.
  /// </summary>
  /// <param name="numberingID">The identifier of the numbering sequence to search for.</param>
  /// <param name="cacheName">The name of the Setup table in which the reference has been found.</param>
  /// <param name="fieldName">The display name of the field that contains the reference.</param>
  /// <returns>If a Setup table with the reference to the specified <paramref name="numberingID" /> has
  /// been found, the method returns <tt>True</tt>; otherwise, the method returns <tt>False</tt>.</returns>
  public bool IsInUseSetups(string numberingID, out string cacheName, out string fieldName)
  {
    return this.IsInUseCustom(numberingID, out cacheName, out fieldName, this.setupNumberingSequences);
  }

  /// <summary>
  /// Searches for a reference to <paramref name="numberingID" /> in segmented keys.
  /// </summary>
  /// <param name="numberingID">The identifier of the numbering sequence to search for.</param>
  /// <param name="demensionID">The ID of the segmented key in which the reference has been found.</param>
  /// <param name="segmentID">The ID of the segment in which the reference has been found.</param>
  /// <returns>If a segmented key with the reference to the specified <paramref name="numberingID" /> has
  /// been found, the method returns <tt>True</tt>; otherwise, the method returns <tt>False</tt>.</returns>
  public bool IsInUseSegments(string numberingID, out string demensionID, out string segmentID)
  {
    using (IEnumerator<PXResult<Dimension>> enumerator = PXSelectBase<Dimension, PXSelectJoin<Dimension, InnerJoin<Segment, On<Segment.dimensionID, Equal<Dimension.dimensionID>>>, Where<Dimension.numberingID, Equal<Optional<Numbering.numberingID>>, And<Segment.autoNumber, Equal<Optional<Segment.autoNumber>>>>>.Config>.Select(this.parentGraph, new object[2]
    {
      (object) numberingID,
      (object) true
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<Dimension, Segment> current = (PXResult<Dimension, Segment>) enumerator.Current;
        PXResult<Dimension, Segment>.op_Implicit(current);
        Segment segment = PXResult<Dimension, Segment>.op_Implicit(current);
        demensionID = segment.DimensionID;
        segmentID = segment.SegmentID.ToString();
        return true;
      }
    }
    demensionID = (string) null;
    segmentID = (string) null;
    return false;
  }

  /// <summary>
  /// Searches for reference to <paramref name="numberingID" /> in the fields, which are specified in <paramref name="sequences" />.
  /// </summary>
  /// <param name="numberingID">The identifier of the numbering sequence to search for.</param>
  /// <param name="cacheName">The name of the cache in which the reference has been found.</param>
  /// <param name="fieldName">The display name of the field that contains the reference.</param>
  /// <param name="sequences">The dictionary that contains the field type and PXSelectBase,
  /// which is used to select a field of the specified type.</param>
  /// <returns>If a field with the reference to the specified <paramref name="numberingID" /> has been found, the method
  /// returns <tt>True</tt>; otherwise, the method returns <tt>False</tt>.</returns>
  private bool IsInUseCustom(
    string numberingID,
    out string cacheName,
    out string fieldName,
    Dictionary<Type, PXSelectBase> sequences)
  {
    foreach (KeyValuePair<Type, PXSelectBase> sequence in sequences)
    {
      PXSelectBase pxSelectBase = sequence.Value;
      Type key = sequence.Key;
      object obj = pxSelectBase.View.SelectSingle(new object[1]
      {
        (object) numberingID
      });
      if (obj != null && (string) pxSelectBase.Cache.GetValue(obj, key.Name) == numberingID)
      {
        cacheName = pxSelectBase.Cache.DisplayName;
        fieldName = PXUIFieldAttribute.GetDisplayName(pxSelectBase.Cache, key.Name);
        return true;
      }
    }
    cacheName = (string) null;
    fieldName = (string) null;
    return false;
  }
}
