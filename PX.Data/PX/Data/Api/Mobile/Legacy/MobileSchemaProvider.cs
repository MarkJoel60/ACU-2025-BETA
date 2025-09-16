// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.Legacy.MobileSchemaProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Mobile.Legacy;
using PX.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Mobile.Legacy;

internal static class MobileSchemaProvider
{
  public static Screen ConvertFromScreenInfo(string screenId)
  {
    Content withServiceCommands = ScreenUtils.GetScreenInfoWithServiceCommands(true, true, screenId, true);
    if (withServiceCommands == null)
      return (Screen) null;
    Screen screen1 = new Screen();
    ((ScreenBase) screen1).Id = screenId;
    ((ScreenBase) screen1).Name = screenId;
    ((ScreenBase) screen1).Type = (ScreenType) 0;
    Screen screen2 = screen1;
    List<ScreenContainer> screenContainerList = new List<ScreenContainer>();
    Dictionary<string, ScreenContainerAction[]> dictionary = ((IEnumerable<PX.Api.Models.Action>) withServiceCommands.Actions).GroupBy<PX.Api.Models.Action, string, ScreenContainerAction>((Func<PX.Api.Models.Action, string>) (a => a.ObjectName), (Func<PX.Api.Models.Action, ScreenContainerAction>) (a => MobileSchemaProvider.ConvertAction(a))).ToDictionary<IGrouping<string, ScreenContainerAction>, string, ScreenContainerAction[]>((Func<IGrouping<string, ScreenContainerAction>, string>) (x => x.Key), (Func<IGrouping<string, ScreenContainerAction>, ScreenContainerAction[]>) (x => x.ToArray<ScreenContainerAction>()));
    foreach (Container container in withServiceCommands.Containers)
    {
      ScreenContainer screenContainer = new ScreenContainer()
      {
        Name = container.Name
      };
      screenContainer.Items = (ContainerElement[]) ((IEnumerable<PX.Api.Models.Field>) container.Fields).Select<PX.Api.Models.Field, ScreenContainerField>((Func<PX.Api.Models.Field, ScreenContainerField>) (f => MobileSchemaProvider.ConvertField(f))).ToArray<ScreenContainerField>();
      if (dictionary.ContainsKey(container.CleanViewName()))
        screenContainer.Actions = dictionary[container.CleanViewName()];
      screenContainerList.Add(screenContainer);
    }
    ((ScreenBase) screen2).Containers = screenContainerList.ToArray();
    return screen2;
  }

  private static ScreenContainerField ConvertField(PX.Api.Models.Field f)
  {
    ScreenContainerField screenContainerField = new ScreenContainerField();
    screenContainerField.DisplayName = f.Descriptor?.DisplayName ?? f.Name;
    ((ContainerElement) screenContainerField).Name = f.Name;
    return screenContainerField;
  }

  private static ScreenContainerAction ConvertAction(PX.Api.Models.Action a)
  {
    ScreenContainerActionExtended containerActionExtended = new ScreenContainerActionExtended();
    containerActionExtended.Name = a.Name;
    containerActionExtended.DisplayName = a.Descriptor.DisplayName;
    containerActionExtended.Context = (ActionContext) 0;
    containerActionExtended.ViewName = a.ViewTypeName;
    return (ScreenContainerAction) containerActionExtended;
  }
}
