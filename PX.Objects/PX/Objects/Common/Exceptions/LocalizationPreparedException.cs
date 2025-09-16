// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.LocalizationPreparedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Exceptions;

/// <summary>
/// Exception that saves message and arguments to be localized in wrapping by <see cref="T:PX.Data.PXException" /> or it's children.
/// It helps to skips double localization if you need to throw exception with args,
/// and then catch it and throw new <see cref="T:PX.Data.PXException" /> with same message.
/// </summary>
/// <example>
/// try
/// {
///   throw new LocalizationPreparedException(Messages.Message, arg1, arg2);
/// }
/// catch (LocalizationPreparedException e)
/// {
///   throw new PXException(e.Format, e.Args);
/// }
/// 
/// </example>
public class LocalizationPreparedException : Exception
{
  public LocalizationPreparedException(string format, params object[] args)
  {
    this.Format = format;
    this.Args = args;
  }

  public LocalizationPreparedException(
    Exception innerException,
    string format,
    params object[] args)
    : base((string) null, innerException)
  {
    this.Format = format;
    this.Args = args;
  }

  public string Format { get; }

  public object[] Args { get; }

  public override string Message => string.Format(this.Format, this.Args);
}
