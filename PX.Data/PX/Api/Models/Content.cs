// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Content
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile.Exceptions;
using PX.Api.Mobile.Legacy;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Models;

public class Content
{
  public Action[] Actions;
  public Container[] Containers;

  internal ContainerSearchStrategy ContainerSearchStrategy { private get; init; }

  internal string PrimaryView { get; init; }

  public Container GetContainer(string name, bool throwExceptionIfNotFound = true)
  {
    IEnumerable<Container> containers = this.GetContainers(name, throwExceptionIfNotFound);
    return containers == null ? (Container) null : containers.FirstOrDefault<Container>();
  }

  public IEnumerable<Container> GetContainers(string name, bool throwExceptionIfNotFound = true)
  {
    IEnumerable<Container> containers = (IEnumerable<Container>) null;
    if (this.Containers.Length != 0 && !Str.IsNullOrEmpty(name))
      containers = this.ContainerSearchStrategy == ContainerSearchStrategy.ContainerNameFirst ? ((IEnumerable<Container>) this.Containers).Where<Container>((Func<Container, bool>) (c => c.Name.OrdinalEquals(name) || c.CleanViewName().OrdinalEquals(name))) : ((IEnumerable<Container>) this.Containers).Where<Container>((Func<Container, bool>) (c => c.CleanViewName().OrdinalEquals(name) || c.Name.OrdinalEquals(name)));
    return !EnumerableExtensions.IsNullOrEmpty<Container>(containers) || !throwExceptionIfNotFound ? containers : throw new PXContainerNotFoundException(name);
  }

  public Action GetAction(string name)
  {
    Action[] actions = this.Actions;
    return actions == null ? (Action) null : ((IEnumerable<Action>) actions).FirstOrDefault<Action>((Func<Action, bool>) (a => a.Name.OrdinalEquals(name)));
  }

  public bool IsPrimaryContainer(ScreenType type, string containerName)
  {
    Container container = this.GetContainer(containerName, false);
    if (container == null)
      return false;
    if (type != 1)
      return this.GetPrimaryContainer().Name.OrdinalEquals(container.Name);
    return this.Containers.Length >= 2 && this.Containers[1].Name.OrdinalEquals(container.Name);
  }

  public Container FindContainerByViewName(string viewName, bool throwIfNotFound = false)
  {
    Container container = (Container) null;
    if (!EnumerableExtensions.IsNullOrEmpty<Container>((IEnumerable<Container>) this.Containers))
      container = ((IEnumerable<Container>) this.Containers).FirstOrDefault<Container>((Func<Container, bool>) (c => c.ViewName().OrdinalEquals(viewName)));
    return container != null || !throwIfNotFound ? container : throw new PXContainerNotFoundException(viewName);
  }

  public bool IsFirstContainer(string containerName)
  {
    return this.Containers[0].Name.OrdinalEquals(this.GetContainer(containerName).Name);
  }

  public bool IsFilterContainer(ScreenType type, string containerName)
  {
    return type == 1 && this.IsFirstContainer(containerName);
  }

  public Container GetContainer(Field field) => this.FindContainerByViewName(field.ObjectName);

  public Dictionary<string, object> GetForms()
  {
    if (this.Containers == null || this.Containers.Length < 2)
      return new Dictionary<string, object>(0);
    Dictionary<string, object> dictionary = ((IEnumerable<Container>) this.Containers).Skip<Container>(1).Where<Container>((Func<Container, bool>) (_ => !_.IsEmptyDescription() && !_.IsGrid())).Select<Container, string>((Func<Container, string>) (_ => _.CleanViewName())).Distinct<string>().ToDictionary<string, string, object>((Func<string, string>) (_ => _), (Func<string, object>) (_ => (object) null));
    Container container = this.FirstContainer();
    if (!container.IsEmptyDescription())
      dictionary.Remove(container.CleanViewName());
    return dictionary;
  }

  internal Container GetPrimaryContainer()
  {
    return !Str.IsNullOrEmpty(this.PrimaryView) ? this.FindContainerByViewName(this.PrimaryView) ?? this.FirstContainer() : this.FirstContainer();
  }

  private Container FirstContainer()
  {
    return this.Containers != null && this.Containers.Length != 0 ? this.Containers[0] : throw new PXContainerNotFoundException(this.PrimaryView);
  }
}
