// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DataIntegrity.DataIntegrityException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.DataIntegrity;

public class DataIntegrityException : PXException
{
  public string InconsistencyCode { get; private set; } = "UNKNOWN";

  public DataIntegrityException()
    : base("An error occurred during record processing. Your changes cannot be saved. Please copy the error details from Help > Trace, and contact Support service.")
  {
  }

  public DataIntegrityException(string inconsistencyCode, string message)
    : base(message)
  {
    this.InconsistencyCode = inconsistencyCode;
  }

  public DataIntegrityException(string inconsistencyCode, string format, params object[] args)
    : base(format, args)
  {
    this.InconsistencyCode = inconsistencyCode;
  }

  public DataIntegrityException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
