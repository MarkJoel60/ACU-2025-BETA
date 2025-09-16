// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteStep
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>Class representing a step within a leg of a route.</summary>
public class RouteStep
{
  private int duration;
  private string durationDescr;
  private double distance;
  private string distanceDescr;
  private LatLng startLocation;
  private LatLng endLocation;
  private string htmlInstructions;
  private string maneuver;
  private string travelMode;

  internal RouteStep(XmlElement step, LatLng startLocation, XmlNamespaceManager nameSpace)
  {
    this.duration = int.Parse(step.SelectSingleNode($".//{"bingSchema:"}TravelDuration", nameSpace).InnerText);
    this.durationDescr = SharedFunctions.parseSecsDurationToString(this.duration);
    this.distance = Math.Round(double.Parse(step.SelectSingleNode($".//{"bingSchema:"}TravelDistance", nameSpace).InnerText), 2);
    this.distanceDescr = $"{this.distance} mi";
    this.startLocation = startLocation;
    this.endLocation = new LatLng((XmlElement) step.SelectSingleNode($".//{"bingSchema:"}ManeuverPoint", nameSpace), nameSpace);
    this.htmlInstructions = step.SelectSingleNode($".//{"bingSchema:"}Instruction", nameSpace).InnerText;
    if (step.SelectSingleNode($".//{"bingSchema:"}TravelMode", nameSpace) != null)
      this.travelMode = step.SelectSingleNode($".//{"bingSchema:"}TravelMode", nameSpace).InnerText;
    if (string.IsNullOrEmpty(step.SelectSingleNode($".//{"bingSchema:"}Instruction", nameSpace).Attributes["maneuverType"].Value))
      return;
    this.maneuver = step.SelectSingleNode($".//{"bingSchema:"}Instruction", nameSpace).Attributes["maneuverType"].Value;
  }

  /// <summary>Gets the duration of this step in seconds.</summary>
  public int Duration => this.duration;

  /// <summary>
  /// Gets the duration of this step in an user-friendly format (e.g. minutes, hours, days, etc.).
  /// </summary>
  public string DurationDescr => this.durationDescr;

  /// <summary>Gets the distance of this step in meters.</summary>
  public double Distance => this.distance;

  /// <summary>
  /// Gets the distance of this step in an user-friendly format (e.g. Km, miles, feet, etc.).
  /// </summary>
  public string DistanceDescr => this.distanceDescr;

  /// <summary>Gets the start location for this step.</summary>
  public LatLng StartLocation => this.startLocation;

  /// <summary>Gets the end location of this step.</summary>
  public LatLng EndLocation => this.endLocation;

  /// <summary>
  /// Gets the instructions for this step with HTML formatting.
  /// </summary>
  public string HtmlInstructions => this.htmlInstructions;

  /// <summary>Gets the instructions for this step.</summary>
  public string Maneuver => this.maneuver;

  /// <summary>Gets the travel mode for this step.</summary>
  public string TravelMode => this.travelMode;
}
