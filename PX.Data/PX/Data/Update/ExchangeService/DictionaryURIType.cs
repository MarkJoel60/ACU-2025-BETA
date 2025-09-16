// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.DictionaryURIType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum DictionaryURIType
{
  /// <remarks />
  [XmlEnum("item:InternetMessageHeader")] itemInternetMessageHeader,
  /// <remarks />
  [XmlEnum("contacts:ImAddress")] contactsImAddress,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddress:Street")] contactsPhysicalAddressStreet,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddress:City")] contactsPhysicalAddressCity,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddress:State")] contactsPhysicalAddressState,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddress:CountryOrRegion")] contactsPhysicalAddressCountryOrRegion,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddress:PostalCode")] contactsPhysicalAddressPostalCode,
  /// <remarks />
  [XmlEnum("contacts:PhoneNumber")] contactsPhoneNumber,
  /// <remarks />
  [XmlEnum("contacts:EmailAddress")] contactsEmailAddress,
  /// <remarks />
  [XmlEnum("distributionlist:Members:Member")] distributionlistMembersMember,
}
