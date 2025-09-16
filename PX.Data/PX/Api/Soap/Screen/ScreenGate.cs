// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ScreenGate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api.ContractBased;
using PX.Api.Models;
using PX.Api.Services;
using PX.Common;
using PX.Data.MultiFactorAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

#nullable disable
namespace PX.Api.Soap.Screen;

public class ScreenGate
{
  public static readonly string SoapApiAuthPrefix = "soap";
  protected readonly ILoginService LoginService = ServiceLocator.Current.GetInstance<ILoginService>();
  protected readonly IScreenService ScreenService = ServiceLocator.Current.GetInstance<IScreenService>();
  protected readonly IMiscService MiscService = ServiceLocator.Current.GetInstance<IMiscService>();
  private readonly IMultiFactorService _multiFactorService = ServiceLocator.Current.GetInstance<IMultiFactorService>();
  private string _screenId;

  protected string ScreenId
  {
    get
    {
      if (this._screenId == null)
        this._screenId = PXContext.GetScreenID();
      if (!string.IsNullOrEmpty(this._screenId))
        this._screenId = this._screenId.Replace(".", "");
      return this._screenId;
    }
  }

  [WebMethod(true)]
  public LoginResult Login(string name, string password)
  {
    this.LoginService.LoginForSoapApi(this._multiFactorService, name, password, (string) null, (string) null, (string) null);
    return new LoginResult()
    {
      Code = LoginResult.ErrorCode.OK,
      Session = HttpContext.Current.Session.SessionID
    };
  }

  [WebMethod(true)]
  public void Logout() => this.LoginService.Logout();

  [WebMethod(true)]
  public void SetBusinessDate(DateTime date)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    this.MiscService.SetBusinessDate(date);
  }

  [WebMethod(true)]
  public void SetLocaleName(string localeName)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    this.MiscService.SetLocaleName(localeName);
  }

  [WebMethod(true)]
  public void SetSchemaMode(SchemaMode mode)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    this.ScreenService.SetMode(mode);
  }

  public virtual Command[] GetScenario(string scenario)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.GetScenario(this.ScreenId, scenario).ToArray<Command>();
  }

  public Content GetSchema(SchemaMode mode)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.GetSchema(this.ScreenId, mode);
  }

  public virtual void SetSchema(Content schema)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    this.ScreenService.Set(this.ScreenId, schema);
  }

  public virtual string[][] Export(
    Command[] commands,
    Filter[] filters,
    int topCount,
    bool includeHeaders,
    bool breakOnError)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.Export(this.ScreenId, commands, filters, 0, topCount, includeHeaders, breakOnError);
  }

  public virtual ImportResult[] Import(
    Command[] commands,
    Filter[] filters,
    string[][] data,
    bool includedHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.Import(this.ScreenId, commands, filters, data, includedHeaders, breakOnError, breakOnIncorrectTarget).ToArray<ImportResult>();
  }

  public virtual void Clear()
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    this.ScreenService.Clear(this.ScreenId);
  }

  public virtual Content[] Submit(Command[] commands)
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.Submit(this.ScreenId, (IEnumerable<Command>) commands, (SchemaMode) PXContext.Session.SchemaMode.GetValueOrDefault()).ToArray<Content>();
  }

  public virtual ProcessResult GetProcessStatus()
  {
    this.LoginService.IsUserAuthenticated(true, ScreenGate.SoapApiAuthPrefix);
    return this.ScreenService.GetProcessStatus(this.ScreenId);
  }
}
