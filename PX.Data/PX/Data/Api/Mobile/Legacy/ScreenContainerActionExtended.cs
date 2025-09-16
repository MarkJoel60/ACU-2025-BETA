// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.Legacy.ScreenContainerActionExtended
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Api.Mobile.Legacy;
using PX.Api.Mobile.Sitemap.Model;
using PX.Common;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Api.Mobile.Legacy;

[PXInternalUseOnly]
public class ScreenContainerActionExtended : ScreenContainerAction
{
  public ScreenContainerActionExtended()
  {
  }

  public ScreenContainerActionExtended(ActionNode action)
    : base(action)
  {
  }

  [JsonIgnore]
  [XmlIgnore]
  public string ViewName { get; set; }
}
