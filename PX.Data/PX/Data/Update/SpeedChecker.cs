// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.SpeedChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Metadata;
using PX.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Update;

[PXHidden(ServiceVisible = true)]
public class SpeedChecker : PXGraph<SpeedChecker>
{
  public PXFilter<UPSpeedCheck> Check;
  public PXCancel<UPSpeedCheck> Cancel;
  public PXAction<UPSpeedCheck> Measure;

  [InjectDependency]
  private IUserManagementService _userManagementService { get; set; }

  [PXUIField(DisplayName = "Measure")]
  [PXProcessButton]
  public IEnumerable measure(PXAdapter a)
  {
    UPSpeedCheck row = this.Check.Current != null ? this.Check.Current : throw new PXException("{0} is empty.", new object[1]
    {
      (object) "Check"
    });
    if (string.IsNullOrEmpty(row.Screen))
      throw new PXException("{0} is empty.", new object[1]
      {
        (object) "Screen"
      });
    if (string.IsNullOrEmpty(row.Action))
      throw new PXException("{0} is empty.", new object[1]
      {
        (object) "Action"
      });
    System.Type type = PXBuildManager.GetType(ScreenUtils.ScreenInfo.Get(row.Screen).GraphName, false);
    if (type == (System.Type) null)
      throw new PXException("{0} has not been found.", new object[1]
      {
        (object) "GraphType"
      });
    System.Reflection.FieldInfo fieldInfo = ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Public)).FirstOrDefault<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => PXLocalesProvider.CollationComparer.Equals(f.Name, row.Action)));
    if (fieldInfo == (System.Reflection.FieldInfo) null || !typeof (PXAction).IsAssignableFrom(fieldInfo.FieldType))
      throw new PXException("{0} has not been found.", new object[1]
      {
        (object) "GraphAction"
      });
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    PXGraph instance1 = PXGraph.CreateInstance(type);
    PXAction action = fieldInfo.GetValue((object) instance1) as PXAction;
    BqlCommand instance2 = BqlCommand.CreateInstance(typeof (PX.Data.Select<>), action.GetRowType());
    PXSelectDelegate handler = (PXSelectDelegate) (() => (IEnumerable) new object[0]);
    PXAdapter adapter = new PXAdapter(new PXView(instance1, true, instance2, (Delegate) handler));
    SpeedChecker.PressButton(action, adapter);
    int numberOfUsersOnline = this._userManagementService.GetNumberOfUsersOnline();
    stopwatch.Stop();
    row.Interval = new int?((int) stopwatch.ElapsedMilliseconds);
    row.UsersCount = new int?(numberOfUsersOnline);
    this.Check.Update(row);
    return a.Get();
  }

  private static void PressButton(PXAction action, PXAdapter adapter)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    IEnumerator enumerator = adapter != null ? action.Press(adapter).GetEnumerator() : throw new ArgumentNullException(nameof (adapter));
    do
      ;
    while (enumerator.MoveNext());
  }
}
