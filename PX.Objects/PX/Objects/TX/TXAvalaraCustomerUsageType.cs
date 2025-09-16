// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXAvalaraCustomerUsageType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public static class TXAvalaraCustomerUsageType
{
  public const string FederalGovt = "A";
  public const string StateLocalGovt = "B";
  public const string TribalGovt = "C";
  public const string ForeignDiplomat = "D";
  public const string CharitableOrg = "E";
  public const string Religious = "F";
  public const string Resale = "G";
  public const string AgriculturalProd = "H";
  public const string IndustrialProd = "I";
  public const string DirectPayPermit = "J";
  public const string DirectMail = "K";
  public const string Other = "L";
  public const string Education = "M";
  public const string LocalGovt = "N";
  public const string ComAquaculture = "P";
  public const string ComFishery = "Q";
  public const string NonResident = "R";
  public const string Taxable = "T";
  public const string Default = "0";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[19]
      {
        PXStringListAttribute.Pair("A", "Federal Government"),
        PXStringListAttribute.Pair("B", "State/Local Govt."),
        PXStringListAttribute.Pair("C", "Tribal Government"),
        PXStringListAttribute.Pair("D", "Foreign Diplomat"),
        PXStringListAttribute.Pair("E", "Charitable Organization"),
        PXStringListAttribute.Pair("F", "Religious"),
        PXStringListAttribute.Pair("G", "Resale"),
        PXStringListAttribute.Pair("H", "Agricultural Production"),
        PXStringListAttribute.Pair("I", "Industrial Prod/Mfg."),
        PXStringListAttribute.Pair("J", "Direct Pay Permit"),
        PXStringListAttribute.Pair("K", "Direct Mail"),
        PXStringListAttribute.Pair("L", "Other"),
        PXStringListAttribute.Pair("M", "Education"),
        PXStringListAttribute.Pair("N", "Local Government"),
        PXStringListAttribute.Pair("P", "Commercial Aquaculture"),
        PXStringListAttribute.Pair("Q", "Commercial Fishery"),
        PXStringListAttribute.Pair("R", "Non-resident"),
        PXStringListAttribute.Pair("T", "Taxable - Override Exemption"),
        PXStringListAttribute.Pair("0", "Default")
      })
    {
    }
  }
}
