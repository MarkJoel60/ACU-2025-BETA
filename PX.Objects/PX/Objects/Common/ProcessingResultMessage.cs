// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ProcessingResultMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public class ProcessingResultMessage
{
  public string Text { get; set; }

  public PXErrorLevel ErrorLevel { get; set; }

  public ProcessingResultMessage(PXErrorLevel errorLevel, string text)
  {
    this.Text = text;
    this.ErrorLevel = errorLevel;
  }

  public override string ToString()
  {
    string str = (string) null;
    if (this.ErrorLevel == 4 || this.ErrorLevel == 5)
      str = "Error";
    else if (this.ErrorLevel == 2 || this.ErrorLevel == 3)
      str = "Warning";
    return str == null ? this.Text : $"{str}: {this.Text}";
  }
}
