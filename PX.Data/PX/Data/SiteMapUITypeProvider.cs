// Decompiled with JetBrains decompiler
// Type: PX.Data.SiteMapUITypeProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal class SiteMapUITypeProvider : ISiteMapUITypeProvider
{
  private readonly WebAppType _webAppType;

  public SiteMapUITypeProvider(WebAppType webAppType) => this._webAppType = webAppType;

  public string GetUIByScreenId(string screenId)
  {
    SiteMapUITypeProvider.Slot slot = SiteMapUITypeProvider.Slot.LocallyCachedSlot(this._webAppType);
    if (slot == null)
      return "D";
    return !new HashSet<string>((IEnumerable<string>) new \u003C\u003Ez__ReadOnlyArray<string>(new string[2]
    {
      "GenericInquiry",
      "Feedback"
    }), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Contains(screenId) ? slot.GetUIByScreenId(screenId) : "T";
  }

  internal class Slot : IPrefetchable<WebAppType>, IPXCompanyDependent
  {
    private const string SlotName = "SiteMapUITypeProvider";
    private readonly Dictionary<string, string> _screenToUi = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    void IPrefetchable<WebAppType>.Prefetch(WebAppType webAppType)
    {
      PXDataField[] pxDataFieldArray = new PXDataField[2]
      {
        new PXDataField("ScreenID"),
        new PXDataField("SelectedUI")
      };
      foreach (PXDataRecord pxDataRecord in webAppType.IsPortal() ? PXDatabase.SelectMulti<PX.SM.PortalMap>(pxDataFieldArray) : PXDatabase.SelectMulti<PX.SM.SiteMap>(pxDataFieldArray))
      {
        string key = pxDataRecord.GetString(0);
        string str = pxDataRecord.GetString(1);
        if (!string.IsNullOrEmpty(key))
          this._screenToUi[key] = str ?? "D";
      }
    }

    public static SiteMapUITypeProvider.Slot GetSlot(WebAppType webAppType)
    {
      return PXDatabase.GetSlot<SiteMapUITypeProvider.Slot, WebAppType>(nameof (SiteMapUITypeProvider), webAppType, typeof (PX.SM.SiteMap));
    }

    public static SiteMapUITypeProvider.Slot LocallyCachedSlot(WebAppType webAppType)
    {
      return PXContext.GetSlot<SiteMapUITypeProvider.Slot>(nameof (SiteMapUITypeProvider)) ?? PXContext.SetSlot<SiteMapUITypeProvider.Slot>(nameof (SiteMapUITypeProvider), SiteMapUITypeProvider.Slot.GetSlot(webAppType));
    }

    public string GetUIByScreenId(string screenId)
    {
      return this._screenToUi.GetOrDefault<string, string>(screenId, "D");
    }
  }
}
