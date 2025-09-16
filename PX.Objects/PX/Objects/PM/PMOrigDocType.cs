// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMOrigDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMOrigDocType
{
  public const 
  #nullable disable
  string Allocation = "AL";
  public const string Timecard = "TC";
  public const string Case = "CS";
  public const string ExpenseClaim = "EC";
  public const string EquipmentTimecard = "ET";
  public const string AllocationReversal = "AR";
  public const string Reversal = "RV";
  public const string ARInvoice = "IN";
  public const string CreditMemo = "CR";
  public const string DebitMemo = "DM";
  public const string UnbilledRemainder = "UR";
  public const string UnbilledRemainderReversal = "RR";
  public const string ProformaBilling = "PB";
  public const string APBill = "BL";
  public const string CreditAdjustment = "CA";
  public const string DebitAdjustment = "DA";
  public const string WipReversal = "WR";
  public const string ServiceOrder = "AP";
  public const string Appointment = "SO";
  public const string RegularPaycheck = "PR";
  public const string SpecialPaycheck = "PS";
  public const string AdjustmentPaycheck = "PA";
  public const string VoidPaycheck = "PV";
  public const string FinalPaycheck = "PF";

  public static string GetOrigDocType(string pmOrigDocType)
  {
    switch (pmOrigDocType)
    {
      case "IN":
        return "INV";
      case "CR":
        return "CRM";
      case "DM":
        return "DRM";
      default:
        return (string) null;
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[24]
      {
        PXStringListAttribute.Pair("AL", "Allocation"),
        PXStringListAttribute.Pair("TC", "Time Card"),
        PXStringListAttribute.Pair("CS", "Case"),
        PXStringListAttribute.Pair("EC", "Expense Claim"),
        PXStringListAttribute.Pair("ET", "Equipment Time Card"),
        PXStringListAttribute.Pair("AR", "Allocation Reversal"),
        PXStringListAttribute.Pair("RV", "Reversal"),
        PXStringListAttribute.Pair("IN", "AR Invoice"),
        PXStringListAttribute.Pair("CR", "Credit Memo"),
        PXStringListAttribute.Pair("DM", "Debit Memo"),
        PXStringListAttribute.Pair("BL", "Bill"),
        PXStringListAttribute.Pair("CA", "Credit Adjustment"),
        PXStringListAttribute.Pair("DA", "Debit Adjustment"),
        PXStringListAttribute.Pair("UR", "Unbilled Remainder"),
        PXStringListAttribute.Pair("RR", "Unbilled Remainder Reversal"),
        PXStringListAttribute.Pair("PB", "Pro Forma Billing"),
        PXStringListAttribute.Pair("WR", "WIP Reversal"),
        PXStringListAttribute.Pair("AP", "Service Order"),
        PXStringListAttribute.Pair("SO", "Appointment"),
        PXStringListAttribute.Pair("PR", "Regular Paycheck"),
        PXStringListAttribute.Pair("PS", "Special Paycheck"),
        PXStringListAttribute.Pair("PA", "Adjustment Paycheck"),
        PXStringListAttribute.Pair("PV", "Void Paycheck"),
        PXStringListAttribute.Pair("PF", "Final Paycheck")
      })
    {
    }
  }

  public class ListARAttribute : PXStringListAttribute
  {
    public ListARAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("IN", "AR Invoice"),
        PXStringListAttribute.Pair("CR", "Credit Memo"),
        PXStringListAttribute.Pair("DM", "Debit Memo")
      })
    {
    }
  }

  public class timeCard : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMOrigDocType.timeCard>
  {
    public timeCard()
      : base("TC")
    {
    }
  }

  public class proformaBilling : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMOrigDocType.proformaBilling>
  {
    public proformaBilling()
      : base("PB")
    {
    }
  }

  /// <summary>Reversal</summary>
  /// <exclude />
  public class reversal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMOrigDocType.reversal>
  {
    public reversal()
      : base("RV")
    {
    }
  }

  /// <summary>Allocation Reversal</summary>
  /// <exclude />
  public class allocationReversal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMOrigDocType.allocationReversal>
  {
    public allocationReversal()
      : base("AR")
    {
    }
  }
}
