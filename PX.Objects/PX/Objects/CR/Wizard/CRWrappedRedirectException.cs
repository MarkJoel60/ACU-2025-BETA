// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Wizard.CRWrappedRedirectException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CR.Wizard;

/// <summary>
/// The service exception that is used to show the exception in the UI during <see cref="M:PX.Data.PXGraph.ExecuteUpdate(System.String,System.Collections.IDictionary,System.Collections.IDictionary,System.Object[])" />.
/// </summary>
/// <remarks>
/// All exceptions that are not inherited from <see cref="T:PX.Data.PXBaseRedirectException" />
/// are hidden by <see cref="M:PX.Data.PXGraph.ExecuteUpdate(System.String,System.Collections.IDictionary,System.Collections.IDictionary,System.Object[])" />.
/// This exception is a wrapper for a normal exception that for some reason (such as, because it is thrown in the wizard),
/// cannot be thrown or shown the usual way.
/// </remarks>
[PXInternalUseOnly]
public class CRWrappedRedirectException : PXBaseRedirectException
{
  public CRWrappedRedirectException(Exception exception)
    : this(exception.Message)
  {
    PXTrace.WriteError(exception);
  }

  public CRWrappedRedirectException(string message)
    : base(message)
  {
  }

  public CRWrappedRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
