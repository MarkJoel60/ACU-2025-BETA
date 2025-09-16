// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Route
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>
/// Class representing a Route containing directions between an origin and a final destination.
/// </summary>
public class Route
{
  private RouteLeg[] legs;

  internal Route(XmlDocument route, XmlNamespaceManager nameSpace)
  {
    XmlNodeList xmlNodeList = route.DocumentElement.SelectNodes($".//{"bingSchema:"}RouteLeg", nameSpace);
    List<RouteLeg> routeLegList = new List<RouteLeg>();
    foreach (XmlElement leg in xmlNodeList)
      routeLegList.Add(new RouteLeg(leg, nameSpace));
    this.legs = routeLegList.ToArray();
  }

  /// <summary>Gets the legs of this Route.</summary>
  public RouteLeg[] Legs => this.legs;

  /// <summary>Gets the duration of the Route in seconds.</summary>
  public int Duration
  {
    get
    {
      int duration = 0;
      for (int index = 0; index < this.legs.Length; ++index)
        duration += this.legs[index].Duration;
      return duration;
    }
  }

  /// <summary>Gets the distance of the Route in miles.</summary>
  public double Distance
  {
    get
    {
      double distance = 0.0;
      for (int index = 0; index < this.legs.Length; ++index)
        distance += this.legs[index].Distance;
      return distance;
    }
  }
}
