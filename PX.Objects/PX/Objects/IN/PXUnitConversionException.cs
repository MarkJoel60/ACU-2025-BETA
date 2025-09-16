// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXUnitConversionException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN;

public class PXUnitConversionException : PXSetPropertyException
{
  public PXUnitConversionException()
    : base("Unit conversion is missing.")
  {
  }

  public PXUnitConversionException(string UOM)
    : base("Unit conversion {0} is missing.", new object[1]
    {
      (object) UOM
    })
  {
  }

  public PXUnitConversionException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public PXUnitConversionException(string from, string to)
    : base("Unit Conversion is not setup on 'Units Of Measure' screen. Please setup Unit Conversion FROM {0} TO {1}.", new object[2]
    {
      (object) from,
      (object) to
    })
  {
  }
}
