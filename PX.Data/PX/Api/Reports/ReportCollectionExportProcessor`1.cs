// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportCollectionExportProcessor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Reports;

internal class ReportCollectionExportProcessor<T> : CommandProcessor<PX.Api.Models.Field> where T : class, new()
{
  private readonly IEnumerable<T> _collection;
  private readonly string _collectionName;
  private readonly ICollection<Content> _resultContent;
  private readonly Content _schema;

  public ReportCollectionExportProcessor(
    IEnumerable<T> collection,
    string collectionName,
    ICollection<Content> resultContent,
    Content schema)
  {
    this._schema = schema;
    this._resultContent = resultContent;
    this._collectionName = collectionName;
    this._collection = collection;
  }

  public override bool CanExecute(Command cmd)
  {
    return base.CanExecute(cmd) && cmd.Name == null && cmd.ObjectName == this._collectionName;
  }

  public override void Execute(PX.Api.Models.Field cmd)
  {
    Content[] array = this._resultContent.Where<Content>((Func<Content, bool>) (c => ((IEnumerable<Container>) c.Containers).Any<Container>((Func<Container, bool>) (cnt => cnt != null && cnt.Name == this._collectionName)))).ToArray<Content>();
    int index = 0;
    foreach (T obj in this._collection)
    {
      Content newContent;
      if (index < array.Length)
      {
        newContent = array[index];
      }
      else
      {
        newContent = ReportCollectionExportProcessor<T>.CreateNewContent();
        this._resultContent.Add(newContent);
      }
      ((IEnumerable<PX.Api.Models.Field>) (((IEnumerable<Container>) newContent.Containers).FirstOrDefault<Container>((Func<Container, bool>) (c => c != null && c.Name == cmd.ObjectName)) ?? ReportCollectionExportProcessor<T>.CreateNewContainer(newContent, cmd.ObjectName, this._schema)).Fields).First<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (f => f.FieldName == cmd.FieldName)).Value = typeof (T).GetProperty(cmd.FieldName).GetValue((object) obj, (object[]) null).ToString();
      ++index;
    }
  }

  public static Content CreateNewContent()
  {
    return new Content()
    {
      Containers = new Container[0],
      Actions = new PX.Api.Models.Action[0]
    };
  }

  public static Container CreateNewContainer(
    Content itemContent,
    string containerName,
    Content schema)
  {
    int originalCOntainerIndex = -1;
    Container container = ((IEnumerable<Container>) schema.Containers).First<Container>((Func<Container, bool>) (c =>
    {
      ++originalCOntainerIndex;
      return c != null && c.Name == containerName;
    }));
    Container newContainer = new Container()
    {
      Name = container.Name,
      Fields = ((IEnumerable<PX.Api.Models.Field>) container.Fields).Select<PX.Api.Models.Field, PX.Api.Models.Field>((Func<PX.Api.Models.Field, PX.Api.Models.Field>) (f =>
      {
        return new PX.Api.Models.Field()
        {
          Commit = f.Commit,
          Descriptor = f.Descriptor,
          FieldName = f.FieldName,
          IgnoreError = f.IgnoreError,
          LinkedCommand = f.LinkedCommand,
          Name = f.Name,
          ObjectName = f.ObjectName,
          Value = f.Value
        };
      })).ToArray<PX.Api.Models.Field>(),
      ViewDescription = container.ViewDescription,
      DisplayName = container.DisplayName,
      ServiceCommands = container.ServiceCommands
    };
    if (itemContent.Containers == null || itemContent.Containers.Length == 0)
      itemContent.Containers = new Container[schema.Containers.Length];
    itemContent.Containers[originalCOntainerIndex] = newContainer;
    return newContainer;
  }
}
