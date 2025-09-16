// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.ILongOperationWorkflowAdapter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Async.Internal;

internal interface ILongOperationWorkflowAdapter
{
  void BeforeStartOperation(PXGraph graph);

  void StartOperation(PXLongOperationPars parameters);

  void RestoreWorkflowParameters(PXLongOperationPars parameters);

  void CompleteOperation(
    PXLongOperationPars parameters,
    OperationCompletion operationCompletion,
    ChainEventArgs<Exception> exceptionArgs = null);
}
