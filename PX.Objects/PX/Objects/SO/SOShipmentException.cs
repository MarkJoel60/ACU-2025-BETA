// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentException : PXException
{
  public SOShipmentException.ErrorCode Code { get; private set; }

  public SOOrderShipment Item { get; private set; }

  public SOShipmentException(
    SOShipmentException.ErrorCode code,
    SOOrderShipment item,
    string message,
    params object[] args)
    : base(message, args)
  {
    this.Code = code;
    this.Item = item;
  }

  public SOShipmentException(string message, params object[] args)
    : base(message, args)
  {
  }

  public SOShipmentException(string message)
    : base(message)
  {
  }

  public SOShipmentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public enum ErrorCode
  {
    None,
    CannotShipTraced,
    NotAllocatedLines,
    CannotShipCompleteTraced,
    NothingToShipTraced,
    NothingToReceiveTraced,
  }
}
