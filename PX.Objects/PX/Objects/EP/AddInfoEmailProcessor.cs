// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.AddInfoEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.EP;

public class AddInfoEmailProcessor : BasicEmailProcessor
{
  private static readonly Regex _HTML_REGEX = new Regex("^.*\\<html( [^\\>]*)?\\>.*\\<head( [^\\>]*)?\\>(?<head>.*)\\</([^\\>]* )?head\\>.*\\<body( [^\\>]*)?\\>(?<body>.*)\\</([^\\>]* )?body\\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault() || !account.AddUpInformation.GetValueOrDefault())
      return false;
    PXGraph graph = package.Graph;
    CRSMEmail message = package.Message;
    List<string> briefInfo = new List<string>();
    object referenceEntity = this.GetReferenceEntity(graph, message.RefNoteID);
    this.AddUpInformation(graph, (IList<string>) briefInfo, referenceEntity);
    if (briefInfo.Count > 0)
      this.InsertInformationIntoMessage(message, (IEnumerable<string>) briefInfo);
    return true;
  }

  private void AddUpInformation(PXGraph graph, IList<string> briefInfo, object reference)
  {
    if (reference == null)
      return;
    string str = EntityHelper.GetEntityDescription(graph, reference).With<string, string>((Func<string, string>) (_ => _.Trim()));
    if (string.IsNullOrEmpty(str))
      return;
    string friendlyEntityName = EntityHelper.GetFriendlyEntityName(reference.GetType());
    string format = string.IsNullOrEmpty(friendlyEntityName) ? "{1}" : "{0}: {1}";
    briefInfo.Add(string.Format(format, (object) friendlyEntityName, (object) str));
  }

  private void InsertInformationIntoMessage(CRSMEmail message, IEnumerable<string> briefInfo)
  {
    if (briefInfo == null)
      return;
    string input = message.Body ?? string.Empty;
    Match match = AddInfoEmailProcessor._HTML_REGEX.Match(input);
    if (match.Success)
    {
      Group group = match.Groups["body"];
      string str1 = group.Value;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str2 in briefInfo)
        stringBuilder.AppendFormat("<i>{0}</i><br/>", (object) str2);
      stringBuilder.AppendFormat("<br/>");
      string str3 = stringBuilder.ToString();
      if (!str1.StartsWith(str3))
      {
        string str4 = str1.Insert(0, str3);
        input = input.Substring(0, group.Index) + str4 + input.Substring(group.Index + group.Length);
      }
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("***");
      foreach (string str in briefInfo)
        stringBuilder.AppendLine(str);
      stringBuilder.AppendLine("***");
      stringBuilder.AppendLine();
      string str5 = stringBuilder.ToString();
      if (!input.StartsWith(str5))
        input = input.Insert(0, str5);
    }
    message.Body = PXRichTextConverter.NormalizeHtml(input);
  }

  private object GetReferenceEntity(PXGraph graph, Guid? refNoteId)
  {
    return !refNoteId.HasValue ? (object) null : new EntityHelper(graph).GetEntityRow(new Guid?(refNoteId.Value), true);
  }
}
