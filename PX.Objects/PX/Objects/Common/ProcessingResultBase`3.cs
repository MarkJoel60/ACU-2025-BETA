// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ProcessingResultBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public abstract class ProcessingResultBase<TProcessingResult, TResult, TMessage> : 
  IProcessingResult<TResult>
  where TProcessingResult : IProcessingResult<TResult>, new()
  where TMessage : ProcessingResultMessage
{
  protected readonly List<TMessage> _messages;

  public TResult Result { get; set; }

  public IReadOnlyList<TMessage> Messages => (IReadOnlyList<TMessage>) this._messages;

  /// <summary>true, if result does not have error level messages</summary>
  public virtual bool IsSuccess
  {
    get
    {
      return this._messages.All<TMessage>((Func<TMessage, bool>) (message => message.ErrorLevel != 5 && message.ErrorLevel != 4));
    }
  }

  public bool HasWarning
  {
    get
    {
      return this._messages.Any<TMessage>((Func<TMessage, bool>) (message => message.ErrorLevel == 3 || message.ErrorLevel == 2));
    }
  }

  public bool HasWarningOrError => this.HasWarning || !this.IsSuccess;

  public PXErrorLevel MaxErrorLevel
  {
    get
    {
      return this._messages.Max<TMessage, PXErrorLevel>((Func<TMessage, PXErrorLevel>) (message => message.ErrorLevel));
    }
  }

  public static TProcessingResult Success => new TProcessingResult();

  public static TProcessingResult CreateSuccess(TResult value)
  {
    TProcessingResult success = new TProcessingResult();
    success.SetResult(value);
    return success;
  }

  public void SetResult(TResult value) => this.Result = value;

  public ProcessingResultBase() => this._messages = new List<TMessage>();

  public void AddErrorMessage(string message, params object[] args)
  {
    this.AddMessage((PXErrorLevel) 4, message, args);
  }

  public void AddErrorMessage(string message) => this.AddMessage((PXErrorLevel) 4, message);

  public abstract void AddMessage(PXErrorLevel errorLevel, string message, params object[] args);

  public abstract void AddMessage(PXErrorLevel errorLevel, string message);

  public void Aggregate(
    ProcessingResultBase<TProcessingResult, TResult, TMessage> processingResult)
  {
    this._messages.AddRange((IEnumerable<TMessage>) processingResult.Messages);
  }

  public void RaiseIfHasError<TException>() where TException : Exception
  {
    if (!this.IsSuccess)
      throw (object) (TException) Activator.CreateInstance(typeof (TException), (object) this.GeneralMessage);
  }

  public void RaiseIfHasError() => this.RaiseIfHasError<PXException>();

  public virtual ProcessingResultBase<TProcessingResult, TResult, TMessage> ThisOrRaiseIfHasError<TException>() where TException : Exception
  {
    this.RaiseIfHasError<TException>();
    return this;
  }

  public virtual ProcessingResultBase<TProcessingResult, TResult, TMessage> ThisOrRaiseIfHasError()
  {
    return this.ThisOrRaiseIfHasError<PXSetPropertyException>();
  }

  public TResult GetValueOrRaiseError()
  {
    if (!this.IsSuccess)
      throw new PXException(this.GetGeneralMessage());
    return this.Result;
  }

  public virtual string GetGeneralMessage() => this.GeneralMessage;

  public virtual string GeneralMessage
  {
    get
    {
      return string.Join(Environment.NewLine, this.Messages.Select<TMessage, string>((Func<TMessage, string>) (message => message.ToString())).ToArray<string>());
    }
  }
}
