// Decompiled with JetBrains decompiler
// Type: PX.Api.SyCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PX.Api;

[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}", Name = "{CommandType}")]
internal class SyCommand : ICloneable
{
  public string ViewAlias;
  internal bool CheckRowNumberCondition;
  public int Skipped;
  public int Enumerated;
  public bool CheckUpdatability;

  public string View { get; internal set; }

  public string Field { get; internal set; }

  public string Formula { get; internal set; }

  public bool Commit { get; internal set; }

  public bool IgnoreError { get; internal set; }

  public SyCommandType CommandType { get; internal set; }

  internal bool UseCurrent { get; set; }

  internal PX.Api.ExecutionBehavior? ExecutionBehavior { get; set; }

  public object Clone()
  {
    return (object) new SyCommand()
    {
      Skipped = this.Skipped,
      Enumerated = this.Enumerated,
      CheckUpdatability = this.CheckUpdatability,
      ViewAlias = this.ViewAlias,
      View = this.View,
      Field = this.Field,
      Formula = this.Formula,
      Commit = this.Commit,
      IgnoreError = this.IgnoreError,
      CommandType = this.CommandType,
      UseCurrent = this.UseCurrent,
      CheckRowNumberCondition = this.CheckRowNumberCondition,
      ExecutionBehavior = this.ExecutionBehavior
    };
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string DebuggerDisplay
  {
    get
    {
      string debuggerDisplay;
      switch (this.CommandType)
      {
        case SyCommandType.Action:
          debuggerDisplay = $"{this.View}->{this.Field}";
          break;
        case SyCommandType.NewRow:
        case SyCommandType.RowNumber:
        case SyCommandType.RowCount:
        case SyCommandType.CustomDelegate:
        case SyCommandType.PossibleNewRow:
          debuggerDisplay = $"{this.View}->{this.CommandType.ToString()}";
          break;
        case SyCommandType.Answer:
          debuggerDisplay = $"{this.View}.{this.CommandType.ToString()}{this.Formula}";
          break;
        case SyCommandType.ExportField:
          debuggerDisplay = $"{this.View}.{this.Formula}";
          break;
        default:
          debuggerDisplay = this.View + this.GetField() + this.GetFormula();
          break;
      }
      if (this.Commit)
        debuggerDisplay += ", Commit";
      return debuggerDisplay;
    }
  }

  private string GetField() => this.Field != null ? "." + this.Field : (string) null;

  private string GetFormula()
  {
    if (this.Formula == null)
      return (string) null;
    return !this.Formula.StartsWith("=") ? "=" + this.Formula : this.Formula;
  }

  internal static string EscapeValue(string value)
  {
    StringBuilder stringBuilder = new StringBuilder(value);
    stringBuilder.Replace("\\", "\\\\");
    stringBuilder.Replace("'", "\\'");
    return stringBuilder.ToString();
  }
}
