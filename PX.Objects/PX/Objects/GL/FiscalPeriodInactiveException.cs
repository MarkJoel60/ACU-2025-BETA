// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodInactiveException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL;

public class FiscalPeriodInactiveException : PXSetPropertyException
{
  public FiscalPeriodInactiveException(string message, PXErrorLevel errorLevel = 4)
    : base(message, errorLevel)
  {
  }

  public FiscalPeriodInactiveException(string finPeriodID, string organizationCD)
    : this(finPeriodID, organizationCD, (PXErrorLevel) 4)
  {
  }

  public FiscalPeriodInactiveException(
    string finPeriodID,
    string organizationCD,
    PXErrorLevel errorLevel)
    : base("The {0} financial period is inactive in the {1} company.", errorLevel, new object[2]
    {
      (object) PeriodIDAttribute.FormatForError(finPeriodID),
      (object) organizationCD
    })
  {
  }

  public FiscalPeriodInactiveException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<FiscalPeriodInactiveException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<FiscalPeriodInactiveException>(this, info);
    base.GetObjectData(info, context);
  }
}
