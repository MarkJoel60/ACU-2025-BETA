// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA;

public static class PaymentMethodType
{
  public const 
  #nullable disable
  string EFT = "EFT";
  public const string CreditCard = "CCD";
  public const string CashOrCheck = "CHC";
  public const string DirectDeposit = "DDT";
  public const string POSTerminal = "POS";
  public const string ExternalPaymentProcessor = "EPP";

  private static (string, string)[] GetValuesAndLabels()
  {
    List<(string, string)> valueTupleList = new List<(string, string)>();
    if (PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>())
      valueTupleList.Add(("EFT", "EFT"));
    valueTupleList.AddRange((IEnumerable<(string, string)>) new (string, string)[4]
    {
      ("CCD", "Credit Card"),
      ("CHC", "Cash/Check"),
      ("DDT", "Direct Deposit"),
      ("POS", "POS Terminal")
    });
    if (PXAccess.FeatureInstalled<FeaturesSet.paymentProcessor>())
      valueTupleList.Add(("EPP", "External Payment Processor"));
    return valueTupleList.ToArray();
  }

  public class ListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
  {
    public ListAttribute()
      : base(PaymentMethodType.GetValuesAndLabels())
    {
    }

    public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      PaymentMethod row = e.Row as PaymentMethod;
      PXStringListAttribute.SetList(sender, (object) row, ((PXEventSubscriberAttribute) this).FieldName, PaymentMethodType.GetValuesAndLabels());
    }
  }

  public class eft : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodType.eft>
  {
    public eft()
      : base("EFT")
    {
    }
  }

  public class creditCard : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodType.creditCard>
  {
    public creditCard()
      : base("CCD")
    {
    }
  }

  public class cashOrCheck : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodType.cashOrCheck>
  {
    public cashOrCheck()
      : base("CHC")
    {
    }
  }

  public class directDeposit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodType.directDeposit>
  {
    public directDeposit()
      : base("DDT")
    {
    }
  }

  public class posTerminal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodType.posTerminal>
  {
    public posTerminal()
      : base("POS")
    {
    }
  }

  public class externalPaymentProcessor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaymentMethodType.externalPaymentProcessor>
  {
    public externalPaymentProcessor()
      : base("EPP")
    {
    }
  }
}
