// Decompiled with JetBrains decompiler
// Type: Page_FS300900
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Objects.FS;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;

#nullable disable
public class Page_FS300900 : PXPage
{
  public string applicationName;
  public string pageUrl;
  public string infoRoute;
  public string RefNbr;
  public string startDate;
  public string apiKey;
  public string branchID;

  protected void Page_Init(object sender, EventArgs e)
  {
    if (((Control) this).Page.IsCallback)
      return;
    Dictionary<string, string> calendarMessages = SharedFunctions.GetCalendarMessages();
    ((Page) this).ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "localeStrings", $"var __localeStrings={JsonConvert.SerializeObject((object) calendarMessages)};", true);
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    this.applicationName = ((Page) this).Request.ApplicationPath.TrimEnd('/');
    this.pageUrl = SharedFunctions.GetWebMethodPath(((Page) this).Request.Path);
    this.apiKey = SharedFunctions.GetMapApiKey(new PXGraph());
    string str = ((Page) this).Request.QueryString["Date"];
    this.RefNbr = ((Page) this).Request.QueryString["RefNbr"];
    this.branchID = ((Page) this).Request.QueryString["BranchID"];
    DateTime? nullable;
    try
    {
      nullable = !string.IsNullOrEmpty(str) ? new DateTime?(Convert.ToDateTime(str)) : PXContext.GetBusinessDate();
    }
    catch (Exception ex)
    {
      throw;
    }
    nullable = nullable ?? new DateTime?(PXTimeZoneInfo.Now);
    this.startDate = nullable.Value.ToString("MM/dd/yyyy h:mm:ss tt", (IFormatProvider) new CultureInfo("en-US"));
    StreamReader streamReader = new StreamReader(((Page) this).Server.MapPath("../../Shared/templates/InfoRoute.html"));
    this.infoRoute = streamReader.ReadToEnd();
    streamReader.Close();
  }
}
