// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectExtension`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>The class that is used to define a mapping-based data view.</summary>
/// <typeparam name="Table">The base DAC.</typeparam>
public class PXSelectExtension<Table> : PXSelectBase<Table> where Table : PXMappedCacheExtension, new()
{
  /// <exclude />
  public PXSelectBase BaseSelect;
  private Lazy<PXCache> _Cache;

  /// <summary>Gets the cache that corresponds to the DAC specified in the type parameter.</summary>
  public override PXCache Cache => this._Cache.Value;

  /// <summary>Initializes a data view based on the specified Select statement.</summary>
  /// <param name="baseSelect">The base Select statement.</param>
  public PXSelectExtension(PXSelectBase baseSelect)
  {
    this.BaseSelect = baseSelect;
    this._Graph = baseSelect.View.Graph;
    this.View = baseSelect.View;
    this._Cache = new Lazy<PXCache>((Func<PXCache>) (() => PXCache._mapping.CreateModelExtension(typeof (Table), this.View.CacheGetItemType(), this._Graph)));
  }

  /// <summary>Gets or sets the <tt>Current</tt> property of the cache that corresponds to the DAC specified in the type parameter.</summary>
  public override Table Current
  {
    get => (Table) this.Cache.Current;
    set => this.Cache.Current = (object) value;
  }

  /// <summary>Inserts a new data record into the cache by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Insert">Insert()</see>
  /// method on the cache. Returns the inserted data record or
  /// <tt>null</tt>-if the insertion fails.</summary>
  public override Table Insert() => (Table) this.Cache.Insert();

  /// <summary>Inserts the provided data record into the cache by invoking
  /// the <see cref="M:PX.Data.PXCache`1.Insert(System.Object)">Insert(object)</see>
  /// method on the cache. Returns the inserted data record or
  /// <tt>null</tt>-if the insertion fails.</summary>
  /// <param name="item">The data record to insert.</param>
  public override Table Insert(Table item) => (Table) this.Cache.Insert((object) item);

  /// <summary>Initializes a data record of the derived DAC from the provided data record of the base DAC and inserts the new data record into the cache. Returns the inserted
  /// data record.</summary>
  /// <param name="item">The instance of the base DAC.</param>
  /// <remarks>The method relies on the <see cref="M:PX.Data.PXCache`1.Extend``1(``0)">Extend&lt;Parent&gt;(Parent)</see>
  /// method called on the cache.</remarks>
  public override Table Extend<Parent>(Parent item) => (Table) this.Cache.Extend<Parent>(item);

  /// <summary>Updates the data record in the cache by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Update(System.Object)">Update(object)</see>
  /// method on the cache. Returns the updated data record.</summary>
  /// <param name="item">The updated version of the data record.</param>
  public override Table Update(Table item) => (Table) this.Cache.Update((object) item);

  /// <summary>Deletes the data record by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Delete(System.Object)">Delete(object)</see>
  /// method on the cache. Returns the data record marked as
  /// deleted.</summary>
  /// <param name="item">The data record to delete.</param>
  public override Table Delete(Table item) => (Table) this.Cache.Delete((object) item);

  /// <summary>Searches the cache for the data record that has the same key
  /// fields as the provided data record, by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Locate(System.Object)">Locate(object)</see>
  /// method on the cache. Returns the data record if it is found in the
  /// cache or null otherwise.</summary>
  /// <param name="item">The data record that is searched in the cache by
  /// the values of its key fields.</param>
  public override Table Locate(Table item) => (Table) this.Cache.Locate((object) item);

  internal new PXResultset<Table> this[params object[] arguments] => this.Select(arguments);

  /// <summary>Retrieves the top data record of the data set that
  /// corresponds to the BQL statement.</summary>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override Table SelectSingle(params object[] arguments) => (Table) this.Select(arguments);

  /// <summary>Executes the BQL statement and retrieves all matching data
  /// records.</summary>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Select(params object[] arguments)
  {
    return this.selectBound((object[]) null, arguments);
  }

  /// <exclude />
  internal override PXResultset<Table> selectBound(object[] currents, params object[] arguments)
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    foreach (object obj in this.View.SelectMultiBound(currents, arguments))
    {
      if (!(obj is PXResult))
        pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(obj)));
      else
        pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(((PXResult) obj)[0])));
    }
    return pxResultset;
  }

  /// <summary>Searches for a data record by the value of specified field in the data set that corresponds to the BQL statement. The method extends the BQL statement with
  /// filtering and ordering by the specified field and retrieves the top data record.</summary>
  /// <param name="field0">The value of <tt>Field0</tt> by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0>(object field0, params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0>>(new object[1]
    {
      field0
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0, field1,field1">The values of <tt>Field0</tt> and
  /// <tt>Field1</tt> by which the data set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1>(
    object field0,
    object field1,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1>>>(new object[2]
    {
      field0,
      field1
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field2-field2">The values of
  /// <tt>Field0</tt>-<tt>Field2</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2>(
    object field0,
    object field1,
    object field2,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2>>>>(new object[3]
    {
      field0,
      field1,
      field2
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field3-field3">The values of
  /// <tt>Field0</tt>-<tt>Field3</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3>(
    object field0,
    object field1,
    object field2,
    object field3,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3>>>>>(new object[4]
    {
      field0,
      field1,
      field2,
      field3
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field4-field4">The values of
  /// <tt>Field0</tt>-<tt>Field4</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4>>>>>>(new object[5]
    {
      field0,
      field1,
      field2,
      field3,
      field4
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field5-field5">The values of
  /// <tt>Field0</tt>-<tt>Field5</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5>>>>>>>(new object[6]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field6-field6">The values of
  /// <tt>Field0</tt>-<tt>Field6</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6>>>>>>>>(new object[7]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field7-field7">The values of
  /// <tt>Field0</tt>-<tt>Field7</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7>>>>>>>>>(new object[8]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0 - field8-field8">The values of
  /// <tt>Field0</tt>-<tt>Field8</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8>>>>>>>>>>(new object[9]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields in the data set that corresponds to the BQL statement. The method extends the BQL statement with
  /// filtering and ordering by the specified fields and retrieves the top data record.</summary>
  /// <param name="field0 - field9-field9">The values of
  /// <tt>Field0</tt>-<tt>Field9</tt> by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the BQL statement.</param>
  public override PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    object field9,
    params object[] arguments)
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8, Asc<Field9>>>>>>>>>>>(new object[10]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8,
      field9
    }, 0, 1, arguments);
  }

  /// <summary>Searches the data set that corresponds to the BQL statement for all data records whose fields have the specified values. The fields are specified in the type
  /// parameter. The method extends the BQL statement with filtering and ordering by the fields and retrieves all data records from the resulting data set.</summary>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the BQL statement.</param>
  /// <remarks>Though ordering may seem superfluous here, it is needed for
  /// better performance of the selection from the database.</remarks>
  public override PXResultset<Table> SearchAll<Sort>(
    object[] searchValues,
    params object[] arguments)
  {
    return this.SearchWindowed<Sort>(searchValues, 0, 0, arguments);
  }

  /// <summary>Retrieves the specified number of contiguous data records
  /// starting from the given position in the filtered data set. The fields
  /// are specified in the type parameter. The method extends the BQL
  /// statement with filtering and ordering by the fields and requests the
  /// limited numer of data records.</summary>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> SearchWindowed<Sort>(
    object[] searchValues,
    int startRow,
    int totalRows,
    params object[] arguments)
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    int totalRows1 = 0;
    if (totalRows == 0)
      totalRows1 = -2;
    string[] columns;
    bool[] descendings;
    PXSelectBase<Table>.generateSort<Sort>(out columns, out descendings);
    PXFilterRow[] filters = (PXFilterRow[]) null;
    if (totalRows == 0)
    {
      filters = new PXFilterRow[columns.Length < searchValues.Length ? columns.Length : searchValues.Length];
      for (int index = 0; index < filters.Length; ++index)
        filters[index] = new PXFilterRow(columns[index], PXCondition.EQ, searchValues[index]);
      searchValues = (object[]) null;
    }
    System.Type itemType = this.View.CacheGetItemType();
    foreach (object o in this.View.Select((object[]) null, arguments, searchValues, columns, descendings, filters, ref startRow, totalRows, ref totalRows1))
    {
      if (!(o is PXResult))
      {
        if (itemType.IsInstanceOfType(o))
          pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(o)));
      }
      else
        pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(((PXResult) o)[0])));
    }
    return pxResultset;
  }

  /// <summary>Retrieves the specified number of data records starting from
  /// the given position.</summary>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public override PXResultset<Table> SelectWindowed(
    int startRow,
    int totalRows,
    params object[] arguments)
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    System.Type itemType = this.View.CacheGetItemType();
    int totalRows1 = 0;
    foreach (object o in this.View.Select((object[]) null, arguments, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, totalRows, ref totalRows1))
    {
      if (!(o is PXResult))
      {
        if (itemType.IsInstanceOfType(o))
          pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(o)));
      }
      else
        pxResultset.Add(new PXResult<Table>(this.View.Cache.GetExtension<Table>(((PXResult) o)[0])));
    }
    return pxResultset;
  }

  internal override PXResultset<Table> this[int argument]
  {
    get => this[new object[1]{ (object) argument }];
  }

  internal override PXResultset<Table> this[string arguments]
  {
    get => this[PXSelectBase.ParseString(arguments)];
  }

  /// <summary>Returns the type of the DAC provided as the type parameter of <tt>PXSelectBase&lt;&gt;</tt> class. For BQL statements that are derived from
  /// <tt>PXSelectBase&lt;&gt;</tt>, it is the first mentioned DAC.</summary>
  public override System.Type GetItemType() => typeof (Table);

  /// <summary>Sets the value of the specified field in the given data
  /// record. The method relies on the <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)">SetValueExt(object,string, object)</see>
  /// method of the cache.</summary>
  /// <param name="row">The data record whose field value is set.</param>
  /// <param name="value">The value to set to the field.</param>
  public override void SetValueExt<Field>(Table row, object value)
  {
    this.SetValueExt((object) row, typeof (Field).Name, value);
  }

  /// <summary>Gets the value of the specified field for the given data
  /// record. The method relies on the <see cref="M:PX.Data.PXCache.GetValueExt``1(System.Object)">GetValueExt&lt;Field&gt;(object)</see>
  /// method of the cache, but unlike the cache's method always
  /// returns a value, not a <tt>PXFieldState</tt> object.</summary>
  /// <param name="row">The data record whose field value is
  /// returned.</param>
  public override object GetValueExt<Field>(Table row)
  {
    return this.GetValueExt((object) row, typeof (Field).Name);
  }
}
