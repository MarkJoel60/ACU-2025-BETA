// Decompiled with JetBrains decompiler
// Type: PX.SM.TraceMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Licensing;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

#nullable disable
namespace PX.SM;

public class TraceMaint : PXGraph<TraceMaint>
{
  private static readonly System.Type[] traceFields = new System.Type[6]
  {
    typeof (TraceItem.raiseDateTime),
    typeof (TraceItem.screenID),
    typeof (TraceItem.eventType),
    typeof (TraceItem.source),
    typeof (TraceItem.fullmessage),
    typeof (TraceItem.stackTrace)
  };
  public PXSelectOrderBy<TraceItem, OrderBy<Asc<TraceItem.raiseDateTime>>> Items;
  public PXFilter<TraceMessage> Message;
  public PXAction<TraceMessage> Send;

  [InjectDependency]
  private ILicensingManager _licensingManager { get; set; }

  [PXSendMailButton]
  [PXUIField(DisplayName = "Send")]
  public IEnumerable send(PXAdapter adapter)
  {
    if (this.Message.View.Answer == WebDialogResult.OK)
      return adapter.Get();
    if (this.Message.Current == null)
      this.Message.Current = new TraceMessage();
    int? defaultMailAccountId = MailAccountManager.DefaultMailAccountID;
    string mailTo = this.Message.Current.MailTo;
    string prettyInstallationId = this._licensingManager.PrettyInstallationId;
    string str1 = prettyInstallationId != null ? this.Message.Current.Subject : this.Message.Current.Subject;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.Message.Current.Body).AppendLine("----");
    stringBuilder.AppendFormat("'''Version: '''{0}", (object) PXVersionInfo.Version).AppendLine().AppendLine();
    stringBuilder.AppendLine($"[{prettyInstallationId}]");
    stringBuilder.AppendLine("{|cellspacing=\"0\" border=\"1\"");
    foreach (MemberInfo traceField in TraceMaint.traceFields)
    {
      PXFieldState stateExt = (PXFieldState) this.Items.Cache.GetStateExt((object) null, traceField.Name);
      stringBuilder.Append("! ").AppendLine(stateExt.DisplayName);
    }
    foreach (TraceItem data in this.Items.Cache.Inserted)
    {
      stringBuilder.AppendLine("|-");
      foreach (System.Type traceField in TraceMaint.traceFields)
      {
        object stateExt = this.Items.Cache.GetStateExt((object) data, traceField.Name);
        string str2 = stateExt.ToString();
        PXIntState pxIntState = stateExt as PXIntState;
        PXStringState pxStringState = stateExt as PXStringState;
        if (pxIntState != null && pxIntState.AllowedValues != null)
        {
          for (int index = 0; index < pxIntState.AllowedValues.Length; ++index)
          {
            if (pxIntState.AllowedValues[index] == (int) pxIntState.Value)
            {
              str2 = pxIntState.AllowedLabels[index];
              break;
            }
          }
        }
        if (pxStringState != null && !string.IsNullOrEmpty(pxStringState.InputMask))
          str2 = Mask.Format(pxStringState.InputMask, str2);
        stringBuilder.Append("| ").Append("style=\"vertical-align: top\" ");
        stringBuilder.Append("| ").AppendLine(str2.Replace("\r\n", "{br}"));
      }
    }
    stringBuilder.AppendLine("|}");
    string str3 = stringBuilder.ToString();
    try
    {
      NotificationSenderProvider.Notify(new EmailNotificationParameters()
      {
        EmailAccountID = defaultMailAccountId,
        To = mailTo,
        Subject = str1,
        Body = str3
      });
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch
    {
    }
    int num = (int) this.Message.Ask("Warning", "The message has been sent.", MessageButtons.OK);
    return adapter.Get();
  }

  public static TraceItem[] GetTrace() => Array.Empty<TraceItem>();

  public static bool SendTrace(Page page, params TraceItem[] items)
  {
    if (HttpContext.Current == null)
      return false;
    TraceMaint graph = new TraceMaint();
    string url = PXSiteMap.Provider.FindSiteMapNode(graph.GetType()).Url;
    if (string.IsNullOrEmpty(url))
      return false;
    foreach (TraceItem traceItem in items)
      graph.Items.Insert(traceItem);
    graph.Items.Cache.IsDirty = false;
    graph.Unload();
    string str = url.ToRelativeUrl() + "?preserveSession=true";
    PXContext.Session.SetRedirectGraphType(str, (PXGraph) graph);
    PXContext.Session.PageInfo[str] = (object) graph.GetType();
    Redirector.Redirect(HttpContext.Current, str);
    return true;
  }
}
