// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXRateIsNotDefinedForThisDateException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CM;

public class PXRateIsNotDefinedForThisDateException : PXSetPropertyException
{
  public PXRateIsNotDefinedForThisDateException(
    string CuryRateType,
    string FromCuryID,
    string ToCuryID,
    DateTime CuryEffDate)
    : base("Rate is not defined for rate type '{0}' from currency '{1}' to currency '{2}' for date {3}", (PXErrorLevel) 2, new object[4]
    {
      (object) CuryRateType,
      (object) FromCuryID,
      (object) ToCuryID,
      (object) CuryEffDate.ToShortDateString()
    })
  {
  }

  public PXRateIsNotDefinedForThisDateException(CurrencyInfo info)
    : this(info.CuryRateTypeID, info.CuryID, info.BaseCuryID, info.CuryEffDate.Value)
  {
  }

  public PXRateIsNotDefinedForThisDateException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
