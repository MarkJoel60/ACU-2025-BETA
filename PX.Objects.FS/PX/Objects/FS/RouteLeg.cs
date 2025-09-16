// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteLeg
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>Class representing the leg of a route.</summary>
public class RouteLeg
{
  private string startAddress;
  private string endAddress;
  private LatLng startLocation;
  private LatLng endLocation;
  private double distance;
  private string distanceDescr;
  private int duration;
  private string durationDescr;
  private RouteStep[] steps;

  internal RouteLeg(XmlElement leg, XmlNamespaceManager nameSpace)
  {
    this.startAddress = leg.SelectSingleNode(string.Format(".//{0}StartLocation//{0}Address//{0}FormattedAddress", (object) "bingSchema:"), nameSpace).InnerText;
    this.endAddress = leg.SelectSingleNode(string.Format(".//{0}EndLocation//{0}Address//{0}FormattedAddress", (object) "bingSchema:"), nameSpace).InnerText;
    this.startLocation = new LatLng((XmlElement) leg.SelectSingleNode($".//{"bingSchema:"}ActualStart", nameSpace), nameSpace);
    this.endLocation = new LatLng((XmlElement) leg.SelectSingleNode($".//{"bingSchema:"}ActualEnd", nameSpace), nameSpace);
    this.distance = Math.Round(double.Parse(leg.SelectSingleNode($".//{"bingSchema:"}TravelDistance", nameSpace).InnerText), 2);
    this.distanceDescr = $"{this.distance} mi";
    this.duration = int.Parse(leg.SelectSingleNode($".//{"bingSchema:"}TravelDuration", nameSpace).InnerText);
    this.durationDescr = SharedFunctions.parseSecsDurationToString(this.duration);
    XmlNodeList xmlNodeList = leg.SelectNodes($".//{"bingSchema:"}ItineraryItem", nameSpace);
    List<RouteStep> routeStepList = new List<RouteStep>();
    foreach (XmlElement step in xmlNodeList)
    {
      if (routeStepList.Count == 0)
        routeStepList.Add(new RouteStep(step, this.startLocation, nameSpace));
      else
        routeStepList.Add(new RouteStep(step, routeStepList[routeStepList.Count - 1].EndLocation, nameSpace));
    }
    this.steps = routeStepList.ToArray();
  }

  /// <summary>Gets the start address for this leg.</summary>
  public string StartAddress => this.startAddress;

  /// <summary>Gets the end address for this leg.</summary>
  public string EndAddress => this.endAddress;

  /// <summary>
  /// Gets the start location coordinates of this leg of the route.
  /// </summary>
  public LatLng StartLocation => this.startLocation;

  /// <summary>
  /// Gets the end location coordinates of this leg of the route.
  /// </summary>
  public LatLng EndLocation => this.endLocation;

  /// <summary>Gets the distance of this leg in miles.</summary>
  public double Distance => this.distance;

  /// <summary>
  /// Gets the distance of this leg in an user-friendly format (e.g. Km, miles, feet, etc.).
  /// </summary>
  public string DistanceDescr => this.distanceDescr;

  /// <summary>Gets the duration of this leg in seconds.</summary>
  public int Duration => this.duration;

  /// <summary>
  /// Gets the duration of this leg in an user-friendly format (e.g. minutes, hours, days, etc.).
  /// </summary>
  public string DurationDescr => this.durationDescr;

  /// <summary>Gets the steps for this leg of the route.</summary>
  public RouteStep[] Steps => this.steps;
}
