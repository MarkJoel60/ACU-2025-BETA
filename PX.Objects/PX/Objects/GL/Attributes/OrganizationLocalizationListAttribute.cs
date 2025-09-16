// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.OrganizationLocalizationListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Attributes;

public class OrganizationLocalizationListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
{
  [InjectDependencyOnTypeLevel]
  protected ILocalizationFeaturesService LocalizationFeaturesService { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    List<(string, string)> valueTupleList = new List<(string, string)>()
    {
      ("00", "None")
    };
    IEnumerable<string> enabledLocalizations = this.LocalizationFeaturesService.GetEnabledLocalizations();
    if (enabledLocalizations != null)
      valueTupleList.AddRange(enabledLocalizations.Select<string, (string, string)>((Func<string, (string, string)>) (s =>
      {
        try
        {
          RegionInfo regionInfo = new RegionInfo(s);
          return (s, regionInfo.DisplayName);
        }
        catch (Exception ex)
        {
          object[] objArray = new object[1]{ (object) s };
          throw new PXException(ex, "Incorrect country code ({0}) for the Localization attribute. Please check the correctness of the Features.xml file.", objArray);
        }
      })));
    int newSize = 0;
    this._AllowedValues = new string[valueTupleList.Count];
    this._AllowedLabels = new string[valueTupleList.Count];
    foreach ((string a, string str) in valueTupleList)
    {
      if (!string.Equals(a, "GB"))
      {
        this._AllowedValues[newSize] = a;
        this._AllowedLabels[newSize] = str;
        ++newSize;
      }
    }
    if (newSize < valueTupleList.Count)
    {
      Array.Resize<string>(ref this._AllowedValues, newSize);
      Array.Resize<string>(ref this._AllowedLabels, newSize);
    }
    base.CacheAttached(sender);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._AllowedValues != null && this._AllowedValues.Length > 1)
    {
      PXUIFieldAttribute.SetEnabled<PXAccess.Organization.organizationLocalizationCode>(sender, e.Row, true);
      PXUIFieldAttribute.SetVisible<PXAccess.Organization.organizationLocalizationCode>(sender, e.Row, true);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<PXAccess.Organization.organizationLocalizationCode>(sender, e.Row, false);
      PXUIFieldAttribute.SetVisible<PXAccess.Organization.organizationLocalizationCode>(sender, e.Row, false);
    }
  }
}
