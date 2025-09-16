// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPMeasureMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Update.UntypedService;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

#nullable disable
namespace PX.Data.Update;

[PXHidden]
public class UPMeasureMaintenance : PXGraph<UPMeasureMaintenance, UPMeasureEndpoint>
{
  private const string MeasureScreenID = "SM203590";
  public PXSelect<UPMeasureEndpoint> Endpoints;
  public PXSelect<UPMeasureHistory, Where<UPMeasureHistory.endpointID, Equal<Current<UPMeasureEndpoint.endpointID>>>, OrderBy<Desc<UPMeasureHistory.measureID>>> History;
  public PXSelectSiteMapTree<False, False, False, False, False> SiteMap;
  public PXAction<UPMeasureEndpoint> Measure;

  public UPMeasureMaintenance()
  {
    this.History.Cache.AllowUpdate = false;
    this.History.Cache.AllowInsert = false;
  }

  [PXUIField(DisplayName = "Measure")]
  [PXProcessButton]
  public IEnumerable measure(PXAdapter a)
  {
    UPMeasureEndpoint row = this.Endpoints.Current != null ? this.Endpoints.Current : throw new PXException("{0} is empty.", new object[1]
    {
      (object) "Endpoint"
    });
    this.Save.Press();
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => UPMeasureMaintenance.MeasureProcess(row)));
    return a.Get();
  }

  public static void MeasureProcess(UPMeasureEndpoint endpoint)
  {
    Screen screen1 = new Screen(UPMeasureMaintenance.AdjustUrl(endpoint.Url));
    screen1.CookieContainer = new CookieContainer();
    PX.Data.Update.UntypedService.LoginResult loginResult = screen1.Login(endpoint.Login, endpoint.Password);
    if (loginResult.Code != PX.Data.Update.UntypedService.ErrorCode.OK)
      throw new Exception(loginResult.Message);
    Content schema = screen1.GetSchema("SM203590");
    IEnumerable<PX.Data.Update.UntypedService.Field> source1 = ((IEnumerable<Container>) schema.Containers).SelectMany<Container, PX.Data.Update.UntypedService.Field>((Func<Container, IEnumerable<PX.Data.Update.UntypedService.Field>>) (c => (IEnumerable<PX.Data.Update.UntypedService.Field>) c.Fields)).Where<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => f != null));
    int length = endpoint.Count.Value >= 0 ? endpoint.Count.Value : 0;
    int[] source2 = new int[length];
    int[] source3 = new int[length];
    int[] source4 = new int[length];
    for (int index = -1; index < length; ++index)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      Screen screen2 = screen1;
      Command[] commands = new Command[6];
      commands[0] = (Command) ((IEnumerable<PX.Data.Update.UntypedService.Action>) schema.Actions).First<PX.Data.Update.UntypedService.Action>((Func<PX.Data.Update.UntypedService.Action, bool>) (a => PXLocalesProvider.CollationComparer.Equals(a.FieldName, "Cancel")));
      PX.Data.Update.UntypedService.Value obj1 = new PX.Data.Update.UntypedService.Value();
      obj1.Value = endpoint.Screen;
      obj1.LinkedCommand = (Command) source1.First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.FieldName, "Screen")));
      commands[1] = (Command) obj1;
      PX.Data.Update.UntypedService.Value obj2 = new PX.Data.Update.UntypedService.Value();
      obj2.Value = endpoint.Action;
      obj2.LinkedCommand = (Command) source1.First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => f.FieldName == "Action"));
      commands[2] = (Command) obj2;
      commands[3] = (Command) ((IEnumerable<PX.Data.Update.UntypedService.Action>) schema.Actions).First<PX.Data.Update.UntypedService.Action>((Func<PX.Data.Update.UntypedService.Action, bool>) (a => PXLocalesProvider.CollationComparer.Equals(a.FieldName, "Measure")));
      commands[4] = (Command) source1.First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.FieldName, "Interval")));
      commands[5] = (Command) source1.First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.FieldName, "UsersCount")));
      Content[] contentArray = screen2.Submit("SM203590", commands);
      stopwatch.Stop();
      if (index >= 0)
      {
        source3[index] = int.Parse(((IEnumerable<Container>) contentArray[0].Containers).SelectMany<Container, PX.Data.Update.UntypedService.Field>((Func<Container, IEnumerable<PX.Data.Update.UntypedService.Field>>) (c => (IEnumerable<PX.Data.Update.UntypedService.Field>) c.Fields)).Where<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => f != null)).First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.FieldName, "Interval"))).Value);
        source4[index] = int.Parse(((IEnumerable<Container>) contentArray[0].Containers).SelectMany<Container, PX.Data.Update.UntypedService.Field>((Func<Container, IEnumerable<PX.Data.Update.UntypedService.Field>>) (c => (IEnumerable<PX.Data.Update.UntypedService.Field>) c.Fields)).Where<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => f != null)).First<PX.Data.Update.UntypedService.Field>((Func<PX.Data.Update.UntypedService.Field, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.FieldName, "UsersCount"))).Value);
        source2[index] = (int) stopwatch.ElapsedMilliseconds;
      }
      Thread.Sleep(100);
    }
    UPMeasureMaintenance instance = PXGraph.CreateInstance<UPMeasureMaintenance>();
    instance.Endpoints.Current = endpoint;
    instance.History.Insert(new UPMeasureHistory()
    {
      EndpointID = endpoint.EndpointID,
      Date = new System.DateTime?(System.DateTime.Now),
      NetworkTime = new int?((int) ((IEnumerable<int>) source2).Average()),
      OperationTime = new int?((int) ((IEnumerable<int>) source3).Average()),
      UsersCount = new int?((int) ((IEnumerable<int>) source4).Average())
    });
    instance.Save.Press();
  }

  private static string AdjustUrl(string url)
  {
    if (!url.EndsWith("asmx"))
    {
      if (!url.EndsWith("/"))
        url += "/";
      url += "Soap/.asmx";
    }
    return url;
  }

  protected virtual void UPMeasureEndpoint_Screen_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    string screen = ((string) e.NewValue).Replace(".", "");
    UPMeasureMaintenance.EnshureScreen(screen);
    e.NewValue = (object) screen;
  }

  protected virtual void UPMeasureEndpoint_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is UPMeasureEndpoint row) || row.Screen == null)
      return;
    PXSiteMap.ScreenInfo screenInfo = UPMeasureMaintenance.EnshureScreen(row.Screen);
    if (screenInfo == null)
      return;
    PXSiteMap.ScreenInfo.Action[] actions = screenInfo.Actions;
    PXStringListAttribute.SetList<UPMeasureEndpoint.action>(sender, (object) row, ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actions).Select<PXSiteMap.ScreenInfo.Action, string>((Func<PXSiteMap.ScreenInfo.Action, string>) (a => a.Name)).ToArray<string>(), ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actions).Select<PXSiteMap.ScreenInfo.Action, string>((Func<PXSiteMap.ScreenInfo.Action, string>) (a => a.DisplayName)).ToArray<string>());
  }

  public static PXSiteMap.ScreenInfo EnshureScreen(string screen)
  {
    if (string.IsNullOrEmpty(screen))
      return (PXSiteMap.ScreenInfo) null;
    return PXSiteMap.Provider.FindSiteMapNodeByScreenID(screen) != null ? ScreenUtils.ScreenInfo.Get(screen) : throw new PXSetPropertyException("{0} has not been found.", new object[1]
    {
      (object) "Screen"
    });
  }
}
