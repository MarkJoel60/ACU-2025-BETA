// Decompiled with JetBrains decompiler
// Type: PX.Data.ProcessingResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// The result of processing with statistics - a number of errors, warnings and successfully processed items.
/// </summary>
internal struct ProcessingResult
{
  public bool HasError => this.Errors > 0;

  public bool HasWarning => !this.HasError && this.Warnings > 0;

  public int Errors { get; private set; }

  public int Warnings { get; private set; }

  public int Processed { get; private set; }

  public ProcessingResult(int errors, int warnings, int processed)
  {
    if (errors < 0)
      throw new ArgumentOutOfRangeException(nameof (errors));
    if (warnings < 0)
      throw new ArgumentOutOfRangeException(nameof (warnings));
    if (processed < 0)
      throw new ArgumentOutOfRangeException(nameof (processed));
    this.Errors = errors;
    this.Warnings = warnings;
    this.Processed = processed;
  }

  public void IncrementError() => ++this.Errors;

  public void IncrementWarnings() => ++this.Warnings;

  public void IncrementProcessed() => ++this.Processed;

  public void CombineWith(ProcessingResult other)
  {
    this.Errors += other.Errors;
    this.Warnings += other.Warnings;
    this.Processed += other.Processed;
  }

  public void AddToResult(PXErrorLevel errorLevel)
  {
    switch (errorLevel)
    {
      case PXErrorLevel.Undefined:
      case PXErrorLevel.RowInfo:
        ++this.Processed;
        break;
      case PXErrorLevel.Warning:
      case PXErrorLevel.RowWarning:
        ++this.Warnings;
        break;
      case PXErrorLevel.Error:
      case PXErrorLevel.RowError:
        ++this.Errors;
        break;
    }
  }

  public PXErrorLevel GetErrorLevel()
  {
    if (this.HasError)
      return PXErrorLevel.Error;
    return !this.HasWarning ? PXErrorLevel.Undefined : PXErrorLevel.Warning;
  }
}
