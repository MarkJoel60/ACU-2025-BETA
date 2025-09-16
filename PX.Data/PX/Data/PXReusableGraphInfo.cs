// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReusableGraphInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class PXReusableGraphInfo
{
  public PXGraph Graph;
  public int CodeVersion;
  public bool IsFromRedirect;
  public string CurrentScreenId;
  public string PreviousScreenId;
  public bool FromNavigation;

  public void Clear()
  {
    this.Graph = (PXGraph) null;
    this.IsFromRedirect = false;
    this.FromNavigation = false;
  }
}
