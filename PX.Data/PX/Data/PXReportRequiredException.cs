// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReportRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>The exception that opens the specified report in the same window or a new one.</summary>
/// <example><para>The following code launches a report with one parameter.</para>
///   <code title="Example" lang="CS">
/// public PXAction&lt;Customer&gt; printInvoice;
/// 
/// [PXUIField(DisplayName = "Print Invoice", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
/// public virtual IEnumerable PrintInvoice(PXAdapter adapter)
/// {
///     Customer customer = [fetch desired customer record here];
/// 
///     if (customer != null &amp;&amp; customer.RefNbr != null)
///     {
///         // Add your report parameters to a Dictionary&lt;string, string&gt; collection.
///         // The dictionary key is the parameter name as shown in the Report Designer.
///         // The dictionary value is the value you assign to this parameter.
///         Dictionary&lt;string, string&gt; parameters = new Dictionary&lt;string, string&gt;();
///         parameters["RefNbr"] = customer.RefNbr;
/// 
///         // Provide the report ID
///         string reportID = "AR641000";
/// 
///         // Provide a title name for your report page
///         string reportName = "Customer Invoice"
/// 
///         // Redirect to report page by throwing a PXReportRequiredException object
///         throw new PXReportRequiredException(parameters, reportID, reportName);
///     }
/// 
///     return adapter.Get();
/// }</code>
/// </example>
public class PXReportRequiredException : PXBaseRedirectException
{
  protected IPXResultset _Resultset;
  protected Dictionary<string, string> _Parameters;
  protected string _ReportID;
  protected const string _REPORTID_PARAM_KEY = "ReportIDParamKey";
  /// <exclude />
  public const string DeviceHubParamSeparator = "~";
  protected List<string> _SiblingReports;
  protected List<object> _SiblingParameters;
  private int _mergeCount;

  /// <summary>Appends the specified report with the redirect parameters to the exception.</summary>
  /// <param name="reportId">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="redirectParameters">The report parameters and the result set to be used for the report.</param>
  /// <remarks>You can use this method to merge multiple reports into a single one.</remarks>
  public void AddSibling(string reportId, PXReportRedirectParameters redirectParameters)
  {
    if (this._SiblingReports == null)
    {
      this._SiblingReports = new List<string>();
      this._SiblingParameters = new List<object>();
    }
    this._SiblingReports.Add(reportId);
    this._SiblingParameters.Add((object) redirectParameters);
    this._mergeCount = 0;
  }

  /// <exclude />
  [Obsolete("Use AddSibling(string reportId, PXReportRedirectParameters redirectParameters) instead", false)]
  protected void AddSibling(string reportID, object value)
  {
    if (this._SiblingReports == null)
    {
      this._SiblingReports = new List<string>();
      this._SiblingParameters = new List<object>();
    }
    this._SiblingReports.Add(reportID);
    this._SiblingParameters.Add(value);
    this._mergeCount = 0;
  }

  /// <summary>Appends the specified report with the report parameters to the exception.</summary>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="parameters">
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </param>
  public void AddSibling(string reportID, Dictionary<string, string> parameters)
  {
    this.AddSibling(reportID, new PXReportRedirectParameters()
    {
      ReportParameters = parameters
    });
    if (!parameters.ContainsKey("ReportIDParamKey"))
      return;
    parameters["ReportIDParamKey"] = reportID;
  }

  /// <summary>Appends the specified report with the result set to the exception.</summary>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="resultset">The data to be used for the report generation.</param>
  public void AddSibling(string reportID, IPXResultset resultset)
  {
    this.AddSibling(reportID, new PXReportRedirectParameters()
    {
      ResultSet = resultset
    });
  }

  public static PXReportRequiredException CombineReport(
    PXReportRequiredException ex,
    string reportID,
    Dictionary<string, string> parameters,
    CurrentLocalization localization = null)
  {
    return PXReportRequiredException.CombineReport(ex, reportID, parameters, true, localization);
  }

  public static PXReportRequiredException CombineReport(
    PXReportRequiredException ex,
    string reportID,
    Dictionary<string, string> parameters,
    bool mergeLast,
    CurrentLocalization localization = null)
  {
    return PXReportRequiredException.CombineReport(ex, reportID, parameters, mergeLast, false, localization);
  }

  public static PXReportRequiredException CombineReport(
    PXReportRequiredException ex,
    string reportID,
    Dictionary<string, string> parameters,
    bool mergeLast,
    bool printWithDeviceHub,
    CurrentLocalization localization = null)
  {
    if (ex == null)
      ex = new PXReportRequiredException(PXReportRequiredException.FormatParams(parameters, printWithDeviceHub), reportID, reportID, localization);
    else
      ex.AddSibling(reportID, parameters, mergeLast, printWithDeviceHub);
    return ex;
  }

  /// <summary>Appends the specified report with the report parameters to the exception.</summary>
  /// <param name="parameters">
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <example><para>The AddSibling method will append the reports generated for the service orders to the PXReportRequiredException instance initially created for the first processed service order. After all selected service orders have been processed, a single PXReportRequiredException will be thrown to redirect the user to the Report Viewer displaying all generated reports at once.</para>
  ///   <code title="Example" lang="CS">
  /// public class PrintServiceOrderProcess : PXGraph&lt;PrintServiceOrderProcess&gt;
  /// {
  ///     public PXCancel&lt;FSServiceOrder&gt; Cancel;
  ///     public PXProcessing&lt;FSServiceOrder&gt; ServiceOrderRecords;
  /// 
  ///     public PrintServiceOrderProcess()
  ///     {
  ///         ServiceOrderRecords.SetProcessDelegate(list =&gt; PrintServiceOrders(list));
  ///     }
  /// 
  ///     public static void PrintServiceOrders(IEnumerable&lt;FSServiceOrder&gt; list)
  ///     {
  ///         PXReportRequiredException ex = null;
  ///         foreach (var order in list)
  ///         {
  ///             Dictionary&lt;string, string&gt; parameters = new Dictionary&lt;string, string&gt;();
  ///             string srvOrdType = SharedFunctions
  ///                 .GetFieldName&lt;FSServiceOrder.srvOrdType&gt;(true);
  ///             string refNbr = SharedFunctions
  ///                 .GetFieldName&lt;FSServiceOrder.refNbr&gt;(true);
  ///             parameters[srvOrdType] = order.SrvOrdType;
  ///             parameters[refNbr] = order.RefNbr;
  /// 
  ///             if (ex == null)
  ///             {
  ///                 ex = new PXReportRequiredException(parameters, "SD641000", "SD641000");
  ///             }
  ///             else
  ///             {
  ///                 ex.AddSibling("SD641000", parameters, false);
  ///             }
  ///         }
  ///         if (ex != null) throw ex;
  ///     }
  /// }</code>
  /// </example>
  public void AddSibling(string reportID, Dictionary<string, string> parameters, bool mergeLast)
  {
    this.AddSibling(reportID, parameters, mergeLast, false);
  }

  public void AddSibling(
    string reportID,
    Dictionary<string, string> parameters,
    bool mergeLast,
    bool printWithDeviceHub)
  {
    if (mergeLast)
    {
      if (this._SiblingReports == null && (reportID != this.ReportID || !this.MergeParameters((object) parameters, this._Parameters, printWithDeviceHub)))
      {
        this.AddSibling(reportID, PXReportRequiredException.FormatParams(parameters, printWithDeviceHub));
      }
      else
      {
        if (this._SiblingReports == null)
          return;
        int index = this._SiblingReports.Count - 1;
        Dictionary<string, string> siblingParameter = this._SiblingParameters[index] as Dictionary<string, string>;
        if (!(this._SiblingReports[index] != reportID) && siblingParameter != null && this.MergeParameters((object) parameters, siblingParameter, printWithDeviceHub))
          return;
        this.AddSibling(reportID, PXReportRequiredException.FormatParams(parameters));
      }
    }
    else
      this.AddSibling(reportID, parameters);
  }

  private static Dictionary<string, string> FormatParams(Dictionary<string, string> parameters)
  {
    return PXReportRequiredException.FormatParams(parameters, false);
  }

  private static Dictionary<string, string> FormatParams(
    Dictionary<string, string> parameters,
    bool printWithDeviceHub)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> parameter in parameters)
      dictionary.Add(!parameter.Key.Contains<char>('.') || parameter.Key.EndsWith("0") ? parameter.Key : $"{parameter.Key}{(printWithDeviceHub ? "~" : string.Empty)}0", parameter.Value);
    return dictionary;
  }

  private static object FormatParams(object p)
  {
    return p is Dictionary<string, string> parameters ? (object) PXReportRequiredException.FormatParams(parameters) : p;
  }

  public bool HasSiblings => this._SiblingReports != null;

  private bool MergeParameters(object source, Dictionary<string, string> destination)
  {
    return this.MergeParameters(source, destination, false);
  }

  private bool MergeParameters(
    object source,
    Dictionary<string, string> destination,
    bool printWithDeviceHub)
  {
    Dictionary<string, string> dictionary1 = source as Dictionary<string, string>;
    Dictionary<string, string> dictionary2 = destination;
    if (dictionary1 == null || destination == null)
      return false;
    ++this._mergeCount;
    foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
    {
      if (keyValuePair.Key.Contains<char>('.'))
        dictionary2.Add(keyValuePair.Key + (printWithDeviceHub ? "~" : string.Empty) + this._mergeCount.ToString(), keyValuePair.Value);
    }
    return true;
  }

  public IPXResultset Resultset => this._Resultset;

  public Dictionary<string, string> Parameters => this._Parameters;

  public string ReportID => this._ReportID;

  public bool SeparateWindows { get; set; }

  public object Value
  {
    get
    {
      PXReportsRedirectList reportsRedirectList = new PXReportsRedirectList(this.SeparateWindows);
      reportsRedirectList.Add(new KeyValuePair<string, object>((string) null, (object) new PXReportRedirectParameters()
      {
        ReportParameters = this._Parameters,
        ResultSet = this._Resultset
      }));
      if (this._SiblingReports != null)
        reportsRedirectList.AddRange(this._SiblingReports.Select<string, KeyValuePair<string, object>>((Func<string, int, KeyValuePair<string, object>>) ((t, i) => new KeyValuePair<string, object>(t, this._SiblingParameters[i]))));
      return (object) reportsRedirectList;
    }
  }

  protected static IPXResultset _CreateResulset(IBqlTable item)
  {
    if (item == null)
      throw new PXArgumentException(nameof (item), "The argument cannot be null.");
    return item != null ? (IPXResultset) new PXReportResultset(new System.Type[1]
    {
      item.GetType()
    })
    {
      new object[1]{ (object) item }
    } : throw new PXArgumentException(nameof (item), "An invalid argument has been specified.");
  }

  /// <summary>Instantiates an exception with the specified data to be used for the report generation, report ID, and the title of the report page.</summary>
  /// <param name="resultset">The data to be used for the report generation.</param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="message">The title of the report page in the browser.</param>
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    string message,
    CurrentLocalization localization = null)
    : base(message)
  {
    if (resultset is PXReportRedirectParameters redirectParameters)
    {
      this._Resultset = redirectParameters.ResultSet;
      this._Parameters = redirectParameters.ReportParameters;
    }
    else
      this._Resultset = resultset;
    this._ReportID = reportID;
    this.AddLocalizationParameter(localization);
  }

  /// <exclude />
  public PXReportRequiredException(
    IBqlTable item,
    string reportID,
    string message,
    CurrentLocalization localization = null)
    : this(PXReportRequiredException._CreateResulset(item), reportID, message, localization)
  {
  }

  /// <exclude />
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : base(format, args)
  {
    if (resultset is PXReportRedirectParameters redirectParameters)
    {
      this._Resultset = redirectParameters.ResultSet;
      this._Parameters = redirectParameters.ReportParameters;
    }
    else
      this._Resultset = resultset;
    this._ReportID = reportID;
    this._mergeCount = 0;
    this.AddLocalizationParameter(localization);
  }

  /// <exclude />
  public PXReportRequiredException(
    IBqlTable item,
    string reportID,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : this(PXReportRequiredException._CreateResulset(item), reportID, format, localization, args)
  {
  }

  /// <summary>Instantiates an exception with the specified report parameters, report ID, and title of the report page.</summary>
  /// <param name="parameters">
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="message">The title of the report page in the browser.</param>
  public PXReportRequiredException(
    Dictionary<string, string> parameters,
    string reportID,
    string message,
    CurrentLocalization localization = null)
    : base(message)
  {
    this._Parameters = parameters;
    this._ReportID = reportID;
    if (this._Parameters != null)
      this._Parameters["ReportIDParamKey"] = this._ReportID;
    this.AddLocalizationParameter(localization);
  }

  /// <exclude />
  public PXReportRequiredException(
    Dictionary<string, string> parameters,
    string reportID,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : base(format, args)
  {
    this._Parameters = parameters;
    this._ReportID = reportID;
    if (this._Parameters != null)
      this._Parameters["ReportIDParamKey"] = this._ReportID;
    this.AddLocalizationParameter(localization);
  }

  /// <summary>Instantiates an exception with the specified report parameters and report ID.</summary>
  /// <param name="parameters">
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  public PXReportRequiredException(
    Dictionary<string, string> parameters,
    string reportID,
    CurrentLocalization localization = null)
    : this(parameters, reportID, string.Empty, localization)
  {
  }

  /// <summary>Instantiates an exception with the specified data to be used for the report generation, report ID, opening mode, and the title of the report page.</summary>
  /// <param name="resultset">The data to be used for the report generation.</param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="message">The title of the report page in the browser.</param>
  /// <param name="newWindow">The mode of opening the report in the browser.</param>
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string message,
    CurrentLocalization localization = null)
    : this(resultset, reportID, message, localization)
  {
    this.Mode = newWindow;
  }

  /// <summary>Instantiates an exception with the specified data to be used for the report generation, report ID, and opening mode.</summary>
  /// <param name="resultset">The data to be used for the report generation.</param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="newWindow">The mode of opening the report in the browser.</param>
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    CurrentLocalization localization = null)
    : this(resultset, reportID, newWindow, string.Empty, localization)
  {
  }

  /// <summary>Instantiates an exception with the specified data to be used for the report generation and the report ID.</summary>
  /// <param name="resultset">The data to be used for the report generation.</param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    CurrentLocalization localization = null)
    : this(resultset, reportID, PXBaseRedirectException.WindowMode.New, localization)
  {
  }

  /// <exclude />
  public PXReportRequiredException(
    IBqlTable item,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string message,
    CurrentLocalization localization = null)
    : this(item, reportID, message, localization)
  {
    this.Mode = newWindow;
  }

  /// <exclude />
  public PXReportRequiredException(
    IPXResultset resultset,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : this(resultset, reportID, format, localization, args)
  {
    this.Mode = newWindow;
  }

  /// <exclude />
  public PXReportRequiredException(
    IBqlTable item,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : this(item, reportID, format, localization, args)
  {
    this.Mode = newWindow;
  }

  /// <summary>Instantiates an exception with the specified report parameters, report ID, opening mode, and the title of the report page.</summary>
  /// <param name="parameters">
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </param>
  /// <param name="reportID">The report identifier, such as <em>AP654000</em>.</param>
  /// <param name="newWindow">The mode of opening the report in the browser.</param>
  /// <param name="message">The title of the report page in the browser.</param>
  public PXReportRequiredException(
    Dictionary<string, string> parameters,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string message,
    CurrentLocalization localization = null)
    : this(parameters, reportID, message, localization)
  {
    this.Mode = newWindow;
  }

  /// <exclude />
  public PXReportRequiredException(
    Dictionary<string, string> parameters,
    string reportID,
    PXBaseRedirectException.WindowMode newWindow,
    string format,
    CurrentLocalization localization = null,
    params object[] args)
    : this(parameters, reportID, format, localization, args)
  {
    this.Mode = newWindow;
  }

  /// <exclude />
  public PXReportRequiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXReportRequiredException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXReportRequiredException>(this, info);
    base.GetObjectData(info, context);
  }

  public void AddLocalizationParameter(CurrentLocalization paramLocalization = null)
  {
    CurrentLocalization slot = PXContext.GetSlot<CurrentLocalization>();
    string str = !string.IsNullOrEmpty(slot?.LocalizationCode) ? slot?.LocalizationCode : (!string.IsNullOrEmpty(paramLocalization?.LocalizationCode) ? paramLocalization?.LocalizationCode : (string) null);
    if (string.IsNullOrEmpty(str))
      return;
    if (this._Parameters == null)
      this._Parameters = new Dictionary<string, string>()
      {
        {
          "EnabledLocalization",
          str
        }
      };
    else
      this._Parameters.Add("EnabledLocalization", str);
  }
}
