// Decompiled with JetBrains decompiler
// Type: Page_FS300500
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Objects.EP;
using PX.Objects.FS;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;

#nullable disable
public class Page_FS300500 : PXPage
{
  public string applicationName;
  public string pageUrl;
  public string RefNbr;
  public string eventBodyTemplate;
  public string startDate;
  public string DefaultEmployee;
  public string ExternalEmployee;

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
    DateTime? nullable = PXContext.GetBusinessDate() ?? new DateTime?(PXTimeZoneInfo.Now);
    this.RefNbr = ((Page) this).Request.QueryString["RefNbr"];
    this.ExternalEmployee = ((Page) this).Request.QueryString["bAccountID"];
    string s = ((Page) this).Request.QueryString["Date"];
    if (!string.IsNullOrEmpty(s))
      nullable = new DateTime?(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddMilliseconds(double.Parse(s)));
    this.startDate = nullable.Value.ToString("MM/dd/yyyy h:mm:ss tt", (IFormatProvider) new CultureInfo("en-US"));
    EPEmployee epEmployee = PXResult<EPEmployee, PX.Objects.CR.Contact>.op_Implicit((PXResult<EPEmployee, PX.Objects.CR.Contact>) PXResultset<EPEmployee>.op_Implicit(((PXSelectBase<EPEmployee>) PXGraph.CreateInstance<ExternalControls>().EmployeeSelected).Select(Array.Empty<object>())));
    if (epEmployee != null)
      this.DefaultEmployee = epEmployee.BAccountID.ToString();
    if (string.IsNullOrEmpty(this.ExternalEmployee) && epEmployee != null)
      this.ExternalEmployee = this.DefaultEmployee;
    StreamReader streamReader = new StreamReader(((Page) this).Server.MapPath("../../Shared/templates/AvailabilityEventTemplate.html"));
    this.eventBodyTemplate = streamReader.ReadToEnd();
    streamReader.Close();
  }
}
