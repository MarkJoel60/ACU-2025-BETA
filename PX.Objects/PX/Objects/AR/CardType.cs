// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CardType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class CardType
{
  public const 
  #nullable disable
  string VisaCode = "VIS";
  public const string MasterCardCode = "MSC";
  public const string AmericanExpressCode = "AME";
  public const string DiscoverCode = "DSC";
  public const string DinersClubCode = "DNC";
  public const string UnionPayCode = "UNI";
  public const string JCBCode = "JCB";
  public const string DebitCode = "DBT";
  public const string AleloCode = "ALE";
  public const string AliaCode = "ALI";
  public const string CabalCode = "CBL";
  public const string CarnetCode = "CRN";
  public const string DankortCode = "DNK";
  public const string EloCode = "ELO";
  public const string ForbrugsforeningenCode = "FRB";
  public const string MaestroCode = "MAE";
  public const string NaranjaCode = "NRJ";
  public const string SodexoCode = "SDX";
  public const string VrCode = "VRR";
  public const string EftCode = "EFT";
  public const string OtherCode = "OTH";
  public const string VisaLabel = "Visa";
  public const string MasterCardLabel = "MasterCard";
  public const string AmericanExpressLabel = "American Express";
  public const string DiscoverLabel = "Discover";
  public const string DinersClubLabel = "Diners Club";
  public const string UnionPayLabel = "Union Pay";
  public const string JCBLabel = "JCB";
  public const string DebitLabel = "Debit";
  public const string AleloLabel = "Alelo";
  public const string AliaLabel = "Alia";
  public const string CabalLabel = "Cabal";
  public const string CarnetLabel = "Carnet";
  public const string DankortLabel = "Dankort";
  public const string EloLabel = "Elo";
  public const string ForbrugsforeningenLabel = "Forbrugsforeningen";
  public const string MaestroLabel = "Maestro";
  public const string NaranjaLabel = "Naranja";
  public const string SodexoLabel = "Sodexo";
  public const string VrLabel = "Vr";
  public const string EftLabel = "EFT";
  public const string OtherLabel = "Other";
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "MSC",
      "MasterCard"
    },
    {
      "VIS",
      "Visa"
    },
    {
      "AME",
      "American Express"
    },
    {
      "DSC",
      "Discover"
    },
    {
      "JCB",
      "JCB"
    },
    {
      "DNC",
      "Diners Club"
    },
    {
      "UNI",
      "Union Pay"
    },
    {
      "MAE",
      "Maestro"
    },
    {
      "OTH",
      "Other"
    },
    {
      "ALE",
      "Alelo"
    },
    {
      "ALI",
      "Alia"
    },
    {
      "CBL",
      "Cabal"
    },
    {
      "CRN",
      "Carnet"
    },
    {
      "DNK",
      "Dankort"
    },
    {
      "ELO",
      "Elo"
    },
    {
      "FRB",
      "Forbrugsforeningen"
    },
    {
      "NRJ",
      "Naranja"
    },
    {
      "SDX",
      "Sodexo"
    },
    {
      "VRR",
      "Vr"
    },
    {
      "EFT",
      "EFT"
    },
    {
      "DBT",
      "Debit"
    }
  };
  public static IEnumerable<ValueLabelPair> valueLabelPairsWithBlankItem = CardType._valueLabelPairs.Append<ValueLabelPair>(new ValueLabelPair("", ""));
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairsWithBlankItem = (IEnumerable<ValueLabelPair>) new ValueLabelList(CardType.valueLabelPairsWithBlankItem);
  private static (CCCardType, string)[] cardTypeCodes = new (CCCardType, string)[21]
  {
    (CCCardType.Visa, "VIS"),
    (CCCardType.MasterCard, "MSC"),
    (CCCardType.AmericanExpress, "AME"),
    (CCCardType.Discover, "DSC"),
    (CCCardType.DinersClub, "DNC"),
    (CCCardType.UnionPay, "UNI"),
    (CCCardType.JCB, "JCB"),
    (CCCardType.Debit, "DBT"),
    (CCCardType.Alelo, "ALE"),
    (CCCardType.Alia, "ALI"),
    (CCCardType.Cabal, "CBL"),
    (CCCardType.Carnet, "CRN"),
    (CCCardType.Dankort, "DNK"),
    (CCCardType.Elo, "ELO"),
    (CCCardType.Forbrugsforeningen, "FRB"),
    (CCCardType.Maestro, "MAE"),
    (CCCardType.Naranja, "NRJ"),
    (CCCardType.Sodexo, "SDX"),
    (CCCardType.Vr, "VRR"),
    (CCCardType.Other, "OTH"),
    (CCCardType.EFT, "EFT")
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => CardType._valueLabelPairs;

  public static string GetCardTypeCode(CCCardType tranType)
  {
    if (!((IEnumerable<(CCCardType, string)>) CardType.cardTypeCodes).Where<(CCCardType, string)>((Func<(CCCardType, string), bool>) (i => i.Item1 == tranType)).Any<(CCCardType, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CCCardType, string)>) CardType.cardTypeCodes).Where<(CCCardType, string)>((Func<(CCCardType, string), bool>) (i => i.Item1 == tranType)).First<(CCCardType, string)>().Item2;
  }

  public static string GetDisplayName(CCCardType cardType)
  {
    return PXMessages.LocalizeNoPrefix(new CardType.ListAttribute().ValueLabelDic[CardType.GetCardTypeCode(cardType)]).Trim();
  }

  public static string GetDisplayName(string cardType)
  {
    return PXMessages.LocalizeNoPrefix(new CardType.ListAttribute().ValueLabelDic[cardType]).Trim();
  }

  public static MeansOfPayment GetMeansOfPayment(string cardType)
  {
    switch (cardType)
    {
      case null:
        return (MeansOfPayment) 2;
      case "EFT":
        return (MeansOfPayment) 1;
      case "OTH":
        return (MeansOfPayment) 2;
      default:
        return (MeansOfPayment) 0;
    }
  }

  public static CCCardType GetCardTypeEnumByCode(string code)
  {
    return !((IEnumerable<(CCCardType, string)>) CardType.cardTypeCodes).Where<(CCCardType, string)>((Func<(CCCardType, string), bool>) (i => i.Item2.Equals(code, StringComparison.CurrentCultureIgnoreCase))).Any<(CCCardType, string)>() ? CCCardType.Other : ((IEnumerable<(CCCardType, string)>) CardType.cardTypeCodes).Where<(CCCardType, string)>((Func<(CCCardType, string), bool>) (i => i.Item2.Equals(code, StringComparison.CurrentCultureIgnoreCase))).First<(CCCardType, string)>().Item1;
  }

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(CardType._valueLabelPairs)
    {
    }
  }

  public class ListWithBlankItemAttribute : LabelListAttribute
  {
    public ListWithBlankItemAttribute()
      : base(CardType._valueLabelPairsWithBlankItem)
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CardType.other>
  {
    public other()
      : base("OTH")
    {
    }
  }
}
