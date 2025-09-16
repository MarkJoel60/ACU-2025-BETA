// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.PXExceptionInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Exceptions;

public class PXExceptionInfo
{
  public string MessageFormat { get; }

  public object[] MessageArguments { get; set; }

  public PXErrorLevel? ErrorLevel { get; set; }

  public string Css { get; set; }

  public PXExceptionInfo(string messageFormat, params object[] messageArgs)
  {
    this.MessageFormat = messageFormat;
    this.MessageArguments = messageArgs ?? Array.Empty<object>();
  }

  public PXExceptionInfo(
    PXErrorLevel errorLevel,
    string messageFormat,
    params object[] messageArgs)
    : this(messageFormat, messageArgs)
  {
    this.ErrorLevel = new PXErrorLevel?(errorLevel);
  }

  public PXSetPropertyException ToSetPropertyException()
  {
    PXErrorLevel pxErrorLevel = this.ErrorLevel ?? (PXErrorLevel) 2;
    return string.IsNullOrEmpty(this.Css) ? new PXSetPropertyException(this.MessageFormat, pxErrorLevel, this.MessageArguments) : new PXSetPropertyException($"|css={this.Css}|{PXMessages.LocalizeFormatNoPrefix(this.MessageFormat, this.MessageArguments)}", pxErrorLevel);
  }
}
