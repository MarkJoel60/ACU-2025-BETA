// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.NotificationHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.EP;
using PX.Data.Wiki.Parser;
using PX.Export;
using PX.Reports.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.Maintenance.SM;

public static class NotificationHelper
{
  private const char FileExtensionDelimiter = '.';

  public static string GetValidAddresses(
    string address,
    Tuple<IDictionary<string, object>, IDictionary<string, object>>[] parameters,
    (PXGraph graph, string primaryView) definitionGraph,
    Dictionary<string, AUWorkflowFormField[]> forms)
  {
    if (string.IsNullOrEmpty(address))
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    string str1 = address;
    char[] chArray = new char[1]{ ';' };
    foreach (string templateText in str1.Split(chArray))
    {
      string str2 = PXTemplateContentParser.NullableInstance.Process(templateText, parameters, definitionGraph.graph, definitionGraph.primaryView, forms);
      if (!string.IsNullOrEmpty(str2))
        stringBuilder.Append(str2 + ";");
    }
    return stringBuilder.ToString();
  }

  public static IEnumerable<Tuple<string, byte[]>> GetAttachments(
    StreamManager manager,
    ReportNode reportNode,
    string reportName)
  {
    foreach (ReportStream stream in manager.Streams)
    {
      string source = string.IsNullOrEmpty(reportNode.ExportFileName) ? stream.Name : reportNode.ExportFileName;
      string extension = MimeTypes.GetExtension(stream.MimeType);
      if (extension != null)
      {
        if (source.Contains<char>('.'))
        {
          string str = ((IEnumerable<string>) source.Split('.')).Last<string>();
          if (!extension.OrdinalEquals("." + str) && HttpMimeTypes.GetTypeBySuffix(str.ToLower()) == null)
            source += extension;
        }
        else
          source += extension;
      }
      int length = reportName.LastIndexOf('.');
      if (length > -1)
        reportName = reportName.Substring(0, length);
      yield return Tuple.Create<string, byte[]>($"{reportName}\\{source}", stream.GetBytes());
    }
  }

  public static PXGraph GetGraph()
  {
    PXGraph instance = PXGraph.CreateInstance<PXGraph>();
    if (!instance.Views.ContainsKey("GeneralInfo"))
    {
      GeneralInfoSelect generalInfoSelect = new GeneralInfoSelect(instance);
      instance.Views.Add("GeneralInfo", generalInfoSelect.View);
      generalInfoSelect.View.Cache.Current = generalInfoSelect.View.SelectSingle();
    }
    return instance;
  }
}
