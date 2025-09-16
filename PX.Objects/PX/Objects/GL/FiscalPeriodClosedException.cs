// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodClosedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL;

public class FiscalPeriodClosedException : PXSetPropertyException
{
  public FiscalPeriodClosedException(string message, PXErrorLevel errorLevel = 4)
    : base(message, errorLevel)
  {
  }

  public FiscalPeriodClosedException(string finPeriodID, string organizationCD)
    : this(finPeriodID, organizationCD, (PXErrorLevel) 4)
  {
  }

  public FiscalPeriodClosedException(
    string finPeriodID,
    string organizationCD,
    PXErrorLevel errorLevel)
    : this(finPeriodID, organizationCD, errorLevel, "The {0} financial period is closed in the {1} company.")
  {
  }

  public FiscalPeriodClosedException(
    string finPeriodID,
    string organizationCD,
    string errorMessageFormat)
    : this(finPeriodID, organizationCD, (PXErrorLevel) 4, errorMessageFormat)
  {
  }

  public FiscalPeriodClosedException(
    string finPeriodID,
    string organizationCD,
    PXErrorLevel errorLevel,
    string errorMessageFormat)
    : base(errorMessageFormat, errorLevel, new object[2]
    {
      (object) PeriodIDAttribute.FormatForError(finPeriodID),
      (object) organizationCD
    })
  {
  }

  public FiscalPeriodClosedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<FiscalPeriodClosedException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<FiscalPeriodClosedException>(this, info);
    base.GetObjectData(info, context);
  }
}
