// Decompiled with JetBrains decompiler
// Type: PX.Data.MapRedirectorProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class MapRedirectorProvider
{
  private List<MapRedirector> redirectors;

  public List<MapRedirector> MapRedirectors
  {
    get
    {
      if (this.redirectors == null)
      {
        this.redirectors = new List<MapRedirector>();
        this.InitList(this.redirectors);
      }
      return this.redirectors;
    }
  }

  protected virtual void InitList(List<MapRedirector> list)
  {
    list.Add((MapRedirector) new GoogleMapRedirector());
  }
}
