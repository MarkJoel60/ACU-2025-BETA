// Decompiled with JetBrains decompiler
// Type: PX.Common.Parser.ExpressionContext
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Common.Parser;

/// <summary>Provides the base class of the expression context.</summary>
public class ExpressionContext : ParserContext, ICustomTypeDescriptor
{
  private static PropertyDescriptorCollection \u0002;

  /// <summary>
  /// Return line number from PXContext (for import/export scenarios)
  /// </summary>
  public virtual object LineNbr(FunctionContext context)
  {
    return (object) PXContext.GetSlot<int>(nameof (LineNbr));
  }

  /// <summary>
  /// Gets the current DateTime object using user's timezone.
  /// </summary>
  public virtual object Now(FunctionContext context) => (object) PXTimeZoneInfo.Now;

  /// <summary>Gets the current DateTime object.</summary>
  public virtual object NowUTC(FunctionContext context) => (object) PXTimeZoneInfo.UtcNow;

  /// <summary>Gets the current date using user's timezone.</summary>
  public virtual object Today(FunctionContext context) => (object) PXTimeZoneInfo.Today;

  /// <summary>Gets the current date.</summary>
  public virtual object TodayUTC(FunctionContext context) => (object) PXTimeZoneInfo.UtcToday;

  AttributeCollection ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetAttributes((object) this, true);
  }

  string ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetClassName((object) this, true);
  }

  string ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u000E()
  {
    return TypeDescriptor.GetComponentName((object) this, true);
  }

  TypeConverter ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetConverter((object) this, true);
  }

  EventDescriptor ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetDefaultEvent((object) this, true);
  }

  PropertyDescriptor ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  object ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002(Type _param1)
  {
    return TypeDescriptor.GetEditor((object) this, _param1, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return TypeDescriptor.GetEvents((object) this, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002(
    Attribute[] _param1)
  {
    return TypeDescriptor.GetEvents((object) this, _param1, true);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002()
  {
    return this.\u000E();
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002(
    Attribute[] _param1)
  {
    return this.\u000E();
  }

  object ICustomTypeDescriptor.cqfvft4y7btvc6jnqwhseqpwcyjucfh8\u2009\u2009\u2009\u0002(
    PropertyDescriptor _param1)
  {
    return (object) this;
  }

  private PropertyDescriptorCollection \u000E()
  {
    if (this.Properties == null)
    {
      if (ExpressionContext.\u0002 == null)
        ExpressionContext.\u0002 = TypeDescriptor.GetProperties((object) this, true);
      PropertyDescriptor[] properties = new PropertyDescriptor[ExpressionContext.\u0002.Count + this.CustomProperties.Count];
      ExpressionContext.\u0002.CopyTo((Array) properties, 0);
      int count = ExpressionContext.\u0002.Count;
      foreach (KeyValuePair<string, object> customProperty in this.CustomProperties)
        properties[count++] = (PropertyDescriptor) new ParserContext.CustomDescriptor(customProperty.Key, customProperty.Value.GetType(), (Attribute[]) null);
      this.Properties = new PropertyDescriptorCollection(properties, true);
    }
    return this.Properties;
  }
}
