// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PXTaskIsCompletedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.PM;

public class PXTaskIsCompletedException : PXTaskSetPropertyException
{
  public PXTaskIsCompletedException(int? projectID, int? taskID)
    : this(projectID, taskID, "Task is Completed and cannot be used for data entry.")
  {
  }

  public PXTaskIsCompletedException(int? projectID, int? taskID, string message)
    : base(projectID, taskID, message)
  {
  }

  public PXTaskIsCompletedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
