// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DocumentListBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public abstract class DocumentListBase<T> : List<T> where T : class
{
  protected PXGraph _Graph;

  public DocumentListBase(PXGraph Graph) => this._Graph = Graph;

  protected abstract object GetValue(object data, Type field);

  protected virtual object GetValue<Field>(object data) where Field : IBqlField
  {
    return this.GetValue(data, typeof (Field));
  }

  public abstract T Find(object item);

  public new int IndexOf(T item) => base.IndexOf(this.Find((object) item));

  public T Find<Field1>(params object[] values) where Field1 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0])));
  }

  public T Find<Field1, Field2>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1])));
  }

  public T Find<Field1, Field2, Field3>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2])));
  }

  public T Find<Field1, Field2, Field3, Field4>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7>(params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
    where Field11 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9]) && object.Equals(this.GetValue<Field11>((object) data), values[10])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
    where Field11 : IBqlField
    where Field12 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9]) && object.Equals(this.GetValue<Field11>((object) data), values[10]) && object.Equals(this.GetValue<Field12>((object) data), values[11])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
    where Field11 : IBqlField
    where Field12 : IBqlField
    where Field13 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9]) && object.Equals(this.GetValue<Field11>((object) data), values[10]) && object.Equals(this.GetValue<Field12>((object) data), values[11]) && object.Equals(this.GetValue<Field13>((object) data), values[12])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
    where Field11 : IBqlField
    where Field12 : IBqlField
    where Field13 : IBqlField
    where Field14 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9]) && object.Equals(this.GetValue<Field11>((object) data), values[10]) && object.Equals(this.GetValue<Field12>((object) data), values[11]) && object.Equals(this.GetValue<Field13>((object) data), values[12]) && object.Equals(this.GetValue<Field14>((object) data), values[13])));
  }

  public T Find<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15>(
    params object[] values)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
    where Field10 : IBqlField
    where Field11 : IBqlField
    where Field12 : IBqlField
    where Field13 : IBqlField
    where Field14 : IBqlField
    where Field15 : IBqlField
  {
    return this.Find((Predicate<T>) (data => object.Equals(this.GetValue<Field1>((object) data), values[0]) && object.Equals(this.GetValue<Field2>((object) data), values[1]) && object.Equals(this.GetValue<Field3>((object) data), values[2]) && object.Equals(this.GetValue<Field4>((object) data), values[3]) && object.Equals(this.GetValue<Field5>((object) data), values[4]) && object.Equals(this.GetValue<Field6>((object) data), values[5]) && object.Equals(this.GetValue<Field7>((object) data), values[6]) && object.Equals(this.GetValue<Field8>((object) data), values[7]) && object.Equals(this.GetValue<Field9>((object) data), values[8]) && object.Equals(this.GetValue<Field10>((object) data), values[9]) && object.Equals(this.GetValue<Field11>((object) data), values[10]) && object.Equals(this.GetValue<Field12>((object) data), values[11]) && object.Equals(this.GetValue<Field13>((object) data), values[12]) && object.Equals(this.GetValue<Field14>((object) data), values[13]) && object.Equals(this.GetValue<Field15>((object) data), values[14])));
  }

  public T Find(params FieldLookup[] values)
  {
    return this.Find((Predicate<T>) (data => ((IEnumerable<FieldLookup>) values).All<FieldLookup>((Func<FieldLookup, bool>) (fld => object.Equals(fld is ICustomFieldLookup customFieldLookup ? customFieldLookup.GetValue(this._Graph, typeof (T), (object) data) : this.GetValue((object) data, fld.Field), fld.Value)))));
  }
}
