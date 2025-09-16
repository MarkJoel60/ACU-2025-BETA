// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RecipientAddToAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public class RecipientAddToAttribute : PXStringListAttribute
{
  public const string To = "T";
  public const string Cc = "C";
  public const string Bcc = "B";

  public RecipientAddToAttribute()
    : base(new string[3]{ "T", "C", "B" }, new string[3]
    {
      nameof (To),
      nameof (Cc),
      nameof (Bcc)
    })
  {
  }
}
