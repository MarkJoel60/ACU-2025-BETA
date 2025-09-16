// Decompiled with JetBrains decompiler
// Type: PX.Objects.AU.PXFinPeriodDoesNotExist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.AU;

[Serializable]
internal class PXFinPeriodDoesNotExist : PXSetPropertyException
{
  public PXFinPeriodDoesNotExist(string message)
    : base(message)
  {
  }

  public PXFinPeriodDoesNotExist(string message, PXErrorLevel errorLevel)
    : base(message, errorLevel)
  {
  }

  public PXFinPeriodDoesNotExist(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
