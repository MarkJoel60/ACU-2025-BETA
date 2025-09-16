// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodInvalidException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL;

public class FiscalPeriodInvalidException : PXSetPropertyException
{
  public FiscalPeriodInvalidException(string FinPeriodID)
    : this(FinPeriodID, (PXErrorLevel) 4)
  {
  }

  public FiscalPeriodInvalidException(string FinPeriodID, PXErrorLevel errorLevel)
    : base("Financial Period '{0}' is invalid.", errorLevel, new object[1]
    {
      (object) PeriodIDAttribute.FormatForError(FinPeriodID)
    })
  {
  }

  public FiscalPeriodInvalidException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<FiscalPeriodInvalidException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<FiscalPeriodInvalidException>(this, info);
    base.GetObjectData(info, context);
  }
}
