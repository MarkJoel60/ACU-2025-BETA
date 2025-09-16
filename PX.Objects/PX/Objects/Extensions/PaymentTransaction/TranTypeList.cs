// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.TranTypeList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;

#nullable disable
namespace PX.Objects.Extensions.PaymentTransaction;

public class TranTypeList : PXStringListAttribute
{
  public const string AUTCode = "AUT";
  public const string AACCode = "AAC";
  public const string PACCode = "PAC";
  public const string CDTCode = "CDT";
  public const string VDGCode = "VDG";
  public const string UKNCode = "UKN";
  private const string AUTTypeName = "Authorize Only";
  private const string AACTypeName = "Authorize and Capture";
  private const string PACTypeName = "Capture Authorized";
  private const string CDTTypeName = "Refund";
  private const string VDGTypeName = "Void";
  private const string UKNTypeName = "Unknown";
  private static (CCTranType, string)[] mapping = new (CCTranType, string)[6]
  {
    (CCTranType.AuthorizeAndCapture, "AAC"),
    (CCTranType.AuthorizeOnly, "AUT"),
    (CCTranType.PriorAuthorizedCapture, "PAC"),
    (CCTranType.Credit, "CDT"),
    (CCTranType.Void, "VDG"),
    (CCTranType.Unknown, "UKN")
  };

  public TranTypeList()
    : base(TranTypeList.GetCommonInputTypes())
  {
  }

  public static Tuple<string, string>[] GetCommonInputTypes()
  {
    return new Tuple<string, string>[6]
    {
      PXStringListAttribute.Pair("AUT", "Authorize Only"),
      PXStringListAttribute.Pair("AAC", "Authorize and Capture"),
      PXStringListAttribute.Pair("PAC", "Capture Authorized"),
      PXStringListAttribute.Pair("CDT", "Refund"),
      PXStringListAttribute.Pair("VDG", "Void"),
      PXStringListAttribute.Pair("UKN", "Unknown")
    };
  }

  public static Tuple<string, string>[] GetCreditInputType()
  {
    return new Tuple<string, string>[1]
    {
      PXStringListAttribute.Pair("CDT", "Refund")
    };
  }

  public static string GetStrCodeByTranType(CCTranType tranType)
  {
    string strCodeByTranType = (string) null;
    bool flag = false;
    foreach ((CCTranType, string) tuple in TranTypeList.mapping)
    {
      if (tuple.Item1 == tranType)
      {
        strCodeByTranType = tuple.Item2;
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new PXInvalidOperationException();
    return strCodeByTranType;
  }

  public static CCTranType GetTranTypeByStrCode(string strCode)
  {
    bool flag = false;
    CCTranType tranTypeByStrCode = CCTranType.AuthorizeAndCapture;
    foreach ((CCTranType, string) tuple in TranTypeList.mapping)
    {
      if (tuple.Item2 == strCode)
      {
        tranTypeByStrCode = tuple.Item1;
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new PXInvalidOperationException();
    return tranTypeByStrCode;
  }
}
