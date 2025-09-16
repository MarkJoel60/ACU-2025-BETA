// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCalculationMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.TX;

public class TaxCalculationMode
{
  public const 
  #nullable disable
  string Net = "N";
  public const string Gross = "G";
  public const string TaxSetting = "T";

  public class ListAttribute : 
    PXStringListAttribute,
    IPXFieldDefaultingSubscriber,
    IPXFieldVerifyingSubscriber
  {
    public ListAttribute()
      : base(new string[3]{ "T", "G", "N" }, new string[3]
      {
        "Tax Settings",
        "Gross",
        "Net"
      })
    {
    }

    public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
    {
      if (e.Row == null)
        return;
      string newValue = e.NewValue as string;
      if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(newValue != "T"))
        return;
      e.NewValue = (object) "T";
    }

    public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (e.Row == null)
        return;
      string newValue = e.NewValue as string;
      bool flag = PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>();
      if (!flag && newValue != "T" && (!sender.Graph.IsImport && !sender.Graph.IsContractBasedAPI || sender.Graph.UnattendedMode))
      {
        e.NewValue = (object) "T";
      }
      else
      {
        if (!flag && newValue != "T")
          throw new PXSetPropertyException("The Tax Calculation Mode field can have only the Tax Settings (T) value because the Net/Gross Entry Mode feature is disabled.");
        if (flag && newValue != "T" && newValue != "N" && newValue != "G")
          throw new PXSetPropertyException("The Tax Calculation Mode field can have only the Tax Settings (T), Net (N), and Gross (G) values.");
      }
    }
  }

  public class gross : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxCalculationMode.gross>
  {
    public gross()
      : base("G")
    {
    }
  }

  public class net : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxCalculationMode.net>
  {
    public net()
      : base("N")
    {
    }
  }

  public class taxSetting : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxCalculationMode.taxSetting>
  {
    public taxSetting()
      : base("T")
    {
    }
  }

  public class ExternalTaxProviderTaxCalcMode : PXStringListAttribute
  {
    public ExternalTaxProviderTaxCalcMode()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("G", "Gross"),
        PXStringListAttribute.Pair("N", "Net")
      })
    {
    }
  }
}
