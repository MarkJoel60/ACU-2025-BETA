// Decompiled with JetBrains decompiler
// Type: PX.SM.PXEmailPluginService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class PXEmailPluginService : IPXEmailPluginService
{
  protected readonly PXGraph _graph;

  public PXEmailPluginService()
    : this(PXGraph.CreateInstance<PXGraph>())
  {
  }

  public PXEmailPluginService(PXGraph graph)
  {
    this._graph = graph ?? throw new ArgumentNullException(nameof (graph));
  }

  public virtual TSettings GetSettings<TSettings>(
    EMailAccount emailAccount,
    bool insertSettingsIfNotFound = false)
    where TSettings : IEmailPluginSettings
  {
    if (this.GetSettings(emailAccount, insertSettingsIfNotFound) is TSettings settings)
      return settings;
    throw new PXInvalidOperationException("The system could not create an email account plug-in.");
  }

  public virtual IEmailPluginSettings GetSettings(
    EMailAccount emailAccount,
    bool insertSettingsIfNotFound = false)
  {
    if (emailAccount.PluginTypeName == null)
      throw new PXArgumentException("The {0} email account cannot be used as an email plug-in.", emailAccount.Description);
    System.Type settingsType = this.GetService(emailAccount.PluginTypeName).SettingsType;
    System.Type bqlField = this._graph.Caches[settingsType].GetBqlField("EmailAccountID");
    if ((object) bqlField == null)
      throw new PXInvalidOperationException("The system could not create an email account plug-in.");
    PXView pxView = new PXView(this._graph, false, BqlCommand.CreateInstance(typeof (Select<,>), settingsType, typeof (Where<,>), bqlField, typeof (Equal<>), typeof (Required<>), bqlField));
    if (!(pxView.SelectSingle((object) emailAccount.EmailAccountID) is IEmailPluginSettings settings))
    {
      if (insertSettingsIfNotFound)
        settings = pxView.Cache.Insert() as IEmailPluginSettings;
      else
        throw new PXException("Plug-in settings for the {0} email account cannot be found.", new object[1]
        {
          (object) emailAccount.Description
        });
    }
    return settings;
  }

  public virtual IEmailConnectedService GetService(string typeName)
  {
    System.Type pluginType = this.GetPluginType(typeName, true);
    return (typeof (PXGraph).IsAssignableFrom(pluginType) ? (object) PXGraph.CreateInstance(pluginType) : Activator.CreateInstance(pluginType)) is IEmailConnectedService connectedService ? connectedService : throw new PXInvalidOperationException("The system could not create an email account plug-in.");
  }

  public virtual TService GetService<TService>(
    EMailAccount emailAccount,
    bool insertSettingsIfNotFound = false)
    where TService : IEmailConnectedService
  {
    if (this.GetService(emailAccount, insertSettingsIfNotFound) is TService service)
      return service;
    throw new PXInvalidOperationException("The system could not create an email account plug-in.");
  }

  public virtual IEmailConnectedService GetService(
    EMailAccount emailAccount,
    bool insertSettingsIfNotFound = false)
  {
    IEmailPluginSettings settings = this.GetSettings(emailAccount, insertSettingsIfNotFound);
    IEmailConnectedService service = this.GetService(emailAccount.PluginTypeName);
    service.InitializeContext(this._graph, settings);
    return service;
  }

  public virtual System.Type GetPluginType(string typeName, bool throwOnError)
  {
    return PXBuildManager.GetType(typeName, throwOnError);
  }
}
