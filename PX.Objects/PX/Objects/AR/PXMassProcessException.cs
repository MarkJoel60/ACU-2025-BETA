// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PXMassProcessException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.AR;

[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2019R1.")]
public class PXMassProcessException : PX.Objects.Common.PXMassProcessException
{
  public PXMassProcessException(int ListIndex, Exception InnerException)
    : base(ListIndex, InnerException)
  {
  }

  public PXMassProcessException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
