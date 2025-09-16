// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.PXFABookPeriodException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.FA;

public class PXFABookPeriodException : PXException
{
  public PXFABookPeriodException()
    : base("Book Period cannot be found in the system.")
  {
  }

  public PXFABookPeriodException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
