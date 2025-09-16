// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.ErrorProcessingEntityException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.Exceptions;

/// <exclude />
public class ErrorProcessingEntityException : PXException
{
  public IBqlTable Entity { get; protected set; }

  protected ErrorProcessingEntityException(
    PXCache cache,
    IBqlTable entity,
    string localizedError,
    Exception innerException)
    : base(innerException, "{0}: {1}", new object[2]
    {
      (object) cache.GetRowDescription((object) entity),
      (object) localizedError
    })
  {
    this.Entity = entity;
  }

  public ErrorProcessingEntityException(
    PXCache cache,
    IBqlTable entity,
    PXException innerException)
    : this(cache, entity, innerException.MessageNoPrefix, (Exception) innerException)
  {
  }

  public ErrorProcessingEntityException(
    PXCache cache,
    IBqlTable entity,
    PXOuterException innerException)
    : this(cache, entity, ErrorProcessingEntityException.GetFullMessage(innerException), (Exception) innerException)
  {
  }

  public ErrorProcessingEntityException(PXCache cache, IBqlTable entity, string errorMessage)
    : this(cache, entity, PXMessages.LocalizeNoPrefix(errorMessage), (Exception) null)
  {
  }

  public static ErrorProcessingEntityException Create(
    PXCache cache,
    IBqlTable entity,
    Exception exception)
  {
    switch (exception)
    {
      case PXOuterException innerException1:
        return new ErrorProcessingEntityException(cache, entity, innerException1);
      case PXException innerException2:
        return new ErrorProcessingEntityException(cache, entity, innerException2);
      default:
        return new ErrorProcessingEntityException(cache, entity, exception.Message);
    }
  }

  public ErrorProcessingEntityException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  protected static string GetFullMessage(PXOuterException outerException)
  {
    string newLine = Environment.NewLine;
    return ((PXException) outerException).MessageNoPrefix + newLine + string.Join(newLine, outerException.InnerMessages);
  }
}
