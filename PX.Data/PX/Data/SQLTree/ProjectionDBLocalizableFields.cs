// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionDBLocalizableFields
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Database.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionDBLocalizableFields : ProjectionReference
{
  private readonly System.Type _propertyType;
  private readonly string _propertyName;
  private readonly int _expectedColumnIndex;

  public ProjectionDBLocalizableFields(
    PXGraph graph,
    System.Type dac,
    System.Type propertyType,
    string propertyName)
    : base(dac)
  {
    this._propertyType = propertyType;
    this._propertyName = propertyName;
    this._expectedColumnIndex = this.FindLocalizableColumnIndex(graph);
  }

  protected override IEnumerable<PropertyInfo> GetDacProperties(System.Type dac)
  {
    return base.GetDacProperties(dac).Where<PropertyInfo>((System.Func<PropertyInfo, bool>) (p => p.Name == this._propertyName));
  }

  public override System.Type GetResultType() => this._propertyType;

  /// <inheritdoc />
  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    int position1 = 0;
    object obj = base.GetValue((PXDataRecord) new ProjectionDBLocalizableFields.DummyPXDataRecord(data, position, this._expectedColumnIndex), ref position1, context);
    ++position;
    string propertyName = this._propertyName;
    return ProjectionDBLocalizableFields.GetPropertyValue(obj, propertyName);
  }

  private static object GetPropertyValue(object obj, string propertyName)
  {
    PropertyInfo runtimeProperty = obj != null ? obj.GetType().GetRuntimeProperty(propertyName) : (PropertyInfo) null;
    return runtimeProperty == (PropertyInfo) null ? (object) null : runtimeProperty.GetValue(obj);
  }

  private int FindLocalizableColumnIndex(PXGraph graph)
  {
    if (!typeof (IBqlTable).IsAssignableFrom(this.type_))
      return -1;
    PXCache cach = graph?.Caches[this.type_];
    if (cach == null)
      return -1;
    int localizableColumnIndex = 0;
    foreach (string field in (List<string>) cach.Fields)
    {
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, this.type_, out description);
      if (description?.Expr != null)
      {
        if (field.Equals(this._propertyName))
          return localizableColumnIndex;
        ++localizableColumnIndex;
      }
    }
    return -1;
  }

  private class DummyPXDataRecord : PXDataRecord
  {
    private readonly PXDataRecord _source;
    private readonly int _position;
    private readonly int _expectedColumn;

    public DummyPXDataRecord(PXDataRecord source, int position, int expectedColumn)
    {
      this._source = source;
      this._position = position;
      this._expectedColumn = expectedColumn;
      this._Reader = (IDataReader) new PXDatabaseDummyProvider.DummyDataReader();
    }

    public override string GetString(int i)
    {
      return i == this._expectedColumn ? this._source.GetString(this._position) : (string) null;
    }
  }
}
