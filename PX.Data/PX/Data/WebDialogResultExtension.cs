// Decompiled with JetBrains decompiler
// Type: PX.Data.WebDialogResultExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Determines whether the answer that a user provided in a dialog box is positive.
/// The positive answers are <see cref="F:PX.Data.WebDialogResult.OK" /> and <see cref="F:PX.Data.WebDialogResult.Yes" />.</summary>
public static class WebDialogResultExtension
{
  public static DialogAnswerType GetAnswerType(this WebDialogResult result)
  {
    switch (result)
    {
      case WebDialogResult.None:
        return DialogAnswerType.None;
      case WebDialogResult.OK:
      case WebDialogResult.Yes:
        return DialogAnswerType.Positive;
      case WebDialogResult.Cancel:
      case WebDialogResult.No:
        return DialogAnswerType.Negative;
      default:
        return DialogAnswerType.Neutral;
    }
  }

  public static bool IsPositive(this WebDialogResult answer)
  {
    return answer.GetAnswerType() == DialogAnswerType.Positive;
  }

  public static bool IsNegative(this WebDialogResult answer)
  {
    return answer.GetAnswerType() == DialogAnswerType.Negative;
  }
}
