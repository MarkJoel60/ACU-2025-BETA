// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PXTaskSetPropertyException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.PM;

public class PXTaskSetPropertyException : PXSetPropertyException
{
  protected int? projectID;
  protected int? taskID;

  public PXTaskSetPropertyException(int? projectID, int? taskID, string message)
    : base(message)
  {
    this.ProjectID = projectID;
    this.TaskID = taskID;
  }

  public PXTaskSetPropertyException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXTaskSetPropertyException>(this, info);
    base.GetObjectData(info, context);
  }

  public int? ProjectID
  {
    get => this.projectID;
    protected set => this.projectID = value;
  }

  public int? TaskID
  {
    get => this.taskID;
    protected set => this.taskID = value;
  }
}
