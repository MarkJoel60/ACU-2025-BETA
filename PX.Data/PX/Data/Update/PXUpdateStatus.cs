// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Update;

public class PXUpdateStatus
{
  public int Peresent { get; private set; }

  public string Message { get; private set; }

  public IEnumerable<PXUpdateEvent> Errors { get; private set; }

  public PXUpdateStatus(int persent, string message, IEnumerable<PXUpdateEvent> errors)
  {
    this.Peresent = persent;
    this.Message = message;
    this.Errors = errors;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Update stage '{0}', percent: {1}", (object) this.Message, (object) this.Peresent);
    stringBuilder.AppendLine();
    foreach (PXUpdateEvent error in this.Errors)
    {
      stringBuilder.AppendLine("--------------");
      stringBuilder.AppendLine(error.ToString());
    }
    return stringBuilder.ToString();
  }
}
