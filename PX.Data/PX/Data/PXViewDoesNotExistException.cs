// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewDoesNotExistException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXViewDoesNotExistException : PXException
{
  public PXViewDoesNotExistException()
  {
  }

  public PXViewDoesNotExistException(string message)
    : base(message)
  {
  }

  public PXViewDoesNotExistException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXViewDoesNotExistException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public PXViewDoesNotExistException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  public PXViewDoesNotExistException(Exception innerException, string format, params object[] args)
    : base(innerException, format, args)
  {
  }

  public void SetMissingPXSelectorAttributeMessage(string controlId, string fieldName)
  {
    this._Message = PXMessages.LocalizeFormatNoPrefixNLA("Cannot create the {0} control.\nThe PXSelector attribute is missing for the {1} field.", (object) controlId, (object) fieldName);
  }
}
