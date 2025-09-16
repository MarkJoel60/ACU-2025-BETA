// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.TitlesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class TitlesAttribute : PXStringListAttribute
{
  public const string Doctor = "Dr.";
  public const string Miss = "Miss";
  public const string Mr = "Mr.";
  public const string Mrs = "Mrs.";
  public const string Ms = "Ms.";
  public const string Prof = "Prof.";

  public TitlesAttribute()
    : base(new string[6]
    {
      "Dr.",
      nameof (Miss),
      "Mr.",
      "Mrs.",
      "Ms.",
      "Prof."
    }, new string[6]
    {
      "Dr.",
      nameof (Miss),
      "Mr.",
      "Mrs.",
      "Ms.",
      "Prof."
    })
  {
  }
}
