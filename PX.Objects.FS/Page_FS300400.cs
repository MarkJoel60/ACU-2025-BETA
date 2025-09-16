// Decompiled with JetBrains decompiler
// Type: Page_FS300400
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
public class Page_FS300400 : PXPage
{
  public string applicationName;
  public string pageUrl;
  public string RefNbr;
  public string CustomerID;
  public string appointmentBridgeUrl;
  public string appointmentBodyTemplate;
  public string toolTipTemplateServiceOrder;
  public string toolTipTemplateAppointment;
  public string startDate;
  public string DefaultEmployee = "";
  public string ExternalEmployee;
  public string ExternalBranchID;
  public string ExternalBranchLocationID;
  public string SMEquipmentID;
  public string AppSource;

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
    this.CustomerID = ((Page) this).Request.QueryString["CustomerID"];
    this.ExternalEmployee = !string.IsNullOrEmpty(((Page) this).Request.QueryString["EmployeeID"]) ? ((Page) this).Request.QueryString["EmployeeID"] : ((Page) this).Request.QueryString["bAccountID"];
    this.SMEquipmentID = ((Page) this).Request.QueryString["SMEquipmentID"];
    this.AppSource = ((Page) this).Request.QueryString["AppSource"];
    this.ExternalBranchID = ((Page) this).Request.QueryString["branchID"];
    this.ExternalBranchLocationID = ((Page) this).Request.QueryString["branchLocationID"];
    try
    {
      if (!string.IsNullOrEmpty(((Page) this).Request.QueryString["Date"]))
        nullable = new DateTime?(Convert.ToDateTime(((Page) this).Request.QueryString["Date"]));
    }
    catch (Exception ex)
    {
    }
    PXResultset<EPEmployee> pxResultset = ((PXSelectBase<EPEmployee>) PXGraph.CreateInstance<ExternalControls>().EmployeeSelected).Select(Array.Empty<object>());
    this.startDate = nullable.Value.ToString("MM/dd/yyyy h:mm:ss tt", (IFormatProvider) new CultureInfo("en-US"));
    EPEmployee epEmployee = PXResult<EPEmployee, PX.Objects.CR.Contact>.op_Implicit((PXResult<EPEmployee, PX.Objects.CR.Contact>) PXResultset<EPEmployee>.op_Implicit(pxResultset));
    if (epEmployee != null)
      this.DefaultEmployee = epEmployee.BAccountID.ToString();
    if (string.IsNullOrEmpty(this.ExternalEmployee) && epEmployee != null)
      this.ExternalEmployee = this.DefaultEmployee;
    StreamReader streamReader1 = new StreamReader(((Page) this).Server.MapPath("../../Shared/templates/EventTemplate.html"));
    this.appointmentBodyTemplate = streamReader1.ReadToEnd();
    streamReader1.Close();
    StreamReader streamReader2 = new StreamReader(((Page) this).Server.MapPath("../../Shared/templates/TooltipServiceOrder.html"));
    this.toolTipTemplateServiceOrder = streamReader2.ReadToEnd();
    streamReader2.Close();
    StreamReader streamReader3 = new StreamReader(((Page) this).Server.MapPath("../../Shared/templates/TooltipAppointment.html"));
    this.toolTipTemplateAppointment = streamReader3.ReadToEnd();
    streamReader3.Close();
  }
}
