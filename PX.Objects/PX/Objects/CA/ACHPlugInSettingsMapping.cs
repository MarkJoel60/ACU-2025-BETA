// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInSettingsMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public static class ACHPlugInSettingsMapping
{
  public static Dictionary<string, string> Settings = new Dictionary<string, string>()
  {
    {
      "CompanyDiscretionaryData".ToUpper(),
      "companyDiscretionaryData"
    },
    {
      "CompanyEntryDescription".ToUpper(),
      "companyEntryDescription"
    },
    {
      "CompanyIdentification".ToUpper(),
      "remittanceSetting"
    },
    {
      "CompanyName".ToUpper(),
      "remittanceSetting"
    },
    {
      "FileIDModifier".ToUpper(),
      "fileIDModifier"
    },
    {
      "DFIAccountNbr".ToUpper(),
      "vendorSetting"
    },
    {
      "ImmediateDestination".ToUpper(),
      "remittanceSetting"
    },
    {
      "ImmediateDestinationName".ToUpper(),
      "remittanceSetting"
    },
    {
      "ImmediateOrigin".ToUpper(),
      "remittanceSetting"
    },
    {
      "ImmediateOriginName".ToUpper(),
      "remittanceSetting"
    },
    {
      "IncludeAddendaRecords".ToUpper(),
      "checkBox"
    },
    {
      "IncludeOffsetRecord".ToUpper(),
      "checkBox"
    },
    {
      "OffsetDFIAccountNbr".ToUpper(),
      "remittanceSetting"
    },
    {
      "OffsetReceivingDEFIID".ToUpper(),
      "remittanceSetting"
    },
    {
      "OffsetReceivingID".ToUpper(),
      "remittanceSetting"
    },
    {
      "OriginatingFDIID".ToUpper(),
      "remittanceSetting"
    },
    {
      "OriginatorStatusCode".ToUpper(),
      "originatorStatusCode"
    },
    {
      "PriorityCode".ToUpper(),
      "priorityCode"
    },
    {
      "ReceivingDEFIID".ToUpper(),
      "vendorSetting"
    },
    {
      "ReceivingID".ToUpper(),
      "vendorSetting"
    },
    {
      "ServiceClassCode".ToUpper(),
      "serviceClassCode"
    },
    {
      "StandardEntryClassCode".ToUpper(),
      "standardEntryClassCode"
    },
    {
      "TransactionCode".ToUpper(),
      "vendorSetting"
    }
  };
  public static Dictionary<SelectorType?, string> SelectorTypes = new Dictionary<SelectorType?, string>()
  {
    {
      new SelectorType?((SelectorType) 6),
      "checkBox"
    },
    {
      new SelectorType?((SelectorType) 5),
      "fileIDModifier"
    },
    {
      new SelectorType?((SelectorType) 0),
      "remittanceSetting"
    },
    {
      new SelectorType?((SelectorType) 2),
      "serviceClassCode"
    },
    {
      new SelectorType?((SelectorType) 3),
      "standardEntryClassCode"
    },
    {
      new SelectorType?((SelectorType) 1),
      "vendorSetting"
    },
    {
      new SelectorType?((SelectorType) 4),
      "vendorSetting"
    },
    {
      new SelectorType?((SelectorType) 7),
      "companyDiscretionaryData"
    }
  };
}
