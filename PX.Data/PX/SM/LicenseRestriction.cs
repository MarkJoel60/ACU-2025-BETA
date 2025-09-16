// Decompiled with JetBrains decompiler
// Type: PX.SM.LicenseRestriction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace PX.SM;

[Serializable]
public class LicenseRestriction
{
  [XmlAttribute]
  public string Name { get; set; }

  [XmlAttribute]
  public string Value { get; set; }

  public LicenseRestriction()
  {
  }

  public LicenseRestriction(string name, string value)
  {
    this.Name = name;
    this.Value = value;
  }

  public static T TryGetRestrictionValue<T>(IEnumerable<LicenseRestriction> data, string name)
  {
    if (string.IsNullOrEmpty(name) || data == null)
      return default (T);
    LicenseRestriction licenseRestriction = data.SingleOrDefault<LicenseRestriction>((Func<LicenseRestriction, bool>) (m => name.Equals(m.Name, StringComparison.InvariantCultureIgnoreCase)));
    if (licenseRestriction == null)
      return default (T);
    if (string.IsNullOrEmpty(licenseRestriction.Value) && typeof (T) != typeof (string))
      return default (T);
    if (typeof (T).IsEnum)
      return (T) Enum.Parse(typeof (T), licenseRestriction.Value);
    return typeof (T) == typeof (DateTime) ? (T) Convert.ChangeType((object) DateTime.Parse(licenseRestriction.Value, (IFormatProvider) CultureInfo.InvariantCulture), typeof (T)) : (T) Convert.ChangeType((object) licenseRestriction.Value, typeof (T));
  }
}
