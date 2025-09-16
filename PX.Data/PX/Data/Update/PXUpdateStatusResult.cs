// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateStatusResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Update;

public class PXUpdateStatusResult
{
  public PXLongRunStatus ProcessStatus { get; private set; }

  public PXUpdateStatus UpdateStatus { get; private set; }

  public PXUpdateQuestion UpdateQuetion { get; private set; }

  public Exception Error { get; private set; }

  public PXUpdateStatusResult(PXLongRunStatus operation) => this.ProcessStatus = operation;

  public PXUpdateStatusResult(PXUpdateStatus status)
  {
    this.ProcessStatus = PXLongRunStatus.InProcess;
    this.UpdateStatus = status;
  }

  public PXUpdateStatusResult(PXUpdateQuestion quetion)
  {
    this.ProcessStatus = PXLongRunStatus.InProcess;
    this.UpdateQuetion = quetion;
  }

  public PXUpdateStatusResult(Exception error)
  {
    this.ProcessStatus = PXLongRunStatus.Aborted;
    this.Error = error;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Update status: {0}", (object) this.ProcessStatus.ToString());
    stringBuilder.AppendLine();
    if (this.Error != null)
      stringBuilder.AppendFormat("An error has occurred in the database update process: '{0}'.", (object) this.Error.Message);
    if (this.UpdateStatus != null)
      stringBuilder.AppendLine(this.UpdateStatus.ToString());
    return stringBuilder.ToString();
  }
}
