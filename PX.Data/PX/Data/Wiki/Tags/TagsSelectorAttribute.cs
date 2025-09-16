// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.TagsSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Xml;

#nullable enable
namespace PX.Data.Wiki.Tags;

public class TagsSelectorAttribute : 
  PXCustomSelectorAttribute,
  IPXRowUpdatedSubscriber,
  IPXCommandPreparingSubscriber,
  IPXRowSelectingSubscriber,
  IPXRowPersistingSubscriber
{
  protected readonly System.Type _FileIDType;
  protected readonly System.Type? _TagIDsType;
  protected string? _DatabaseFieldName;
  protected readonly BqlCommand _SingleSelect;
  protected readonly BqlCommand? _SubSelect;
  protected readonly IBqlWhere _PureWhere;
  protected readonly System.Type _Field;
  public const char Separator = ';';
  public static readonly char[] SeparatorArray = new char[1]
  {
    ';'
  };

  public virtual string DatabaseFieldName
  {
    get => this._DatabaseFieldName ?? this._FieldName;
    set => this._DatabaseFieldName = value;
  }

  public bool IsActive { get; set; }

  public bool AllowTagsCreation { get; set; }

  public TagsSelectorAttribute(System.Type fileIDType)
    : this(fileIDType, (System.Type) null)
  {
  }

  public TagsSelectorAttribute(System.Type fileIDType, System.Type? tagIDsType)
    : base(typeof (Tag.tagCD))
  {
    this._FileIDType = !(fileIDType == (System.Type) null) ? fileIDType : throw new PXArgumentException(nameof (fileIDType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
    {
      (object) fileIDType
    });
    this._TagIDsType = tagIDsType;
    this.ValidateValue = false;
    this._Field = typeof (Tag.tagID);
    this._SingleSelect = BqlCommand.CreateInstance(BqlCommand.Compose(typeof (Search<,>), typeof (UploadFileTag.tagID), typeof (Where<,>), typeof (UploadFileTag.fileID), typeof (Equal<>), fileIDType));
    this._PureWhere = ((IHasBqlWhere) this._SingleSelect).GetWhere();
    this._SingleSelect = this._SingleSelect.WhereAnd(BqlCommand.Compose(typeof (Where<,>), this._Field, typeof (Equal<>), typeof (Required<>), this._Field));
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXCache cach = sender.Graph.Caches[typeof (UploadFileTag)];
    sender.Graph.Views.Caches.Add(cach.GetItemType());
  }

  public static string[] Split(string? value)
  {
    return value == null ? Array.Empty<string>() : ((IEnumerable<string>) value.Split(TagsSelectorAttribute.SeparatorArray, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (t => t.Trim())).ToArray<string>();
  }

  public static string Combine(string? value1, string? value2)
  {
    if (string.IsNullOrEmpty(value1))
      return value2 ?? string.Empty;
    return string.IsNullOrEmpty(value2) ? value1 ?? string.Empty : $"{value1}{(ValueType) ';'}{value2}";
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null || this._FileIDType == (System.Type) null || !this.IsActive)
      return;
    string str1 = sender.GetValue(e.OldRow, this._FieldOrdinal) as string;
    string str2 = sender.GetValue(e.Row, this._FieldOrdinal) as string;
    if (!(str1 != str2) || sender.Graph.IsCopyPasteContext)
      return;
    string[] strArray1 = TagsSelectorAttribute.Split(str1);
    string[] strArray2 = TagsSelectorAttribute.Split(str2);
    HashSet<string> stringSet1 = new HashSet<string>((IEnumerable<string>) strArray2);
    stringSet1.ExceptWith((IEnumerable<string>) strArray1);
    HashSet<string> stringSet2 = new HashSet<string>((IEnumerable<string>) strArray1);
    stringSet2.ExceptWith((IEnumerable<string>) strArray2);
    PXCache<UploadFileTag> pxCache = sender.Graph.Caches<UploadFileTag>();
    Guid? nullable = sender.GetValue(e.Row, this._FileIDType.Name) as Guid?;
    foreach (string tagCD in stringSet1)
    {
      Tag tag = Tag.UK.Find(sender.Graph, tagCD);
      if (tag != null)
        pxCache.Insert(new UploadFileTag()
        {
          TagID = tag.TagID,
          FileID = nullable
        });
    }
    foreach (string tagCD in stringSet2)
    {
      Tag tag = Tag.UK.Find(sender.Graph, tagCD);
      if (tag != null)
      {
        UploadFileTag data = UploadFileTag.PK.Find(sender.Graph, nullable.Value, tag.TagID.Value);
        if (data != null)
          pxCache.Delete(data);
      }
    }
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation.Command() != PXDBOperation.Delete)
      return;
    PXDatabase.Delete<UploadFileTag>((PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileTag.fileID>(PXDbType.UniqueIdentifier, sender.GetValue(e.Row, this._FileIDType.Name)));
  }

  public void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (!e.IsSelect())
      return;
    PXDBOperation pxdbOperation = e.Operation & PXDBOperation.Option;
    int num;
    switch (pxdbOperation)
    {
      case PXDBOperation.Select:
      case PXDBOperation.Internal:
      case PXDBOperation.External:
        num = 1;
        break;
      case PXDBOperation.GroupBy:
        num = e.Value == null ? 1 : 0;
        break;
      default:
        num = 0;
        break;
    }
    bool flag = num != 0;
    System.Type type = pxdbOperation == PXDBOperation.External ? sender.GetItemType() : e.Table ?? this._BqlTable;
    e.DataType = PXDbType.Xml;
    if (!this.IsActive)
    {
      e.DataType = PXDbType.NVarChar;
      e.DataLength = new int?(4);
      e.Expr = SQLExpression.Null();
    }
    else
    {
      if (!this._BqlTable.IsAssignableFrom(sender.BqlTable))
      {
        if (sender.Graph.Caches[this._BqlTable].BqlSelect != null & flag)
        {
          e.BqlTable = this._BqlTable;
          e.Expr = (SQLExpression) new Column(this.DatabaseFieldName, (e.Operation & PXDBOperation.Option) == PXDBOperation.External ? sender.GetItemType() : this._BqlTable);
        }
        else
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.Graph.Caches[this._BqlTable].RaiseCommandPreparing(this.DatabaseFieldName, e.Row, e.Value, e.Operation, e.Table, out description);
          if (description != null)
          {
            e.DataType = description.DataType;
            e.DataValue = description.DataValue;
            e.BqlTable = this._BqlTable;
            e.Expr = description.Expr;
          }
        }
      }
      else
      {
        ISqlDialect sqlDialect = e.SqlDialect;
        if (flag && (e.Operation & PXDBOperation.Option) != PXDBOperation.External)
        {
          List<System.Type> types = new List<System.Type>()
          {
            type
          };
          e.BqlTable = type;
          e.DataType = PXDbType.NVarChar;
          e.Expr = (SQLExpression) new SubQuery(this.GetTagsJoinedQuery(sender.Graph, types, ((IBqlSearch) this._SingleSelect).GetField(), this._PureWhere));
          return;
        }
        e.Expr = SQLExpression.Null();
      }
      e.DataType = PXDbType.UniqueIdentifier;
      e.DataLength = new int?(16 /*0x10*/);
    }
  }

  protected Query GetTagsJoinedQuery(
    PXGraph graph,
    List<System.Type> types,
    System.Type fieldWithValue,
    IBqlWhere _PureWhere)
  {
    System.Type itemType = BqlCommand.GetItemType(fieldWithValue);
    System.Type table = BqlCommand.FindRealTableForType(types, itemType);
    SQLExpression exp1 = (SQLExpression) null;
    _PureWhere.AppendExpression(ref exp1, graph, new BqlCommandInfo(false)
    {
      Tables = types
    }, (BqlCommand.Selection) null);
    List<System.Type> list = types.ToList<System.Type>();
    bool realTables = types.All<System.Type>((Func<System.Type, bool>) (t => graph.Caches[t].BqlSelect == null));
    list.Add(realTables ? table : itemType);
    SQLExpression exp2 = exp1;
    TableChangingScope.AppendRestrictionsOnIsNew(ref exp2, graph, list, new BqlCommand.Selection(), realTables);
    int num = exp2 != exp1 ? 1 : 0;
    Func<Table> SQLTableGetter = (Func<Table>) (() => (Table) new SimpleTable(table.Name));
    Table srcTable;
    if (num != 0)
    {
      TableChangingScope.AddUnchangedRealName(table.Name);
      srcTable = TableChangingScope.GetSQLTable(SQLTableGetter, table.Name);
    }
    else
      srcTable = SQLTableGetter();
    return (Query) new JoinedAttrQuery(srcTable, fieldWithValue.Name, "TagID", "TagID", exp2);
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    string str1 = e.Record.GetString(e.Position);
    HashSet<Guid> source = new HashSet<Guid>();
    if (str1 != null)
    {
      using (XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(string.Format("<{0}>{1}</{0}>", (object) "ROOT", (object) str1)), new XmlReaderSettings()))
      {
        while (xmlReader.Read())
        {
          switch (xmlReader.NodeType)
          {
            case XmlNodeType.Text:
              Guid result;
              if (xmlReader.Value != null && Guid.TryParse(xmlReader.Value, out result))
              {
                source.Add(result);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    IPrincipal authUser = PXContext.PXIdentity.AuthUser;
    Dictionary<Guid, string> dictionary = TagsSlot.TagByID();
    Dictionary<Guid, short> tagsAccessForUser = TagsSlot.GetTagsAccessForUser(authUser);
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Guid key in source)
    {
      string str2;
      if (TagsSlot.CanViewTag(new Guid?(key), (IDictionary<Guid, short>) tagsAccessForUser) && dictionary.TryGetValue(key, out str2))
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append("; ");
        stringBuilder.Append(str2);
      }
    }
    string str3 = stringBuilder.ToString();
    sender.SetValue(e.Row, this._FieldOrdinal, (object) str3);
    if (this._TagIDsType != (System.Type) null)
      sender.SetValue(e.Row, this._TagIDsType.Name, (object) source.ToArray<Guid>());
    ++e.Position;
  }

  public IEnumerable GetRecords()
  {
    TagsSelectorAttribute selectorAttribute = this;
    Dictionary<Guid, short> tagAccess = TagsSlot.GetTagsAccessForUser(PXContext.PXIdentity.AuthUser);
    foreach (PXResult<Tag> pxResult in PXSelectBase<Tag, PXSelect<Tag>.Config>.Select(selectorAttribute._Graph))
    {
      Tag record = (Tag) pxResult;
      if (TagsSlot.HasAccessToTag(record.TagID, (short) 3, (IDictionary<Guid, short>) tagAccess))
        yield return (object) record;
    }
  }
}
