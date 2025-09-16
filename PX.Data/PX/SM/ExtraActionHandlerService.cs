// Decompiled with JetBrains decompiler
// Type: PX.SM.ExtraActionHandlerService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac.Features.Metadata;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

internal class ExtraActionHandlerService : IExtraActionHanlderService
{
  private readonly IDictionary<string, IExtraActionRunHandler> _actionHandlers;

  public ExtraActionHandlerService(
    IEnumerable<Meta<IExtraActionRunHandler, IExtraActionHandlerMetadata>> registeredHandlers)
  {
    this._actionHandlers = (IDictionary<string, IExtraActionRunHandler>) registeredHandlers.ToDictionary<Meta<IExtraActionRunHandler, IExtraActionHandlerMetadata>, string, IExtraActionRunHandler>((Func<Meta<IExtraActionRunHandler, IExtraActionHandlerMetadata>, string>) (it => it.Metadata.Discriminator), (Func<Meta<IExtraActionRunHandler, IExtraActionHandlerMetadata>, IExtraActionRunHandler>) (it => it.Value));
  }

  private IExtraActionRunHandler FindHandler(string discriminator)
  {
    IExtraActionRunHandler actionRunHandler;
    return !this._actionHandlers.TryGetValue(discriminator, out actionRunHandler) ? (IExtraActionRunHandler) null : actionRunHandler;
  }

  public PXButtonDelegate GetActionHandler(ScreenActionExtraData action)
  {
    IExtraActionRunHandler handler = this.FindHandler(action.ActionDiscriminator);
    return handler != null ? handler.GetHandler(action) : (PXButtonDelegate) (adapter => adapter.Get());
  }
}
