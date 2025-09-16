// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PXTaskIsInactiveException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.PM;

public class PXTaskIsInactiveException : PXTaskSetPropertyException
{
  public PXTaskIsInactiveException(int? projectID, int? taskID, string message)
    : base(projectID, taskID, message)
  {
  }

  public PXTaskIsInactiveException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
