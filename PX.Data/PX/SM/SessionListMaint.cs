// Decompiled with JetBrains decompiler
// Type: PX.SM.SessionListMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.SM;

public class SessionListMaint : PXGraph<
#nullable disable
SessionListMaint>
{
  public PXFilter<SessionListMaint.RowFilter> Filter;
  public PXSelect<SessionListMaint.RowListItem> List;
  private static System.Reflection.FieldInfo[] SFields = SessionListMaint.EnumStaticFields().ToArray<System.Reflection.FieldInfo>();
  public PXSelectOrderBy<SessionListMaint.RowTypeInfo, OrderBy<Desc<SessionListMaint.RowTypeInfo.size>>> Details;
  public PXAction<SessionListMaint.RowFilter> ActionGC;
  public PXAction<SessionListMaint.RowFilter> ActionStaticVars;

  public SessionListMaint()
  {
    this.List.Cache.AllowInsert = false;
    this.List.Cache.AllowUpdate = false;
    this.List.Cache.AllowDelete = false;
    this.Details.Cache.AllowInsert = false;
    this.Details.Cache.AllowUpdate = false;
    this.Details.Cache.AllowDelete = false;
  }

  protected IEnumerable list()
  {
    this.List.Cache.Clear();
    this.AddSessionItems();
    return this.List.Cache.Cached;
  }

  private void AddSessionItems()
  {
    EnumerableExtensions.ForEach<MemProfilerResult>((IEnumerable<MemProfilerResult>) PXSessionStateStore.GetSessionsMemoryInfo(), (System.Action<MemProfilerResult>) (_ => this.List.Cache.SetStatus((object) new SessionListMaint.RowListItem()
    {
      Source = "User Session",
      User = _.User,
      ID = _.ID,
      Created = _.DateCreated,
      Size = new long?(_.TotalSize),
      Details = _.Details
    }, PXEntryStatus.Held)));
  }

  private static IEnumerable<System.Reflection.FieldInfo> EnumStaticFields()
  {
    System.Collections.Generic.List<System.Reflection.FieldInfo> fieldInfoList = new System.Collections.Generic.List<System.Reflection.FieldInfo>();
    foreach (Assembly assembly in ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).ToList<Assembly>())
    {
      try
      {
        System.Type[] typeArray = (System.Type[]) null;
        try
        {
          if (!assembly.IsDynamic)
            typeArray = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
          typeArray = ex.Types;
        }
        if (typeArray != null)
        {
          foreach (System.Type type in typeArray)
          {
            if (!(type == (System.Type) null))
            {
              if (!type.IsGenericTypeDefinition)
              {
                try
                {
                  foreach (System.Reflection.FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    fieldInfoList.Add(field);
                }
                catch
                {
                }
              }
            }
          }
        }
      }
      catch
      {
      }
    }
    return (IEnumerable<System.Reflection.FieldInfo>) fieldInfoList;
  }

  private void AddStaticVars()
  {
    foreach (System.Reflection.FieldInfo sfield in SessionListMaint.SFields)
    {
      object obj = (object) null;
      try
      {
        obj = sfield.GetValue((object) null);
      }
      catch
      {
      }
      if (obj != null)
      {
        MemProfilerResult size = PXReflectionSerializer.GetSize(obj);
        if (size.TotalSize >= 10000L)
          this.List.Cache.SetStatus((object) new SessionListMaint.RowListItem()
          {
            Source = "Static Field",
            ID = sfield.ToString(),
            Size = new long?(size.TotalSize),
            Details = size.Details
          }, PXEntryStatus.Held);
      }
    }
  }

  protected IEnumerable details()
  {
    SessionListMaint.RowListItem current = this.List.Current;
    return current == null ? (IEnumerable) new SessionListMaint.RowTypeInfo[0] : (IEnumerable) ((IEnumerable<MemProfilerTypeInfo>) current.Details).Select<MemProfilerTypeInfo, SessionListMaint.RowTypeInfo>((Func<MemProfilerTypeInfo, SessionListMaint.RowTypeInfo>) (_ => new SessionListMaint.RowTypeInfo()
    {
      TypeName = _.Type,
      Count = new int?(_.Count),
      Size = new long?(_.Size),
      RetainedSize = new long?(_.RetainedSize)
    })).ToArray<SessionListMaint.RowTypeInfo>();
  }

  [PXButton]
  [PXUIField(DisplayName = "Collect Memory")]
  protected void actionGC() => GCHelper.ForcedCollect(false);

  [PXButton]
  [PXUIField(DisplayName = "Static Vars")]
  protected void actionStaticVars()
  {
    long num = 0;
    try
    {
      foreach (Assembly assembly in ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).ToList<Assembly>())
      {
        try
        {
          System.Type[] typeArray = (System.Type[]) null;
          try
          {
            if (!assembly.IsDynamic)
              typeArray = assembly.GetTypes();
          }
          catch (ReflectionTypeLoadException ex)
          {
            typeArray = ex.Types;
          }
          if (typeArray != null)
          {
            foreach (System.Type type in typeArray)
            {
              try
              {
                foreach (System.Reflection.FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                  object obj = field.GetValue((object) null);
                  if (obj != null)
                  {
                    MemProfilerResult size = PXReflectionSerializer.GetSize(obj);
                    num += size.TotalSize;
                  }
                }
              }
              catch
              {
              }
            }
          }
        }
        catch
        {
        }
      }
    }
    catch (Exception ex)
    {
    }
    throw new Exception("Size: " + num.ToString());
  }

  [Serializable]
  public class RowFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "Managed Memory, Mb", Enabled = false)]
    [PXLong]
    public long? GCTotalMemory => new long?(GC.GetTotalMemory(false) / 1000000L);

    [PXUIField(DisplayName = "GC Collections", Enabled = false)]
    [PXString]
    public string GCCollection
    {
      get => $"{GC.CollectionCount(0)}/{GC.CollectionCount(1)}/{GC.CollectionCount(2)}";
    }

    [PXUIField(DisplayName = "Working Set, Mb", Enabled = false)]
    [PXLong]
    public long? WorkingSet => new long?(Process.GetCurrentProcess().WorkingSet64 / 1000000L);
  }

  [Serializable]
  public class RowListItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    internal MemProfilerTypeInfo[] Details;

    [PXString]
    [PXUIField(DisplayName = "Source")]
    public string Source { get; set; }

    [PXString]
    [PXUIField(DisplayName = "User")]
    public string User { get; set; }

    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "ID")]
    public string ID { get; set; }

    [PXDateAndTime]
    [PXUIField(DisplayName = "Created")]
    public System.DateTime? Created { get; set; }

    [PXLong]
    [PXUIField(DisplayName = "Size")]
    public long? Size { get; set; }
  }

  [Serializable]
  public class RowTypeInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "Type")]
    public string TypeName { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Count")]
    public int? Count { get; set; }

    [PXLong]
    [PXUIField(DisplayName = "Size")]
    public long? Size { get; set; }

    [PXLong]
    [PXUIField(DisplayName = "Retained Size")]
    public long? RetainedSize { get; set; }

    public abstract class typeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SessionListMaint.RowTypeInfo.typeName>
    {
    }

    public abstract class count : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SessionListMaint.RowTypeInfo.count>
    {
    }

    public abstract class size : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SessionListMaint.RowTypeInfo.size>
    {
    }

    public abstract class retainedSize : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SessionListMaint.RowTypeInfo.retainedSize>
    {
    }
  }
}
