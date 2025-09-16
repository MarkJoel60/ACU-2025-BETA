// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.CCProcTranHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

public static class CCProcTranHelper
{
  public static IEnumerable<ICCPaymentTransaction> FilterDeclinedTrans(
    IEnumerable<ICCPaymentTransaction> ccProcTrans)
  {
    bool declineAfterReviewFound = false;
    string declinedPCTranNbr = (string) null;
    foreach (ICCPaymentTransaction ccProcTran in ccProcTrans)
    {
      ICCPaymentTransaction tran = ccProcTran;
      if (!declineAfterReviewFound && tran.TranStatus == "DEC" && ccProcTrans.Any<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => i.PCTranNumber == tran.PCTranNumber && i.TranStatus == "HFR")))
      {
        declineAfterReviewFound = true;
        declinedPCTranNbr = tran.PCTranNumber;
      }
      else if (declineAfterReviewFound && tran.PCTranNumber == declinedPCTranNbr)
      {
        if (tran.TranStatus == "HFR")
          declineAfterReviewFound = false;
      }
      else if (tran.TranStatus != "DEC")
        yield return tran;
    }
  }

  public static bool IsExpired(ICCPaymentTransaction tran)
  {
    return tran.ExpirationDate.HasValue && tran.ExpirationDate.Value < PXTimeZoneInfo.Now || tran.TranStatus == "EXP";
  }

  public static bool HasOpenCCTran(
    IEnumerable<ICCPaymentTransaction> paymentTransaction)
  {
    return paymentTransaction.Any<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => i.ProcStatus == "OPN" && !CCProcTranHelper.IsExpired(i)));
  }

  public static ICCPaymentTransaction FindCCLastSuccessfulTran(
    IEnumerable<ICCPaymentTransaction> ccProcTran)
  {
    IEnumerable<ICCPaymentTransaction> paymentTransactions = CCProcTranHelper.FilterDeclinedTrans(ccProcTran);
    ICCPaymentTransaction pt2 = (ICCPaymentTransaction) null;
    CCProcTranHelper.CCProcTranOrderComparer tranOrderComparer = new CCProcTranHelper.CCProcTranOrderComparer();
    foreach (ICCPaymentTransaction pt1 in paymentTransactions)
    {
      if (!(pt1.ProcStatus != "FIN") && (pt1.TranStatus == "APR" || pt1.TranStatus == "HFR") && (pt2 == null || tranOrderComparer.Compare(pt1, pt2) > 0))
        pt2 = pt1;
    }
    return pt2;
  }

  public static ICCPaymentTransaction FindOpenForReviewTran(
    IEnumerable<ICCPaymentTransaction> ccProcTran)
  {
    return CCProcTranHelper.FilterDeclinedTrans(ccProcTran).Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => i.TranStatus == "HFR")).Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => !ccProcTran.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (ii =>
    {
      if (!(ii.PCTranNumber == i.PCTranNumber))
        return false;
      int? tranNbr1 = ii.TranNbr;
      int? tranNbr2 = i.TranNbr;
      return !(tranNbr1.GetValueOrDefault() == tranNbr2.GetValueOrDefault() & tranNbr1.HasValue == tranNbr2.HasValue);
    })).Any<ICCPaymentTransaction>())).FirstOrDefault<ICCPaymentTransaction>();
  }

  public static IEnumerable<ICCPaymentTransaction> FindAuthCaptureActiveTrans(
    IEnumerable<ICCPaymentTransaction> ccProcTran)
  {
    IOrderedEnumerable<ICCPaymentTransaction> orderedEnumerable = CCProcTranHelper.FilterDeclinedTrans(ccProcTran).OrderBy<ICCPaymentTransaction, int?>((Func<ICCPaymentTransaction, int?>) (i => i.TranNbr));
    List<ICCPaymentTransaction> source = new List<ICCPaymentTransaction>();
    foreach (ICCPaymentTransaction paymentTransaction1 in (IEnumerable<ICCPaymentTransaction>) orderedEnumerable)
    {
      ICCPaymentTransaction item = paymentTransaction1;
      if (!(item.ProcStatus == "ERR") && !(item.TranStatus == "ERR"))
      {
        if (item.TranType == "AAC" || item.TranType == "AUT" && item.TranStatus != "EXP" || item.TranType == "IAA" && item.TranStatus != "EXP" || item.TranType == "CAP" || item.TranType == "PAC")
        {
          ICCPaymentTransaction paymentTransaction2 = source.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i =>
          {
            if (item.RefTranNbr.HasValue)
            {
              int? refTranNbr = item.RefTranNbr;
              int? tranNbr = i.TranNbr;
              if (refTranNbr.GetValueOrDefault() == tranNbr.GetValueOrDefault() & refTranNbr.HasValue == tranNbr.HasValue)
                return true;
            }
            return i.PCTranNumber == item.PCTranNumber;
          })).FirstOrDefault<ICCPaymentTransaction>();
          if (paymentTransaction2 != null)
            source.Remove(paymentTransaction2);
          source.Add(item);
        }
        if (item.TranType == "VDG" || item.TranType == "REJ")
        {
          ICCPaymentTransaction paymentTransaction3 = source.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => i.PCTranNumber == item.PCTranNumber)).FirstOrDefault<ICCPaymentTransaction>();
          if (paymentTransaction3 != null)
            source.Remove(paymentTransaction3);
        }
        if (item.TranType == "CDT")
        {
          ICCPaymentTransaction paymentTransaction4 = source.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i =>
          {
            int? tranNbr = i.TranNbr;
            int? refTranNbr = item.RefTranNbr;
            return tranNbr.GetValueOrDefault() == refTranNbr.GetValueOrDefault() & tranNbr.HasValue == refTranNbr.HasValue;
          })).FirstOrDefault<ICCPaymentTransaction>();
          if (paymentTransaction4 != null)
            source.Remove(paymentTransaction4);
        }
        if ((item.TranType == "AUT" || item.TranType == "IAA") && item.TranStatus == "EXP")
        {
          ICCPaymentTransaction paymentTransaction5 = source.Where<ICCPaymentTransaction>((Func<ICCPaymentTransaction, bool>) (i => i.PCTranNumber == item.PCTranNumber)).FirstOrDefault<ICCPaymentTransaction>();
          if (paymentTransaction5 != null)
            source.Remove(paymentTransaction5);
        }
      }
    }
    return (IEnumerable<ICCPaymentTransaction>) source;
  }

  public static bool IsActiveTran(CCProcTran procTran)
  {
    bool flag = true;
    if (procTran.TranStatus == "DEC" || procTran.TranStatus == "APR" && EnumerableExtensions.IsIn<string>(procTran.TranType, "VDG", "REJ"))
      flag = false;
    if ((!procTran.RefTranNbr.HasValue || procTran.TranType == "CDT") && procTran.TranStatus == "ERR")
      flag = false;
    if (procTran.TranStatus == "EXP")
      flag = false;
    return flag;
  }

  public static bool IsCompletedTran(CCProcTran procTran)
  {
    bool flag = false;
    if (procTran.TranStatus == "APR" && (procTran.TranType == "AAC" || procTran.TranType == "CAP" || procTran.TranType == "PAC" || procTran.TranType == "CDT"))
      flag = true;
    return flag;
  }

  public static bool IsNeedSync(CCProcTran procTran)
  {
    return procTran.TranStatus == "APR" && procTran.Imported.GetValueOrDefault();
  }

  public static bool IsFailedTran(CCProcTran procTran)
  {
    return procTran.TranStatus == "DEC" || procTran.TranStatus == "ERR";
  }

  public static bool HasCaptureFailed(IEnumerable<ICCPaymentTransaction> trans)
  {
    bool flag = false;
    foreach (ICCPaymentTransaction tran in trans)
    {
      if (EnumerableExtensions.IsIn<string>(tran.TranType, "VDG", "CAP", "PAC", "AAC", "REJ", Array.Empty<string>()))
      {
        if (EnumerableExtensions.IsIn<string>(tran.TranStatus, "APR", "HFR"))
          break;
      }
      if (EnumerableExtensions.IsIn<string>(tran.TranType, "PAC", "AAC") && (tran.ProcStatus == "ERR" || EnumerableExtensions.IsIn<string>(tran.TranStatus, "ERR", "DEC")))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public class CCProcTranOrderComparer : IComparer<CCProcTran>, IComparer<ICCPaymentTransaction>
  {
    private bool _descending;

    public CCProcTranOrderComparer(bool aDescending) => this._descending = aDescending;

    public CCProcTranOrderComparer() => this._descending = false;

    public virtual int Compare(CCProcTran x, CCProcTran y)
    {
      int num = x.TranNbr.Value.CompareTo(y.TranNbr.Value);
      return !this._descending ? num : -num;
    }

    public virtual int Compare(ICCPaymentTransaction pt1, ICCPaymentTransaction pt2)
    {
      int num = pt1.TranNbr.Value.CompareTo(pt2.TranNbr.Value);
      return !this._descending ? num : -num;
    }
  }
}
