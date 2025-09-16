// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.EmailFormatListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.EP;

/// <summary>Determines email formats.</summary>
public class EmailFormatListAttribute : PXStringListAttribute
{
  public const string Text = "T";
  public const string Html = "H";

  public EmailFormatListAttribute()
    : base(new string[2]{ "T", "H" }, new string[2]
    {
      nameof (Text),
      "HTML"
    })
  {
  }
}
