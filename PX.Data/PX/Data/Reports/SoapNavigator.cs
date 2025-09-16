// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.SoapNavigator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Common;
using PX.Common.Parser;
using PX.Data.Localizers;
using PX.Data.SQLTree;
using PX.Reports;
using PX.Reports.Data;
using PX.Reports.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web.Compilation;
using System.Xml;

#nullable disable
namespace PX.Data.Reports;

[Serializable]
public sealed class SoapNavigator : IDataNavigator, ICloneable, ISiblingNavigator
{
  private List<object> _records;
  private Dictionary<object, IDataNavigator> _children;
  private int _index;
  private readonly ReportSelectArguments _arguments = new ReportSelectArguments();
  private Dictionary<string, int> _fieldMap;
  public const string ReportParametersOrganization = "organizationid";
  [NonSerialized]
  private Dictionary<string, KeyValuePair<PXCache, int>> _cacheMap;
  [NonCompared]
  private Dictionary<string, KeyValuePair<System.Type, int>> _cacheMapSerialized;
  [NonSerialized]
  private KeyValuePair<PXCache, int> _default;
  private KeyValuePair<System.Type, int>? _defaultSerialized;
  private bool _defaultFound;
  private Dictionary<string, string> _Formats;
  private Dictionary<object, string> _Masks;
  private SoapNavigator.DATA _Data;
  private PXGraph _Graph;
  private Func<string, System.Type> _Tables;
  private IPXResultset _Incoming;
  private List<KeyValuePair<string, IPXResultset>> _Siblings;
  [NonSerialized]
  private PXCache[] caches;
  [NonCompared]
  private System.Type[] cachesSerialized;
  [NonSerialized]
  private static readonly char[] delimiterChars = new char[14]
  {
    ' ',
    ',',
    '.',
    ':',
    '\t',
    '[',
    ']',
    '(',
    ')',
    '=',
    '+',
    '-',
    '*',
    '/'
  };
  public Dictionary<string, SoapNavigator.DependentParameters> reportParams;
  private string currentlyProcessingParam;
  private Dictionary<string, PXFieldState[]> _Schemas;

  internal bool SelectData { get; set; } = true;

  internal bool SecurityTrimming { get; private set; }

  public void Clear()
  {
    this._records = (List<object>) null;
    this._children = (Dictionary<object, IDataNavigator>) null;
    this._fieldMap = (Dictionary<string, int>) null;
    this._Tables = (Func<string, System.Type>) (s =>
    {
      ReportSelectArguments selectArguments = this.SelectArguments;
      return ServiceManager.ReportNameResolver.ResolveTable(s, selectArguments.Tables);
    });
    this._Masks = new Dictionary<object, string>();
    this._Formats = new Dictionary<string, string>();
    this._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
    this._Data.ClearKeys();
  }

  public object Clone()
  {
    SoapNavigator soapNavigator = new SoapNavigator();
    soapNavigator._arguments.Load(this._arguments);
    soapNavigator._children = this._children != null ? this._children.ToDictionary<KeyValuePair<object, IDataNavigator>, object, IDataNavigator>((Func<KeyValuePair<object, IDataNavigator>, object>) (i => i.Key), (Func<KeyValuePair<object, IDataNavigator>, IDataNavigator>) (i => (IDataNavigator) ((ICloneable) i.Value).Clone())) : (Dictionary<object, IDataNavigator>) null;
    soapNavigator._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
    soapNavigator._default = this._default;
    soapNavigator._defaultFound = this._defaultFound;
    soapNavigator._defaultSerialized = this._defaultSerialized;
    soapNavigator._fieldMap = this._fieldMap != null ? new Dictionary<string, int>((IDictionary<string, int>) this._fieldMap) : (Dictionary<string, int>) null;
    soapNavigator._Formats = this._Formats != null ? new Dictionary<string, string>((IDictionary<string, string>) this._Formats) : (Dictionary<string, string>) null;
    soapNavigator._Graph = this._Graph;
    soapNavigator._Incoming = this._Incoming;
    soapNavigator._Siblings = this._Siblings;
    soapNavigator._index = this._index;
    soapNavigator._Masks = this._Masks != null ? new Dictionary<object, string>((IDictionary<object, string>) this._Masks) : (Dictionary<object, string>) null;
    soapNavigator._records = this._records != null ? new List<object>((IEnumerable<object>) this._records) : (List<object>) null;
    soapNavigator._Schemas = this._Schemas != null ? new Dictionary<string, PXFieldState[]>((IDictionary<string, PXFieldState[]>) this._Schemas) : (Dictionary<string, PXFieldState[]>) null;
    soapNavigator._Tables = this._Tables;
    soapNavigator.SecurityTrimming = this.SecurityTrimming;
    return (object) soapNavigator;
  }

  internal void AddSibling(string name, IPXResultset resultSet)
  {
    if (this._Siblings == null)
      this._Siblings = new List<KeyValuePair<string, IPXResultset>>();
    this._Siblings.Add(new KeyValuePair<string, IPXResultset>(name, resultSet));
  }

  public void SwitchSibling(string name)
  {
    if (this._Siblings != null)
    {
      foreach (KeyValuePair<string, IPXResultset> sibling in this._Siblings)
      {
        if (StringComparer.InvariantCultureIgnoreCase.Compare(sibling.Key, name) == 0)
        {
          this._Incoming = sibling.Value;
          return;
        }
      }
    }
    this._Incoming = (IPXResultset) null;
  }

  public int FieldCount => this._fieldMap != null ? this._fieldMap.Count : int.MaxValue;

  internal IPXResultset Incoming => this._Incoming;

  private void addRecord(KeyValuePair<object, IDataNavigator> pair)
  {
    if (pair.Key == null)
      return;
    this._records.Add(pair.Key);
    if (pair.Value == null)
      return;
    this._children[pair.Key] = pair.Value;
  }

  private void addRecord(object item) => this._records.Add(item);

  private SoapNavigator()
  {
  }

  private SoapNavigator(
    PXGraph graph,
    Func<string, System.Type> tables,
    Dictionary<object, string> masks,
    Dictionary<string, int> fieldmap)
  {
    this._Graph = graph;
    this._Tables = tables;
    this._Masks = masks;
    this._fieldMap = fieldmap;
    this._records = new List<object>();
    this._children = new Dictionary<object, IDataNavigator>();
    this._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
  }

  private SoapNavigator(
    PXGraph graph,
    Func<string, System.Type> tables,
    Dictionary<object, string> masks,
    Dictionary<string, KeyValuePair<PXCache, int>> cachemap)
  {
    this._Graph = graph;
    this._Tables = tables;
    this._Masks = masks;
    if (cachemap.Count > 1)
      this._cacheMap = cachemap;
    foreach (KeyValuePair<PXCache, int> keyValuePair in this._cacheMap.Values)
    {
      if (keyValuePair.Value == 0)
      {
        this._default = keyValuePair;
        this._defaultFound = true;
        break;
      }
    }
    this._records = new List<object>();
    this._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
  }

  private SoapNavigator(
    PXGraph graph,
    Func<string, System.Type> tables,
    Dictionary<object, string> masks,
    KeyValuePair<PXCache, int> def)
  {
    this._Graph = graph;
    this._Tables = tables;
    this._Masks = masks;
    this._default = def;
    this._defaultFound = true;
    this._records = new List<object>();
    this._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
  }

  public SoapNavigator(PXGraph graph)
    : this(graph, (IPXResultset) null)
  {
  }

  public SoapNavigator(PXGraph graph, IPXResultset incoming)
  {
    this._Graph = graph;
    this._Incoming = incoming;
    this._Graph.Defaults[typeof (AccessInfo)] = (PXGraph.GetDefaultDelegate) (() => (object) this._Graph.Accessinfo);
    this._Tables = (Func<string, System.Type>) (s =>
    {
      ReportSelectArguments selectArguments = this.SelectArguments;
      return ServiceManager.ReportNameResolver.ResolveTable(s, selectArguments.Tables);
    });
    this._Masks = new Dictionary<object, string>();
    this._Formats = new Dictionary<string, string>();
    this._Data = new SoapNavigator.DATA(this._Graph, this._Tables, new SoapNavigator.DATA._GetSourceDelegate(this._GetSource), new SoapNavigator.DATA._GetCurrentDelegate(this._GetCurrent));
  }

  internal SoapNavigator(PXGraph graph, IPXResultset incoming, bool securityTrimming)
    : this(graph, incoming)
  {
    this.SecurityTrimming = securityTrimming;
  }

  public SoapNavigator(PXGraph graph, List<KeyValuePair<string, IPXResultset>> siblings)
    : this(graph, siblings.Count > 0 ? siblings[0].Value : (IPXResultset) null)
  {
    this._Siblings = siblings;
  }

  public static PXReportResultset FromXml(string xml)
  {
    PXReportResultset pxReportResultset = (PXReportResultset) null;
    if (xml != null)
    {
      List<System.Type> typeList = new List<System.Type>();
      using (TextReader input = (TextReader) new StringReader(xml))
      {
        using (XmlReader xmlReader = XmlReader.Create(input))
        {
          if (xmlReader.ReadToDescendant("Resultset"))
          {
            if (xmlReader.ReadToDescendant("Result"))
            {
              if (xmlReader.ReadToDescendant("Row"))
              {
                do
                {
                  System.Type type = PXBuildManager.GetType(xmlReader.GetAttribute("type"), false);
                  if (type != (System.Type) null)
                    typeList.Add(type);
                }
                while (xmlReader.ReadToNextSibling("Row"));
              }
            }
          }
        }
      }
      if (typeList.Count > 0)
      {
        using (TextReader input = (TextReader) new StringReader(xml))
        {
          using (XmlReader xmlReader = XmlReader.Create(input))
          {
            PXGraph pxGraph = new PXGraph();
            List<PXCache> pxCacheList = new List<PXCache>();
            foreach (System.Type key in typeList)
              pxCacheList.Add(pxGraph.Caches[key]);
            pxReportResultset = new PXReportResultset(typeList.ToArray());
            xmlReader.ReadToDescendant("Resultset");
            xmlReader.ReadToDescendant("Result");
            do
            {
              if (xmlReader.ReadToDescendant("Row"))
              {
                List<object> objectList = new List<object>();
                do
                {
                  objectList.Add(pxCacheList[objectList.Count].FromXml(xmlReader.ReadOuterXml()));
                }
                while (xmlReader.Name == "Row");
                pxReportResultset.Add(objectList.ToArray());
              }
            }
            while (xmlReader.ReadToNextSibling("Result"));
          }
        }
      }
    }
    return pxReportResultset;
  }

  public static string ToXml(SoapNavigator navigator)
  {
    navigator.ensureinit();
    List<object> objectList = new List<object>();
    objectList.Add((object) navigator);
    bool flag = true;
    while (flag)
    {
      flag = false;
      int index1 = 0;
      while (index1 < objectList.Count)
      {
        if (objectList[index1] is SoapNavigator)
        {
          SoapNavigator soapNavigator = (SoapNavigator) objectList[index1];
          objectList.RemoveAt(index1);
          if (soapNavigator._records.Count > 0)
          {
            for (int index2 = 0; index2 < soapNavigator._records.Count; ++index2)
            {
              if (soapNavigator._records[index2] != null)
              {
                SoapNavigator childNavigator = (SoapNavigator) soapNavigator.GetChildNavigator(soapNavigator._records[index2]);
                if (childNavigator != null)
                {
                  objectList.Insert(index1, (object) childNavigator);
                  flag = true;
                }
                else
                  objectList.Insert(index1, soapNavigator._records[index2]);
                ++index1;
              }
            }
          }
        }
      }
    }
    if (objectList.Count <= 0)
      return (string) null;
    StringBuilder sb = new StringBuilder();
    using (XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) new StringWriter(sb)))
    {
      xmlTextWriter.Formatting = Formatting.Indented;
      xmlTextWriter.Indentation = 2;
      xmlTextWriter.WriteStartElement("Resultset");
      for (int index3 = 0; index3 < objectList.Count; ++index3)
      {
        xmlTextWriter.WriteStartElement("Result");
        xmlTextWriter.WriteAttributeString("index", index3.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        for (int index4 = 0; index4 < navigator.caches.Length; ++index4)
          xmlTextWriter.WriteRaw(navigator.caches[index4].ToXml(objectList[index3] is object[] ? ((object[]) objectList[index3])[index4] : objectList[index3]));
        xmlTextWriter.WriteEndElement();
      }
      xmlTextWriter.WriteEndElement();
    }
    return sb.ToString();
  }

  [PXInternalUseOnly]
  public PXGraph GetGraph() => this._Graph;

  private void ensureinit()
  {
    if (this._records != null || ((List<ReportTable>) this.SelectArguments.Tables).Count <= 0)
      return;
    this.init();
  }

  private ReportParameter findParameter(object val)
  {
    return val is string str && str.Length > 1 && str.StartsWith("@") ? this.SelectArguments.Parameters[str.Substring(1)] : (ReportParameter) null;
  }

  private object checkParameter(object val)
  {
    if (!(val is string str) || str.Length <= 1 || !str.StartsWith("@"))
      return val;
    ReportParameter parameter = this.SelectArguments.Parameters[str.Substring(1)];
    return parameter == null ? (object) null : PXDatabase.Provider.GetDirectExpressionForReportParameter(parameter.Type, parameter.Value);
  }

  private bool isParameter(string name)
  {
    return name == null || name.Length < 3 || name[0] != '[' || name[name.Length - 1] != ']';
  }

  private void prepareValue(PXCache cache, string name, ref object val)
  {
    this.prepareValue(cache, name, ref val, out int? _);
  }

  private bool prepareValue(PXCache cache, string name, ref object val, out int? length)
  {
    length = new int?();
    PXDBOperation operation = PXDBOperation.Select;
    if (name.EndsWith("_Attributes") || cache.IsKvExtAttribute(name))
      operation |= PXDBOperation.External;
    PXCommandPreparingEventArgs.FieldDescription description;
    cache.RaiseCommandPreparing(name, (object) null, val, operation, (System.Type) null, out description);
    if (description?.Expr == null && (operation & PXDBOperation.External) != PXDBOperation.External)
      cache.RaiseCommandPreparing(name, (object) null, val, operation | PXDBOperation.External, (System.Type) null, out description);
    if (description == null || description.Expr == null)
      return false;
    if (description.DataValue != null)
    {
      val = description.DataValue;
      length = description.DataLength;
      return true;
    }
    if (description.Expr.Oper() == SQLExpression.Operation.NULL)
    {
      cache.RaiseCommandPreparing(name, (object) null, val, PXDBOperation.External, (System.Type) null, out description);
      if (description != null && description.Expr != null && description.Expr is SubQuery)
      {
        val = description.DataValue;
        length = description.DataLength;
      }
      else
        val = (object) null;
    }
    else
      val = (object) null;
    return true;
  }

  /// <summary>true, if expr is formula, otherwise false</summary>
  public static bool IsFormula(string expr)
  {
    return expr.StartsWith("=") || expr.Contains("[") || expr.Contains("]") || expr.Contains("*") || expr.Contains("/") || expr.Contains("+") || expr.Contains("-") || expr.Contains(",");
  }

  private void init()
  {
    PXGraph graph = new PXGraph()
    {
      StringTable = new StringTable()
    };
    graph.UnattendedMode = false;
    graph.Defaults[typeof (AccessInfo)] = (PXGraph.GetDefaultDelegate) (() => (object) graph.Accessinfo);
    graph._ForceUnattended = !this.SecurityTrimming;
    BqlSoapCommand bqlSoapCommand = new BqlSoapCommand(graph, this.SelectArguments);
    List<PXDataValue> pars = new List<PXDataValue>();
    this.fillCaches(bqlSoapCommand, graph);
    if (this._Incoming == null || this._Incoming.GetTableCount() == 1 && this._Incoming.GetRowCount() == 1 && this.caches != null && this.caches.Length > 1)
    {
      if (this.caches.Length > 1)
      {
        if (this._Incoming != null)
        {
          bqlSoapCommand.IncomingRow = this._Incoming.GetItem(0, 0);
          bqlSoapCommand.IncomingRowType = this._Incoming.GetItemType(0);
        }
        for (int index = 0; index < ((CollectionBase) this.SelectArguments.Relations).Count; ++index)
        {
          foreach (RelationRow link in (List<RelationRow>) this.SelectArguments.Relations[index].Links)
          {
            if (!string.IsNullOrEmpty(link.ParentField))
            {
              foreach (object val1 in ((IEnumerable<string>) link.ParentField.Split(SoapNavigator.delimiterChars)).Where<string>((Func<string, bool>) (el => el.StartsWith("@"))))
              {
                object val2 = this.checkParameter(val1);
                if (!link.ChildField.Contains("@"))
                {
                  string pname = this.fixPName(link.ChildField, this.SelectArguments.Relations[index].ChildName);
                  val2 = this.findInCacheAndUpdateValue(bqlSoapCommand, graph, pname, val2);
                }
                pars.Add(new PXDataValue(PXDbType.DirectExpression, val2));
              }
            }
            if (!string.IsNullOrEmpty(link.ChildField))
            {
              foreach (object val3 in ((IEnumerable<string>) link.ChildField.Split(SoapNavigator.delimiterChars)).Where<string>((Func<string, bool>) (el => el.StartsWith("@"))))
              {
                object val4 = this.checkParameter(val3);
                if (!link.ParentField.Contains("@"))
                {
                  string pname = this.fixPName(link.ParentField, this.SelectArguments.Relations[index].ParentName);
                  val4 = this.findInCacheAndUpdateValue(bqlSoapCommand, graph, pname, val4);
                }
                pars.Add(new PXDataValue(PXDbType.DirectExpression, val4));
              }
            }
          }
        }
      }
      this.addFilterArguments(bqlSoapCommand, graph, pars);
      this.addGroupingArguments(pars);
      this.ensureSorting(bqlSoapCommand, this.SelectArguments.Sorting, graph);
      int num = this.SelectArguments.DeletedRecords != "O" || this.setOnlyDeleted(graph, bqlSoapCommand, pars) ? (this.SelectArguments.ArchivedRecords != "O" ? 1 : (this.setOnlyArchived(graph, bqlSoapCommand, pars) ? 1 : 0)) : 0;
      List<object> objectList = new List<object>();
      if (num != 0)
      {
        if (this.SelectData)
        {
          try
          {
            PXDatabase.ReadDeleted = this.SelectArguments.DeletedRecords == "P" || this.SelectArguments.DeletedRecords == "O";
            PXDatabase.ReadThroughArchived = this.SelectArguments.ArchivedRecords == "P" || this.SelectArguments.ArchivedRecords == "O";
            this._EnsureBranches(this.SelectArguments);
            this._EnsureSpecificTable(graph, bqlSoapCommand);
            if (this._cacheMap != null)
              bqlSoapCommand.IndexReportFields(this._cacheMap.ToDictionary<KeyValuePair<string, KeyValuePair<PXCache, int>>, string, PXCache>((Func<KeyValuePair<string, KeyValuePair<PXCache, int>>, string>) (_ => _.Key), (Func<KeyValuePair<string, KeyValuePair<PXCache, int>>, PXCache>) (_ => _.Value.Key)));
            Async.SetStatus("Executing SQL Query");
            Query query = bqlSoapCommand.GetQuery(graph, (long) this.SelectArguments.TopCount);
            int to = PXDatabase.GetReportQueryTimeout();
            foreach (PXDataRecord record in PXDatabase.Provider.Select(query, (IEnumerable<PXDataValue>) pars.ToArray(), (System.Action<PXDatabaseProvider.ExecutionParameters>) (c =>
            {
              if (to > 0)
                c.CommandTimeout = to;
              else
                c.CommandTimeout *= 10;
              c.TraceQuery = true;
            })))
            {
              if (objectList.Count % 1000 == 0)
                Async.SetStatus($"{objectList.Count} Rows Processed");
              int position = 0;
              object[] objArray = new object[this.caches.Length];
              int index = -1;
              foreach (BqlSoapCommand.RowSelectingInfo selectingHandler1 in bqlSoapCommand.RowSelectingHandlers)
              {
                ++index;
                object keyNode = selectingHandler1.KeyNode;
                if (selectingHandler1.KeysCount == 0)
                {
                  objArray[index] = keyNode;
                  if (objectList.Count > 0)
                  {
                    position = selectingHandler1.LastPos >= 0 ? selectingHandler1.LastPos : throw new PXException("info.LastPos < 0*");
                    continue;
                  }
                }
                PXRowSelectingEventArgs e = new PXRowSelectingEventArgs(keyNode, record, position, false);
                foreach (BqlSoapCommand.RowSelectingFieldInfo field in selectingHandler1.Fields)
                {
                  if (objectList.Count == 0)
                    field.PosBefore = e.Position;
                  foreach (IPXRowSelectingSubscriber selectingHandler2 in field.RowSelectingHandlers)
                    selectingHandler2.RowSelecting(selectingHandler1.Cache, e);
                  if (objectList.Count == 0)
                    field.PosAfter = e.Position;
                  if (field.IsLastKey)
                  {
                    object obj;
                    if (selectingHandler1.RowIndex.TryGetValue(keyNode, out obj))
                    {
                      objArray[index] = obj;
                      if (selectingHandler1.LastPos < 0)
                        throw new PXException("info.LastPos < 0*");
                    }
                    else
                    {
                      selectingHandler1.ResetKeyNode();
                      selectingHandler1.RowIndex.Add(keyNode, keyNode);
                      objArray[index] = keyNode;
                    }
                  }
                }
                position = e.Position;
                if (objectList.Count == 0)
                  selectingHandler1.LastPos = position;
              }
              objectList.Add(objArray.Length > 1 ? (object) objArray : objArray[0]);
            }
            foreach (BqlSoapCommand.RowSelectingInfo selectingHandler in bqlSoapCommand.RowSelectingHandlers)
            {
              Delegate[] invocationList = selectingHandler.Cache._EventsRow.RowSelecting?.GetInvocationList();
              if (invocationList != null && invocationList.Length != 0)
              {
                HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
                foreach (BqlSoapCommand.RowSelectingFieldInfo field in selectingHandler.Fields)
                  stringSet.Add(field.Field);
                foreach (PXRowSelecting pxRowSelecting in invocationList.OfType<PXRowSelecting>())
                {
                  if (pxRowSelecting.Target is PXEventSubscriberAttribute target)
                  {
                    bool flag = stringSet.Contains(target.FieldName);
                    if (!flag)
                    {
                      foreach (string str in PXDependsOnFieldsAttribute.GetDependsRecursive(selectingHandler.Cache, target.FieldName))
                      {
                        flag = stringSet.Contains(str);
                        if (flag)
                          break;
                      }
                    }
                    if (flag)
                    {
                      foreach (object row in selectingHandler.RowIndex.Values)
                      {
                        PXRowSelectingEventArgs e = new PXRowSelectingEventArgs(row, (PXDataRecord) null, 0, false);
                        pxRowSelecting(selectingHandler.Cache, e);
                      }
                    }
                  }
                }
              }
              selectingHandler.RowIndex.Clear();
            }
            graph.Clear();
          }
          finally
          {
            PXDatabase.ReadDeleted = false;
            PXDatabase.BranchIDs = (List<int>) null;
            PXDatabase.SpecificBranchTable = (string) null;
          }
        }
      }
      Async.SetStatus("Creating Groups");
      this._records = objectList;
      this._default = new KeyValuePair<PXCache, int>(this.caches[0], 0);
      this._defaultFound = true;
    }
    else
    {
      this._records = new List<object>();
      int tableCount = this._Incoming.GetTableCount();
      if (tableCount > 1)
      {
        for (int rowNbr = 0; rowNbr < this._Incoming.GetRowCount(); ++rowNbr)
        {
          object[] objArray = new object[tableCount];
          for (int i = 0; i < tableCount; ++i)
            objArray[i] = this._Incoming.GetItem(rowNbr, i);
          this._records.Add((object) objArray);
        }
      }
      else
      {
        for (int rowNbr = 0; rowNbr < this._Incoming.GetRowCount(); ++rowNbr)
          this._records.Add(this._Incoming.GetItem(rowNbr, 0));
      }
      this._default = new KeyValuePair<PXCache, int>(graph.Caches[this._Incoming.GetItemType(0)], 0);
      this._defaultFound = true;
    }
    if (this.SelectArguments.Groups.Count <= 0)
      return;
    SoapNavigator[] soapNavigatorArray = new SoapNavigator[this.SelectArguments.Groups.Count + 1];
    soapNavigatorArray[this.SelectArguments.Groups.Count] = this._cacheMap != null ? new SoapNavigator(this._Graph, this._Tables, this._Masks, this._cacheMap) : new SoapNavigator(this._Graph, this._Tables, this._Masks, this._default);
    Dictionary<string, int>[] dictionaryArray = new Dictionary<string, int>[this.SelectArguments.Groups.Count];
    for (int index1 = 0; index1 < this.SelectArguments.Groups.Count; ++index1)
    {
      GroupExpCollection group = this.SelectArguments.Groups[index1];
      dictionaryArray[index1] = new Dictionary<string, int>();
      for (int index2 = 0; index2 < ((List<GroupExp>) group).Count; ++index2)
        dictionaryArray[index1][((List<GroupExp>) group)[index2].DataField] = index2;
      soapNavigatorArray[index1] = new SoapNavigator(this._Graph, this._Tables, this._Masks, dictionaryArray[index1]);
      if (index1 == 0)
        soapNavigatorArray[index1]._Formats = this._Formats;
    }
    this.Reset();
    SoapNavigator.ComparedValue[][] comparedValueArray1 = new SoapNavigator.ComparedValue[this.SelectArguments.Groups.Count][];
    while (this.MoveNext())
    {
      for (int index3 = this.SelectArguments.Groups.Count - 1; index3 >= 0; --index3)
      {
        GroupExpCollection group = this.SelectArguments.Groups[index3];
        SoapNavigator.ComparedValue[] comparedValueArray2 = new SoapNavigator.ComparedValue[((List<GroupExp>) group).Count];
        bool flag = true;
        if (soapNavigatorArray[index3] == null)
          soapNavigatorArray[index3] = new SoapNavigator(this._Graph, this._Tables, this._Masks, dictionaryArray[index3]);
        for (int index4 = 0; index4 < ((List<GroupExp>) group).Count; ++index4)
        {
          string dataField = ((List<GroupExp>) group)[index4].DataField;
          if (SoapNavigator.IsFormula(dataField))
          {
            SoapNavigator.ComparedValue comparedValue = new SoapNavigator.ComparedValue(this, dataField, this._index);
            object obj = ReportExprParser.Eval(dataField, this._arguments.Relations.Report, (IDataNavigator) this, this._records[this._index]);
            comparedValue._Value = obj != null ? (object) obj.ToString() : (object) string.Empty;
            comparedValueArray2[index4] = comparedValue;
          }
          else
            comparedValueArray2[index4] = this.GetComparedValue(dataField, this.Current);
          if (flag && comparedValueArray1[index3] != null)
            flag = !SoapNavigator.IsFormula(dataField) ? object.Equals((object) comparedValueArray2[index4], (object) comparedValueArray1[index3][index4]) : object.Equals(comparedValueArray2[index4].GetValue(), comparedValueArray1[index3][index4].GetValue());
        }
        if (!flag)
        {
          for (int index5 = this.SelectArguments.Groups.Count - 1; index5 >= index3; --index5)
          {
            if (soapNavigatorArray[index5 + 1] != null)
            {
              object[] array = ((IEnumerable<SoapNavigator.ComparedValue>) comparedValueArray1[index5]).Select<SoapNavigator.ComparedValue, object>((Func<SoapNavigator.ComparedValue, object>) (_ => _.GetValueEx())).ToArray<object>();
              soapNavigatorArray[index5].addRecord(new KeyValuePair<object, IDataNavigator>((object) array, (IDataNavigator) soapNavigatorArray[index5 + 1]));
              soapNavigatorArray[index5 + 1] = (SoapNavigator) null;
            }
          }
        }
        comparedValueArray1[index3] = comparedValueArray2;
      }
      if (soapNavigatorArray[this.SelectArguments.Groups.Count] == null)
        soapNavigatorArray[this.SelectArguments.Groups.Count] = this._cacheMap != null ? new SoapNavigator(this._Graph, this._Tables, this._Masks, this._cacheMap) : new SoapNavigator(this._Graph, this._Tables, this._Masks, this._default);
      soapNavigatorArray[this.SelectArguments.Groups.Count].addRecord(this.Current);
    }
    for (int index = this.SelectArguments.Groups.Count - 1; index >= 0; --index)
    {
      if (soapNavigatorArray[index + 1] != null)
      {
        if (soapNavigatorArray[index] == null)
          soapNavigatorArray[index] = new SoapNavigator(this._Graph, this._Tables, this._Masks, dictionaryArray[index]);
        object[] key = (object[]) null;
        if (comparedValueArray1[index] != null)
          key = ((IEnumerable<SoapNavigator.ComparedValue>) comparedValueArray1[index]).Select<SoapNavigator.ComparedValue, object>((Func<SoapNavigator.ComparedValue, object>) (_ => _.GetValueEx())).ToArray<object>();
        soapNavigatorArray[index].addRecord(new KeyValuePair<object, IDataNavigator>((object) key, (IDataNavigator) soapNavigatorArray[index + 1]));
      }
    }
    this._defaultFound = false;
    this._records = soapNavigatorArray[0]._records;
    this._fieldMap = soapNavigatorArray[0]._fieldMap;
    this._children = soapNavigatorArray[0]._children;
  }

  private void addFilterArguments(BqlSoapCommand comm, PXGraph graph, List<PXDataValue> pars)
  {
    // ISSUE: unable to decompile the method.
  }

  private (bool IsTimeCondition, System.DateTime?[] DatesRange) GetTimeConditionInfo(
    FilterCondition condition,
    System.DateTime? businessDate)
  {
    switch (condition - 13)
    {
      case 0:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.TODAY, businessDate));
      case 1:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.OVERDUE, businessDate));
      case 2:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.TODAY_OVERDUE, businessDate));
      case 3:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.TOMMOROW, businessDate));
      case 4:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.THIS_WEEK, businessDate));
      case 5:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.NEXT_WEEK, businessDate));
      case 6:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.THIS_MONTH, businessDate));
      case 7:
        return (true, PXView.DateTimeFactory.GetDateRange(PXCondition.NEXT_MONTH, businessDate));
      default:
        return (false, (System.DateTime?[]) null);
    }
  }

  private void addGroupingArguments(List<PXDataValue> pars)
  {
    foreach (GroupExp groupExp in this.SelectArguments.Groups.SelectMany<GroupExpCollection, GroupExp>((Func<GroupExpCollection, IEnumerable<GroupExp>>) (col => (IEnumerable<GroupExp>) col)))
    {
      foreach (ReportParameter reportParameter in this.GetParametersFromFormula(groupExp.DataField))
      {
        object obj = this.checkParameter((object) ("@" + reportParameter.Name));
        pars.Add(new PXDataValue(PXDbType.DirectExpression, obj));
      }
    }
  }

  private SortExpCollection ensureSorting(
    BqlSoapCommand cmd,
    SortExpCollection sorts,
    PXGraph graph)
  {
    if (EnumerableExtensions.IsNullOrEmpty<SortExp>((IEnumerable<SortExp>) sorts) && graph != null && !graph.SqlDialect.ClusterdIndexSupported)
    {
      PXView.PXSearchColumn[] defaultSorts = this.GetDefaultSorts(cmd, graph);
      if (defaultSorts != null)
      {
        sorts = new SortExpCollection();
        EnumerableExtensions.ForEach<PXView.PXSearchColumn>(((IEnumerable<PXView.PXSearchColumn>) defaultSorts).Where<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (s => s.Description?.Expr is Column)), (System.Action<PXView.PXSearchColumn>) (s => ((List<SortExp>) sorts).Add(new SortExp($"{((Column) s.Description.Expr).Table().AliasOrName()}.{s.Column}", s.Descending ? (SortOrder) 1 : (SortOrder) 0))));
      }
    }
    return sorts;
  }

  private (string name, System.Type ttype) GetMainTable(BqlSoapCommand command)
  {
    if (((CollectionBase) this.SelectArguments.Relations).Count > 0)
    {
      string parentName = this.SelectArguments.Relations[0].ParentName;
      System.Type type;
      if (!command._tablemap.TryGetValue(parentName, out type))
        command.throwTable(parentName);
      return (parentName, type);
    }
    string name = ((List<ReportTable>) this.SelectArguments.Tables)[0].Name;
    System.Type type1;
    if (!command._tablemap.TryGetValue(name, out type1))
      command.throwTable(name);
    return (name, type1);
  }

  /// <summary>Generates sort columns.</summary>
  /// <returns>Sort columns</returns>
  private PXView.PXSearchColumn[] GetDefaultSorts(BqlSoapCommand command, PXGraph graph)
  {
    (string name, System.Type ttype) mainTable = this.GetMainTable(command);
    System.Type cacheType = mainTable.ttype;
    PXCache cach = graph.Caches[cacheType];
    System.Type[] tables = ((IEnumerable<PXCache>) this.caches).Select<PXCache, System.Type>((Func<PXCache, System.Type>) (c => c.BqlTable)).ToArray<System.Type>();
    string[] array = cach.Keys.ToArray();
    List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) new System.Type[array.Length]);
    PXView.PXSearchColumn[] source = new PXView.PXSearchColumn[array.Length];
    for (int index = 0; index < array.Length; ++index)
    {
      string str = array[index];
      PXCache cache = cach;
      string field;
      PXView.CorrectCacheAndField(graph, (BqlCommand) command, str, ref cache, out field);
      PXView.PXSearchColumn searchColumn = source[index] = new PXView.PXSearchColumn(str, false, (object) null);
      searchColumn.SelSort = typeList[index];
      try
      {
        PXView.RaiseCommandPreparingForSearchColumn(cache, searchColumn, 0, field, (object) null, (Func<System.Type>) (() => cacheType), (Func<System.Type[]>) (() => tables), out bool _);
      }
      catch (Exception ex)
      {
        PXTrace.Logger.Error<Exception>($"Exception while preparing sort column {Environment.NewLine}   {{0}}", ex);
      }
    }
    EnumerableExtensions.ForEach<PXView.PXSearchColumn>(((IEnumerable<PXView.PXSearchColumn>) source).Where<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (s => s.Description?.Expr != null)), (System.Action<PXView.PXSearchColumn>) (s => s.Description.Expr = (SQLExpression) new Column(s.Column, mainTable.name)));
    return source;
  }

  private IEnumerable<ReportParameter> GetParametersFromFormula(string formula)
  {
    if (!string.IsNullOrEmpty(formula) && formula.Contains("@"))
    {
      foreach (string str in ((IEnumerable<string>) formula.Split(SoapNavigator.delimiterChars)).Where<string>((Func<string, bool>) (el => el.StartsWith("@"))))
      {
        ReportParameter parameter = this.SelectArguments.Parameters[str.Substring(1)];
        if (parameter != null)
          yield return parameter;
      }
    }
  }

  private static string makeLikeClause(string fValue, FilterCondition condition)
  {
    string str1 = fValue;
    string str2;
    if (str1 != null)
    {
      string str3 = str1.TrimEnd();
      str2 = condition == 7 ? str3 + "%" : (condition == 8 ? "%" + str3 : $"%{str3}%");
    }
    else
      str2 = "%";
    return str2;
  }

  private void fillCaches(BqlSoapCommand comm, PXGraph graph)
  {
    this.caches = new PXCache[((CollectionBase) this.SelectArguments.Relations).Count + 1];
    if (this.caches.Length == 1)
    {
      string name = ((List<ReportTable>) this.SelectArguments.Tables)[0].Name;
      System.Type key;
      if (!comm._tablemap.TryGetValue(name, out key))
        comm.throwTable(name);
      this.caches[0] = graph.Caches[key];
    }
    else
    {
      this._cacheMap = new Dictionary<string, KeyValuePair<PXCache, int>>();
      ReportRelation relation = this._arguments.Relations[0];
      string parentName = relation.ParentName;
      System.Type key;
      if (!comm._tablemap.TryGetValue(parentName, out key))
        comm.throwTable(parentName);
      this.caches[0] = graph.Caches[key];
      this._cacheMap[string.IsNullOrEmpty(relation.ParentAlias) ? parentName : relation.ParentAlias] = new KeyValuePair<PXCache, int>(this.caches[0], 0);
      for (int index = 0; index < ((CollectionBase) this.SelectArguments.Relations).Count; ++index)
      {
        string childName = this.SelectArguments.Relations[index].ChildName;
        if (!comm._tablemap.TryGetValue(childName, out key))
          comm.throwTable(childName);
        this.caches[index + 1] = graph.Caches[key];
        this._cacheMap[string.IsNullOrEmpty(this.SelectArguments.Relations[index].ChildAlias) ? childName : this.SelectArguments.Relations[index].ChildAlias] = new KeyValuePair<PXCache, int>(this.caches[index + 1], index + 1);
      }
    }
  }

  private string fixPName(string pname, string tablePrefix)
  {
    if (pname.StartsWith("="))
      pname = pname.Substring(1);
    if (!pname.Contains("."))
      pname = $"{tablePrefix}.{pname}";
    return pname;
  }

  private object findInCacheAndUpdateValue(
    BqlSoapCommand comm,
    PXGraph graph,
    string pname,
    object val)
  {
    Tuple<System.Type, string, System.Type> bqlField = comm.tryFindBqlField(graph, ref pname);
    PXCache cach = bqlField == null ? (PXCache) null : graph.Caches[bqlField.Item1];
    if (cach == null)
      return val;
    try
    {
      cach.RaiseFieldUpdating(bqlField.Item2, (object) null, ref val);
    }
    catch
    {
      val = (object) null;
    }
    this.prepareValue(cach, bqlField.Item2, ref val);
    return val;
  }

  private SoapNavigator.ComparedValue GetComparedValue(string dataField, object row)
  {
    string[] strArray = dataField.Split('.');
    string key = strArray[0];
    string str = strArray[1];
    if (this._cacheMap == null)
      return new SoapNavigator.ComparedValue()
      {
        Row = row,
        FieldName = str,
        Cache = this.caches[0]
      };
    KeyValuePair<PXCache, int> cache = this._cacheMap[key];
    object[] objArray = (object[]) row;
    return new SoapNavigator.ComparedValue()
    {
      Row = objArray[cache.Value],
      FieldName = str,
      Cache = cache.Key
    };
  }

  private static (bool result, string databaseFieldName) IsReadDeletedFieldSupported(System.Type table)
  {
    string fieldName;
    return (PXDatabase.IsReadDeletedSupported(table, out fieldName), fieldName);
  }

  private static (bool result, string databaseFieldName) IsRecordStatusFieldSupported(System.Type table)
  {
    string fieldName;
    return (PXDatabase.Provider.IsRecordStatusSupported(table, out fieldName), fieldName);
  }

  private void appendCacheField(
    PXCache cache,
    string cacheFieldName,
    Func<System.Type, (bool result, string databaseFieldName)> isFieldSupported)
  {
    if (cache.Fields.Contains(cacheFieldName))
      return;
    if (cache.BqlSelect == null)
    {
      (bool, string) supported = isFieldSupported(cache.BqlTable);
      if (!supported.Item1)
        return;
      cache.Fields.Add(cacheFieldName);
      cache.CommandPreparingEvents[cacheFieldName.ToLower()] += (PXCommandPreparing) ((sender, e) =>
      {
        if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
          return;
        PXCommandPreparingEventArgs preparingEventArgs = e;
        SQLExpression sqlExpression;
        if ((e.Operation & PXDBOperation.Option) == PXDBOperation.GroupBy)
        {
          sqlExpression = SQLExpression.Null();
        }
        else
        {
          string name = supported.Item2;
          System.Type dac = e.Table;
          if ((object) dac == null)
            dac = cache.BqlTable;
          SimpleTable t = new SimpleTable(dac);
          sqlExpression = (SQLExpression) new Column(name, (Table) t);
        }
        preparingEventArgs.Expr = sqlExpression;
        e.DataType = PXDbType.Bit;
        e.DataLength = new int?(1);
      });
      cache.RowSelectingWhileReading += (PXRowSelecting) ((sender, e) => ++e.Position);
    }
    else
    {
      BqlCommand bqlSelect = cache.BqlSelect;
      cache.BqlSelect = (BqlCommand) null;
      foreach (PXCache cache1 in ((IEnumerable<System.Type>) bqlSelect.GetTables()).Select<System.Type, PXCache>((Func<System.Type, PXCache>) (table => cache.Graph.Caches[table])))
      {
        this.appendCacheField(cache1, cacheFieldName, isFieldSupported);
        if (cache1.Fields.Contains(cacheFieldName))
        {
          if (cache != cache1)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            cache1.RaiseCommandPreparing(cacheFieldName, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
            cache.Fields.Add(cacheFieldName);
            cache.CommandPreparingEvents[cacheFieldName.ToLower()] += (PXCommandPreparing) ((sender, e) =>
            {
              if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
                return;
              e.Expr = (e.Operation & PXDBOperation.Option) != PXDBOperation.GroupBy ? (e.Table == (System.Type) null ? description.Expr : (SQLExpression) new Column(((Column) description.Expr).Name, e.Table)) : SQLExpression.Null();
              e.DataType = PXDbType.Bit;
              e.DataLength = new int?(1);
            });
            cache.RowSelectingWhileReading += (PXRowSelecting) ((sender, e) => ++e.Position);
            break;
          }
          break;
        }
      }
      cache.BqlSelect = bqlSelect;
    }
  }

  private void appendDeletedField(PXCache cache, string deletedFieldName)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.appendCacheField(cache, deletedFieldName, SoapNavigator.\u003C\u003EO.\u003C0\u003E__IsReadDeletedFieldSupported ?? (SoapNavigator.\u003C\u003EO.\u003C0\u003E__IsReadDeletedFieldSupported = new Func<System.Type, (bool, string)>(SoapNavigator.IsReadDeletedFieldSupported)));
  }

  private bool setOnlyDeleted(PXGraph graph, BqlSoapCommand comm, List<PXDataValue> pars)
  {
    string str = "DeletedDatabaseRecord";
    if (((List<FilterExp>) this.SelectArguments.Filters).Count > 1)
    {
      ++((List<FilterExp>) this.SelectArguments.Filters)[0].OpenBraces;
      ++((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].CloseBraces;
      ((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].Operator = (FilterOperator) 0;
    }
    if (((CollectionBase) this.SelectArguments.Relations).Count > 0)
    {
      bool flag = false;
      ReportRelation relation1 = this.SelectArguments.Relations[0];
      System.Type key1 = comm._tablemap[relation1.ParentName];
      PXCache cach1 = graph.Caches[key1];
      this.appendDeletedField(cach1, str);
      if (cach1.Fields.Contains(str))
      {
        FilterExp filterExp;
        if (cach1.BqlSelect != null)
        {
          filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation1.ParentAlias) ? relation1.ParentAlias : relation1.ParentName)}.{str}", (FilterCondition) 0);
        }
        else
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          cach1.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
          filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation1.ParentAlias) ? relation1.ParentAlias : relation1.ParentName)}.{graph.SqlDialect.quoteDbIdentifier(((Column) description.Expr).Name)}", (FilterCondition) 0);
        }
        filterExp.Value = "1";
        filterExp.Operator = (FilterOperator) 1;
        filterExp.OpenBraces = 1;
        ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp);
        flag = true;
        pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
      }
      foreach (ReportRelation relation2 in (CollectionBase) this.SelectArguments.Relations)
      {
        System.Type key2 = comm._tablemap[relation2.ChildName];
        PXCache cach2 = graph.Caches[key2];
        this.appendDeletedField(cach2, str);
        if (cach2.Fields.Contains(str))
        {
          FilterExp filterExp;
          if (cach2.BqlSelect != null)
          {
            filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation2.ChildAlias) ? relation2.ChildAlias : relation2.ChildName)}.{str}", (FilterCondition) 0);
          }
          else
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            cach2.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
            filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation2.ChildAlias) ? relation2.ChildAlias : relation2.ChildName)}.{graph.SqlDialect.quoteDbIdentifier(((Column) description.Expr).Name)}", (FilterCondition) 0);
          }
          filterExp.Value = "1";
          filterExp.Operator = (FilterOperator) 1;
          ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp);
          if (!flag)
          {
            filterExp.OpenBraces = 1;
            flag = true;
          }
          pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
        }
      }
      if (flag)
        ((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].CloseBraces = 1;
      return flag;
    }
    System.Type key = comm._tablemap[((List<ReportTable>) this.SelectArguments.Tables)[0].Name];
    PXCache cach = graph.Caches[key];
    this.appendDeletedField(cach, str);
    if (!cach.Fields.Contains(str))
      return false;
    FilterExp filterExp1;
    if (cach.BqlSelect != null)
    {
      filterExp1 = new FilterExp($"{((List<ReportTable>) this.SelectArguments.Tables)[0].Name}.{str}", (FilterCondition) 0);
    }
    else
    {
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
      filterExp1 = new FilterExp($"{((List<ReportTable>) this.SelectArguments.Tables)[0].Name}.{graph.SqlDialect.quoteDbIdentifier((description.Expr as Column).Name)}", (FilterCondition) 0);
    }
    filterExp1.Value = "1";
    ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp1);
    pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
    return true;
  }

  private void appendDatabaseRecordStatusField(PXCache cache, string recordStatusFieldName)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.appendCacheField(cache, recordStatusFieldName, SoapNavigator.\u003C\u003EO.\u003C1\u003E__IsRecordStatusFieldSupported ?? (SoapNavigator.\u003C\u003EO.\u003C1\u003E__IsRecordStatusFieldSupported = new Func<System.Type, (bool, string)>(SoapNavigator.IsRecordStatusFieldSupported)));
  }

  private bool setOnlyArchived(PXGraph graph, BqlSoapCommand comm, List<PXDataValue> pars)
  {
    string str = "DatabaseRecordStatus";
    if (((List<FilterExp>) this.SelectArguments.Filters).Count > 1)
    {
      ++((List<FilterExp>) this.SelectArguments.Filters)[0].OpenBraces;
      ++((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].CloseBraces;
      ((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].Operator = (FilterOperator) 0;
    }
    if (((CollectionBase) this.SelectArguments.Relations).Count > 0)
    {
      bool flag = false;
      ReportRelation relation1 = this.SelectArguments.Relations[0];
      System.Type key1 = comm._tablemap[relation1.ParentName];
      PXCache cach1 = graph.Caches[key1];
      this.appendDatabaseRecordStatusField(cach1, str);
      if (cach1.Fields.Contains(str))
      {
        FilterExp filterExp;
        if (cach1.BqlSelect != null)
        {
          filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation1.ParentAlias) ? relation1.ParentAlias : relation1.ParentName)}.{str}", (FilterCondition) 0);
        }
        else
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          cach1.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
          filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation1.ParentAlias) ? relation1.ParentAlias : relation1.ParentName)}.{graph.SqlDialect.quoteDbIdentifier(((Column) description.Expr).Name)}", (FilterCondition) 0);
        }
        filterExp.Value = "1";
        filterExp.Operator = (FilterOperator) 1;
        filterExp.OpenBraces = 1;
        ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp);
        flag = true;
        pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
      }
      foreach (ReportRelation relation2 in (CollectionBase) this.SelectArguments.Relations)
      {
        System.Type key2 = comm._tablemap[relation2.ChildName];
        PXCache cach2 = graph.Caches[key2];
        this.appendDatabaseRecordStatusField(cach2, str);
        if (cach2.Fields.Contains(str))
        {
          FilterExp filterExp;
          if (cach2.BqlSelect != null)
          {
            filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation2.ChildAlias) ? relation2.ChildAlias : relation2.ChildName)}.{str}", (FilterCondition) 0);
          }
          else
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            cach2.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
            filterExp = new FilterExp($"{(!string.IsNullOrEmpty(relation2.ChildAlias) ? relation2.ChildAlias : relation2.ChildName)}.{graph.SqlDialect.quoteDbIdentifier(((Column) description.Expr).Name)}", (FilterCondition) 0);
          }
          filterExp.Value = "1";
          filterExp.Operator = (FilterOperator) 1;
          ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp);
          if (!flag)
          {
            filterExp.OpenBraces = 1;
            flag = true;
          }
          pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
        }
      }
      if (flag)
        ((List<FilterExp>) this.SelectArguments.Filters)[((List<FilterExp>) this.SelectArguments.Filters).Count - 1].CloseBraces = 1;
      return flag;
    }
    System.Type key = comm._tablemap[((List<ReportTable>) this.SelectArguments.Tables)[0].Name];
    PXCache cach = graph.Caches[key];
    this.appendDatabaseRecordStatusField(cach, str);
    if (!cach.Fields.Contains(str))
      return false;
    FilterExp filterExp1;
    if (cach.BqlSelect != null)
    {
      filterExp1 = new FilterExp($"{((List<ReportTable>) this.SelectArguments.Tables)[0].Name}.{str}", (FilterCondition) 0);
    }
    else
    {
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(str, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
      filterExp1 = new FilterExp($"{((List<ReportTable>) this.SelectArguments.Tables)[0].Name}.{graph.SqlDialect.quoteDbIdentifier((description.Expr as Column).Name)}", (FilterCondition) 0);
    }
    filterExp1.Value = "1";
    ((List<FilterExp>) this.SelectArguments.Filters).Add(filterExp1);
    pars.Add(new PXDataValue(PXDbType.DirectExpression, new int?(1), (object) 1));
    return true;
  }

  [OnSerializing]
  internal void OnSerializingMethod(StreamingContext context)
  {
    if (this._cacheMap != null)
    {
      this._cacheMapSerialized = new Dictionary<string, KeyValuePair<System.Type, int>>();
      foreach (KeyValuePair<string, KeyValuePair<PXCache, int>> cache in this._cacheMap)
      {
        Dictionary<string, KeyValuePair<System.Type, int>> cacheMapSerialized = this._cacheMapSerialized;
        string key = cache.Key;
        KeyValuePair<PXCache, int> keyValuePair1 = cache.Value;
        System.Type itemType = keyValuePair1.Key.GetItemType();
        keyValuePair1 = cache.Value;
        int num = keyValuePair1.Value;
        KeyValuePair<System.Type, int> keyValuePair2 = new KeyValuePair<System.Type, int>(itemType, num);
        cacheMapSerialized.Add(key, keyValuePair2);
      }
    }
    if (this.caches != null)
      this.cachesSerialized = ((IEnumerable<PXCache>) this.caches).Select<PXCache, System.Type>((Func<PXCache, System.Type>) (cache => cache.GetItemType())).ToArray<System.Type>();
    if (!this._defaultFound)
      return;
    this._defaultSerialized = new KeyValuePair<System.Type, int>?(new KeyValuePair<System.Type, int>(this._default.Key.GetItemType(), this._default.Value));
  }

  [OnDeserialized]
  internal void OnDeserializedMethod(StreamingContext context)
  {
    if (this._cacheMapSerialized != null)
    {
      this._cacheMapSerialized.OnDeserialization((object) null);
      this._cacheMap = new Dictionary<string, KeyValuePair<PXCache, int>>();
      foreach (KeyValuePair<string, KeyValuePair<System.Type, int>> keyValuePair1 in this._cacheMapSerialized)
      {
        Dictionary<string, KeyValuePair<PXCache, int>> cacheMap = this._cacheMap;
        string key1 = keyValuePair1.Key;
        PXCacheCollection caches = this._Graph.Caches;
        KeyValuePair<System.Type, int> keyValuePair2 = keyValuePair1.Value;
        System.Type key2 = keyValuePair2.Key;
        PXCache key3 = caches[key2];
        keyValuePair2 = keyValuePair1.Value;
        int num = keyValuePair2.Value;
        KeyValuePair<PXCache, int> keyValuePair3 = new KeyValuePair<PXCache, int>(key3, num);
        cacheMap.Add(key1, keyValuePair3);
      }
      this._cacheMapSerialized = (Dictionary<string, KeyValuePair<System.Type, int>>) null;
    }
    if (this.cachesSerialized != null)
    {
      this.caches = ((IEnumerable<System.Type>) this.cachesSerialized).Select<System.Type, PXCache>((Func<System.Type, PXCache>) (type => this._Graph.Caches[type])).ToArray<PXCache>();
      this.cachesSerialized = (System.Type[]) null;
    }
    if (!this._defaultSerialized.HasValue)
      return;
    this._default = new KeyValuePair<PXCache, int>(this._Graph.Caches[this._defaultSerialized.Value.Key], this._defaultSerialized.Value.Value);
  }

  /// <summary>Gets the child rows navigator for specified record.</summary>
  public IDataNavigator GetChildNavigator(object record)
  {
    if (record == null)
      return (IDataNavigator) new SoapNavigator(this._Graph);
    if (this._children == null)
      return (IDataNavigator) null;
    IDataNavigator idataNavigator;
    return !this._children.TryGetValue(record, out idataNavigator) ? (IDataNavigator) null : idataNavigator;
  }

  /// <summary>Returns a list of the data rows.</summary>
  public IList GetList()
  {
    this.ensureinit();
    return (IList) this._records;
  }

  /// <summary>Clear data navigator</summary>
  public void Refresh()
  {
    if (this._Incoming != null && (this._Incoming.GetTableCount() != 1 || this._Incoming.GetRowCount() != 1 || this.caches == null || this.caches.Length <= 1))
      return;
    this.Clear();
  }

  /// <summary>Sets the navigator to its initial position.</summary>
  public void Reset()
  {
    this.ensureinit();
    this._index = -1;
  }

  /// <summary>Moves navigator to the next record.</summary>
  public bool MoveNext()
  {
    if (this._records == null)
      throw new Exception("Navigator is not initialized.");
    ++this._index;
    return this._index < this._records.Count;
  }

  /// <summary>Gets the current record object.</summary>
  public object Current
  {
    get
    {
      if (this._records == null)
        throw new Exception("Navigator is not initialized.");
      if (this._index < 0 || this._index >= this._records.Count)
        throw new Exception("Navigator is out of range.");
      return this._records[this._index];
    }
  }

  public object GetItem(object row, string dataField)
  {
    return this.GetItem(row, dataField, out PXCache _, out string[] _, false);
  }

  public object GetItem(
    object row,
    string dataField,
    out PXCache cache,
    out string[] fieldList,
    bool getLast)
  {
    cache = (PXCache) null;
    fieldList = (string[]) null;
    if (this._fieldMap != null)
    {
      IDataNavigator idataNavigator;
      if (this._children == null || !this._children.TryGetValue(row, out idataNavigator))
        return (object) null;
      SoapNavigator soapNavigator = (SoapNavigator) idataNavigator;
      row = soapNavigator.GetItem(soapNavigator._records[getLast ? soapNavigator._records.Count - 1 : 0], dataField, out cache, out fieldList, getLast);
      if (fieldList != null)
      {
        Array.Resize<string>(ref fieldList, fieldList.Length + this._fieldMap.Count);
        this._fieldMap.Keys.CopyTo(fieldList, fieldList.Length - this._fieldMap.Count);
      }
      else
      {
        fieldList = new string[this._fieldMap.Count];
        this._fieldMap.Keys.CopyTo(fieldList, 0);
      }
      return row;
    }
    bool flag = false;
    int length = dataField != null ? dataField.IndexOf('.') : 0;
    KeyValuePair<PXCache, int> keyValuePair;
    if (this._cacheMap != null)
    {
      if (dataField != null && length > 0)
      {
        if (this._cacheMap.TryGetValue(dataField.Substring(0, length), out keyValuePair))
          flag = true;
      }
      else
      {
        keyValuePair = this._default;
        flag = this._defaultFound;
      }
    }
    else
    {
      keyValuePair = this._default;
      flag = this._defaultFound;
    }
    if (!flag)
      return (object) null;
    cache = keyValuePair.Key;
    return this._cacheMap == null ? row : ((object[]) row)[keyValuePair.Value];
  }

  /// <summary>Gets the specified field value for specified record.</summary>
  public object GetValue(object row, string dataField, ref string format, bool valueOnly = false)
  {
    string key = dataField;
    if (!string.IsNullOrEmpty(key))
    {
      string name = Thread.CurrentThread.CurrentCulture.Name;
      bool flag1 = key.EndsWith(".Format", StringComparison.OrdinalIgnoreCase) && key.IndexOf('.') != key.Length - 7;
      bool flag2 = false;
      bool flag3 = false;
      if (flag1)
        key = key.Substring(0, dataField.Length - 7);
      else if (key.EndsWith(".Raw", StringComparison.OrdinalIgnoreCase) && key.IndexOf('.') != key.Length - 4)
      {
        flag2 = true;
        key = key.Substring(0, dataField.Length - 4);
      }
      else if (key.EndsWith(".DisplayName", StringComparison.OrdinalIgnoreCase) && key.IndexOf('.') != key.Length - 12)
      {
        flag3 = true;
        key = key.Substring(0, dataField.Length - 12);
      }
      if (this._fieldMap != null)
      {
        if (flag1)
        {
          IDataNavigator idataNavigator;
          if ((this._Formats == null || !this._Formats.TryGetValue(key + name, out format) || string.IsNullOrEmpty(format)) && this._children != null && this._children.TryGetValue(row, out idataNavigator))
          {
            SoapNavigator soapNavigator = (SoapNavigator) idataNavigator;
            soapNavigator.GetValue(soapNavigator._records[0], dataField, ref format);
          }
          return (object) format;
        }
        int index;
        if (!flag3 && !flag2 && this._fieldMap.TryGetValue(key, out index))
        {
          IDataNavigator idataNavigator;
          if ((this._Formats == null || !this._Formats.TryGetValue(key + name, out format) || string.IsNullOrEmpty(format)) && this._children != null && this._children.TryGetValue(row, out idataNavigator))
          {
            SoapNavigator soapNavigator = (SoapNavigator) idataNavigator;
            soapNavigator.GetValue(soapNavigator._records[0], dataField, ref format, valueOnly);
          }
          return ((object[]) row)[index];
        }
        IDataNavigator idataNavigator1;
        if (this._children != null && this._children.TryGetValue(row, out idataNavigator1))
        {
          SoapNavigator soapNavigator = (SoapNavigator) idataNavigator1;
          return soapNavigator.GetValue(soapNavigator._records[0], dataField, ref format, valueOnly);
        }
      }
      else
      {
        int length = key.IndexOf('.');
        KeyValuePair<PXCache, int> keyValuePair;
        bool flag4;
        if (this._cacheMap != null && length > 0)
        {
          if (!this._cacheMap.TryGetValue(key.Substring(0, length), out keyValuePair) && (!key.StartsWith("Row") || !this._cacheMap.TryGetValue(key.Substring(3, length - 3), out keyValuePair)))
            throw new Exception($"Invalid table name {key.Substring(0, length)} of the field {key} has been specified.");
          flag4 = true;
        }
        else
        {
          keyValuePair = this._default;
          flag4 = this._defaultFound;
        }
        if (flag4)
        {
          string str = key;
          if (length > 0 && length < key.Length - 1)
            str = key.Substring(length + 1);
          if (flag3)
            return keyValuePair.Key.GetStateExt((object) null, str) is PXFieldState stateExt && stateExt.Visibility != PXUIVisibility.HiddenByAccessRights ? (object) stateExt.DisplayName : (object) null;
          object returnValue = (object) null;
          if (row is object[] objArray && keyValuePair.Value >= objArray.Length)
            return (object) string.Empty;
          object data = this._cacheMap != null ? ((object[]) row)[keyValuePair.Value] : row;
          if (data is IBqlTable)
            returnValue = keyValuePair.Key.GetStateInt(data, str);
          else
            keyValuePair.Key.RaiseFieldSelectingInt(str, (object) null, ref returnValue, true);
          if (returnValue == null && !keyValuePair.Key.Fields.Contains(str))
            throw new Exception($"Invalid field name {key} has been specified.");
          returnValue = this._GetValueInt(returnValue, ref format, valueOnly && !flag1);
          if (this._Formats == null)
            this._Formats = new Dictionary<string, string>();
          string b;
          if (!this._Formats.TryGetValue(key + name, out b))
            this._Formats[key + name] = format;
          else if (!string.Equals(format, b) && b != null)
            this._Formats[key + name] = (string) null;
          if (flag1)
            return (object) format;
          if (flag2)
          {
            returnValue = keyValuePair.Key.GetValue(data, str);
            format = "";
          }
          return returnValue;
        }
      }
    }
    return (object) null;
  }

  private object _GetValueInt(object val, ref string format, bool valueOnly = false)
  {
    string name = Thread.CurrentThread.CurrentCulture.Name;
    if (!(val is PXFieldState pxFieldState))
      return val;
    if (valueOnly)
      return pxFieldState.Value;
    switch (val)
    {
      case PXStringState pxStringState:
        if (pxStringState.AllowedValues == null || pxStringState.AllowedValues.Length == 0)
        {
          if (string.IsNullOrEmpty(pxStringState.InputMask) || this._Masks.TryGetValue((object) pxStringState.InputMask, out format))
            return pxStringState.Value;
          this._Masks[(object) pxStringState.InputMask] = format = $"{{{pxStringState.InputMask}}}";
        }
        else
        {
          SoapNavigator.MaskKey key = new SoapNavigator.MaskKey((object) pxStringState.AllowedValues, name);
          if (!this._Masks.TryGetValue((object) key, out format))
          {
            StringBuilder stringBuilder = new StringBuilder("[");
            for (int index = 0; index < pxStringState.AllowedValues.Length; ++index)
            {
              if (index > 0)
                stringBuilder.Append(Delimiters.RecordsDelimiter);
              stringBuilder.Append(pxStringState.AllowedValues[index]);
              stringBuilder.Append(Delimiters.ValueDelimiter);
              if (pxStringState.AllowedLabels != null && index < pxStringState.AllowedLabels.Length)
                stringBuilder.Append(pxStringState.AllowedLabels[index]);
              else
                stringBuilder.Append(pxStringState.AllowedValues[index]);
            }
            stringBuilder.Append(']');
            this._Masks[(object) key] = format = stringBuilder.ToString();
          }
        }
        return pxStringState.Value;
      case PXDateState pxDateState:
        format = pxDateState.DisplayMask ?? "d";
        return pxDateState.Value;
      case PXIntState pxIntState:
        if (pxIntState.AllowedValues != null && pxIntState.AllowedValues.Length != 0)
        {
          SoapNavigator.MaskKey key = new SoapNavigator.MaskKey((object) pxIntState.AllowedValues, name);
          if (this._Masks.TryGetValue((object) key, out format))
            return pxIntState.Value;
          StringBuilder stringBuilder = new StringBuilder("[");
          for (int index = 0; index < pxIntState.AllowedValues.Length; ++index)
          {
            if (index > 0)
              stringBuilder.Append(Delimiters.RecordsDelimiter);
            stringBuilder.Append(pxIntState.AllowedValues[index]);
            stringBuilder.Append(Delimiters.ValueDelimiter);
            if (pxIntState.AllowedLabels != null && index < pxIntState.AllowedLabels.Length)
              stringBuilder.Append(pxIntState.AllowedLabels[index]);
            else
              stringBuilder.Append(pxIntState.AllowedValues[index]);
          }
          stringBuilder.Append(']');
          this._Masks[(object) key] = format = stringBuilder.ToString();
        }
        return pxIntState.Value;
      default:
        if ((pxFieldState.DataType == typeof (float) || pxFieldState.DataType == typeof (double) || pxFieldState.DataType == typeof (Decimal)) && !this._Masks.TryGetValue((object) pxFieldState.Precision, out format))
        {
          Dictionary<object, string> masks = this._Masks;
          // ISSUE: variable of a boxed type
          __Boxed<int> precision = (ValueType) pxFieldState.Precision;
          ref string local = ref format;
          string str1 = pxFieldState.Precision.ToString();
          string str2;
          string str3 = str2 = "N" + str1;
          local = str2;
          string str4 = str3;
          masks[(object) precision] = str4;
        }
        return pxFieldState.Value;
    }
  }

  /// <summary>Gets the specified field value for current record.</summary>
  public object this[string dataField]
  {
    get
    {
      if (this._records == null)
        throw new Exception("Navigator is not initialized.");
      if (this._index < 0 || this._index >= this._records.Count)
        throw new Exception("Navigator is out of range.");
      string format = (string) null;
      return this.GetValue(this.Current, dataField, ref format);
    }
  }

  /// <summary>Gets the the report data source select arguments.</summary>
  public ReportSelectArguments SelectArguments => this._arguments;

  private bool _EnsureBranches(ReportSelectArguments reportInfo)
  {
    if (reportInfo.Parameters == null || !string.IsNullOrEmpty(this.CurrentlyProcessingParam) && this.CurrentlyProcessingParam.ToLower() == "organizationid" || !PXDatabase.ReadBranchRestricted)
      return false;
    if (!string.IsNullOrEmpty(this.CurrentlyProcessingParam))
    {
      ReportParameter parameter1 = reportInfo.Parameters["organizationid"];
      ReportParameter parameter2 = reportInfo.Parameters[this.CurrentlyProcessingParam];
      if (parameter1 != null && parameter2 != null)
      {
        int num1 = ((List<ReportParameter>) reportInfo.Parameters).IndexOf(parameter1);
        int num2 = ((List<ReportParameter>) reportInfo.Parameters).IndexOf(parameter2);
        if (num1 != -1 && num2 != -1 && num2 <= num1)
          return false;
      }
    }
    List<int> source = (List<int>) null;
    foreach (ReportParameter parameter in (List<ReportParameter>) reportInfo.Parameters)
    {
      if (!(parameter.Name.ToLower() != "organizationid"))
      {
        string organizationCD = (string) parameter.Value;
        if (!string.IsNullOrEmpty(organizationCD))
        {
          source = ((IEnumerable<int>) PXAccess.GetChildBranchIDs(organizationCD)).ToList<int>();
          if (!source.Any<int>())
          {
            source = new List<int>() { 0 };
            break;
          }
          break;
        }
        break;
      }
    }
    PXDatabase.SpecificBranchTable = (string) null;
    PXDatabase.BranchIDs = source;
    PXDatabase.ReadBranchRestricted = false;
    return true;
  }

  private void _EnsureSpecificTable(PXGraph graph, BqlSoapCommand comm)
  {
    if (graph == null || comm == null || ((CollectionBase) this.SelectArguments.Relations).Count <= 0)
      return;
    System.Type key1 = comm._tablemap[this.SelectArguments.Relations[0].ParentName];
    PXCache cach1 = graph.Caches[key1];
    if (cach1.BqlSelect == null)
    {
      if (PXDatabase.CheckBranchRestrictionSupported(cach1.BqlTable, cach1.Graph.SqlDialect))
        return;
    }
    else
    {
      foreach (System.Type table in cach1.BqlSelect.GetTables())
      {
        if (PXDatabase.CheckBranchRestrictionSupported(graph.Caches[table].BqlTable, cach1.Graph.SqlDialect))
          return;
      }
    }
    foreach (ReportRelation relation in (CollectionBase) this.SelectArguments.Relations)
    {
      System.Type key2 = comm._tablemap[relation.ChildName];
      PXCache cach2 = graph.Caches[key2];
      if (cach2.BqlSelect == null)
      {
        if (PXDatabase.CheckBranchRestrictionSupported(cach2.BqlTable, cach2.Graph.SqlDialect))
          break;
      }
      else
      {
        foreach (System.Type table in cach2.BqlSelect.GetTables())
        {
          if (PXDatabase.CheckBranchRestrictionSupported(graph.Caches[table].BqlTable, cach2.Graph.SqlDialect))
            return;
        }
      }
    }
  }

  private PXCache _GetSource(object sourceName, out string fieldName)
  {
    string str1 = sourceName as string;
    if (!Str.IsNullOrEmpty(str1))
    {
      int length = str1.LastIndexOf('.');
      if (length != -1 && length < str1.Length - 1)
      {
        string str2 = str1.Substring(0, length);
        KeyValuePair<PXCache, int> keyValuePair;
        if (this._cacheMap != null && this._cacheMap.TryGetValue(str2, out keyValuePair))
        {
          fieldName = str1.Substring(length + 1);
          return keyValuePair.Key;
        }
        ReportSelectArguments selectArguments = this.SelectArguments;
        System.Type key = ServiceManager.ReportNameResolver.ResolveTable(str2, selectArguments.Tables);
        if (key != (System.Type) null)
        {
          fieldName = str1.Substring(length + 1);
          return this._Graph.Caches[key];
        }
      }
    }
    fieldName = (string) null;
    return (PXCache) null;
  }

  private object _GetCurrent(PXCache cache)
  {
    if (cache == null)
      return (object) null;
    if (this._EnsureBranches(this.SelectArguments) && cache.Keys.Count == 0)
    {
      cache.Current = (object) null;
      cache.Clear();
    }
    bool branchRestricted = PXDatabase.ReadBranchRestricted;
    try
    {
      if (((string.IsNullOrEmpty(this.CurrentlyProcessingParam) ? 0 : (this.CurrentlyProcessingParam.ToLower() == "organizationid" ? 1 : 0)) & (branchRestricted ? 1 : 0)) != 0)
        PXDatabase.ReadBranchRestricted = false;
      return SoapNavigator.DATA._SelfGetCurrent(cache);
    }
    finally
    {
      PXDatabase.ReadBranchRestricted = branchRestricted;
    }
  }

  public object GetDefInt(object field) => this._Data.GetDefInt(field);

  public object GetDefExt(object field) => this._Data.GetDefExt(field);

  public object GetDefUI(object field) => this._Data.GetDefUI(field);

  public object GetDisplayName(object field) => this._Data.GetDisplayName(field);

  public object GetDescription(object field, object value)
  {
    return this._Data.GetDescription(field, value);
  }

  public object GetFormat(object field) => this._Data.GetFormat(field);

  public object GetMask(object field) => this._Data.GetMask(field);

  public object IntToExt(object field, object value) => this._Data.IntToExt(field, value);

  public object ExtToInt(object field, object value) => this._Data.ExtToInt(field, value);

  public object IntToUI(object field, object value) => this._Data.IntToUI(field, value);

  public object UIToInt(object field, object value) => this._Data.UIToInt(field, value);

  public object ExtToUI(object field, object value) => this._Data.ExtToUI(field, value);

  public object UIToExt(object field, object value) => this._Data.UIToExt(field, value);

  public string CurrentlyProcessingParam
  {
    get => this.currentlyProcessingParam;
    set => this.currentlyProcessingParam = value;
  }

  public object GetView(object field)
  {
    return !(this.GetFieldSchema(field) is PXFieldState fieldSchema) ? (object) null : (object) fieldSchema.ViewName;
  }

  public int[] GetFieldSegments(string field)
  {
    if (SoapNavigator.IsFormula(field))
      return (int[]) null;
    if (!(this.GetFieldSchema((object) field) is PXSegmentedState fieldSchema))
      return (int[]) null;
    List<int> list = new List<int>();
    EnumerableExtensions.ForEach<PXSegment>((IEnumerable<PXSegment>) fieldSchema.Segments, (System.Action<PXSegment>) (s => list.Add((int) s.Length)));
    return list.ToArray();
  }

  public object GetFieldSchema(object field)
  {
    Tuple<string, Func<object, object>>[] parameters = (Tuple<string, Func<object, object>>[]) null;
    if (field is string)
    {
      string[] strArray = ((string) field).Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length > 1)
      {
        field = (object) strArray[0];
        parameters = new Tuple<string, Func<object, object>>[strArray.Length - 1];
        for (int index = 0; index < parameters.Length; ++index)
          parameters[index] = new Tuple<string, Func<object, object>>(strArray[index + 1], (Func<object, object>) (_ => _));
        if (this.reportParams == null)
          this.reportParams = new Dictionary<string, SoapNavigator.DependentParameters>();
      }
    }
    string fieldName;
    PXCache cache = this._GetSource(field, out fieldName);
    if (cache != null)
    {
      foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(fieldName))
      {
        if (attribute is PXDimensionAttribute dimensionAttribute)
          dimensionAttribute.ValidComboRequired = true;
      }
      EnumerableExtensions.ForEach<PXSelectorAttribute>(cache.Fields.SelectMany<string, PXSelectorAttribute>((Func<string, IEnumerable<PXSelectorAttribute>>) (f => cache.GetAttributesOfType<PXSelectorAttribute>((object) null, f))), (System.Action<PXSelectorAttribute>) (s => s.ValidateValue = false));
      EnumerableExtensions.ForEach<PXRestrictorAttribute>(cache.Fields.SelectMany<string, PXRestrictorAttribute>((Func<string, IEnumerable<PXRestrictorAttribute>>) (f => cache.GetAttributesOfType<PXRestrictorAttribute>((object) null, f))), (System.Action<PXRestrictorAttribute>) (s => s.SuppressVerify = true));
      object data = this._Data._GetCurrent(cache);
      if (cache.GetStateExt(data, fieldName) is PXFieldState stateExt)
      {
        if (!string.IsNullOrEmpty(stateExt.ViewName))
        {
          if (this._Schemas == null)
            this._Schemas = new Dictionary<string, PXFieldState[]>();
          if (stateExt.FieldList != null)
          {
            PXFieldState[] pxFieldStateArray = new PXFieldState[stateExt.FieldList.Length];
            bool flag = false;
            for (int index = 0; index < pxFieldStateArray.Length; ++index)
            {
              pxFieldStateArray[index] = cache.Graph.GetStateExt(stateExt.ViewName, (object) null, stateExt.FieldList[index]) as PXFieldState;
              if (pxFieldStateArray[index] == null)
              {
                if (stateExt.ViewName != null && parameters != null)
                  this.reportParams[this.CurrentlyProcessingParam] = new SoapNavigator.DependentParameters(stateExt.ViewName, parameters);
                return (object) stateExt;
              }
              int length = stateExt.FieldList[index].IndexOf("__");
              if (length > 0 && length < stateExt.FieldList[index].Length - 2)
                pxFieldStateArray[index].SetAliased(stateExt.FieldList[index].Substring(0, length));
              pxFieldStateArray[index].Visibility = PXUIVisibility.SelectorVisible;
              if (!flag)
                flag = pxFieldStateArray[index].PrimaryKey;
            }
            if (!flag && pxFieldStateArray.Length != 0)
              pxFieldStateArray[0]._IsKey = true;
            this._Schemas[stateExt.ViewName] = pxFieldStateArray;
          }
          if (stateExt.ViewName != null && parameters != null && !string.IsNullOrEmpty(this.CurrentlyProcessingParam))
            this.reportParams[this.CurrentlyProcessingParam] = new SoapNavigator.DependentParameters(stateExt.ViewName, parameters);
        }
        return (object) stateExt;
      }
    }
    return (object) null;
  }

  public string[] GetDependParameterNames(string paramName)
  {
    SoapNavigator.DependentParameters dependentParameters;
    return !string.IsNullOrEmpty(paramName) && this.reportParams != null && this.reportParams.TryGetValue(paramName, out dependentParameters) && dependentParameters != null ? dependentParameters.paramNames : (string[]) null;
  }

  public PXFieldState[] GetSchema(string viewName)
  {
    PXFieldState[] schema = (PXFieldState[]) null;
    if (this._Schemas != null)
      this._Schemas.TryGetValue(viewName, out schema);
    return schema;
  }

  public bool TryAddKey(string key) => this._Data.TryAddKey(key);

  public bool TryAddKey(string setId, string key) => this._Data.TryAddKey(setId, key);

  public static Dictionary<string, string> FilterDBFields(
    ICollection<object> fields,
    Dictionary<string, string> aliasMap,
    ReportSelectArguments selectArguments)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXGraph graph = new PXGraph();
    BqlSoapCommand bqlSoapCommand = new BqlSoapCommand(graph, selectArguments);
    foreach (object field in (IEnumerable<object>) fields)
    {
      string name1 = field is ViewerField ? ((ViewerField) field).Name : (string) field;
      if (!string.IsNullOrEmpty(name1) && !name1.StartsWith("="))
      {
        string fieldNameOrig;
        string str1 = fieldNameOrig = name1;
        int length;
        if ((length = str1.IndexOf('.')) > 0)
          str1 = str1.Substring(length + 1);
        Tuple<System.Type, string, System.Type> bqlField = bqlSoapCommand.tryFindBqlField(graph, ref name1);
        PXCache cach = bqlField == null ? (PXCache) null : graph.Caches[bqlField.Item1];
        if (cach != null && BqlSoapCommand.GetFieldExprInternal(bqlField.Item1, cach, str1, cach.GetItemType().Name, BqlCommand.FieldPlace.Select) != null)
        {
          string str2 = (string) null;
          if (cach.GetStateExt((object) null, bqlField.Item2) is PXFieldState stateExt)
          {
            string name2 = fieldNameOrig;
            string fname = "";
            PXCache cache = bqlSoapCommand.tryFindCache(graph, ref name2, ref fname);
            System.Type t = cache == null ? cach.GetItemType() : cache.GetItemType();
            string str3 = aliasMap.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (_ => _.Value == t.Name || _.Value == "Row" + t.Name)).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (_ => _.Key)).FirstOrDefault<string>((Func<string, bool>) (_ => fieldNameOrig.StartsWith(_ + ".")));
            string displayName = stateExt.DisplayName;
            if (!(stateExt is PXNoteState))
              PXReportItemLocalizer.LocalizeReportField(str1, ref displayName, cach);
            if (t.IsDefined(typeof (PXCacheNameAttribute), true) && (fieldNameOrig.StartsWith(t.Name + ".") || fieldNameOrig.StartsWith($"Row{t.Name}.") || str3 != null))
            {
              PXCacheNameAttribute customAttribute = (PXCacheNameAttribute) t.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0];
              str2 = str3 == null ? $"{customAttribute.GetName()}.{displayName}" : $"{customAttribute.GetName()} ({str3}).{displayName}";
            }
            else
              str2 = length <= 0 ? $"{t.Name}.{displayName}" : $"{fieldNameOrig.Substring(0, length)}.{displayName}";
          }
          dictionary[fieldNameOrig] = str2;
        }
      }
    }
    return dictionary;
  }

  private class MaskKey
  {
    public object Key;
    public string Culture;

    public MaskKey(object key, string culture)
    {
      this.Key = key;
      this.Culture = culture;
    }

    public override bool Equals(object obj)
    {
      SoapNavigator.MaskKey maskKey = (SoapNavigator.MaskKey) obj;
      return maskKey.Key == this.Key && maskKey.Culture == this.Culture;
    }

    public override int GetHashCode() => this.GetType().GetHashCode();
  }

  private class ComparedValue
  {
    private SoapNavigator _nav;
    private int _index;
    public object Row;
    public object _Value;
    public string FieldName;
    private bool _IsCalced;
    public PXCache Cache;

    public ComparedValue()
    {
    }

    public ComparedValue(SoapNavigator nav, string fieldName, int _rowIndex)
    {
      this._nav = nav;
      this._index = _rowIndex;
      this.FieldName = fieldName;
    }

    public object GetValue()
    {
      if (!this._IsCalced)
      {
        if (SoapNavigator.IsFormula(this.FieldName))
        {
          if (this._Value == null && this._nav != null)
          {
            this._IsCalced = true;
            this._Value = (object) ReportExprParser.Eval(this.FieldName, this._nav._arguments.Relations.Report, (IDataNavigator) this._nav, (object) (this._nav._records[this._index] as IEnumerable<object>)).ToString();
          }
        }
        else
        {
          this._IsCalced = true;
          this._Value = this.Cache.GetValue(this.Row, this.FieldName);
        }
      }
      return this._Value;
    }

    public object GetValueEx()
    {
      return SoapNavigator.IsFormula(this.FieldName) ? this.GetValue() : PXFieldState.UnwrapValue(this.Cache.GetValueExt(this.Row, this.FieldName));
    }

    public override bool Equals(object obj)
    {
      SoapNavigator.ComparedValue comparedValue = (SoapNavigator.ComparedValue) obj;
      return this.Row == comparedValue.Row || object.Equals(this.GetValue(), comparedValue.GetValue());
    }

    public override int GetHashCode() => base.GetHashCode();
  }

  public class DependentParameters
  {
    private string _viewName;
    private Tuple<string, Func<object, object>>[] _parameters;

    public DependentParameters(string viewName, string[] paramNames)
    {
      this._viewName = viewName;
      this.paramNames = paramNames;
    }

    public DependentParameters(string viewName, Tuple<string, Func<object, object>>[] parameters)
    {
      this._viewName = viewName;
      this._parameters = parameters;
    }

    public string viewName
    {
      get => this._viewName;
      set => this._viewName = value;
    }

    public string[] paramNames
    {
      get
      {
        Tuple<string, Func<object, object>>[] parameters = this._parameters;
        return parameters == null ? (string[]) null : ((IEnumerable<Tuple<string, Func<object, object>>>) parameters).Select<Tuple<string, Func<object, object>>, string>((Func<Tuple<string, Func<object, object>>, string>) (_ => _.Item1)).ToArray<string>();
      }
      set
      {
        this._parameters = value != null ? ((IEnumerable<string>) value).Select<string, Tuple<string, Func<object, object>>>((Func<string, Tuple<string, Func<object, object>>>) (_ => new Tuple<string, Func<object, object>>(_, (Func<object, object>) (__ => __)))).ToArray<Tuple<string, Func<object, object>>>() : (Tuple<string, Func<object, object>>[]) null;
      }
    }

    public Tuple<string, Func<object, object>>[] parameters
    {
      get => this._parameters;
      set => this._parameters = value;
    }
  }

  [Serializable]
  public class DATA
  {
    protected PXGraph _Graph;
    private Func<string, System.Type> _Tables;
    internal SoapNavigator.DATA._GetSourceDelegate _GetSource;
    internal SoapNavigator.DATA._GetCurrentDelegate _GetCurrent;
    private const string KEYS_POSTFIX = "_keys";
    private const string DEFAULT_SETID = "_";
    private Dictionary<string, HashSet<string>> _Keys;

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
      this._Graph.Defaults[typeof (AccessInfo)] = (PXGraph.GetDefaultDelegate) (() => (object) this._Graph.Accessinfo);
    }

    public DATA()
    {
      this._Graph = new PXGraph();
      this._Graph.Defaults[typeof (AccessInfo)] = (PXGraph.GetDefaultDelegate) (() => (object) this._Graph.Accessinfo);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this._Tables = SoapNavigator.DATA.\u003C\u003EO.\u003C0\u003E__ResolveShortName ?? (SoapNavigator.DATA.\u003C\u003EO.\u003C0\u003E__ResolveShortName = new Func<string, System.Type>(ServiceManager.ReportNameResolver.ResolveShortName));
      this._GetSource = new SoapNavigator.DATA._GetSourceDelegate(this._SelfGetSource);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this._GetCurrent = SoapNavigator.DATA.\u003C\u003EO.\u003C1\u003E___SelfGetCurrent ?? (SoapNavigator.DATA.\u003C\u003EO.\u003C1\u003E___SelfGetCurrent = new SoapNavigator.DATA._GetCurrentDelegate(SoapNavigator.DATA._SelfGetCurrent));
    }

    internal DATA(PXGraph graph)
    {
      this._Graph = graph;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this._Tables = SoapNavigator.DATA.\u003C\u003EO.\u003C0\u003E__ResolveShortName ?? (SoapNavigator.DATA.\u003C\u003EO.\u003C0\u003E__ResolveShortName = new Func<string, System.Type>(ServiceManager.ReportNameResolver.ResolveShortName));
      this._GetSource = new SoapNavigator.DATA._GetSourceDelegate(this._SelfGetSource);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this._GetCurrent = SoapNavigator.DATA.\u003C\u003EO.\u003C1\u003E___SelfGetCurrent ?? (SoapNavigator.DATA.\u003C\u003EO.\u003C1\u003E___SelfGetCurrent = new SoapNavigator.DATA._GetCurrentDelegate(SoapNavigator.DATA._SelfGetCurrent));
    }

    internal DATA(
      PXGraph graph,
      Func<string, System.Type> tables,
      SoapNavigator.DATA._GetSourceDelegate getSource,
      SoapNavigator.DATA._GetCurrentDelegate getCurrent)
    {
      this._Graph = graph;
      this._Tables = tables;
      this._GetSource = getSource;
      this._GetCurrent = getCurrent;
    }

    internal static object _GetValueInt(object val, ref string format)
    {
      if (!(val is PXFieldState pxFieldState))
        return val;
      if (val is PXStringState pxStringState)
      {
        if (pxStringState.AllowedValues == null || pxStringState.AllowedValues.Length == 0)
        {
          if (!string.IsNullOrEmpty(pxStringState.InputMask))
            format = $"{{{pxStringState.InputMask}}}";
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder("[");
          for (int index = 0; index < pxStringState.AllowedValues.Length; ++index)
          {
            if (index > 0)
              stringBuilder.Append(Delimiters.RecordsDelimiter);
            stringBuilder.Append(pxStringState.AllowedValues[index]);
            stringBuilder.Append(Delimiters.ValueDelimiter);
            if (pxStringState.AllowedLabels != null && index < pxStringState.AllowedLabels.Length)
              stringBuilder.Append(pxStringState.AllowedLabels[index]);
            else
              stringBuilder.Append(pxStringState.AllowedValues[index]);
          }
          stringBuilder.Append(']');
          format = stringBuilder.ToString();
        }
        return pxStringState.Value;
      }
      if (val is PXDateState pxDateState)
      {
        format = pxDateState.DisplayMask ?? "d";
        return pxDateState.Value;
      }
      if (val is PXIntState pxIntState)
      {
        if (pxIntState.AllowedValues != null && pxIntState.AllowedValues.Length != 0)
        {
          StringBuilder stringBuilder = new StringBuilder("[");
          for (int index = 0; index < pxIntState.AllowedValues.Length; ++index)
          {
            if (index > 0)
              stringBuilder.Append(Delimiters.RecordsDelimiter);
            stringBuilder.Append(pxIntState.AllowedValues[index]);
            stringBuilder.Append(Delimiters.ValueDelimiter);
            if (pxIntState.AllowedLabels != null && index < pxIntState.AllowedLabels.Length)
              stringBuilder.Append(pxIntState.AllowedLabels[index]);
            else
              stringBuilder.Append(pxIntState.AllowedValues[index]);
          }
          stringBuilder.Append(']');
          format = stringBuilder.ToString();
        }
        return pxIntState.Value;
      }
      if (pxFieldState.DataType == typeof (float) || pxFieldState.DataType == typeof (double) || pxFieldState.DataType == typeof (Decimal))
        format = "N" + (pxFieldState.Precision >= 0 ? pxFieldState.Precision.ToString() : string.Empty);
      return pxFieldState.Value;
    }

    private PXCache _SelfGetSource(object sourceName, out string fieldName)
    {
      string str = sourceName as string;
      if (!string.IsNullOrEmpty(str))
      {
        int length = str.IndexOf('.');
        if (length != -1 && length < str.Length - 1)
        {
          System.Type key = this._Tables(str.Substring(0, length));
          if (key != (System.Type) null)
          {
            fieldName = str.Substring(length + 1);
            return this._Graph.Caches[key];
          }
        }
      }
      fieldName = (string) null;
      return (PXCache) null;
    }

    public static object _SelfGetCurrent(PXCache cache)
    {
      if (cache == null)
        return (object) null;
      if (cache.Current == null)
      {
        if (cache.Keys.Count == 0)
        {
          try
          {
            if (cache.Update((IDictionary) new Dictionary<string, object>(), (IDictionary) new Dictionary<string, object>()) == 0)
              cache.Insert();
          }
          catch
          {
            cache.Insert();
          }
        }
        else
          cache.Insert();
      }
      return cache.Current;
    }

    public object GetDefInt(object field)
    {
      string fieldName;
      PXCache cache = this._GetSource(field, out fieldName);
      object data = this._GetCurrent(cache);
      return data != null ? cache.GetValue(data, fieldName) : (object) null;
    }

    public object GetDefExt(object field)
    {
      string fieldName;
      PXCache cache = this._GetSource(field, out fieldName);
      object data = this._GetCurrent(cache);
      if (data == null)
        return (object) null;
      object valueExt = cache.GetValueExt(data, fieldName);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      return valueExt;
    }

    public object GetDefUI(object field)
    {
      string fieldName;
      PXCache cache = this._GetSource(field, out fieldName);
      object obj = this._GetCurrent(cache);
      if (obj == null)
        return (object) null;
      object stringValue = cache.GetValue(obj, fieldName);
      if (stringValue != null)
      {
        cache.RaiseFieldSelecting(fieldName, obj, ref stringValue, true);
        stringValue = (object) PXFieldState.GetStringValue(stringValue as PXFieldState, "N", "d");
      }
      return stringValue;
    }

    public object GetDisplayName(object field)
    {
      string fieldName;
      PXCache cache = this._GetSource(field, out fieldName);
      if (cache == null || !(cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt))
        return (object) null;
      System.Type itemType = cache.GetItemType();
      string displayName = stateExt.DisplayName;
      PXReportItemLocalizer.LocalizeReportField(fieldName, ref displayName, cache, false);
      return itemType.IsDefined(typeof (PXCacheNameAttribute), true) ? (object) $"{((PXNameAttribute) itemType.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName()}.{displayName}" : (object) $"{itemType.Name}.{displayName}";
    }

    public object GetDescription(object field, object value)
    {
      string fieldName;
      PXCache cache = this._GetSource(field, out fieldName);
      if (cache != null && cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.ViewName))
      {
        if (!string.IsNullOrEmpty(stateExt.DescriptionName))
        {
          try
          {
            cache.RaiseFieldUpdating(fieldName, (object) null, ref value);
          }
          catch
          {
          }
          object data = PXSelectorAttribute.Select(cache, (object) null, fieldName, value);
          if (data != null)
            return (!PXDBLocalizableStringAttribute.IsEnabled ? 0 : (cache.GetAttributes(stateExt.DescriptionName).OfType<PXDBLocalizableStringAttribute>().Any<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (l => l.MultiLingual)) ? 1 : 0)) == 0 ? cache.Graph.GetValue(stateExt.ViewName, data, stateExt.DescriptionName) : PXFieldState.UnwrapValue(cache.Graph.GetValueExt(stateExt.ViewName, data, stateExt.DescriptionName));
        }
      }
      return (object) null;
    }

    public object GetFormat(object field)
    {
      string fieldName;
      PXCache pxCache = this._GetSource(field, out fieldName);
      if (pxCache == null)
        return (object) null;
      string format = (string) null;
      SoapNavigator.DATA._GetValueInt(pxCache.GetStateExt((object) null, fieldName), ref format);
      return (object) format;
    }

    public object GetMask(object field)
    {
      if (this.GetFormat(field) is string mask)
        mask = mask.Replace("{", "").Replace("}", "");
      return (object) mask;
    }

    public object IntToExt(object field, object value)
    {
      string fieldName;
      PXCache pxCache = this._GetSource(field, out fieldName);
      if (pxCache != null && value != null)
      {
        pxCache.RaiseFieldSelecting(fieldName, (object) null, ref value, false);
        if (value is PXFieldState)
          value = ((PXFieldState) value).Value;
      }
      return value;
    }

    public object ExtToInt(object field, object value)
    {
      string fieldName;
      PXCache pxCache = this._GetSource(field, out fieldName);
      if (pxCache != null)
      {
        if (value != null)
        {
          try
          {
            pxCache.RaiseFieldUpdating(fieldName, (object) null, ref value);
          }
          catch
          {
            value = (object) null;
          }
        }
      }
      return value;
    }

    public object IntToUI(object field, object value)
    {
      string fieldName;
      PXCache pxCache = this._GetSource(field, out fieldName);
      if (pxCache != null && value != null)
      {
        pxCache.RaiseFieldSelecting(fieldName, (object) null, ref value, true);
        value = (object) PXFieldState.GetStringValue(value as PXFieldState, "N", "d");
      }
      return value;
    }

    public object UIToInt(object field, object value) => value;

    public object ExtToUI(object field, object value)
    {
      string fieldName;
      PXCache pxCache = this._GetSource(field, out fieldName);
      if (pxCache != null && value != null)
      {
        object returnValue = (object) null;
        pxCache.RaiseFieldSelecting(fieldName, (object) null, ref returnValue, true);
        if (returnValue is PXFieldState state)
        {
          state.Value = value;
          value = (object) PXFieldState.GetStringValue(state, "N", "d");
        }
      }
      return value;
    }

    public object UIToExt(object field, object value) => value;

    public bool TryAddKey(string key) => this.TryAddKey("_", key);

    public bool TryAddKey(string setId, string key)
    {
      if (this._Keys == null)
        this._Keys = PXContext.GetSlot<Dictionary<string, HashSet<string>>>(this._Graph.UID?.ToString() + "_keys");
      if (this._Keys == null)
      {
        this._Keys = new Dictionary<string, HashSet<string>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        PXContext.SetSlot<Dictionary<string, HashSet<string>>>(this._Graph.UID?.ToString() + "_keys", this._Keys);
      }
      HashSet<string> stringSet;
      if (!this._Keys.TryGetValue(setId, out stringSet))
      {
        stringSet = new HashSet<string>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        this._Keys.Add(setId, stringSet);
      }
      return stringSet.Add(key);
    }

    internal void ClearKeys() => PXContext.ClearSlot(this._Graph.UID?.ToString() + "_keys");

    internal delegate PXCache _GetSourceDelegate(object sourceName, out string fieldName);

    internal delegate object _GetCurrentDelegate(PXCache cache);
  }
}
