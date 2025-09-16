// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.FullTextIndexRebuildProc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.SM;

public class FullTextIndexRebuildProc : PXGraph<
#nullable disable
FullTextIndexRebuildProc>
{
  public static void BuildIndex(
    FullTextIndexRebuildProc graph,
    FullTextIndexRebuildProc.RecordType item)
  {
    graph.BuildIndex(item);
  }

  public virtual void BuildIndex(FullTextIndexRebuildProc.RecordType item)
  {
    Stopwatch stopwatch = new Stopwatch();
    ((Dictionary<Type, PXCache>) ((PXGraph) this).Caches).Clear();
    ((PXGraph) this).Clear((PXClearOption) 3);
    PXProcessing<FullTextIndexRebuildProc.RecordType>.SetCurrentItem((object) item);
    Type type = GraphHelper.GetType(item.Entity);
    PXCache cach = ((PXGraph) this).Caches[type];
    PXSearchableAttribute searchableAttribute = cach.GetAttributes("NoteID").OfType<PXSearchableAttribute>().FirstOrDefault<PXSearchableAttribute>();
    if (searchableAttribute == null)
      return;
    PXView pxView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(new Type[1]
    {
      FullTextIndexRebuildProc.ComposeViewToSelectRecordsForIndexing(searchableAttribute, type)
    }));
    List<Type> typeList = new List<Type>((IEnumerable<Type>) searchableAttribute.GetSearchableFields(cach));
    for (Type c = type; typeof (IBqlTable).IsAssignableFrom(c); c = c.BaseType)
    {
      Type nestedType = c.GetNestedType("noteID");
      if ((Type) null != nestedType)
        typeList.Add(nestedType);
    }
    typeList.Add(typeof (SearchIndex.noteID));
    typeList.Add(typeof (SearchIndex.category));
    typeList.Add(typeof (SearchIndex.content));
    typeList.Add(typeof (SearchIndex.entityType));
    typeList.Add(typeof (Note.noteID));
    typeList.Add(typeof (Note.noteText));
    stopwatch.Start();
    int num1 = 0;
    List<object> objectList;
    do
    {
      using (new PXFieldScope(pxView, (IEnumerable<Type>) typeList, true))
        objectList = pxView.SelectWindowed((object[]) null, (object[]) null, (string[]) null, (bool[]) null, num1, 50000);
      stopwatch.Stop();
      stopwatch.Reset();
      stopwatch.Start();
      num1 += 50000;
      int count = objectList.Count;
      int num2 = 0;
      int num3 = 0;
      try
      {
        Dictionary<Guid, SearchIndex> dictionary1 = new Dictionary<Guid, SearchIndex>(objectList.Count);
        foreach (PXResult pxResult in objectList)
        {
          ++num2;
          if (searchableAttribute.IsSearchable(cach, pxResult[type]))
          {
            ++num3;
            Note note = (Note) pxResult[typeof (Note)];
            SearchIndex searchIndex1 = searchableAttribute.BuildSearchIndex(cach, pxResult[type], pxResult, FullTextIndexRebuildProc.ExtractNoteText(note));
            SearchIndex searchIndex2 = (SearchIndex) pxResult[typeof (SearchIndex)];
            if (searchIndex2.NoteID.HasValue)
            {
              Guid? noteId1 = searchIndex2.NoteID;
              Guid? noteId2 = searchIndex1.NoteID;
              if ((noteId1.HasValue == noteId2.HasValue ? (noteId1.HasValue ? (noteId1.GetValueOrDefault() != noteId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
                PXSearchableAttribute.Delete(searchIndex1);
            }
            if (!searchIndex2.NoteID.HasValue)
            {
              Dictionary<Guid, SearchIndex> dictionary2 = dictionary1;
              Guid? noteId = searchIndex1.NoteID;
              Guid key1 = noteId.Value;
              if (!dictionary2.ContainsKey(key1))
              {
                Dictionary<Guid, SearchIndex> dictionary3 = dictionary1;
                noteId = searchIndex1.NoteID;
                Guid key2 = noteId.Value;
                SearchIndex searchIndex3 = searchIndex1;
                dictionary3.Add(key2, searchIndex3);
              }
            }
            else
            {
              if (!(searchIndex1.Content != searchIndex2.Content))
              {
                int? category1 = searchIndex1.Category;
                int? category2 = searchIndex2.Category;
                if (category1.GetValueOrDefault() == category2.GetValueOrDefault() & category1.HasValue == category2.HasValue && !(searchIndex1.EntityType != searchIndex2.EntityType))
                  continue;
              }
              PXSearchableAttribute.Update(searchIndex1);
            }
          }
        }
        stopwatch.Stop();
        stopwatch.Reset();
        stopwatch.Start();
        PXSearchableAttribute.BulkInsert((IEnumerable<SearchIndex>) dictionary1.Values);
        stopwatch.Stop();
      }
      catch (Exception ex)
      {
        throw new Exception($"The system processed {num2} out of {count}. {num3} are searchable. Error: {ex.Message}.", ex);
      }
    }
    while (objectList.Count > 0);
    PXProcessing<FullTextIndexRebuildProc.RecordType>.SetProcessed();
  }

  private static Type ComposeViewToSelectRecordsForIndexing(
    PXSearchableAttribute searchableAttribute,
    Type entity)
  {
    Type type1 = typeof (LeftJoin<Note, On<Note.noteID, Equal<SearchIndex.noteID>>>);
    Type recordsForIndexing;
    if (searchableAttribute.SelectForFastIndexing != (Type) null)
    {
      Type type2 = entity;
      if (searchableAttribute.SelectForFastIndexing.IsGenericType)
      {
        Type[] genericArguments = searchableAttribute.SelectForFastIndexing.GetGenericArguments();
        if (genericArguments != null && genericArguments.Length != 0 && typeof (IBqlTable).IsAssignableFrom(genericArguments[0]))
          type2 = genericArguments[0];
      }
      Type type3 = BqlCommand.Compose(new Type[6]
      {
        typeof (LeftJoin<,>),
        typeof (SearchIndex),
        typeof (On<,>),
        typeof (SearchIndex.noteID),
        typeof (Equal<>),
        type2.GetNestedType("noteID")
      });
      recordsForIndexing = BqlCommand.AppendJoin(BqlCommand.AppendJoin(searchableAttribute.SelectForFastIndexing, type3), type1);
    }
    else
    {
      Type type4 = BqlCommand.Compose(new Type[6]
      {
        typeof (LeftJoin<,>),
        typeof (SearchIndex),
        typeof (On<,>),
        typeof (SearchIndex.noteID),
        typeof (Equal<>),
        entity.GetNestedType("noteID")
      });
      recordsForIndexing = BqlCommand.AppendJoin(BqlCommand.AppendJoin(BqlCommand.Compose(new Type[2]
      {
        typeof (Select<>),
        entity
      }), type4), type1);
    }
    return recordsForIndexing;
  }

  private static string ExtractNoteText(Note note)
  {
    string noteText = note.NoteText;
    if (string.IsNullOrWhiteSpace(noteText))
      return (string) null;
    string[] strArray = noteText.Split(PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar);
    if (strArray.Length < 1)
      return (string) null;
    return string.IsNullOrWhiteSpace(strArray[0]) ? (string) null : strArray[0];
  }

  [Serializable]
  public class RecordType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected string _Entity;
    protected string _Name;
    protected string _DisplayName;

    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXString(250, IsKey = true)]
    [PXUIField(DisplayName = "Entity", Enabled = false)]
    public virtual string Entity
    {
      get => this._Entity;
      set => this._Entity = value;
    }

    [PXString(250)]
    [PXUIField(DisplayName = "Entity", Enabled = false)]
    public virtual string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXString(250)]
    [PXUIField(DisplayName = "Name", Enabled = false)]
    public virtual string DisplayName
    {
      get => this._DisplayName;
      set => this._DisplayName = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      FullTextIndexRebuildProc.RecordType.selected>
    {
    }

    public abstract class entity : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FullTextIndexRebuildProc.RecordType.entity>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FullTextIndexRebuildProc.RecordType.name>
    {
    }

    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FullTextIndexRebuildProc.RecordType.displayName>
    {
    }
  }
}
