// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.APExternalPaymentPocessorType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

public class APExternalPaymentPocessorType
{
  public const string BillCom = "BC";

  public static string GetLabel(string type)
  {
    (string, string) tuple1 = ((IEnumerable<(string, string)>) APExternalPaymentPocessorType.ListAttribute.GetTypes).FirstOrDefault<(string, string)>((Func<(string, string), bool>) (p => p.Item1 == type));
    (string, string) tuple2 = tuple1;
    return !(tuple2.Item1 != (string) null) && !(tuple2.Item2 != (string) null) ? type : tuple1.Item2;
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(APExternalPaymentPocessorType.ListAttribute.GetTypes)
    {
    }

    public static (string, string)[] GetTypes
    {
      get => new (string, string)[1]{ ("BC", "Bill.com") };
    }
  }

  public class billCom : BqlType<IBqlString, string>.Constant<APExternalPaymentPocessorType.billCom>
  {
    public billCom()
      : base("BC")
    {
    }
  }
}
