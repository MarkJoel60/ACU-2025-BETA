// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWindowModeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data;

/// <exclude />
public class PXWindowModeAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Base = "B";
  public const string Same = "S";
  public const string New = "N";
  public const string NewWindow = "W";
  public const string InlineWindow = "I";
  public const string Layer = "L";
  private static readonly PXWindowModeAttribute.WindowMode[] _modes = new PXWindowModeAttribute.WindowMode[6]
  {
    new PXWindowModeAttribute.WindowMode("B", nameof (Base), PXBaseRedirectException.WindowMode.Base),
    new PXWindowModeAttribute.WindowMode("S", "Same Tab", PXBaseRedirectException.WindowMode.Same),
    new PXWindowModeAttribute.WindowMode("N", "New Tab", PXBaseRedirectException.WindowMode.New),
    new PXWindowModeAttribute.WindowMode("W", "Pop-Up Window", PXBaseRedirectException.WindowMode.NewWindow),
    new PXWindowModeAttribute.WindowMode("I", "Inline", PXBaseRedirectException.WindowMode.InlineWindow),
    new PXWindowModeAttribute.WindowMode("L", "Side Panel", PXBaseRedirectException.WindowMode.Layer)
  };

  public PXWindowModeAttribute()
    : base(((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).Select<PXWindowModeAttribute.WindowMode, string>((Func<PXWindowModeAttribute.WindowMode, string>) (m => m.Code)).ToArray<string>(), ((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).Select<PXWindowModeAttribute.WindowMode, string>((Func<PXWindowModeAttribute.WindowMode, string>) (m => m.DisplayName)).ToArray<string>())
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Data.PXWindowModeAttribute" /> class.
  /// </summary>
  /// <param name="windowModes">The list of available window modes.</param>
  public PXWindowModeAttribute(
    params PXBaseRedirectException.WindowMode[] windowModes)
    : base(((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).Where<PXWindowModeAttribute.WindowMode>((Func<PXWindowModeAttribute.WindowMode, bool>) (m => ((IEnumerable<PXBaseRedirectException.WindowMode>) windowModes).Contains<PXBaseRedirectException.WindowMode>(m.Mode))).Select<PXWindowModeAttribute.WindowMode, string>((Func<PXWindowModeAttribute.WindowMode, string>) (m => m.Code)).ToArray<string>(), ((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).Where<PXWindowModeAttribute.WindowMode>((Func<PXWindowModeAttribute.WindowMode, bool>) (m => ((IEnumerable<PXBaseRedirectException.WindowMode>) windowModes).Contains<PXBaseRedirectException.WindowMode>(m.Mode))).Select<PXWindowModeAttribute.WindowMode, string>((Func<PXWindowModeAttribute.WindowMode, string>) (m => m.DisplayName)).ToArray<string>())
  {
  }

  public static void SetList<TField>(
    PXCache cache,
    object data,
    params PXBaseRedirectException.WindowMode[] windowModes)
    where TField : IBqlField
  {
    PXWindowModeAttribute.SetList(cache, data, typeof (TField).Name, windowModes);
  }

  public static void SetList(
    PXCache cache,
    object data,
    string field,
    params PXBaseRedirectException.WindowMode[] windowModes)
  {
    List<string> stringList1 = new List<string>(windowModes.Length);
    List<string> stringList2 = new List<string>(windowModes.Length);
    foreach (PXBaseRedirectException.WindowMode windowMode1 in windowModes)
    {
      PXBaseRedirectException.WindowMode wmode = windowMode1;
      PXWindowModeAttribute.WindowMode windowMode2 = ((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).FirstOrDefault<PXWindowModeAttribute.WindowMode>((Func<PXWindowModeAttribute.WindowMode, bool>) (m => m.Mode == wmode));
      if (windowMode2 == null)
        throw new PXException("Unknown window mode");
      stringList1.Add(windowMode2.Code);
      stringList2.Add(windowMode2.DisplayName);
    }
    PXStringListAttribute.SetList(cache, data, field, stringList1.ToArray(), stringList2.ToArray());
  }

  public static PXBaseRedirectException.WindowMode Convert(string value)
  {
    return (((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).FirstOrDefault<PXWindowModeAttribute.WindowMode>((Func<PXWindowModeAttribute.WindowMode, bool>) (m => m.Code == value)) ?? throw new PXException("Unknown window mode")).Mode;
  }

  public static string FromMode(PXBaseRedirectException.WindowMode value)
  {
    return (((IEnumerable<PXWindowModeAttribute.WindowMode>) PXWindowModeAttribute._modes).FirstOrDefault<PXWindowModeAttribute.WindowMode>((Func<PXWindowModeAttribute.WindowMode, bool>) (m => m.Mode == value)) ?? throw new PXException("Unknown window mode")).Code;
  }

  public class layer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXWindowModeAttribute.layer>
  {
    public layer()
      : base("L")
    {
    }
  }

  /// <exclude />
  private class WindowMode
  {
    public string Code { get; set; }

    public string DisplayName { get; set; }

    public PXBaseRedirectException.WindowMode Mode { get; set; }

    public WindowMode()
    {
    }

    public WindowMode(string code, string displayName, PXBaseRedirectException.WindowMode mode)
    {
      this.Code = code;
      this.DisplayName = displayName;
      this.Mode = mode;
    }
  }
}
